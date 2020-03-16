namespace Samaritan.Utilities.Extensions
{
   using System;
   using System.Diagnostics.CodeAnalysis;
   using System.Linq;

   using EnsoulSharp;
   using EnsoulSharp.SDK;

   [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
   public static class Spell
   {
      public static AIHeroClient GetTarget(this EnsoulSharp.SDK.Spell spell)
      {
         var target = ObjectManager
                     .Get<AIHeroClient>()
                     .Where(unit => unit.IsValidTarget(spell.Range))
                     .OrderByDescending(TargetSelector.GetPriority)
                     .FirstOrDefault();

         return TargetSelector.SelectedTarget ?? target;
      }

      public static void CastSkillShot(this EnsoulSharp.SDK.Spell spell, Predicate<AIHeroClient> unit = null)
      {
         if (spell == default(EnsoulSharp.SDK.Spell) || !spell.IsReady())
         {
            return;
         }

         var target = GetTarget(spell);

         if (!target.IsValidTarget(spell.Range))
         {
            return;
         }

         var prediction = spell.GetPrediction(target);

         if (!HitChance.IsValidHitChance(prediction))
         {
            return;
         }

         if (unit != null && !unit.Invoke(target))
         {
            return;
         }

         spell.Cast(prediction.CastPosition);
      }
   }
}