namespace RapidAIO.Interfaces
{
   using System;

   using EnsoulSharp;
   using EnsoulSharp.SDK;

   internal interface IEvent
   {
      void InitializeEvents();
      void OnUpdate(EventArgs args);
      void OnDrawEndScene(EventArgs args);
      void OnBuffGain(AIBaseClient sender, AIBaseClientBuffGainEventArgs args);
      void OnGameObjectCreate(GameObject sender, EventArgs args);
      void OnMissileCreate(GameObject sender, EventArgs args);
      void OnGameObjectDelete(GameObject sender, EventArgs args);
      void OnOrbwalkerAction(object sender, OrbwalkerActionArgs args);
      void OnInterrupterSpell(AIHeroClient sender, Interrupter.InterruptSpellArgs args);
      void OnGapClose(AIHeroClient sender, Gapcloser.GapcloserArgs args);
      void OnDraw(EventArgs args);
      void OnDrawPresent(EventArgs args);
      void OnProcessSpellCast(AIBaseClient sender, AIBaseClientProcessSpellCastEventArgs args);
      void OnCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args);
   }
}