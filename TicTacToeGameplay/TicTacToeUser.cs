using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLibrary;

namespace TicTacToeGameplay
{
    public class TicTacToeUser : TicTacToePlayer
    {
        public TicTacToeUser(TicTacToeState state, short num)
        {
            playerState = state;
            playerNum = num;
        }
        public override void MakeMove(TicTacToeGame currentGame)
        {
            int moveNum = Match.inputInteger("Please enter an integer move for player " + PlayerState);
            while (moveNum < 0 || moveNum > 8 || !(currentGame.GameBoard.Tiles[moveNum].State == TicTacToeState.Empty))
            {
                moveNum = Match.inputInteger("Error! Please enter a valid move for player " + PlayerState);
            }
            currentGame.MakeMove(new TicTacToeMove(currentGame.GameBoard.Tiles[moveNum]));
        }
        public override void EndGame (int outcome) { }

    }
}
