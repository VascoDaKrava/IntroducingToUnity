using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class Storage : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    private void Awake()
    {
        // Search NavPoints
        foreach (Transform item in GameObject.FindGameObjectWithTag(_navPointsTag).GetComponentsInChildren(typeof(Transform)))
        {
            if (item.CompareTag(NordWayTag))
                _nordWay.Add(item.position);

            else if (item.CompareTag(UndergroundWayTag))
                _undergroundWay.Add(item.position);

            else if (item.CompareTag(SandWayTag))
                _sandWay.Add(item.position);
        }

        // Load audio settings
        if (Settings.VolumeMute)
            _mixer.SetFloat(VolumeNameMaster, -80f);
        else
            _mixer.SetFloat(VolumeNameMaster, Settings.VolumeMaster);
        
        _mixer.SetFloat(VolumeNameMusic, Settings.VolumeMusic);
        _mixer.SetFloat(VolumeNameMenu, Settings.VolumeMenu);
        _mixer.SetFloat(VolumeNameFX, Settings.VolumeFX);
    }

    #region For Navigation

    private List<Vector3> _nordWay = new List<Vector3>();
    private List<Vector3> _undergroundWay = new List<Vector3>();
    private List<Vector3> _sandWay = new List<Vector3>();

    public List<Vector3> NordWay { get { return _nordWay; } }
    public List<Vector3> UndeegroundWay { get { return _undergroundWay; } }
    public List<Vector3> SandWay { get { return _sandWay; } }

    #endregion

    #region Static for Volume

    private static string _mixerMaster = "MasterVolume";
    private static string _mixerMusic = "MusicVolume";
    private static string _mixerMenu = "MenuVolume";
    private static string _mixerFX = "FXVolume";

    public static string VolumeNameMaster { get { return _mixerMaster; } }
    public static string VolumeNameMusic { get { return _mixerMusic; } }
    public static string VolumeNameMenu { get { return _mixerMenu; } }
    public static string VolumeNameFX { get { return _mixerFX; } }

    #endregion

    #region Static for Tags

    #region ToDo

    //public enum VarTag
    //{
    //    PlayerTag
    //}

    //private static Dictionary<VarTag, string> _tags = new Dictionary<VarTag, string>
    //{
    //    {VarTag.PlayerTag, "Player" }
    //};
    //public string this[VarTag tag]
    //{
    //    get { return _tags[tag]; }
    //}
    #endregion


    private static string _respTag = "Resp";// 00
    private static string _globalTag = "GlobalScript";// 01
    private static string _dynamicalyCreatedTag = "DynamicallyCreatedTag";// 02
    //private static string _lootTag = "LootTag";// 03
    //private static string _ammoBoxLockTag = "AmmoBoxLockTag";// 04
    //private static string _ammoBoxTopTag = "AmmoBoxTopTag";// 05
    //private static string _DoorLeftTag = "DoorLeft";// 06
    //private static string _DoorRightTag = "DoorRight";// 07
    private static string _platformTag = "QuestPlatform";// 08
    private static string _nordWayPointsTag = "NordWayPoints";// 09
    private static string _undergroundWayPointsTag = "UndergroundWayPoints";// 10
    private static string _sandWayPointsTag = "SandWayPoints";// 11
    private static string _navPointsTag = "NavPoints";// 12
    private static string _weaponPosTag = "WeaponPositionTag";// 13
    private static string _weaponTag = "Weapon";// 14
    private static string _bullet1StartPositionTag = "BulletStart1";// 15
    private static string _bullet2StartPositionTag = "BulletStart2";// 16
    private static string _playerHeadTag = "PlayerHead";// 17
    private static string _enemyNavigatedTag = "EnemyNavigated";// 18
    private static string _healthUI = "UI_health";// 19
    private static string _armorUI = "UI_armor";// 20
    private static string _bulletsUI = "UI_bullets";// 21
    private static string _grenadesUI = "UI_grenades";// 22
    private static string _keysUI = "UI_keys";// 23
    private static string _c4UI = "UI_C4";// 24
    private static string _UI = "UI";// 25

    public static string RespTag { get { return _respTag; } }
    public static string GlobalTag { get { return _globalTag; } }
    public static string DynamicallyCreatedTag { get { return _dynamicalyCreatedTag; } }
    public static string PlatformTag { get { return _platformTag; } }
    public static string NordWayTag { get { return _nordWayPointsTag; } }
    public static string UndergroundWayTag { get { return _undergroundWayPointsTag; } }
    public static string SandWayTag { get { return _sandWayPointsTag; } }
    public static string NavPointsTag { get { return _navPointsTag; } }
    public static string WeaponPositionTag { get { return _weaponPosTag; } }
    public static string WeaponTag { get { return _weaponTag; } }
    public static string Bullet1StartPositionTag { get { return _bullet1StartPositionTag; } }
    public static string Bullet2StartPositionTag { get { return _bullet2StartPositionTag; } }
    public static string PlayerHeadTag { get { return _playerHeadTag; } }
    public static string EnemyNavigatedTag { get { return _enemyNavigatedTag; } }
    public static string HealthUITag { get { return _healthUI; } }
    public static string ArmorUITag { get { return _armorUI; } }
    public static string BulletsUITag { get { return _bulletsUI; } }
    public static string GrenadesUITag { get { return _grenadesUI; } }
    public static string KeysUITag { get { return _keysUI; } }
    public static string C4UITag { get { return _c4UI; } }
    public static string UITag { get { return _UI; } }

    private static string _untagged = "Untagged";
    private static string _mainCamera = "MainCamera";
    private static string _playerTag = "Player";
    public static string PlayerTag { get { return _playerTag; } }
    public static string MainCameraTag { get { return _mainCamera; } }
    public static string Untagged { get { return _untagged; } }

    #endregion

    #region Static for Weapons

    #endregion

    #region Static methods

    /// <summary>
    /// Find transform with "tag" in children of "gameObj"
    /// </summary>
    /// <param name="gameObj">GameObject, where searching</param>
    /// <param name="tag">Tag for searched transform</param>
    /// <returns></returns>
    public static Transform FindTransformInChildrenWithTag(GameObject gameObj, string tag)
    {
        foreach (Transform item in gameObj.GetComponentsInChildren(typeof(Transform)))
        {
            if (item.CompareTag(tag))
                return item;
        }
        return null;
    }

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
        return (trans.eulerAngles.y - trans.parent.eulerAngles.y < 0) ? trans.eulerAngles.y - trans.parent.eulerAngles.y + 360 : trans.eulerAngles.y - trans.parent.eulerAngles.y;
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