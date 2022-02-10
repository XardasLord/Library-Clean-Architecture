using System;
using Library.Domain.SharedKernel;

namespace Library.Infrastructure.Time
{
    public class CurrentDateTime : ICurrentDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}