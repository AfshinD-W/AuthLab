namespace AuthLab.Application.Exceptions
{
    public class BusinessException : Exception
    {
        public IReadOnlyCollection<string> Errors { get; }

        public BusinessException(IEnumerable<string> errors)
            : base("Job failed.")
        {
            Errors = errors.ToList().AsReadOnly();
        }
    }
}
