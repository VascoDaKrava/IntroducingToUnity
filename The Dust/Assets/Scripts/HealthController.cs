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
        if (newHealth >= 0)
        {
            _health += newHealth;
            if (_health > 100) _health = 100;
            return;
        }

        if (_armor > 0)
        {
            reduceArmor(newHealth);
            return;
        }

        _health += newHealth;

        if (_health <= 0) Die();

    }

    /// <summary>
    /// Change Armor. If nothing to change - change Health.
    /// </summary>
    /// <param name="armor"></param>
    public void AddArmor(int armor)
    {
        _armor += armor;

        if (_armor > 100) _armor = 100;
    }

    /// <summary>
    /// Reduce armor and health (if armor may be negative)
    /// </summary>
    /// <param name="newArmor"></param>
    private void reduceArmor(int newArmor)
    {
        if (_armor + newArmor < 0)
        {
            newArmor += _armor;
            _armor = 0;
            ChangeHealth(newArmor);
        }
        else
            _armor += newArmor;
    }

    private void Die()
    {
        Storage.ToLog(this, Storage.GetCallerName(), "Death is coming so soon..");
        Destroy(gameObject);
    }
}
