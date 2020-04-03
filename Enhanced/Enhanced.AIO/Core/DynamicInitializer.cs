namespace Enhanced.Core
{
   using System;
   using System.Reflection.Emit;

   public static class DynamicInitializer
   {
      public static TV NewInstance<TV>() where TV : class => ObjectGenerator(typeof(TV)) as TV;

      public static object NewInstance(Type type) => ObjectGenerator(type);

      private static object ObjectGenerator(Type type)
      {
         var target = type.GetConstructor(Type.EmptyTypes);

         if (target == null || target.DeclaringType == null)
         {
            return null;
         }

         var dynamic = new DynamicMethod(string.Empty, type, new Type[0], target.DeclaringType);
         var il = dynamic.GetILGenerator();

         if (target.DeclaringType == null)
         {
            return null;
         }

         il.DeclareLocal(target.DeclaringType);
         il.Emit(OpCodes.Newobj, target);
         il.Emit(OpCodes.Stloc_0);
         il.Emit(OpCodes.Ldloc_0);
         il.Emit(OpCodes.Ret);

         var method = (Func<object>)dynamic.CreateDelegate(typeof(Func<object>));
         return method();
      }
   }
}