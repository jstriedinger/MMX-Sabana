using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int jumpForce;
    Rigidbody2D myBody;
    Animator myAnim;
    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        StartCoroutine( MiCorutina() );
    }

    IEnumerator MiCorutina()
    {
        while(true)
        {
            Debug.Log("Esperando 4 segundos");
            yield return new WaitForSeconds(4);
            Debug.Log("pasaron 4 segundos");
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, 
                                            Vector2.down, 
                                            1.3f,
                                            LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 1.3f, Color.red);

        isGrounded = (ray.collider != null);
        Jump();
        Fire();
    }

    void Fire()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            myAnim.SetLayerWeight(1, 1);
        }
        else
        {
            myAnim.SetLayerWeight(1, 0);
        }
    }

    void FinishingRun()
    {
        Debug.Log("Termina animacion de correr");
    }

    void Jump()
    {
        if(isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Saltando!");
                myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
        if(myBody.velocity.y != 0 && !isGrounded)
            myAnim.SetBool("isJumping", true);
        else
            myAnim.SetBool("isJumping", false);
    }

    private void FixedUpdate()
    {
        float dirH = Input.GetAxis("Horizontal");

        if(dirH != 0)
        {
            myAnim.SetBool("isRunning", true);
            if (dirH < 0)
                transform.localScale = new Vector2(-1, 1);
            else
                transform.localScale = new Vector2(1, 1);

        }
        else
            myAnim.SetBool("isRunning", false);

        myBody.velocity = new Vector2(dirH * speed, myBody.velocity.y);
    }

   

}
