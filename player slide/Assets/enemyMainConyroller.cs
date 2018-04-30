using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMainConyroller : MonoBehaviour {

    public float enemySpeed; // speed if the enemy set here
    Animator anim; // ref to animator

    // facing -----------------------------------
    public GameObject enemyTroll;
    bool canFlip = true; // checks whether the troll can flip or not, this is after checking 
                         // if the player is to the left or right of the troll
    bool facingRight = false;
    public float flipTime; // how often should the troll flip while patrolling
    float nextFlipChance = 0f;
    // -----------------------------------------

    // charging the player --------------------------
    public float chargTime; // how long before troll will charge when player enters trigger zone
    float startChargeTime; // this will determine when the troll will charge
    bool charge; // checks if the troll is charging
    Rigidbody2D enemyRigid; // ref to rigidbody
    // ----------------------------------------------


	// Use this for initialization
	void Start () {
        anim =  GetComponent<Animator>();
        // anim =  GetComponentInChildren<Animator>();     this is if the troll is in the child hierarchy of the gameobject scene
        enemyRigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > nextFlipChance) // if the current game time is more than next flip chance then ... 
        {
            if (Random.Range(0, 10) >= 5); // gives it 50% chance that the troll will ... 
            {
                flipTroll(); // flip
                nextFlipChance = Time.time + flipTime; // sets the next flip timer
            }
            
        }
	}
    //flips the troll
    void flipTroll()
    {
        if (!canFlip)   return; // if not flipped already then return
        float facingX = enemyTroll.transform.localScale.x; // checking which direction the troll is facing
        facingX *= -1f; // flip the troll (sprite.transform) the opposite way
        enemyTroll.transform.localScale = new Vector3(facingX, enemyTroll.transform.localScale.y, enemyTroll.transform.localScale.z);
        facingRight = !facingRight;
    }

    void OnTriggerEnter2D(Collider2D other) // player enters trigger zone
    {
        if(other.tag == "Player") 
        {
            // if facing right is true and player's position.x is less than trolls position.x
            if(facingRight == true && other.transform.position.x < transform.position.x) 
            {
                flipTroll(); // flip the troll
            }
            // if facing right is false and player's position.x is more than trolls position.x
            else if ( facingRight == false && other.transform.position.x > transform.position.x)
            {
                flipTroll(); // flip the troll
            }
            canFlip = false;
            charge = true;
            startChargeTime = Time.time + chargTime;
        }
    }

    void OnTriggerStay2D(Collider2D other) // while the player is in the trigger zone
    {
        if(other.tag == "Player")
        {
            if(startChargeTime < Time.time) // 
            {
                // charge the player
                if (!facingRight)
                {
                    enemyRigid.AddForce(new Vector2(-1, 0) * enemySpeed); // to the left
                }
                else

                    enemyRigid.AddForce(new Vector2(1, 0) * enemySpeed); // or to the right
                    anim.SetBool("charge", charge);

            }
        }
    }

    void OnTriggerExit2D(Collider2D other) // when the player leaves the trigger zone
    {
        if (other.tag == "Player")
        {
            canFlip = true;
            charge = false;
            enemyRigid.velocity = new Vector2(9f, 0f); // stop the troll from moving
            anim.SetBool("charge", charge);
        }
    }
}
