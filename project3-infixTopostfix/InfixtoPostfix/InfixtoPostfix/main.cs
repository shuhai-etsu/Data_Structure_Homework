//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 3 - Infix to Postfix Expressions
//	File Name:		main.cs
//	Description:	Main entry to the program
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Shuhai Li, lis002@goldmail.etsu.edu, Department of Computing, East Tennessee State University
//	Created:		Friday, 11/06/2015
//	Copyright:		Shuhai Li, 2015
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfixtoPostfix
{
    /// <summary>
    /// Main entry to the program
    /// </summary>

    static class main
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InfixToPostfix());
        }
    }
}
