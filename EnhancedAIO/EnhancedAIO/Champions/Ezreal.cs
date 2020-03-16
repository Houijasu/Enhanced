namespace EnhancedAIO.Champions
{
   using EnsoulSharp.SDK.Prediction;

   using Samaritan.Abstractions;
   using Samaritan.Utilities.Extensions;

   internal class Ezreal : Champion
   {
      protected override void InitializeSpells()
      {
         Q.Range = 1150f;
         Q.SetSkillshot(0.25f, 80f, 2000f, true, true, SkillshotType.Line);

         W.Range = 1000f;
         W.SetSkillshot(0.25f, 80f, 1150f, false, true, SkillshotType.Line);

         E.Range = 475f;
         E.Delay = 0.65f;

         R.Range = 25000;
         R.SetSkillshot(1f, 160f, 2000f, false, true, SkillshotType.Line);
      }

      protected override void InitializeMenu()
      {
         ComboMenu.AddSpellMenu(Q);

         MainMenu.Add(ComboMenu);

         MainMenu.Attach();
      }

      public string Wow { get; set; } = "Wow";

      protected override void Combo()
      {
         Q.CastSkillShot();

         W.CastSkillShot();
      }
   }
}