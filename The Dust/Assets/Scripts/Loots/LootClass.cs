public class LootClass
{
    public enum LootTypes
    {
        Aim,
        Ammo,
        Armor,
        Key,
        Weapon
    }

    public enum WeaponNames
    {
        AK74,
        C4
    }

    private LootTypes _loot;
    private int _power;
    private WeaponNames _weaponName;
    private int _quantityBullets;

    public LootTypes LootType { get { return _loot; } }

    public int LootPower { get { return _power; } }

    /// <summary>
    /// Loot for keys
    /// </summary>
    /// <param name="loot"></param>
    public LootClass(LootTypes loot)
    {
        _loot = loot;
    }

    /// <summary>
    /// Loot for aim, armor
    /// </summary>
    /// <param name="loot"></param>
    /// <param name="power"></param>
    public LootClass(LootTypes loot, int power)
    {
        _loot = loot;
        _power = power;
    }

    /// <summary>
    /// Loot for weapon
    /// </summary>
    /// <param name="loot"></param>
    public LootClass(LootTypes loot, WeaponNames weapon)
    {
        _loot = loot;
        _weaponName = weapon;
    }

    /// <summary>
    /// Loot for ammo
    /// </summary>
    /// <param name="loot"></param>
    /// <param name="weapon"></param>
    /// <param name="quantityBullets"></param>
    public LootClass(LootTypes loot, WeaponNames weapon, int quantityBullets)
    {
        _loot = loot;
        _weaponName = weapon;
        _quantityBullets = quantityBullets;
    }
}
