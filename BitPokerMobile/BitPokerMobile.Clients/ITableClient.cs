using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitPokerMobile.Clients
{
	public interface ITableClient : IDisposable
	{
		Task<IEnumerable<PCL.Models.TableInfo>> GetTablesAsync();
	}
}