using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Actors
{
    public interface ISpeedStrategy
    {
        public double GetSpeed(double speed);
    }
    
}
