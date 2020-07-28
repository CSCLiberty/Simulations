using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLibrary;

namespace TicTacToeGameplay
{
    public abstract class TicTacToePlayer
    {
        protected TicTacToeState playerState;
        protected short playerNum;
        public abstract void MakeMove(TicTacToeGame currentGame);

        public TicTacToeState PlayerState
        {
            get
            {
                return playerState;
            }
            set
            {
                playerState = value;
            }
        }
        public short PlayerNum
        {
            get
            {
                return playerNum;
            }
            set
            {
                playerNum = value;
            }
        }
        public abstract void EndGame(int outcome);
    }
}
