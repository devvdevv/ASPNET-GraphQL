namespace WebApplication.Domain.Models
{
    public class Licence : Entity
    {
        public Licence(Guid publicId, string level, bool active, Person owner)
        {
            PublicId = publicId;
            Level = level;
            Active = active;
            Owner = owner;
        }

        private Licence()
        {
        }

        public Guid PublicId { get; set; }

        public string Level { get; set; }

        public bool Active { get; set; }

        public string? Description { get; set; }

        public Person Owner { get; set; }
    }
}
