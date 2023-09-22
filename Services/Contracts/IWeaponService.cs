using RPGClient.Models.Weapon;

namespace RPGClient.Services.Contracts;

public interface IWeaponService
{
    Task AddWeapon(AddWeaponDto weaponDto);
}