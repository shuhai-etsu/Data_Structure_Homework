//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Simulating conference registration with Queues and Priority Queues
//	File Name:		ConventionRegistractionDriver.cs
//	Description:	Driver class for convention registraction
//	Course:			CSCI 2210-201 - Data Structures
//	Author:			Caleb Ignace & Shuhai Li; ignacec@goldmail.etsu.edu & lis002@goldmail.etsu.edu
//	Created:		Friday, November 13, 2015
//	Copyright:		Caleb Ignace & Shuhai Li, 2015
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////11
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4
{
    /// <summary>
    /// Drivers class for this application. This application allows users to change
    /// the expected number of registrants, processing time for one registration,
    /// open hours of registration, and number of registration windows  
    /// </summary>
    class ConventionRegistrationDriver
	{
		static void Main(string[] args)
		{
			Console.Title = "Ignace & Li -- Project 4 -- Convention Registration";

			int numberOfRegistrants = 1000;   //Number of expected registrants
			int expectedProcessingTime = 255; //Expected processing time for one registrant (second)
			int numberOfWindowsStaffed = 10;   //Number of registration windows
			int timeRegistrationIsOpen = 10 * 60 * 60; //Number of open registration hours (default 10)
			double minProcessingTime = 90.0;
			DateTime timeWeOpen = new DateTime(2015, 11, 15, 8, 0, 0); //Open hour of registration(default 8:00 AM)


			Utility.WelcomeMessage("Welcome to the Convention Registration Simulator!\n\n" +
				"By Shuhai Li & Caleb Ignace\n\n" +
				"November 16, 2015\n\n" +
				"Today, we will run a simulation of a registration process\n" +
				"at a conference between the hours 8 am and 6 pm; you will\n" +
				"be able to specify some of the conditions.\n\n" +
				"We would like to know when how many registration windows\n" +
				"must be open to keep the line down to a specific number."
				,"CSCI 2210-201", "Shuhai & Ignace");

			bool notValid;
			string choose;

			Console.WriteLine("Time to run simulation (in seconds): " + timeRegistrationIsOpen + ", i.e. 10 hours");
			Console.WriteLine("Expected number of registrants: " + numberOfRegistrants);
			Console.WriteLine("Expected processing time: " + expectedProcessingTime);
			Console.WriteLine("Minimum processing time: "+ minProcessingTime);
			Console.WriteLine("Number of windows staffed: "+numberOfWindowsStaffed + "\n");

			do
			{
				notValid = true;

				Console.WriteLine("Would you like to enter different values than the ones listed above? (Y/N): ");
				choose = Console.ReadLine().ToUpper();

				if (choose == "Y" || choose == "N")
					notValid = false;
				else
					Console.WriteLine(" INVALID");

			} while (notValid);

			if (choose == "Y")
			{
				do  //Input seconds to run program
				{
					notValid = true;

					Console.WriteLine("How long (in seconds) would you like to run: ");

					if (int.TryParse(Console.ReadLine(), out timeRegistrationIsOpen))
						notValid = false;
					else
						Console.WriteLine(" INVALID");

				} while (notValid);

				do  //take input from Console for the expected number of registrants
				{
					notValid = true;

					Console.WriteLine("Please supply the number of registrants that you expect: ");

					if (int.TryParse(Console.ReadLine(), out numberOfRegistrants))
						notValid = false;
					else
						Console.WriteLine(" INVALID");

				} while (notValid);

                do   //take input from Console for the expected processing time
                {
					notValid = true;

					Console.WriteLine("Please supply the expected processing time for each registrant (in seconds): ");

					if (int.TryParse(Console.ReadLine(), out expectedProcessingTime))
						notValid = false;
					else
						Console.WriteLine(" INVALID");

				} while (notValid);

				do  //Input seconds to run program
				{
					notValid = true;

					Console.WriteLine("What is the minimum processing time (in seconds): ");

					if (double.TryParse(Console.ReadLine(), out minProcessingTime))
						notValid = false;
					else
						Console.WriteLine(" INVALID");

				} while (notValid);


				do    //take input from Console for the number of registration windows
                {
					notValid = true;

					Console.WriteLine("Please supply number of windows you want to be staffed: ");

					if (int.TryParse(Console.ReadLine(), out numberOfWindowsStaffed))
						notValid = false;
					else
						Console.WriteLine(" INVALID");

				} while (notValid);
			}
            //Create an instance of ConventionRegistration
			ConventionRegistration CR = new ConventionRegistration(numberOfRegistrants, expectedProcessingTime,
					numberOfWindowsStaffed, timeRegistrationIsOpen, timeWeOpen, minProcessingTime);

			CR.GenerateArrivalEvents();  //generate arrival events
			CR.Simulate();               //simulate the registration process

			Utility.GoodbyeMessage("It turns out that the longest line consisted of " + CR.GetLongestLineLength() +
                "\nregistrants at " + numberOfRegistrants + " expected registrants, an expected processing time of " + 
				"\n" + expectedProcessingTime + ", and " + numberOfWindowsStaffed + " number of windows staffted.");  
		}
	}
}