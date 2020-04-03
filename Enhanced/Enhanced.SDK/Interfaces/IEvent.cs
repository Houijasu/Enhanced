namespace SDK.Interfaces
{
   using System;

   using EnsoulSharp;

   public interface IEvent
   {
      void InitializeEvents();
      void OnGameUpdate(EventArgs args);
      void OnGameObjectCreate(GameObject sender, EventArgs args);
      void OnGameObjectDelete(GameObject sender, EventArgs args);
      void OnGameObjectMissileCreate(GameObject sender, EventArgs args);
   }
}