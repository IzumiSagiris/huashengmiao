using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IzumiSagirisCommon.Resolver;
using IzumiSagiris.Service.IzmuService;

namespace IzumiSagiri.App_Start
{
    public static class IzumiIocManager
    {
        /// <summary>
        /// _IzumiLocator
        /// </summary>
        public static IzumiServiceLocator IzumiLocator;

        public static void RegisterIzumiLocator(IzumiContainer container)
        {
            if (IzumiLocator == null)
            {
                container.RegisterType<IzumiInterFace, IzumiService>();
                //IzumiLocator = new IzumiServiceLocator(container);
                IzumiStaticLocator.SetContainaer(container);
            }
        }
    }
}