using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using RadialAsMenu.Helper;
using RadialAsMenu.Model;
using Color = System.Windows.Media.Color;
using Path = System.Windows.Shapes.Path;
using Point = System.Windows.Point;

namespace RadialAsMenu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private List<RadialPad> _pads = new List<RadialPad>();

        private void MainWindow_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _pads.Last().Wheel(e.Delta);
        }
        
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            grdButtonRing.Children.Clear();

            RadialPad pad = new RadialPad(RadialPadMode.FullCircle, 0)
            {
                Height = 200,
                Width = 200
            };
            _pads.Add(pad);
            grdButtonRing.Children.Insert(0, pad);


        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var angle = _pads.Last().GetSelectAngle();
                //txtMessage.Text= angle.ToString();
                var pad = new RadialPad(RadialPadMode.HalfCircle, angle)
                {
                    Height = 250+ _pads.Count *50,
                    Width = 250 + _pads.Count * 50
                };
                _pads.Add(pad);
                grdButtonRing.Children.Insert(0, pad);
            }

            if (e.ChangedButton == MouseButton.Right)
            {
                if (_pads.Count <=1)
                {
                    return;
                }

                var pad = _pads.Last();
                grdButtonRing.Children.Remove(pad);
                _pads.Remove(pad);

            }
        }
    }
}
