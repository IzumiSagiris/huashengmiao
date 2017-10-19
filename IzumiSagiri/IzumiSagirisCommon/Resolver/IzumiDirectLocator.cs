using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace IzumiSagirisCommon.Resolver
{
    public static class IzumiDirectLocator
    {
        /// <summary>
        /// IzumiContainer
        /// </summary>
        private static IzumiContainer _container;

        public static void SetContainaer(IzumiContainer container)
        {
            _container = container;
        }
        /// <summary>
        /// Service Factory, without any parameter
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public static TInterface GetService<TInterface>()
        {
            return GetService<TInterface>(new object[] { });
        }

        /// <summary>
        /// Service Factory, need a parameter array and check your parameter’
        /// </summary>
        /// <typeparam name="TInterface">your Interface</typeparam>
        /// <returns></returns>
        public static TInterface GetService<TInterface>(object[] parameters)
        {
            Type type;
            var result = _container.ServiceDic.TryGetValue(typeof(TInterface), out type);
            if (result)
            {
                Type[] ptypes = GetParameterTypes(parameters);
                DynamicMethod dm = new DynamicMethod(new Guid().ToString("N"), typeof(object), new Type[] { typeof(object[]) }, true);
                ILGenerator il = dm.GetILGenerator();
                ConstructorInfo constructor = type.GetConstructor(ptypes);

                il.Emit(OpCodes.Nop);
                for (int i = 0; i < ptypes.Length; i++)
                {
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldc_I4, i);
                    il.Emit(OpCodes.Ldelem_Ref);
                    if (ptypes[i].IsValueType)
                    {
                        il.Emit(OpCodes.Unbox_Any, ptypes[i]);
                    }
                    else
                    {
                        il.Emit(OpCodes.Castclass, ptypes[i]);
                    }
                }
                il.Emit(OpCodes.Newobj, constructor);
                il.Emit(OpCodes.Ret);
                Func<object[], object> func = (Func<object[], object>)dm.CreateDelegate(typeof(Func<object[], object>));
                return (TInterface)func.Invoke(parameters);
            }
            else
            {
                throw new Exception("none of your service from interface");
            }
        }

        private static Type[] GetParameterTypes(params object[] parameters)
        {
            if (parameters == null)
            {
                return new Type[0];
            }
            Type[] values = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                values[i] = parameters[i].GetType();
            }
            return values;
        }
    }
}
