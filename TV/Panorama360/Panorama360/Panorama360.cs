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

namespace Panorama360
{
    /// <summary>
    /// 360 panorama
    /// </summary>
    public class Panorama360 : TizenGameApplication
    {
        private static float UNIT_SIZE = 1.0f;
        private float r = 0.6f, toRadiansRate = 3.1415926f / 180;
        int angleSpan = 10;
        private float[] vertices;
        int vCount = 3996;
        private float[] textures;
        private int program, uTextureUnitLocation, aTextureCoordinates, texture, uMatrixLocation, aPositionLocation;
        private SPlayer sPlayer = new SPlayer();
        Matrix4 umatrix;
        /// <summary>
        /// Init Vertex Data,To calculate the Vertices Data
        /// </summary>
        public void InitVertexData()
        {
            List<float> alVertix = new List<float>();
            List<float> textureVertix = new List<float>();
            vertices = new float[11988];
            textures = new float[7992];
            int veNum = 0, teNum = 0;
            //180 degrees in the vertical direction
            //360 degrees in the horizontal direction
            //Take a point every "angleSpan" degrees in the horizontal direction and horizontal direction 
            //Calculate the coordinates of each point
            for (int vAngle = 0; vAngle < 180; vAngle = vAngle + angleSpan)
            {
                for (int hAngle = 0; hAngle <= 360; hAngle = hAngle + angleSpan)
                {
                    //Index 1 point
                    vertices[veNum++] = (float)(r * UNIT_SIZE * Math.Sin(vAngle * toRadiansRate) * Math.Cos((hAngle + angleSpan) * toRadiansRate));
                    vertices[veNum++] = (float)(r * UNIT_SIZE * Math.Sin(vAngle * toRadiansRate) * Math.Sin((hAngle + angleSpan) * toRadiansRate));
                    vertices[veNum++] = (float)(r * UNIT_SIZE * Math.Cos(vAngle * toRadiansRate));
                    //Index 3 point
                    vertices[veNum++] = (float)(r * UNIT_SIZE * Math.Sin((vAngle + angleSpan) * toRadiansRate) * Math.Cos(hAngle * toRadiansRate));
                    vertices[veNum++] = (float)(r * UNIT_SIZE * Math.Sin((vAngle + angleSpan) * toRadiansRate) * Math.Sin(hAngle * toRadiansRate));
                    vertices[veNum++] = (float)(r * UNIT_SIZE * Math.Cos((vAngle + angleSpan) * toRadiansRate));
                    //Index 0 point
                    vertices[veNum++] = (float)(r * UNIT_SIZE * Math.Sin(vAngle * toRadiansRate) * Math.Cos(hAngle * toRadiansRate));
                    vertices[veNum++] = (float)(r * UNIT_SIZE * Math.Sin(vAngle * toRadiansRate) * Math.Sin(hAngle * toRadiansRate));
                    vertices[veNum++] = (float)(r * UNIT_SIZE * Math.Cos(vAngle * toRadiansRate));

                    //Index 1 point
                    vertices[veNum] = vertices[veNum++ - 9];
                    vertices[veNum] = vertices[veNum++ - 9];
                    vertices[veNum] = vertices[veNum++ - 9];
                    //Index 2 point
                    vertices[veNum++] = (float)(r * UNIT_SIZE * Math.Sin((vAngle + angleSpan) * toRadiansRate) * Math.Cos((hAngle + angleSpan) * toRadiansRate));
                    vertices[veNum++] = (float)(r * UNIT_SIZE * Math.Sin((vAngle + angleSpan) * toRadiansRate) * Math.Sin((hAngle + angleSpan) * toRadiansRate));
                    vertices[veNum++] = (float)(r * UNIT_SIZE * Math.Cos((vAngle + angleSpan) * toRadiansRate));
                    //Index 3 point
                    vertices[veNum] = vertices[veNum++ - 12];
                    vertices[veNum] = vertices[veNum++ - 12];
                    vertices[veNum] = vertices[veNum++ - 12];
                    //Calculate the corresponding texture coordinates of each point
                    float s0 = hAngle / 360.0f;
                    float s1 = (hAngle + angleSpan) / 360.0f;
                    float t0 = 1 - vAngle / 180.0f;
                    float t1 = 1 - (vAngle + angleSpan) / 180.0f;
                    textures[teNum++] = s1;
                    textures[teNum++] = t0;
                    textures[teNum++] = s0;
                    textures[teNum++] = t1;
                    textures[teNum++] = s0;
                    textures[teNum++] = t0;

                    textures[teNum++] = s1;
                    textures[teNum++] = t0;
                    textures[teNum++] = s1;
                    textures[teNum++] = t1;
                    textures[teNum++] = s0;
                    textures[teNum++] = t1;
                }
            }
        }
        /// <summary>
        /// Init Texture which will load texture image
        /// </summary>
        private void InitTexture()
        {
            aTextureCoordinates = GL.GetAttribLocation(program, "a_TextureCoordinates");
            uTextureUnitLocation = GL.GetAttribLocation(program, "u_TextureUnit");
            texture = LoadTexture(DirectoryInfo.Resource + "1.bmp");
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.Uniform1(uTextureUnitLocation, 0);
        }
        /// <summary>
        /// Create Program and load shader
        /// </summary>
        private void GetProgram()
        {
            string vertexShaderSrc = "uniform mat4 uMVPMatrix;   \n" +
                              "attribute vec4 aPosition;    \n" +
                              "attribute vec2 a_TextureCoordinates;    \n" +
                              "varying vec2 v_TextureCoordinates;" +
                              "void main()                  \n" +
                              "{                            \n" +
                              "   gl_Position = uMVPMatrix * aPosition;  \n" +
                              "   v_TextureCoordinates = a_TextureCoordinates;\n" +
                              "}                            \n";

            string fragmentShaderSrc = "precision mediump float;             \n" +
                               "uniform sampler2D u_TextureUnit;             \n" +
                               "varying vec2 v_TextureCoordinates;           \n" +
                               "void main()                                  \n" +
                               "{                                            \n" +
                               "  gl_FragColor = texture2D(u_TextureUnit, v_TextureCoordinates);\n" +
                               "}                                            \n";

            program = ShaderHelper.BuildProgram(vertexShaderSrc, fragmentShaderSrc);
            GL.BindAttribLocation(program, 0, "aPosition");
            GL.LinkProgram(program);
            GL.UseProgram(program);
        }
        /// <summary>
        /// Load texture image 
        /// </summary>
        /// <param name="path">image path</param>
        /// <returns>texture Id</returns>
        private int LoadTexture(string path)
        {
            int[] textureId = { 0 };
            MBitmap bitm = MImageUtil.LoadImage(path);
            byte[] pixels = bitm.byteBuffer;
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 4);
            GL.GenTextures(1, textureId);
            GL.BindTexture(TextureTarget.Texture2D, textureId[0]);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)All.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)All.Linear);

            GL.TexImage2D(TextureTarget2d.Texture2D, 0, TextureComponentCount.Rgb, (int)bitm.inforHeader.biWidth, (int)bitm.inforHeader.biHeight, 0, PixelFormat.Rgb, PixelType.UnsignedByte, pixels);
            GL.GenerateMipmap(TextureTarget.Texture2D);
            return textureId[0];
        }
        /// <summary>
        /// To draw
        /// </summary>
        public void Draw()
        {
            umatrix = MatrixState.Get360Matrix(Window.Height, Window.Width);

            GL.UniformMatrix4(uMatrixLocation, false, ref umatrix);
            GL.DrawArrays(PrimitiveType.Triangles, 0, vCount);
            GL.Finish();
            Window.SwapBuffers();
        }
        /// <summary>
        /// This well be called when key pressed down
        /// </summary>
        /// <param name="sender">Key instance</param>
        /// <param name="e">Key's args</param>
        public void OnKeyEvent(object sender, KeyboardKeyEventArgs e)
        {
            //right key pressed, the scene will turn right
            if (e.Key == Key.Right)
            {
                MatrixState.Rotate(-60f, 0f, 360f);
            }
            //Left key pressed, the scene will turn Left
            else if (e.Key == Key.Left)
            {
                MatrixState.Rotate(60f, 0f, 360f);
            }
            //return key pressed, the App will exit
            else if (e.Key == Key.Escape)
            {
                Exit();
            }

            Draw();
        }
        /// <summary>
        /// This gets called when the TizenGameApplication has been created.
        /// </summary>
        protected override void OnCreate()
        {
            Window.RenderFrame += OnRenderFrame;
            Window.KeyDown += OnKeyEvent;
            //Mouse movement event handling
            Window.MouseMove += (sender, e) =>
            {
                if (e.Mouse[MouseButton.Left])
                {
                    float x = (float)(e.XDelta);
                    float y = (float)(e.YDelta);
                    MatrixState.Rotate(x, 0, 10f);
                    Draw();
                }
            };
            InitVertexData();
            GetProgram();
            aPositionLocation = GL.GetAttribLocation(program, "aPosition");
            uMatrixLocation = GL.GetUniformLocation(program, "uMVPMatrix");
            InitTexture();
            GL.VertexAttribPointer(aPositionLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), vertices);
            GL.VertexAttribPointer(aTextureCoordinates, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), textures);

            GL.EnableVertexAttribArray(aPositionLocation);
            GL.EnableVertexAttribArray(aTextureCoordinates);
            GL.ClearColor(Color4.Gray);
            sPlayer.SetSource(DirectoryInfo.Resource + "1.m4a");
            sPlayer.ToPlay(true);

        }
        /// <summary>
        /// This well be called when the application is resumed
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            Window.MakeCurrent();
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
        /// <param name="args">The main arguments </param>
        static void Main(string[] args)
        {
            var app = new Panorama360() { GLMajor = 2, GLMinor = 0 };
            app.Run(args);
        }
    }

}
