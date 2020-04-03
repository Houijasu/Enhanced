namespace SDK.Abstractions
{
   using System;
   using System.Diagnostics.CodeAnalysis;

   using EnsoulSharp;
   using EnsoulSharp.SDK;

   using Interfaces;

   using SharpDX;

   public abstract class Champion : IEvent, ISpell, IMenu, IModes
   {
      protected Champion()
      {
         InitializeEvents();
         InitializeSpells();
         InitializePrediction();

         Game.Print($"Enhanced.AIO | {Self.CharacterName} loaded!", Color.BlueViolet);
      }

      protected AIHeroClient Self { get; } = ObjectManager.Player;

      public virtual void InitializeEvents()
      {
         Game.OnUpdate              += OnGameUpdate;
         GameObject.OnCreate        += OnGameObjectCreate;
         GameObject.OnDelete        += OnGameObjectDelete;
         GameObject.OnMissileCreate += OnGameObjectMissileCreate;
      }

      public virtual void KillSteal() { }

      [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Local")]
      private void InitializePrediction() => Samaritan.Load();

   #region IEvent Delegates

      public virtual void OnGameUpdate(EventArgs args)
      {
         if (Self.IsDead || Self.IsRecalling() || MenuGUI.IsChatOpen)
         {
            return;
         }

         KillSteal();

         switch (Orbwalker.ActiveMode)
         {
            case OrbwalkerMode.Combo:
               Combo();
               break;
            case OrbwalkerMode.Harass:
               Harass();
               break;
            case OrbwalkerMode.LaneClear:
               LaneClear();
               break;
            case OrbwalkerMode.LastHit:
               LastHit();
               break;
            case OrbwalkerMode.None:
               // Ignore
               break;
         }
      }

      public virtual void OnGameObjectCreate(GameObject sender, EventArgs args) { }
      public virtual void OnGameObjectDelete(GameObject sender, EventArgs args) { }
      public virtual void OnGameObjectMissileCreate(GameObject sender, EventArgs args) { }

   #endregion

   #region Spells

      public virtual Spell Q { get; set; }
      public virtual Spell Q2 { get; set; }
      public virtual Spell W { get; set; }
      public virtual Spell W2 { get; set; }
      public virtual Spell E { get; set; }
      public virtual Spell E2 { get; set; }
      public virtual Spell R { get; set; }
      public virtual Spell R2 { get; set; }

      public abstract void InitializeSpells();

   #endregion

   #region IMode Delegates

      public abstract void Combo();
      public virtual void Harass() { }
      public virtual void LaneClear() { }
      public virtual void LastHit() { }
      public virtual void Flee() { }

   #endregion
   }
}