using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace IzumiSagirisCommon.Resolver
{
    public static class IzumiStaticLocator
    {
        /// <summary>
        /// 
        /// </summary>
        private static IzumiContainer _container;

        public static void SetContainaer(IzumiContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// GetService
        /// </summary>
        /// <typeparam name="TInterface">your Interface</typeparam>
        /// <returns></returns>
        public static TInterface GetService<TInterface>()
        {
            Type type;
            var result = _container.ServiceDic.TryGetValue(typeof(TInterface), out type);
            if (result)
            {
                BindingFlags defaultFlags = BindingFlags.Public | BindingFlags.Instance;
                var constructors = type.GetConstructors(defaultFlags);//Defualt Constructors
                var t = CreateInstanceEmit<TInterface>(constructors[0]);
                return t;
            }
            else
            {
                throw new Exception("none of your service from interface");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="constructor"></param>
        /// <returns></returns>
        private static TInterface CreateInstanceEmit<TInterface>(ConstructorInfo constructor)
        {
            var dynamicMethod = new DynamicMethod(Guid.NewGuid().ToString("N"), typeof(TInterface), new[] { typeof(TInterface[]) }, true);
            ILGenerator il = dynamicMethod.GetILGenerator();
            il.Emit(OpCodes.Newobj, constructor);
            if (constructor.ReflectedType.IsValueType)
            {
                il.Emit(OpCodes.Box, constructor.ReflectedType);
            }
            il.Emit(OpCodes.Ret);
            var func = (Func<TInterface>)dynamicMethod.CreateDelegate(typeof(Func<TInterface>));
            return func.Invoke();
        }

    }
}
