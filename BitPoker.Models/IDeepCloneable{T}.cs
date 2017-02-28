namespace BitPoker.Models
{
    public interface IDeepCloneable<out T>
    {
        T DeepClone();
    }
}
