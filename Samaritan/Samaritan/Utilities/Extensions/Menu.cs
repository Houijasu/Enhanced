namespace Samaritan.Utilities.Extensions
{
   using System;
   using System.Collections.Generic;
   using System.Linq;

   using EnsoulSharp.SDK.MenuUI;
   using EnsoulSharp.SDK.MenuUI.Values;

   public static class Menu
   {
      private static string[] HitChances { get; } = Enum
                                                   .GetValues(typeof(EnsoulSharp.SDK.Prediction.HitChance))
                                                   .Cast<EnsoulSharp.SDK.Prediction.HitChance>()
                                                   .Where(chance => chance > EnsoulSharp.SDK.Prediction.HitChance.OutOfRange)
                                                   .TakeWhile(chance => chance < EnsoulSharp.SDK.Prediction.HitChance.Dash)
                                                   .Select(chance => chance.ToString())
                                                   .ToArray();

      private static List<AMenuComponent> SpellMenuComponents { get; } = new List<AMenuComponent>
      {
         new MenuBool("Cast", "Cast").SetValue(true),
         new MenuBool("GapClose", "On Gap Close").SetValue(false),
         new MenuBool("CC", "On CC").SetValue(false),
         new MenuBool("KillSteal", "KillSteal").SetValue(true),
         new MenuList("HitChance", "HitChance", HitChances).SetValue((int)EnsoulSharp.SDK.Prediction.HitChance.High)
      };

      private static void AddSpellMenuComponents(this EnsoulSharp.SDK.MenuUI.Menu menu)
      {
         foreach (var component in SpellMenuComponents)
         {
            menu.Add(component);
         }
      }

      public static EnsoulSharp.SDK.MenuUI.Menu AddSpellMenu(this EnsoulSharp.SDK.MenuUI.Menu menu, EnsoulSharp.SDK.Spell spell)
      {
         var spellMenu = new EnsoulSharp.SDK.MenuUI.Menu($"{spell.Slot}", $"{spell.Slot}");
         spellMenu.AddSpellMenuComponents();
         menu.Add(spellMenu);

         return menu;
      }
   }
}