using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationGameObjectsAtRespawns : MonoBehaviour
{
    // Prefabs for GameObjects
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject _charactersParent;

    //private List<GameObject> _characters = new List<GameObject>();

    private void Awake()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Resp"))
        {
            //_characters.Add(item);

            switch (item.name)
            {
                case "Player":
                    GameObject.Instantiate(_player, item.transform.position, Quaternion.identity, _charactersParent.transform);
                    break;

                case "Enemy":
                    GameObject.Instantiate(_enemy, item.transform.position, Quaternion.identity, _charactersParent.transform);
                    break;

                case "Boss":
                    GameObject.Instantiate(_boss, item.transform.position, Quaternion.identity, _charactersParent.transform);
                    break;

                default:
                    break;
            }
        }
    }
}
