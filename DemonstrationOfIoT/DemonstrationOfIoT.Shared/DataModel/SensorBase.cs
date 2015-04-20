using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using Windows.UI.Xaml;

namespace MyerPlant.DataModel
{
    public class SensorBase:ViewModelBase,ISensor
    {
        public SensorValue CurrentValue
        {
            get 
            {
                if (ValueList != null)
                {
                    return ValueList.FirstOrDefault();
                }
                else return default(SensorValue);
            }
        }

        private ObservableCollection<SensorValue> valueList;
        public ObservableCollection<SensorValue> ValueList
        {
            get
            {
                if (valueList == null) valueList = new ObservableCollection<SensorValue>();
                return valueList;
            }
            set
            {
                if(valueList!=value)
                {
                    valueList = value;
                    RaisePropertyChanged(() => ValueList);
                    Messenger.Default.Send<GenericMessage<SensorValue>>(new GenericMessage<SensorValue>(ValueList.LastOrDefault()));
                }
                
            }
        }

        private Visibility _manuallyVisibility;
        public Visibility ManuallyVisibility
        {
            get
            {
                return _manuallyVisibility;
            }
            set
            {
                _manuallyVisibility = value;
                RaisePropertyChanged(() => ManuallyVisibility);
            }
        }

        private Visibility _auto1Visibility;
        public Visibility Auto1Visibility
        {
            get
            {
                return _auto1Visibility;
            }
            set
            {
                _auto1Visibility = value;
                RaisePropertyChanged(() => Auto1Visibility);
            }
        }

        private Visibility _auto2Visibility;
        public Visibility Auto2Visibility
        {
            get
            {
                return _auto2Visibility;
            }
            set
            {
                _auto2Visibility = value;
                RaisePropertyChanged(() => Auto2Visibility);
            }
        }

        private int _modeIndex;
        public int ModeIndex
        {
            get
            {
                return _modeIndex;
            }
            set
            {
                if (_modeIndex != value)
                {
                    _modeIndex = value;
                    RaisePropertyChanged(() => ModeIndex);
                    switch (value)
                    {
                        case 0:
                            {
                                ManuallyVisibility = Visibility.Visible;
                                Auto1Visibility = Visibility.Collapsed;
                                Auto2Visibility = Visibility.Collapsed;
                            }; break;
                        case 1:
                            {
                                ManuallyVisibility = Visibility.Collapsed;
                                Auto1Visibility = Visibility.Visible;
                                Auto2Visibility = Visibility.Collapsed;
                            }; break;
                        case 2:
                            {
                                ManuallyVisibility = Visibility.Collapsed;
                                Auto1Visibility = Visibility.Collapsed;
                                Auto2Visibility = Visibility.Visible;
                            }; break;
                    }
                }

            }
        }


        public SensorBase()
        {
            Auto2Visibility = Visibility.Visible;
            ManuallyVisibility = Visibility.Collapsed;
            Auto1Visibility = Visibility.Collapsed;
            ModeIndex = 2;
        }

        public virtual void AutoSet()
        {

        }

        public SensorValue OutputLastValue()
        {
            return CurrentValue;
        }

        public ObservableCollection<SensorValue> OutputValues()
        {
            return ValueList;
        }

        public void InputValue(SensorValue newValue)
        {
            ValueList.Add(newValue);
        }
    }
}
