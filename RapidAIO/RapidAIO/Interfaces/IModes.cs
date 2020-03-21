namespace RapidAIO.Interfaces
{
   public interface IModes
   {
   #region Orbwalker

      void Combo();
      void Harass();
      void LaneClear();
      void LastHit();

   #endregion

   #region Miscellaneous

      void KillSteal();
      void Flee();

   #endregion
   }
}