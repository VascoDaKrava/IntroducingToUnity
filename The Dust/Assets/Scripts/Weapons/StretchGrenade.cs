using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchGrenade : MonoBehaviour
{
    [SerializeField] private int _damage = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("StretchGrenade - B O O M");
            other.GetComponent<HealthController>().ChangeHealth(-_damage);
            Destroy(transform.parent.gameObject);
        }
    }
}
