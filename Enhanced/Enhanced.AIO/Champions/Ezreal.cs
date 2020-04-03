namespace Enhanced.Champions
{
   using System.Diagnostics.CodeAnalysis;

   using EnsoulSharp;
   using EnsoulSharp.SDK;

   using SDK.Abstractions;

   [SuppressMessage("ReSharper", "UnusedType.Global")]
   internal sealed class Ezreal : Champion
   {
      public override void InitializeSpells()
      {
         Q = new Spell(SpellSlot.Q, 1150f) { DamageType = DamageType.Physical };
         Q.SetSkillshot(0.25f, 80f, 2000f, true, SpellType.Line);
      }

      public override void Combo()
      {
         var target = Q.GetTarget();

         if (!target.IsValidTarget(Q.Range))
         {
            return;
         }

         var prediction = Q.GetPrediction(target);

         if (prediction.Hitchance < HitChance.Low)
         {
            return;
         }

         Q.Cast(prediction.CastPosition);
      }
   }
}