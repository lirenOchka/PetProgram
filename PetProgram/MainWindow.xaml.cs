using PetProgram.Classes;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using tamogoji.Classes;

namespace tamogoji
{
    public partial class MainWindow : Window
    {
        private bool isDragging = false;

        public MainWindow()
        {
            InitializeComponent();

            DataPet.openPet = ChoicePet();
            try
            {
                var icoPath = Path.GetFullPath("image\\Doki_Doki_Literature_Club_Logo.ico");
                if (File.Exists(icoPath))
                    this.Icon = BitmapFrame.Create(new Uri(icoPath, UriKind.Absolute));
            }
            catch { }

            SetPetImage(DataPet.openPet.calmness);
        }

        private petImages ChoicePet()
        {
            var str = File.ReadAllLines("settings.txt");
            switch (str[0].Split('=')[1].Trim())
            {
                case "sayori": return DataPet.saoyri;
                case "yuri": return DataPet.Yuri;
                case "natsuki": return DataPet.Natsuki;
                case "monika": return DataPet.monika;
                default: return DataPet.saoyri;
            }
        }

        private void SetPetImage(ImageSource src)
        {
            pet.Source = src;
            pet.Width = src.Width;
            pet.Height = src.Height;
        }

        private bool Activity()
        {
            bool result;
            switch (DataPet.Activity)
            {
                case "1":
                    result = DataPet.GodRandom.Next(3, 7) == DataPet.boredomStatus || DataPet.boredomStatus == 8;
                    if (DataPet.boredomStatus == 8) DataPet.boredomStatus = 0;
                    break;
                case "2":
                    result = DataPet.GodRandom.Next(10, 14) == DataPet.boredomStatus || DataPet.boredomStatus == 15;
                    if (DataPet.boredomStatus == 15) DataPet.boredomStatus = 0;
                    break;
                case "3":
                    result = DataPet.GodRandom.Next(25, 29) == DataPet.boredomStatus || DataPet.boredomStatus == 30;
                    if (DataPet.boredomStatus == 30) DataPet.boredomStatus = 0;
                    break;
                case "4":
                    result = DataPet.GodRandom.Next(55, 59) == DataPet.boredomStatus || DataPet.boredomStatus == 60;
                    if (DataPet.boredomStatus == 60) DataPet.boredomStatus = 0;
                    break;
                default:
                    result = DataPet.GodRandom.Next(5, 9) == DataPet.boredomStatus || DataPet.boredomStatus == 10;
                    if (DataPet.boredomStatus == 8) DataPet.boredomStatus = 0;
                    break;
            }
            return result;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // разместим питомца внизу экрана по центру и стартуем логику
            Canvas.SetLeft(pet, (SystemParameters.PrimaryScreenWidth - pet.Width) / 2);
            Canvas.SetTop(pet, SystemParameters.PrimaryScreenHeight - pet.Height);
            await Jump();
            _ = BoredomLoop();
        }

        private async Task BoredomLoop()
        {
            while (true)
            {
                await Task.Delay(1000);
                DataPet.boredomStatus++;

                if (Activity() && !DataPet.movePet)
                {
                    int fff = DataPet.GodRandom.Next(1, 6);
                    if (!DataPet.movePet)
                    {
                        if (fff <= 2)
                        {
                            for (int i = 0; i < DataPet.GodRandom.Next(1, 3); i++)
                            {
                                await JumpLeft();
                                await Task.Delay(DataPet.GodRandom.Next(100, 1000));
                            }
                        }
                        else if (fff == 3)
                        {
                            for (int i = 0; i < DataPet.GodRandom.Next(1, 3); i++)
                            {
                                await Turn();
                                await Task.Delay(DataPet.GodRandom.Next(100, 1000));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < DataPet.GodRandom.Next(1, 3); i++)
                            {
                                await JumpRight();
                                await Task.Delay(DataPet.GodRandom.Next(100, 1000));
                            }
                            await Drive((int)Canvas.GetLeft(pet), (int)Canvas.GetTop(pet));
                        }
                    }
                }
            }
        }

        private void ApplyFlip()
        {
            pet.RenderTransform = new ScaleTransform(DataPet.spritePetRith ? 1 : -1, 1);
        }

        private async Task JumpLeft()
        {
            SetPetImage(DataPet.openPet.calmness);
            if (DataPet.spritePetRith)
            {
                ApplyFlip();
                DataPet.spritePetRith = false;
            }
            if (!DataPet.movePet)
            {
                double posY = Canvas.GetTop(pet);
                SetPetImage(DataPet.openPet.calmness);
                for (int j = 4; j < 10; j++)
                {
                    Canvas.SetLeft(pet, Canvas.GetLeft(pet) - 2);
                    Canvas.SetTop(pet, Canvas.GetTop(pet) - j);
                    await Task.Delay(1);
                }
                for (int j = 10; j > 5; j--)
                {
                    Canvas.SetLeft(pet, Canvas.GetLeft(pet) - 2);
                    Canvas.SetTop(pet, Canvas.GetTop(pet) + j);
                    await Task.Delay(1);
                }
                await Drive((int)Canvas.GetLeft(pet), (int)posY);
            }
        }

        private async Task JumpRight()
        {
            if (!DataPet.movePet)
            {
                double posY = Canvas.GetTop(pet);
                SetPetImage(DataPet.openPet.calmness);
                if (!DataPet.spritePetRith)
                {
                    ApplyFlip();
                    DataPet.spritePetRith = true;
                }
                for (int j = 4; j < 10; j++)
                {
                    Canvas.SetLeft(pet, Canvas.GetLeft(pet) + 2);
                    Canvas.SetTop(pet, Canvas.GetTop(pet) - j);
                    await Task.Delay(1);
                }
                for (int j = 10; j > 5; j--)
                {
                    Canvas.SetLeft(pet, Canvas.GetLeft(pet) + 2);
                    Canvas.SetTop(pet, Canvas.GetTop(pet) + j);
                    await Task.Delay(1);
                }
                await Drive((int)Canvas.GetLeft(pet), (int)posY);
            }
        }

        private async Task Turn()
        {
            ApplyFlip();
            DataPet.spritePetRith = !DataPet.spritePetRith;
            await Task.CompletedTask;
        }

        private async void Pet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            DataPet.movePet = true;
            pet.CaptureMouse();
            SetPetImage(DataPet.openPet.active);
            await Drive((int)Canvas.GetLeft(pet), (int)Canvas.GetTop(pet));
            DataPet.boredomStatus = 0;
        }

        private void Pet_MouseMove(object sender, MouseEventArgs e)
        {
            if (DataPet.movePet && isDragging)
            {
                var pos = e.GetPosition(this);
                // смещение чтобы указатель был примерно по центру питомца
                Canvas.SetLeft(pet, pos.X - pet.Width / 3);
                Canvas.SetTop(pet, pos.Y - pet.Height / 3);
            }
        }

        private async void Pet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            DataPet.movePet = false;
            pet.ReleaseMouseCapture();
            SetPetImage(DataPet.openPet.calmness);
            await Drive((int)Canvas.GetLeft(pet), (int)Canvas.GetTop(pet));
        }

        private async void Pet_MouseLeave(object sender, MouseEventArgs e)
        {
            SetPetImage(DataPet.openPet.calmness);
            DataPet.movePet = false;
            await Drive((int)Canvas.GetLeft(pet), (int)Canvas.GetTop(pet));
        }

        private async void Pet_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            // как в старом коде — правый клик открывает меню
            if (!DataPet.openMenu)
            {
                DataPet.Menu = new MenuWindow(this);
                DataPet.Menu.Show();
                DataPet.openMenu = true;
            }
            else
            {
                var w = DataPet.Menu;
                if (w != null) w.WindowState = WindowState.Normal;
            }
        }

        private async Task Jump()
        {
            SetPetImage(DataPet.openPet.active);
            int speed = 0;
            Canvas.SetLeft(pet, (SystemParameters.PrimaryScreenWidth - pet.Width) / 2);
            Canvas.SetTop(pet, SystemParameters.PrimaryScreenHeight);
            for (double i = Canvas.GetTop(pet); i > SystemParameters.PrimaryScreenHeight / 1.4;)
            {
                speed++;
                i -= speed;
            }
            while (true)
            {
                Canvas.SetTop(pet, Canvas.GetTop(pet) - speed);
                speed--;
                await Task.Delay(1);
                if (Canvas.GetTop(pet) < SystemParameters.PrimaryScreenHeight / 1.4) break;
            }
            await Drive((int)Canvas.GetLeft(pet), (int)Canvas.GetTop(pet));
            SetPetImage(DataPet.openPet.calmness);
        }

        private async Task Drive(int X, int Y)
        {
            int speed = 1;
            // границы
            double maxX = SystemParameters.PrimaryScreenWidth - pet.ActualWidth;
            double maxY = SystemParameters.PrimaryScreenHeight - pet.ActualHeight;

            if (Canvas.GetLeft(pet) > maxX) Canvas.SetLeft(pet, maxX);
            else if (Canvas.GetLeft(pet) < 0) Canvas.SetLeft(pet, 0);

            if (Canvas.GetTop(pet) > maxY) Canvas.SetTop(pet, maxY);
            else if (Canvas.GetTop(pet) < 0) Canvas.SetTop(pet, 0);

            if (!DataPet.movePet)
            {
                while (Canvas.GetTop(pet) < maxY - 1)
                {
                    Canvas.SetTop(pet, Canvas.GetTop(pet) + speed);
                    await Task.Delay(1);
                    speed++;
                    if (Canvas.GetTop(pet) >= maxY)
                    {
                        Canvas.SetTop(pet, maxY);
                        break;
                    }
                }
            }
        }
    }
}
