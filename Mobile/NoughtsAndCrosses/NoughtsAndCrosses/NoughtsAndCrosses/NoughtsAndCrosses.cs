using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace NoughtsAndCrosses
{
    public class App : Application
    {
        public MainLayout mainLayout;

        int[, ] gameboard = new int[,]{
            {MainLayout.EMPTY, MainLayout.EMPTY, MainLayout.EMPTY },
            {MainLayout.EMPTY, MainLayout.EMPTY, MainLayout.EMPTY },
            {MainLayout.EMPTY, MainLayout.EMPTY, MainLayout.EMPTY }
        };

        public App()
        {
            // The root page of your application
            mainLayout = new MainLayout(this) { };

            MainPage = mainLayout;

            mainLayout.SetGameBoard(gameboard);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            mainLayout.SetLabelHelloWorld("Noughts and crosses");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void ChangeGameBoardBtnText(Button button, int row, int col)
        {
            gameboard[row, col] = MainLayout.HUMAN_SELECTED;
            mainLayout.SetGameBoard(gameboard);
        }

        public void ChangeResetBtnText(Button button)
        {
            int i, t;
            for(i = MainLayout.ZERO; i < MainLayout.GAMEBOARD_SIZE; i += 1)
            {
                for(t = MainLayout.ZERO; t < MainLayout.GAMEBOARD_SIZE; t += 1)
                {
                    gameboard[i, t] = MainLayout.EMPTY;
                }
            }
            mainLayout.SetGameBoard(gameboard);
        }
    }
}
