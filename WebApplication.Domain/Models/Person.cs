namespace WebApplication.Domain.Models
{
    public class Person : Entity
    {
        public Person(string name, string idNumber)
        {
            Name = name;
            IdNumber = idNumber;
            VersionUpdateGuid = Guid.NewGuid();
        }

        private Person()
        {
        }

        public string Name { get; set; }

        public string IdNumber { get; set; }

        public Guid VersionUpdateGuid { get; set; }

        public List<Licence> Licences { get; set; } = new();

        public List<Car> Cars { get; set; } = new();
    }
}
