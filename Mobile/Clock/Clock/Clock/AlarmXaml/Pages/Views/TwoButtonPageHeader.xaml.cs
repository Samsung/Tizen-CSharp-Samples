using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Clock.Pages.Views
{
    public partial class TwoButtonPageHeader : ContentView
    {
        private Button leftButton;
        private Label centerTitleLabel;
        private Button rightButton;

        public TwoButtonPageHeader()
        {
            InitializeComponent();
            leftButton = this.FindByName<Button>("LeftButton");
            centerTitleLabel = this.FindByName<Label>("CenterTitle");
            rightButton = this.FindByName<Button>("RightButton");
        }

        /// <summary>
        /// Left button property to show on top of the page
        /// </summary>
        public string LeftButtonText
        {
            get
            {
                return leftButton.Text;
            }

            set
            {
                leftButton.Text = value;
            }
        }

        /// <summary>
        /// Right button property to show on top of the page
        /// </summary>
        public string RightButtonText
        {
            get
            {
                return rightButton.Text;
            }

            set
            {
                rightButton.Text = value;
            }
        }

        /// <summary>
        /// Title label on top of the page
        /// </summary>
        public string CenterTitleText
        {
            get
            {
                return centerTitleLabel.Text;
            }

            set
            {
                centerTitleLabel.Text = value;
            }
        }

        public event EventHandler RightButtonEvent
        {
            add
            {
                rightButton.Clicked += value;
            }

            remove
            {
                rightButton.Clicked -= value;
            }
        }

        public event EventHandler LeftButtonEvent
        {
            add
            {
                leftButton.Clicked += value;
            }

            remove
            {
                leftButton.Clicked -= value;
            }
        }

        /// <summary>
        /// Default event behavior to be invoked when left button clicked
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event argument</param>
        private void LeftButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
