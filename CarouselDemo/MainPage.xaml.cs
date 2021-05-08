﻿using Microsoft.Toolkit.Uwp.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CarouselDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MainPageViewModel PageViewModel { get; set; }

        public MainPage()
        {
            PageViewModel = new MainPageViewModel();
            CreateTestItemsColors();
            DataContext = PageViewModel;
            this.Loaded += MainPage_Loaded;
            this.InitializeComponent();
            this.KeyDown += MainPage_KeyDown;
            this.PointerWheelChanged += MainPage_PointerWheelChanged;
        }

        private void MainPage_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                Input.WheelChanged(myCarousel, e.GetCurrentPoint(myCarousel));
            }
            catch { }
        }

        private void MainPage_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            try
            {
                Input.KeyDown(myCarousel, e.Key);
            }
            catch { }
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1500);
            var focusableElement = myCarousel.FindDescendant<ContentControl>();
            focusableElement.Focus(FocusState.Programmatic);
        }

        private void CreateTestItemsColors()
        {
            foreach (var prop in typeof(Colors).GetProperties())
            {
                if (prop.GetValue(null) is Color color)
                {
                    PageViewModel.Items.Add(new ItemModel { BackgroundColor = new SolidColorBrush(color), Text = prop.Name });
                }
            }
        }

        private void PostersButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MoviePostersView));
        }

        private void AlbumsButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AlbumCoversView));
        }
    }
}
