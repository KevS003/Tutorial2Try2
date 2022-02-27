using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;//rigidbody type variable
    public float speed;

    public Text score;
    private int scoreValue = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();//rigid body from the object is given to variable 
        score.text = scoreValue.ToString();

    }

    void update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    

    // Update is called once per frame//fixed update because physics are happening 
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");//Gets axis from default unity options?
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));//adds force to the rigidbody based on input from variable. I think?//variable for rigid body is moved through addforce
        
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)//senses impact with objects// part of the MonoBehaviour 
    {
            if(collision.collider.tag == "Coin")
            {
                scoreValue+=1;
                score.text = scoreValue.ToString();
                Destroy(collision.collider.gameObject); 
            }
    }

    private void OnCollisionStay2D(Collision2D collision)//stays connected to collider//aka ground
    {
        if(collision.collider.tag == "Ground")//checks if on ground 
        {
            if(Input.GetKey(KeyCode.W))//allows for w key input for a jump
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);//3 vert for jump.//impulse allows for sudden force b4 gravity pulls u back
            }
        }
      
    }
}
