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
using System;

namespace ScratchPaper
{
    public static class TextureHelper
    {
        /// <summary>
        /// Load texture resource
        /// </summary>
        /// <param name="imagePath">texture image</param>
        /// <param name="isRepeat"> the texture is or not to repeat </param>
        /// <param name="textureId">texture Id</param>
        /// <param name="textureIndex">texture Index Array</param>
        public static void CreateTexture2DById(string imagePath, bool isRepeat, int textureId, ref int[] textureIndex)
        {
            byte[] pixels;
            MBitmap imageBitMap = MImageUtil.LoadImage(imagePath);
            pixels = imageBitMap.byteBuffer;

            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 4);
            GL.ActiveTexture(TextureUnit.Texture0 + textureId);
            GL.BindTexture(TextureTarget.Texture2D, textureIndex[textureId]);
            unsafe
            {
                fixed (byte* pix = pixels)
                    GL.TexImage2D(TextureTarget2d.Texture2D, 0, TextureComponentCount.Rgb, (int)imageBitMap.inforHeader.biWidth, (int)imageBitMap.inforHeader.biHeight, 0, PixelFormat.Rgb, PixelType.UnsignedByte, new IntPtr(pix));
            }
            if (isRepeat)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float)All.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float)All.ClampToEdge);
            }
            // Set the filtering mode
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)All.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)All.LinearMipmapLinear);
            GL.GenerateMipmap(TextureTarget.Texture2D);
        }
    }
}
