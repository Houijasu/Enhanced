namespace Samaritan.Static
{
   using System;
   using System.Diagnostics.CodeAnalysis;
   using System.Linq;
   using System.Reflection;

   using Abstractions;

   using EnsoulSharp;

   [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
   public static class Script
   {
      public static string Name { get; set; }

      public static Champion ChampionAssembly { get; set; }

      public static string Author { get; set; }

      public static string ChampionName { get; set; }

      public static Champion Initialize()
      {
         var championAssembly = Assembly
                               .GetCallingAssembly()
                               .GetTypes()
                               .FirstOrDefault(type => type.Name.Equals(ObjectManager.Player.CharacterName));

         if (championAssembly != null)
         {
            ChampionName = championAssembly.Name;
            return (Champion)championAssembly.InvokeMember(championAssembly.Name, BindingFlags.CreateInstance, Type.DefaultBinder, null, null);
         }

         Console.WriteLine($"{Name} | Champion is not supported.");
         return default(Champion);
      }
   }
}