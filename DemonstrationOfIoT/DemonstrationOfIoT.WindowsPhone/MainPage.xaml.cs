using GalaSoft.MvvmLight.Messaging;
using MyerPlant.DataModel;
using MyerPlant.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MyerPlant
{
    public sealed partial class MainPage : Page
    {
        bool _isInSide = false;
        TranslateTransform transfer = new TranslateTransform();
        double _oriXPosition = 0;
        MainViewModel mainVM;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            StatusBar.GetForCurrentView().BackgroundOpacity = 0.2;

            ContentGrid.ManipulationDelta += ContentGrid_ManipulationDelta;
            ContentGrid.ManipulationStarted += ContentGrid_ManipulationStarted;
            transfer = new TranslateTransform();
            ContentGrid.RenderTransform = this.transfer;

            mainVM = new MainViewModel();
            this.DataContext = mainVM;

            Messenger.Default.Register<GenericMessage<string>>(this, act =>
                {
                    var message = act.Content;
                    toastTB.Text = message;
                    ToastStory.Begin();
                });
            Messenger.Default.Register<GenericMessage<string>>(this, "SideOut", act =>
                {
                    SideOutStory.Begin();
                    _isInSide = false;

                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += ((sendert, et) =>
                        {
                            loadingGrid.Visibility = Visibility.Collapsed;
                            timer.Stop();
                        });
                    loadingGrid.Visibility = Visibility.Visible;
                    timer.Start();
                });
        }

        private void HamburgerClick(object sender, RoutedEventArgs e)
        {
            if (_isInSide)
            {
                SideOutStory.Begin();
                _isInSide = false;
            }
            else
            {
                SideInStory.Begin();
                _isInSide = true;
            }
        }

        private void TapTranBorder(object sender, RoutedEventArgs e)
        {
            if (_isInSide)
            {
                SideOutStory.Begin();
                _isInSide = false;
                return;
            }
        }

        private void ContentGrid_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            _oriXPosition = e.Position.X;
        }

        private void ContentGrid_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (e.Delta.Translation.X > 10 && _oriXPosition <= 10)
            {
                if (!_isInSide)
                {
                    SideInStory.Begin();
                    _isInSide = true;
                }
            }
            else if (e.Delta.Translation.X < -10)
            {
                if (_isInSide)
                {
                    SideOutStory.Begin();
                    _isInSide = false;
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            Frame.BackStack.Clear();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            if (_isInSide)
            {
                SideOutStory.Begin();
                _isInSide = false;
                return;
            }
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (_isInSide)
            {
                e.Handled = true;
                SideOutStory.Begin();
                _isInSide = false;
                return;
            }
            Frame rootframe = Window.Current.Content as Frame;
            if (rootframe != null && rootframe.CanGoBack)
            {
                e.Handled = true;
                rootframe.GoBack();
            }

        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var sp = sender as StackPanel;
            var tb = sp.Children.Last() as TextBlock;
            var name = tb.Text;
            Messenger.Default.Send<GenericMessage<string>>(new GenericMessage<string>(name),"TapPlant");
        }
    }
}
