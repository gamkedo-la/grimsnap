using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Can to change how this works to a singleton that takes a more generic input later if necessary 
public class PlayerInput : MonoBehaviour
{
    //This will contain more inputs for things like abilities, healing etc. For now Left click can't be tied down to one action so it will simply be named leftClick
    public bool leftClick = false;
    
    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            leftClick = true;
        }
        else
        {
            leftClick = false;
        }
    }
}
