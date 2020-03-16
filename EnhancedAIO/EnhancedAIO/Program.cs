namespace EnhancedAIO
{
   using System.Diagnostics.CodeAnalysis;

   using Samaritan.Interfaces;

   using static Samaritan.Static.Script;

   [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
   internal class Program : IScript
   {
      private static Program Assembly { get; } = new Program();

      public void Load()
      {
         Name             = "Enhanced AIO";
         Author           = "Toshiya Joushima";
         ChampionAssembly = Initialize();
      }

      [SuppressMessage("ReSharper", "UnusedParameter.Global")]
      internal static void Main(string[] args)
      {
         Assembly.Load();
      }
   }
}