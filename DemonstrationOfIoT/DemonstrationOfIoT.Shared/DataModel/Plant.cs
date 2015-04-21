using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using System.Linq;

namespace MyerPlant.DataModel
{
    public class Plant:ViewModelBase
    {
        private DispatcherTimer _humidTimer;
        private DispatcherTimer _rainTimer;
        private DispatcherTimer _lightTimer;
        private DispatcherTimer _tempTimer;

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

        private RainSensor _currentRainSensor;
        public RainSensor CurrentRainSensor
        {
            get
            {
                return _currentRainSensor;
            }
            set
            {
                if(_currentRainSensor!=value)
                {
                    _currentRainSensor = value;
                    RaisePropertyChanged(() => CurrentRainSensor);
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
                     SensorBase sensor = null;
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
                        SensorBase sensor = null;
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
                        SensorBase sensor = null;
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

            CurrentRainSensor = new RainSensor();
            CurrentRainSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 23, 19, 5), 46.2));
            CurrentRainSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 21, 11, 5), 33.1));
            CurrentRainSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 20, 12, 5), 48.5));
            CurrentRainSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 20, 9, 5), 46.0));
            CurrentRainSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 17, 14, 5), 45.4));
            CurrentRainSensor.ValueList.Add(new SensorValue(new DateTime(2015, 4, 19, 17, 15, 5), 44.9));

            _humidTimer = new DispatcherTimer();
            _humidTimer.Interval = TimeSpan.FromSeconds(5);
            _humidTimer.Tick += ((sendert, et) =>
            {
                var lastValue = CurrentHumidSensor.ValueList.FirstOrDefault().Value;
                var newValue = lastValue;
                var random = new Random();
                var result = random.Next(0, 2);
                if (result == 1) newValue += 0.1;
                else newValue -= 0.1;
                CurrentHumidSensor.ValueList.Insert(0, new SensorValue(DateTime.Now, newValue));
                RaisePropertyChanged(() => CurrentHumidSensor);
            });
            _humidTimer.Start();

            _tempTimer = new DispatcherTimer();
            _tempTimer.Interval = TimeSpan.FromSeconds(5);
            _tempTimer.Tick += ((sendert, et) =>
            {
                var lastValue = CurrentTempSensor.ValueList.FirstOrDefault().Value;
                var newValue = lastValue;
                var random = new Random();
                var result = random.Next(0, 2);
                if (result == 1) newValue += 0.1;
                else newValue -= 0.1;
                CurrentTempSensor.ValueList.Insert(0, new SensorValue(DateTime.Now, newValue));
                RaisePropertyChanged(() => CurrentTempSensor);
            });
            _tempTimer.Start();

            _lightTimer = new DispatcherTimer();
            _lightTimer.Interval = TimeSpan.FromSeconds(5);
            _lightTimer.Tick += ((sendert, et) =>
            {
                var lastValue = CurrentLightSensor.ValueList.FirstOrDefault().Value;
                var newValue = lastValue;
                var random = new Random();
                var result = random.Next(0, 2);
                if (result == 1) newValue += 0.1;
                else newValue -= 0.1;
                CurrentLightSensor.ValueList.Insert(0, new SensorValue(DateTime.Now, newValue));
                RaisePropertyChanged(() => CurrentLightSensor);
            });
            _lightTimer.Start();

             _rainTimer = new DispatcherTimer();
             _rainTimer.Interval = TimeSpan.FromSeconds(5);
             _rainTimer.Tick += ((sendert, et) =>
                {
                    var lastValue = CurrentRainSensor.ValueList.FirstOrDefault().Value;
                    var newValue = lastValue;
                    var random = new Random();
                    var result=random.Next(0, 2);
                    if (result == 1) newValue += 0.1;
                    else newValue -= 0.1;
                    CurrentRainSensor.ValueList.Insert(0, new SensorValue(DateTime.Now, newValue));
                    RaisePropertyChanged(() => CurrentRainSensor);
                });
             _rainTimer.Start();

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
