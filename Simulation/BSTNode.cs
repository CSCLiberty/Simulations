using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TicTacToe_Simulation
{
     public class BSTNode <TKey, TValue>
    {
        private int nodeKey;
        private TValue nodeValue;
        private BSTNode<TKey, TValue> leftChild;
        private BSTNode<TKey, TValue> rightChild;

        public BSTNode(int key, TValue val, BSTNode<TKey, TValue> left = null, BSTNode<TKey, TValue> right = null)
        {
            nodeKey = key;
            nodeValue = val;
            leftChild = left;
            rightChild = right;
        }

        public BSTNode()
        {
            nodeKey = 0;
            leftChild = rightChild = null;
        }

        public int Key
        {
            get
            {
                return nodeKey;
            }
            set
            {
                nodeKey = value;
            }
        }

        public ref TValue Value
        {
            get
            {
                return ref nodeValue;
            }
        }
        public void setValue (TValue newVal)
        {
                nodeValue = newVal;
        }
        

        public ref BSTNode<TKey, TValue> Left
        {
            get
            {
                return ref leftChild;
            }
        }
        public void setLeft(BSTNode<TKey, TValue> left)
        {
            leftChild = left;
        }

        public ref BSTNode<TKey, TValue> Right
        {
            get
            {
                return ref rightChild;
            }
        }
        public void setRight(BSTNode<TKey, TValue> right)
        {
            rightChild = right;
        }
        public override string ToString()
        {
            return nodeKey.ToString("000000000000") + nodeValue.ToString();
            
        }

    }
}
