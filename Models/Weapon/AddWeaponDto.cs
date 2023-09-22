using System.ComponentModel.DataAnnotations;

namespace RPGClient.Models.Weapon;

public class AddWeaponDto
{
    [Required]
    [StringLength(55)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Range(1,100)]
    public float Damage { get; set; }
    
    [Required]
    public int CharacterRef { get; set; }
}