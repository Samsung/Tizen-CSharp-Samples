using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoughtsAndCrosses
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainLayout : ContentPage
	{
        public App app;

		public MainLayout ()
		{
			InitializeComponent ();
		}

        public MainLayout(App app)
        {
            InitializeComponent();
            this.app = app;
        }

        public void SetLabelHelloWorld(string text)
        {
            labelHelloWorld.Text = text;
        }

        public void SetGameBoard(int[, ] gameboardArray)
        {
            
        }

        public void HandleGameBoardBtnClick(Object sender, System.EventArgs e)
        {
            Button thisButton = (Button)sender;
            int row = Grid.GetRow(thisButton);
            int col = Grid.GetColumn(thisButton);
            app.ChangeGameBoardBtnText(thisButton, row, col);
        }

        public void HandleGameResetBtnClick(Object sender, System.EventArgs e)
        {
            Button thisButton = (Button)sender;
            app.ChangeResetBtnText(thisButton);
        }
	}
}