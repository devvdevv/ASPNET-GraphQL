namespace LearnEFCore.Domain
{
    public class Fighter
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual List<Fighter>? Fighters { get; set; } = new();

        public virtual List<Fighter>? FighterMestres { get; set; } = new();
    }
}
