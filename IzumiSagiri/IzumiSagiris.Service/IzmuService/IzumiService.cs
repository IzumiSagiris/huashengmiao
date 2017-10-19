using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumiSagiris.Service.IzmuService
{
    public class IzumiService : IzumiInterFace
    {
        public IzumiService()
        {
            three = 10;
        }
        public IzumiService(double _three)
        {
            three = _three;
        }

        private double three;
        public double Shimada(double one, double two)
        {
            return one * two * three;
        }
    }
}
