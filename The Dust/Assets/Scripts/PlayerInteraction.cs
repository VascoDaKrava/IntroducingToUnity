using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool _isFire1;
    private bool _isFire2;
    private bool _weaponActive = true;
    private int _quantityBullets = 0;

    private LootClass _loot;
    private HealthController _healthController;

    private List<LootClass.LootTypes> _inventory = new List<LootClass.LootTypes>();
    private List<LootClass.WeaponNames> _weaponsList = new List<LootClass.WeaponNames>();

    // Start is called before the first frame update
    void Start()
    {
        // Add own trigger-collider to Global List
        GameObject.FindGameObjectWithTag("GlobalScript").GetComponent<Storage>().TriggerColliderList.Add(GetComponent<BoxCollider>().GetHashCode());

        _healthController = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        if (_weaponActive) LetFire();
    }

    private void getInput()
    {
        _isFire1 = Input.GetButton("Fire1");
        _isFire2 = Input.GetButtonDown("Fire2");
    }

    private void LetFire()
    {
        if (_isFire1) Debug.Log("Fire 1");
        if (_isFire2)
        {
            Debug.Log("Fire 2");
            Debug.Log(Time.time + " Health\t\t: " + GetComponent<HealthController>().Health);
            Debug.Log(Time.time + " Armor\t\t: " + GetComponent<HealthController>().Armor);
            Debug.Log(Time.time + " Bullet\t\t: " + _quantityBullets);
            Debug.Log(Time.time + " Key\t\t: " + _inventory.Contains(LootClass.LootTypes.Key));
            Debug.Log(Time.time + " Weapon\t\t: " + _inventory.Contains(LootClass.LootTypes.Weapon));
            Debug.Log(Time.time + " \tC4\t: " + _weaponsList.Contains(LootClass.WeaponNames.C4));
            Debug.Log(Time.time + " \tAK74\t: " + _weaponsList.Contains(LootClass.WeaponNames.AK74));

        }
    }

    /// <summary>
    /// Object contacter
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PlayerInteraction - Find " + other.name);
    }


    public void GetLoot(GameObject pickUpedLootObj)
    {
        _loot = pickUpedLootObj.GetComponent<LootModel>().Loot;

        switch (_loot.LootType)
        {
            case LootClass.LootTypes.Aim:
                _healthController.ChangeHealth(_loot.LootPower);
                break;

            case LootClass.LootTypes.Ammo:
                _quantityBullets += _loot.QuantityBullets;
                break;

            case LootClass.LootTypes.Armor:
                _healthController.ChangeArmor(_loot.LootPower);
                break;

            case LootClass.LootTypes.Key:
                _inventory.Add(_loot.LootType);
                break;

            case LootClass.LootTypes.Weapon:
                if (_loot.WeaponName == LootClass.WeaponNames.C4)
                {
                    if (!_inventory.Contains(_loot.LootType)) _inventory.Add(_loot.LootType);
                    _weaponsList.Add(_loot.WeaponName);
                }
                else
                {
                    if (!_inventory.Contains(_loot.LootType)) _inventory.Add(_loot.LootType);
                    if (!_weaponsList.Contains(_loot.WeaponName)) _weaponsList.Add(_loot.WeaponName);
                }
                break;

            default:
                break;
        }

        Debug.Log("PlayerInteraction - Get " + pickUpedLootObj.name);
        Destroy(pickUpedLootObj);
    }
}
