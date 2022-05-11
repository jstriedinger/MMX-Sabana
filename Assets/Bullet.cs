using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float dir, speed;
    bool isMoving = false;
    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            transform.Translate(new Vector2(speed * dir, 0) * Time.deltaTime);
        }
    }

    public void Shoot(float _dir, float _speed)
    {
        dir = _dir;
        speed = _speed;
        isMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Chocando con " + collision.gameObject.name);
        isMoving = false;
        myAnim.SetTrigger("death");

    }

    void Destroy()
    {
        Destroy(gameObject);
    }


}
