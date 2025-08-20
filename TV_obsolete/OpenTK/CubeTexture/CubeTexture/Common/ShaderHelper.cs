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
using OpenTK.Graphics.ES20;

namespace CubeTexture
{
    public static class ShaderHelper
    {
        /// <summary>
        /// Create program and load shade base shade type
        /// </summary>
        /// <param name="ve">Vertex shade source</param>
        /// <param name="fr">Fragment shade source</param>
        /// <returns>Program Handle</returns>
        public static int BuildProgram(string ve, string fr)
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
        static int LoadShader(ShaderType type, string source)
        {
            int shader = GL.CreateShader(type);

            GL.ShaderSource(shader, 1, new string[] { source }, (int[])null);
            GL.CompileShader(shader);
            return shader;
        }
    }
}
