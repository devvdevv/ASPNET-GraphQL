namespace WebApplication.Domain.Models
{
    public class Car : Entity
    {
        public Car(string name, string vin, Guid versionGuid, string brand, Person owner)
        {
            Name = name;
            Vin = vin;
            VersionGuid = versionGuid;
            Brand = brand.ToUpper();
            Owner = owner;
        }

        private Car()
        {

        }

        public string Name { get; }

        public string Vin { get; set; }

        public Guid VersionGuid { get; set; }

        public string Brand { get; }

        public Person Owner { get; set; }
    }
}
