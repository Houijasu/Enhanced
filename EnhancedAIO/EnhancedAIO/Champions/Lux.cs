namespace EnhancedAIO.Champions
{
   using EnsoulSharp.SDK.Prediction;

   using Samaritan.Abstractions;
   using Samaritan.Utilities.Extensions;

   public class Lux : Champion
   {
      protected override void InitializeSpells()
      {
         Q.Range = 1175f;
         Q.SetSkillshot(0.25f, 60f, 1200f, true, true, SkillshotType.Line);

         W.Range = 1075f;
         W.SetSkillshot(0.25f, 120f, 1400f, false, true, SkillshotType.Line);

         E.Range = 1000f;
         E.SetSkillshot(0.25f, 310f, 1300f, false, true, SkillshotType.Circle);

         R.Range = 3340;
         E.SetSkillshot(0.25f, 115, float.MaxValue, false, true, SkillshotType.Circle);
      }

      protected override void Combo()
      {
         // Ignore
      }

      protected override void InitializeMenu()
      {
         ComboMenu.AddSpellMenu(Q);

         MainMenu.Add(ComboMenu);

         MainMenu.Attach();
      }
   }
}