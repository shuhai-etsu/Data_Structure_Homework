
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 3 - Infix to Postfix Expressions
//	File Name:		Operator.cs
//	Description:	Define a class Operator
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Shuhai Li, lis002@goldmail.etsu.edu, Department of Computing, East Tennessee State University
//	Created:		Friday, 11/06/2015
//	Copyright:		Shuhai Li, 2015
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixtoPostfix
{
    /// <summary>
    /// Operator class that assigns precedences to various opeartors
    /// </summary>

    class Operator
    {
        public int Precedence;
        public string Op;

        /// <summary>
        /// Parameterized constructor: Initializes a new instance 
        /// 	of the Operator class using a string as the parameter
        /// </summary>
        /// <param name="inputString">the operator that an Operator object constructed from</param>
        public Operator(string inputString)
        {
            Op = inputString;
            switch (inputString)
            {
                case "=": Precedence = 0; break;
                case "+": Precedence = 2; break;
                case "-": Precedence = 2; break;
                case "*": Precedence = 3; break;
                case "/": Precedence = 3; break;
                case "(": Precedence = 1; break;
                case ")": Precedence = 4; break;
            }

        }

        /// <summary>
        /// 	Null Constructor
        /// </summary>
        public Operator()
        {
            Op = null; ;
            Precedence = 0;
        }

        /// <summary>
        /// Print an Operator object as a string 
        /// </summary>
        public string ToString2()
        {
            return this.Op;
        }
    }
}
