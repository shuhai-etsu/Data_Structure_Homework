//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 5 - B-Trees
//	File Name:		BTreeDriver.cs
//	Description:	Immplementation of a BTree via menu and console text
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
using System.Windows.Forms;

namespace B_Tree
{
    /// <summary>
    /// Driver class for the application
    /// </summary>

    class BTreeDriver
    {
        private static BTree BTREE = new BTree(3); // BTREE is a 3-ary by default

        private static Menu menu;
        private enum CHOICES { SET = 1, DISPLAY, ADD, FIND, QUIT } // Choices of menu

        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        
        public static void Main(string[] args)
        {
            Setup();       // Setup Console and Menu; Display a welcome message

            RunMenu();    // Display menu, get user's choices in a loop

            Utility.GoodbyeMessage("I hope you found this helpful!");
        }


        /// <summary>
        /// Setups the menu.
        /// </summary>

        private static void Setup()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Title = "Ignace & Li: Project 5 -- B-Tree";
            Console.Clear();

            Utility.WelcomeMessage("Welcome to the B-Tree simulation by Caleb Ignace and Shuhai Li.\n\n"+
                "The BTree is a 3-ary by default if you would like to go ahead and add values.\n"+
                "Also, note that the BTree starts off with a Root, which is an Index node.");

            menu = new Menu("B-Tree Menu");
            menu = menu + "Set Size of Node and Create a new B-Tree" +
                "Display the B-Tree" +
                "Add a Value to the B-Tree" +
                "Find a Value in the B-Tree" +
                "End the program";
        }

        /// <summary>
        /// Runs the menu.
        /// </summary>

        private static void RunMenu()
        {
            CHOICES choice = (CHOICES)menu.GetChoice();
            while (choice != CHOICES.QUIT)
            {
                switch (choice)
                {
                    case CHOICES.SET:
                        createNewBTree();       // Create a new BTree with 500 random integers 
                        Utility.PressAnyKey();
                        break;

                    case CHOICES.DISPLAY:
                        DisplayBTree();         // Display BTree
                        Utility.PressAnyKey();
                        break;

                    case CHOICES.ADD:
                        AddValue();             // Add a user-specified integer to the BTree
                        Utility.PressAnyKey();
                        break;

                    case CHOICES.FIND:
                        FindValue();            // Find a user-specified integer in the BTree
                        Utility.PressAnyKey();
                        break;


                }

                choice = (CHOICES)menu.GetChoice();
            }
        }


        /// <summary>
        /// Creates the new BTree with 500 random integers.
        /// </summary>
        
        public static void createNewBTree()
        {
            // Generate 500 random integers
            Random R = new Random();
            List<int> IntList = new List<int>(500);
            int valueTobeAdded;

            for (int i = 0; i < IntList.Capacity; i++)
            {
                valueTobeAdded = R.Next(9999);
                while (IntList.Contains(valueTobeAdded))
                    valueTobeAdded = R.Next(9999);
                IntList.Add(valueTobeAdded);
            }

            // Ask for the arity of the BTree
            Console.WriteLine("What is the arity of the tree to be created? ");
            string input = Console.ReadLine();

            int value;

            if (int.TryParse(input, out value))
            {
                BTREE.NodeSize = value;

                // Put integers into the BTree
                for (int i = 0; i < IntList.Count; i++)
                {
                    BTREE.AddValue(IntList[i]);
                }
                Console.WriteLine("A B-Tree has been created with an arity of '" + input + "'." +
                                     "\n500 random values have been added to the BTree");
            }
            else
                Console.WriteLine("'" + input + "'" + " is invalid input.");
        }

        /// <summary>
        /// Adds the value.
        /// </summary>

        private static void AddValue()
        {
            // Ask for the value to be added
            Console.WriteLine("What value do you want to add to the tree? ");

            string input = Console.ReadLine();

            int value;
            bool result;

            if (int.TryParse(input, out value))
            {
                result = BTREE.AddValue(value);

                if (result)
                    Console.WriteLine("'" + input + "'" + " have been added to the B-Tree.");
                else
                    Console.WriteLine("'" + input + "'" + " have NOT been added to the B-Tree because it already exists.");
            }
            else
                Console.WriteLine("'" + input + "'" + " is invalid input.");
        }


        /// <summary>
        /// Finds the value.
        /// </summary>

        private static void FindValue()
        {
            // Ask for the value to be searched for in the tree
            Console.WriteLine("What value do you want to find?");

            string input = Console.ReadLine();

            int value;
            bool result;

            if (int.TryParse(input, out value))
            {

                Console.WriteLine("\nThe nodes visited in the search for " + value + " were: ");

                //Leaf leaf = BTREE.FindLeaf(value);
                result = BTREE.FindValue(value);

                if (result)
                {
                    Console.WriteLine("\n'" + value + "'" + " has been found in the B-Tree:");
                }
                else
                    Console.WriteLine("'" + value + "'" + " has NOT been found in the B-Tree; it doesn't exist.");
            }
            else
                Console.WriteLine("'" + input + "'" + " is invalid input.");
        }
        /// <summary>
        /// Displays the BTree.
        /// </summary>
        private static void DisplayBTree()
        {
            Console.WriteLine("Displaying the B-Tree... ");

            BTREE.Display();
        }
    }
}
