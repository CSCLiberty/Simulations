using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLibrary;

namespace TicTacToeLibrary
{
    public enum ColumnName
    {
        HorizontalTop,
        HorizontalMiddle,
        HorizontalBottom,
        VerticalLeft,
        VerticalMiddle,
        VerticalRight,
        DiagonalTopLeft,
        DiagonalTopRight
    }

    public class TicTacToeGame : Game
    {

        private TicTacToeState _currentPlayer;
        private int tilesPlaced;
        private Column[] columns;
        public TicTacToeGame(TicTacToeState whoGoesFirst)
        {
            _currentPlayer = whoGoesFirst;
            gameBoard = new BoardState();
            tilesPlaced = 0;
            columns = new Column[8];
            columns[(int)ColumnName.HorizontalTop] = new Column(GameBoard.Tiles[0], GameBoard.Tiles[1], GameBoard.Tiles[2]);
            columns[(int)ColumnName.HorizontalMiddle] = new Column(GameBoard.Tiles[3], GameBoard.Tiles[4], GameBoard.Tiles[5]);
            columns[(int)ColumnName.HorizontalBottom] = new Column(GameBoard.Tiles[6], GameBoard.Tiles[7], GameBoard.Tiles[8]);
            columns[(int)ColumnName.VerticalLeft] = new Column(GameBoard.Tiles[0], GameBoard.Tiles[3], GameBoard.Tiles[6]);
            columns[(int)ColumnName.VerticalMiddle] = new Column(GameBoard.Tiles[1], GameBoard.Tiles[4], GameBoard.Tiles[7]);
            columns[(int)ColumnName.VerticalRight] = new Column(GameBoard.Tiles[2], GameBoard.Tiles[5], GameBoard.Tiles[8]);
            columns[(int)ColumnName.DiagonalTopLeft] = new Column(GameBoard.Tiles[0], GameBoard.Tiles[4], GameBoard.Tiles[8]);
            columns[(int)ColumnName.DiagonalTopRight] = new Column(GameBoard.Tiles[2], GameBoard.Tiles[4], GameBoard.Tiles[6]);


        }
        public TicTacToeGame(TicTacToeState whoGoesFirst, BoardState oldGameBoard, int oldTilesPlaced)
        {
            _currentPlayer = whoGoesFirst;
            gameBoard = new BoardState(oldGameBoard);
            tilesPlaced = oldTilesPlaced;
            columns = new Column[8];
            columns[(int)ColumnName.HorizontalTop] = new Column(GameBoard.Tiles[0], GameBoard.Tiles[1], GameBoard.Tiles[2]);
            columns[(int)ColumnName.HorizontalMiddle] = new Column(GameBoard.Tiles[3], GameBoard.Tiles[4], GameBoard.Tiles[5]);
            columns[(int)ColumnName.HorizontalBottom] = new Column(GameBoard.Tiles[6], GameBoard.Tiles[7], GameBoard.Tiles[8]);
            columns[(int)ColumnName.VerticalLeft] = new Column(GameBoard.Tiles[0], GameBoard.Tiles[3], GameBoard.Tiles[6]);
            columns[(int)ColumnName.VerticalMiddle] = new Column(GameBoard.Tiles[1], GameBoard.Tiles[4], GameBoard.Tiles[7]);
            columns[(int)ColumnName.VerticalRight] = new Column(GameBoard.Tiles[2], GameBoard.Tiles[5], GameBoard.Tiles[8]);
            columns[(int)ColumnName.DiagonalTopLeft] = new Column(GameBoard.Tiles[0], GameBoard.Tiles[4], GameBoard.Tiles[8]);
            columns[(int)ColumnName.DiagonalTopRight] = new Column(GameBoard.Tiles[2], GameBoard.Tiles[4], GameBoard.Tiles[6]);

        }
        public TicTacToeGame(TicTacToeGame oldGame)
        {
            _currentPlayer = oldGame.CurrentPlayer;
            gameBoard = new BoardState(oldGame.GameBoard);
            tilesPlaced = oldGame.TilesPlaced;
            columns = new Column[8];
            columns[(int)ColumnName.HorizontalTop] = new Column(GameBoard.Tiles[0], GameBoard.Tiles[1], GameBoard.Tiles[2]);
            columns[(int)ColumnName.HorizontalMiddle] = new Column(GameBoard.Tiles[3], GameBoard.Tiles[4], GameBoard.Tiles[5]);
            columns[(int)ColumnName.HorizontalBottom] = new Column(GameBoard.Tiles[6], GameBoard.Tiles[7], GameBoard.Tiles[8]);
            columns[(int)ColumnName.VerticalLeft] = new Column(GameBoard.Tiles[0], GameBoard.Tiles[3], GameBoard.Tiles[6]);
            columns[(int)ColumnName.VerticalMiddle] = new Column(GameBoard.Tiles[1], GameBoard.Tiles[4], GameBoard.Tiles[7]);
            columns[(int)ColumnName.VerticalRight] = new Column(GameBoard.Tiles[2], GameBoard.Tiles[5], GameBoard.Tiles[8]);
            columns[(int)ColumnName.DiagonalTopLeft] = new Column(GameBoard.Tiles[0], GameBoard.Tiles[4], GameBoard.Tiles[8]);
            columns[(int)ColumnName.DiagonalTopRight] = new Column(GameBoard.Tiles[2], GameBoard.Tiles[4], GameBoard.Tiles[6]);
        }

        public override BoardState GameBoard
        {
            get
            {
                return gameBoard;
            }
        }

        public override Game Copy()
        {
            TicTacToeGame newGame = new TicTacToeGame(_currentPlayer, gameBoard, tilesPlaced);
            return newGame;
        }
        
        public static TicTacToeGame Start()
        {
            return new TicTacToeGame(TicTacToeState.X);

        }
        
        public override bool MakeMove(Move move)
        {
            bool succesfulMove = false;

            if (GameBoard.Tiles[(int)move.DestinationTile.TileNum].State == TicTacToeState.Empty)
            {
                GameBoard.Tiles[(int)move.DestinationTile.TileNum].State = _currentPlayer;
                succesfulMove = true;
            }
            else
                succesfulMove = false;
            tilesPlaced += succesfulMove ? 1 : 0;
            /*if (CheckForWin())
            {
                Console.WriteLine("player " + _currentPlayer + " wins.");
            }
            */
            _currentPlayer = (_currentPlayer == TicTacToeState.O) ? TicTacToeState.X : TicTacToeState.O;
            return succesfulMove;
            
        }

        public override bool CheckForWin()
        {
            foreach (Column currentColumn in columns)
            {
                if (currentColumn.CheckColumnForWin())
                    return true;
            }
            return false;

        }

        public ref Column[] Columns
        {
            get
            {
                return ref columns;
            }
        }

        /*
         * Checks to see if the game can be won by the specified player. If so, returns the tile number
         * on which they can win. Otherwise returns -1.
         */
        protected override int CheckForNecessaryMove(int playerNum)
        {
            TicTacToeState state = (TicTacToeState)playerNum;
            if (tilesPlaced == 1)
                return -1;
            int temp;
            foreach (Column currentColumn in columns)
            {
                temp = currentColumn.CheckForTwoOfTheSame();
                if (temp != -1)
                {
                    foreach (Tile currentTile in currentColumn.Tiles)
                    {
                        if (currentTile.State != TicTacToeState.Empty)
                        {
                            if (currentTile.State == state)
                                return temp;
                            else
                                break;
                        }
                    }
                }
            }
            return -1;
        }

        public override int CheckForPossibleWin()
        {
            return CheckForNecessaryMove((int)_currentPlayer);
        }
        public override int CheckForBlock()
        {
            return CheckForNecessaryMove((_currentPlayer == TicTacToeState.X ? (int)TicTacToeState.O : (int)TicTacToeState.X));
        }

        public override bool CheckForDraw()
        {
            if (tilesPlaced == 9 & !CheckForWin())
                return true;
            return false;

        }
        public override bool IsPlayer1Turn()
        {
            return _currentPlayer == TicTacToeState.X ? true : false;
        }

        public TicTacToeState CurrentPlayer
        {
            get
            {
                return _currentPlayer;
            }
        }

        public int TilesPlaced
        {
            get
            {
                return tilesPlaced;
            }
        }

        public override Move[] FindPossibleMoves()
        {
            int tileNum = CheckForPossibleWin();
            Move[] moves;
            if (tileNum != -1)
            {
                moves = new Move[1];
                moves[0] = new TicTacToeMove(gameBoard.Tiles[tileNum]);
            }
            else
            {
                tileNum = CheckForBlock();
                if (tileNum != -1)
                {
                    moves = new Move[1];
                    moves[0] = new TicTacToeMove(gameBoard.Tiles[tileNum]);
                }
                else
                {
                    moves = new Move[(9 - TilesPlaced)];
                    int i = 0;
                    foreach (Tile currentTile in GameBoard.Tiles)
                    {
                        if (currentTile.State == TicTacToeState.Empty)
                            moves[i++] = new TicTacToeMove(gameBoard.Tiles[currentTile.TileNum]);
                    }
                }
            }
            return moves;


        }

        public override int GetStateId()
        {
            int id = 0;
            foreach (Tile currentTile in GameBoard.Tiles)
            {
                if (currentTile.State != TicTacToeState.Empty)
                {
                    id += (int)currentTile.State * (int)Math.Pow(3, (int)currentTile.TilePosition);
                }
            }
            return id;
        }
    }
}
