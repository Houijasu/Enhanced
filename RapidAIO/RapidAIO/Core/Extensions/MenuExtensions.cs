namespace RapidAIO.Core.Extensions
{
   using EnsoulSharp.SDK;
   using EnsoulSharp.SDK.MenuUI;
   using EnsoulSharp.SDK.MenuUI.Values;

   internal static class MenuExtensions
   {
      internal static Menu GenerateMenu(this Spell spell, OrbwalkerMode orbwalkerMode)
      {
         return new Menu($"{spell.Slot}", $"{spell.Slot}");
      }

      internal static Menu AddComponent<T>(this Menu menu, string name, string displayName = null) where T : AMenuComponent
      {
         var component = default(AMenuComponent);
         displayName = displayName ?? name;

         if (typeof(T) == typeof(MenuBool))
         {
            component = AddMenuBool(menu, name, displayName);
         }

         menu.Add(component);
         return menu;
      }

      internal static MenuBool GetMenuBool(this Menu menu, Spell spell, string name)
      {
         return menu[$"{spell.Slot}"].GetValue<MenuBool>(name);
      }

      private static Menu AddMenuBool(this Menu menu, string name, string displayName)
      {
         var result = new MenuBool(name, displayName);
         menu.Add(result);
         return menu;
      }
   }
}