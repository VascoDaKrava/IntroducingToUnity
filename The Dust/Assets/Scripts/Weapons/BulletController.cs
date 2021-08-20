using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Transform _bulletTransform;
    //private Collider[] _parentColliders;
    private List<int> _triggerColliderList;

    public float BulletSpeed { get; set; }
    public int BulletDamage { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        _bulletTransform = GetComponent<Transform>();
        //_parentColliders = GetComponentsInParent<Collider>();
        _triggerColliderList = GameObject.FindGameObjectWithTag("GlobalScript").GetComponent<Storage>().TriggerColliderList;
    }

    // Update is called once per frame
    void Update()
    {
        _bulletTransform.Translate(Vector3.forward * BulletSpeed * Time.deltaTime);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        //foreach (Collider item in _parentColliders)
        //{
        //    Debug.Log(item.GetHashCode());
        //}



        if (_triggerColliderList.Contains(other.GetHashCode()))
        {
            Debug.Log("BulletController - Hash compared");
            return;
        }

        //if (other.GetHashCode() == transform.parent.GetHashCode()) return;
        Debug.Log("BulletController - Bullet hit the target " + other.name);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterHealth>().ChangeHealth(-BulletDamage);
        }
        Destroy(_bulletTransform.gameObject);
    }
}
