﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Actors
{
    public class NormalSpeedStrategy: ISpeedStrategy
    {
        public double GetSpeed(double speed)
        {
            return speed;
        }
    }
}
