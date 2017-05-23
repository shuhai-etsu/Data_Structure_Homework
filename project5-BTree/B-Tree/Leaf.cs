//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 5 - B-Trees
//	File Name:		Leaf.cs
//	Description:	The Leaf is the node at the bottom of the B-Tree containing the data
//	Course:			CSCI 2210-201 - Data Structures
//	Author:			Caleb Ignace & Shuhai Li; ignacec@goldmail.etsu.edu  & lis002@goldmail.etsu.edu
//	Created:		Friday, November 21, 2015
//	Copyright:		Caleb Ignace & Shuhai Li, 2015
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
    /// The Leaf is the node at the bottom of the B-Tree containing the data
    /// </summary>

    class Leaf : Node
    {
        /// <summary>
        /// Base constructor
        /// </summary>

        public Leaf() : base()
        {
            Type = "Leaf";
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="nodeSize">int</param>

        public Leaf(int nodeSize) : base(nodeSize)
        {
            Type = "Leaf";
        }

        /// <summary>
        /// Insert n into this leaf if possible
        /// </summary>
        /// <param name="n">An integer to add at this node</param>
        /// <returns>INSERT -- either we need to split, n is a duplicate, 
        /// or we add n to the list Value and sort</returns>

        public INSERT Insert(int n)
        {
            // If leaf is full, we need to split
            if (NodeSize == Value.Count)
                return INSERT.NEEDSPLIT;

            // If the integer n already exists in leaf, then it is a duplicate
            if (Value.Contains(n))
                return INSERT.DUPLICATE;

            // We can now add the integer n to the list of integers, Value
            Value.Add(n);
            Value.Sort();
            return INSERT.SUCCESS;
        }


        /// <summary>
        /// Returns a string that represents this instance.
        /// </summary>
        /// <returns>
        /// A string that represents this instance.
        /// </returns>
        
        public override string ToString()
        {
            fullness = (int)Value.Count * 100 / (NodeSize);
            string message = "Node Type         : " + Type +
                           "\nNumber of Values  : " + Value.Count +
                           "     Fullness of node: " + fullness + "%" +
                           "\nValues:  ";

            foreach (int i in Value)
                message += i + " ";

            return message + "\n";
        }
    }
}

