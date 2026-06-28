namespace AuthLab.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public IReadOnlyCollection<string> Errors { get; }

        public NotFoundException(IEnumerable<string> errors)
            : base("Nothing Found.")
        {
            Errors = errors.ToList().AsReadOnly();
        }
    }
}
