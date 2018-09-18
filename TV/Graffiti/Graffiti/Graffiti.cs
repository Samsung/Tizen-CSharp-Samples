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
using System.Collections.Generic;

namespace Graffiti
{
    class Graffiti : TizenGameApplication
    {
        List<Vector3> lines = new List<Vector3>();
        Vector3[] linesArray;
        //The start coordinate of graffiti
        Vector2 original = new Vector2(0, 0);
        int mProgramHandle, mColorHandle, mPositionHandle, mMVPMatrixHandle;
        int posN = 0;
        Matrix4 mProjectionMatrix, mViewMatrix, mModelViewProjectionMatrix;
        SPlayer sPlayer = new SPlayer();
        // Set color with red, green, blue and alpha (opacity) values
        float[] color = new float[] { 0.63671875f, 0.76953125f, 0.22265625f, 1.0f };
        /// <summary>
        /// This will be called when app created
        /// </summary>
        protected override void OnCreate()
        {
            Window.RenderFrame += OnRenderFrame;
            //when key down,add a new point and draw a new line in this Render frame
            Window.KeyDown += (sender, e) =>
            {
                if (original.X == 0 && original.Y == 0)
                {
                    lines.Add(new Vector3(0f));
                }

                if (e.Key == Key.Right)
                {
                    original.X += 0.1f;
                }

                else if (e.Key == Key.Left)
                {
                    original.X -= 0.1f;
                }

                else if (e.Key == Key.Up)
                {
                    original.Y += 0.1f;
                }

                else if (e.Key == Key.Down)
                {
                    original.Y -= 0.1f;
                }

                else if (e.Key == Key.Escape)
                {
                    Exit();
                }

                lines.Add(new Vector3(original.X, original.Y, 0f));
            };
            Window.MouseMove += (sender, e) =>
            {
                if (e.Mouse[MouseButton.Left])
                {
                    // Scale mouse coordinates from
                    // (0, 0):(Width, Height) to
                    // (-1, -1):(+1, +1)
                    // Note, we must flip the y-coordinate
                    // since mouse is reported with (0, 0)
                    // at top-left and our projection uses
                    // (-1, -1) at bottom left.
                    float x = (e.X - Window.Width) / (float)Window.Width;
                    float y = (Window.Height - e.Y) / (float)Window.Height;
                    lines.Add(new Vector3(2 * x + 1, 2 * y - 1, 0f));
                }
            };

            // Vertex and fragment shaders
            string vertexShaderSrc = "uniform mat4 uMVPMatrix;   \n" +
                              "attribute vec4 vPosition;    \n" +
                              "void main()                  \n" +
                              "{                            \n" +
                              "   gl_Position = vPosition;  \n" +
                              "   gl_PointSize = 30.0;\n" +
                              "}                            \n";
            string fragmentShaderSrc = "precision mediump float;             \n" +
                               "uniform vec4 vColor;                         \n" +
                               "void main()                                  \n" +
                               "{                                            \n" +
                               "  gl_FragColor = vColor;                     \n" +
                               "}                                            \n";
            int vertexShader = LoadShader(ShaderType.VertexShader, vertexShaderSrc);
            int fragmentShader = LoadShader(ShaderType.FragmentShader, fragmentShaderSrc);
            mProgramHandle = GL.CreateProgram();
            GL.AttachShader(mProgramHandle, vertexShader);
            GL.AttachShader(mProgramHandle, fragmentShader);
            GL.BindAttribLocation(mProgramHandle, 0, "vPosition");
            GL.LinkProgram(mProgramHandle);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
            sPlayer.SetSource(DirectoryInfo.Resource + "2.m4a");
            sPlayer.ToPlay(true);
        }
        /// <summary>
        /// Load shade according to shade type
        /// </summary>
        /// <param name="type">shade type</param>
        /// <param name="source">Shade code</param>
        /// <returns>Shader ID </returns>
        int LoadShader(ShaderType type, string source)
        {
            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, 1, new string[] { source }, (int[])null);
            GL.CompileShader(shader);
            return shader;
        }
        /// <summary>
        ///  Rendering code here.
        /// </summary>
        void Render()
        {
            GL.ClearColor(Color4.MidnightBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            // Set the camera position (View matrix)
            mViewMatrix = Matrix4.LookAt(0, 0, -3, 0f, 0f, 0f, 0f, 1.0f, 0.0f);
            // Calculate the projection and view transformation
            mModelViewProjectionMatrix = Matrix4.Mult(mProjectionMatrix, mViewMatrix);
            GL.UseProgram(mProgramHandle);
            // get handle to vertex shader's vPosition member
            mPositionHandle = GL.GetAttribLocation(mProgramHandle, "vPosition");
            // Enable a handle to the triangle vertices
            GL.EnableVertexAttribArray(mPositionHandle);
            // pin the data, so that GC doesn't move them, while used
            // by native code
            linesArray = new Vector3[lines.Count + 1];
            posN = 0;
            foreach (var p in lines)
            {
                linesArray[posN].X = p.X;
                linesArray[posN].Y = p.Y;
                linesArray[posN++].Z = p.Z;
            }

            GL.VertexAttribPointer<Vector3>(0, 3, VertexAttribPointerType.Float, true, 0, linesArray);
            // get handle to fragment shader's vColor member
            mColorHandle = GL.GetUniformLocation(mProgramHandle, "vColor");
            // Set color for drawing the triangle
            GL.Uniform4(mColorHandle, 1, color);
            // get handle to shape's transformation matrix
            mMVPMatrixHandle = GL.GetUniformLocation(mProgramHandle, "uMVPMatrix");
            // Apply the projection and view transformation
            GL.UniformMatrix4(mMVPMatrixHandle, false, ref mModelViewProjectionMatrix);
            GL.DrawArrays(PrimitiveType.LineStrip, 0, posN);
            GL.Finish();
            // Disable vertex array
            GL.DisableVertexAttribArray(mPositionHandle);
            Window.SwapBuffers();
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
            float ratio = (float)Window.Width / Window.Height;
            // this projection matrix is applied to object coordinates
            // in the Render() method
            mProjectionMatrix = OpenTK.Matrix4.CreatePerspectiveOffCenter(-ratio, ratio, -1, 1, 3, 7);
            Render();
        }
        /// <summary>
        /// Called when it is time to render the next frame. 
        /// </summary>
        /// <param name="oe">object</param>
        /// <param name="e">Frame Event</param>
        protected void OnRenderFrame(object oe, FrameEventArgs e)
        {
            Render();
        }
        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="args">The main arguments</param>
        static void Main(string[] args)
        {
            var app = new Graffiti() { GLMajor = 2, GLMinor = 0 };
            app.Run(args);
        }
    }

}
