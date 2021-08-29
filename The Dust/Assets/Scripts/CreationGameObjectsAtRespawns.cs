using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationGameObjectsAtRespawns : MonoBehaviour
{
    // Prefabs for GameObjects
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _boss;

    private GameObject _temp;

    private GameObject _charactersParent;

    private void Awake()
    {
        _charactersParent = GameObject.FindGameObjectWithTag("DynamicallyCreatedTag");
        
        foreach (Transform item in GameObject.FindGameObjectWithTag("Resp").GetComponentsInChildren(typeof(Transform)))
        {
            if (item.CompareTag(Storage.PlayerTag))
                Instantiate(_player, item.position, Quaternion.identity, _charactersParent.transform);
            
            else if (item.CompareTag(Storage.NordWayTag))
            {
                _temp = Instantiate(_enemy, item.position, Quaternion.identity, _charactersParent.transform);
                _temp.tag = Storage.NordWayTag;
            }

            else if (item.CompareTag(Storage.UndergroundWayTag))
            {
                _temp = Instantiate(_enemy, item.position, Quaternion.identity, _charactersParent.transform);
                _temp.tag = Storage.UndergroundWayTag;
            }

            else if (item.CompareTag(Storage.SandWayTag))
            {
                _temp = Instantiate(_enemy, item.position, Quaternion.identity, _charactersParent.transform);
                _temp.tag = Storage.SandWayTag;
            }

            // BOSS

        }
    }
}
