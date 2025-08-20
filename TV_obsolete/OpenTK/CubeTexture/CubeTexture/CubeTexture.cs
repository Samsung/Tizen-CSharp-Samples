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

namespace CubeTexture
{
    class CubeByMultProgramView : TizenGameApplication
    {
        // Handle to a program object
        int programObject, programObject2;
        // Attribute locations
        int positionLoc, positionLoc2, uniformLoc;
        // Uniform locations
        int mvpLoc, mvpLoc2, textureHandle;
        // Vertex data
        float[] vertices2;
        float[] vertices;
        // Attribute locations
        ushort[] indices;
        // Rotation angle
        float angleX2 = 45.0f, angleY2 = 45.0f;
        // Rotation angle
        float angleX = 45.0f, angleY = 45.0f;
        // MVP matrix
        Matrix4 mvpMatrix, mvpMatrix2;
        Matrix4 perspective, perspective2;
        Matrix4 modelview, modelview2;

        SPlayer sPlayer = new SPlayer();
        /// <summary>
        /// This well be called when key pressed down
        /// </summary>
        /// <param name="sender">Key instance</param>
        /// <param name="e">Key's args</param>
        public void OnKeyEvent(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                Rotate(-80f, 0f, ref angleX, ref angleY, ref perspective, ref modelview, ref mvpMatrix, 1);
            }

            else if (e.Key == Key.Left)
            {
                Rotate(80f, 0f, ref angleX, ref angleY, ref perspective, ref modelview, ref mvpMatrix, 1);

            }

            else if (e.Key == Key.Escape)
            {
                Exit();
            }

            Draw();
        }

        void Rotate(float a, float b, ref float anglex, ref float angley, ref Matrix4 perspective, ref Matrix4 model, ref Matrix4 mvmatrix, int flag)
        {
            anglex += (a * 0.5f);
            if (anglex >= 360.0f)
            {
                anglex -= 360.0f;
            }

            GL.Viewport(0, 0, Window.Width, Window.Height);
            float ratio = (float)Window.Width / Window.Height;
            MatrixState.EsMatrixLoadIdentity(ref perspective);
            MatrixState.EsPerspective(ref perspective, 60.0f, ratio, 1.0f, 20.0f);
            MatrixState.EsMatrixLoadIdentity(ref model);
            if (flag == 1)
            {
                MatrixState.EsTranslate(ref modelview, 0.0f, -0.8f, -3.0f);
            }
            else
            {
                MatrixState.EsTranslate(ref model, 0.0f, 1f, -5.0f);
            }

            MatrixState.EsRotate(ref model, anglex, 0.0f, 1.0f, 0.0f);
            if (b != 0f)
            {
                angley += (b * 0.5f);
                if (angley >= 360.0f)
                {
                    angley -= 360.0f;
                }

                MatrixState.EsRotate(ref model, angley, 1.0f, 0.0f, 0.0f);
            }

            mvmatrix = Matrix4.Mult(model, perspective);
        }
        /// <summary>
        /// This gets called when the TizenGameApplication has been created.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Window.RenderFrame += OnRenderFrame;
            Window.MouseMove += (sender, e) =>
            {
                if (e.Mouse[MouseButton.Left])
                {
                    float x = (float)(e.XDelta);
                    float y = (float)(e.YDelta);

                    Rotate(-x, -y, ref angleX, ref angleY, ref perspective, ref modelview, ref mvpMatrix, 1);
                    Draw();
                }
            };
            Window.KeyDown += OnKeyEvent;
            GL.ClearColor(Color4.Gray);
            GL.Enable(EnableCap.DepthTest);
            string vShaderStr = "uniform mat4 u_mvpMatrix;              \n" +
                          "attribute vec2 aTexture;                      \n" +
                          "attribute vec4 a_position;                  \n" +
                          "varying vec2 vtexture;\n" +
                          "void main()                                 \n" +
                          "{                                           \n" +
                          "   vtexture = aTexture;  \n" +
                          "   gl_Position = u_mvpMatrix * a_position;  \n" +
                          "}                                           \n";
            string fShaderStr = "precision mediump float;  \n" +
                                "uniform sampler2D uTexMap;\n" +
                                "varying vec2 vtexture;\n" +
                               "void main()    \n" +
                               "{             \n" +
                               "  gl_FragColor = texture2D( uTexMap, vtexture); \n" +
                               "}\n";

            programObject = ShaderHelper.BuildProgram(vShaderStr, fShaderStr);
            GL.BindAttribLocation(programObject, 0, "a_position");
            GL.BindAttribLocation(programObject, 1, "aTexture");
            GL.LinkProgram(programObject);
            int textID = TextureHelper.CreateTexture2D(DirectoryInfo.Resource + "1.bmp");
            GL.ActiveTexture(TextureUnit.Texture0);
            // Bind the texture to this unit.
            GL.BindTexture(TextureTarget.Texture2D, textID);
            GL.Uniform1(uniformLoc, 0);
            vertices = new float[]
            {
                -0.5f, 0.5f, 0.5f, 0.0f,1.0f,//0
                0.5f, 0.5f,  0.5f, 1.0f,1.0f,//1
                -0.5f, 0.5f,  -0.5f, 0.0f,0.0f,//2
                0.5f, 0.5f, -0.5f, 1.0f,0.0f,//3
                -0.5f,  -0.5f, -0.5f, 0.0f,1.0f,//4
                0.5f,  -0.5f,  -0.5f, 1.0f,1.0f,//5
                -0.5f, -0.5f, 0.5f, 0.0f,0.0f,//6
                0.5f,  -0.5f,  0.5f, 1.0f, 0.0f,//7

                -0.5f, 0.5f, 0.5f, 0.0f,1.0f,//0
                0.5f, 0.5f,  0.5f, 1.0f,1.0f,//1
                0.5f, 0.5f,  0.5f, 1.0f,1.0f,//1
                0.5f, 0.5f, -0.5f, 1.0f,0.0f,//3

                0.5f, 0.5f, -0.5f, 1.0f,1.0f,//3
                0.5f, 0.5f,  0.5f, 0.0f,1.0f,//1
                0.5f,  -0.5f,  -0.5f, 1.0f,0.0f,//5
                0.5f,  -0.5f,  0.5f, 0.0f, 0.0f,//7
                
                0.5f,  -0.5f,  0.5f, 1.0f, 0.0f,//7
                -0.5f, -0.5f, 0.5f, 0.0f,0.0f,//6

                -0.5f, -0.5f, 0.5f, 0.0f,0.0f,//6
                -0.5f,  -0.5f, -0.5f, 1.0f,0.0f,//4
                -0.5f, 0.5f, 0.5f, 0.0f,1.0f,//0
                -0.5f, 0.5f,  -0.5f, 1.0f,1.0f,//2
            };

            indices = new ushort[]
            {
                0, 2, 1,3,//up
				6,5,//right
				7,4,//below
				0,2,//left
				4,3,5,2,//back
				7,6,0,1 //front
            };
            Load2();
            sPlayer.SetSource(DirectoryInfo.Resource + "3.m4a");
            sPlayer.ToPlay(true);
        }


        void Draw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.UseProgram(programObject);
            positionLoc = GL.GetAttribLocation(programObject, "a_position");
            textureHandle = GL.GetAttribLocation(programObject, "aTexture");
            uniformLoc = GL.GetAttribLocation(programObject, "uTexMap");

            GL.EnableVertexAttribArray(positionLoc);
            GL.EnableVertexAttribArray(textureHandle);
            unsafe
            {
                // Prepare the triangle coordinate data
                GL.VertexAttribPointer(positionLoc, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), vertices);
                mvpLoc = GL.GetUniformLocation(programObject, "u_mvpMatrix");
                GL.UniformMatrix4(mvpLoc, false, ref mvpMatrix);

                fixed (float* atexture = &vertices[3])
                {
                    // Prepare the triangle coordinate data
                    GL.VertexAttribPointer(textureHandle, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), new IntPtr(atexture));
                }

                GL.DrawArrays(PrimitiveType.TriangleStrip, 0, vertices.Length / 5);
            }
            // Disable vertex array
            GL.DisableVertexAttribArray(positionLoc);
            GL.DisableVertexAttribArray(textureHandle);
            Draw2();
            Window.SwapBuffers();
        }

        void Draw2()
        {
            GL.UseProgram(programObject2);
            positionLoc2 = GL.GetAttribLocation(programObject2, "a_position");
            GL.EnableVertexAttribArray(positionLoc2);
            // Prepare the triangle coordinate data
            GL.VertexAttribPointer(positionLoc2, 4, VertexAttribPointerType.Float, false, 4 * sizeof(float), vertices2);
            mvpLoc2 = GL.GetUniformLocation(programObject2, "u_mvpMatrix");

            // Apply the projection and view transformation
            GL.UniformMatrix4(mvpLoc2, false, ref mvpMatrix2);
            GL.DrawElements(PrimitiveType.TriangleStrip, indices.Length, DrawElementsType.UnsignedShort, indices);
        }

        protected void Load2()
        {
            string vShaderStr = "uniform mat4 u_mvpMatrix;              \n" +
                          "attribute vec4 a_position;                  \n" +
                          "varying vec4 vColor;                \n" +
                          "void main()                                 \n" +
                          "{                                           \n" +
                          "   vColor = a_position;                \n" +
                          "   gl_Position = u_mvpMatrix * a_position;  \n" +
                          "}                                           \n";
            string fShaderStr = "precision mediump float;                            \n" +
                               "varying vec4 vColor;                \n" +
                               "void main()                                         \n" +
                               "{                                                   \n" +
                               "  gl_FragColor = vColor;        \n" +
                               "}                                                   \n";
            programObject2 = ShaderHelper.BuildProgram(vShaderStr, fShaderStr);
            GL.BindAttribLocation(programObject2, 0, "a_position");
            GL.LinkProgram(programObject2);
            vertices2 = new float[]
            {
                 -0.5f,0.5f, 0.5f, 1.0f,
                0.5f, 0.5f,  0.5f, 1.0f,
                -0.5f,0.5f,  -0.5f, 1.0f,
                0.5f, 0.5f, -0.5f, 1.0f,
                -0.5f,  -0.5f, -0.5f, 1.0f,
                0.5f,  -0.5f,  -0.5f, 1.0f,
                0.5f,  -0.5f,  0.5f, 1.0f,
                -0.5f, -0.5f, 0.5f, 1.0f
            };
        }
        /// <summary>
        /// This well be called when the application is resumed
        /// </summary>
        protected override void OnResume()
        {
            Window.MakeCurrent();
            // Adjust the viewport based on geometry changes,
            // such as screen rotation
            GL.Viewport(0, 0, Window.Width, Window.Height);

            Rotate(0.8f, 0.8f, ref angleX, ref angleY, ref perspective, ref modelview, ref mvpMatrix, 1);
            Draw();
        }
        /// <summary>
        /// Called when it is time to render the next frame. 
        /// </summary>
        /// <param name="oe">object</param>
        /// <param name="e">Frame Event</param>
        protected void OnRenderFrame(object oe, FrameEventArgs e)
        {
            Rotate(3f, 0f, ref angleX2, ref angleY2, ref perspective2, ref modelview2, ref mvpMatrix2, 2);
            Draw();
        }
        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="args">The main arguments </param>
        static void Main(string[] args)
        {
            var app = new CubeByMultProgramView() { GLMajor = 2, GLMinor = 0 };
            app.Run(args);
        }
    }

}
