//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 3 - Infix to Postfix Expressions
//	File Name:		Utility.cs
//	Description:	Tokenize a string into list of substrings
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Don Bailes, bailes@etsu.edu, Department of Computing, East Tennessee State University
//	Copyright:		Don Bailes
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixtoPostfix
{
    public static class Utility
    {
        public static List<String> Tokenize(string original, string delimiters)
        {
            List<string> Tokens = new List<string>();
            string work = original;
            work = Clean(work);

            int col;
            string token;

            while (!String.IsNullOrEmpty(work))
            {
                col = work.IndexOfAny(delimiters.ToCharArray());
                if (col == 0)
                    col = 1;
                if (col < 0)
                    col = work.Length;
                token = work.Substring(0, col);
                Tokens.Add(token);
                work = work.Substring(col);
                work = work.Trim(" \t".ToCharArray());
            }
            return Tokens;
        }
        private static string Clean(string work)
        {
            work = work.Trim(" \t".ToCharArray());
            int col = work.IndexOf("\r\n");
            while (col != -1)
            {
                work = work.Remove(col, 1);
                col = work.IndexOf("\r\n");
            }
            return work;
        }
    }
}
