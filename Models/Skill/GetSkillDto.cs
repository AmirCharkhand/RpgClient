namespace RPGClient.Models.Skill;

public class GetSkillDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Damage { get; set; }
}