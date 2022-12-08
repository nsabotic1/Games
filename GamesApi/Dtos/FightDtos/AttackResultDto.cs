namespace GamesApi.Dtos.FightDtos
{
    public class AttackResultDto
    {
        public string Attacker { get; set; }
        public string Opponent { get; set; }
        public int AttackerHitPoints { get; set; }
        public int OpponentHitPoints { get; set; }
        public int Damage { get; set; }
    }
}
