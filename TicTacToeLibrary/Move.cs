using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeLibrary
{
    public abstract class Move
    {
        public abstract Tile DestinationTile
        {
            get;
        }
        public override string ToString()
        {
            return DestinationTile.ToString();
        }
    }
}
