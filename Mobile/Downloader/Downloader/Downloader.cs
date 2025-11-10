using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace Downloader
{
    class Program : NUIApplication
    {
        private Window window;
        private Navigator navigator;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            window = NUIApplication.GetDefaultWindow();
            navigator = window.GetDefaultNavigator();

            var downloadMainPage = new DownloadMainPage();
            var downloadInfoPage = new DownloadInfoPage();

            navigator.Push(downloadMainPage);
        }

        static void Main(string[] args)
        {
            NUIApplication.IsUsingXaml = false;
            var app = new Program();
            app.Run(args);
        }
    }
}
