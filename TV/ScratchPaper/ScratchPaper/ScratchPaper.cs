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

namespace ScratchPaper
{
    class ScratchPaper : TizenGameApplication
    {
        //A collection of newly added points used to draw a new line
        List<Vector2> newDrawline = new List<Vector2>();
        //A collection of all the added points(new and old)
        List<List<Vector2>> drawLines = new List<List<Vector2>>();
        Vector2[] linesArray;
        Vector2 original = new Vector2(0f, 0f);
        //Background's vertex coordinate set
        Vector2[] surfaceArray = { new Vector2(-1, -1), new Vector2(-1, 1), new Vector2(1, -1), new Vector2(1, 1) };
        int mProgramHandle, uniformLoc2, mPositionHandle, backHandle;
        int[] texturIndex = new int[2];
        SPlayer sPlayer = new SPlayer();
        /// <summary>
        /// This gets called when the TizenGameApplication has been created
        /// </summary>
        protected override void OnCreate()
        {
            newDrawline = new List<Vector2>();
            //when direction(up/down/left/right) key pressed down,
            //find a rectangle ,width is 0.1,height is 0.1, 
            //and add the rectangle's four angular point as vertices in the "newDrawline" list
            Window.KeyDown += (sender, e) =>
            {
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
                    original.Y += 0.2f;
                }

                else if (e.Key == Key.Down)
                {
                    original.Y -= 0.2f;
                }

                else if (e.Key == Key.Escape)
                {
                    Exit();
                }

                newDrawline.Add(new Vector2(original.X - 0.05f, original.Y + 0.1f));
                newDrawline.Add(new Vector2(original.X + 0.05f, original.Y + 0.1f));
                newDrawline.Add(new Vector2(original.X - 0.05f, original.Y - 0.1f));
                newDrawline.Add(new Vector2(original.X + 0.05f, original.Y - 0.1f));
            };
            Window.MouseDown += (sender, e) =>
            {
                if (e.X > 610 && e.Y < 100)
                {
                    newDrawline = new List<Vector2>();
                    drawLines = new List<List<Vector2>>();
                    return;
                }
                if (e.Mouse[MouseButton.Left])
                {
                    newDrawline = new List<Vector2>();
                    float x = (e.X - Window.Width) / (float)Window.Width;
                    float y = (Window.Height - e.Y) / (float)Window.Height;
                    float xpos = 2 * x + 1, ypos = 2 * y - 1;
                    newDrawline.Add(new Vector2(xpos - 0.05f, ypos + 0.05f));
                    newDrawline.Add(new Vector2(xpos + 0.05f, ypos + 0.05f));
                    newDrawline.Add(new Vector2(xpos - 0.05f, ypos - 0.05f));
                    newDrawline.Add(new Vector2(xpos + 0.05f, ypos - 0.05f));
                }
            };
            Window.MouseMove += (sender, e) =>
            {
                if (e.Mouse[MouseButton.Left])
                {
                    float x = (e.X - Window.Width) / (float)Window.Width;
                    float y = (Window.Height - e.Y) / (float)Window.Height;
                    float xpos = 2 * x + 1, ypos = 2 * y - 1;
                    newDrawline.Add(new Vector2(xpos - 0.05f, ypos + 0.05f));
                    newDrawline.Add(new Vector2(xpos + 0.05f, ypos + 0.05f));
                    newDrawline.Add(new Vector2(xpos - 0.05f, ypos - 0.05f));
                    newDrawline.Add(new Vector2(xpos + 0.05f, ypos - 0.05f));
                }
            };
            Window.MouseUp += (s, e) =>
            {
                drawLines.Add(newDrawline);
            };
            Window.RenderFrame += OnRenderFrame;
            //Load vertex file
            string vertexShaderSrc = ShaderHelper.Read(DirectoryInfo.Resource + "vsSand.vesh");
            //Load fragment file
            string fragmentShaderSrc = ShaderHelper.Read(DirectoryInfo.Resource + "fsSand.frsh");
            //Create program and load the shades.
            mProgramHandle = ShaderHelper.BuildProgram(vertexShaderSrc, fragmentShaderSrc);
            GL.BindAttribLocation(mProgramHandle, 0, "vPosition");
            GL.GenTextures(2, texturIndex);
            //load surface texture image
            TextureHelper.CreateTexture2DById(DirectoryInfo.Resource + "1.bmp", true, 0, ref texturIndex);
            //load bottomed texture image
            TextureHelper.CreateTexture2DById(DirectoryInfo.Resource + "2.bmp", true, 1, ref texturIndex);
            //Link program 
            GL.LinkProgram(mProgramHandle);
            //set the base color
            GL.ClearColor(Color4.DarkSlateGray);
            //Enable Depthtest
            GL.Enable(EnableCap.DepthTest);

            //play the background audio
            sPlayer.SetSource(DirectoryInfo.Resource + "5.m4a");
            sPlayer.ToPlay(true);
        }
        /// <summary>
        /// Draw surface Pic.
        /// </summary>
        void DrawSurface()
        {
            backHandle = GL.GetUniformLocation(mProgramHandle, "f_flag");
            GL.Uniform1(backHandle, 0f);
            //load vertex coordinate
            GL.VertexAttribPointer<Vector2>(0, 2, VertexAttribPointerType.Float, false, 0, surfaceArray);
            GL.EnableVertexAttribArray(0);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
        }
        /// <summary>
        /// Rendering code here.
        /// </summary>
        void Draw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.UseProgram(mProgramHandle);
            // get handle to vertex shader's vPosition member
            mPositionHandle = GL.GetAttribLocation(mProgramHandle, "vPosition");
            backHandle = GL.GetUniformLocation(mProgramHandle, "f_flag");
            GL.LineWidth(10.0f);

            //0 :draw surface
            //1 :draw lines with bottomed texture
            GL.Uniform1(backHandle, 1.0f);
            // Enable a handle to the triangle vertices
            GL.EnableVertexAttribArray(mPositionHandle);
            int posi;
            GL.ActiveTexture(TextureUnit.Texture1);
            uniformLoc2 = GL.GetUniformLocation(mProgramHandle, "uTexMap2");
            GL.Uniform1(uniformLoc2, 1);
            //To draw the latest added line
            if (newDrawline != null)
            {
                posi = newDrawline.Count;
                linesArray = new Vector2[posi--];
                foreach (Vector2 p in newDrawline)
                {
                    linesArray[posi--] = p;
                }
                //load vertex coordinate
                GL.VertexAttribPointer<Vector2>(0, 2, VertexAttribPointerType.Float, false, 0, linesArray);
                GL.DrawArrays(PrimitiveType.TriangleStrip, 0, newDrawline.Count);
            }
            // Draw the lines added before
            List<Vector2> drawline;
            for (int i = drawLines.Count - 1; i >= 0; i--)
            {
                drawline = drawLines[i];
                posi = drawline.Count;
                linesArray = new Vector2[posi--];
                foreach (Vector2 p in drawline)
                {
                    linesArray[posi--] = p;
                }
                //load vertex coordinate
                GL.VertexAttribPointer<Vector2>(0, 2, VertexAttribPointerType.Float, false, 0, linesArray);
                GL.DrawArrays(PrimitiveType.TriangleStrip, 0, drawline.Count);
            }
            //draw the surface pic
            DrawSurface();
            GL.Finish();
            //Swaps the front and back buffers of the current GraphicsContext, presenting the
            //rendered scene to the user.
            Window.SwapBuffers();
        }
        /// <summary>
        /// Called when it is time to render the next frame. 
        /// </summary>
        /// <param name="oe">object</param>
        /// <param name="e">Frame Event</param>
        protected void OnRenderFrame(object oe, FrameEventArgs e)
        {
            Draw();
        }
        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="args">The main arguments </param>
        static void Main(string[] args)
        {
            var app = new ScratchPaper() { GLMajor = 2, GLMinor = 0 };
            app.Run(args);
        }
    }
}
