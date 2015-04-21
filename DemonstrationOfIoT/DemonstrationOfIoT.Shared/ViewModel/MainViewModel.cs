using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyerPlant.DataModel;
using System;
using Windows.System.Threading;
using Windows.UI.Xaml;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Popups;

namespace MyerPlant.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {

        private ObservableCollection<Plant> _plants;
        public ObservableCollection<Plant> Plants
        {
            get
            {
                if (_plants == null) _plants = new ObservableCollection<Plant>();
                return _plants;
            }
            set
            {
                if(_plants!=value)
                {
                    _plants = value;
                    RaisePropertyChanged(() => Plants);
                }
            }
        }

        public Plant CurrentPlant { get; set; }


        public MainViewModel()
        {
            Plants = new ObservableCollection<Plant>();
            Plants.Add(new Plant("Jasmine"));
            Plants.Add(new Plant("Hyacinth"));
            Plants.Add(new Plant("Orchid"));
            CurrentPlant = Plants.LastOrDefault();
            RaisePropertyChanged(() => CurrentPlant);

            Messenger.Default.Register<GenericMessage<string>>(this,"TapPlant", args =>
                {
                    var name = args.Content;
                    CurrentPlant = Plants.ToList().Find((plant) =>
                    {
                        if (plant.DisplayName == name) return true;
                        else return false;
                    });
                    RaisePropertyChanged(() => CurrentPlant);
                    Messenger.Default.Send<GenericMessage<string>>(new GenericMessage<string>(""), "SideOut");
                });
        }

       
    }
}