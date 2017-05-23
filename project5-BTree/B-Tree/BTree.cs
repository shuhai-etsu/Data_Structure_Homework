//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Project: Project 5 - B-Trees
// File Name: BTree.cs
// Description: Definition of BTree class
// Course: CSCI 2210-201 - Data Structures
// Author: Caleb Ignace & Shuhai Li; ignacec@goldmail.etsu.edu & lis002@goldmail.etsu.edu
// Created: Friday, November 21, 2015
// Copyright: Caleb Ignace & Shuhai Li, 2015
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace B_Tree
{
    /// <summary>
    /// BTree is a tree comprized of Index nodes and Leaf nodes
    /// </summary>
    
    class BTree
    {
        public int ValueCount { get; set; } // Total number of values in the tree
        public int IndexCount { get; set; } // Total number of index nodes
        public int LeafCount { get; set; }  // Total number of leaf nodes
        public int NodeSize { get; set; }   // Size of a Leaf node, Index nodes have size NodeSize - 1
        public Index Root { get; set; }     // Root of BTree
        public Stack<Node> StackN { get; set; } // Stack of Nodes -- used when splitting Leaves and Indexes

        /// <summary>
        /// Initializes a new instance of the <see cref="BTree"/> class.
        /// </summary>
        /// <param name="n">The size of a node</param>
        
        public BTree(int n)
        {
            NodeSize = n;  
            ValueCount = 0; // No Values
            LeafCount = 0;  // No Leafs

            Root = new Index(NodeSize - 1);
            IndexCount = 1;

            StackN = new Stack<Node>();
        }


        /// <summary>
        /// Let a class outside the BTree enter a value
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Boolean -- value has been added (true)</returns>

        public bool AddValue(int value)
        {
            StackN.Clear();

            // index of the Indexes in Root that we should go to next
            int indexInsert = Root.Insert(value);

            if (indexInsert == -2) // Duplicate
            {
                return false;
            }
            else if (indexInsert == -1) // No values in Root, so no Leaf(s) || there is one value in Root and 'value' is less than it
            {
                Root.Indexes.Insert(0, new Leaf(NodeSize));
                LeafCount++;
                indexInsert = 0;
            }
            else
            {
                // The result is between 0 and (Indexes.Count - 1) -- Indicating which Node to go to next from Root
            }

            StackN.Push(Root); // We have visited the Root

            return AddValue(value, Root.Indexes[indexInsert], Root);
        }

        /// <summary>
        /// Recusion add value method -- conditioned to find the correct Leaf to insert value in
        /// </summary>
        /// <param name="value">Int -- The value.</param>
        /// <param name="node">Node -- the next node on the way to the leaf to insert value in</param>
        /// <param name="parent">Node -- the parent node of the current node</param>
        /// <returns>Boolean -- value has been added (true)</returns>

        private bool AddValue(int value, Node node, Index parent)
        {
            bool valueAdded = false;

            INSERT leafInsert; // Can I insert this value into this Leaf?
            int indexInsert;   // Is there a Leaf under this Index that could accept this value?
            
            if (node is Leaf)
            {
                // If the node is a Leaf, determine if we can insert the value into it

                #region Leaf case

                Leaf leaf = (Leaf)node;

                leafInsert = leaf.Insert(value);

                if (leafInsert == INSERT.DUPLICATE)
                {
                    valueAdded = false;
                }
                else if (leafInsert == INSERT.SUCCESS)
                {
                    ValueCount++;

                    // Add needed values to the Indexes that we passed along the way
                    parent.UpdateIndexValues();

                    valueAdded = true;
                }
                else //result1 == INSERT.NEEDSPLIT
                {
                    StackN.Push(leaf);

                    Index topIndexEffected = Split(value);

                    topIndexEffected.Sort();
                    topIndexEffected.UpdateIndexValues();

                    valueAdded = true;

                    ValueCount++;
                }

                #endregion
            }
            else 
            {
                // It is an Index, determine the index of the child node to go to next

                #region Index case

                Index index = (Index)node;

                indexInsert = index.Insert(value);

                if (indexInsert == -2) // Duplicate
                {
                    valueAdded = false;
                }
                else // The result is between 0 and (Indexes.Count - 1) -- Indicating which Node to go to next from Root
                {
                    if (indexInsert == 0 && index.Indexes.Count == 1)
                    {
                        index.Indexes.Insert(0, new Leaf(NodeSize));
                        LeafCount++;
                    }

                    StackN.Push(index);

                    valueAdded = AddValue(value, index.Indexes[indexInsert], index);
                }

                #endregion
            }

            return valueAdded;
        }


        /// <summary>
        /// Splits the Leaf where we want to add value and possible Indexes above it 
        /// until we come to an Index that will accept another child Node or the Root
        /// </summary>
        /// <param name="value">Int -- the value to be added.</param>
        /// <param name="nodes">List of Nodes -- the halfs</param>
        /// <returns>The top-most Index effected by the split</returns>
        
        private Index Split(int value, List<Node> nodes = null)
        {

            #region Pop stack, split node into two

            Node node = StackN.Pop(); // The current node; the node to split

            Node newNode1;  // The new left node
            Node newNode2;  // The new right node; where value will go

            int numberOfIndexesToBeGiven;

            if (node is Leaf)
            {
                newNode1 = new Leaf(NodeSize);
                newNode2 = new Leaf(NodeSize);

                // Sort the values
                List<int> values = node.Value;
                values.Add(value);
                values.Sort();

                // I want to make sure the left side has at least half, hence the "NodeSize % 2"
                int numberOfValuesToBeGiven = values.Count / 2;

                for (int i = 0; i < values.Count; i++)
                {
                    if (i < numberOfValuesToBeGiven)
                        newNode1.Value.Add(values[i]);
                    else
                        newNode2.Value.Add(values[i]);
                }
                


                LeafCount += 2;

                //Both new nodes should already be sorted
            }
            else
            {
                Index nodeAsIndex = (Index)node;

                newNode1 = new Index(NodeSize - 1);
                newNode2 = new Index(NodeSize - 1);

                // Sort the Indexes of nodeAsIndex, in addition to 
                //  the new half nodes
                Index tempNode = new Index(NodeSize + 1);
                tempNode.Indexes.Add(nodes[0]);
                tempNode.Indexes.Add(nodes[1]);

                for (int i = 0; i < nodeAsIndex.Indexes.Count; i++)
                {
                    tempNode.Indexes.Add(nodeAsIndex.Indexes[i]);
                }

                tempNode.Sort();

                // Give newNode1 and newNode2 approximately half the Indexes
                numberOfIndexesToBeGiven = tempNode.Indexes.Count / 2;

                for (int i = 0; i < tempNode.Indexes.Count; i++)
                {
                    if (i < numberOfIndexesToBeGiven)
                        ((Index)newNode1).Indexes.Add(tempNode.Indexes[i]);
                    else
                        ((Index)newNode2).Indexes.Add(tempNode.Indexes[i]);
                }

                IndexCount += 2;
            }

            #endregion

            #region Delete old Node; define index at all scenarios

            Index index;

            if (node is Leaf) // For the first iteration
            {
                // There is at least one more Index, the Root
                index = (Index)StackN.Peek();

                // Remove the Leaf node
                index.Indexes.Remove((Leaf)node);

                LeafCount--;

            }
            else if (node is Index && StackN.Count != 0) // Every iteration inbetween
            {
                index = (Index)StackN.Peek();

                // Remove the Index node
                index.Indexes.Remove((Index)node);

                IndexCount--;
            }
            else // Last iteration
            {
                // index is the Root node
                index = (Index)node;
            }

            #endregion

            #region Recursion, reached desired node

            // Find a Node that doesn't need to split || find the root
            if (index.Value.Count < index.NodeSize)
            {
                index.Indexes.Add(newNode1);
                index.Indexes.Add(newNode2);

                return index;
            }
            else if (StackN.Count == 0)
            {
                // The Index node is the Root
                // Create a new Root
                Root = new Index(NodeSize-1);

                // Add the two new Nodes
                Root.Indexes.Add(newNode1);
                Root.Indexes.Add(newNode2);

                return Root;
            }
            else
            {
                // REcusion to find an Index that will allow for a new child or
                //  the Root
                nodes = new List<Node>();
                nodes.Add(newNode1);
                nodes.Add(newNode2);

                return Split(value, nodes);
            }

            #endregion

        }

        /// <summary>
        /// Display this instance -- this displays the whole Tree
        /// </summary>
        
        public void Display()
        {
            Console.WriteLine("\nRoot: ");

            Display(Root, FindDepth());

            DispalyStats();
        }

        /// <summary>
        /// Displays the specified node as the Root of a subtree
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="depthOfTree">The depth of tree.</param>
        
        public void Display(Node node, int depthOfTree)
        {
            // Display the node
            Console.WriteLine("\n"+node.ToString());

            if (node is Index)
            {
                Console.Write("Depth of Index in BTree: " + (depthOfTree - FindDepth(node) + "\n"));

                Index index = ((Index)node);

                // Display all the nodes one level under index
                for (int i = 0; i < index.Indexes.Count; i++)
                    Display(index.Indexes[i], depthOfTree);
            }

            // Stops at the Leaves
        }

        /// <summary>
        /// Find the leaf accoring to a specific value
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="node">The starting node</param>
        /// <returns></returns>

        public Leaf FindLeaf(int value, Node node = null)
        {
            if (node == null)
            {
                node = Root;

                Console.WriteLine("\nRoot: \n");
            }

            Console.WriteLine(node.ToString() + "\n");

            // If Index, find the next node to compare value to
            if(node is Index)
            {
                Index index = (Index)node;

                int position = -1;

                if (index.Value.Count == 0)
                {
                    // Do nothing
                }
                else if (value < index.Value[0])
                {
                    position = 0;
                }
                else if(value >= index.Value[index.Value.Count - 1])
                {
                    position = index.Indexes.Count - 1;
                }
                else
                {
                    for (int i = 0; i < (index.Value.Count - 1); i++)
                        if (value >= index.Value[i] && value < index.Value[i + 1])
                            position = i + 1;
                }

                if (position == -1)
                    return null;
                else
                    return FindLeaf(value, index.Indexes[position]);
            }
            else // node is Leaf
            {
                return (Leaf)node;
            }
            
        }

        /// <summary>
        /// Finds the depth of the subtree stating at node as root
        /// </summary>
        /// <param name="node">The Root of the subtree we want to find the depth of</param>
        /// <param name="depth">The depth.</param>
        /// <returns></returns>

        public int FindDepth(Node node = null, int depth = 0)
        {
            // No depth
            if (ValueCount == 0)
                return 0;

            if (node == null)
                node = Root;

            // Add one to the depth any time we find an Index
            if (node is Index)
            {
                depth++;
                return FindDepth(((Index)node).Indexes[0], depth);
            }
            else
                return depth;
        }

        /// <summary>
        /// This method search a value in a B-Tree. Ruturn true if found; false if not found 
        /// </summary>
        /// <param name="value">The value to search for </param>
        public bool FindValue(int value)
        {
            return FindValue(Root, value);  //call FindValue(Index node, int nValue) using Root node as the parameter
        }

        /// <summary>
        /// This method search a value in a B-Tree. Ruturn true if found; false if not found 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        public bool FindValue(Node node, int value)
        {
            if (node == null)
                node = Root;

            Leaf leaf = FindLeaf(value);

            if (leaf == null)
                return false;
            else
                return leaf.Value.Contains(value);
        }

        /// <summary>
        /// This method finds the index (position) of child node of an Node where further search should be routed in a B-Tree 
        /// 	It is done by comparing the search value with each of values in the node
        /// </summary>
        /// <param name="node">The node where search is taking place currently </param>
        /// <param name="nValue">The value to search for in the B-Tree</param>
        public int FindChildNodePosition(Index node, int nValue)
        {
            int position = 0;
            if (nValue < node.Value[0])
            {
                position = 0;         //if nValue is smaller than the first value, the position is 0
            }
            for (int i = 0; i < node.Value.Count - 1; i++)
            {
                if (nValue >= node.Value[i] && nValue < node.Value[i + 1])
                {
                    position = i + 1;   //if nValue is greater than (i+1)th value but less than (i+2)th value, the 
                }                       //position is i+1
            }
            if (nValue >= node.Value[node.Value.Count - 1])
                position = node.Value.Count;  //if nValue is greater than the last value in the node, position is the Count of the node

            return position;
        }

        /// <summary>
        /// Determines if nValue is in leaf node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="nValue">Int -- value.</param>
        /// <returns></returns>
        public bool FindInLeafNode(Node node, int nValue)
        {
            bool isFound;
            isFound = node.Value.Contains(nValue);
            return isFound;
        }

        /// <summary>
        /// Dispalies the stats.
        /// </summary>

        public void DispalyStats()
        {
            // Statistics
            int depth = FindDepth();
            double averageLeafFullness = (double)ValueCount/(NodeSize * LeafCount); 
            Console.WriteLine ("Number of Index nodes is : " + IndexCount +
            "\nNumber of Leaf nodes is : " + LeafCount +
            " and they're average " + String.Format("fullness is: {0:P2}.", averageLeafFullness) + " full" +
            "\nThe depth of the tree is : " + depth +
            "\n    with " + depth + " levels of Index nodes and 1 level of Leaf nodes" +
            "\n\nThe total number of values in the tree is " + ValueCount);
        }
    }
}