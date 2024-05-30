namespace Common.Application.Exceptions
{
    public class BaseApplicationExceptions : Exception
    {
        public BaseApplicationExceptions()
        {

        }

        public BaseApplicationExceptions(string message) : base(message)
        {

        }
    }
}
