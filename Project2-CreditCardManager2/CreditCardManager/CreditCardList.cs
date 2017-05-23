///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Credit Card Mananger
//	File Name:         CreditCardList.cs
//	Description:       Implement a credit card list class
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Shuhai Li, lis002@goldmail.etsu.edu, Dept. of Computing, East Tennessee State University
//	Created:           Wednesday, September 30, 2015
//	Copyright:         Shuhai Li, 2015
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace CreditCardManager
{
    public class CreditCardList
    {
        static List<CreditCard> CCList;
        int count;
        public bool SaveNeeded;
        CreditCard newcard;
        int n; //the object to be removed
        //default constructor
        public CreditCardList()
        {
            CCList = null;
        }

        //Constructor that read text file and create a new list of creditcard
        public CreditCardList(string filename)
        {

        }
        //Add a CreditCard object to CreditCardList
        public static void AddCC(CreditCard a)
        {
            CCList.Add(a);
        }

        //Remove a CreditCard object from CreditCardList
        public static void RemoveCC(int n)
        {
            CCList.RemoveAt(n);
        }

        //Retrieve the CreditCard in position n of the list
        public static CreditCard RetrieveCC(int n)
        {
            if ((n>=0)&&(n<=CCList.Count))
            {
              return CCList[n];
            }else
            {
                return null;
            }
            
        }

        //Retrieve a list of all CreditCard objects belonging to a specified person 
        public List<CreditCard> RetrieveCC(string name)
        {
           List<CreditCard> SearchResult = CCList.FindAll(
           delegate (CreditCard cc)
           {
               return cc.cardHolderName == name;
           }
           );
           if (SearchResult.Count!=0)
            {
                return SearchResult;
            }else
            {
                return null;
            }
        }

        //Retrieve a list of all non-expired, valid CreditCards
        public List<CreditCard> RetrieveValid()
        {
           for (int i=0;i<CCList.Count;i++)
            {
                CCList[i].ExpirationChecking(CCList[i].expirationDate);
                CCList[i].CardNoValidate(CCList[i].creditCardNumber);
            }

            List<CreditCard> SearchResult = CCList.FindAll(
            delegate (CreditCard cc)
            {
                return ((cc.cardType!="Invalid")&&(cc.isExpired= false));
            }
            );
            if (SearchResult.Count != 0)
            {
                return SearchResult;
            }
            else
            {
                return null;
            }
        }

        //sort the CreditCard list based on the credit card number
        public void SortCC()
        {
            CCList.Sort();
        }
        //save the entire list of credit card objects in a text file
        public void SaveCC()
        {
            StreamWriter writer=null;
            writer = new StreamWriter(new FileStream("creditcard.txt", FileMode.Create, FileAccess.Write));
            for (int i=0;i<CCList.Count;i++)
            {
                writer.WriteLine(CCList[i].cardHolderName + "|" + CCList[i].telephone + "|" + CCList[i].email + "|"
                    + CCList[i].creditCardNumber+"|"+CCList[i].expirationDate);
            }
        }

    }//end of CreditCardList
}
