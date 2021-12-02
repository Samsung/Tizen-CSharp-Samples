//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.
using Xamarin.Forms;
using TDisplay = Tizen.System.Display;
using TInformation = Tizen.System.Information;

namespace SystemInfo.Components
{
    /// <summary>
    /// An easy to access fascade for Tizen.System.Information and Tizen.System.Display with information like screen resolution or dpi.  
    /// </summary>
    public class Display : BindableObject
    {
        /// <summary>
        /// User friendly brightness summary (e.g. 90/100)
        /// </summary>
        public string Brightness =>  TDisplay.Displays[0].Brightness + " / " + TDisplay.Displays[0].MaxBrightness;
        
        /// <summary>
        /// Gets screen height.
        /// </summary>
        public string Resolution
        {
            get
            {
                int width, height;                
                TInformation.TryGetValue<int>("http://tizen.org/feature/screen.width", out width);
                TInformation.TryGetValue<int>("http://tizen.org/feature/screen.height", out height);
                return width + "x" + height;
            }
        }
        /// <summary>
        /// Gets display DPI (Dots per inch).
        /// </summary>
        public int Dpi
        {
            get
            {
                int dpi;
                TInformation.TryGetValue<int>("http://tizen.org/feature/screen.dpi", out dpi);
                return dpi;
            }
        }
        /// <summary>
        /// Gets display Bpp (color depth, bits per pixel).
        /// </summary>
        public int Bpp
        {
            get
            {
                int bpp;
                TInformation.TryGetValue<int>("http://tizen.org/feature/screen.dpi", out bpp);
                return bpp;
            }
        }
    }
}
