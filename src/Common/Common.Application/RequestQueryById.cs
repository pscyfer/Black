namespace Common.Application
{
    public record RequestQueryById(Guid Identifier)
    {
        public override string ToString()
        {
            return Identifier.ToString();
        }
    }
    public record RequestQueryById<T>(T Identifier)
    {
        public override string ToString()
        {
            return Identifier.ToString();
        }
    }
}


