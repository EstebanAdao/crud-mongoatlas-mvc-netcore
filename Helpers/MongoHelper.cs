using Crud_MVC_MongoDB.Models;
using MongoDB.Driver;
using System;

namespace Crud_MVC_MongoDB.Helpers
{
	public class MongoHelper<T> where T : class
	{
		private static IMongoClient _client { get; set; }
		private static readonly string MongoConnection = "mongodb+srv://crud_user:crud_user@cluster0-6tpw5.gcp.mongodb.net/test?retryWrites=true&w=majority";
		private static readonly string MongoDataBase = "crud_mongo";
		public static IMongoCollection<Student> _studentsCollection { get; set; }

		public static IMongoCollection<T> Connect(string collectionName)
		{
			try
			{
				_client = new MongoClient(MongoConnection);
				return _client.GetDatabase(MongoDataBase).GetCollection<T>(collectionName);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
