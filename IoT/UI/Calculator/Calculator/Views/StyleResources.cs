/*
 * Copyright (c) 2025 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Calculator.Views
{
    internal static class StyleResources
    {
        public static readonly TextLabelStyle SumTextLabelStyle = new TextLabelStyle()
        {
            TextColor = new Color("#7F0000"), // originally "#7F000000"
            FontFamily = "Samsung sans",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.End,
            PointSize = 38,
            LineWrapMode = LineWrapMode.Character,
        };

        public static readonly TextLabelStyle InputTextLabelStyle = new TextLabelStyle()
        {
            TextColor = new Color("#000000"),
            FontFamily = "Samsung sans",
            HorizontalAlignment = HorizontalAlignment.End,
            LineWrapMode = LineWrapMode.Word,
        };

        public static readonly TextLabelStyle AlertTextLabelStyle = new TextLabelStyle()
        {
            TextColor = new Color("#7F0000"), // originally "#7F000000"
            FontFamily = "Samsung sans",
            BackgroundColor = new Color("#DDFFFFFF"),
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            PointSize = 25,
            LineWrapMode = LineWrapMode.Word,
        };

        public static readonly ImageViewStyle BackButtonStyle = new ImageViewStyle()
        {
            Size = new Size(88, 47),
            ResourceUrl = Application.Current.DirectoryInfo.Resource + "btn_delete.png",
        };
    }
}
