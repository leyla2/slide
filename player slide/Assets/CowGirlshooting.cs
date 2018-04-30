using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowGirlshooting : MonoBehaviour {

    public GameObject bullet; // the prefab of the bullet being shot
    public Vector2 velocity; // speed of the bullet, and direction
    bool canshoot = true; // boolean to make sure if the player can shoot or not
    public Vector2 offSet = new Vector2(0.1f, 0.1f); // to make sure that the bullet moves out of a certain position of the sprite
    public float coolDown = 0.5f;
    Animator animator;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.RightShift) && canshoot) // if shift is pressed and can shoot is true then 
        {
            // spawn the bullet at the position of the player + another position  times by the local scale x, which rotates the correct position -1 local scale is left and 1 is right
            // and Quaternion.identity means there's no rotation. Also I had to turn this into a game object so that I can give it a velocity.
            GameObject go = Instantiate(bullet, (Vector2) transform.position + offSet * transform.localScale.x, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y); // giving the bullet velocity
            animator.SetBool("shoot", true);
                    }
        else
        {
            animator.SetBool("shoot", false);
        }
	}

    // coroutine
    IEnumerator Canshoot()
    {
        canshoot = false;
        yield return new WaitForSeconds(coolDown);
        canshoot = true;
    }
}
