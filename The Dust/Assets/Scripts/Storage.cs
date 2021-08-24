using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    // Colliders, that are triggers. For example, they are can not get damaged.
    private List<int> _triggerColliderList = new List<int>();

    public List<int> TriggerColliderList { get { return _triggerColliderList; } }

    /// <summary>
    /// Translate X-rotation angle of transform to humanity-like variant (0-360 degree)
    /// </summary>
    /// <param name="trans"></param>
    /// <returns></returns>
    public static float ToAngleX(Transform trans)
    {
        return trans.eulerAngles.x > 90 ?
                Mathf.Abs(trans.eulerAngles.z * 3 - trans.eulerAngles.x) :
                Mathf.Abs(trans.eulerAngles.z - trans.eulerAngles.x);
    }

    /// <summary>
    /// Translate Y-rotation angle of transform to humanity-like variant (0-360 degree)
    /// </summary>
    /// <param name="trans"></param>
    /// <returns></returns>
    public static float ToAngleY(Transform trans)
    {
        return trans.eulerAngles.y - 90;
    }
}
