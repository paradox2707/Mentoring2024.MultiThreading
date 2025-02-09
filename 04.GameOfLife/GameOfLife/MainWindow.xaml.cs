using System;
using System.Windows;
using System.Windows.Threading;

namespace GameOfLife
{
    public partial class MainWindow : Window
    {
        private Grid mainGrid;
        private DispatcherTimer timer;   // Generation timer
        private int genCounter;
        private AdWindow[] adWindows;

        public MainWindow()
        {
            InitializeComponent();
            mainGrid = new Grid(MainCanvas);

            timer = new DispatcherTimer();
            timer.Tick += OnTimer;
            timer.Interval = TimeSpan.FromMilliseconds(200);
        }

        private void StartAd()
        {
            adWindows = new AdWindow[2];
            for (int i = 0; i < 2; i++)
            {
                if (adWindows[i] == null)
                {
                    adWindows[i] = new AdWindow(this);
                    adWindows[i].Closed += AdWindowOnClosed;
                    adWindows[i].Top = this.Top + (330 * i) + 70;
                    adWindows[i].Left = this.Left + 240;
                    adWindows[i].Show();
                }
            }
        }

        private void AdWindowOnClosed(object sender, EventArgs eventArgs)
        {
            var adWindow = sender as AdWindow;
            if (adWindow != null)
            {
                adWindow.Closed -= AdWindowOnClosed;
                for (int i = 0; i < adWindows.Length; i++)
                {
                    if (adWindows[i] == adWindow)
                    {
                        adWindows[i] = null;
                        break;
                    }
                }
            }
        }

        private void Button_OnClick(object sender, EventArgs e)
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
                ButtonStart.Content = "Stop";
                StartAd();
            }
            else
            {
                timer.Stop();
                ButtonStart.Content = "Start";
                CloseAds();
            }
        }

        private void CloseAds()
        {
            if (adWindows != null)
            {
                foreach (var adWindow in adWindows)
                {
                    if (adWindow != null)
                    {
                        adWindow.Close();
                    }
                }
            }
        }

        private void OnTimer(object sender, EventArgs e)
        {
            mainGrid.Update();
            genCounter++;
            lblGenCount.Content = "Generations: " + genCounter;
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Clear();
        }
    }
}
