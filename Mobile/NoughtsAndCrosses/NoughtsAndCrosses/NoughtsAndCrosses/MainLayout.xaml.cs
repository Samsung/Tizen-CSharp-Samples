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
        public static readonly int
            EMPTY = 0,
            COMPUTER_SELECTED = 1,
            HUMAN_SELECTED = 2,
            
            ZERO = 0,
            
            GAMEBOARD_SIZE = 3;
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
            int i, t, q = ZERO;
            for(i = ZERO; i < GAMEBOARD_SIZE; i += 1)
            {
                for(t = ZERO; t < GAMEBOARD_SIZE; t += 1)
                {
                    Button indexOfButton = (Button)gameboard.Children.ElementAt(q);
                    if(gameboardArray[i, t] == HUMAN_SELECTED)
                    {
                        indexOfButton.Text = "O";
                    }
                    else if(gameboardArray[i, t] == COMPUTER_SELECTED)
                    {
                        indexOfButton.Text = "X";
                    }
                    else
                    {
                        indexOfButton.Text = "";
                    }
                    q += 1;
                }
            }
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