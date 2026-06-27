namespace AuthLab.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IReadOnlyCollection<string> Errors { get; }

        public ValidationException(IEnumerable<string> errors)
            : base("Validation failed.")
        {
            Errors = errors.ToList().AsReadOnly();
        }
    }
}
