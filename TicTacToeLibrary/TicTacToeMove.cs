using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeLibrary
{
    public class TicTacToeMove : Move
    {
        private Tile destinationTile;
        public TicTacToeMove (Tile dest)
        {
            destinationTile = dest;
        }
        public TicTacToeMove (string moveString)
        {
            destinationTile = new Tile(moveString);
        }
        public override Tile DestinationTile
        {
            get
            {
                return destinationTile;
            }
        }

    }
}
