using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyerPlant.DataModel
{
    public interface ISensor
    {
        SensorValue CurrentValue { get; }
        ObservableCollection<SensorValue> ValueList { get; set; }

        SensorValue OutputLastValue();
        ObservableCollection<SensorValue> OutputValues();
        void InputValue(SensorValue newValue);
        void AutoSet();
    }
}
