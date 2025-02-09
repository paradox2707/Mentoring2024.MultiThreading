using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GameOfLife
{
    class AdWindow : Window
    {
        private readonly DispatcherTimer adTimer;
        private int imgNmb;     // the number of the image currently shown
        private string link;    // the URL where the currently shown ad leads to
        private readonly ImageBrush myBrush; // Cached ImageBrush for performance

        public AdWindow(Window owner)
        {
            Owner = owner;
            Width = 350;
            Height = 100;
            ResizeMode = ResizeMode.NoResize;
            WindowStyle = WindowStyle.ToolWindow;
            Title = "Support us by clicking the ads";
            Cursor = Cursors.Hand;
            ShowActivated = false;
            MouseDown += OnClick;

            myBrush = new ImageBrush(); // Initialize the ImageBrush once

            // Initialize the random number generator
            Random rnd = new Random();
            imgNmb = rnd.Next(1, 4); // Changed to 1-4 to match cases

            ChangeAds(this, EventArgs.Empty); // Initialize the ad display

            // Run the timer that changes the ad's image 
            adTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3)
            };
            adTimer.Tick += ChangeAds;
            adTimer.Start();
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(link);
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            Unsubscribe(); // Unsubscribe from the event when the window is closed
            base.OnClosed(e);
        }

        public void Unsubscribe()
        {
            adTimer.Tick -= ChangeAds;
        }

        private void ChangeAds(object sender, EventArgs eventArgs)
        {
            switch (imgNmb)
            {
                case 1:
                    myBrush.ImageSource = new BitmapImage(new Uri("ad1.jpg", UriKind.Relative));
                    link = "http://example.com";
                    break;
                case 2:
                    myBrush.ImageSource = new BitmapImage(new Uri("ad2.jpg", UriKind.Relative));
                    link = "http://example.com";
                    break;
                case 3:
                    myBrush.ImageSource = new BitmapImage(new Uri("ad3.jpg", UriKind.Relative));
                    link = "http://example.com";
                    break;
            }

            Background = myBrush; // Set the background to the cached ImageBrush

            imgNmb++;
            if (imgNmb > 3) imgNmb = 1; // Reset to 1 after 3
        }
    }
}