using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace IzumiSagirisCommon.Resolver
{
    public class IzumiServiceLocator
    {
        /// <summary>
        /// 
        /// </summary>
        private IzumiContainer _container;

        public IzumiServiceLocator(IzumiContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// GetService
        /// </summary>
        /// <typeparam name="TInterface">your Interface</typeparam>
        /// <returns></returns>
        public object Get(Type TInterface)
        {
            Type type;
            var result = _container.ServiceDic.TryGetValue(TInterface, out type);
            if (result)
            {
                BindingFlags defaultFlags = BindingFlags.Public | BindingFlags.Instance;
                var constructors = type.GetConstructors(defaultFlags);//Defualt Constructors
                var t = this.CreateInstanceEmit(constructors[0]);
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
        private object CreateInstanceEmit(ConstructorInfo constructor)
        {
            var dynamicMethod = new DynamicMethod(Guid.NewGuid().ToString("N"), typeof(object), new[] { typeof(object[]) }, true);
            ILGenerator il = dynamicMethod.GetILGenerator();
            il.Emit(OpCodes.Newobj, constructor);
            if (constructor.ReflectedType.IsValueType)
            {
              il.Emit(OpCodes.Box, constructor.ReflectedType);
            }
            il.Emit(OpCodes.Ret);
            var func = (Func<object>)dynamicMethod.CreateDelegate(typeof(Func<object>));
            return func.Invoke();
        }

    }
}
