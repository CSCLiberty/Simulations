using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLibrary;
using TicTacToe_Simulation;

namespace TicTacToeGameplay
{
    public class Match
    {
        TicTacToePlayer player1;
        TicTacToePlayer player2;
        int player1Wins;
        int player2Wins;
        int draws;
        BST<GameState> stateTree;

        public Match (TicTacToePlayer p1, TicTacToePlayer p2, BST<GameState> tree)
        {
            player1Wins = 0;
            player2Wins = 0;
            draws = 0;
            player1 = p1;
            player2 = p2;
            stateTree = tree;
        }

        public void RunMatch()
        {
            int decision = 0;
            bool player1IsX = true;
            RunGame(player1, player2);
            decision = Match.inputInteger("Enter 1 to play again, 2 to switch states, or -1 to exit");
            if (decision == 2)
            {
                player1IsX = !player1IsX;
                SwitchStates();
            }
            while (decision != -1)
            {
                //for (int i = 0; i < 1000; i++)
                RunGame(player1IsX ? player1 : player2, player1IsX ? player2 : player1);
                decision = Match.inputInteger("Enter 1 to play again, 2 to switch states, or -1 to exit");
                if (decision == 2)
                {
                    player1IsX = !player1IsX;
                    SwitchStates();
                }
            }
        }
        public void OutputGame(Game currentGame, TicTacToePlayer playerX, TicTacToePlayer playerO, TicTacToeState state, short num)
        {
            BSTNode<GameState> currentNode = stateTree.Find(currentGame.GetStateId());
            
            Console.WriteLine("Player 1 (" + player1.PlayerState + ") Wins: " + player1Wins);
            Console.WriteLine("Player 2 (" + player2.PlayerState + ") Wins: " + player2Wins);
            Console.WriteLine("Draws: " + draws);
            if (currentNode != stateTree.NullNode)
            {
                GameState currentGameState = currentNode.Value;
                Console.WriteLine("Player " + playerX.PlayerNum + " win probability: " + currentGameState.WinProbability1 + "%");
                Console.WriteLine("Player " + playerO.PlayerNum + " win probability: " + currentGameState.WinProbability2 + "%");
            }
            Console.WriteLine(state + "(Player " + num + ")'s Move");
            Console.Write(currentGame.GameBoard.RenderAsString());
        }

        public void SwitchStates()
        {
            if (player1.PlayerState == TicTacToeState.X)
            {
                player1.PlayerState = TicTacToeState.O;
                player2.PlayerState = TicTacToeState.X;
            }
            else
            {
                player1.PlayerState = TicTacToeState.X;
                player2.PlayerState = TicTacToeState.O;
            }
        }
        public void RunGame()
        {
            RunGame(player1, player2);
        }
        public void RunGame(TicTacToePlayer playerX, TicTacToePlayer playerO)
        {
            TicTacToeGame currentGame = new TicTacToeGame(TicTacToeState.X);
            short winningPlayer = 0;
            TicTacToePlayer currentPlayer = playerX;
            OutputGame(currentGame, playerX, playerO, TicTacToeState.X, playerX.PlayerNum);
            while (true)
            {
                currentPlayer.MakeMove(currentGame);
                OutputGame(currentGame, playerX, playerO, currentPlayer.PlayerState, currentPlayer.PlayerNum);
                if (currentGame.CheckForWin())
                {
                    winningPlayer = currentPlayer.PlayerNum;
                    break;
                }
                else if (currentGame.CheckForDraw())
                    break;
                currentPlayer = currentPlayer == playerX ? playerO : playerX;
               
            }
            if (winningPlayer == 1)
            {
                player1Wins++;
                Console.WriteLine("Player 1 wins!");
                player1.EndGame(1);
                player2.EndGame(-1);
            }
            else if (winningPlayer == 2)
            {
                player2Wins++;
                Console.WriteLine("Player 2 wins!");
                player1.EndGame(-1);
                player2.EndGame(1);
            }
            else
            {
                draws++;
                Console.WriteLine("It's a tie!");
                player1.EndGame(0);
                player2.EndGame(0);
            }
        }

        public static int inputInteger(string outputText)
        {
            bool success = false;
            int num = 0;
            while (!success)
            {
                Console.WriteLine(outputText);
                string input = Console.ReadLine();
                try
                {
                    success = true;
                    num = int.Parse(input);
                }
                catch (System.FormatException)
                {
                    success = false;
                }
            }
            return num;
        }
    }
   
}
