using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootModel : MonoBehaviour
{
    private LootClass _loot;

    [SerializeField] private LootClass.LootTypes _lootType;
    [SerializeField] private int _power;
    [SerializeField] private LootClass.WeaponNames _weaponName;
    [SerializeField] private int _quantityBullets;

    public LootClass Loot { get { return _loot; } }

    // Start is called before the first frame update
    void Start()
    {
        switch (_lootType)
        {
            case LootClass.LootTypes.Aid:
                _loot = new LootClass(_lootType, _power);
                break;
            
            case LootClass.LootTypes.Ammo:
                _loot = new LootClass(_lootType, _weaponName, _quantityBullets);
                break;
            
            case LootClass.LootTypes.Armor:
                _loot = new LootClass(_lootType, _power);
                break;
            
            case LootClass.LootTypes.Key:
                _loot = new LootClass(_lootType);
                break;
            
            case LootClass.LootTypes.Weapon:
                _loot = new LootClass(_lootType, _weaponName);
                break;
            
            default:
                break;
        }
    }
}
