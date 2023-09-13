using System.ComponentModel.DataAnnotations;

namespace RPGClient.Models.Character;

public class UpdateCharacterDto
{
    [Required]
    [StringLength(55, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Range(0,200)]
    public float Health { get; set; }
    
    [Required]
    [Range(0,200)]
    public float Strength { get; set; }
    
    [Required]
    [Range(0,200)]
    public float Attack { get; set; }
    
    [Required]
    [Range(0,200)]
    public float Defence { get; set; }
    
    public CharacterType Type { get; set; }
}