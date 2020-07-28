using System;
using TicTacToeLibrary;

namespace TicTacToe_Simulation
{
    public class Simulation
    {
        private Game game;
        private BST<GameState> stateTree;
        private int moveCount;
        public Simulation(Game simGame)
        {
            game = simGame;
            stateTree = new BST<GameState>();
            moveCount = 0;

        }
        public bool makeMove(Game currentGame, Move move, int level)
        {
            //Console.WriteLine("Level:" + level);
            if (!currentGame.MakeMove(move))
                return false;
            
            int id = currentGame.GetStateId();
            //Console.WriteLine("Id: " + id);
            if (stateTree.Find(id) == stateTree.NullNode)
            {
                moveCount++;
                //Console.WriteLine(moveCount);
                if (currentGame.CheckForWin())
                {
                    Move[] moves = new Move[0];
                    //Console.WriteLine("Level " + level);
                    //Console.WriteLine("Id: " + id);
                    //Console.WriteLine("Move: " + moveCount);
                    //Console.WriteLine(currentGame.GameBoard.BoardState.RenderAsString());
                    
                    if (currentGame.IsPlayer1Turn())
                    {
                        GameState winState = new GameState(id, moves, true, -100, 100, ref stateTree);
                        BSTNode<GameState> winNode = new BSTNode<GameState>(id, winState);
                        stateTree.Insert(winNode);
                    }
                    else
                    {
                        GameState winState = new GameState(id, moves, false, 100, -100, ref stateTree);
                        BSTNode<GameState> winNode = new BSTNode<GameState>(id, winState);
                        stateTree.Insert(winNode);
                    }
                }
                else if (currentGame.CheckForDraw())
                {
                    //Console.WriteLine("Level: " + level);
                    //Console.WriteLine("Id: " + id);
                    //Console.WriteLine("Move: " + moveCount);
                    //Console.WriteLine(currentGame.GameBoard.BoardState.RenderAsString());
                    
                    Move[] moves = new Move[0];
                    GameState drawState = new GameState(id, moves, currentGame.IsPlayer1Turn(), 0, 0, ref stateTree);
                    BSTNode<GameState> drawNode = new BSTNode<GameState>(id, drawState);
                    stateTree.Insert(drawNode);
                }
                else
                {
                    Move[] possibleMoves = currentGame.FindPossibleMoves();

                    GameState newState = new GameState(id, possibleMoves, currentGame.IsPlayer1Turn(), -101, -101, ref stateTree);
                    BSTNode<GameState> newNode = new BSTNode<GameState>(id, newState);
                    stateTree.Insert(newNode);
                    foreach (Move currentMove in possibleMoves)
                        makeMove(currentGame.Copy(), currentMove, level + 1);
                }
            }
            return true;
            
        }

        public void RunSimulation()
        {
            Move[] moves = game.FindPossibleMoves();
            GameState baseState = new GameState(0, moves, true, -101, -101, ref stateTree);
            BSTNode<GameState> newNode = new BSTNode<GameState>(0, baseState);
            stateTree.Insert(newNode);
            foreach (Move currentMove in moves)
            {
                makeMove(game.Copy(), currentMove, 1);
            }
            baseState.setWinProbabilities();

        }

       public BST<GameState> StateTree
        {
            get
            {
                return stateTree;
            }
            set
            {
                stateTree = value;
            }
        }



        public int Count
        {
            get
            {
                return moveCount;
            }
        }
    }
}
