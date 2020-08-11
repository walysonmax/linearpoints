
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace linearpoints.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrawPage : ContentPage
    {
        public DrawPage()
        {
            InitializeComponent();
        }

        Dictionary<long, SKPath> temporaryPaths = new Dictionary<long, SKPath>();
        List<SKPath> paths = new List<SKPath>();


        private void Canvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            var circleFill = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColors.Blue
            };
            var touchPathStroke = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Purple,
                StrokeWidth = 10
            };           
            foreach (var item in paths)
                canvas.DrawCircle(item.Points[0], 10, circleFill);
            if (paths.Count == 2)
            {
                var startPoint = paths.FirstOrDefault().Points[0];
                var endPoint = paths.LastOrDefault().Points[0];
                var path = new SKPath();
                path.MoveTo(startPoint);
                path.LineTo(endPoint);
                canvas.DrawPath(path, touchPathStroke);
                distance.Text = (GetDistance(startPoint, endPoint)).ToString();

            }
        }

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            if (paths.Count > 1)
                return;

            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    var p = new SKPath();
                    p.MoveTo(e.Location);
                    paths.Add(p);
                    ((SKCanvasView)sender).InvalidateSurface();                   
                    break;                
            }         
            e.Handled = true;
        }
        SKPoint ConvertToPixel(SKPoint pt)
        {
            return new SKPoint((float)(Canvas.CanvasSize.Width * pt.X / Canvas.Width),
                               (float)(Canvas.CanvasSize.Height * pt.Y / Canvas.Height));
        }

        double GetDistance(SKPoint firstPoint, SKPoint endPoint) =>
             Math.Sqrt(Math.Pow(endPoint.Y - firstPoint.Y, 2) +  Math.Pow(endPoint.X - firstPoint.X, 2));

        private void Clear_Clicked(object sender, EventArgs e)
        {
            paths.Clear();
            temporaryPaths.Clear();
            distance.Text = "";
            Canvas.InvalidateSurface();
        }

        
    }
}