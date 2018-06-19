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

using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using System;

namespace FirstScreen
{
    // TODO: Add this feature to DALi
    class ClippingImage
    {
        // This is currently a static as it is a single method.
        // This could be incorporated into ImageView or View/Control as a property.
        /// <summary>
        /// Creates and initializes a new instance of ImageView class
        /// </summary>
        /// <param name="filename">file name</param>
        /// <returns>clipped view</returns>
        public static ImageView Create(String filename)
        {
            ImageView clipView = new ImageView();

            String fragmentShaderString = "#extension GL_OES_standard_derivatives : enable\n" +
                "varying mediump vec2 vTexCoord;" +
                "uniform sampler2D sTexture;" +
                "void main()" +
                "{" +
                "  if (texture2D(sTexture, vTexCoord).a > 0.4)" +
                "  {" +
                "    gl_FragColor = vec4(0.0, 0.0, 0.0, 0.0);" +
                "  }" +
                "  else" +
                "  {" +
                "    discard;" +
                "  }" +
                "}";

            PropertyMap shaderMap = new PropertyMap();
            shaderMap.Add(Tizen.NUI.Visual.ShaderProperty.FragmentShader, new PropertyValue(fragmentShaderString));
            shaderMap.Add(Tizen.NUI.Visual.ShaderProperty.ShaderHints, new PropertyValue((int)Shader.Hint.Value.OUTPUT_IS_TRANSPARENT));

            PropertyMap imageMap = new PropertyMap();
            imageMap.Add(Tizen.NUI.ImageVisualProperty.URL, new PropertyValue(filename));
            imageMap.Add(Tizen.NUI.Visual.Property.Shader, new PropertyValue(shaderMap));

            clipView.ImageMap = imageMap;
            clipView.ClippingMode = ClippingModeType.ClipChildren;
            return clipView;
        }
    }

}
