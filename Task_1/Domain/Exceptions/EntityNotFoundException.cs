namespace Task_1.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message)
        : base(message)
    { }
    }
}