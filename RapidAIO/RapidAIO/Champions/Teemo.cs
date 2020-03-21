namespace RapidAIO.Champions
{
   using Abstractions;

   using Core.Extensions;

   using EnsoulSharp;
   using EnsoulSharp.SDK;
   using EnsoulSharp.SDK.MenuUI.Values;

   internal sealed class Teemo : Champion
   {
      public override void InitializeSpells() { }

      public override void InitializeMenus()
      {
         var comboQ = Q
                     .GenerateMenu(OrbwalkerMode.Combo)
                     .AddComponent<MenuBool>("Cast");

         ComboMenu.Add(comboQ);

         MainMenu.Add(ComboMenu);

         MainMenu.Attach();
      }

      public override void Combo()
      {
         if (!ComboMenu.GetMenuBool(Q, "Cast"))
         {
            return;
         }

         Game.Print("Cast Q");
      }

      public static Champion Initialize()
      {
         return new Teemo();
      }
   }
}