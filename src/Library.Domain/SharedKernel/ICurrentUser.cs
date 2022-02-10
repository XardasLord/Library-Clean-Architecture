namespace Library.Domain.SharedKernel
{
    public interface ICurrentUser
    {
        long UserId { get; }
    }
}