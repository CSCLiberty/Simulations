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
            myMatch.RunMatch();
            //LearningSimulator myLearningSimulation = new LearningSimulator();
            //myLearningSimulation.RunSimulation(100000);
            //myLearningSimulation.StateTree.OutputTree("myLearningSimulationTree.txt");
            //mySimulation.StateTree.OutputTree("mySimulationTree.txt");
            //Simulation secondSimulation = new Simulation(baseGame);
            //secondSimulation.StateTree = LearningSimulator.InputStateTree("mySimulationTree.txt");
            /*BST<GameState> myTree = LearningSimulator.InputStateTree("myLearningSimulationTree.txt");
            Match myMatch2 = new Match(new TicTacToeComputer(TicTacToeState.X, 1, mySimulation.StateTree), new LearningPlayer(TicTacToeState.O, 2, myLearningSimulation.StateTree), myTree);
            for (int i = 0; i < 10000000; i++)
            {
                myMatch2.RunGame();
                if (i % 1000 == 0)
                    myMatch2.SwitchStates();

            }
            */
            Console.WriteLine("Press enter key...");
            Console.Read();
 

        }


    }
}
