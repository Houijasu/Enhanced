namespace RapidAIO.Abstractions
{
   using System;

   using EnsoulSharp;
   using EnsoulSharp.SDK;
   using EnsoulSharp.SDK.MenuUI;

   using Interfaces;

   using static Core.Main;

   using static EnsoulSharp.SDK.Interrupter;

   using GapCloser = EnsoulSharp.SDK.Gapcloser;

   internal abstract class Champion : IEvent, ISpell, IMenu, IModes
   {
      protected Champion()
      {
         InitializeEvents();
         InitializeSpells();
         InitializeMenus();

         Game.Print($"{Name} | {Self.CharacterName} is loaded.");
      }

      private AIHeroClient Self { get; } = ObjectManager.Player;

   #region Events

      public virtual void InitializeEvents()
      {
         /* Main */
         Game.OnUpdate += OnUpdate;

         /* Drawings */
         Drawing.OnDraw     += OnDraw;
         Drawing.OnPresent  += OnDrawPresent;
         Drawing.OnEndScene += OnDrawEndScene;

         /* Gap Closer */
         GapCloser.OnGapcloser += OnGapClose;

         /* AIBaseClient */
         AIBaseClient.OnProcessSpellCast += OnProcessSpellCast;
         AIBaseClient.OnBuffGain         += OnBuffGain;

         /* Spell Book */
         Spellbook.OnCastSpell += OnCastSpell;

         /* Interrupter */
         Interrupter.OnInterrupterSpell += OnInterrupterSpell;

         /* GameObject */
         GameObject.OnMissileCreate += OnMissileCreate;
         GameObject.OnCreate        += OnGameObjectCreate;
         GameObject.OnDelete        += OnGameObjectDelete;

         /* Orbwalker */
         Orbwalker.OnAction += OnOrbwalkerAction;
      }

      public virtual void OnUpdate(EventArgs args)
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

      public virtual void OnDrawEndScene(EventArgs args) { }
      public virtual void OnBuffGain(AIBaseClient sender, AIBaseClientBuffGainEventArgs args) { }
      public virtual void OnGameObjectCreate(GameObject sender, EventArgs args) { }
      public virtual void OnMissileCreate(GameObject sender, EventArgs args) { }
      public virtual void OnGameObjectDelete(GameObject sender, EventArgs args) { }
      public virtual void OnOrbwalkerAction(object sender, OrbwalkerActionArgs args) { }
      public virtual void OnInterrupterSpell(AIHeroClient sender, InterruptSpellArgs args) { }
      public virtual void OnGapClose(AIHeroClient sender, Gapcloser.GapcloserArgs args) { }
      public virtual void OnDraw(EventArgs args) { }
      public virtual void OnDrawPresent(EventArgs args) { }
      public virtual void OnProcessSpellCast(AIBaseClient sender, AIBaseClientProcessSpellCastEventArgs args) { }
      public virtual void OnCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args) { }

   #endregion

   #region Spells

      public virtual Spell Q { get; set; } = new Spell(SpellSlot.Q);
      public virtual Spell Q2 { get; set; } = new Spell(SpellSlot.Q);
      public virtual Spell W { get; set; } = new Spell(SpellSlot.W);
      public virtual Spell W2 { get; set; } = new Spell(SpellSlot.W);
      public virtual Spell E { get; set; } = new Spell(SpellSlot.E);
      public virtual Spell E2 { get; set; } = new Spell(SpellSlot.E);
      public virtual Spell R { get; set; } = new Spell(SpellSlot.R);
      public virtual Spell R2 { get; set; } = new Spell(SpellSlot.R);

      public abstract void InitializeSpells();

   #endregion

   #region Menus

      public virtual Menu MainMenu { get; set; } = new Menu(Name, $"{Name} | {Player.CharacterName}", true);
      public virtual Menu ComboMenu { get; set; } = new Menu(nameof(OrbwalkerMode.Combo), nameof(OrbwalkerMode.Combo));
      public virtual Menu HarassMenu { get; set; } = new Menu(nameof(OrbwalkerMode.Harass), nameof(OrbwalkerMode.Harass));
      public virtual Menu LaneClearMenu { get; set; } = new Menu(nameof(OrbwalkerMode.LaneClear), nameof(OrbwalkerMode.LaneClear));
      public virtual Menu LastHitMenu { get; set; } = new Menu(nameof(OrbwalkerMode.LastHit), nameof(OrbwalkerMode.LastHit));

      public abstract void InitializeMenus();

   #endregion

   #region Modes

      public abstract void Combo();
      public virtual void Harass() { }
      public virtual void LaneClear() { }
      public virtual void LastHit() { }
      public virtual void KillSteal() { }
      public virtual void Flee() { }

   #endregion
   }
}