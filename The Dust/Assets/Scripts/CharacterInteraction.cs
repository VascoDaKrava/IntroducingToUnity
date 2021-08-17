using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    private int _health = 100;
    private int _armor = 0;

    /// <summary>
    /// Change Health
    /// </summary>
    /// <param name="newHealth"></param>
    public void ChangeHealth(int newHealth)
    {
        if (_armor > 0) ChangeArmor(ref newHealth);
        if (newHealth != 0)
        {
            _health += newHealth;
            if (_health > 100) _health = 100;
            if (_health <= 0) Die();
        }
    }

    /// <summary>
    /// Change Armor. If nothing to change - change Health.
    /// </summary>
    /// <param name="newArmor"></param>
    public void ChangeArmor(ref int newArmor)
    {
        _armor += newArmor;

        if (_armor > 100)
        {
            _armor = 100;
        }
        else
        {
            if (_armor < 0)
            {
                newArmor = _armor;
            }
            else
            {
                newArmor = _armor;
            }
        }
    }

    private void Die()
    {
        GameObject.Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
