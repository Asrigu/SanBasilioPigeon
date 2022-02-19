using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot_Controller : MonoBehaviour
{
    // Variables
    public Player_Controller _PlayerController;

    private void OnTriggerStay(Collider other)
    {
        _PlayerController.puedoSaltar = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _PlayerController.puedoSaltar = false;
    }
}
