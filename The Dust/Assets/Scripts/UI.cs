using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private Text _healthText;
    private Text _armorText;
    private Text _bulletsText;
    private Text _grenadesText;

    private Text _keys;
    private Text _c4;
        
    void Awake()
    {
        _healthText = Storage.FindTransformInChildrenWithTag(gameObject, Storage.HealthUITag).GetComponent<Text>();
        _armorText = Storage.FindTransformInChildrenWithTag(gameObject, Storage.ArmorUITag).GetComponent<Text>();
        _bulletsText = Storage.FindTransformInChildrenWithTag(gameObject, Storage.BulletsUITag).GetComponent<Text>();
        _grenadesText = Storage.FindTransformInChildrenWithTag(gameObject, Storage.GrenadesUITag).GetComponent<Text>();
        _keys = Storage.FindTransformInChildrenWithTag(gameObject, Storage.KeysUITag).GetComponent<Text>();
        _c4 = Storage.FindTransformInChildrenWithTag(gameObject, Storage.C4UITag).GetComponent<Text>();
    }

    public string Health { get { return _healthText.text; } set { _healthText.text = value; } }
    public string Armor { get { return _armorText.text; } set { _armorText.text = value; } }
    public string Bullets { get { return _bulletsText.text; } set { _bulletsText.text = value; } }
    public string Grenades { get { return _grenadesText.text; } set { _grenadesText.text = value; } }
    public string Keys { get { return _keys.text; } set { _keys.text = value; } }
    public string C4 { get { return _c4.text; } set { _c4.text = value; } }
}
