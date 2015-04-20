using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyerPlant.DataModel
{
    public class LightSensor:SensorBase
    {
        public const double BestValue = 80.1;

        public override void AutoSet()
        {
            ValueList.Insert(0, new SensorValue(DateTime.Now, LightSensor.BestValue));
            RaisePropertyChanged(() => CurrentValue);
            Messenger.Default.Send<GenericMessage<string>>(new GenericMessage<string>("Auto set light successfully!"));
        }
        public LightSensor()
        {

        }
    }
}
