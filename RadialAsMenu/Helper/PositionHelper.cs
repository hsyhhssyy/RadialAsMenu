using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace RadialAsMenu.Helper
{
    public class PositionHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPos()
        {
            POINT point;
            if (GetCursorPos(out point))
            {
                Point mousePosition = new Point(point.X, point.Y);
                return mousePosition;
            }

            return default;
        }

        public static Point GetWindowCenter(Window window)
        {
            if (window == null)
                throw new ArgumentNullException(nameof(window));

            var hwndSource = PresentationSource.FromVisual(window) as HwndSource;
            if (hwndSource == null)
                throw new ArgumentException("The specified window is not yet initialized.", nameof(window));

            var windowWidth = window.ActualWidth;
            var windowHeight = window.ActualHeight;

            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;

            var dpiX = GetDpi(hwndSource.Handle);
            var dpiY = GetDpi(hwndSource.Handle);

            var scaleX = dpiX / 96.0;
            var scaleY = dpiY / 96.0;

            var scaledWindowWidth = windowWidth * scaleX;
            var scaledWindowHeight = windowHeight * scaleY;

            var windowLeft = (screenWidth - scaledWindowWidth) / 2;
            var windowTop = (screenHeight - scaledWindowHeight) / 2;

            return new Point(windowLeft, windowTop);
        }

        private static double GetDpi(IntPtr hwnd)
        {
            var hwndSource = HwndSource.FromHwnd(hwnd);
            var matrix = hwndSource?.CompositionTarget?.TransformToDevice;
            return matrix?.M11 ?? 96.0;
        }
    }
}
