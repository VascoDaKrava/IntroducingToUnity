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
            other.GetComponent<CharacterHealth>().ChangeHealth(-_damage);
            Destroy(transform.parent.gameObject);
        }
    }
}
