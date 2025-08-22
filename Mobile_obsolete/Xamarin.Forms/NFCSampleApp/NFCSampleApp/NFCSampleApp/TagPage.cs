using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace NFCSampleApp
{
    /// <summary>
    /// Page class for Tag
    /// </summary>
    public class TagPage : TabbedPage
    {
        INfcImplementation nfc = DependencyService.Get<INfcImplementation>();
        ILog log = DependencyService.Get<ILog>();

        /// <summary>
        /// Constructor of Tag Page class
        /// </summary>
        public TagPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize function of Tag Page class
        /// </summary>
        private void InitializeComponent()
        {
            this.Title = "Tabbed Page";

            this.Children.Add(new ReadPage());

            this.Children.Add(new WritePage());
        }
    }
}