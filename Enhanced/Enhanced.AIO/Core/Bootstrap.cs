namespace Enhanced.Core
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;

   using EnsoulSharp;

   using SDK.Abstractions;

   internal static class Bootstrap
   {
      private static IEnumerable<Type> SupportedChampions { get; } = Assembly
                                                                    .GetExecutingAssembly()
                                                                    .GetTypes()
                                                                    .Where(type => type.IsClass && !type.IsAbstract)
                                                                    .Where(type => type.BaseType == typeof(Champion) && typeof(Champion).IsAssignableFrom(type));

      internal static Champion Champion { get; set; }

      private static Champion Initialize()
      {
         if (!SupportedChampions.Any())
         {
            return null;
         }

         var type = SupportedChampions.FirstOrDefault(x => x.Name.Equals(ObjectManager.Player.CharacterName, StringComparison.OrdinalIgnoreCase));

         if (type == null)
         {
            return null;
         }

         return (Champion)DynamicInitializer.NewInstance(type);
      }

      internal static void Init() => Champion = Initialize();
   }
}