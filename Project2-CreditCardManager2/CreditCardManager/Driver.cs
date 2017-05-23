///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Credit Card Mananger
//	File Name:         Driver.cs
//	Description:       Implement a Driver class to handle user iteractions
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Shuhai Li, lis002@goldmail.etsu.edu, Dept. of Computing, East Tennessee State University
//	Created:           Wednesday, September 30, 2015
//	Copyright:         Shuhai Li, 2015
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace CreditCardManager
{
    
    class Driver
    {
        static CreditCard newCard = new CreditCard();
        static List<CreditCard> creditCardData = new List<CreditCard>();
        CreditCardList ccData2 = new CreditCardList();
        public static void MainMenu()
        {
            string selection;
            Console.WriteLine("1: Create a Credit Card List from a text file\n");
            Console.WriteLine("2: Credit Card List Processing(add,remove,select,etc.)\n");
            Console.WriteLine("3: Report Credit Card List\n");
            Console.WriteLine("4: Exit Program\n");
            Console.Write("Select your option(1, 2, 3, or 4):");

            selection = Console.ReadLine();

            if (selection == "1")
            {
                Console.Clear();
                CreditCardCreate();
            }


            if (selection == "2")
            {
                Console.Clear();
                ChildMenu();
            }

            if (selection == "3")
            {
                Console.Clear();
                ReportPage(creditCardData);
            }

            if (selection == "4")
            {
                Console.Clear();
                Environment.Exit(0);
            }

        }
        #region
        public static void CreditCardCreate()
        {
            //create a new CreditCard List
            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.Filter = "text files|*.txt;*.text|all files|*.*";
            //dlg.InitialDirectory = Application.StartupPath;

            StreamReader rdr = null;
            rdr = new StreamReader("creditcard.txt");
            //rdr = new StreamReader(dlg.FileName);

            while (rdr.Peek() != -1)
            {
                string[] fields = rdr.ReadLine().Split('|');
                creditCardData.Add(new CreditCard() { cardHolderName = fields[0], telephone = fields[1], email = fields[2], creditCardNumber=fields[3], expirationDate=fields[4] });
            }
            rdr.Close();
            Console.WriteLine("Data in the txt file have been imported!\n");
            Console.Write("Return to Main Menu?(Y or N):");
            string a = Console.ReadLine();
            if ((a == "Y") || (a == "y")) { Console.Clear(); MainMenu(); }
        }

        //Display information for one credit card
        public static void ReportPage(CreditCard card)
        {
            Console.Clear();
            Console.WriteLine("Summary of Credit Card Information\n\n");
            Console.WriteLine("The credit card number: {0}\n", card.creditCardNumber);
            Console.WriteLine("The expiration date:    {0}\n", card.expirationDate);
            Console.WriteLine("The card holder'name:   {0}\n", card.cardHolderName);
            Console.WriteLine("The card holder'email:  {0}\n", card.email);
            Console.WriteLine("The card holder's phone:{0}\n", card.telephone);
            Console.WriteLine("The card type:          {0}\n", card.cardType);

            Console.Write("Return to Main Menu?(Y or N):");
            string a = Console.ReadLine();
            if ((a == "Y") || (a == "y")) { Console.Clear(); MainMenu(); }
        }//ReportPage

        //Display information for a credit card list
        public static void ReportPage(List<CreditCard> results)
        {
            Console.Clear();
            Console.WriteLine("Credit Card List Information\n\n");

            foreach (CreditCard b in results)
            {

                Console.Write("\n{0}\t{1}\t{2}\t{3}\t{4}", b.cardHolderName,
                    b.telephone, b.email, b.creditCardNumber, b.expirationDate);
            }
            Console.WriteLine();


            Console.Write("Return to Main Menu?(Y or N):");
            string a = Console.ReadLine();
            if ((a == "Y") || (a == "y")) { Console.Clear(); MainMenu(); }
        }//ReportPage
        #endregion

        #region
        public static void ChildMenu()
        {
            string selection2;
            //Console.Clear();
            Console.WriteLine("1: Add a Credit Card\n");
            Console.WriteLine("2: Remove a Credit Card\n");
            Console.WriteLine("3: Retrieve a Credit Card from position n\n");
            Console.WriteLine("4: Retrieve Credit Card belonging to a person\n");
            Console.WriteLine("5: Display all non-expired valid credit card\n");
            Console.WriteLine("6: Sort the Credit Cards by card number\n");
            Console.WriteLine("7: Save the Credit Cards to a file\n");
            Console.WriteLine("8: Return to Main Menu \n");
            Console.Write("Select your option:");
            selection2 = Console.ReadLine();
            if (selection2 == "1")
            {
                CreditCardAdd();
            }

            if (selection2 == "2")
            {
                int n;
                Console.Write("Input the index of record to remove:");
                n = int.Parse(Console.ReadLine());
                
                CreditCardRemove(n);
            }

            if (selection2 == "3")
            {
                int n;
                Console.WriteLine("Input the index of record to search:");
                n = int.Parse(Console.ReadLine());

                CreditCardRetrieveByPosition(n);
            }

            if (selection2 == "4")
            {
                string name;
                Console.WriteLine("Input a name to search");
                name = Console.ReadLine();
                CreditCardRetrieveByName(name);
            }

            if (selection2 == "5")
            {
                CreditCardDisplayValid();
            }

            if (selection2 == "6")
            {
                CreditCardSort();
            }

            if (selection2 == "7")
            {
                CreditCardSave();
            }

            if (selection2 == "8")
            {
                Console.Clear();
                MainMenu();
            }

        }
        //Add a credit card to the list
        public static void CreditCardAdd()
        {
            CreditCard newCard = new CreditCard();
            
            //Input card holder's name
            Console.Write("\nCardholder's Name (First+space+Last,e.g. John Smith):");
            newCard.cardHolderName = Console.ReadLine();

             //Input card holder's phone
            Console.Write("\nCardholder's telophone[xxx-xxx-xxxx]:");
            newCard.telephone = Console.ReadLine();       

            //Input card holder's email
            Console.Write("\nCardholder's email:");
            newCard.email = Console.ReadLine();      
                   
            //Input credit card number
            Console.Write("\nCredit Card Number:");
            newCard.creditCardNumber = Console.ReadLine();

            //Input expiration date
            Console.Write("\nExpiration Date[MM/YYYY]:");
            newCard.expirationDate = Console.ReadLine();

            creditCardData.Add(newCard);
            Console.Clear();
            Console.Write("Card Info completed.New Card added to the list\n");
            ChildMenu();
        }

        //Remove a credit card from the list
        public static void CreditCardRemove(int n)
        {
            CreditCard a = new CreditCard();
            a = creditCardData[n];
            creditCardData.Remove(a);
            Console.Clear();
            Console.WriteLine("Item removed");
            ChildMenu();
        
        }

        //retrieve a record from the list based on its position
        public static void CreditCardRetrieveByPosition(int n)
        {
            CreditCard a = new CreditCard();
            a = creditCardData[n];
            //CreditCardList.RetrieveCC(3);
            Console.Write(a);
            Console.WriteLine("\n");
            ChildMenu();
        }

        //retrieve record(s) from the list based on credit card holder's name
        public static List<CreditCard> CreditCardRetrieveByName(string name)
        {
          List<CreditCard> SearchResult = creditCardData.FindAll(
          delegate (CreditCard cc)
          {
              return cc.cardHolderName == name;
          }
          );

            foreach (CreditCard b in SearchResult)
            {
                Console.Write("\n{0}\t{1}\t{2}\t{3}\t{4}", b.cardHolderName,
                b.telephone, b.email, b.creditCardNumber, b.expirationDate);
            }
            Console.WriteLine();
            ChildMenu();

            if (SearchResult.Count != 0)
            {
                return SearchResult;
            }
            else
            {
                return null;
            }

            
        }
        
        //display all the non-expired valid cards
        public static List<CreditCard> CreditCardDisplayValid()
        {
            for (int i = 0; i < creditCardData.Count; i++)
            {
                creditCardData[i].ExpirationChecking(creditCardData[i].expirationDate);
                creditCardData[i].CardNoValidate(creditCardData[i].creditCardNumber);
            }

            List<CreditCard> SearchResult = creditCardData.FindAll(
            delegate (CreditCard cc)
            {
                return ((cc.cardType != "Invalid") && (cc.isExpired == false));
            }
            );
            Console.Clear();
            foreach (CreditCard b in SearchResult)
            {
                Console.Write("\n{0}\t{1}\t{2}\t{3}\t{4}", b.cardHolderName,
                b.telephone, b.email, b.creditCardNumber, b.expirationDate);
            }
            Console.WriteLine("non-expired valid cards displayed!");
            Console.WriteLine();
            ChildMenu();

            if (SearchResult.Count != 0)
            {
                return SearchResult;
            }
            else
            {
                return null;
            }


        }
        //sort the credit cards in the list based on the card number
        public static void CreditCardSort()
        {
            creditCardData.Sort();
            Console.Clear();
            Console.WriteLine("Credit Card List Sorted!\n");
            Console.WriteLine("You can view the sorted list by going to back to main menu\n");
            ChildMenu();
        }
        //save the list to a text file
        public static void CreditCardSave()
        {
            StreamWriter writer = null;
            writer = new StreamWriter(new FileStream("creditcard_updated.txt", FileMode.Create, FileAccess.Write));
            for (int i = 0; i < creditCardData.Count; i++)
            {
                writer.WriteLine(creditCardData[i].cardHolderName + "|" + creditCardData[i].telephone + "|" + creditCardData[i].email + "|"
                    + creditCardData[i].creditCardNumber + "|" + creditCardData[i].expirationDate);
            }
            writer.Close();
            Console.Clear();
            Console.WriteLine("Credit Card List Saved!\n");
            ChildMenu();
        }
        #endregion
    } //End of Driver class
} 
