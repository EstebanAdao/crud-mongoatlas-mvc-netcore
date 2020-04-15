using System;

namespace Crud_MVC_MongoDB.Models
{
	public class Student
	{
		public Object _id { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string emailAddress { get; set; }
	}
}
