///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Credit Card Mananger
//	File Name:         CreditCard.cs
//	Description:       Implement a credit card class
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
using System.Text.RegularExpressions;

namespace CreditCardManager
{
    public class CreditCard : IEquatable<CreditCard>, IComparable<CreditCard>
    {
        public string creditCardNumber { get; set; }
        public string expirationDate { get; set; }
        public string cardHolderName { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }

        public string cardType;
        public bool isDateOk;
        public bool isExpired;
        public bool isEmailOk;
        public bool isPhoneOK;
        public bool isNameOk;


        #region ToString
        /// <summary>
        /// Format user information as a string 
        /// for possible display
        /// </summary>
        /// <returns>formatted string</returns>
        public override string ToString()
        {
            String result = String.Format("\nName: {0}\n", cardHolderName);
            result += String.Format("Phone: {0}\n", telephone);
            result += String.Format("Email: {0}\n", email);
            result += String.Format("Card Number: {0}\n", creditCardNumber);
            result += String.Format("Expiration Date: {0}\n", expirationDate);
            return result;
        }
        #endregion

        #region IEquatable<User> Members
        /// <summary>
        /// Two Credit Cards are equal if their numbers are the same
        /// </summary>
        /// <param name="other">another credit card</param>
        /// <returns>true if equal; false otherwise</returns>
        public bool Equals(CreditCard other)
        {
            return this.creditCardNumber.Equals(other.creditCardNumber);
        }

        /// <summary>
        /// Override of Object.Equals
        /// </summary>
        /// <param name="obj">the entity being compared to this credit card</param>
        /// <returns>true if obj is a User object and they have the same number; 
        ///                     false otherwise</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return base.Equals(obj);

            if (!(obj is CreditCard))
                throw new ArgumentException(String.Format("Cannot compare a User object to a {0} object",
                                                obj.GetType()));
            return Equals(obj as CreditCard);
        }

        /// <summary>
        /// Override of Object.GetHashCode
        /// </summary>
        /// <returns hash code of the creditCardNo</returns>
        public override int GetHashCode()
        {
            return this.creditCardNumber.GetHashCode();
        }
        #endregion

        #region IComparable<CreditCard> Members
        /// <summary>
        /// Ordering comparer for User class
        /// </summary>
        /// <param name="other">the other User to compare against this User</param>
        /// <returns>0 if equal; > 0 if this is greater; 
        ///              < 0 if this is smaller</returns>
        public int CompareTo(CreditCard other)
        {
            return this.creditCardNumber.CompareTo(other.creditCardNumber);
        }
        #endregion

        public void CardNoValidate(string creditcardno)
        {
            this.cardType = "Others";

            //length of credit card should be between 12 and 19 digits
            if (creditcardno.Length > 19 || creditcardno.Length < 12)
            {
                this.cardType = "Invalid";
            }

            //credit card should be 12-19 decimals
            Regex pattern = new Regex(@"\b\d{12,19}\b");
            Match match = pattern.Match(creditcardno);
            if (!match.Success)
            {
                this.cardType = "Invalid";
            }

            //Use of Luhn algorithm to check the validity of credit card
            int length = creditcardno.Length;
            char[] chararray = new char[length]; // {'5','4','9','9','9','9','0','1','2','3','4','5','6','7','8','1'};   //create a character array
            int[] integerarray = new int[length];  //create a integer array
            int temp = 0;
            int sum = 0;

            //chararray = creditcardno.ToCharArray();    //convert string to character array

            //Convert character array to decimal array
            for (int i = 0; i < length; i++)
            {
                integerarray[i] = Convert.ToInt16(creditcardno.Substring(i, 1));
            }

            //Apply Luhn algorithm
            for (int i = (length - 2); i >= 0; i--)
            {
                temp = integerarray[i];
                temp = temp * 2;
                if (temp >= 10)
                {
                    temp = (temp - 10) + 1;
                }
                integerarray[i] = temp;
                i--;
            }

            for (int i = 0; i < length; i++)
            {
                sum = sum + integerarray[i];
            }

            if (Math.IEEERemainder(sum, 10) != 0)
            {
                this.cardType = "Invalid";
            }


            /*Determine the type of credit Card*/
            if (this.cardType != "Invalid")
            {
                Regex pattern2 = new Regex(@"^(34|37)");
                if (pattern2.Match(creditcardno).Success)
                {
                    this.cardType = "AmericanExpress";
                }
                Regex pattern3 = new Regex(@"^4");
                if (pattern3.Match(creditcardno).Success)
                {
                    this.cardType = "Visa";
                }

                Regex pattern4 = new Regex(@"^(51|52|53|54|55)");
                if (pattern4.Match(creditcardno).Success)
                {
                    this.cardType = "MasterCard";
                }

                Regex pattern5 = new Regex(@"^(6011|644|65)");
                if (pattern5.Match(creditcardno).Success)
                {
                    this.cardType = "Discover";
                }
            }
        }


        //Expiration date format checking
        public void DateValidate(string date)
        {
            if (String.IsNullOrEmpty(date))
            {
                this.isDateOk= false;
            }

            Regex pattern = new Regex(@"\b[0-1]\d/(\d\d|20\d\d)\b");
            Match match = pattern.Match(date);
            if (match.Success)
            {
                this.isDateOk= true;
            }
            else
            {
                this.isDateOk= false;
            }
        }

        //Expiration date checking
        //'true' means the card expires
        // 'false' means the card has not expired
        public void ExpirationChecking(string date)
        {
            DateTime CurrentDate = DateTime.Today;
            int CurrentMonth = CurrentDate.Month;
            int CurrentYear = CurrentDate.Year;
            int ExpirationMonth = Convert.ToInt16(date.Substring(0, 2));
            int ExpirationYear = Convert.ToInt16(date.Substring(3, 4));
            if ((CurrentYear * 12 + CurrentMonth) >= (ExpirationYear * 12 + ExpirationMonth))
            {
                this.isExpired= true;
            }
            else
            {
                this.isExpired= false;
            }
        }

        //Email format checking
        public void EmailValidate(string strIn)
        {

            if (String.IsNullOrEmpty(strIn))
                this.isEmailOk= false;

            // Return true if strIn is in valid e-mail format. 
            try
            {
                this.isEmailOk= Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                this.isEmailOk= false;
            }

        }

        //Telephone number checking
        public void TelephoneValidate(string phone)
        {
            if (String.IsNullOrEmpty(phone))
            {
                this.isPhoneOK= false;
            }

            Regex pattern = new Regex(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$");
            Match match = pattern.Match(phone);
            if (match.Success)
            {
                this.isPhoneOK= true;
            }
            else
            {
                this.isPhoneOK= false;
            }
        }

        //human name checking
        public void NameValidate(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                this.isNameOk= false;
            }
            if (name.Length >= 100)   //too long name
            {
                this.isNameOk = false;
            }

            Regex pattern = new Regex(@"^\w{2,30} +\w{2,30}$");
            Match match = pattern.Match(name);
            if (match.Success)
            {
                this.isNameOk = true;
            }
            else
            {
                this.isNameOk = false;
            }

        }

    }
}
