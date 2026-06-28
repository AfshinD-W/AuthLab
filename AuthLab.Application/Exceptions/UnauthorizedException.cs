namespace AuthLab.Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public IReadOnlyCollection<string> Errors { get; }

        public UnauthorizedException(IEnumerable<string> errors)
            : base("Authorize failed.")
        {
            Errors = errors.ToList().AsReadOnly();
        }
    }
}
