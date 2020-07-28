using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TicTacToe_Simulation
{
    public class BST<TKey, TValue, TComp>
    {
        private BSTNode<TKey, TValue> head;
        private int count;
        public static BSTNode<TKey, TValue> nullNode = new BSTNode<TKey, TValue>();

        /*
         * RemoveHelp() is a recursive function that searches the proper subtree for each node until either finding a node with 
         * the correct key or finding a null value, indicating that such a node is not in the tree. If null is found, null is returned
         * so that no change is made in the tree structure. If the value is found, it is logically deleted and replaced by the
         * node with the minimum key from its right subtree. Reference parameter deleted stores the node that is removed (or null if none).
         */
        private BSTNode<TKey, TValue> RemoveHelp(BSTNode<TKey, TValue> rt, int key, ref BSTNode<TKey, TValue> deleted)
        {
            if (rt == null)
            {
                deleted = null; //Not in the tree, nothing is deleted
                return null; //Empty subtree
            }
            else if (key < rt.Key)
                rt.Left = RemoveHelp(rt.Left, key, ref deleted);
            else if (key > rt.Key)
                rt.Right = RemoveHelp(rt.Right, key, ref deleted);
            else //Found: rt.Key == key
            {
                BSTNode<TKey, TValue> temp = DeleteMin(rt.Right);
                deleted = rt;
                rt.Key = temp.Key;
                rt.Value = temp.Value;
            }
            return rt;
        }
        /*
         *InsertHelp() is a recursive function that chooses the proper subtree for each node it checks, reassigning the pointer 
         * for that subtree. Once a null value is found, indicating the bottom of the tree has been found, the node to insert is 
         * returned, causing it to be assigned as its new parent's proper child. The only node that will have a new/different child
         * is the node under which the new node is inserted.
         */
        private BSTNode<TKey, TValue> InsertHelp(BSTNode<TKey, TValue> node, BSTNode<TKey, TValue> rt)
        {
            if (rt == null) //Nothing here, insert node
            {
                count++;
                return node;
            }
            if (node.Key < rt.Key)
            {
                rt.setLeft(InsertHelp(node, rt.Left)); //Travel down left subtree
            }
            else
            {
                rt.setRight(InsertHelp(node, rt.Right)); //Travel down right subtree
            }
            return rt;
        }
        /*
         * FindHelp() is a recursive function that searches the proper subtree of each node until finding the node 
         * with the key value that it is looking for. This node is then returned up the function call chain.
         * If the node is not found, null is returned
         */
        private ref BSTNode<TKey, TValue> FindHelp(int key, ref BSTNode<TKey, TValue> rt)
        {
            if (rt == null)
                return ref nullNode; //Not in tree, return null
            if (key < rt.Key)
                return ref FindHelp(key, ref rt.Left);
            else if (key > rt.Key)
                return ref FindHelp(key, ref rt.Right);
            else //Found: node.Key == rt.Key
                return ref rt;
        }
        public BST()
        {
            head = new BSTNode<TKey, TValue>();
            count = 0;
        }
        /*
         * Function Insert() inserts the given node at its proper position within the tree
         */
        public void Insert(BSTNode<TKey, TValue> node)
        {
            head.setRight(InsertHelp(node, head.Right));
        }
        /*
         * Remove() removes the node with the given key from the tree if it is found. If not found, nothing is deleted and null is returned
         */
        public BSTNode<TKey, TValue> Remove(int key)
        {
            BSTNode<TKey, TValue> deleted = null;
            RemoveHelp(head.Right, key, ref deleted);
            return deleted;
        }

        public int Count
        {
            get
            {
                return count;
            }
        }
        /*
         * getMin() returns the node with the minimum key in the tree with root rt.
         */
        public BSTNode<TKey, TValue> GetMin(BSTNode<TKey, TValue> rt)
        {
            while (rt.Left != null)
            {
                rt = rt.Left;
            }
            return rt;
        }

        /*
         *deleteMin() removes and returns the node with the minimum key in the tree with root rt
         */
        public BSTNode<TKey, TValue> DeleteMin(BSTNode<TKey, TValue> rt)
        {
            if (rt == null)
            {
                return null;
            }
            BSTNode<TKey, TValue> temp;
            if (rt.Left == null) //If root is the minimum value
            {
                temp = rt; //Store the node so that we can return it later
                rt = rt.Right; //Logically remove the node. If there is no subtree, the root can be replaced by the root of its right subtree
            }
            else
            {
                while (rt.Left.Left != null) //Keep going until rt.Left is the minimum value
                {
                    rt = rt.Left; //Travel down the left subtree
                }
                temp = rt.Left; //Store the node so that we can return it
                rt.setLeft(rt.Left.Right); //Logically delete the node
            }
            count--;
            return temp; //Return the minimum value
        }

        /*
         * Find() returns the node whose key value equals to input parameter
         */
        public  ref BSTNode<TKey, TValue> Find(int key)
        {
            return ref FindHelp(key, ref head.Right);
        }

        public void OutputTree(String filename)
        {
            FileStream fileStream = new FileStream(filename, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine(count);
            PreorderPrint(head.Right, streamWriter);
            fileStream.Close();
        }
        public int OutputNode(StreamWriter myWriter, BSTNode<TKey, TValue> currentNode)
        {
            myWriter.WriteLine(currentNode.Key);
            myWriter.WriteLine(currentNode.Value);
            return -1;
        }


        public void PreorderPrint(BSTNode<TKey, TValue> root, StreamWriter myStreamWriter)
        {
            if (root != null)
            {
                OutputNode(myStreamWriter, root);
                PreorderPrint(root.Left, myStreamWriter);
                PreorderPrint(root.Right, myStreamWriter);
            }
        }
        
        public ref BSTNode<TKey, TValue> NullNode
        {
            get
            {
                return ref nullNode;
            }
        }
    }
}

