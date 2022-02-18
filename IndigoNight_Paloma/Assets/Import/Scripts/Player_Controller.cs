using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]

public class Player_Controller : MonoBehaviour
{
    // Variables
    [SerializeField] private float speed;
    private float rotationSpeed = 200.0f;
    public Animator anim;

    private float x, y;

    public Rigidbody rb;
    public float jumpForce = 8.0f;
    public bool puedoSaltar;

    private Timer_Controller _timerController;
    
    // Start is called before the first frame update
    void Start()
    {
        puedoSaltar = false;
        anim = GetComponent<Animator>();
        speed = 2.5f;
    }

    void FixedUpdate()
    {
        Player_Movement();
    }

    private void Update()
    {
        Player_Run();
        Player_Jump();
    }

    #region WALK
    private void Player_Movement()
    {
        // Funciones
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        anim.SetFloat("Speed_X", x);
        anim.SetFloat("Speed_Y", y);

        // Movimiento player
        transform.Translate(0, 0, y * Time.deltaTime * speed);

        // Rotaci�n player
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
    }
    #endregion

    #region RUN
    private void Player_Run()
    {
        // Aumentar velocidad mientras el shift izquierdo esté presionado
        if (speed == 2.5f && Input.GetKeyDown((KeyCode.LeftShift)))
        {
            anim.SetBool("Run", true);
            speed = speed + 2.5f;
        }
        
        // Reestablecer velocidad inicial mientras el shift izquierdo no esté pulsado
        if (speed <= 5.0f && Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("Run", false);
            speed = 2.5f;
        }
    }
    #endregion

    #region JUMP
    private void Player_Jump()
    {
        if (puedoSaltar)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("Saltar", true);
                rb.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
            }
        } else
        {
            anim.SetBool("Saltar", false);
        }
    }
    #endregion

    #region LOSE

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(ChangeSceneToLose());
        }
    }

    IEnumerator ChangeSceneToLose()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Lose");
    }
    #endregion
}
