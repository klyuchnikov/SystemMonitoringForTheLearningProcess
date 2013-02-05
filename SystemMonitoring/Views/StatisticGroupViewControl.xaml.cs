using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SystemMonitoring.Views
{
    public partial class StatisticGroupViewControl : UserControl
    {
        public StatisticGroupViewControl()
        {
            InitializeComponent();
            dispatcherTimer.Tick += delegate(object o, EventArgs args)
            {
                if (Event != null)
                    Event(this, args);
            };
        }

        private void ScrollViewer_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            var s = new ScrollViewer();

        }

        private void StackPanel_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            /* if (e.DeltaManipulation.Translation.Y != 0)
                 return;
             if (Event != null)
                 Event(this, e);*/
        }
        public event EventHandler<EventArgs> Event;

        private void scroll_MouseMove(object sender, MouseEventArgs e)
        {
            if (Event != null)
                Event(this, e);
        }

        private DispatcherTimer dispatcherTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(10) };
        private void scroll_Tap(object sender, GestureEventArgs e)
        {
           dispatcherTimer.Start();
        }

        private void scroll_Hold(object sender, GestureEventArgs e)
        {

        }
    }
}
