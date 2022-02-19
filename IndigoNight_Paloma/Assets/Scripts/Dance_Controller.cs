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
        Dance();
    }

    #region Dance

    private void Dance()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetBool("HipHop1", false);
            anim.SetBool("HipHop2", true);
            anim.SetBool("HipHop3", false);
            anim.SetBool("Maraschino", false);
            anim.SetBool("Shopping", false);
        }

        if (Input.GetKeyDown(KeyCode.U))
        { 
            anim.SetBool("HipHop1", false);
            anim.SetBool("HipHop2", true);
            anim.SetBool("HipHop3", false);
            anim.SetBool("Maraschino", false);
            anim.SetBool("Shopping", false);
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            anim.SetBool("HipHop1", false);
            anim.SetBool("HipHop2", false);
            anim.SetBool("HipHop3", true);
            anim.SetBool("Maraschino", false);
            anim.SetBool("Shopping", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("HipHop1", false);
            anim.SetBool("HipHop2", false);
            anim.SetBool("HipHop3", false);
            anim.SetBool("Maraschino", true);
            anim.SetBool("Shopping", false);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            anim.SetBool("HipHop1", false);
            anim.SetBool("HipHop2", false);
            anim.SetBool("HipHop3", false);
            anim.SetBool("Maraschino", false);
            anim.SetBool("Shopping", true);
        }
    }

    #endregion
}
