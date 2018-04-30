using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {


    [SerializeField]
    Slider HealthBar;
    [SerializeField]
    Text healthValue;

    public GameObject DeathUI;

    float maxHealth = 100f;
    float CurrentHealth;

    enemyMainConyroller player;//allow to use the variables from the movement class.
    public GameObject BloodParticle;
    public GameObject RespawnParticle; //This will be moved once I have a respawn location

    Animator anim;

    void Start () {
        anim = GetComponent<Animator>();
       

        player = GetComponent<enemyMainConyroller>();

        HealthBar.value = maxHealth;
        CurrentHealth = HealthBar.value;
	}
	
	// Update is called once per frame
	void Update () {
        
        healthValue.text = CurrentHealth.ToString() + "%";
        Death();
        
    }

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "damager")
        {
            
            HealthBar.value -= 1f;
            CurrentHealth = HealthBar.value;
            Instantiate(BloodParticle, gameObject.transform.position, gameObject.transform.rotation);//make a copy of bloodparticle in this position and rotation, of the player when it hits the game object. 

        }
        if (collision.gameObject.tag == "hell")
        {

            HealthBar.value -= 100f;
            CurrentHealth = HealthBar.value;
           
        }

        if (collision.gameObject.tag == "eagle")
        {

            HealthBar.value -= 5f;
            CurrentHealth = HealthBar.value;
            Instantiate(BloodParticle, gameObject.transform.position, gameObject.transform.rotation);//make a copy of bloodparticle in this position and rotation, of the player when it hits the game object. 

        }
    }

    public void Death()// going to try and respawn location here
    {
        if (CurrentHealth <= 0)
        {

            anim.SetBool("death", true);
            GetComponent<enemyMainConyroller>().enabled = false;
            
            DeathUI.gameObject.SetActive(true);

           // Instantiate(BloodParticle, player.transform.position, player.transform.rotation);//make a copy of bloodparticle in this position and rotation, of the player's. 

           // player.transform.position = new Vector2(50, 0);
        }
       
    }

    public void addDamage(float damage)
    {
        if(damage <= 0)
        
            return;
        CurrentHealth -= damage;          


    }

}
