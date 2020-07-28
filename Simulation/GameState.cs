using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLibrary;

namespace TicTacToe_Simulation
{
    public class GameState
    {
        private int [] stateId;
        private double winProbability1;
        private double winProbability2;
        private Move[] statePossibleMoves;
        private bool player1Turn;                                                                       
        private BST<int[], GameState> stateTree;
        private Move bestMove;
        private int winCount;
        private int lossCount;
        private int gameCount;
        public TicTacToeGameState(int[] id, Move[] possibleMoves, bool isPlayer1Turn,  double winProb1, double winProb2, ref BST<TicTacToeGameState> tree)
        {
            stateId = id;
            statePossibleMoves = possibleMoves;
            player1Turn = isPlayer1Turn;
            winProbability1 = (double)winProb1;
            winProbability2 = (double)winProb2;
            stateTree = tree;
            bestMove = null;
            winCount = 0;
            lossCount = 0;
            gameCount = 0;
        }
        public TicTacToeGameState (string stateString, ref BST<GameState> tree)
        {
            int position = 0;
            int idLength = GetIntFromString(stateString, ref position, 2);
            int numDigits;
            for (int i = 0; i < idLength; i++)
            {
                numDigits = GetIntFromString(stateString, ref position, 2);
                if (idLength > 1)
                    stateId[i] = GetIntFromString(stateString, ref position, numDigits);
            }

            stateTree = tree;
            
            int playerTurn = GetIntFromString(stateString, ref position, 1);
            player1Turn = playerTurn == 1 ? true : false;
            numDigits = GetIntFromString(stateString, ref position, 1);
            winProbability1 = GetDoubleFromString(stateString, ref position, numDigits);
            numDigits = GetIntFromString(stateString, ref position, 1);
            winProbability2 = GetDoubleFromString(stateString, ref position, numDigits);
            numDigits = GetIntFromString(stateString, ref position, 2);
            winCount = GetIntFromString(stateString, ref position, numDigits);
            numDigits = GetIntFromString(stateString, ref position, 2);
            lossCount = GetIntFromString(stateString, ref position, numDigits);
            numDigits = GetIntFromString(stateString, ref position, 2);
            gameCount = GetIntFromString(stateString, ref position, numDigits);
            if (position >= (stateString.Length - 1))
            {
                bestMove = null;
                statePossibleMoves = new Move[0];
            }
            else
            {
                bestMove = new TicTacToeMove(stateString.Substring(position, 5));
                position += 5;
                int count = (stateString.Length - position) / 5;
                statePossibleMoves = new Move[count];
                for (int i = 0; i < count; i++)
                {
                    statePossibleMoves[i] = new TicTacToeMove(stateString.Substring(position, 5));
                    position += 5;
                }
            }

        }
        private int GetIntFromString(string inputString, ref int position, int length)
        {
            int numToReturn = int.Parse(inputString.Substring(position, length));
            position += length;
            return numToReturn;
        }
        private double GetDoubleFromString(string inputString, ref int position, int length)
        {
            double numToReturn = double.Parse(inputString.Substring(position, length));
            position += length;
            return numToReturn;
        }
        public override string ToString()
        { 
            string myString = "";
            string playerTurn = "";
            while (true)
            {
                try
                {
                    playerTurn = player1Turn ? "1" : "2";
                    myString = NumDigits(stateId).ToString("00");
                    myString += stateId.ToString();
                    myString += playerTurn;
                    myString += winProbability1 >= 0 ? "6" : "7";
                    myString += winProbability1.ToString("000.00");
                    myString += winProbability2 >= 0 ? "6" : "7";
                    myString += winProbability2.ToString("000.00");
                    myString += NumDigits(winCount).ToString("00");
                    myString += winCount.ToString();
                    myString += NumDigits(lossCount).ToString("00");
                    myString += lossCount.ToString();
                    myString += NumDigits(gameCount).ToString("00");
                    myString += gameCount.ToString(); 
                    if (statePossibleMoves.Length == 0)
                        return myString;
                    Move bMove = BestMove;
                    myString += bMove.ToString();
                    foreach (Move move in statePossibleMoves)
                        myString += move.ToString();

                    return myString;
                }
                catch (System.NullReferenceException)
                {
                    Console.WriteLine("oops");
                    Console.WriteLine(myString.Length);
                }
            }
            

        }
        public int playerNum
        {
            get
            {
                return player1Turn ? 1 : 2;
            }
        }
        public int CalculateChildId(int tileNum)
        {

            return stateId + playerNum * (int)Math.Pow(3, tileNum);
        }

        public void setWinProbabilities()
        {
            
            if (gameCount < 1000 && !IsOver)
            {
                int count = 0;
                int tempKey;
                BSTNode<GameState> tempNode;
                winProbability1 = 0;
                winProbability2 = 0;
                foreach (Move move in statePossibleMoves)
                {
                    tempKey = CalculateChildId(move.DestinationTile.TileNum);
                    tempNode = stateTree.Find(tempKey);
                    double tempWinProbability1 = tempNode.Value.WinProbability1;
                    double tempWinProbability2 = tempNode.Value.winProbability2;
                    winProbability1 += tempWinProbability1;
                    winProbability2 += tempWinProbability2;
                    count++;
                }
                winProbability1 /= count;
                winProbability2 /= count;
                if (stateTree.Count == 2706)
                    setBestMove();
            }
            else
            {
                if (player1Turn)
                {
                    if (winCount > 0)
                        winProbability1 = (winCount-lossCount)*100 / (winCount + lossCount);
                    else
                        winProbability1 = 0;
                }
                else
                {
                    if (lossCount > 0)
                        winProbability1 = (lossCount-winCount)*100 / (winCount + lossCount);
                    else
                        winProbability1 = 0;
                }
                if (winProbability1 > 0)
                    winProbability2 = -winProbability1;
                if (stateTree.Count == 2706)
                    setBestMove();
            }
            
        }
        public double WinProbability1
        {
            get
            {
                if (winProbability1 < -100)
                    setWinProbabilities();
                return winProbability1;
            }
            set
            {
                winProbability1 = value;
            }
        }
        public double WinProbability2
        {
            get
            {
                if (winProbability2 < -100)
                    setWinProbabilities();
                return winProbability2;
            }
            set
            {
                winProbability2 = value;
            }
        }
        public Move BestMove
        {
            get
            {
                if (bestMove == null)
                    setBestMove();
                return bestMove;
            }
        }
        public void setBestMove()
        {
            if (!IsOver)
            {
                
                bestMove = statePossibleMoves[0];
                if (statePossibleMoves.Length != 1)
                {
                    int tempId;
                    double tempWinProb;
                    double maxWinProb = -101;
                    foreach (Move currentMove in statePossibleMoves)
                    {
                            tempId = CalculateChildId(currentMove.DestinationTile.TileNum);
                        BSTNode<GameState> tempNode = stateTree.Find(tempId);
                            tempWinProb = player1Turn ? tempNode.Value.WinProbability1 : tempNode.Value.WinProbability2;
                            if (tempWinProb > maxWinProb)
                            {
                                maxWinProb = tempWinProb;
                                bestMove = currentMove;
                            }
                        
                    }
                }
            }
            else
                bestMove = null;

        }
        public void AddGame(int outcome)
        {
            if (outcome == -1)
                lossCount++;
            else if (outcome == 1)
                winCount++;
            gameCount++;
            if (gameCount > 1000)
                setWinProbabilities();
        }

        public int GameCount
        {
            get
            {
                return gameCount;
            }
        }

        public int NumDigits(int num)
        {
            int numDigits = 1;
            for (; numDigits < 10000 && num >= Math.Pow(10, numDigits); numDigits++) ;
            return numDigits;
        }
        public int Id
        {
            get
            {
                return stateId;
            }
        }
        
        public bool IsOver
        {
            get
            {
                return (statePossibleMoves.Length == 0);
            }
        }
    }
        
}
