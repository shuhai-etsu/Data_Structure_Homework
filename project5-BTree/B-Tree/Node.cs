//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 5 - B-Trees
//	File Name:		Node.cs
//	Description:	The Node is the building block of the B-Tree
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
    /// The Node is the building block of the B-Tree
    /// </summary>

    class Node
    {
        public int NodeSize { get; set; }   // The max number of children the node can have and no more
        public List<int> Value { get; set; }   // The list of integer values

        public string Type { get; set; }

        public int fullness { get; set; }

        /// <summary>
        /// Base constructor
        /// </summary>

        public Node()
        {
            Value = new List<int>();
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="nodeSize">The max number of children it can have</param>

        public Node(int nodeSize)
        {
            NodeSize = nodeSize;
            Value = new List<int>();
        }

        /// <summary>
        /// Override the ToSting Method
        /// </summary>
        /// <returns name = "message">Value in string form</returns>

        public override string ToString()
        {
            fullness = (int)Value.Count * 100 / NodeSize;
            string message = "Node Type : " + Type +
                           "\nNumber of Values is : " + Value.Count +
                           //String.Format("          {0:P2}.", (this.Value.Count / this.NodeSize)) + " full" + 
                           "       Fullness of node: " + fullness + "%" +
                           "\nValues:  ";

            foreach (int i in Value)
                message += i + " ";

            return message + "\n";
        }
    }
}
