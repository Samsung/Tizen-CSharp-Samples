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

namespace ParticleDynamic
{
    /// <summary>
    /// Dynamic particle system Sample
    /// </summary>
    class ParticleDynamic : TizenGameApplication
    {
        int mProgramHandle, mColorHandle, mPositionHandle, mMVPMatrixHandle, VisibleParticleCount;
        Vector3 originPoint = new Vector3(0, 0, 0);
        Matrix4 mModelViewProjectionMatrix;
        SPlayer sPlayer = new SPlayer();
        static int MaxParticleCount = 2000;
        VertexC4ubV3f[] VBO = new VertexC4ubV3f[MaxParticleCount];
        ParticleAttribut[] ParticleAttributes = new ParticleAttribut[MaxParticleCount];

        // this struct is used for drawing
        struct VertexC4ubV3f
        {
            //The particle color
            public float R, G, B, A;
            //The particle coordinate
            public Vector3 Position;
            //The particle size
            public static int SizeInBytes = 28;
        }

        // this struct is used for marking the state of each particle
        struct ParticleAttribut
        {
            //direction of movement
            public Vector3 Direction;
            //The life cycle of each particle
            public uint Age;
        }

        uint VBOHandle;

        /// <summary>
        /// This will be called when mouse pressed down
        /// </summary>
        /// <param name="sender">key instance</param>
        /// <param name="e"> key's args</param>
        protected void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            originPoint.X = (float)e.Position.X / Window.Width * 2f - 1f;
            originPoint.Y = 1f - (float)e.Position.Y / Window.Height * 2f;
            originPoint.Z = 0;
            for (int i = 0; i < MaxParticleCount - VisibleParticleCount; i++)
            {
                VBO[i].Position = originPoint;
            }
        }

        /// <summary>
        /// This will be called when key pressed down
        /// </summary>
        /// <param name="sender">key instance</param>
        /// <param name="e"> key's args</param>
        protected void OnKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                originPoint.X += 0.1f;
            }

            else if (e.Key == Key.Left)
            {
                originPoint.X -= 0.1f;
            }

            else if (e.Key == Key.Up)
            {
                originPoint.Y += 0.1f;
            }

            else if (e.Key == Key.Down)
            {
                originPoint.Y -= 0.1f;
            }

            else if (e.Key == Key.Escape)
            {
                Exit();

            }

            originPoint.Z = 0;
            for (int i = 0; i < MaxParticleCount - VisibleParticleCount; i++)
            {
                VBO[i].Position = originPoint;
            }

            Draw();
        }

        /// <summary>
        /// This gets called when the TizenGameApplication has been created
        /// </summary>
        protected override void OnCreate()
        {
            Window.RenderFrame += OnRenderFrame;
            Window.KeyDown += OnKeyDown;
            Window.MouseDown += OnMouseDown;
            string vertexShaderSrc = ShaderHelper.Read(DirectoryInfo.Resource, "vsParti.vesh");
            string fragmentShaderSrc = ShaderHelper.Read(DirectoryInfo.Resource, "fsParti.frsh");
            mProgramHandle = ShaderHelper.BuildProgram(vertexShaderSrc, fragmentShaderSrc);

            GL.BindAttribLocation(mProgramHandle, 0, "vPosition");
            GL.BindAttribLocation(mProgramHandle, 1, "vColor");
            GL.LinkProgram(mProgramHandle);
            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);

            // Setup parameters for Points
            GL.Enable(EnableCap.PointSmooth);
            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);
            GL.GenBuffers(1, out VBOHandle);

            // Bind VBO Buffer.
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOHandle);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, VertexC4ubV3f.SizeInBytes, (IntPtr)0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, VertexC4ubV3f.SizeInBytes, (IntPtr)(16 * sizeof(byte)));

            Random rnd = new Random();
            Vector3 temp = Vector3.Zero;

            // generate some random stuff for the particle system
            for (uint i = 0; i < MaxParticleCount; i++)
            {
                VBO[i].R = ((float)rnd.Next(0, 256)) / 256;
                VBO[i].G = ((float)rnd.Next(0, 256)) / 256;
                VBO[i].B = ((float)rnd.Next(0, 256)) / 256;
                // isn't actually used
                VBO[i].A = ((float)rnd.Next(0, 256)) / 256;
                // all particles are born at the origin
                VBO[i].Position = originPoint;

                // generate direction vector in the range [-0.25f...+0.25f] 
                // that's slow enough so you can see particles 'disappear' when they are respawned
                temp.X = (float)((rnd.NextDouble() - 0.5) * 0.5f);
                temp.Y = (float)((rnd.NextDouble() - 0.5) * 0.5f);
                temp.Z = (float)((rnd.NextDouble() - 0.5) * 0.5f);
                ParticleAttributes[i].Direction = temp;
                ParticleAttributes[i].Age = 0;
            }

            VisibleParticleCount = 0;
            sPlayer.SetSource(DirectoryInfo.Resource + "5.m4a");
            sPlayer.ToPlay(true);
        }
        /// <summary>
        /// this will be called when the application is terminated
        /// </summary>
        protected override void OnTerminate()
        {
            GL.DeleteBuffers(1, ref VBOHandle);
        }

        /// <summary>
        /// Called when your window is resumed. Set your viewport here. It is also
        /// a good place to set up your projection matrix (which probably changes
        /// along when the aspect ratio of your window).
        /// </summary>
        protected override void OnResume()
        {
            GL.Viewport(0, 0, Window.Width, Window.Height);
            Matrix4 p = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Window.Width / (float)Window.Height, 0.1f, 50.0f);
            Matrix4 mv = Matrix4.LookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
            mModelViewProjectionMatrix = Matrix4.Mult(p, mv);
        }

        /// <summary>
        /// Called when it is time to setup the next frame. Add you game logic here.
        /// </summary>
        /// <param name="e">Contains timing information for frame rate independent logic.</param>
        protected void OnUpdateFrame(FrameEventArgs e)
        {
            // will update particles here. When using a Physics SDK, it's update rate is much higher than
            // the frame rate and it would be a waste of cycles copying to the VBO more often than drawing it.
            if (VisibleParticleCount < MaxParticleCount)
            {
                VisibleParticleCount++;
            }

            Vector3 temp;

            for (int i = MaxParticleCount - VisibleParticleCount; i < MaxParticleCount; i++)
            {
                if (ParticleAttributes[i].Age >= 500)
                {
                    // reset particle
                    ParticleAttributes[i].Age = 0;
                    VBO[i].Position = originPoint;
                }
                else
                {
                    ParticleAttributes[i].Age += (uint)Math.Max(ParticleAttributes[i].Direction.LengthFast * 10, 1);
                    Vector3.Multiply(ref ParticleAttributes[i].Direction, (float)e.Time, out temp);
                    Vector3.Add(ref VBO[i].Position, ref temp, out VBO[i].Position);
                }
            }
        }
        /// <summary>
        /// Rendering code here.
        /// </summary>
        protected void Draw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.UseProgram(mProgramHandle);
            // get handle to vertex shader's vPosition member
            mPositionHandle = GL.GetAttribLocation(mProgramHandle, "vPosition");
            mColorHandle = GL.GetAttribLocation(mProgramHandle, "vColor");
            GL.EnableVertexAttribArray(mColorHandle);
            // Enable a handle to the triangle vertices
            GL.EnableVertexAttribArray(mPositionHandle);
            // Tell OpenGL to discard old VBO when done drawing it and reserve memory _now_ for a new buffer.
            // without this, GL would wait until draw operations on old VBO are complete before writing to it
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(VertexC4ubV3f.SizeInBytes * MaxParticleCount), IntPtr.Zero, BufferUsageHint.StreamDraw);
            // Fill newly allocated buffer
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(VertexC4ubV3f.SizeInBytes * MaxParticleCount), VBO, BufferUsageHint.StreamDraw);
            mMVPMatrixHandle = GL.GetUniformLocation(mProgramHandle, "uMVPMatrix");

            // Apply the projection and view transformation
            GL.UniformMatrix4(mMVPMatrixHandle, false, ref mModelViewProjectionMatrix);
            // Only draw particles that are alive
            GL.DrawArrays(PrimitiveType.Points, MaxParticleCount - VisibleParticleCount, VisibleParticleCount);
            GL.Finish();
            Window.SwapBuffers();
        }

        /// <summary>
        /// Called when it is time to render the next frame. 
        /// </summary>
        /// <param name="oe">object</param>
        /// <param name="e">Frame Event</param>
        protected void OnRenderFrame(object oe, FrameEventArgs e)
        {
            OnUpdateFrame(e);
            Draw();
        }
        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="args">The main arguments</param>
        static void Main(string[] args)
        {
            var app = new ParticleDynamic() { GLMajor = 2, GLMinor = 0 };
            app.Run(args);
        }
    }

}
