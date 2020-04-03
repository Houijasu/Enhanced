namespace SDK.Interfaces
{
   using Abstractions;

   public interface IScript
   {
      string Name { get; }

      Champion ChampionAssembly { get; set; }
   }
}