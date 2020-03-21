namespace RapidAIO.Core
{
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;

   using Abstractions;

   using EnsoulSharp;

   internal static class Main
   {
      public static string Name { get; } = "Rapid AIO";

      private static IEnumerable<string> SupportedChampions { get; } = Assembly
                                                                      .GetCallingAssembly()
                                                                      .GetTypes()
                                                                      .Where(type => type.BaseType == typeof(Champion))
                                                                      .Select(type => type.Name);

      internal static AIHeroClient Player { get; } = ObjectManager.Player;

      public static bool IsSupported { get; } = SupportedChampions.Contains(Player.CharacterName);
   }
}