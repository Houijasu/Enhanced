namespace SDK.Interfaces
{
   using EnsoulSharp.SDK;

   public interface ISpell
   {
      Spell Q { get; set; }
      Spell Q2 { get; set; }
      Spell W { get; set; }
      Spell W2 { get; set; }
      Spell E { get; set; }
      Spell E2 { get; set; }
      Spell R { get; set; }
      Spell R2 { get; set; }
      void InitializeSpells();
   }
}