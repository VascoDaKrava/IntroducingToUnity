using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    // Colliders, that are triggers. For example, they are can not get damaged.
    private List<int> _triggerColliderList = new List<int>();

    public List<int> TriggerColliderList { get { return _triggerColliderList; } }
}
