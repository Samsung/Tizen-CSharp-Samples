using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.Components;

namespace SpeechToText.views.MainPageViews
{
    class LoadingIndicator : Loading
    {
        /// <summary>
        /// Contains the loading images
        /// </summary>
        private readonly string[] imageArray;

        /// <summary>
        /// Contains the absolute path to the application resource directory
        /// </summary>
        private readonly string directory;

        public LoadingIndicator() {
            //Initial state for the loading indicator
            Hide();
            Stop();
            directory = Application.Current.DirectoryInfo.Resource;
            imageArray = new string[36];
            for (int i = 0; i < 36; i++)
            {
                imageArray[i] = directory + "images/loading/loading_" + i.ToString("00") + ".png";
            }
            ImageArray = imageArray;
            Size2D = new Size2D(150, 180);
            //Centering the Loading indicator according to the absolute layout
            PositionUsesPivotPoint = true;
            ParentOrigin = Position.ParentOriginCenter;
            PivotPoint = Position.PivotPointCenter;
        }
    }
}
