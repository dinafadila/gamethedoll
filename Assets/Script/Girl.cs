using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{
    public float upForce;
    private Animator anim;
    private bool isDead = false;            //Has the player collided with a wall?
    private Rigidbody2D rb2d;                //Holds a reference to the Rigidbody2D component of the bird.
    private bool slash = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
		
    }

    void Update()
    {
        //Don't allow control if the bird has died.
        if (isDead != false)
        {
            if (Input.GetMouseButtonDown(1))
            {
                anim.SetTrigger("Girl_Jump");
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)){
				slash = true;
				anim.SetTrigger("Girl_Slashing");
			}
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
		if(collision.gameObject.name == "HumanGirl" || collision.gameObject.name == "HumanBoy")
		{
			if(slash == false)
			{
				isDead = true;
				anim.SetTrigger("Girl_Die");
				GameController.instance.GirlDied();
			}
			else
			{	
				ScoreScript.scoreVal++;
				Destroy(collision.gameObject);
				slash = false;
			}
		}
    }
}
