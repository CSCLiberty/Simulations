using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeLibrary
{


    public class BoardState
    {
        private Tile[] tiles;
        private int numTiles;
        private int numRows;
        public BoardState(int numTiles, int numRows)
        {
            tiles = new Tile[numTiles];
            for (int i = 0; i < 9; i++)
            {
                tiles[i] = new Tile((TileLocation)(i), i / numRows, i % numRows);
            }

        }

        public BoardState(BoardState oldBoardState)
        {
            tiles = new Tile[9];
            for (int i = 0; i < 9; i++)
            {
                tiles[i] = new Tile((TileLocation)(i), i/3, i % 3);
                tiles[i].State = oldBoardState.Tiles[i].State;
                
            }

        }

        public Tile Position(TileLocation position)
        {
            return tiles[(int) position];
        }

        public String RenderAsString()
        {
            String _rendering = "-------\n";

            for (int i=0;i<9;i++)
            {
                _rendering += Tiles[i].State.ToString().Substring(0,1) + " ";
                if ((i+1) % 3 == 0)
                {
                    _rendering += "\n";
                }
            }

            _rendering += "-------\n";

            return _rendering;

        }

        public Tile[] Tiles
        {
            get
            {
                return tiles;
            }
        }


    }
}
