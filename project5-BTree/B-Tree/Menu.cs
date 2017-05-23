///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project 2 - CreditCardList/Project 2 - CreditCardList
//	File Name:         Menu.cs
//	Description:       Manage a Menu for console applications
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Don Bailes, bailes@etsu.edu, Dept. of Computing, East Tennessee State University
//	Created:           Wednesday, September 16, 2015
//	Copyright:         Don Bailes, 2015
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

namespace B_Tree
{
	/// <summary>
	/// Manage a menu for a console application
	/// </summary>
	public class Menu
	{
		private List<string> Items = new List<string>();

		public string Title { get; set; }

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="title">Title displayed over the menu</param>
		public Menu(string title = "Menu")
		{
			Title = title;
		}
		#endregion

		#region operator+ and operator-
		/// <summary>
		/// Add an item to the menu
		/// </summary>
		/// <param name="m">the menu to have an item added</param>
		/// <param name="item">the item to be added</param>
		/// <returns>the modified menu</returns>
		public static Menu operator +(Menu m, string item)
		{
			m.Items.Add(item);
			return m;
		}

		/// <summary>
		/// Remove an item from the menu
		/// </summary>
		/// <param name="m">the menu to have an item removed</param>
		/// <param name="item">the item to be removed</param>
		/// <returns>the modified menu</returns>
		public static Menu operator -(Menu m, int n)
		{
			if (n >= 0 && n < m.Items.Count)
				m.Items.RemoveAt(n);
			return m;
		}
		#endregion

		#region Display Menu
		/// <summary>
		/// Display the menu on a console window
		/// </summary>
		public void Display()
		{
			string str = "";
			Console.Clear();
			str = DateTime.Today.ToLongDateString();
			Console.SetCursorPosition(Console.WindowWidth - str.Length, 0);
			Console.WriteLine(str);
			Console.ForegroundColor = ConsoleColor.Red;

			Console.WriteLine("\n\n\t   " + Title);
			Console.Write("\t   ");
			for (int n = 0; n < Title.Length; n++)
				Console.Write("-");
			Console.WriteLine("\n");
			Console.ForegroundColor = ConsoleColor.Blue;
			for (int n = 0; n < Items.Count; n++)
				Console.WriteLine("\t{0}. {1}", (n + 1).ToString().PadLeft(2), Items[n]);
		}
		#endregion

		#region GetChoice Method
		/// <summary>
		/// Prompt the user to select a choice and verify that the choice is permissible
		/// </summary>
		/// <returns>The 1-based number of the choice the user selected</returns>
		public int GetChoice()
		{
			int choice = -1;
			string line;
			if (Items.Count < 1)
				throw new Exception("The menu is empty");

			while (true)
			{
				Display();
				Console.Write("\n\t   Type the number of your choice from the menu: ");
				Console.ForegroundColor = ConsoleColor.Red;
				line = Console.ReadLine();
				Console.ForegroundColor = ConsoleColor.Blue;
				if (!Int32.TryParse(line, out choice))
				{
					Console.WriteLine("\n\t   Your choice is not a number between 1 and {0}.  Please try again.",
						Items.Count);
					Utility.PressAnyKey();
				}
				else
				{
					if (choice < 1 || choice > Items.Count)
					{
						Console.WriteLine("\n\t   Your choice is not a number between 1 and {0}.  Please try again.",
						Items.Count);
						Utility.PressAnyKey();
					}
					else
					{
						Console.Clear();
						return choice;
					}
				}
			}
		}
		#endregion
	}
}
