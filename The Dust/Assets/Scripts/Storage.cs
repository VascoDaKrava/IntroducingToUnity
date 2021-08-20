using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{

    // Colliders, that are triggers
    private List<int> _triggerColliderList = new List<int>();

    public List<int> TriggerColliderList { get { return _triggerColliderList; } }

    //public void AddTriggerCollider(int value)
    //{
    //    _triggerColliderList.Add(value);
    //}

}
