using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;

namespace MyerPlant.DataModel
{
    public class LightSensor:SensorBase
    {
        public const double BestValue = 80.1;

        private bool _isMaskOn;
        public bool IsMaskOn
        {
            get
            {
                return _isMaskOn;
            }
            set
            {
               if(_isMaskOn!=value)
               {
                   _isMaskOn = value;
                   RaisePropertyChanged(() => IsMaskOn);
               }
            }
        }

        private bool _isLightOn;
        public bool IsLightOn
        {
            get
            {
                return _isLightOn;
            }
            set
            {
                 if(_isLightOn!=value)
                 {
                     _isLightOn = value;
                     RaisePropertyChanged(() => IsLightOn);
                     if (value == true)
                     {
                         SiderVisibility = Visibility.Visible;
                     }
                     else SiderVisibility = Visibility.Collapsed;
                 }
            }
        }

        private Visibility _siderVisibility;
        public Visibility SiderVisibility
        {
           get
            {
                return _siderVisibility;
            }
            set
            {
                if(_siderVisibility!=value)
                {
                    _siderVisibility = value;
                    RaisePropertyChanged(() => SiderVisibility);
                }
            }
        }

        public override void AutoSet()
        {
            ValueList.Insert(0, new SensorValue(DateTime.Now, LightSensor.BestValue));
            RaisePropertyChanged(() => CurrentValue);
            Messenger.Default.Send<GenericMessage<string>>(new GenericMessage<string>("Auto set light successfully!"));
        }
        public LightSensor()
        {
            IsMaskOn = false;
            IsLightOn = true;
            ModeIndex = 0;
        }
    }
}
