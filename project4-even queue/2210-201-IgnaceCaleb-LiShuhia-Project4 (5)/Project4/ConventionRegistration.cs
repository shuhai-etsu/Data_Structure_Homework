//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Simulating conference registration with Queues and Priority Queues
//	File Name:		ConventionRegistraction.cs
//	Description:	Simulating the convention registraction
//	Course:			CSCI 2210-201 - Data Structures
//	Author:			Caleb Ignace & Shuhai Li; ignacec@goldmail.etsu.edu & lis002@goldmail.etsu.edu
//	Created:		Friday, November 13, 2015
//	Copyright:		Caleb Ignace & Shuhai Li, 2015
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Project4
{
	/// <summary>
	/// The convention registraction simulation
	/// </summary>
	class ConventionRegistration
	{
		public int NumberOfRegistrants { private get; set; } //Number of registrants on-site
		public int ExpectedProcessingTime { get; set; }//Expected n required for each rtime registration(in seconds)
		public int RegistrationTime { get; set; }    //Length of time the registrion process is open (in seconds)
		public int NumberOfWindowsStaffed { get; set; } //Number of windows that are staffed
        public DateTime TimeWeOpen { get; set; }	 //The time that the conference registration opens
		public DateTime TimeWeClose { get; set; }  //The time the conference registration closes

		public double MinimumProcessTime { get; set; }

		private PriotityQueue<Event> ArrivalEvents = new PriotityQueue<Event>(); //Priority queue composed of Event objects
		private List<Queue<Registrant>> Lines;			//Lines of registrants
        private List<int> MaxLineLength;				//Maximum number of people in each line
		private List<TimeSpan> StopWatches;				//To dequeue a registrant, at the front of a line, when his processing time is up
		private Random R = new Random();               //Create Random instance

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="numberOfRegistrants">The number of registrants</param>
		/// <param name="expectedProcessingTime">The expected processing time for a registrant</param>
		/// <param name="numberOfWindowsStaffed">Number of windows open for the registration period</param>
		/// <param name="registrionTime">Time in seconds that registration is open</param>
		/// <param name="timeWeOpen">The time that we open -- DateTime</param>

		public ConventionRegistration(int numberOfRegistrants, int expectedProcessingTime,
			int numberOfWindowsStaffed, int registrionTime, DateTime timeWeOpen, double minProcessingTime)
		{
			NumberOfRegistrants = numberOfRegistrants;
			ExpectedProcessingTime = expectedProcessingTime;
			NumberOfWindowsStaffed = numberOfWindowsStaffed;
			RegistrationTime = registrionTime;
			TimeWeOpen = timeWeOpen;
			MinimumProcessTime = minProcessingTime;

			TimeWeClose = TimeWeOpen.Add(new TimeSpan(0, 0, RegistrationTime));

			//Set stop watches to zero
			StopWatches = new List<TimeSpan>(NumberOfWindowsStaffed);
            for (int i=0;i<StopWatches.Capacity;i++)
                StopWatches.Add(new TimeSpan(0, 0, 0));

			//Set initial max line lengths to zero
            MaxLineLength = new List<int>(NumberOfWindowsStaffed);
            for (int i=0;i<MaxLineLength.Capacity;i++)
                MaxLineLength.Add(0);  

			//Create lines
			Lines = new List<Queue<Registrant>>(NumberOfWindowsStaffed);
			for (int i = 0; i < NumberOfWindowsStaffed; i++)
				Lines.Add(new Queue<Registrant>());

			int width = 10 * NumberOfWindowsStaffed + 55;

			if (width > Console.LargestWindowWidth)
				width = Console.LargestWindowWidth;

			Console.SetWindowSize(width, 10);
		}

		/// <summary>
		/// Generate the arrival events, which will be put in the priority queue
		/// </summary>
		/// 
		public void GenerateArrivalEvents()
		{
			NumberOfRegistrants = Poisson(NumberOfRegistrants);
			Event arrivalEvent;          //The actual event
			TimeSpan arrivalTime;        //Arrival time for each registrant	/ event
			TimeSpan processingTime;     //Processing time for each registrant / event	
			List<TimeSpan> listOfArrivalTimes = new List<TimeSpan>();

			for (int registrant = 0; registrant < NumberOfRegistrants; registrant++)
			{
				arrivalEvent = new Event();

				//For each time, make sure it is unique
				do
				{
					arrivalTime = new TimeSpan(0, 0, R.Next(RegistrationTime));
				} while (!UniqueArrivalTime(arrivalTime, listOfArrivalTimes));

				//Add the time to a list
				listOfArrivalTimes.Add(arrivalTime);

				//Generate processing time using negative exponential function
				processingTime = new TimeSpan(0, 0, (int)(MinimumProcessTime + NegExp((double)ExpectedProcessingTime - MinimumProcessTime)));

				//Set the arrival event properties

				arrivalEvent.Type = EventType.ARRIVAL;
				arrivalEvent.Time = TimeWeOpen.Add(arrivalTime);
				arrivalEvent.Person = new Registrant(arrivalEvent, processingTime);
				ArrivalEvents.Enqeue(arrivalEvent);
				
			}
		}


		/// <summary>
		/// Simulates this instance.
		/// </summary>
		public void Simulate()
		{
			int NumberOfRegistrantsInShotestLine = 0;                       //Number of registrants in line currently
			int NumberOfRegistrantsInLine = 0;
			TimeSpan zero = new TimeSpan(0, 0, 0);                          //Will be used to check if a stopwatch is zero
			for (int i = 0; i < StopWatches.Count; i++)						//Set all stopwatches to zero
				StopWatches[i] = zero;

			int numberOfArrivals=0;
			int numberOfDepartures=0;

			TimeSpan oneSecond = new TimeSpan(0, 0, 1);						//One seccond -- will be used to add a second to CurrentTime
			DateTime CurrentTime = new DateTime(2015,11,15,8, 0, 0);		//Starts at 8 am -- the current time
            bool run = true;

			do
			{
				CurrentTime = CurrentTime.Add(new TimeSpan(0, 0, 1)); //timer ticks one second
				int shortestLine = FindShortestLine();   //find the shortest line

				if (ArrivalEvents.Count > 0)
				{
					//Console.WriteLine(ArrivalEvents.Peek().ToString());
					if (ArrivalEvents.Peek().Time.Equals(CurrentTime))   //Arrival event takes place
					{
						NumberOfRegistrantsInLine++;

						if (Lines[shortestLine].Count == 0)   //if noboday is in the line
							StopWatches[shortestLine] = ArrivalEvents.Peek().Person.ProcessingTime;  //set stopwatch to the processing time of a new person

						Lines[shortestLine].Enqueue(ArrivalEvents.Peek().Person);//add the person to the shortest line
						
						ArrivalEvents.Dequeue();  //Remove the event from the Events Queue

						numberOfArrivals++;
					}
				}

				for (int i = 0; i < StopWatches.Capacity; i++)    //Lines.Count = StopWatch.Count
				{
					if (StopWatches[i] > zero)
					{
						StopWatches[i] = StopWatches[i].Subtract(oneSecond); 
					}
					if (StopWatches[i] <= zero && Lines[i].Count > 0)  //if stopwatch beeps and the line has somebody
					{
						StopWatches[i] = zero;

						NumberOfRegistrantsInLine--;

						numberOfDepartures++;

						Lines[i].Dequeue();    //finish the person and dequeue the line

						//Console.WriteLine("Person Removed");

						if (Lines[i].Count > 0)  //if somebody still in the line
						{
							StopWatches[i] = Lines[i].Peek().ProcessingTime;    //set stop watch to the processing time of the first person
						}
					}
				} 

                DisplayLines(CurrentTime);
                DisplayWatch( numberOfArrivals, numberOfDepartures);

                NumberOfRegistrantsInShotestLine = Lines[shortestLine].Count;
				TrackLineLength();
                Thread.Sleep(100);

                //Console.ReadLine();

                //Stop run if the total number of registrants in all lines is 0 and the current time is greater than closing time
                if (NumberOfRegistrantsInLine == 0 && CurrentTime > TimeWeClose)
					run = false;
			} while (run);
        }

		/// <summary>
		/// Find the shortest line
		///   If there is a tie, return the smaller first index
		/// </summary>
		/// <returns>Index of line</returns>
		private int FindShortestLine()
		{
			int index = 0;

			for (int i = 0; i < Lines.Count - 1; i++)
			{
				//Console.WriteLine(Lines[i].Count + "<" + Lines[i + 1].Count);

				if (Lines[i].Count > Lines[i + 1].Count)
				{
					index = i+1;
				}
			}
			return index;
		}
        
        /// <summary>
        /// Track the maximum length of registration lines
        /// </summary>
        /// <returns></returns>
        public void TrackLineLength()
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                if (Lines[i].Count>MaxLineLength[i])
                {
                    MaxLineLength[i] = Lines[i].Count;
                }
            }
        }

        /// <summary>
        /// Keep track the longest lines during the simulation
        /// </summary>
        /// <returns>length of longest lines in the simulation</returns>
        public int GetLongestLineLength()
        {
            int max = MaxLineLength[0];

            for (int i = 0; i < MaxLineLength.Count - 1; i++)
            {
                if (MaxLineLength[i] < MaxLineLength[i + 1])
                {
                    max = i + 1;
                }
            }
            return max;
        }

        /// <summary>
        /// Check whether the generated arrival time is unique 
        /// </summary>
        /// <param name="arrivalTime">randomly generated arrival time</param>
        /// <param name="arrivalTimes">the list of randomly generated arrival time</param>
        /// <returns>true if arrival time is unique; false if not unique </returns>
        public bool UniqueArrivalTime(TimeSpan arrivalTime, List<TimeSpan> arrivalTimes)
        {
            foreach (TimeSpan at in arrivalTimes)
            {
                if (at.CompareTo(arrivalTime) == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Display the length of registration lines
        /// </summary>
        /// <returns></returns>
        public void DisplayLines(DateTime currentTime)
		{
			Console.WriteLine();
			Console.Write(" \n");
            Console.Write("Current time: "+currentTime+"\n");
			Console.WriteLine("Closing time: " + TimeWeClose);

			Console.WriteLine();

			int k = 1;

			foreach (Queue<Registrant> line in Lines)
			{
				Console.Write("Line "+ k + "     ");
				k++;
			}

			Console.Write(" \n");

			for (int i = 0; i < Lines.Count; i++)
			{
				Console.Write(Lines[i].Count + "          ");
			}
			Console.Write("|| Number of people in line");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Display the stopwatch for each line
        /// </summary>
        /// <returns></returns>
        public void DisplayWatch( int numberOfArrivals, int numberOfDepartures)
        {


			for (int i = 0; i < StopWatches.Count; i++)
            {
                Console.Write(StopWatches[i].ToString() + "   ");
            }

			Console.Write("|| Time left for person in front to process");

			Console.WriteLine("\n\nArrivals: " + numberOfArrivals + 
				"    Departures: " + numberOfDepartures + 
				"    Longest line so far: " + GetLongestLineLength());
        }

        /// <summary>
        /// Generate random number that follows negative exponential distribution
        /// </summary>
        /// <param name="ExpectedValue">Expection value of exponential distribution</param>
        /// <returns> value of random number</returns>
        private double NegExp(double ExpectedValue)
		{
			return -ExpectedValue * Math.Log(R.NextDouble(), Math.E);
		}

        /// <summary>
        /// Generate random number that follows Poisson distribution
        /// </summary>
        /// <param name="ExpectedValue">Expection value of exponential distribution</param>
        /// <returns> value of random number</returns>
        private int Poisson(double ExpectedValue)
		{
			double dLimit = -ExpectedValue;
			double dSum = Math.Log(R.NextDouble());

			int Count;
			for (Count = 0; dSum > dLimit; Count++)
				dSum += Math.Log(R.NextDouble());

			return Count;
		} 
	}
}