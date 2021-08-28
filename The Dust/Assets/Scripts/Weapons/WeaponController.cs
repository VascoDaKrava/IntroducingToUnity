using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private bool _readyForFire = false;

    public bool ReadyForFire { get { return _readyForFire; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        Storage.ToLog(this, Storage.GetCallerName(), "Fire 1");
    }
}
