using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int jumpForce;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;

    Rigidbody2D myBody;
    Animator myAnim;
    bool isGrounded = true;
    float nextFire = 0;


    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        //StartCoroutine( MiCorutina() );
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

    IEnumerator ChangeLayerWeight()
    {
        yield return new WaitForSeconds(0.5f);
        myAnim.SetLayerWeight(1, 0);

    }

    void Fire()
    {
        if(Input.GetKeyDown(KeyCode.Z) && Time.time >= nextFire)
        {
            GameObject myBullet = Instantiate(bullet, transform.position, transform.rotation);
            myBullet.GetComponent<Bullet>().Shoot(transform.localScale.x, bulletSpeed);


            nextFire = Time.time + fireRate;
            myAnim.SetLayerWeight(1, 1);
        }
        else if(Time.time > nextFire + 0.2f)
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
