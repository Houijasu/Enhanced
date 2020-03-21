namespace RapidAIO.Interfaces
{
   using EnsoulSharp.SDK.MenuUI;

   public interface IMenu
   {
      Menu MainMenu { get; set; }
      Menu ComboMenu { get; set; }
      Menu HarassMenu { get; set; }
      Menu LaneClearMenu { get; set; }
      Menu LastHitMenu { get; set; }
      void InitializeMenus();
   }
}