using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDamage : MonoBehaviour {

    enemyHealth enemyHealth;
    public float enemyDamage;
    public float otherDamage;

	// Use this for initialization
	void Start ()
    {
        enemyHealth = GetComponent<enemyHealth>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            enemyHealth.addDamage(enemyDamage);
        }
    }
}
