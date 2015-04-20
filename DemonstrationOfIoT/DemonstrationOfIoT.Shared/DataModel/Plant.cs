using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;

namespace MyerPlant.DataModel
{
    public class Plant:ViewModelBase
    {
        private string _displayName;
        public string DisplayName
        { 
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
                RaisePropertyChanged(() => DisplayName);
            }
        }

        private Guid _id;
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                RaisePropertyChanged(() => ID);
            }
        }

        private Visibility _showProgressVisibility;
        public Visibility ShowProgressVisibility
        {
            get
            {
                return _showProgressVisibility;
            }
            set
            {
                if (_showProgressVisibility != value)
                {
                    _showProgressVisibility = value;
                    RaisePropertyChanged(() => ShowProgressVisibility);
                }
            }
        }

        private HumidSensor _currentHumidSensor;
        public HumidSensor CurrentHumidSensor
        {
            get
            {
                return _currentHumidSensor;
            }
            set
            {
                if (_currentHumidSensor != value)
                {
                    _currentHumidSensor = value;
                    RaisePropertyChanged(() => CurrentHumidSensor);
                }
            }
        }

        private TempSensor _currentTempSensor;
        public TempSensor CurrentTempSensor
        {
            get
            {
                return _currentTempSensor;
            }
            set
            {
                if (_currentTempSensor != value)
                {
                    _currentTempSensor = value;
                    RaisePropertyChanged(() => CurrentTempSensor);
                }
            }
        }

        private LightSensor _currentLightSensor;
        public LightSensor CurrentLightSensor
        {
            get
            {
                return _currentLightSensor;
            }
            set
            {
                if (_currentLightSensor != value)
                {
                    _currentLightSensor = value;
                    RaisePropertyChanged(() => CurrentLightSensor);
                }
            }
        }

        private RelayCommand<string> _raiseCommand;
        public RelayCommand<string> RaiseCommand
        {
            get
            {
                return _raiseCommand ?? (_raiseCommand = new RelayCommand<string>((token) =>
                 {
                     ISensor sensor = null;
                     string propertyToChange = "";
                     switch (token)
                     {
                         case "Humid": sensor = CurrentHumidSensor; propertyToChange = "CurrentHumidSensor"; break;
                         case "Temp": sensor = CurrentTempSensor; propertyToChange = "CurrentTempSensor"; break;
                         case "Light": sensor = CurrentLightSensor; propertyToChange = "CurrentLightSensor"; break;
                     }
                     ProgressAndAct(TimeSpan.FromSeconds(0.5), () =>
                     {
                         ControlMachine.GetInstance().Raise(sensor, 1);
                         RaisePropertyChanged(propertyToChange);
                     });
                 }));
            }
            set
            {
                if (_raiseCommand != value)
                {
                    _raiseCommand = value;
                    RaisePropertyChanged(() => RaiseCommand);
                }
            }
        }

        private RelayCommand<string> _reduceCommand;
        public RelayCommand<string> ReduceCommand
        {
            get
            {
                if (_reduceCommand == null)
                {
                    _reduceCommand = new RelayCommand<string>((token) =>
                    {
                        ISensor sensor = null;
                        string propertyToChange = "";
                        switch (token)
                        {
                            case "Humid": sensor = CurrentHumidSensor; propertyToChange = "CurrentHumidSensor"; break;
                            case "Temp": sensor = CurrentTempSensor; propertyToChange = "CurrentTempSensor"; break;
                            case "Light": sensor = CurrentLightSensor; propertyToChange = "CurrentLightSensor"; break;
                        }
                        ProgressAndAct(TimeSpan.FromSeconds(0.5), () =>
                        {
                            ControlMachine.GetInstance().Reduce(sensor, 1);
                            RaisePropertyChanged(propertyToChange);
                        });
                    });
                }
                return _reduceCommand;
            }
            set
            {
                if (_reduceCommand != value)
                {
                    _raiseCommand = value;
                    RaisePropertyChanged(() => ReduceCommand);
                }
            }
        }

        private RelayCommand<string> _autoSetCommand;
        public RelayCommand<string> AutoSetCommand
        {
            get
            {
                if (_autoSetCommand == null)
                {
                    _autoSetCommand = new RelayCommand<string>((token) =>
                    {
                        ISensor sensor = null;
                        switch (token)
                        {
                            case "Humid": sensor = CurrentHumidSensor; break;
                            case "Temp": sensor = CurrentTempSensor; break;
                            case "Light": sensor = CurrentLightSensor; break;
                        }
                        ProgressAndAct(TimeSpan.FromSeconds(1), () =>
                        {
                            ControlMachine.GetInstance().AutoSet(sensor);
                        });
                    });
                }
                return _autoSetCommand;
            }
            set
            {
                if (_autoSetCommand != value)
                {

                    _autoSetCommand = value;
                    RaisePropertyChanged(() => AutoSetCommand);
                }
            }
        }

        public Plant(string displayName)
        {
            DisplayName = displayName;
            ID = Guid.NewGuid();
            ShowProgressVisibility = Visibility.Collapsed;
            CurrentHumidSensor = new HumidSensor();
            CurrentHumidSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 23, 19, 5), 48));
            CurrentHumidSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 21, 11, 5), 49));
            CurrentHumidSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 20, 12, 5), 48));
            CurrentHumidSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 20, 9, 5), 50));
            CurrentHumidSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 17, 14, 5), 51));
            CurrentHumidSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 17, 15, 5), 48));

            CurrentTempSensor = new TempSensor();
            CurrentTempSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 23, 19, 5), 26.2));
            CurrentTempSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 21, 11, 5), 27.1));
            CurrentTempSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 20, 12, 5), 28.5));
            CurrentTempSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 20, 9, 5), 26.0));
            CurrentTempSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 17, 14, 5), 25.4));
            CurrentTempSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 17, 15, 5), 24.9));

            CurrentLightSensor = new LightSensor();
            CurrentLightSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 23, 19, 5), 66.2));
            CurrentLightSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 21, 11, 5), 73.1));
            CurrentLightSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 20, 12, 5), 68.5));
            CurrentLightSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 20, 9, 5), 86.0));
            CurrentLightSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 17, 14, 5), 105.4));
            CurrentLightSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 17, 15, 5), 54.9));
        }

        private void ProgressAndAct(TimeSpan time, Action action)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = time;
            timer.Tick += ((sendert, et) =>
            {
                ShowProgressVisibility = Visibility.Collapsed;
                action.Invoke();
                timer.Stop();
            });
            ShowProgressVisibility = Visibility.Visible;
            timer.Start();
        }
    }
}
