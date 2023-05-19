using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RadialAsMenu.Helper
{
    internal class GeometryHelper
    {
        public static Path CreateSector(double centerX,double centerY,double startAngle, double endAngle, double radius)
        {
            // 将角度转换为弧度
            var startAngleRad = (startAngle - 90) * Math.PI / 180;
            var endAngleRad = (endAngle - 90) * Math.PI / 180;

            // 计算起点和终点的坐标
            var startX = centerX + radius * Math.Cos(startAngleRad);
            var startY = centerY + radius * Math.Sin(startAngleRad);
            var endX = centerX + radius * Math.Cos(endAngleRad);
            var endY = centerY + radius * Math.Sin(endAngleRad);

            // 生成 SVG 路径
            var geometryData = "M " + centerX + "," + centerY +
                               " L " + startX + "," + startY +
                               " A " + radius + "," + radius + " 0 0 1 " + endX + "," + endY +
                               " Z";
            
            Geometry geometry = Geometry.Parse(geometryData);

            Path path = new Path
            {
                Data = geometry
            };

            return path;
        }
    }
}
