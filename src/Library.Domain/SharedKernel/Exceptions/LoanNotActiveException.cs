﻿using Library.Domain.Exceptions;

namespace Library.Domain.SharedKernel.Exceptions
{
    public class LoanNotActiveException : DomainException
   {
       public override string Code => "loan_is_not_active";
       public long LoanId { get; }

       public LoanNotActiveException(long loanId) : base($"Loan with ID {loanId} is not active.") 
            => LoanId = loanId;
   }
}
