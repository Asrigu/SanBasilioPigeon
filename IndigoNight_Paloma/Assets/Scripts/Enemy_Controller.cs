using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]

public class Enemy_Controller : MonoBehaviour
{
    // Variables
    [SerializeField] private Animator anim;
    public int rutine;
    public float chronometer;
    public Quaternion angle;
    public float grade;

    public GameObject target;

    private Player_Controller _playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
    }

    void FixedUpdate()
    {
        Enemy_Movement();
    }

    #region MOVEMENT

    private void Enemy_Movement()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 5)
        {
            anim.SetBool("Run", false);
            chronometer += 1 * Time.deltaTime;
            if (chronometer >= 4)
            {
                rutine = Random.Range(0, 2);
                chronometer = 0;
            }

            switch (rutine)
            {
                case 0:
                    anim.SetBool("Walk", false);
                    break;
                
                case 1 :
                    grade = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, grade, 0);
                    rutine++;
                    break;
                
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                    transform.Translate(Vector3.forward * 5 * Time.deltaTime);
                    anim.SetBool("Walk", true);
                    break;
            }
        }

        else
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 5);
            anim.SetBool("Walk", false);
            
            anim.SetBool("Run", true);
            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        }
    }

    #endregion
}
