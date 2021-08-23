using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootModel : MonoBehaviour
{
    private LootClass _loot;

    [SerializeField] private LootClass.LootTypes _lootType;
    [SerializeField] private int _power;

    public LootClass Loot { get { return _loot; } }

    // Start is called before the first frame update
    void Start()
    {
        _loot = new LootClass(_lootType, _power);
    }

}
