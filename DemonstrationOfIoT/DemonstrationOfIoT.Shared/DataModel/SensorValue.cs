using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyerPlant.DataModel
{
    public class SensorValue:ViewModelBase
    {
        private DateTime _time;
        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                if(_time!=value)
                {
                    _time = value;
                    RaisePropertyChanged(() => Time);
                }
            }
        }

        private double _value;
        public double Value
        {
            get
            {
                return _value;

            }
            set
            {
                if(_value!=value)
                {
                    _value = value;
                    RaisePropertyChanged(() => Value);
                }
            }
        }

        public SensorValue()
        {
            Time = DateTime.Now;
            Value = 0;
        }

        public SensorValue(DateTime time,double value)
        {
            this.Time = time;
            this.Value = value;
        }

    }
}
