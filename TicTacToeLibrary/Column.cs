using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeLibrary
{
    public class Column
    {
        public Tile Tile1;
        public Tile Tile2;
        public Tile Tile3;
        private Tile[] tiles;

        public Column (Tile tile1, Tile tile2, Tile tile3)
        {
            Tile1 = tile1;
            Tile2 = tile2;
            Tile3 = tile3;
            tiles = new Tile[3];
            tiles[0] = tile1;
            tiles[1] = tile2;
            tiles[2] = tile3;
        }

        public bool CheckColumnForWin()
        {
            return (Tile1.State == Tile2.State
                 && Tile2.State == Tile3.State
                 && Tile1.State != TicTacToeState.Empty);
        }

        /*
         * Checks to see if the game can be won on this column in the next turn. If it can be, 
         * returns the tile number of the tile on which it can be won. Otherwise returns -1
         */
        public int CheckForTwoOfTheSame()
        {
            if (Tile1.State == Tile2.State && Tile1.State != TicTacToeState.Empty && Tile3.State == TicTacToeState.Empty)
                return (int)Tile3.TilePosition;
            if (Tile2.State == Tile3.State && Tile2.State != TicTacToeState.Empty && Tile1.State == TicTacToeState.Empty)
                return (int)Tile1.TilePosition;
            if (Tile1.State == Tile3.State && Tile1.State != TicTacToeState.Empty && Tile2.State == TicTacToeState.Empty)
                return (int)Tile2.TilePosition;
            return -1;
        }

        public ref Tile[] Tiles
        {
            get
            {
                return ref tiles;
            }
        }

        //private bool CheckForBlock()
        //{
        //    return (Tile1.State != Tile2.State || )
        //}
    }
}
