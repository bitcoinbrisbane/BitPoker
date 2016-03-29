using System;
namespace BitPoker.Models
{
    public interface IDeck
    {
        void New();

        void Shuffle();
    }
}
