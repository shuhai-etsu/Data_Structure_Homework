using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CreditCardManager
{
    class Program
    {
        static void Main(string[] args)
        {
            IODriver.MainMenu();
        }
    }

    public class CreditCard
    {
        public int creditCardID;
        public string creditCardNumber;
        public string expirationDate;
        public string cardHolderName;
        public string telephone;
        public string email;
        public string cardType;
        //public enum cardType {invalid, VISA, MASTERCARD, AMERICAN_EXPPRESS, DISCOVER, OTHER };
    }

    public class IODriver
    {
        static CreditCard newCard = new CreditCard();
        public static void MainMenu()
        {
            string selection;
            Console.WriteLine("1: Create a Credit Card\n");
            Console.WriteLine("2: Validate Credit Card Info\n");
            Console.WriteLine("3: Report Credit Card Summary\n");
            Console.WriteLine("4: Exit Program\n");
            Console.Write("Select your option(1, 2, 3 or 4):");

            selection = Console.ReadLine();

            if (selection == "1")
            {
                Console.Clear();
                CreditCardCreate();
            }

            if (selection == "2")
            {
                ValidatePage();
            }

            if (selection == "3")
            {
                Console.Clear();
                ReportPage(newCard);
            }

            if (selection == "4")
            {
                Console.Clear();
                Environment.Exit(0);
            }

        }

        public static void CreditCardCreate()
        {
            //create a new CreditCard object

            
            /*Input a serial number to represent a specific credit number.
            This number is used to specify which recredit card to report*/
            Console.Write("Credit Card ID (please input an integral):");
            newCard.creditCardID = Convert.ToInt16(Console.ReadLine());
            
            //Input credit card number
            Console.Write("\nCredit Card Number:");
            newCard.creditCardNumber = Console.ReadLine();
            
            //Input expiration date
            Console.Write("\nExpiration Date[MM/YYYY]:");
            newCard.expirationDate = Console.ReadLine();

            //Input card holder's name
            Console.Write("\nCardholder's Name (First+space+Last,e.g. John Smith):");
            newCard.cardHolderName = Console.ReadLine();

            //Input card holder's email
            Console.Write("\nCardholder's email:");
            newCard.email = Console.ReadLine();

            //Input card holder's phone
            Console.Write("\nCardholder's telophone[xxx-xxx-xxxx]:");
            newCard.telephone = Console.ReadLine();

            Console.Write("Card Info completed.Return to Main Menu?(Y or N):");
            string a = Console.ReadLine();
            if ((a == "Y") || (a == "y")) { Console.Clear(); MainMenu();  }
        }
        public static void ValidatePage()
        {
            Console.Clear();
            InfoValidation CDValidate = new InfoValidation();
            bool isDateOk;
            bool isExpired;
            bool isEmailOk;
            bool isNameOk;
            string type;

            type = CDValidate.CardNoValidate(newCard.creditCardNumber);
            isDateOk = CDValidate.DateValidate(newCard.expirationDate);
            isExpired = CDValidate.ExpirationChecking(newCard.expirationDate);
            isEmailOk = CDValidate.EmailValidate(newCard.email);
            isNameOk = CDValidate.NameValidate(newCard.cardHolderName);

            if (type == "Invalid")
            {
                Console.WriteLine("The credit card no is not a valid number\n");
            }

            if (isDateOk == false)
            {
                Console.WriteLine("The expiration date you input is in correct format\n");
            }

            if (isExpired == true)
            {
                Console.WriteLine("Your credit card has expired\n");
            }

            if (isNameOk == false)
            {
                Console.WriteLine("Your name is not typed correctly\n");
            }

            if (isEmailOk == false)
            {
                Console.WriteLine("Your email is not in correct formate\n");
            }

            if ((type != "Invalid") && (isDateOk == true) && (isExpired == false) && (isNameOk == true) && (isEmailOk == true))
            {
                Console.WriteLine("Everything is all right\n");
                newCard.cardType = type;
            }
            Console.Write("Return to Main Menu?(Y or N):");
            string a = Console.ReadLine();
            if ((a == "Y") || (a == "y")) { Console.Clear(); MainMenu();  }
        }
        public static void ReportPage(CreditCard card)
        {
            Console.Clear();
            Console.WriteLine("Summary of Credit Card Information\n\n");
            Console.WriteLine("The card ID:            {0}\n", card.creditCardID);
            Console.WriteLine("The credit card number: {0}\n", card.creditCardNumber);
            Console.WriteLine("The expiration date:    {0}\n", card.expirationDate);
            Console.WriteLine("The card holder'name:   {0}\n", card.cardHolderName);
            Console.WriteLine("The card holder'email:  {0}\n", card.email);
            Console.WriteLine("The card holder's phone:{0}\n", card.telephone);
            Console.WriteLine("The card type:          {0}\n", card.cardType);
            Console.Write("Return to Main Menu?(Y or N):");
            string a = Console.ReadLine();
            if ((a == "Y") || (a == "y")) { Console.Clear(); MainMenu(); }
        }
    }

    public class InfoValidation
    {
  
        public string CardNoValidate(string creditcardno)
        {
            string type = "Others";

            //length of credit card should be between 12 and 19 digits
            if (creditcardno.Length > 19 || creditcardno.Length < 12)
            {
                type = "Invalid";
            }

            //credit card should be 12-19 decimals
            Regex pattern = new Regex(@"\b\d{12,19}\b");
            Match match = pattern.Match(creditcardno);
            if (!match.Success)
            {
                type = "Invalid";
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
                type = "Invalid";
            }


            /*Determine the type of credit Card*/
            if (type != "Invalid")
            {
                Regex pattern2 = new Regex(@"^(34|37)");
                if (pattern2.Match(creditcardno).Success)
                {
                    type = "AmericanExpress";
                }
                Regex pattern3 = new Regex(@"^4");
                if (pattern3.Match(creditcardno).Success)
                {
                    type = "Visa";
                }

                Regex pattern4 = new Regex(@"^(51|52|53|54|55)");
                if (pattern4.Match(creditcardno).Success)
                {
                    type = "MasterCard";
                }

                Regex pattern5 = new Regex(@"^(6011|644|65)");
                if (pattern5.Match(creditcardno).Success)
                {
                    type = "Discover";
                }
            }
            
            return type;
        }


        //Expiration date format checking
        public bool DateValidate(string date)
        {
            if (String.IsNullOrEmpty(date))
            {
                return false;
            }

            Regex pattern = new Regex(@"\b[0-1]\d/(\d\d|20\d\d)\b");
            Match match = pattern.Match(date);
            if (match.Success)
            {
                return true;
            }else
            {
                return false;
            }
        }
         
        //Expiration date checking
        //'true' means the card expires
        // 'false' means the card has not expired
        public bool ExpirationChecking(string date)
        {
            DateTime CurrentDate = DateTime.Today;
            int CurrentMonth = CurrentDate.Month;
            int CurrentYear = CurrentDate.Year;
            int ExpirationMonth = Convert.ToInt16(date.Substring(0, 2));
            int ExpirationYear = Convert.ToInt16(date.Substring(3, 4));
            if ((CurrentYear*12+CurrentMonth)>=(ExpirationYear*12+ExpirationMonth))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        //Email format checking
        public bool EmailValidate(string strIn)
        {

            if (String.IsNullOrEmpty(strIn))
                return false;

            // Return true if strIn is in valid e-mail format. 
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

        }
        
        //Telephone number checking
        public bool TelephoneValidate(string phone)
        {
            if (String.IsNullOrEmpty(phone))
            {
                return false;
            }

            Regex pattern = new Regex(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$");
            Match match = pattern.Match(phone);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        //human name checking
        public bool NameValidate(string name)
        {     
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }
            if (name.Length>=100)   //too long name
            {
                return false;
            }

            Regex pattern = new Regex(@"^\w{2,30} +\w{2,30}$");
            Match match = pattern.Match(name);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }

}
