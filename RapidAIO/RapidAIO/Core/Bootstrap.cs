namespace RapidAIO.Core
{
   using Champions;

   using EnsoulSharp;

   using static Main;

   internal static class Bootstrap
   {
      internal static void Init()
      {
         switch (Player.CharacterName)
         {
            case "Teemo":
               Teemo.Initialize();
               break;
            default:
               Game.Print($"{Name} | {Player.CharacterName} is not supported.");
               break;
         }
      }
   }
}