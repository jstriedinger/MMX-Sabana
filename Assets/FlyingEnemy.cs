using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    AIPath myPath;

    // Start is called before the first frame update
    void Start()
    {
        myPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
    }

    void ChasePlayer()
    {
        //Alternativa 1 - vector2.distance
        /*float d = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log("Distancia con jugador: " + d);
        if (d < 8)
        {

        }
        Debug.DrawLine(transform.position, player.transform.position, Color.red);*/

        //Alternativa 2 - Overlapcircle
        Collider2D col = Physics2D.OverlapCircle(transform.position, 5f, 
                                                LayerMask.GetMask("Player"));
        if (col != null)
            myPath.isStopped = false;
        else
            myPath.isStopped = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
