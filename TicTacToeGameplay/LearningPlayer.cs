using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLibrary;
using TicTacToe_Simulation;

namespace TicTacToeGameplay
{
    public class LearningPlayer : TicTacToeComputer
    {
        private GameState[] accessedStates = new GameState[5];
        private int count = 0;
        public LearningPlayer(TicTacToeState state, short num, BST<GameState> tree) : base(state, num, tree) { }
        public override void MakeMove(TicTacToeGame currentGame)
        {

            Move currentMove;
            GameState currentState = stateTree.Find(currentGame.GetStateId()).Value;
            int temp = currentGame.CheckForPossibleWin();
            if (temp == -1)
            {
                temp = currentGame.CheckForBlock();
                if (temp != -1)
                    currentMove = new TicTacToeMove(currentGame.GameBoard.Tiles[temp]);
                else
                    currentMove = currentState.BestMove;

            }
            else
                currentMove = new TicTacToeMove(currentGame.GameBoard.Tiles[temp]);
            currentGame.MakeMove(currentMove);
            accessedStates[count++] = currentState;
        }
        public override void EndGame(int outcome)
        {
            for (int i = 0; i < 5; i++)
            {
                if (accessedStates[i] != null)
                {
                    accessedStates[i].AddGame(outcome);
                    accessedStates[i] = null;
                }
            }
            count = 0;
        }
    }
}
