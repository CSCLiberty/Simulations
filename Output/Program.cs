using System;
using TicTacToeLibrary;
using TicTacToe_Simulation;
using TicTacToeGameplay;

namespace ticktacktoe
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                        Game myGame = Game.Start();


                            myGame.PlaceTile(TileLocation.LowerLeft);
                            Console.WriteLine(myGame.GameBoard.BoardState.RenderAsString());
                            myGame.PlaceTile(TileLocation.LowerRight);
                            Console.WriteLine(myGame.GameBoard.BoardState.RenderAsString());
                            myGame.PlaceTile(TileLocation.MiddleCenter);
                            Console.WriteLine(myGame.GameBoard.BoardState.RenderAsString());
                            myGame.PlaceTile(TileLocation.LowerCenter);
                            Console.WriteLine(myGame.GameBoard.BoardState.RenderAsString());
                            myGame.PlaceTile(TileLocation.MiddleLeft);
                            Console.WriteLine(myGame.GameBoard.BoardState.RenderAsString());
                            myGame.PlaceTile(TileLocation.TopCenter);
                            Console.WriteLine(myGame.GameBoard.BoardState.RenderAsString());
                            myGame.PlaceTile(TileLocation.TopRight);
                            Console.WriteLine(myGame.GameBoard.BoardState.RenderAsString());
                            myGame.PlaceTile(TileLocation.TopLeft);
                            Console.WriteLine(myGame.GameBoard.BoardState.RenderAsString());
                            myGame.PlaceTile(TileLocation.MiddleRight);
                            Console.WriteLine(myGame.GameBoard.BoardState.RenderAsString());


            */
            TicTacToeGame baseGame = new TicTacToeGame(TicTacToeState.X);
            Simulation mySimulation = new Simulation(baseGame);
            mySimulation.RunSimulation();
            Match myMatch = new Match(new TicTacToeUser(TicTacToeState.X, 1), new TicTacToeComputer(TicTacToeState.O, 2, mySimulation.StateTree), mySimulation.StateTree);
            Console.WriteLine("Press enter key...");
            Console.Read();

        }


    }
}
