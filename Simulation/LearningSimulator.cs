using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe_Simulation;
using TicTacToeLibrary;
using System.IO;

namespace TicTacToe_Simulation
{
    public class LearningSimulator
    {
        //private BST<GameState> initialTree;
        private BST<GameState> finalTree;
        private uint iterationCount;
        private uint threshold;



        public LearningSimulator (uint thresh = 1000000)
        {
            //initialTree = initTree;
            finalTree = new BST<GameState>();
            iterationCount = 0;
            threshold = thresh;
        }

        public void RunSimulation(uint iterations)
        {
            int currentCount = 0;
            while ((currentCount < iterations && iterationCount < threshold))
            {
                MakeMoveRandom(TicTacToeGame.Start());
                currentCount++;
                iterationCount++;
                if (currentCount % 100000 == 0)
                    Console.WriteLine(currentCount);
            }
            while (currentCount < iterations)
            {
                MakeMoveGuided(TicTacToeGame.Start());
                currentCount++;
                iterationCount++;
                if (currentCount % 100000 == 0)
                    Console.WriteLine(currentCount);
            }

        }

        public int MakeMoveRandom(Game currentGame)
        {
            Move nextMove;
            int outcome;
            BSTNode<GameState> currentNode = finalTree.Find(currentGame.GetStateId());
            GameState currentState;
            if (currentNode == finalTree.NullNode)
            {
                currentState = new GameState(currentGame.GetStateId(), currentGame.FindPossibleMoves(), currentGame.IsPlayer1Turn(), -101, -101, ref finalTree);
                currentNode = new BSTNode<GameState>(currentState.Id, currentState);
                finalTree.Insert(currentNode);
            }
            else
                currentState = currentNode.Value;
            nextMove = FindRandomMove(currentGame);
            currentGame.MakeMove(nextMove);
            

            if (currentGame.CheckForWin())
            {
                outcome = -1;
                currentState.WinProbability1 = currentGame.IsPlayer1Turn() ? -1 : 1;
                currentState.WinProbability2 = -1 * currentState.WinProbability1;
            }
            else if (currentGame.CheckForDraw())
            {
                outcome = 0;
                currentState.WinProbability1 = currentGame.IsPlayer1Turn() ? -1 : 1;
                currentState.WinProbability2 = currentState.WinProbability1;
            }
            else
                outcome = -1 * MakeMoveRandom(currentGame);
            currentState.AddGame(outcome);
            return outcome;

        }

        public int MakeMoveGuided(Game currentGame)
        {
            int currentId = currentGame.GetStateId();
            BSTNode<GameState> currentNode = finalTree.Find(currentId);
            Move nextMove;
            int outcome;
            GameState currentState = null;
            if (currentNode == finalTree.NullNode) //If new state
            {
                nextMove = FindRandomMove(currentGame);
                currentState = new GameState(currentId, currentGame.FindPossibleMoves(), currentGame.IsPlayer1Turn(), -101, -101, ref finalTree);
                currentNode = new BSTNode<GameState>(currentState.Id, currentState);
                finalTree.Insert(currentNode);
            }
            else
            {
                currentState = currentNode.Value;
                nextMove = currentState.BestMove;
            }
            if(nextMove == null)
            {
                return currentGame.CheckForWin()? 1 : 0;
            }
            currentGame.MakeMove(nextMove);
            if (currentGame.CheckForWin())
                outcome = -1;
            else if (currentGame.CheckForDraw())
                outcome = 0;
            else
            {
                outcome = -1 * MakeMoveGuided(currentGame);                
            }

            currentState.AddGame(outcome);
            currentState.setWinProbabilities();

            return outcome;
        }
        public Move FindRandomMove(Game currentGame)
        {
            Move[] possibleMoves = currentGame.FindPossibleMoves();
            int count = 0;
            foreach (Move currentMove in possibleMoves)
            {
                count++;
            }
            Random myRandomGenerator = new Random();
            int index = myRandomGenerator.Next(0, count);
            return possibleMoves[index];
        }

        public BST<GameState> StateTree
        {
            get
            {
                return finalTree;
            }
        }

        public static BST<GameState> InputStateTree(string filename)
        {
            BST<GameState> tree = new BST<GameState>();
            StreamReader streamReader = new StreamReader(filename);
            int count = int.Parse(streamReader.ReadLine());
            bool success = true;
            for (int i = 0; i < count && success; i++)
                success = InputNode(streamReader, ref tree);
            return tree;
        }
        private static bool  InputNode(StreamReader streamReader, ref BST<GameState> tree)
        {
            string nodeKeyString = streamReader.ReadLine();
            string nodeValueString = streamReader.ReadLine();
            if (nodeValueString == null)
                return false;
            if (nodeValueString.Length >= 27)
            {

                int nodeKey = int.Parse(nodeKeyString);

                GameState nodeValue = new GameState(nodeValueString, ref tree);
                BSTNode<GameState> node = new BSTNode<GameState>(nodeKey, nodeValue);
                tree.Insert(node);
            }
            return true;
        }
    }
}
