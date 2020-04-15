using Crud_MVC_MongoDB.Helpers;
using Crud_MVC_MongoDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_MVC_MongoDB.Controllers
{
	public class StudentController : Controller
	{
		private static readonly Random random = new Random();
		private IMongoCollection<Student> _collectionStudent { get; set; } = null;

		// GET: Student
		public async Task<IActionResult> Index(string firstName = "", string lastName = "", string emailAddress = "")
		{
			ViewData["firstName"] = firstName;
			ViewData["lastName"] = lastName;
			ViewData["emailAddress"] = emailAddress;

			StartMongo();

			Dictionary<string, string> dictionaryFilter = new Dictionary<string, string>();

			if (!string.IsNullOrEmpty(firstName))
			{
				dictionaryFilter.Add("firstName", firstName);
			}
			if (!string.IsNullOrEmpty(lastName))
			{
				dictionaryFilter.Add("lastName", lastName);
			}
			if (!string.IsNullOrEmpty(emailAddress))
			{
				dictionaryFilter.Add("emailAddress", emailAddress);
			}

			var filter = dictionaryFilter.Count > 0 ? new BsonDocument(dictionaryFilter) : Builders<Student>.Filter.Empty;
			var result = await _collectionStudent.Find(filter).ToListAsync();

			return View(result);
		}

		// GET: Student/Details/5
		public async Task<IActionResult> Details(string id)
		{
			StartMongo();

			var filter = Builders<Student>.Filter.Eq("_id", id);
			var result = await _collectionStudent.Find(filter).FirstOrDefaultAsync();

			return View(result);
		}

		// GET: Student/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Student/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(IFormCollection collection)
		{
			try
			{
				StartMongo();

				var _id = GenerateRandomId(24);

				await _collectionStudent.InsertOneAsync(
						new Student
						{
							_id = _id,
							firstName = collection["firstName"],
							lastName = collection["lastName"],
							emailAddress = collection["emailAddress"],
						}
					);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		private object GenerateRandomId(int v)
		{
			var strAray = "abcdefghijlmnopqrstuvwxyz123456789";
			return new string(Enumerable.Repeat(strAray, v).Select(s => s[random.Next(s.Length)]).ToArray());
		}

		// GET: Student/Edit/5
		public async Task<IActionResult> Edit(string id)
		{
			StartMongo();

			var filter = Builders<Student>.Filter.Eq("_id", id);
			var result = await _collectionStudent.Find(filter).FirstOrDefaultAsync();

			return View(result);
		}

		// POST: Student/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, IFormCollection collection)
		{
			try
			{
				StartMongo();

				var filter = Builders<Student>.Filter.Eq("_id", id);
				var update = Builders<Student>.Update
					.Set("firstName", collection["firstName"])
					.Set("lastName", collection["LastName"])
					.Set("emailAddress", collection["emailAddress"]);

				await _collectionStudent.UpdateOneAsync(filter, update);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: Student/Delete/5
		public async Task<IActionResult> Delete(string id)
		{
			StartMongo();

			var filter = Builders<Student>.Filter.Eq("_id", id);
			var result = await _collectionStudent.Find(filter).FirstOrDefaultAsync();

			return View(result);
		}

		// POST: Student/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(string id, IFormCollection collection)
		{
			try
			{
				StartMongo();

				var filter = Builders<Student>.Filter.Eq("_id", id);
				await _collectionStudent.DeleteOneAsync(filter);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		public async Task<IActionResult> CreateMany()
		{
			try
			{
				StartMongo();

				var listStudents = new List<Student>
				{
					new Student{ _id = GenerateRandomId(24),firstName ="Cinthya",lastName = "Perea",emailAddress = "emailAddress1@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Paul",lastName = "Calmet",emailAddress = "emailAddress2@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Stefano",lastName = "Calmet",emailAddress = "emailAddress3@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Ian",lastName = "Calmet",emailAddress = "emailAddress4@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Ysabel",lastName = "Perea",emailAddress = "emailAddress5@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Esteban",lastName = "Cerna",emailAddress = "emailAddress6@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Elba Ysabel",lastName = "Perea Garate",emailAddress = "emailAddress7@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Camila",lastName = "Costa",emailAddress = "emailAddress8@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Juliana",lastName = "Brito",emailAddress = "emailAddress9@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Julia",lastName = "Leonidas",emailAddress = "emailAddress10@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Rosemary",lastName = "Oliveira",emailAddress = "emailAddress11@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Wilker",lastName = "Da Silva",emailAddress = "emailAddress12@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Anselmo Julio",lastName = "Mondragon Melendez",emailAddress = "emailAddress13@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Maria Jesus",lastName = "Cerna Rojas",emailAddress = "emailAddress14@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Natalia",lastName = "Mondragon",emailAddress = "emailAddress15@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Anselmo Julio",lastName = "Mondragon",emailAddress = "emailAddress17@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Anselmo",lastName = "Cerna",emailAddress = "emailAddress18@gmail.com"},
					new Student{ _id = GenerateRandomId(24),firstName ="Carmen",lastName = "Cerna",emailAddress = "emailAddress19@gmail.com"},
				};

				await _collectionStudent.InsertManyAsync(listStudents);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		private void StartMongo()
		{
			if (_collectionStudent == null)
				_collectionStudent = MongoHelper<Student>.Connect("students");
		}
	}
}