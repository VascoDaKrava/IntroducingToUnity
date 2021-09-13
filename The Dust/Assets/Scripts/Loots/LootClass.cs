public class LootClass
{
    #region Enums

    public enum LootTypes
    {
        NULL,
        Aid,
        Ammo,
        Armor,
        Key,
        Weapon
    }

    public enum WeaponNames
    {
        NULL,
        AK74,
        C4
    }

    #endregion

    #region Private fields

    private LootTypes _loot = LootTypes.NULL;
    private int _power = 0;
    private WeaponNames _weaponName = WeaponNames.NULL;
    private int _quantityBullets = 0;

    #endregion

    #region Public properties

    public LootTypes LootType { get { return _loot; } }

    public int LootPower { get { return _power; } }

    public WeaponNames WeaponName { get { return _weaponName; } }

    public int QuantityBullets { get { return _quantityBullets; } }

    #endregion

    #region Constructors
    
    /// <summary>
    /// Loot for keys
    /// </summary>
    /// <param name="loot"></param>
    public LootClass(LootTypes loot)
    {
        _loot = loot;
    }

    /// <summary>
    /// Loot for aid, armor
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

    #endregion
}
