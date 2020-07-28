using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeLibrary
{
    public enum TicTacToeState
    {
        Empty,
        X,
        O
    }

    public enum TileLocation
    {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        LowerLeft,
        LowerCenter,
        LowerRight
    }

    public class Tile
    {
        public TicTacToeState State = TicTacToeState.Empty;
        private TileLocation tilePosition;
        private int rowNum;
        private int colNum;
        public Tile(TileLocation position, int row, int col)
        {
            tilePosition = position;
            rowNum = row;
            colNum = col;
        }
        public Tile(string tileString)
        {
            rowNum = int.Parse(tileString.Substring(0, 1));
            colNum = int.Parse(tileString.Substring(1, 1));
            tilePosition = (TileLocation)int.Parse(tileString.Substring(2, 2));
            State = (TicTacToeState)int.Parse(tileString.Substring(4, 1));
        }
        public override string ToString()
        {
            return rowNum.ToString() + colNum.ToString() + ((int)tilePosition).ToString("00") + ((int)State).ToString();
        }
        public TileLocation TilePosition
        {
            get
            {
                return tilePosition;
            }
            set
            {
                tilePosition = value;
            }
        }
        public int RowNum
        { 
            get
            {
                return rowNum;
            }
        }
        public int ColNum
        {
            get
            {
                return colNum;
            }
        }
        public int TileNum
        {
            get
            {
                return (int)TilePosition;
            }
        }

    }
}
