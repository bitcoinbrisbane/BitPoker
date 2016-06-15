using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.API.Repository
{
    public class Factory
    {
        public static BitPoker.Repository.IPlayerRepository GetPlayerRepository()
        {
            String repoName = System.Configuration.ConfigurationManager.AppSettings["PlayerRepository"];

            if (!String.IsNullOrEmpty(repoName))
            {
                BitPoker.Repository.IPlayerRepository repo = (BitPoker.Repository.IPlayerRepository)Activator.CreateInstance(Type.GetType(repoName));

                if (repo != null)
                {
                    return repo;
                }
                else
                {
                    return new InMemoryPlayerRepo();
                }
            }
            else
            {
                return new InMemoryPlayerRepo();
            }
        }

        public static BitPoker.Repository.ITableRepository GetTableRepository()
        {
            String repoName = System.Configuration.ConfigurationManager.AppSettings["TableRepository"];

            if (!String.IsNullOrEmpty(repoName))
            {
                //String path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                //System.Reflection.Assembly assembly = AppDomain.CurrentDomain.Load(File.ReadAllBytes(path + @"/bin/Debug/BitPoker.Repository.dll"));
                //BitPoker.Repository.ITableRepository repo = (BitPoker.Repository.ITableRepository)assembly.CreateInstance(repoName); //Type.GetType(

                if (repoName.Contains("Mock"))
                {
                    return new BitPoker.Repository.MockTableRepo();
                }
                else
                {
                    return new InMemoryTableRepo();
                }
            }
            else
            {
                return new InMemoryTableRepo();
            }
        }

        public static BitPoker.Repository.ITableRepository GetHandRepository()
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
                    return new InMemoryTableRepo();
                }
            }
            else
            {
                return new InMemoryTableRepo();
            }
        }

        private static T GetRepo<T>(String repoName)
        {
            T p = (T)Activator.CreateInstance(Type.GetType(repoName));
            return p;
        }
    }
}
