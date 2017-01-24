using System;

namespace BitPoker.Repository
{
    public interface IMessagesRepository : IGenericRepository<Models.Messages.ActionMessage>, IDisposable
    {
    }
}
