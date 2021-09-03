using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Storage.PlayerTag))
        {
            Storage.ToLog(this, Storage.GetCallerName(), "Exit point was found");
            Application.Quit();
        }
    }
}
