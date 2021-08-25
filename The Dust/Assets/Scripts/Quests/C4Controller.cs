using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Controller : MonoBehaviour
{
    [SerializeField] private GameObject _bombC4;

    private bool _bombPlanted = false;

    private float _timeToExplosion = 10f; // In seconds
    private float _damage = 100f;
    private float _radiusOfExplosion = 15f;

    private string _boomMethodName = "BombExplosion";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_bombPlanted)
        {
            if (other.GetComponent<PlayerInteraction>().IsInInventory(LootClass.WeaponNames.C4))
            {
                other.GetComponent<PlayerInteraction>().RemoveFromInventory(LootClass.WeaponNames.C4);
                Instantiate(_bombC4, other.transform.position, Quaternion.identity, this.transform);
                Storage.ToLog(this, Storage.GetCallerName(), "Bomb has bin planted!");
                Invoke(_boomMethodName, _timeToExplosion);
                _bombPlanted = true;
            }
            else
                Storage.ToLog(this, Storage.GetCallerName(), "No bomb for planted");
        }
    }

    /// <summary>
    /// Do not forget to change value of variable _boomMethodName with name of thos method
    /// </summary>
    private void BombExplosion()
    {
        Storage.ToLog(this, Storage.GetCallerName(), "C4 Explosion!!!");

        Destroy(gameObject);
    }
}
