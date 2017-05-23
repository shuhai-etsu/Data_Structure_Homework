//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Simulating conference registration with Queues and Priority Queues
//	File Name:		PriorityQueue.cs
//	Description:	A queue giving priority to its members.Implementation of the class copied from class slides
//	Course:			CSCI 2210-201 - Data Structures
//	Author:			Caleb Ignace & Shuhai Li; ignacec@goldmail.etsu.edu  & lis002@goldmail.etsu.edu
//	Created:		Friday, November 13, 2015
//	Copyright:		Caleb Ignace & Shuhai Li, 2015
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4
{
	
	/// <summary>
	/// IContainer will be an interface that IPrioiryQueue needs
	/// </summary>
	/// <typeparam name="T">Generic data type</typeparam>
		
	public interface IContainer<T>
	{
		//Remove all objects from the container
		void Clear();
        
        //Returns true if container is empty
		bool IsEmpty();

		//Returns the number fo enteries in the container
		int Count { get; set; }
	}

	/// <summary>
	/// The data structure IPriorityQueue is a Queue that 
	///    givess priiority to its elements, items
	/// </summary>
	/// <typeparam name="T">Generic data type</typeparam>
	public interface IPriorityQueue<T> : IContainer<T>
		where T : IComparable
	{
		//Inserts item based on its priority
		void Enqeue(T item);

		//Removed first item in the queue
		void Dequeue();

		//Query
		T Peek();
	}

	/// <summary>
	/// Private Node class representing the nodes in the priority queue
	/// </summary>
	public class Node<T>
	{
		//Properties
		public T Item { get; set; }
		public Node<T> Next { get; set; } //Reference to the Next Node

		//Constructor
		public Node(T value, Node<T> link)
		{
			Item = value;
			Next = link;
		}
	}

	/// <summary>
	/// The actual prioity queue
	/// </summary>
	/// <typeparam name="T">Generic data type</typeparam>
	public class PriotityQueue<T> : IPriorityQueue<T> where T : IComparable
	{
		//Fields and properties
		private Node<T> top;       //Reference to teh top of the PQ
		public int Count { get; set; }    //Number of items in the PQ

		/// <summary>
		/// Add an item to the PQ
		/// </summary>
		/// <param name="item"></param>
		public void Enqeue(T item)
		{
			if (Count == 0)
				top = new Node<T>(item, null);
			else
			{
				Node<T> current = top;
				Node<T> previous = null;

				//Search for the first node in the linked structure that is smaller than item
				while (current != null &&
					current.Item.CompareTo(item) >= 0)
				{
					previous = current;
					current = current.Next;
				}

				//Have found the place to insert the new node
				Node<T> newNode = new Node<T>(item, current);

				//If there is a prious node, set it to link to the new node
				if (previous != null)
					previous.Next = newNode;
				else
					top = newNode;
			}

			Count++;  //Add 1 to the number of nodes in the PQ
		}

		/// <summary>
		/// Remove an item from the priority queue and discard it
		/// </summary>
		public void Dequeue()
		{
			if (IsEmpty())
				throw new IndexOutOfRangeException("Cannot remove from empty queue.");
			else
			{
				Node<T> oldNode = top;
				top = top.Next;
				Count--;
				oldNode = null; //do this so the removed node can be garbage collected
			}
		}

		/// <summary>
		/// Make the PQ empty
		/// </summary>
		public void Clear()
		{
			top = null;
		}

		/// <summary>
		/// Retreive the top item on the PQ
		/// </summary>
		/// <returns>The item of the top node</returns>
		public T Peek()
		{
			if (!IsEmpty())
				return top.Item;
			else
				throw new InvalidOperationException("Cannot obtain top of empty priority queue.");
		}

		/// <summary>
		/// Ask whether the PQ is empty
		/// </summary>
		/// <returns>Bool -- true if empty</returns>
		public bool IsEmpty()
		{
			return Count == 0;
		}
	}
}
