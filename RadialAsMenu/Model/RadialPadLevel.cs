using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using RadialAsMenu.Helper;
using Path = System.Windows.Shapes.Path;

namespace RadialAsMenu.Model
{
    public enum RadialPadMode
    {
        FullCircle,
        HalfCircle
    }

    internal class RadialPad:Grid
    {
        private readonly RadialPadMode _mode;
        private readonly double _centerAngle;
        private readonly List<Path> _paths = new List<Path>();
        private int currentWheel;

        private int ButtonCount = 13;

        public RadialPad(RadialPadMode mode, double centerAngle)
        {
            _mode = mode;
            _centerAngle = centerAngle;

            Loaded += RadialPad_Loaded;
            currentWheel = (int)(ButtonCount / 2);
        }

        private void RadialPad_Loaded(object sender, RoutedEventArgs e)
        {

            double angleDelta;
            double startAngle;
            double endAngle;

            if (_mode == RadialPadMode.FullCircle)
            {
                angleDelta = 360.0 / ButtonCount;
                startAngle = _centerAngle - 180;
                if (startAngle <= -360)
                {
                    startAngle += 360;
                }
                endAngle = startAngle + angleDelta;

            }
            else
            {
                angleDelta = 360.0 / ButtonCount / 2;
                startAngle = _centerAngle - 90;
                if (startAngle <= -360)
                {
                    startAngle += 360;
                }
                endAngle = startAngle + angleDelta;
            }

            _paths.Clear();
            Children.Clear();

            for (int i = 0; i < ButtonCount; i++)
            {
                var path = GeometryHelper.CreateSector(ActualWidth / 2, ActualHeight/ 2, startAngle, endAngle,
                    (ActualWidth+ActualHeight)/4);
                _paths.Add(path);

                Random random = new Random();
                Color randomColor = Color.FromArgb(
                    255,
                    (byte)random.Next(256),
                    (byte)random.Next(256),
                    (byte)random.Next(256));
                path.Fill = new SolidColorBrush(randomColor);

                Children.Add(path);

                if (FindResource("Ring2InAnimation") is Storyboard fadeInAnimation)
                {
                    path.Opacity = 0;
                    Storyboard.SetTarget(fadeInAnimation, path);
                    fadeInAnimation.Begin();
                }

                startAngle = endAngle;
                endAngle += angleDelta;
            }
        }

        public void Wheel(int delta)
        {
            var newDelta = currentWheel;
            if (delta > 0)
            {
                newDelta += 1;
            }
            else if (delta < 0)
            {
                newDelta -= 1;
            }

            if (_mode == RadialPadMode.FullCircle)
            {
                while (newDelta < 0)
                {
                    newDelta += ButtonCount;
                }

                while (newDelta >= ButtonCount)
                {
                    newDelta -= ButtonCount;
                }
            }
            else
            {
                if (newDelta < 0)
                {
                    newDelta = 0;
                }

                if (newDelta >= ButtonCount)
                {
                    newDelta = ButtonCount - 1;
                }
            }

            currentWheel = newDelta;

            var path = _paths[currentWheel];
            Random random = new Random();
            Color randomColor = Color.FromArgb(
                255,
                (byte)random.Next(256),
                (byte)random.Next(256),
                (byte)random.Next(256));
            path.Fill = new SolidColorBrush(randomColor);
        }

        public double GetSelectAngle()
        {
            double angleDelta;
            double startAngle;

            if (_mode == RadialPadMode.FullCircle)
            {
                angleDelta = 360.0 / ButtonCount;
                startAngle = _centerAngle - 180;
                if (startAngle <= -360)
                {
                    startAngle += 360;
                }

            }
            else
            {
                angleDelta = 360.0 / ButtonCount / 2;
                startAngle = _centerAngle - 90;
                if (startAngle <= -360)
                {
                    startAngle += 360;
                }
            }

            return startAngle + currentWheel * angleDelta + angleDelta / 2;
        }
    }
}
