using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private bool _isFire1;
    private bool _isFire2;
    private bool _weaponActive = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        if (_weaponActive) LetFire();
    }

    private void getInput()
    {
        _isFire1 = Input.GetButton("Fire1");
        _isFire2 = Input.GetButtonDown("Fire2");
    }

    private void LetFire()
    {
        if (_isFire1) Debug.Log("Fire 1");
        if (_isFire2)
        {
            Debug.Log("Fire 2");
            Debug.Log(GetComponent<CharacterHealth>().Health);
        }
    }
}
