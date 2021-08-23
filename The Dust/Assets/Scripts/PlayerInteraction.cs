using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool _isFire1;
    private bool _isFire2;
    private bool _weaponActive = true;
    
    // Start is called before the first frame update
    void Start()
    {
        // Add own trigger-collider to Global List
        GameObject.FindGameObjectWithTag("GlobalScript").GetComponent<Storage>().TriggerColliderList.Add(GetComponent<BoxCollider>().GetHashCode());
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
            Debug.Log(GetComponent<HealthController>().Health);
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

    //public void GetLoot(LootClass collectedLoot)
    public void GetLoot(GameObject collectedLoot)
    {
        switch (collectedLoot.GetComponent<LootModel>().Loot.LootType)
        {
            case LootClass.LootTypes.Aim:
                Debug.Log("PlayerInteraction - Aim, power : " + collectedLoot.GetComponent<LootModel>().Loot.LootPower);
                break;
            case LootClass.LootTypes.Ammo:
                break;
            case LootClass.LootTypes.Armor:
                break;
            case LootClass.LootTypes.Key:
                break;
            case LootClass.LootTypes.Weapon:
                break;
            default:
                break;
        }

        Debug.Log("PlayerInteraction - Get " + collectedLoot.name);
        Destroy(collectedLoot);
    }
}
