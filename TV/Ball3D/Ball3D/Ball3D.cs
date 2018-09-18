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
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES20;
using OpenTK.Input;
using OpenTK.Platform.Tizen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ball3D
{
    public class Ball : TizenGameApplication
    {
        private static float UNIT_SIZE = 1.0f;
        private float r = 0.6f;
        private float toRadiansRate = 3.1415926f / 180;
        int angleSpan = 10;
        private float[] vertices, textures;
        int vCount = 0;
        SPlayer sPlayer = new SPlayer();
        Matrix4 umatrix;
        private static String A_TEXTURE_COORDINATES = "a_TextureCoordinates";
        private static String U_TEXTURE_UNIT = "u_TextureUnit";
        private int uTextureUnitLocation, aTextureCoordinates, texture, program, uMatrixLocation, aPositionLocation;
        private static String A_POSITION = "aPosition";
        private static String U_MATRIX = "uMVPMatrix";

        /// <summary>
        /// Init vertex data for every vertex. 
        /// </summary>
        public void InitVertexData()
        {
            List<float> alVertix = new List<float>();
            List<float> textureVertix = new List<float>();
            for (int vAngle = 0; vAngle < 180; vAngle = vAngle + angleSpan)
            {
                for (int hAngle = 0; hAngle <= 360; hAngle = hAngle + angleSpan)
                {
                    float x0 = (float)(r * UNIT_SIZE
                            * Math.Sin(vAngle * toRadiansRate) * Math.Cos(hAngle * toRadiansRate));
                    float y0 = (float)(r * UNIT_SIZE
                            * Math.Sin(vAngle * toRadiansRate) * Math.Sin(hAngle * toRadiansRate));
                    float z0 = (float)(r * UNIT_SIZE * Math.Cos(vAngle * toRadiansRate));

                    float x1 = (float)(r * UNIT_SIZE
                            * Math.Sin(vAngle * toRadiansRate) * Math.Cos((hAngle + angleSpan) * toRadiansRate));
                    float y1 = (float)(r * UNIT_SIZE
                            * Math.Sin(vAngle * toRadiansRate) * Math.Sin((hAngle + angleSpan) * toRadiansRate));
                    float z1 = (float)(r * UNIT_SIZE * Math.Cos(vAngle * toRadiansRate));

                    float x2 = (float)(r * UNIT_SIZE
                            * Math.Sin((vAngle + angleSpan) * toRadiansRate) * Math
                            .Cos((hAngle + angleSpan) * toRadiansRate));
                    float y2 = (float)(r * UNIT_SIZE
                            * Math.Sin((vAngle + angleSpan) * toRadiansRate) * Math
                            .Sin((hAngle + angleSpan) * toRadiansRate));
                    float z2 = (float)(r * UNIT_SIZE * Math.Cos((vAngle + angleSpan) * toRadiansRate));
                    float x3 = (float)(r * UNIT_SIZE
                            * Math.Sin((vAngle + angleSpan) * toRadiansRate) * Math
                            .Cos(hAngle * toRadiansRate));
                    float y3 = (float)(r * UNIT_SIZE
                            * Math.Sin((vAngle + angleSpan) * toRadiansRate) * Math
                            .Sin(hAngle * toRadiansRate));
                    float z3 = (float)(r * UNIT_SIZE * Math.Cos((vAngle + angleSpan) * toRadiansRate));
                    alVertix.Add(x1);
                    alVertix.Add(y1);
                    alVertix.Add(z1);
                    alVertix.Add(x3);
                    alVertix.Add(y3);
                    alVertix.Add(z3);
                    alVertix.Add(x0);
                    alVertix.Add(y0);
                    alVertix.Add(z0);
                    float s0 = hAngle / 360.0f;
                    float s1 = (hAngle + angleSpan) / 360.0f;
                    float t0 = 1 - vAngle / 180.0f;
                    float t1 = 1 - (vAngle + angleSpan) / 180.0f;

                    textureVertix.Add(s1);
                    textureVertix.Add(t0);
                    textureVertix.Add(s0);
                    textureVertix.Add(t1);
                    textureVertix.Add(s0);
                    textureVertix.Add(t0);
                    alVertix.Add(x1);
                    alVertix.Add(y1);
                    alVertix.Add(z1);
                    alVertix.Add(x2);
                    alVertix.Add(y2);
                    alVertix.Add(z2);
                    alVertix.Add(x3);
                    alVertix.Add(y3);
                    alVertix.Add(z3);

                    textureVertix.Add(s1);
                    textureVertix.Add(t0);
                    textureVertix.Add(s1);
                    textureVertix.Add(t1);
                    textureVertix.Add(s0);
                    textureVertix.Add(t1);

                }
            }

            vCount = alVertix.Count() / 3;
            vertices = new float[vCount * 3];
            for (int i = 0; i < alVertix.Count(); i++)
            {
                vertices[i] = alVertix[i];
            }

            textures = new float[textureVertix.Count()];
            for (int i = 0; i < textureVertix.Count(); i++)
            {
                textures[i] = textureVertix[i];
            }
        }

        /// <summary>
        /// Preparing texture before drawing. 
        /// </summary>
        private void InitTexture()
        {
            aTextureCoordinates = GL.GetAttribLocation(program, A_TEXTURE_COORDINATES);
            uTextureUnitLocation = GL.GetAttribLocation(program, U_TEXTURE_UNIT);
            texture = LoadTexture(DirectoryInfo.Resource + "1.bmp", false);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.Uniform1(uTextureUnitLocation, 0);
        }

        private void GetProgram()
        {
            // Vertex shader
            string vertexShaderSrc = "uniform mat4 uMVPMatrix;   \n" +
                              "attribute vec4 aPosition;    \n" +
                              "attribute vec2 a_TextureCoordinates;    \n" +
                              "varying vec2 v_TextureCoordinates;" +
                              "void main()                  \n" +
                              "{                            \n" +
                              "   gl_Position = uMVPMatrix * aPosition;  \n" +
                              "   v_TextureCoordinates = a_TextureCoordinates;\n" +
                              "}                            \n";
            // Fragment shader
            string fragmentShaderSrc = "precision mediump float;             \n" +
                               "uniform sampler2D u_TextureUnit;             \n" +
                               "varying vec2 v_TextureCoordinates;           \n" +
                               "void main()                                  \n" +
                               "{                                            \n" +
                               "  gl_FragColor = texture2D(u_TextureUnit, v_TextureCoordinates);\n" +
                               "}                                            \n";
            program = ShaderHelper.BuildProgram(vertexShaderSrc, fragmentShaderSrc);
            GL.BindAttribLocation(program, 0, A_POSITION);
            GL.LinkProgram(program);
            GL.UseProgram(program);
        }

        /// <summary>
        /// Load texture from the image. 
        /// </summary>
        /// <param name="imagePath">path of image</param>
        /// <param name="isRepeat">whether repeat the image or not</param>
        /// <returns>texture Id</returns>
        private int LoadTexture(string imagePath, bool isRepeat)
        {
            int textureId;
            MBitmap bitm = MImageUtil.LoadImage(imagePath);
            byte[] pixels = bitm.byteBuffer;
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 4);
            unsafe
            {
                int* teid = &textureId;
                GL.GenTextures(1, teid);
                GL.BindTexture(TextureTarget.Texture2D, textureId);
                GL.TexImage2D(TextureTarget2d.Texture2D, 0, TextureComponentCount.Rgb, (int)bitm.inforHeader.biWidth, (int)bitm.inforHeader.biHeight, 0, PixelFormat.Rgb, PixelType.UnsignedByte, pixels);

            }

            if (isRepeat)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float)All.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float)All.ClampToEdge);
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)All.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)All.Linear);
            GL.GenerateMipmap(TextureTarget.Texture2D);
            return textureId;
        }

        /// <summary>
        /// Draw the ball. 
        /// </summary>
        public void Draw()
        {
            umatrix = MatrixState.GetFinalMatrix(Window.Height, Window.Width);
            GL.UniformMatrix4(uMatrixLocation, false, ref umatrix);
            GL.DrawArrays(PrimitiveType.Triangles, 0, vCount);
            GL.Finish();
            Window.SwapBuffers();
        }

        /// <summary>
        /// Called when the key board event coming. 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Key Event</param>
        public void OnKeyEvent(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                MatrixState.Rotate(80f, 0f, 360f);
            }

            else if (e.Key == Key.Left)
            {
                MatrixState.Rotate(-80f, 0f, 360f);
            }

            else if (e.Key == Key.Escape)
            {
                Exit();
            }

            Draw();
        }

        /// <summary>
        /// This will be called when app created
        /// </summary>
        protected override void OnCreate()
        {
            Window.RenderFrame += OnRenderFrame;
            Window.KeyDown += OnKeyEvent;
            Window.MouseMove += (sender, e) =>
            {
                if (e.Mouse[MouseButton.Left])
                {
                    float x = (float)(e.XDelta);
                    float y = (float)(e.YDelta);
                    MatrixState.Rotate(x, 0f, 360f);
                    Draw();
                }
            };
            InitVertexData();
            GetProgram();
            aPositionLocation = GL.GetAttribLocation(program, A_POSITION);
            uMatrixLocation = GL.GetUniformLocation(program, U_MATRIX);
            InitTexture();
            unsafe
            {
                fixed (float* vVer = vertices)
                {
                    GL.VertexAttribPointer(aPositionLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), new IntPtr(vVer));
                }

                fixed (float* teture = textures)
                {
                    GL.VertexAttribPointer(aTextureCoordinates, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), new IntPtr(teture));
                }
            }

            GL.EnableVertexAttribArray(aPositionLocation);
            GL.EnableVertexAttribArray(aTextureCoordinates);
            GL.ClearColor(Color4.SlateGray);
            GL.Enable(EnableCap.CullFace);
            sPlayer.SetSource(DirectoryInfo.Resource + "4.m4a");
            sPlayer.ToPlay(true);
        }

        /// <summary>
        /// Called when your window is resumed. Set your viewport here. It is also
        /// a good place to set up your projection matrix (which probably changes
        /// along when the aspect ratio of your window).
        /// </summary>
        protected override void OnResume()
        {
            // the surface change event makes your context
            // not be current, so be sure to make it current again
            Window.MakeCurrent();
            // Adjust the viewport based on geometry changes,
            // such as screen rotation
            GL.Viewport(0, 0, Window.Width, Window.Height);
        }

        /// <summary>
        /// Called when it is time to render the next frame. 
        /// </summary>
        /// <param name="oe">object</param>
        /// <param name="e">Frame Event</param>
        protected void OnRenderFrame(object oe, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Draw();
        }

        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="args">The main arguments</param>
        static void Main(string[] args)
        {
            var app = new Ball() { GLMajor = 2, GLMinor = 0 };
            app.Run(args);
        }
    }
}
