//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace SkiaSharp2DSample
{
    /// <summary>
    /// SkiaSharp sample main page class.
    /// This class creates a UI page to draw SkiaSharp clock.
    /// To use SkiaSharp in wearable app, you need to add following nuget dependencies:
    /// SkiaSharp (1.60.2 or higher),
    /// SkiaSharp.Views (1.60.2 or higher),
    /// SkiaSharp.Views.Forms (1.60.2 or higher),
    /// SkiaSharp.HarfBuzz (1.60.2 or higher),
    /// HarfBuzzSharp (1.4.6.1 or higher)
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
        SKPaint blackFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Black
        };

        SKPaint grayFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Gray
        };
        
        SKPaint whiteStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            Color = SKColors.White,
            IsAntialias = true,
        };

        SKPaint blackStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 20,
            StrokeCap = SKStrokeCap.Round,
            Color = SKColors.Black,
        };

        SKPaint whiteFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.White,
        };

        SKPaint redFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Red,
        };

        SKPaint greenFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.PaleGreen,
        };

        SKPaint blackStrokePaint2 = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Black,
            StrokeWidth = 20,
            StrokeCap = SKStrokeCap.Round
        };

        SKPaint backgroundFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill
        };

        SKPath hourHandPath = SKPath.ParseSvgPathData(
            "M 0 -60 C 0 -30 20 -30 5 -20 L 5 0 C 5 7.5 -5 7.5 -5 0 L -5 -20 C -20 -30 0 -30 0 -60");
        SKPath minHandPath = SKPath.ParseSvgPathData(
            "M 0 -80 C 0 -75 0 -70 2.5 -60 L 2.5 0 C 2.5 5 -2.5 5 -2.5 0 L -2.5 -60 C 0 -70 0 -75 0 -80");

        public MainPage()
        {
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(1f / 60), () =>
            {
                canvasView.InvalidateSurface();
                return true;
            });
        }
        private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.DrawPaint(backgroundFillPaint);
            int width = e.Info.Width;
            int height = e.Info.Height;

            DateTime dateTime = DateTime.Now;

            canvas.Translate(width / 2, height / 2);
            canvas.Scale(Math.Min(width / 80f, height / 200f));
            canvas.DrawCircle(0, -160, 75, blackFillPaint);
            canvas.DrawCircle(0, 0, 100, blackFillPaint);

            for (int angle = 0; angle < 360; angle += 6)
            {
                if (angle % 30 == 0)
                {
                    canvas.DrawCircle(0, -90, angle % 30 == 0 ? 4 : 2, redFillPaint);
                }
                else
                {
                    canvas.DrawCircle(0, -90, angle % 30 == 0 ? 4 : 2, whiteFillPaint);
                }
                canvas.RotateDegrees(6);
            }

            // Hour hand
            canvas.Save();
            canvas.RotateDegrees(30 * dateTime.Hour + dateTime.Minute / 2f);
            canvas.DrawPath(hourHandPath, grayFillPaint);
            canvas.DrawPath(hourHandPath, whiteStrokePaint);
            canvas.Restore();

            // Minute hand
            canvas.Save();
            canvas.RotateDegrees(6 * dateTime.Minute + dateTime.Second / 10f);
            canvas.DrawPath(minHandPath, grayFillPaint);
            canvas.DrawPath(minHandPath, whiteStrokePaint);
            canvas.Restore();

            // Second hand
            canvas.Save();
            float seconds = dateTime.Second + dateTime.Millisecond / 1000f;
            canvas.RotateDegrees(6 * seconds);
            canvas.DrawLine(0, 0, 0, -80, whiteStrokePaint);
            canvas.Restore();
        }
    }
}