using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Storage : MonoBehaviour
{
    #region For trigger-colliders

    // Colliders, that are triggers. For example, they are can not get damaged.
    private List<int> _triggerColliderList = new List<int>();
    public List<int> TriggerColliderList { get { return _triggerColliderList; } }

    #endregion

    #region For Navigation

    private Stack<Vector3> _nordWay = new Stack<Vector3>();
    private Stack<Vector3> _undergroundWay = new Stack<Vector3>();
    private Stack<Vector3> _sandWay = new Stack<Vector3>();

    public Stack<Vector3> NordWay { get { return _nordWay; } }
    public Stack<Vector3> UndeegroundWay { get { return _undergroundWay; } }
    public Stack<Vector3> SandWay { get { return _sandWay; } }

    #endregion

    #region Statics for Tags

    private static string _playerTag = "Player";
    private static string _platformTag = "QuestPlatform";
    private static string _nordWayPointsTag = "NordWayPoints";
    private static string _undergroundWayPointsTag = "UndergroundWayPoints";
    private static string _sandWayPointsTag = "SandWayPoints";

    public static string PlayerTag { get { return _playerTag; } }
    public static string PlatformTag { get { return _platformTag; } }

    #endregion

    private void Start()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(_nordWayPointsTag))
        {
            _nordWay.Push(item.transform.position);
        }
    }

    #region Static methods

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
        return (trans.eulerAngles.y - trans.parent.eulerAngles.y < 0)? trans.eulerAngles.y - trans.parent.eulerAngles.y + 360 : trans.eulerAngles.y - trans.parent.eulerAngles.y;
    }

    /// <summary>
    /// Write message to Unity log from current class and method
    /// </summary>
    /// <param name="obj">Current class. Write - this</param>
    /// <param name="met">Current method. Write - Storage.GetCallerName()</param>
    /// <param name="message">Message for logging</param>
    public static void ToLog(Object obj, string met, string message)
    {
        Debug.Log($"{obj.GetType()} : {met} - {message}");
    }

    /// <summary>
    /// Get name of current method
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetCallerName([CallerMemberName] string name = "")
    {
        return name;
    }

    #endregion
}
