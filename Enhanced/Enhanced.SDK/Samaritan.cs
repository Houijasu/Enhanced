namespace SDK
{
   using System;
   using System.Collections.Generic;
   using System.Linq;

   using EnsoulSharp;
   using EnsoulSharp.SDK;

   using Extensions;

   using SharpDX;

   public sealed class Samaritan : IPrediction
   {
      private Samaritan() { }

      private static string Name { get; } = "Samaritan";

      public static IPrediction Instance { get; set; }

      public PredictionOutput GetPrediction(PredictionInput input)
      {
         var result = new PredictionOutput { Hitchance = HitChance.Low };

         var target = input.Unit;

         if (target.IsMoving)
         {
            MovementPrediction(ref result, input);
         }
         else
         {
            IdlePrediction(ref result, input);
         }

         CheckCollision(ref result, input);
         CheckRange(ref result, input);

         return result;
      }

      public PredictionOutput GetPrediction(PredictionInput input, bool ft, bool checkCollision) => GetPrediction(input);

      private void CheckRange(ref PredictionOutput result, PredictionInput input)
      {
         if (input.RangeCheckFrom.Distance(result.CastPosition) > input.Range)
         {
            result.Hitchance = HitChance.OutOfRange;
         }
      }

      private void IdlePrediction(ref PredictionOutput result, PredictionInput input)
      {
         result.UnitPosition = input.Unit.Position3D();
         result.CastPosition = input.Unit.Position3D();
      }

      private void CheckCollision(ref PredictionOutput result, PredictionInput input)
      {
         var positions = new List<Vector3> { result.CastPosition };
         var collisions = Collisions.GetCollision(positions, input);

         if (collisions.Any())
         {
            result.Hitchance = HitChance.Collision;
         }
      }

      public static void Load()
      {
         Instance = new Samaritan();
         Prediction.AddPrediction(Name, Instance);
         Game.Print($"Enhanced.SDK | {Name} prediction loaded!", Color.OrangeRed);
      }

      private void MovementPrediction(ref PredictionOutput result, PredictionInput input)
      {
         var target = input.Unit;
         var cosTheta = Vector2.Dot(target.Direction2D(), input.ToUnitDirection2D());

         var targetRelativePosition = target.Position2D() - input.RangeCheckFrom.ToVector2();
         var targetRelativeVelocity = -Math.Sign(cosTheta) * target.Velocity2D();

         var a = Vector2.Dot(targetRelativeVelocity, targetRelativeVelocity) - (float)Math.Pow(input.Speed, 2);
         var b = Vector2.Dot(targetRelativeVelocity, targetRelativePosition) * 2f;
         var c = Vector2.Dot(targetRelativePosition, targetRelativePosition);

         var discriminant = b * b - 4.0f * a * c;

         var impactTime = 2f * c / (float)(Math.Sqrt(discriminant) - b);

         if (float.IsNaN(impactTime))
         {
            result.Hitchance = HitChance.OutOfRange;
            var speed = input.Speed - Math.Sign(cosTheta) * target.MoveSpeed;
            impactTime = (float)Math.Sqrt(c) / speed;
         }

         result.UnitPosition = target.Position3D() + target.Velocity3D() * impactTime;
         result.CastPosition = target.Position3D() + target.Velocity3D() * impactTime;
      }

      private float GetPing() => (float)TimeSpan.FromMilliseconds(Game.Ping).TotalSeconds;
   }
}