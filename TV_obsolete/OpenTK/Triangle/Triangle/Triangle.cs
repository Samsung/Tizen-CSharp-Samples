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

namespace Triangle
{
    /// <summary>
    /// To Draw a Triangle
    /// </summary>
    class Triangle : TizenGameApplication
    {
        int viewportWidth, viewportHeight, mProgramHandle, mColorHandle, mPositionHandle, mMVPMatrixHandle;
        float[] vertices;
        SPlayer sPlayer = new SPlayer();
        Matrix4 mProjectionMatrix, mViewMatrix, mModelViewProjectionMatrix;
        // Set color with red, green, blue and alpha (opacity) values
        float[] color = new float[] { 0.63671875f, 0.76953125f, 0.22265625f, 1.0f };
        /// <summary>
        /// This gets called when the TizenGameApplication has been created
        /// </summary>
        protected override void OnCreate()
        {
            Window.KeyDown += (sender, e) =>
             {
                 if (e.Key == Key.Escape)
                 {
                     Exit();
                 }
             };
            Window.RenderFrame += RenderTriangle;
            viewportHeight = Window.Height;
            viewportWidth = Window.Width;
            // Set our triangle's Vertices
            vertices = new float[]
            {
                    0.0f, 0.5f, 0.0f,
                    -0.5f, -0.5f, 0.0f,
                    0.5f, -0.5f, 0.0f
            };
            // Vertex and fragment shaders
            string vertexShaderSrc = "uniform mat4 uMVPMatrix;   \n" +
                              "attribute vec4 vPosition;    \n" +
                              "void main()                  \n" +
                              "{                            \n" +
                              "   gl_Position = vPosition;  \n" +
                              "}                            \n";
            string fragmentShaderSrc = "precision mediump float;             \n" +
                               "uniform vec4 vColor;                         \n" +
                               "void main()                                  \n" +
                               "{                                            \n" +
                               "  gl_FragColor = vColor;  \n" +
                               "}                                            \n";
            mProgramHandle = BuildProgram(vertexShaderSrc, fragmentShaderSrc);
            GL.LinkProgram(mProgramHandle);
            sPlayer.SetSource(DirectoryInfo.Resource + "1.m4a");
            sPlayer.ToPlay(true);
        }

        /// <summary>
        /// Create program and load shade base shade type
        /// </summary>
        /// <param name="ve">Vertex shade source</param>
        /// <param name="fr">Fragment shade source</param>
        /// <returns>Program Handle</returns>
        int BuildProgram(string ve, string fr)
        {
            int vertexShader = LoadShader(ShaderType.VertexShader, ve);
            int fragmentShader = LoadShader(ShaderType.FragmentShader, fr);
            int mProgramHandle = GL.CreateProgram();
            GL.AttachShader(mProgramHandle, vertexShader);
            GL.AttachShader(mProgramHandle, fragmentShader);
            return mProgramHandle;
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
        /// Draw Function
        /// </summary>
        /// <param name="ob">Instance</param>
        /// <param name="e">Instance's Args</param>
        void RenderTriangle(object ob, FrameEventArgs e)
        {
            GL.ClearColor(Color4.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            // Set the camera position (View matrix)
            mViewMatrix = Matrix4.LookAt(0, 0, -3, 0f, 0f, 0f, 0f, 1.0f, 0.0f);
            // Calculate the projection and view transformation
            mModelViewProjectionMatrix = Matrix4.Mult(mProjectionMatrix, mViewMatrix);
            GL.UseProgram(mProgramHandle);
            // Get handle to vertex shader's vPosition member
            mPositionHandle = GL.GetAttribLocation(mProgramHandle, "vPosition");
            // Enable a handle to the triangle vertices
            GL.EnableVertexAttribArray(mPositionHandle);
            // Pin the data, so that GC doesn't move them, while used
            // by native code
            unsafe
            {
                fixed (float* pvertices = vertices)
                {
                    // Prepare the triangle coordinate data
                    GL.VertexAttribPointer(mPositionHandle, 3, VertexAttribPointerType.Float, false, 0, new IntPtr(pvertices));
                    // get handle to fragment shader's vColor member
                    mColorHandle = GL.GetUniformLocation(mProgramHandle, "vColor");
                    // Set color for drawing the triangle
                    GL.Uniform4(mColorHandle, 1, color);
                    // get handle to shape's transformation matrix
                    mMVPMatrixHandle = GL.GetUniformLocation(mProgramHandle, "uMVPMatrix");
                    // Apply the projection and view transformation
                    GL.UniformMatrix4(mMVPMatrixHandle, false, ref mModelViewProjectionMatrix);
                    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
                    // Disable vertex array
                    GL.DisableVertexAttribArray(mPositionHandle);
                    // Swaps the front and back buffers of the current GraphicsContext, presenting the
                    // rendered scene to the user.
                    Window.SwapBuffers();
                }
            }
        }

        /// <summary>
        /// This is called when the application is resumed.
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
            // in the RenderTriangle() method
            mProjectionMatrix = OpenTK.Matrix4.CreatePerspectiveOffCenter(-ratio, ratio, -1, 1, 3, 7);
        }

        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="args">The main arguments </param>
        static void Main(string[] args)
        {
            var app = new Triangle() { GLMajor = 2, GLMinor = 0 };
            app.Run(args);
        }
    }

}
