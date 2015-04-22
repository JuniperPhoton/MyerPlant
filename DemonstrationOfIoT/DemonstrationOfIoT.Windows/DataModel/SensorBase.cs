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
    public class SensorBase:ViewModelBase
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
            set
            {
                if (ValueList == null) ValueList = new ObservableCollection<SensorValue>();
                ValueList.Add(new SensorValue(DateTime.Now, value.Value));
                RaisePropertyChanged(() => CurrentValue);
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
                    //Messenger.Default.Send<GenericMessage<SensorValue>>(new GenericMessage<SensorValue>(ValueList.LastOrDefault()));
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
                    if(value>2 || value<0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    _modeIndex = value;
                    RaisePropertyChanged(() => ModeIndex);
                    switch (value)
                    {
                        case 0:
                            {
                                ManuallyVisibility = Visibility.Visible;
                                Auto1Visibility = Visibility.Collapsed;
                                Auto2Visibility = Visibility.Collapsed;
                                Messenger.Default.Send<GenericMessage<string>>(new GenericMessage<string>("Set to manually successfully!"));
                            }; break;
                        case 1:
                            {
                                ManuallyVisibility = Visibility.Collapsed;
                                Auto1Visibility = Visibility.Visible;
                                Auto2Visibility = Visibility.Collapsed;
                                Messenger.Default.Send<GenericMessage<string>>(new GenericMessage<string>("Set to costum successfully!"));
                            }; break;
                        case 2:
                            {
                                ManuallyVisibility = Visibility.Collapsed;
                                Auto1Visibility = Visibility.Collapsed;
                                Auto2Visibility = Visibility.Visible;
                                Messenger.Default.Send<GenericMessage<string>>(new GenericMessage<string>("Set to recommand successfully!"));
                            }; break;
                    }
             
            }
        }


        public SensorBase()
        {
            Auto2Visibility = Visibility.Visible;
            ManuallyVisibility = Visibility.Collapsed;
            Auto1Visibility = Visibility.Collapsed;
            
        }

        public virtual void AutoSet()
        {

        }
    }
}
