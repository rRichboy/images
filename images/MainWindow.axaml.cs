using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;

namespace images
{
    public partial class MainWindow : Window
    {
        //массивы картинки + описания
        private string[] my_pathes = new string[] { "avares://images/Assets/cat1.jpg", "avares://images/Assets/cat2.jpg", "avares://images/Assets/cat3.jpg" };
        private string[] imageDescriptions = new string[] { "Этот мявчик нашкодил", "Этот мявчик удивленный", "Этот мявчик только проснулся" };
        private int now_pic = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        //изначально скрыты далее по нажатию кнопки крутит по одной
        public void ShowPic(object sender, RoutedEventArgs args)
        {
            myPic1.IsVisible = false;
            myPic2.IsVisible = false;
            myPic3.IsVisible = false;

            now_pic = now_pic + 1;
            if (now_pic == 3) now_pic = 0;

            switch (now_pic)
            {
                case 0:
                    myPic1.IsVisible = true;
                    break;
                case 1:
                    myPic2.IsVisible = true;
                    break;
                case 2:
                    myPic3.IsVisible = true;
                    break;
            }

            imageText.IsVisible = true;
            imageText.Text = imageDescriptions[now_pic];
        }

        //скрыть блохастых по нажатию кнопки
        public void HidePic(object sender, RoutedEventArgs args)
        {
            myPic1.IsVisible = false;
            myPic2.IsVisible = false;
            myPic3.IsVisible = false;

            imageText.IsVisible = false;
        }

        //выход
        public void Exit(object sender, RoutedEventArgs args)
        {
            Environment.Exit(0);
        }

        //переход на новое окно
        public void Regs(object sender, RoutedEventArgs args)
        {
            new Window1().Show();
        }
    }
}
