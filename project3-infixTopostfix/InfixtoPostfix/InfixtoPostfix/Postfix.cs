//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 3 - Infix to Postfix Expressions
//	File Name:		Postfix.cs
//	Description:	Define a class Postfix
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Shuhai Li, lis002@goldmail.etsu.edu, Department of Computing, East Tennessee State University
//	Created:		Friday, 11/06/2015
//	Copyright:		Shuhai Li, 2015
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixtoPostfix
{
    /// <summary>
    /// Implementation of main functions of the program that converts infix expressions to postfix expressions
    /// </summary>
    class Postfix
    {
        public string infix2;
        public string postfix2;
        public Postfix(string a)
        {
            infix2 = a;
            postfix2 = null;
        }

        /// <summary>
        /// Check if the infix expression is a valid expression,e.g. unpaired parenthesis
        /// </summary>
        /// <param name="infix">input infix expression</param>
        public bool isValid(string infix)
        {
            Stack<Operator> stack1 = new Stack<Operator>();
            int n;
            n = infix.Length;
            for (int i = 0; i < n; i++)
            {
                if (infix.Substring(i, 1) == "(")
                {
                    Operator b = new Operator(infix.Substring(i, 1));
                    stack1.Push(b);
                }

                if (infix.Substring(i, 1) == ")")
                {
                    if (stack1.Count > 0)
                    {
                        stack1.Pop();
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (stack1.Count != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// This function converts a infix expression to a postfix expression 
        /// </summary>
        /// <param name="infix">Input infix expression</param>
        public string Convert(string infix)
        {
            string postfix = null;  //postfix return from this method
            List<string> StringA = new List<string>();  //Tokenized string list from input
            List<string> StringB = new List<string>();  //string list for output 
            Stack<Operator> OperatorStack = new Stack<Operator>(); //stack holding operators
            string delimiter = "+-*/()=";  //characters used to tokenized the input string

            if (isValid(infix) == false)
            {
                postfix = "Error! Unpaired parenthesis!";
                return postfix;
            }
            infix.Replace(" ", string.Empty);//remove spaces in the infix expression

            StringA = Utility.Tokenize(infix, delimiter); //tokenzie the infix expression

            for (int i = 0; i < StringA.Count; i++)
            {
                //if incoming string is an operand, add to a second string list
                if (StringA[i] != "+" & StringA[i] != "-" & StringA[i] != "*" & StringA[i] != "/" & StringA[i] != "(" & StringA[i] != ")" & StringA[i] != "=")
                {
                    StringB.Add(StringA[i] + " ");
                }
                //if incoming string is an operator
                if (StringA[i] == "+" | StringA[i] == "-" | StringA[i] == "*" | StringA[i] == "/" | StringA[i] == "(" | StringA[i] == ")" | StringA[i] == "=")
                {
                    Operator a = new Operator(StringA[i]);//create an Operator object
                    //if the stack is empty , push the incoming string to the stack
                    if (OperatorStack.Count == 0)
                    {
                        OperatorStack.Push(a);
                    }
                    else  //if the stack is not empty
                    {
                        if (StringA[i] == "(" || StringA[i] == ")" || OperatorStack.Peek().ToString2() == "(")
                        {
                            //if incoming string or the top of the stack is (, push the incoming string to the stack
                            if (StringA[i] == "(" || OperatorStack.Peek().ToString2() == "(")
                            {
                                OperatorStack.Push(a);
                            }

                            //if the incoming string is ), pop the stack and add to StringB until ( is seen
                            if (StringA[i] == ")")
                            {
                                while (OperatorStack.Count != 0 && OperatorStack.Peek().ToString2() != "(")
                                {
                                    StringB.Add(OperatorStack.Peek().ToString2());
                                    OperatorStack.Pop();
                                }
                                OperatorStack.Pop();   //pop the (
                            }
                        }
                        else  //if the incoming string is not ( or )
                        {
                            //if the incoming string has higher precedence than the top one of the stack, push  it
                            if (a.Precedence > OperatorStack.Peek().Precedence)
                            {
                                OperatorStack.Push(a);
                            }
                            else
                            {    //if the incoming string has lower or equal precedence than the top one of the stack, 
                                 //pop  it until stack is empty or incoming string has higher precedenece
                                while (OperatorStack.Count != 0 && a.Precedence <= OperatorStack.Peek().Precedence)
                                {
                                    StringB.Add(OperatorStack.Peek().ToString2());
                                    OperatorStack.Pop();
                                }
                                OperatorStack.Push(a);
                            }
                        }
                    }
                }
            }
            //move remaining elements in the stack to the output string
            while (OperatorStack.Count != 0)
            {
                StringB.Add(OperatorStack.Peek().ToString2());
                OperatorStack.Pop();
            }
            //add space to each substring
            foreach (string x in StringB)
            {
                postfix = postfix + x + " ";
            }
            return postfix;
        }
    }
}