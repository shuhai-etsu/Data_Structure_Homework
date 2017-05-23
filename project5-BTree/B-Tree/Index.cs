//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Project: Project 5 - B-Trees
// File Name: Index.cs
// Description: The Index Node points to Nodes below itself
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
    /// A special type of Node -- this points to other nodes
    /// </summary>

    class Index : Node
    {
        public List<Node> Indexes { get; set; } // A list of Nodes that the Index points to

        /// <summary>
        /// Initializes a new instance of the Index class.
        /// </summary>
        
        public Index() : base()
        {
            Type = "Index";
            Indexes = new List<Node>();
        }
        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="nodeSize">The size of the Index</param>
        public Index(int nodeSize) : base(nodeSize)
        {
            Type = "Index";
            Indexes = new List<Node>();
        }

        /// <summary>
        /// See if a number can be inserted into a leave below this Index
        /// </summary>
        /// <param name = "value" >Int -- the value to be inserted</ param >
        /// < returns >
        /// The index of the Indexes list where there is a Leaf that may accept value
        /// </returns>
        public int Insert(int value)
        {
            if (Value.Contains(value)) // Duplicate value
            {
                return -2;
            }
            else if(Value.Count == 0 || (value < Value[0] && Indexes.Count == 1))
            {
                // If there are no values in this index, there are no leaves || 
                // there is only one Indexi and the value is smaller than all those in it
                return -1; 
            }
            else
            {
                // value is less than the first Value
                if (value < Value[0])
                {
                    return 0;
                }
                else if (value > Value[Value.Count - 1])
                {
                    // value is greater than the last Value
                    return (Indexes.Count - 1);
                }
                else
                {
                    // value is between two Values
                    for (int i = 0; i < (Value.Count - 1); i++)
                    {
                        if (value > Value[i] && value < Value[i + 1])
                        {
                            return (i + 1);
                        }
                    }
                }

                //Duplicate, just to satisfy syntax..
                return -2;
            }
        }

        /// <summary>
        /// Sorts the specified index by the first Value in the list of Indexes
        /// </summary>
        /// <param name="index">The Index to sort</param>
        
        public void Sort(Index index = null)
        {
            // In order to sort Indexes properly, we need to sort from the Leaves up

            if (index == null)
                index = this;

            int j;              // Iteration variable
            Node tempNode;      
            
            for (int i = 1; i < index.Indexes.Count; i++)
            {
                Node nextNode = index.Indexes[i]; // One of index's Indexes

                if (nextNode is Index) // Sort the Index
                {
                    Sort(index = (Index)nextNode);
                }
                else    // Sort the Leaf -- the algorithm does this first
                {
                    tempNode = nextNode;

                    for (j = i; j > 0 && tempNode.Value[0] < index.Indexes[j - 1].Value[0]; j--)
                        index.Indexes[j] = index.Indexes[j - 1];
             
                    // This is where the sorting of the index comes into play
                    index.Indexes[j] = tempNode;
                }
            }
        }

        /// <summary>
        /// Updates the index values of a subtree of this Index
        /// </summary>
        /// <param name="index">Index -- node to update</param>

        public void UpdateIndexValues(Index index = null)
        {
            if (index == null)
                index = this;
           
            index.Value.Clear();

            int min;            // The value I will set i to be creater than
            int smallestValue;  // The smallest value of this subtree

            if (index.Indexes.Count == 1)
                min = -1;
            else
                min = 0;

            // Find the smallest value of every subtree of index that is not the first subtree, unless it is the only one
            for (int i = index.Indexes.Count - 1; i > min; i--)
            {
                smallestValue = FindSmallestValueInSubtree(index.Indexes[i]);

                index.Value.Add(smallestValue);
            }

            index.Value.Sort();

            for (int i = 0; i < index.Indexes.Count; i++)
            {
                // Use recursion to go to lower indexes
                if (index.Indexes[i] is Index)
                    UpdateIndexValues((Index)index.Indexes[i]);
                else
                    return;
            }
        }


        /// <summary>
        /// Finds the smallest value in any given subtree where the starting Node is node
        /// </summary>
        /// <param name="node">Node -- root of subtree</param>
        /// <returns>Int -- the smallest value</returns>
        
        private int FindSmallestValueInSubtree(Node node)
        {
            if (node is Index) // Locate the first Leaf
            {
                return FindSmallestValueInSubtree(((Index)node).Indexes[0]);
            }
            else // Node is a leaf, return the smallest value
            {
                return node.Value[0];
            }
        }


        /// <summary>
        /// Returns a string that represents this instance.
        /// </summary>
        /// <returns>
        /// returns a string
        /// </returns>
        
        public override string ToString()
        {
            fullness = (int)Value.Count * 100 / (NodeSize);
            string message = "Node Type         : " + Type +
                           "\nNumber of Indexes : " + Indexes.Count +
                           "\nNumber of Values  : " + Value.Count +
                           "     Fullness of node: " + fullness + "%" +
                           "\nValues:  ";

            foreach (int i in Value)
                message += i + " ";

            return message;
        }
    }
}