using System;
using System.Collections.Generic;
using BitPoker.Models.Messages;
using LiteDB;

namespace BitPoker.Repository.LiteDB
{
	public class ActionMessgeRepository : BaseRepository, Repository.IMessagesRepository
	{
		public ActionMessgeRepository(String filePath)
		{
			_filePath = filePath;
		}

		public void Add(ActionMessage entity)
		{
			using (var db = new LiteDatabase(_filePath))
			{
				var entities = db.GetCollection<ActionMessage>("actions");
				entities.Insert(entity);
			}
		}

		public IEnumerable<ActionMessage> All()
		{
			using (var db = new LiteDatabase(_filePath))
			{
				var entities = db.GetCollection<ActionMessage>("actions");
				return entities.FindAll();;
			}
		}

		public void Delete(ActionMessage entity)
		{
			throw new NotImplementedException();
		}

		public ActionMessage Find(string id)
		{
			throw new NotImplementedException();
		}

		public void Update(ActionMessage entity)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
		}
	}
}
