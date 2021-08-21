using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool _isFire1;
    private bool _isFire2;
    private bool _weaponActive = true;

    //private List<int> _triggerColliderList;

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
    /// Loot collector
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PlayerInteraction - Find " + other.name);

        //if (other.CompareTag("LootTag")) other.gameObject.GetComponent<>
    }
}
