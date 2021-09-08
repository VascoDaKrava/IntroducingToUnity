using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool _isFire1 = false;
    private bool _isFire2 = false;
    private bool _showStat = false;
    private bool _weaponActive = false;

    private int _quantityBullets = 0;

    private Transform _weaponPositionTransform;

    private GameObject _weaponObj;

    private WeaponController _weaponController;

    private LootClass _loot;
    private HealthController _healthController;
    private Storage _storage;

    private List<LootClass.LootTypes> _inventoryList = new List<LootClass.LootTypes>();
    private List<LootClass.WeaponNames> _weaponsList = new List<LootClass.WeaponNames>();

    public List<LootClass.LootTypes> Inventory { get { return _inventoryList; } }

    // Start is called before the first frame update
    void Start()
    {
        // Add own trigger-collider to Global List
        _storage = GameObject.FindGameObjectWithTag("GlobalScript").GetComponent<Storage>();
        _storage.TriggerColliderList.Add(GetComponent<BoxCollider>().GetHashCode());

        _weaponPositionTransform = Storage.FindTransformInChildrenWithTag(gameObject, Storage.WeaponPositionTag);

        _healthController = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        if (_weaponActive) LetFire();

        if (_showStat)
        {
            Debug.Log(Time.time + " Health\t\t: " + GetComponent<HealthController>().Health);
            Debug.Log(Time.time + " Armor\t\t: " + GetComponent<HealthController>().Armor);
            Debug.Log(Time.time + " Bullet\t\t: " + _quantityBullets);
            Debug.Log(Time.time + " Key\t\t: " + _inventoryList.Contains(LootClass.LootTypes.Key));
            Debug.Log(Time.time + " Weapon\t\t: " + _inventoryList.Contains(LootClass.LootTypes.Weapon));
            Debug.Log(Time.time + " \tC4\t: " + _weaponsList.Contains(LootClass.WeaponNames.C4));
            Debug.Log(Time.time + " \tAK74\t: " + _weaponsList.Contains(LootClass.WeaponNames.AK74));
            Debug.Log(Time.time + " \tAK74 clip fill\t: " + _weaponController.ClipFullness);
        }
    }


    private void getInput()
    {
        _isFire1 = Input.GetButton("Fire1");
        _isFire2 = Input.GetButtonDown("Fire2");
        _showStat = Input.GetButton("ShowStat");
    }

    private void LetFire()
    {
        if (_isFire1) Fire1();
        if (_isFire2) Fire2();
    }

    private void Fire1()
    {
        Storage.ToLog(this, Storage.GetCallerName(), "Try Fire 1");
        if (_weaponController.ReadyForFire)
            _weaponController.Fire();
    }

    private void Fire2()
    {
        Debug.Log("Fire 2");
    }

    /// <summary>
    /// Object contacter
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Storage.ToLog(this, Storage.GetCallerName(), "Find " + other.name);
    }

    /// <summary>
    /// Recieve pickUpedLootObj
    /// </summary>
    /// <param name="pickUpedLootObj">Link to pickuped Object</param>
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
                if (_weaponActive)
                {
                    _weaponController.ClipFullness = _quantityBullets;
                    _quantityBullets = 0;
                }
                break;

            case LootClass.LootTypes.Armor:
                _healthController.ChangeArmor(_loot.LootPower);
                break;

            case LootClass.LootTypes.Key:
                _inventoryList.Add(_loot.LootType);
                break;

            case LootClass.LootTypes.Weapon:
                if (_loot.WeaponName == LootClass.WeaponNames.C4)
                {
                    if (!_inventoryList.Contains(_loot.LootType)) _inventoryList.Add(_loot.LootType);
                    _weaponsList.Add(_loot.WeaponName);
                }
                else
                {
                    if (!_inventoryList.Contains(_loot.LootType)) _inventoryList.Add(_loot.LootType);
                    if (!_weaponsList.Contains(_loot.WeaponName)) _weaponsList.Add(_loot.WeaponName);

                    _weaponActive = true;

                    _weaponObj = Storage.FindTransformInChildrenWithTag(pickUpedLootObj, Storage.WeaponTag).gameObject;

                    _weaponController = _weaponObj.GetComponent<WeaponController>();

                    _weaponObj.transform.SetParent(_weaponPositionTransform, false);
                    _weaponObj.transform.localPosition = Vector3.zero;
                    _weaponObj.transform.localRotation = Quaternion.identity;
                    _weaponObj.transform.localScale = Vector3.one;

                    // Load bullets to weapon clip
                    _weaponController.ClipFullness = _quantityBullets;
                    _quantityBullets = 0;
                }
                break;

            default:
                break;
        }

        Storage.ToLog(this, Storage.GetCallerName(), "Get " + pickUpedLootObj.name);
        Destroy(pickUpedLootObj);
    }

    /// <summary>
    /// Remove item from inventory
    /// </summary>
    /// <param name="item">Loot</param>
    public void RemoveFromInventory(LootClass.LootTypes item)
    {
        _inventoryList.Remove(item);
    }

    /// <summary>
    /// Remove item from inventory
    /// </summary>
    /// <param name="item">Weapon</param>
    public void RemoveFromInventory(LootClass.WeaponNames item)
    {
        _weaponsList.Remove(item);
    }

    /// <summary>
    /// Check item in inventory
    /// </summary>
    /// <param name="item">Loot</param>
    /// <returns></returns>
    public bool IsInInventory(LootClass.LootTypes item)
    {
        return _inventoryList.Contains(item);
    }

    /// <summary>
    /// Check item in inventory
    /// </summary>
    /// <param name="item">Weapon</param>
    /// <returns></returns>
    public bool IsInInventory(LootClass.WeaponNames item)
    {
        return _weaponsList.Contains(item);
    }
}
