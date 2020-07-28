using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe_Simulation;
using TicTacToeLibrary;
namespace TicTacToeGameplay
{
    public class TicTacToeComputer : TicTacToePlayer
    {
        protected BST<GameState> stateTree;
        public TicTacToeComputer(TicTacToeState state, short num, BST<GameState> tree)
        {
            playerState = state;
            stateTree = tree;
            playerNum = num;

        }
        public override void MakeMove(TicTacToeGame currentGame)
        {

            Move currentMove;
            int temp = currentGame.CheckForPossibleWin();
            if (temp == -1)
            {
                temp = currentGame.CheckForBlock();
                if (temp != -1)
                    currentMove = new TicTacToeMove(currentGame.GameBoard.Tiles[temp]);
                else
                    currentMove = stateTree.Find(currentGame.GetStateId()).Value.BestMove;
                
            }
            else
                currentMove = new TicTacToeMove(currentGame.GameBoard.Tiles[temp]);
            currentGame.MakeMove(currentMove);

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
        public override void EndGame(int outcome) { }
    }
}
