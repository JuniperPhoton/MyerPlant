using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MyerPlant.DataModel
{
    public class ControlMachine
    {
        private static object o = new object();

        private static ControlMachine Instance;

        public static ControlMachine GetInstance()
        {
            if(Instance==null)
            {
                lock(o)
                {
                    if(Instance==null)   Instance = new ControlMachine();
                }
            }
            return Instance;
        }

        private ControlMachine()
        {

        }

        public void Raise(ISensor sensor,double valueToAdd)
        {
            var lastValue = sensor.ValueList.First().Value;
            var newValue = lastValue + valueToAdd;
            sensor.ValueList.Insert(0,new SensorValue(DateTime.Now, newValue));
            Messenger.Default.Send<GenericMessage<string>>(new GenericMessage<string>("Raise successfully!"));
        }

        public void Reduce(ISensor sensor, double valueToMinus)
        {
            var lastValue = sensor.ValueList.First().Value;
            var newValue = lastValue - valueToMinus;
            sensor.ValueList.Insert(0,new SensorValue(DateTime.Now, newValue));
            Messenger.Default.Send<GenericMessage<string>>(new GenericMessage<string>("Reduce successfully!"));
        }

        public void AutoSet(ISensor sensor)
        {
            sensor.AutoSet();
        }
    }
}
