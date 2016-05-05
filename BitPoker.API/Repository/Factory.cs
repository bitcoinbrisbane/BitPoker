using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.API.Repository
{
    public class Factory
    {
        public static BitPoker.Repository.ITableRepository GetTableRepository()
        {
            String repoName = System.Configuration.ConfigurationManager.AppSettings["TableRepository"];

            if (!String.IsNullOrEmpty(repoName))
            {
                BitPoker.Repository.ITableRepository repo = (BitPoker.Repository.ITableRepository)Activator.CreateInstance(Type.GetType(repoName));

                if (repo != null)
                {
                    return repo;
                }
                else
                {
                    return new Repository.InMemoryTableRepo();
                }
            }
            else
            {
                return new Repository.InMemoryTableRepo();
            }
        }

        private static T GetRepo<T>(String repoName)
        {
            T p = (T)Activator.CreateInstance(Type.GetType(repoName));
            return p;
        }
    }
}
