namespace Library.Domain.Exceptions
{
    public class LoanNotActiveException : DomainException
   {
       protected override string Code => "loan_is_not_active";
       public long LoanId { get; }

       public LoanNotActiveException(long loanId) : base($"Loan with ID {loanId} is not active.") 
            => LoanId = loanId;
   }
}
