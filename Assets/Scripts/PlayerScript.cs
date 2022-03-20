using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;



public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;//rigidbody type variable

    Animator move;

    public Tilemap wall;

    public AudioSource source;

    public AudioClip mainSong;

    public AudioClip victory;

    public float speed;

    public float jumpForce;

    public Text score;

    public Text win;

    public Text lives;

    public GameObject tele;

    private int livesAmount;
    private int scoreValue = 0;

    private bool facingRight = true;

    private bool isOnGround;

    public Transform groundcheck;
    
    public float checkRadius;

    public LayerMask allGround;//var type to check layer


    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();//rigid body from the object is given to variable 
        score.text = scoreValue.ToString();
        livesAmount = 3;
        win.enabled = false;
        lives.text += livesAmount.ToString();
        source.clip = mainSong;
        source.loop = true;
        tele.SetActive(false);
        source.Play();

    }


    // Update is called once per frame//fixed update because physics are happening 
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");//Gets axis from default unity options?
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));//adds force to the rigidbody based on input from variable. I think?//variable for rigid body is moved through addforce
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        /*if (Input.GetKey("escape"))
        {
            Application.Quit();
        }*/ //moved to camera 

        if (facingRight == false && hozMovement > 0)//checks where you are facing then checks your movement 
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
            
        }
    }
    void OnCollisionEnter2D(Collision2D collision)//senses impact with objects// part of the MonoBehaviour 
    {
            if(collision.collider.tag == "Coin")
            {
                scoreValue+=1;
                score.text = scoreValue.ToString();
                Destroy(collision.collider.gameObject); 
                Checkwin(scoreValue);
            }
            if(collision.collider.tag == "Enemy")
            {
                livesAmount-=1;
                lives.text = "Lives: " + livesAmount.ToString();
                Destroy(collision.collider.gameObject);
                CheckL(livesAmount);

            }
    }

    private void OnCollisionStay2D(Collision2D collision)//stays connected to collider//aka ground
    {
        if(collision.collider.tag == "Ground" && isOnGround)//checks if on ground 
        {
            move.SetInteger("State",0);

            if(Input.GetAxis("Horizontal") != 0)
            {
                move.SetInteger("State",1);
            }

            if(Input.GetKey(KeyCode.W))//allows for w key input for a jump
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);//3 vert for jump.//impulse allows for sudden force b4 gravity pulls u back
                move.SetInteger("State",2);
            }
        }
      
    }

    private void OnTriggerEnter2D(Collider2D second)
    {
        if(second.gameObject.tag == "SecLevel")
        {
            this.transform.position = new Vector3(-17.43f, 48.72f, -0.13f);
            jumpForce = 3.2f;
            livesAmount = 3; 
            lives.text = "Lives: " + livesAmount.ToString();

        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x*-1;
        transform.localScale = Scaler; 
    }

    void Checkwin(int score)
    {
        if(score == 4)
        {
            Destroy(wall.gameObject);
            tele.SetActive(true);
        }
        if(score == 9)
        {
            win.enabled= true;
            source.clip = victory;
            source.loop = false;
            source.Play();
        }
    }
    void CheckL(int Life)
    {
        if(Life == 0)
        {
            this.gameObject.SetActive(false);
            win.text= "You Lose!";
            win.enabled=true;
        }
    }
}
