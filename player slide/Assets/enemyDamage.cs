using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour {


    public float damage;
    public float damageRate;
    public float pushBackForce;
    float nextDamage;

    PlayerHealth playerHealth;

    // Use this for initialization
    void Start () {
        nextDamage = 0f;
        playerHealth = GetComponent<PlayerHealth>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player" && nextDamage<Time.time)
        {
            playerHealth = col.gameObject.GetComponent<PlayerHealth>();
            playerHealth.addDamage(damage);
            nextDamage = Time.time + damageRate;
            pushBack(col.transform);
        }
    }

    void pushBack ( Transform pushBackObject) // enemy rams the player and pushes the player back
    {
        // pushing the player directly on the y-axis and normalizing it
        Vector2 pushForceDir = new Vector2(0, pushBackObject.position.y - transform.position.y).normalized;
        // giving it speed in that direction
        pushForceDir *= pushBackForce;
        // extracting the rigidbody from player
        Rigidbody2D pushRigid = pushBackObject.gameObject.GetComponent<Rigidbody2D>();
        // when the player gets hit, stop all velocity/force of the player 
        pushRigid.velocity = Vector2.zero;
        // adding aditional force to player
        pushRigid.AddForce(pushForceDir, ForceMode2D.Impulse);
    }
}
