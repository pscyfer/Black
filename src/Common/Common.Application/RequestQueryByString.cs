namespace Identity.Core.Dto.Shared
{
    public record RequestQueryByString(string Identifier)
    {
        public override string ToString()
        {
            return Identifier;
        }
    }
}


