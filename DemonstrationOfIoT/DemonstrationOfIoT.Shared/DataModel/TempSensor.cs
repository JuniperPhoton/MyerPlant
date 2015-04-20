using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyerPlant.DataModel
{
    public class TempSensor:SensorBase
    {
        public const double BestValue = 29.5;

        public override void AutoSet()
        {
            ValueList.Insert(0, new SensorValue(DateTime.Now, TempSensor.BestValue));
            RaisePropertyChanged(() => CurrentValue);
            Messenger.Default.Send<GenericMessage<string>>(new GenericMessage<string>("Auto set temperature successfully!"));
        }

        public TempSensor()
        {
            
        }
    }
}
