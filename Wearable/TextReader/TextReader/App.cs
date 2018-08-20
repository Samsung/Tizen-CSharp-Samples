using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using TextReader.Views;
using TextReader.Tizen.Wearable.Views;

namespace TextReader
{
    public class App : Application
    {
        #region methods

        /// <summary>
        /// App class constructor.
        /// Creates the main page of application.
        /// </summary>
        public App()
        {
            MainPage = DependencyService.Get<IViewResolver>().GetRootPage();
        }

        #endregion
    }
}
