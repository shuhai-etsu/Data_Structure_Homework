//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 5 - B-Trees
//	File Name:		INSERT.cs
//	Description:	A enumerated data type
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

namespace B_Tree
{
    /// <summary>
    /// A enumerated data type
    /// </summary>
    /// 
    public enum INSERT
    {
        DUPLICATE,
        SUCCESS,
        NEEDSPLIT
    }
}
