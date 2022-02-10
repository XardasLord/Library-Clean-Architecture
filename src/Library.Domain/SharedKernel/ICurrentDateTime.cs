using System;

namespace Library.Domain.SharedKernel
{
    public interface ICurrentDateTime
    {
        DateTime UtcNow { get; }
    }
}