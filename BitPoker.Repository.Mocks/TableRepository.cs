using System;
using System.Collections.Generic;
using BitPoker.Models.Contracts;

namespace BitPoker.Repository.Mocks
{
	public class TableRepository : ITableRepository
	{
		public TableRepository()
		{
		}

		public void Add(Table entity)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Table> All()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public Table Find(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Save()
		{
			throw new NotImplementedException();
		}

		public void Update(Table entity)
		{
			throw new NotImplementedException();
		}
	}
}
