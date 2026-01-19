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
            fonAniDig();
        }
        private async Task fonAniMen()
        {
            var animX = new DoubleAnimation
            {
                From = 0,
                To = 300,
                Duration = TimeSpan.FromSeconds(10),
                RepeatBehavior = RepeatBehavior.Forever
            };

            var animY = new DoubleAnimation
            {
                From = 0,
                To = 200,
                Duration = TimeSpan.FromSeconds(10),
                RepeatBehavior = RepeatBehavior.Forever
            };

            imgShiftMen.BeginAnimation(TranslateTransform.XProperty, animX);
            imgShiftMen.BeginAnimation(TranslateTransform.YProperty, animY);


        }
        DoubleAnimation animX;
        DoubleAnimation animY;
        private async Task fonAniDig()
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
                To = 100,
                Duration = TimeSpan.FromSeconds(120),
                RepeatBehavior = RepeatBehavior.Forever
            };



        }


        private void aniDigRun(object sender, MouseEventArgs e)
        {
            imgShiftDig.BeginAnimation(TranslateTransform.XProperty, animX);
            imgShiftDig.BeginAnimation(TranslateTransform.YProperty, animY);
        }

        private void aniDigStop(object sender, MouseEventArgs e)
        {
            imgShiftDig.BeginAnimation(TranslateTransform.XProperty, null);
            imgShiftDig.BeginAnimation(TranslateTransform.YProperty, null);
        }
    }
}