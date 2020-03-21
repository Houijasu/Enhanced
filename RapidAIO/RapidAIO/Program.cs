namespace RapidAIO
{
   using EnsoulSharp.SDK;

   using Bootstrap = Core.Bootstrap;

   internal static class Program
   {
      public static void Main(string[] args)
      {
         GameEvent.OnGameLoad += Bootstrap.Init;
      }
   }
}