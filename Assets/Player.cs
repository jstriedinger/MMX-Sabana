using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D myBody;
    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float dirH = Input.GetAxis("Horizontal");

        if(dirH != 0)
        {
            myAnim.SetBool("isRunning", true);
        }
        else
            myAnim.SetBool("isRunning", false);

        myBody.velocity = new Vector2(dirH * speed, myBody.velocity.y);
    }

}
