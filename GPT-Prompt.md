请用WPF设计一个窗体，该窗体是一个无边框异形窗体，外观类似一个圆形的拨号轮盘，轮盘外围有10个按钮，以扇形分布在轮盘周围。
鼠标在屏幕上移动时，会按照鼠标和窗体中心的相对位置，高亮显示对应的按钮。
请先给出XAML，我会在下一个问题询问后台代码。

<Window x:Class="DialerWindow.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="DialerWindow" Height="500" Width="500" WindowStyle="None" AllowsTransparency="True" Background="Transparent">
    <Grid>
    <!-- Place the dialer content here -->
    </Grid>
</Window>

我WPF有如下这段代码，通过函数获取了一个Path对象，然后将其加入一个Grid中。
var path2 = DrawSingleButton(startAngle, endAngle, 300);
Paths2.Add(path2);
// 添加 Path 对象到容器中
buttonRing2.Children.Add(path2);
现在我想实现一个动画效果，让他加入到容器时，有一个淡入效果，该如何实现？

M 300,300 L 12.152107915650845,215.48023294757098 A 300,300 0 0 1 47.623940150645666,137.80775476332067 Z

M 300,300 L 47.623940150645666,137.80775476332067 A 300,300 0 0 1 103.54177981641439,73.2751276937226 Z

