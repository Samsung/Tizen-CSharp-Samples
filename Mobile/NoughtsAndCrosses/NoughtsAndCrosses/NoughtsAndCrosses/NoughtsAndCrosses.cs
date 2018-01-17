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
            FindGoodPlace();
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

        void FindGoodPlace()
        {
            int i, t, maxScore = -0x7fffffff, maxScoreRow = MainLayout.ZERO, maxScoreCol = MainLayout.ZERO;
            int[,] tempGameboard = new int[,]{
                {MainLayout.EMPTY, MainLayout.EMPTY, MainLayout.EMPTY },
                {MainLayout.EMPTY, MainLayout.EMPTY, MainLayout.EMPTY },
                {MainLayout.EMPTY, MainLayout.EMPTY, MainLayout.EMPTY }
            };
            for (i = MainLayout.ZERO; i < MainLayout.GAMEBOARD_SIZE; i += 1)
            {
                for (t = MainLayout.ZERO; t < MainLayout.GAMEBOARD_SIZE; t += 1)
                {
                    if (gameboard[i, t] == MainLayout.HUMAN_SELECTED)
                    {
                        if (i > 0)//N
                        {
                            if (gameboard[i - 1, t] == MainLayout.EMPTY)
                            {
                                tempGameboard[i - 1, t] += 1;
                            }
                        }
                        if (i > 0 && t < MainLayout.GAMEBOARD_SIZE - 1)//NE
                        {
                            if (gameboard[i - 1, t + 1] == MainLayout.EMPTY)
                            {
                                tempGameboard[i - 1, t + 1] += 1;
                            }
                        }
                        if (t < MainLayout.GAMEBOARD_SIZE - 1)//E
                        {
                            if (gameboard[i, t + 1] == MainLayout.EMPTY)
                            {
                                tempGameboard[i, t + 1] += 1;
                            }
                        }
                        if(i < MainLayout.GAMEBOARD_SIZE - 1 && t < MainLayout.GAMEBOARD_SIZE - 1)//SE
                        {
                            if(gameboard[i + 1, t + 1] == MainLayout.EMPTY)
                            {
                                tempGameboard[i + 1, t + 1] += 1;
                            }
                        }
                        if (i < MainLayout.GAMEBOARD_SIZE - 1)//S
                        {
                            if (gameboard[i + 1, t] == MainLayout.EMPTY)
                            {
                                tempGameboard[i + 1, t] += 1;
                            }
                        }
                        if(i < MainLayout.GAMEBOARD_SIZE - 1 && t > 0)//SW
                        {
                            if(gameboard[i + 1, t - 1] == MainLayout.EMPTY)
                            {
                                tempGameboard[i + 1, t - 1] += 1;
                            }
                        }
                        if (t > 0)
                        {
                            if (gameboard[i, t - 1] == MainLayout.EMPTY)//W
                            {
                                tempGameboard[i, t - 1] += 1;
                            }
                        }
                        if(i > 0 && t > 0)
                        {
                            if(gameboard[i - 1, t - 1] == MainLayout.EMPTY)//NW
                            {
                                tempGameboard[i - 1, t - 1] += 1;
                            }
                        }
                    }
                }
            }

            for (i = MainLayout.ZERO; i < MainLayout.GAMEBOARD_SIZE; i += 1)
            {
                for (t = MainLayout.ZERO; t < MainLayout.GAMEBOARD_SIZE; t += 1)
                {
                    if(maxScore < tempGameboard[i, t])
                    {
                        maxScore = tempGameboard[i, t];
                        maxScoreRow = i;
                        maxScoreCol = t;
                    }
                }
            }

            gameboard[maxScoreRow, maxScoreCol] = MainLayout.COMPUTER_SELECTED;
        }
    }
}
