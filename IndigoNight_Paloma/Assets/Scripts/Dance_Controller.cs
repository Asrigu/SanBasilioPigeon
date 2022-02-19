using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance_Controller : MonoBehaviour
{
    // Variables
    [SerializeField] private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Dance

    private void Dance()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            anim.SetBool("HipHop1", true);
            anim.SetBool("HipHop2", false);
            anim.SetBool("Maraschino", false);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            anim.SetBool("HipHop1", false);
            anim.SetBool("HipHop2", true);
            anim.SetBool("Maraschino", false);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            anim.SetBool("HipHop1", false);
            anim.SetBool("HipHop2", false);
            anim.SetBool("Maraschino", true);
        }
    }

    #endregion
}
