using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchGrenade : MonoBehaviour
{
    private int _damage = 50;

    private List<int> _triggerColliderList;

    private void Start()
    {
        _triggerColliderList = GameObject.FindGameObjectWithTag("GlobalScript").GetComponent<Storage>().TriggerColliderList;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_triggerColliderList.Contains(other.GetHashCode()))
        {
            Debug.Log("StretchGrenade - Skip trigger-collider " + other.name);
            return;
        }

        Debug.Log("StretchGrenade - B O O M " + other.name);

        if (other.GetComponent<HealthController>() != null)
            other.GetComponent<HealthController>().ChangeHealth(-_damage);

        Destroy(transform.parent.gameObject);
    }
}
