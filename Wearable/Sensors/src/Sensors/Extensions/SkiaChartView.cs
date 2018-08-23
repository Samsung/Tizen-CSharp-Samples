/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Sensors.Extensions
{
    /// <summary>
    /// Point class.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Property for time ticks.
        /// </summary>
        public long Ticks { get; set; }

        /// <summary>
        /// Property for X axis.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Property for Y axis.
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// Converts the instance to its equivalent string representation.
        /// </summary>
        /// <returns>Formatted text</returns>
        public override string ToString()
        {
            return "Ticks:" + Ticks + ", X:" + X + ", Y:" + Value;
        }
    }

    /// <summary>
    /// Series class.
    /// </summary>
    public class Series
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public Series()
        {
            Points = new List<Point>();
        }

        /// <summary>
        /// Property for series color.
        /// </summary>
        public SKColor Color { get; set; }

        /// <summary>
        /// Property for formatted text for legend.
        /// </summary>
        public string FormattedText { get; set; }

        /// <summary>
        /// Property for name of serie.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Property for points of serie.
        /// </summary>
        public List<Point> Points { get; set; }
    }

    /// <summary>
    /// Custom Skia chart view for drawing sensors values.
    /// </summary>
    public class SkiaChartView : SKCanvasView
    {
        /// <summary>
        /// Identifies the AutoScale bindable property.
        /// </summary>
        public static readonly BindableProperty AutoScaleProperty = BindableProperty.Create(nameof(AutoScale), typeof(bool), typeof(SkiaChartView), true, propertyChanged: OnPropertyChanged);

        /// <summary>
        /// Identifies the ChartScale bindable property.
        /// </summary>
        public static readonly BindableProperty ChartScaleProperty = BindableProperty.Create(nameof(ChartScale), typeof(float), typeof(SkiaChartView), BaseScale, BindingMode.TwoWay, propertyChanged: OnPropertyChanged);

        /// <summary>
        /// Identifies the LegendIsVisible bindable property.
        /// </summary>
        public static readonly BindableProperty LegendIsVisibleProperty = BindableProperty.Create(nameof(LegendIsVisible), typeof(bool), typeof(SkiaChartView), true, propertyChanged: OnPropertyChanged);

        /// <summary>
        /// Identifies the MaxTicksRange bindable property.
        /// </summary>
        public static readonly BindableProperty MaxTicksRangeProperty = BindableProperty.Create(nameof(MaxTicksRange), typeof(long), typeof(SkiaChartView), TimeSpan.FromMinutes(.5f).Ticks, propertyChanged: OnPropertyChanged);

        /// <summary>
        /// Identifies the Series bindable property.
        /// </summary>
        public static readonly BindableProperty SeriesProperty = BindableProperty.Create(nameof(Series), typeof(List<Series>), typeof(SkiaChartView), new List<Series>(), BindingMode.TwoWay, propertyChanged: OnPropertyChanged);

        private const float BaseScale = 100f;
        private long maxTicks;
        private long minTicks;

        /// <summary>
        /// Class constructor.
        /// </summary>
        public SkiaChartView()
        {
            PaintSurface += SkiaChartView_PaintSurface;
        }

        /// <summary>
        /// Property for turn on/off auto scale chart.
        /// </summary>
        public bool AutoScale
        {
            get { return (bool)GetValue(AutoScaleProperty); }
            set { SetValue(AutoScaleProperty, value); }
        }

        /// <summary>
        /// Property for get/set chart scale.
        /// </summary>
        public float ChartScale
        {
            get { return (float)GetValue(ChartScaleProperty); }
            set { SetValue(ChartScaleProperty, value); }
        }

        /// <summary>
        /// Property for control the legend visibility
        /// </summary>
        public bool LegendIsVisible
        {
            get { return (bool)GetValue(LegendIsVisibleProperty); }
            set { SetValue(LegendIsVisibleProperty, value); }
        }

        /// <summary>
        /// Property for get/set max tick range.
        /// </summary>
        public long MaxTicksRange
        {
            get { return (long)GetValue(MaxTicksRangeProperty); }
            set { SetValue(MaxTicksRangeProperty, value); }
        }

        /// <summary>
        /// Property for get/set series for drawing chart.
        /// </summary>
        public List<Series> Series
        {
            get { return (List<Series>)GetValue(SeriesProperty); }
            set { SetValue(SeriesProperty, value); }
        }

        private static void DrawAxisX(SKCanvas cr, float lineWidth)
        {
            var linePaint = new SKPaint()
            {
                Color = SKColors.Black,
                StrokeWidth = lineWidth,
                Style = SKPaintStyle.StrokeAndFill,
                IsAntialias = true
            };

            float width = cr.LocalClipBounds.Width;

            SKPoint[] arrowPoints = new SKPoint[]
            {
                new SKPoint(width,0),
                new SKPoint(width- 6*lineWidth, -3*lineWidth),
                new SKPoint(width- 6*lineWidth, 3*lineWidth),
            };
            SKPath arrowPath = new SKPath();
            arrowPath.AddPoly(arrowPoints, true);

            cr.DrawLine(0, 0, width - 6f * linePaint.StrokeWidth, 0, linePaint);

            cr.DrawPath(arrowPath, linePaint);
        }

        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as SkiaChartView).InvalidateSurface();
        }

        private void CalculateAutoScale(List<Series> series)
        {
            if (AutoScale)
            {
                var maxY = series.SelectMany(m => m.Points).Select(m => Math.Abs(m.Value)).Max();
                ChartScale = (BaseScale * (maxY / BaseScale)) * 2f;
            }
        }

        private void CalculateAxisX(SKRect localClipBounds)
        {
            foreach (var item in Series)
            {
                foreach (var point in item.Points)
                {
                    long ticks = point.Ticks - minTicks;
                    point.X = (ticks / (float)MaxTicksRange) * localClipBounds.Width;
                }
            }
        }

        private void CheckRangeData()
        {
            maxTicks = Series.Select(m => m.Points.Last()).Max(m => m.Ticks);
            minTicks = Series.Select(m => m.Points.First()).Min(m => m.Ticks);
            if (maxTicks - minTicks <= MaxTicksRange)
            {
                return;
            }
            else
            {
                minTicks = maxTicks - MaxTicksRange;

                foreach (var item in Series)
                {
                    item.Points.RemoveAll(m => m.Ticks < minTicks);
                }
            }
        }

        private void DrawLegend(SKCanvas cr, SKImageInfo info)
        {
            cr.Scale(1, -1);

            SKPaint textPaint = new SKPaint
            {
                TextAlign = SKTextAlign.Center
            };

            float legendWidth = 0;
            List<LegendSeries> legendSeries = new List<LegendSeries>();
            foreach (var item in Series)
            {
                string str = string.Format(item.FormattedText, item.Points.Last().Value);
                var width = textPaint.MeasureText(str);
                legendSeries.Add(new LegendSeries(str, item.Color));
                legendWidth += width;
            }

            float percentWidthText = .6f;
            textPaint.TextSize = percentWidthText * cr.LocalClipBounds.Width * textPaint.TextSize / legendWidth;

            float shiftX = cr.LocalClipBounds.Width / (Series.Count + 1);
            float startX = shiftX;

            foreach (var item in legendSeries)
            {
                textPaint.Color = item.Color;
                cr.DrawText(item.Name, startX, -.2f, textPaint);
                startX += shiftX;
            }
        }

        private void DrawSeries(SKCanvas cr, Series serie, float lineWidth)
        {
            var linePaint = new SKPaint()
            {
                Color = serie.Color,
                StrokeWidth = lineWidth,
                Style = SKPaintStyle.Stroke,
                IsAntialias = true
            };

            SKPath seriesPath = new SKPath();

            for (int i = 0; i < serie.Points.Count; i++)
            {
                var point = new SKPoint(serie.Points[i].X, serie.Points[i].Value / ChartScale);
                if (i > 0)
                {
                    seriesPath.LineTo(point);
                }
                else
                {
                    seriesPath.MoveTo(point);
                }
            }

            cr.DrawPath(seriesPath, linePaint);
        }

        private void Repaint(SKSurface surface, SKImageInfo info)
        {
            int scale = Math.Min(info.Width, info.Height);
            var cr = surface.Canvas;
            cr.Scale(scale, -scale);
            cr.Translate(0f, -0.5f);
            cr.Clear(SKColors.White);

            CheckRangeData();
            CalculateAxisX(cr.LocalClipBounds);
            CalculateAutoScale(Series);

            float lineWidth = 0.005f;
            DrawAxisX(cr, lineWidth);
            foreach (var item in Series)
            {
                DrawSeries(cr, item, lineWidth);
            }
            if (LegendIsVisible)
            {
                DrawLegend(cr, info);
            }
        }

        private void SkiaChartView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (Series.Count > 0 && Series.Any(m => m.Points.Count > 0))
            {
                Repaint(e.Surface, e.Info);
            }
        }

        private struct LegendSeries
        {

            internal string Name { get; set; }

            internal SKColor Color { get; set; }

            public LegendSeries(string name, SKColor color)
            {
                Name = name;
                Color = color;
            }
        }
    }
}