/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using System;
using System.Runtime.InteropServices;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES20;
using OpenTK.Input;
using OpenTK.Platform;
using OpenTK.Platform.Tizen;
using SkiaSharp;

namespace OpenTKSample
{
    public class OpenTKCubeWithSkiaSharp : TizenGameApplication
    {
        // Handle to a program object
        private int mProgramHandle;
        // Attribute locations
        private int positionLoc, texCoordLoc, textureLoc, mvpLoc;
        // MainWindow
        private IGameWindow mainWindow;

        // Memory block which SKSurface will be create on it.
        private IntPtr pBitMap;
        // SKSurface used to draw text, it created on the memory block
        private SKSurface surface;
        // SKCanvas used to draw text
        private SKCanvas canvas;

        // row bytes of bitmap
        private int rowByte;
        // size of bitmap
        private int bitmapHeight, bitmapWidth;

        // angle of the cube
        float angleX = 45.0f, angleY = 45.0f;

        // vertex array
        private readonly float[] vertices;
        // texture coordinate array
        private readonly float[] textCoord;

        // mvp matrix
        Matrix4 mvpMatrix;
        // view matrix
        Matrix4 viewMatrix;
        // model matrix
        Matrix4 modelMatrix;

        // vertex shader source
        private readonly string vertexShaderSrc = "uniform mat4 u_mvpMatrix;          \n" +
                                "attribute vec4 a_position;                 \n" +
                                "attribute vec2 a_texCoord;                 \n" +
                                "varying vec2 v_texCoord;                   \n" +
                                "void main()                                \n" +
                                "{                                          \n" +
                                "   gl_Position = u_mvpMatrix * a_position; \n" +
                                "   v_texCoord = a_texCoord;            \n" +
                                "}                                          \n";
        // fragment shader source
        private readonly string fragmentShaderSrc = "precision mediump float;                \n" +
                                "varying vec2 v_texCoord;                    \n" +
                                "uniform sampler2D s_texture;                \n" +
                                "void main()                                 \n" +
                                "{                                           \n" +
                                "  gl_FragColor = texture2D( s_texture, v_texCoord );\n" +
                                "}                                           \n";
        // texture id
        private int _texture;

        public OpenTKCubeWithSkiaSharp()
        {
            vertices = new float[]
            {
                /* front surface is blue */
                0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f,
                -0.5f, -0.5f, 0.5f, 0.0f, 0.0f, 1.0f,
                0.5f, -0.5f, 0.5f, 0.0f, 0.0f, 1.0f,
                0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f,
                -0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f,
                -0.5f, -0.5f, 0.5f, 0.0f, 0.0f, 1.0f,
                /* left surface is green */
                -0.5f, 0.5f, 0.5f, 0.0f, 1.0f, 0.0f,
                -0.5f, -0.5f, -0.5f, 0.0f, 1.0f, 0.0f,
                -0.5f, -0.5f, 0.5f, 0.0f, 1.0f, 0.0f,
                -0.5f, 0.5f, 0.5f, 0.0f, 1.0f, 0.0f,
                -0.5f, 0.5f, -0.5f, 0.0f, 1.0f, 0.0f,
                -0.5f, -0.5f, -0.5f, 0.0f, 1.0f, 0.0f,
                /* top surface is red */
                -0.5f, 0.5f, 0.5f, 1.0f, 0.0f, 0.0f,
                0.5f, 0.5f, -0.5f, 1.0f, 0.0f, 0.0f,
                -0.5f, 0.5f, -0.5f, 1.0f, 0.0f, 0.0f,
                -0.5f, 0.5f, 0.5f, 1.0f, 0.0f, 0.0f,
                0.5f, 0.5f, 0.5f, 1.0f, 0.0f, 0.0f,
                0.5f, 0.5f, -0.5f, 1.0f, 0.0f, 0.0f,
                /* right surface is yellow */
                0.5f, 0.5f, -0.5f, 1.0f, 1.0f, 0.0f,
                0.5f, -0.5f, 0.5f, 1.0f, 1.0f, 0.0f,
                0.5f, -0.5f, -0.5f, 1.0f, 1.0f, 0.0f,
                0.5f, 0.5f, -0.5f, 1.0f, 1.0f, 0.0f,
                0.5f, 0.5f, 0.5f, 1.0f, 1.0f, 0.0f,
                0.5f, -0.5f, 0.5f, 1.0f, 1.0f, 0.0f,
                /* back surface is cyan */
                -0.5f, 0.5f, -0.5f, 0.0f, 1.0f, 1.0f,
                0.5f, -0.5f, -0.5f, 0.0f, 1.0f, 1.0f,
                -0.5f, -0.5f, -0.5f, 0.0f, 1.0f, 1.0f,
                -0.5f, 0.5f, -0.5f, 0.0f, 1.0f, 1.0f,
                0.5f, 0.5f, -0.5f, 0.0f, 1.0f, 1.0f,
                0.5f, -0.5f, -0.5f, 0.0f, 1.0f, 1.0f,
                /* bottom surface is magenta */
                -0.5f, -0.5f, -0.5f, 1.0f, 0.0f, 1.0f,
                0.5f, -0.5f, 0.5f, 1.0f, 0.0f, 1.0f,
                -0.5f, -0.5f, 0.5f, 1.0f, 0.0f, 1.0f,
                -0.5f, -0.5f, -0.5f, 1.0f, 0.0f, 1.0f,
                0.5f, -0.5f, -0.5f, 1.0f, 0.0f, 1.0f,
                0.5f, -0.5f, 0.5f, 1.0f, 0.0f, 1.0f
            };

            textCoord = new float[]
            {
                /* Texture coordinate of front surface*/
                1.0f, 0.0f,   0.0f, 1.0f,   1.0f, 1.0f,   1.0f, 0.0f,   0.0f, 0.0f,   0.0f, 1.0f,

                /* Texture coordinate of left surface*/
                1.0f, 0.0f,   0.0f, 1.0f,   1.0f, 1.0f,   1.0f, 0.0f,   0.0f, 0.0f,   0.0f, 1.0f,

                /* Texture coordinate of top surface*/
                1.0f, 0.0f,   0.0f, 1.0f,   1.0f, 1.0f,   1.0f, 0.0f,   0.0f, 0.0f,   0.0f, 1.0f,

                /* Texture coordinate of right surface*/
                1.0f, 0.0f,   0.0f, 1.0f,   1.0f, 1.0f,   1.0f, 0.0f,   0.0f, 0.0f,   0.0f, 1.0f,

                /* Texture coordinate of back surface*/
                1.0f, 0.0f,   0.0f, 1.0f,   1.0f, 1.0f,   1.0f, 0.0f,   0.0f, 0.0f,   0.0f, 1.0f,

                /* Texture coordinate of bottom surface*/
                1.0f, 0.0f,   0.0f, 1.0f,   1.0f, 1.0f,   1.0f, 0.0f,   0.0f, 0.0f,   0.0f, 1.0f,
            };
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            mainWindow = Window;

            mainWindow.MouseMove += OnMouseMoveEvent;
            mainWindow.KeyDown += OnKeyEvent;
            mainWindow.RenderFrame += OnRenderFrame;

            LoadApp();
        }

        protected override void OnTerminate()
        {
            base.OnTerminate();
            GL.DeleteTextures(1, ref _texture);
            FreeBitmap();
        }

        /// <summary>
        /// Initialize the shaders of the program
        /// </summary>
        private void InitShader()
        {
            mProgramHandle = ShaderHelper.BuildProgram(vertexShaderSrc, fragmentShaderSrc);
            GL.BindAttribLocation(mProgramHandle, 0, "a_position");
            GL.BindAttribLocation(mProgramHandle, 1, "a_texCoord");
            GL.LinkProgram(mProgramHandle);

            GL.UseProgram(mProgramHandle);
        }

        private void LoadApp()
        {
            InitShader();

            GL.ClearColor(Color4.DarkSlateGray);
            GL.Enable(EnableCap.DepthTest);

            CreateBitmap();
            CreateSKCanvas();
            DrawTextOnSkCanvas();
            Create2DTextureFromMemory();
        }

        private void OnUnload(Object sender, EventArgs e)
        {
            GL.DeleteTextures(1, ref _texture);
            FreeBitmap();
        }

        /// <summary>
        /// Called when it is time to render the next frame. Add your rendering code here.
        /// </summary>
        /// <param name="sender">the subject of RenderFrame Event </param>
        /// <param name="e">Contains timing information.</param>
        private void OnRenderFrame(Object sender, FrameEventArgs e)
        {
            GL.Viewport(0, 0, mainWindow.Width, mainWindow.Height);
            Rotate(1.0f, 1.0f);

            GL.ClearColor(Color4.CornflowerBlue);
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(mProgramHandle);
            positionLoc = GL.GetAttribLocation(mProgramHandle, "a_position");
            texCoordLoc = GL.GetAttribLocation(mProgramHandle, "a_texCoord");

            textureLoc = GL.GetUniformLocation(mProgramHandle, "s_texture");
            GL.Uniform1(textureLoc, 0);

            unsafe
            {
                fixed (float* pvertices = vertices)
                {
                    // Prepare the vertex coordinate data
                    GL.VertexAttribPointer(positionLoc, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), new IntPtr(pvertices));
                    GL.EnableVertexAttribArray(positionLoc);
                }

                fixed (float* texCoord = textCoord)
                {
                    // Prepare the texture coordinate data
                    GL.VertexAttribPointer(texCoordLoc, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), new IntPtr(texCoord));
                    GL.EnableVertexAttribArray(texCoordLoc);
                }

            }

            mvpLoc = GL.GetUniformLocation(mProgramHandle, "u_mvpMatrix");

            // Apply the projection and view transformation
            GL.UniformMatrix4(mvpLoc, false, ref mvpMatrix);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            GL.Finish();

            // Disable vertex array
            GL.DisableVertexAttribArray(positionLoc);
            mainWindow.SwapBuffers();
        }

        /// <summary>
        /// This will be called when mouse move
        /// </summary>
        /// <param name="sender">target instance</param>
        /// <param name="e">mouse move event arguments</param>
        private void OnMouseMoveEvent(object sender, MouseMoveEventArgs e)
        {
            if (e.Mouse[MouseButton.Left])
            {
                float x = (float)(e.XDelta);
                float y = (float)(e.YDelta);
                Rotate(-x, -y);
            }
        }

        /// <summary>
        /// This will be called when key pressed down
        /// </summary>
        /// <param name="sender">target instance</param>
        /// <param name="e">keyboard key event arguments</param>
        private void OnKeyEvent(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                Rotate(-80f, 0f);
            }

            else if (e.Key == Key.Left)
            {
                Rotate(80f, 0f);

            }

            else if (e.Key == Key.Escape)
            {
                FreeBitmap();
                Exit();
            }
        }

        /// <summary>
        /// Allocate a memory block with the size of bitmap.
        /// </summary>
        private void CreateBitmap()
        {
            FreeBitmap();

            // set bitmap size as half of the window size
            bitmapHeight = (int)(0.5f * mainWindow.Height);
            bitmapWidth = (int)(0.5f * mainWindow.Width);
            pBitMap = Marshal.AllocHGlobal(bitmapWidth * bitmapHeight * 4);

            rowByte = bitmapWidth * 4;
        }

        /// <summary>
        /// Release memory resource of bitmap.
        /// </summary>
        private void FreeBitmap()
        {
            if (pBitMap != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(pBitMap);
                pBitMap = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Create SKCanvas on the memory block
        /// </summary>
        private void CreateSKCanvas()
        {
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 4);

            // create the SKSurface
            var info = new SKImageInfo(bitmapWidth, bitmapHeight, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
            surface = SKSurface.Create(info, pBitMap, rowByte);
            if (surface != null)
            {
                canvas = surface.Canvas;
            }
        }

        /// <summary>
        /// Draw text on SKCanvas
        /// </summary>
        private void DrawTextOnSkCanvas()
        {
            if (canvas != null)
            {
                DrawTextBySkiaSharp(canvas, bitmapWidth, bitmapHeight);

                canvas.Flush();
            }
        }

        /// <summary>
        /// Draw text on the canvas.
        /// </summary>
        /// <param name="canvas">The target canvas</param>
        /// <param name="width">width of canvas</param>
        /// <param name="height">height of canvas</param>
        private void DrawTextBySkiaSharp(SKCanvas canvas, int width, int height)
        {
            canvas.DrawColor(SKColors.White);

            using (var paint = new SKPaint())
            {
                paint.TextSize = 64.0f;
                paint.IsAntialias = true;
                paint.Color = SKColors.Red;
                paint.IsStroke = false;
                paint.TextAlign = SKTextAlign.Center;

                canvas.DrawText("SkiaSharp", width / 2f, 144.0f, paint);
            }

            using (var paint = new SKPaint())
            {
                paint.TextSize = 64.0f;
                paint.IsAntialias = true;
                paint.Color = (SKColor)0xFF9CAFB7;
                paint.IsStroke = true;
                paint.StrokeWidth = 3;
                paint.TextAlign = SKTextAlign.Center;

                canvas.DrawText("SkiaSharp", width / 2f, 244.0f, paint);
            }

            using (var paint = new SKPaint())
            {
                paint.TextSize = 64.0f;
                paint.IsAntialias = true;
                paint.Color = (SKColor)0xFFE6B89C;
                paint.TextScaleX = 1.2f;
                paint.TextAlign = SKTextAlign.Center;

                canvas.DrawText("SkiaSharp", width / 2f, 344.0f, paint);
            }
        }

        /// <summary>
        /// Create 2D texture from memory block
        /// </summary>
        private void Create2DTextureFromMemory()
        {
            GL.TexImage2D(TextureTarget2d.Texture2D, 0, TextureComponentCount.Rgba, bitmapWidth, bitmapHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pBitMap);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)All.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)All.Linear);
            GL.GenerateMipmap(TextureTarget.Texture2D);
        }

        /// <summary>
        /// Rotate the cube
        /// </summary>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Position</param>
        private void Rotate(float x, float y)
        {
            angleX += (x * 0.2f);
            if (angleX >= 360.0f)
            {
                angleX -= 360.0f;
            }

            angleY += (y * 0.2f);
            if (angleY >= 360.0f)
            {
                angleY -= 360.0f;
            }

            MatrixHelper.EsMatrixLoadIdentity(ref viewMatrix);
            viewMatrix = MatrixHelper.EsPerspective(60.0f, (float)mainWindow.Width / (float)mainWindow.Height, 1.0f, 20.0f, viewMatrix);

            MatrixHelper.EsMatrixLoadIdentity(ref modelMatrix);
            MatrixHelper.EsTranslate(ref modelMatrix, 0.0f, 0.0f, -2.5f);
            if (y == 0)
            {
                MatrixHelper.EsRotate(ref modelMatrix, angleX, 0.0f, 1.0f, 0.0f);
            }
            else
            {
                MatrixHelper.EsRotate(ref modelMatrix, angleX, angleY / angleX, 1.0f, 0.0f);
            }

            mvpMatrix = Matrix4.Mult(modelMatrix, viewMatrix);
        }

        static void Main(string[] args)
        {
            // The 'using' idiom guarantees proper resource cleanup.
            // We request 30 UpdateFrame events per second, and unlimited
            // RenderFrame events (as fast as the computer can handle).
            using (OpenTKCubeWithSkiaSharp game = new OpenTKCubeWithSkiaSharp() { GLMajor = 2, GLMinor = 0 })
            {
                game.Run(args);
            }
        }
    }
}
