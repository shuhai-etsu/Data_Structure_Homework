//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Simulating conference registration with Queues and Priority Queues
//	File Name:		Event.cs
//	Description:	An event of Convention Registraction
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

namespace Project4
{
	/// <summary>
	/// An event of convention registraction
	/// </summary>
	public class Event : IComparable
	{
		public DateTime Time { get; set; }		   //Time of event
		public Registrant Person { get; set; }	   //The registrant associated with event
		public EventType Type { get; set; }		   //The type of event: ARRIVAL or DEPARTURE
		
        /// <summary>
		/// Base constructor
		/// </summary>
		public Event()
		{
			
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="type">EventType</param>
		/// <param name="time">DateTime</param>
		/// <param name="registrant">Registrant</param>
		public Event(EventType type, DateTime time, Registrant registrant)
		{
			Time = time;
			Type = type;
			Person = registrant;
		}

		/// <summary>
		/// Conpare two Events
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>positive if greater than, 
		/// negative is less than, and zero if the same</returns>
		int IComparable.CompareTo(object obj)
		{
			if(!(obj is Event))
				throw new NotImplementedException();

			Event e = (Event)obj;

			return (e.Time.CompareTo(Time));
		}

		/// <summary>
		/// Print the Event 
		/// </summary>
		/// <returns></returns>
		public override String ToString()
		{
			return String.Format(Type+"  "+Time.ToString());
		}
	}
}
