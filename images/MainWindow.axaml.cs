using Avalonia.Controls;
using Avalonia.Interactivity;
using SkiaSharp;
using System.Diagnostics.Metrics;
using Tmds.DBus.Protocol;
using Avalonia.Media.Imaging;
using Avalonia.Media;
using Avalonia;
using System;

namespace images
{
    public partial class MainWindow : Window
    {
        private string[] my_pathes = new string[] { "C:/cat1.jpg", "C:/cat2.jpg", "C:/cat3.jpg" };
        private string[] imageDescriptions = new string[] { "Этот мявчик нашкодил", "Этот мявчик удивленный", "Этот мявчик только проснулся" };
        private int now_pic = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        public void ShowPic(object sender, RoutedEventArgs args)
        {
            myPic.IsVisible = true;
            now_pic = now_pic + 1;
            if (now_pic == 3) now_pic = 0;
            myPic.Source = new Bitmap (my_pathes[now_pic]);

            imageText.IsVisible = true;

            imageText.Text = imageDescriptions[now_pic];
        }

        public void HidePic(object sender, RoutedEventArgs args)
        {
            myPic.IsVisible = false;
            imageText.IsVisible = false;
        }

        public void Exit(object sender, RoutedEventArgs args)
        {
            Environment.Exit(0);
        }

        public void Regs(object sender, RoutedEventArgs args)
        {
            new Window1().Show();
        }
    }
}