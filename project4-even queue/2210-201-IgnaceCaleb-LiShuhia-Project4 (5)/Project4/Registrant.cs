//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Simulating conference registration with Queues and Priority Queues
//	File Name:		Registrant.cs
//	Description:	registrant class that defines convention registrants.
//	Course:			CSCI 2210-201 - Data Structures
//	Author:			Caleb Ignace & Shuhai Li; ignacec@goldmail.etsu.edu  & lis002@goldmail.etsu.edu
//	Created:		Friday, November 13, 2015
//	Copyright:		Caleb Ignace & Shuhai Li, 2015
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4
{

	/// <summary>
	/// The registrant class -- each registrant is associated with two events
	/// </summary>
	public class Registrant
	{
		public Event Arrival { get; set; }		   //The arrival of the registrant
		public Event Departure { get; set; }        //The departure of the registrant

		public TimeSpan ProcessingTime { get; set; }       //The time it take for the 


		/// <summary>
		/// Basic constructor
		/// </summary>
		public Registrant()
		{
            Arrival = null;
            Departure = null;
            ProcessingTime = new TimeSpan(0,0,255); 
		}


		/// <summary>
		/// Parameterized constructor 
		/// </summary>
		/// <param name="arrival">The arrival.</param>
		/// <param name="processingTime">The processing time.</param>
		public Registrant(Event arrival, TimeSpan processingTime)
		{
			Arrival = arrival;
			ProcessingTime = processingTime;
		}


		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		public override String ToString()
        {
            return String.Format("Arrival Time="+Arrival.Time.ToString()+ "Processing Time="+ProcessingTime.ToString());
        }
    }
}
