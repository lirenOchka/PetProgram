using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace tamogoji
{
    public partial class MenuWindow : Window
    {
        private Window ownerForm;

        public MenuWindow(Window owner)
        {
            InitializeComponent();
            ownerForm = owner;
            this.Owner = owner;
            fonAniMen();
            setFonAni();
        }
        private async Task fonAniMen()
        {
            var animX = new DoubleAnimation
            {
                From = 0,
                To = 300,
                Duration = TimeSpan.FromSeconds(20),
                RepeatBehavior = RepeatBehavior.Forever
            };

            var animY = new DoubleAnimation
            {
                From = 0,
                To = 300,
                Duration = TimeSpan.FromSeconds(20),
                RepeatBehavior = RepeatBehavior.Forever
            };

            imgShiftMen.BeginAnimation(TranslateTransform.XProperty, animX);
            imgShiftMen.BeginAnimation(TranslateTransform.YProperty, animY);


        }

        DoubleAnimation animX;
        DoubleAnimation animY;
        private async Task setFonAni()
        {
            animX = new DoubleAnimation
            {
                From = 0,
                To = 200,
                Duration = TimeSpan.FromSeconds(45),
                RepeatBehavior = RepeatBehavior.Forever
            };

            animY = new DoubleAnimation
            {
                From = 0,
                To = 200,
                Duration = TimeSpan.FromSeconds(120),
                RepeatBehavior = RepeatBehavior.Forever
            };



        }


        private async void aniDigRun(object sender, MouseEventArgs e)
        {
            imgShiftDig.BeginAnimation(TranslateTransform.XProperty, animX);
            imgShiftDig.BeginAnimation(TranslateTransform.YProperty, animY);
            for (int i = 10; i > 5; i--)
            {
                imageDialog.Margin = new Thickness(i);
                await Task.Delay(10);
            }
            imageDialog.Margin = new Thickness(5);
        }

        private async void aniDigStop(object sender, MouseEventArgs e)
        {
            imgShiftDig.BeginAnimation(TranslateTransform.XProperty, null);
            imgShiftDig.BeginAnimation(TranslateTransform.YProperty, null);

            for (int i = 5; i < 10; i++)
            {
                imageDialog.Margin = new Thickness(i);
                await Task.Delay(10);
            }
            imageDialog.Margin = new Thickness(10);
        }
        private async void aniExitRun(object sender, MouseEventArgs e)
        {
            imgShiftExit.BeginAnimation(TranslateTransform.XProperty, animX);
            imgShiftExit.BeginAnimation(TranslateTransform.YProperty, animY);
            for (int i = 10; i > 5; i--)
            {
                imageExit.Margin = new Thickness(i);
                await Task.Delay(10);
            }
            imageExit.Margin = new Thickness(5);
        }

        private async void aniExitStop(object sender, MouseEventArgs e)
        {
            imgShiftExit.BeginAnimation(TranslateTransform.XProperty, null);
            imgShiftExit.BeginAnimation(TranslateTransform.YProperty, null);

            for (int i = 5; i < 10; i++)
            {
                imageExit.Margin = new Thickness(i);
                await Task.Delay(10);
            }
            imageExit.Margin = new Thickness(10);
        }

        private void DialogButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Кнопка нажата!");
        }
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ownerForm.Close();
                this.Close();
            }
        }
    }
}