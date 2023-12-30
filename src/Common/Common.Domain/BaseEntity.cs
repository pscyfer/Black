namespace Common.Domain
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifyDate { get; set; }

        public BaseEntity()
        {
            CreationDate = DateTime.Now;
            Id = SequentialGuidGenerator.GenerateNewGuid(); 
        }
    }
}