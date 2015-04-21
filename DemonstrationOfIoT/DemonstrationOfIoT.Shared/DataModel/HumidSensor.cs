using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;

namespace MyerPlant.DataModel
{
    public class HumidSensor:SensorBase
    {
        public const double BestValue = 50;

     
        public override void AutoSet()
        {
            ValueList.Insert(0, new SensorValue(DateTime.Now, HumidSensor.BestValue));
            RaisePropertyChanged(() => CurrentValue);
            Messenger.Default.Send<GenericMessage<string>>(new GenericMessage<string>("Auto set humid successfully!"));
        }

        public HumidSensor()
        {
            ModeIndex = 0;
            
        }
    }
}
