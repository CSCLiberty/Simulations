using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeLibrary
{
    public abstract class Game
    { 
        protected BoardState gameBoard;
        public abstract Game Copy();
        public abstract bool MakeMove(Move move);
        public abstract bool CheckForWin();
        public abstract bool CheckForDraw();
        protected abstract int CheckForNecessaryMove(int playerNum);
        public abstract int CheckForPossibleWin();
        public abstract int CheckForBlock();
        public abstract bool IsPlayer1Turn();
        public abstract Move[] FindPossibleMoves();
        public abstract BoardState GameBoard
        {
            get;
        }
        public abstract int GetStateId();


    }
}
