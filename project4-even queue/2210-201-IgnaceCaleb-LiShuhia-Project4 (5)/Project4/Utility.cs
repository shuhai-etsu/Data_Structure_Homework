///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Simulating conference registration with Queues and Priority Queues
//	File Name:		Utility.cs
//	Description:	Utility Methods that may be reused in console applications
//	Course:			CSCI 2210 - Data Structures	
//	Author:			Don Bailes, bailes@etsu.edu, Dept. of Computing, East Tennessee State University
//	Created:		Saturday, March 16, 2013
//	Copyright:		Don Bailes, 2013
//
//=====================================================================================================================
//
//	Last Modified:	Saturday, March 16, 2013
//	Modified by:	Don Bailes
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Project4
{
	/// <summary>
	/// The Utility class contains several methods that may be useful in multiple projects
	/// </summary>
	public static class Utility
	{
		/// <summary>
		/// Clears the page.
		/// </summary>
		public static void ClearPage()
		{
			for(int i = 0; i < 80; i++)
			{
				Console.WriteLine();
			}
		}


 		#region WelcomeMessage
		/// <summary>
		/// Display a specified welcome message in a Message Box
		/// </summary>
		/// <param name="msg">The message to be displayed</param>
		/// <param name="caption">the caption for the Message Box - the author's name is appended</param>
		/// <param name="author">the name of the author of the program</param>
		public static void WelcomeMessage(String msg, String caption = "Computer Science 2210", String author = "Don Bailes")
		{
			MessageBox.Show(null, DateTime.Today.ToLongDateString() + "\n\n" + msg, caption + " - " + author,
			MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		#endregion

		#region GoodbyeMessage
		/// <summary>
		/// Display a goodbye message
		/// </summary>
		/// <param name="msg">the message to be displayed</param>
		public static void GoodbyeMessage(String msg = "Goodbye and thank you for using this program.")
		{
			MessageBox.Show(null, msg, "Goodbye and Thank You",
			MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		#endregion
	}
}
