namespace GamesApi.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int HitPoints { get; set; } 
        public int Strength { get; set; }
        public int Defence { get; set; }
        public int Intelligence { get; set; } 
        public RpgClass Class { get; set; }
        public User? User { get; set; }
        public Weapon Weapon { get; set; }
        public List<Skill> Skills { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}
