using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using SkiaSharp.Views.Forms;
using SkiaSharp;

namespace PlayingWithHWInputs
{
    enum ONGOING_EVENT_STATE
    {
        RIGHT_DIRECTION_ROTATE,
        LEFT_DIRECTION_ROTATE,
        BACK_BUTTON_PRESS,
        FINISHED
    }
    public class RotaryEventPage : CirclePage, IRotaryEventReceiver
    {
        // public properties
        public Color StartColor { get; set; } = Color.Transparent;
        public Color EndColor { get; set; } = Color.Transparent;
        public bool Horizontal { get; set; } = false;

        // private fields
        bool rotating; // indicating currently on rotating
        double angle; // calculate angle to rotate
        const int kRotateMax = 3; // Need to rotate 3 times
        string instruction = ""; // The text will be shown on the page
        ONGOING_EVENT_STATE currentState;
        SKCanvasView canvasView;
        int rightDirectionRotateCount = kRotateMax;
        int leftDirectionRotateCount = kRotateMax;
        SKPaint redFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Red,
        };
        SKPaint blackStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true,
        };

        public RotaryEventPage()
        {
            // NOTICE: IRotaryEventReceiver object MUST set 'RotaryFocusObject' to get the event.
            RotaryFocusObject = this;

            // Initialize variables to set the state to rotating right
            currentState = ONGOING_EVENT_STATE.RIGHT_DIRECTION_ROTATE;
            instruction = "Rotate the bezel to the right.";
            angle = 0;

            // Initialize gradient to draw arrows
            // Create the main canvas new
            canvasView = new SKCanvasView();
            canvasView.HeightRequest = 360;
            canvasView.WidthRequest = 360;

            canvasView.PaintSurface += (sender, e) =>
            {
                // Right arrow svg data
                SKPath rightDirectionArrow = SKPath.ParseSvgPathData(
                    "M2.5,4.375l1.5625,-1.25h-1.25a8,8,0,0,0,-6,-6.25a2,2,0,0,1,-0.625,3.125a4.6,4.6,0,0,1,5.875,3.125h-1.25Z"
                    );
                // Left arrow svg data
                SKPath leftDirectionArrow = SKPath.ParseSvgPathData(
                    "M-2.5,4.375L-4.0625,3.125H-2.8125A8,8,0,0,1,3.1875,-3.125A2,2,0,0,0,3.8125,0A4.6,4.6,0,0,0,-2.0625,3.125H-0.8125Z"
                    );
                // Get surface
                SKSurface canvasSurface = e.Surface;
                // Get canvas of surface
                SKCanvas canvas = canvasSurface.Canvas;

                canvas.Save();
                // Fill black
                canvas.Clear(SKColors.Black);
                if (currentState == ONGOING_EVENT_STATE.RIGHT_DIRECTION_ROTATE)
                {
                    canvas.Translate(e.Info.Width * 3 / 4, e.Info.Height / 4);
                    // Set scale for the path
                    SKMatrix matrix = SKMatrix.MakeScale(15, 15);
                    rightDirectionArrow.Transform(matrix);
                    // Set a shader for gradient 
                    var colors = new SKColor[] { SKColors.White, SKColors.Blue }; 
                    SKPoint startPoint = new SKPoint(0, 0);
                    SKPoint endPoint = new SKPoint(20, 20);
                    var shader = SKShader.CreateLinearGradient(startPoint, endPoint, colors, null, SKShaderTileMode.Clamp);
                    blackStrokePaint.Shader = shader;
                    canvas.DrawPath(rightDirectionArrow, blackStrokePaint);
                }
                else if (currentState == ONGOING_EVENT_STATE.LEFT_DIRECTION_ROTATE)
                {
                    canvas.Translate(e.Info.Width * 1 / 4, e.Info.Height / 4);
                    // Set scale for the path
                    SKMatrix matrix = SKMatrix.MakeScale(15, 15);
                    // Set a shader for gradient 
                    leftDirectionArrow.Transform(matrix);
                    var colors = new SKColor[] { SKColors.White, SKColors.Blue };
                    SKPoint startPoint = new SKPoint(20, 0);
                    SKPoint endPoint = new SKPoint(0, 20);
                    var shader = SKShader.CreateLinearGradient(startPoint, endPoint, colors, null, SKShaderTileMode.Clamp);
                    blackStrokePaint.Shader = shader;
                    canvas.DrawPath(leftDirectionArrow, blackStrokePaint);
                }
                canvas.Restore();
                // Text drawing
                SKPaint textPaint = new SKPaint
                {
                    Color = SKColors.White,
                    TextSize = 10,
                    IsAntialias = true
                };
                string str = instruction;
                float textWidth = textPaint.MeasureText(str);
                textPaint.TextSize = 0.7f * e.Info.Width * textPaint.TextSize / textWidth;
                // Draw instruction
                SKRect textBounds = new SKRect();
                textPaint.MeasureText(str, ref textBounds);
                float xText = e.Info.Width / 2 - textBounds.MidX;
                float yText = e.Info.Height / 2 - textBounds.MidY;
                canvas.DrawText(str, xText, yText, textPaint);
            };

            Content = new StackLayout
            {
                Children = {
                    canvasView
                }
            };
        }

        public void Rotate(RotaryEventArgs args)
        {
            if (rotating) return;

            rotating = true;

            if (currentState == ONGOING_EVENT_STATE.RIGHT_DIRECTION_ROTATE)
            {
                if (rightDirectionRotateCount > 0)
                {
                    rightDirectionRotateCount--;
                    angle += args.IsClockwise ? 30 : -30;
                }
                else
                {
                    angle = 0;
                    currentState = ONGOING_EVENT_STATE.LEFT_DIRECTION_ROTATE;
                    instruction = "Rotate the bezel to the left.";
                }
            }
            else if (currentState == ONGOING_EVENT_STATE.LEFT_DIRECTION_ROTATE)
            {
                if (leftDirectionRotateCount > 0)
                {
                    leftDirectionRotateCount--;
                    angle += args.IsClockwise ? 30 : -30;
                }
                else
                {
                    currentState = ONGOING_EVENT_STATE.BACK_BUTTON_PRESS;
                    instruction = "Press back button.";
                    angle = 0;
                }
            }

            canvasView.RotateTo(angle).ContinueWith(
                (b) =>
                {
                    rotating = false;
                    if (angle == 360)
                    {
                        canvasView.Rotation = 0;
                        angle = 0;
                    }
                });
            canvasView.InvalidateSurface();
        }

        protected override bool OnBackButtonPressed()
        {
            if (currentState == ONGOING_EVENT_STATE.BACK_BUTTON_PRESS)
            {
                instruction = "Yes, just like that.";
                canvasView.InvalidateSurface();
                currentState = ONGOING_EVENT_STATE.FINISHED;
                // If return value is true, this app will not be closed.
                return true; 
            }
            else
            {
                // If return value is false, it will be closed.
                return false;
            }
        }
    }
}