using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUsage : MonoBehaviour
{
    private DoorController _door;
    private BoxCollider _zoneKeyUsage;
    private Vector3 _zoneSize = new Vector3(1, 2, 1);
    private Vector3 _zoneCenter = new Vector3(-0.5f, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        _door = GetComponent<DoorController>();
        _zoneKeyUsage = gameObject.AddComponent<BoxCollider>();
        _zoneKeyUsage.size = _zoneSize;
        _zoneKeyUsage.center = _zoneCenter;
        _zoneKeyUsage.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _door.IsClosed)
        {
            if (other.GetComponent<PlayerInteraction>().IsInInventory(LootClass.LootTypes.Key))
            {
                other.GetComponent<PlayerInteraction>().RemoveFromInventory(LootClass.LootTypes.Key);
                _door.NeedOpen = true;
            }
            else
                Storage.ToLog(this, Storage.GetCallerName(), "No keys");
        }
    }
}
