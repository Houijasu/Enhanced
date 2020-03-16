namespace Samaritan.Utilities.Extensions
{
   using static EnsoulSharp.SDK.Prediction.SpellPrediction;

   public static class HitChance
   {
      internal static bool IsValidHitChance(PredictionOutput predictionOutput)
      {
         return predictionOutput.Hitchance != EnsoulSharp.SDK.Prediction.HitChance.None &&
               predictionOutput.Hitchance != EnsoulSharp.SDK.Prediction.HitChance.OutOfRange &&
               predictionOutput.Hitchance != EnsoulSharp.SDK.Prediction.HitChance.Collision;
      }
   }
}