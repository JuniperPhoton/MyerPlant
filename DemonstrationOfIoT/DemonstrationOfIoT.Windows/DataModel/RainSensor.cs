using System;
using System.Collections.Generic;
using System.Text;

namespace MyerPlant.DataModel
{
    public class RainSensor:SensorBase
    {
        public const double BestValue = 40;

        private bool _isMaskOn;
        public bool IsMaskOn
        {
            get
            {
                return _isMaskOn;
            }
            set
            {
                if (_isMaskOn != value)
                {
                    _isMaskOn = value;
                    RaisePropertyChanged(() => IsMaskOn);
                }
            }
        }

        public RainSensor()
        {
            IsMaskOn = false;
            ModeIndex = 2;
        }
    }
}
