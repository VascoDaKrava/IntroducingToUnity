using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _grenadePrefab;
    private GameObject _grenadeGameObject;
    private Rigidbody _grenadeRigidbody;
    private ExplosionController _grebadeExplosionController;
    private float _grenadeThrowForce = 7.5f;
    private float _grenadeTimer = 3f;
    private int _grenadeDamage = 50;
    private float _grenadeExplosionRadius = 8f;
    private float _grenadePushForce = 2000f;

    private int _quantityGrenadesInLoot = 3;
    private int _quantityGrenades = 0;

    private int _aidEffectTime = 4; // How much seconds aid work (add 1/_aidEffectTime of own power per second)

    private bool _isFire1 = false;
    private bool _isFire2 = false;
    private bool _showStat = false;
    private bool _weaponActive = false;

    private int _quantityBullets = 0;

    private Transform _weaponPositionTransform;
    private Transform _grenadeParentTransform;
    private Transform _playerHead;

    private GameObject _weaponObj;

    private WeaponController _weaponController;

    private LootClass _loot;
    private HealthController _healthController;

    private List<LootClass.LootTypes> _inventoryList = new List<LootClass.LootTypes>();
    private List<LootClass.WeaponNames> _weaponsList = new List<LootClass.WeaponNames>();

    public List<LootClass.LootTypes> Inventory { get { return _inventoryList; } }

    // Start is called before the first frame update

    private UI _ui;

    void Start()
    {
        _weaponPositionTransform = Storage.FindTransformInChildrenWithTag(gameObject, Storage.WeaponPositionTag);
        _grenadeParentTransform = GameObject.FindGameObjectWithTag(Storage.DynamicallyCreatedTag).transform;
        _playerHead = Storage.FindTransformInChildrenWithTag(gameObject, Storage.PlayerHeadTag);

        _healthController = GetComponent<HealthController>();

        _ui = GameObject.FindGameObjectWithTag(Storage.UITag).GetComponent<UI>();

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        getInput();

        if (_weaponActive && _isFire1) Fire1();

        if (_isFire2) Fire2();

        if (_showStat)
        {
            //Debug.Log(Time.time + " Health\t\t: " + GetComponent<HealthController>()?.Health);
            //Debug.Log(Time.time + " Armor\t\t: " + GetComponent<HealthController>()?.Armor);
            //Debug.Log(Time.time + " Bullet\t\t: " + _quantityBullets);
            //Debug.Log(Time.time + " Grenades\t\t: " + _quantityGrenades);
            //Debug.Log(Time.time + " Key\t\t: " + _inventoryList.Contains(LootClass.LootTypes.Key));
            //Debug.Log(Time.time + " Weapon\t\t: " + _inventoryList.Contains(LootClass.LootTypes.Weapon));
            //Debug.Log(Time.time + " \tC4\t: " + _weaponsList.Contains(LootClass.WeaponNames.C4));
            //Debug.Log(Time.time + " \tAK74\t: " + _weaponsList.Contains(LootClass.WeaponNames.AK74));
            //Debug.Log(Time.time + " \tAK74 clip fill\t: " + _weaponController?.ClipFullness);
        }
    }


    private void getInput()
    {
        _isFire1 = Input.GetButton("Fire1");
        _isFire2 = Input.GetButtonDown("Fire2");
        _showStat = Input.GetButtonDown("ShowStat");
    }

    private void Fire1()
    {
        if (_weaponController.ReadyForFire)
        {
            _weaponController.Fire();
            UpdateUI();
        }
    }

    private void Fire2()
    {
        if (_quantityGrenades == 0)
        {
            Storage.ToLog(this, Storage.GetCallerName(), "No more grenades");
            return;
        }

        _grenadeGameObject = Instantiate(_grenadePrefab, _weaponPositionTransform.position, _playerHead.rotation, _grenadeParentTransform);

        _grenadeGameObject.GetComponent<BoxCollider>().enabled = true;

        _grebadeExplosionController = _grenadeGameObject.GetComponent<ExplosionController>();

        _grenadeRigidbody = _grenadeGameObject.GetComponent<Rigidbody>();
        _grenadeRigidbody.isKinematic = false;
        _grenadeRigidbody.AddForce(_playerHead.forward * _grenadeThrowForce, ForceMode.Impulse);

        _grebadeExplosionController.Damage = _grenadeDamage;
        _grebadeExplosionController.ExplosionRadius = _grenadeExplosionRadius;
        _grebadeExplosionController.ExplosionForce = _grenadePushForce;
        _grebadeExplosionController.TimeToExplosion = _grenadeTimer;
        _grebadeExplosionController.LetBoom(_grenadeGameObject);

        _quantityGrenades--;
        UpdateUI();
        Storage.ToLog(this, Storage.GetCallerName(), "Fire in the hole!!!");
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
            case LootClass.LootTypes.Aid:
                StartCoroutine(Aid(_loot.LootPower, _aidEffectTime));
                break;

            case LootClass.LootTypes.Ammo:
                _quantityBullets += _loot.QuantityBullets;

                // Add grenades
                _quantityGrenades += _quantityGrenadesInLoot;

                if (_weaponActive)
                {
                    _weaponController.ClipFullness = _quantityBullets;
                    _quantityBullets = 0;
                }
                UpdateUI();
                break;

            case LootClass.LootTypes.Armor:
                _healthController.AddArmor(_loot.LootPower);
                UpdateUI();
                break;

            case LootClass.LootTypes.Key:
                _inventoryList.Add(_loot.LootType);
                UpdateUI();
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

                    // Add grenades
                    _quantityGrenades += _quantityGrenadesInLoot;

                    // Load bullets to weapon clip
                    _weaponController.ClipFullness = _quantityBullets;
                    _quantityBullets = 0;
                }
                UpdateUI();
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
        UpdateUI();
    }

    /// <summary>
    /// Remove item from inventory
    /// </summary>
    /// <param name="item">Weapon</param>
    public void RemoveFromInventory(LootClass.WeaponNames item)
    {
        _weaponsList.Remove(item);
        UpdateUI();
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

    /// <summary>
    /// Adding health with a volume of "power" several times (one per second)
    /// </summary>
    /// <param name="power"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    IEnumerator Aid(int power, int times)
    {
        for (int i = 0; i < times; i++)
        {
            _healthController.ChangeHealth(power / times);
            UpdateUI();
            yield return StartCoroutine(WaitOneSec());
        }
    }

    IEnumerator WaitOneSec()
    {
        yield return new WaitForSeconds(1f);
    }

    public void UpdateUI()
    {
        _ui.Health = $"{_healthController.Health:d3}";
        _ui.Armor = $"{_healthController.Armor:d3}";
        _ui.Bullets = $"{(_quantityBullets > 0 ? _quantityBullets : _weaponActive ? _weaponController.ClipFullness : 0):d3}";
        _ui.Grenades = $"{_quantityGrenades:d3}";

        _ui.Keys = $"{(_inventoryList.Contains(LootClass.LootTypes.Key)?"Have key":"No more")}";
        _ui.C4 = $"{(_weaponsList.Contains(LootClass.WeaponNames.C4)?"Have bomb":"No more")}";
    }
}
