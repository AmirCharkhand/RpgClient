﻿using RPGClient.Models.Weapon;

namespace RPGClient.Models.Character;

public class GetCharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Health { get; set; }
    public float Strength { get; set; }
    public float Attack { get; set; }
    public float Defence { get; set; }
    public CharacterType Type { get; set; }
    public int Fights { get; set; }
    public int Victories { get; set; }
    public int Defeats { get; set; }
    public GetWeaponDto? Weapon { get; set; }
}