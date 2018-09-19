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
    /// <summary>
    /// Enumarate
    /// </summary>
    enum ONGOING_EVENT_STATE
    {
        // A state needs to rotate right to move to the next state
        RIGHT_DIRECTION_ROTATE,
        // A state needs to rotate left to move to the next state
        LEFT_DIRECTION_ROTATE,
        // A state needs to press back button to move to the next state
        BACK_BUTTON_PRESS,
        // The end state
        FINISHED
    }

    /// <summary>
    /// RotaryEventPage class
    /// It implements IRotaryEventReceiver to take rotary events
    ///  when app users turn the bezel clockwise or counter-clockwise.
    /// </summary>
    public class RotaryEventPage : CirclePage, IRotaryEventReceiver
    {
        // public properties
        // A color needed for gradient start
        public Color StartColor { get; set; } = Color.Transparent;
        // A color needed for gradient end
        public Color EndColor { get; set; } = Color.Transparent;
        // TODO(vincent): 
        public bool Horizontal { get; set; } = false;

        // private fields
        // indicating currently on rotating
        bool rotating;
        // calculate angle to rotate
        double angle;
        // Need to rotate 3 times
        const int kRotateMax = 3;
        // The text will be shown on the page
        string instruction = "";
        // The current state of this app
        ONGOING_EVENT_STATE currentState;
        // SkiaSharp canvas view
        SKCanvasView canvasView;
        // # of rotation count to move to the next state
        int rightDirectionRotateCount = kRotateMax;
        // # of rotation count to move to the next state
        int leftDirectionRotateCount = kRotateMax;
        // SkiaSharp paint color red
        SKPaint redFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Red,
        };
        // SkiaSharp paint color black
        SKPaint blackStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            // Look smoother by turning on antialias
            IsAntialias = true,
        };

        /// <summary>
        /// Constructor
        /// </summary>
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
            // Need to set heightRequest and WidthReques to SkiaSharp canvas
            canvasView.HeightRequest = 360;
            canvasView.WidthRequest = 360;

            // Add tap gesture recognizer
            var tapGestureRecognizer = new TapGestureRecognizer();
            // Show a toast when tapped.
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                Toast.DisplayText("Tapped !!!");
            };
            // Add to GestureRecognizers in canvas view
            canvasView.GestureRecognizers.Add(tapGestureRecognizer);
            // This event handler actually draws in SkiaSharp canvas
            canvasView.PaintSurface += (sender, e) =>
            {
                // Right arrow svg data
                SKPath rightDirectionArrow = SKPath.ParseSvgPathData("M2.5,4.375l1.5625,-1.25h-1.25a8,8,0,0,0,-6,-6.25a2,2,0,0,1,-0.625,3.125a4.6,4.6,0,0,1,5.875,3.125h-1.25Z");
                // Left arrow svg data
                SKPath leftDirectionArrow = SKPath.ParseSvgPathData("M-2.5,4.375L-4.0625,3.125H-2.8125A8,8,0,0,1,3.1875,-3.125A2,2,0,0,0,3.8125,0A4.6,4.6,0,0,0,-2.0625,3.125H-0.8125Z");
                // Get surface
                SKSurface canvasSurface = e.Surface;
                // Get canvas of surface
                SKCanvas canvas = canvasSurface.Canvas;
                // Save canvas here to later retrieve
                canvas.Save();
                // Initialize canvas before drawing
                canvas.Clear(SKColors.Black);
                // Handling for the right rotation state
                if (currentState == ONGOING_EVENT_STATE.RIGHT_DIRECTION_ROTATE)
                {
                    // To draw arrow for rotating direction, move canvas point
                    canvas.Translate(e.Info.Width * 3 / 4, e.Info.Height / 4);
                    // Set scale for the path
                    SKMatrix matrix = SKMatrix.MakeScale(15, 15);
                    // Adjust scale before drawing
                    rightDirectionArrow.Transform(matrix);
                    // Set a shader for gradient 
                    var colors = new SKColor[] { SKColors.White, SKColors.Blue }; 
                    // Points needed for gradient
                    SKPoint startPoint = new SKPoint(0, 0);
                    SKPoint endPoint = new SKPoint(20, 20);
                    var shader = SKShader.CreateLinearGradient(startPoint, endPoint, colors, null, SKShaderTileMode.Clamp);
                    // Add the shader to paint to present gradient effect
                    blackStrokePaint.Shader = shader;
                    // Draw SVG path for right rotate direction arrow
                    canvas.DrawPath(rightDirectionArrow, blackStrokePaint);
                }
                else if (currentState == ONGOING_EVENT_STATE.LEFT_DIRECTION_ROTATE)
                {
                    // Move the canvas translate point
                    canvas.Translate(e.Info.Width * 1 / 4, e.Info.Height / 4);
                    // Set scale for the path
                    SKMatrix matrix = SKMatrix.MakeScale(15, 15);
                    // Set a shader for gradient 
                    leftDirectionArrow.Transform(matrix);
                    // Colors for gradient
                    var colors = new SKColor[] { SKColors.White, SKColors.Blue };
                    // Points for gradient
                    SKPoint startPoint = new SKPoint(20, 0);
                    SKPoint endPoint = new SKPoint(0, 20);
                    // Define a shader for left rotate arrow
                    var shader = SKShader.CreateLinearGradient(startPoint, endPoint, colors, null, SKShaderTileMode.Clamp);
                    blackStrokePaint.Shader = shader;
                    // Draw SVG path for left rotate direction arrow
                    canvas.DrawPath(leftDirectionArrow, blackStrokePaint);
                }

                // Need to reset canvas which were changed by Translate
                canvas.Restore();
                // Text drawing
                SKPaint textPaint = new SKPaint
                {
                    // Text color white
                    Color = SKColors.White,
                    // Text size 10
                    TextSize = 10,
                    // Antialiasing true
                    IsAntialias = true
                };
                string str = instruction;
                // Before draw a text, need to calculate total width of a text to locate in the middle
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
                Children =
                {
                    canvasView
                }
            };
        }

        /// <summary>
        /// Rotate
        /// </summary>
        /// <param name="args">RotaryEventArgs</param>
        public void Rotate(RotaryEventArgs args)
        {
            // When rotating (previous rotation is ongoing, do nothing.
            if (rotating)
            {
                return;
            }
            // Set ongoing flag true
            rotating = true;
            // Right rotate handling
            if (currentState == ONGOING_EVENT_STATE.RIGHT_DIRECTION_ROTATE)
            {
                // If at least one rotation performed.
                if (rightDirectionRotateCount > 0)
                {
                    // Decrease rotation count
                    rightDirectionRotateCount--;
                    // Increase the angle to rotate canvas
                    angle += args.IsClockwise ? 30 : -30;
                }
                else
                {
                    // Finished to rotate right. Set angle to zero for left rotate
                    angle = 0;
                    // Set the state to rotate left
                    currentState = ONGOING_EVENT_STATE.LEFT_DIRECTION_ROTATE;
                    // Change the instruction
                    instruction = "Rotate the bezel to the left.";
                }
            }
            // Left rotate handling
            else if (currentState == ONGOING_EVENT_STATE.LEFT_DIRECTION_ROTATE)
            {
                // If at least one rotation performed
                if (leftDirectionRotateCount > 0)
                {
                    // Decrease rotation count
                    leftDirectionRotateCount--;
                    // Increase the angle to rotate canvas.
                    angle += args.IsClockwise ? 30 : -30;
                }
                else
                {
                    // Done rotate left, and move to next state (back button)
                    currentState = ONGOING_EVENT_STATE.BACK_BUTTON_PRESS;
                    // Change the instruction
                    instruction = "Press back button.";
                    angle = 0;
                }
            }
            // Rotate the canvas
            canvasView.RotateTo(angle).ContinueWith(
                (b) =>
                {   
                    // Set this rotation done.
                    rotating = false;
                    // Angle should be set to 0 when it reaches 360. 
                    if (angle == 360)
                    {
                        canvasView.Rotation = 0;
                        angle = 0;
                    }
                });
            // Redraw the canvas
            canvasView.InvalidateSurface();
        }

        // If you inherited CirclePage and IRotaryEventListener,
        // then you can implement OnBackButtonPressed method which is 
        // called when the back button pressed. 
        protected override bool OnBackButtonPressed()
        {
            // If the back button is pressed in the right context, show the message.
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
