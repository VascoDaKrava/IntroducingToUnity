using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int _health;
    private int _armor = 0;

    public int Health { get { return _health; } }
    public int Armor { get { return _armor; } }

    /// <summary>
    /// Change Health
    /// </summary>
    /// <param name="newHealth"></param>
    public void ChangeHealth(int newHealth)
    {
        if (_armor > 0) changeArmor(ref newHealth);
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
    /// <param name="armor"></param>
    public void ChangeArmor(int armor)
    {
        changeArmor(ref armor);
    }

    private void changeArmor(ref int newArmor)
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
        Storage.ToLog(this, Storage.GetCallerName(), "Death is coming so soon..");
        Destroy(gameObject);
    }
}
