namespace Samaritan.Abstractions
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;
   using System.Linq;

   using EnsoulSharp;
   using EnsoulSharp.SDK;
   using EnsoulSharp.SDK.MenuUI;

   using Static;

   using static EnsoulSharp.SDK.Interrupter;

   [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
   [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
   public abstract class Champion
   {
      protected internal Champion()
      {
         Game.Print($"{Script.Name} | {Script.ChampionName} loaded.");

         InitializeEvents();
         InitializeSpells();
         InitializeMenu();

         Spells = GetValidSpells();
      }

      protected internal virtual IEnumerable<Spell> Spells { get; }
      protected internal AIHeroClient Self { get; } = ObjectManager.Player;

      private List<Spell> GetValidSpells()
      {
         return new List<Spell> { Q, W, E, R, Q2, W2, E2, R2 }
               .Where(spell => Math.Abs(spell.Range - float.MaxValue) > 0)
               .ToList();
      }

      protected abstract void InitializeSpells();
      protected virtual void InitializeMenu() { }

      protected virtual void InitializeEvents()
      {
         /* Main */
         Game.OnUpdate += OnUpdate;

         /* Drawings */
         Drawing.OnDraw     += OnDraw;
         Drawing.OnPresent  += OnDrawPresent;
         Drawing.OnEndScene += OnDrawEndScene;

         /* Gap Closer */
         Gapcloser.OnGapcloser += OnGapClose;

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


   #region Miscellaneous Modes

      protected virtual void KillSteal() { }

      protected virtual void Flee() { }

   #endregion

   #region Event Methods

      protected virtual void OnDrawEndScene(EventArgs args) { }
      protected virtual void OnBuffGain(AIBaseClient sender, AIBaseClientBuffGainEventArgs args) { }
      protected virtual void OnGameObjectCreate(GameObject sender, EventArgs args) { }
      protected virtual void OnMissileCreate(GameObject sender, EventArgs args) { }
      protected virtual void OnGameObjectDelete(GameObject sender, EventArgs args) { }
      protected virtual void OnOrbwalkerAction(object sender, OrbwalkerActionArgs args) { }
      protected virtual void OnInterrupterSpell(AIHeroClient sender, InterruptSpellArgs args) { }
      protected virtual void OnGapClose(AIHeroClient sender, Gapcloser.GapcloserArgs args) { }
      protected virtual void OnDraw(EventArgs args) { }
      protected virtual void OnDrawPresent(EventArgs args) { }
      protected virtual void OnProcessSpellCast(AIBaseClient sender, AIBaseClientProcessSpellCastEventArgs args) { }
      protected virtual void OnCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args) { }

      [SuppressMessage("ReSharper", "SwitchStatementHandlesSomeKnownEnumValuesWithDefault")]
      protected virtual void OnUpdate(EventArgs args)
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

   #endregion

   #region Spells

      protected virtual Spell Q { get; set; } = new Spell(SpellSlot.Q);
      protected virtual Spell Q2 { get; set; } = new Spell(SpellSlot.Q);
      protected virtual Spell W { get; set; } = new Spell(SpellSlot.W);
      protected virtual Spell W2 { get; set; } = new Spell(SpellSlot.W);
      protected virtual Spell E { get; set; } = new Spell(SpellSlot.E);
      protected virtual Spell E2 { get; set; } = new Spell(SpellSlot.E);
      protected virtual Spell R { get; set; } = new Spell(SpellSlot.R);
      protected virtual Spell R2 { get; set; } = new Spell(SpellSlot.R);

   #endregion

   #region Orbwalker Modes

      protected abstract void Combo();
      protected virtual void Harass() { }
      protected virtual void LaneClear() { }
      protected virtual void LastHit() { }

   #endregion

   #region Menus

      protected virtual Menu MainMenu { get; set; } = new Menu(Script.Name, $"{Script.Name} | {Script.ChampionName}", true);

      protected virtual Menu ComboMenu { get; set; } = new Menu(nameof(OrbwalkerMode.Combo), nameof(OrbwalkerMode.Combo));

      protected virtual Menu HarassMenu { get; set; } = new Menu(nameof(OrbwalkerMode.Harass), nameof(OrbwalkerMode.Harass));

      protected virtual Menu LaneClearMenu { get; set; } = new Menu(nameof(OrbwalkerMode.LaneClear), nameof(OrbwalkerMode.LaneClear));

      protected virtual Menu LastHitMenu { get; set; } = new Menu(nameof(OrbwalkerMode.LastHit), nameof(OrbwalkerMode.LastHit));

   #endregion
   }
}