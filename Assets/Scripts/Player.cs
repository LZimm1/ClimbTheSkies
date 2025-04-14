using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private AudioSource jumpSound;
    [SerializeField]
    private float moveForce = 5;

    [SerializeField]
    private float jumpForce = 7;

    private float movementX;

    private float movementY;

    private string walk = "walk";

    private string jump = "jump";

    private string climb = "climb";

    private string grounded = "grounded";
    
    private string onLadder = "onLadder";
    private bool onGround;

    [SerializeField]
    private Rigidbody2D mybody;

    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private Animator anim;

    private bool nearLadder;

    [SerializeField]
    private float vertSpeed = 2f;

    private bool isClimbing;

    public static int zombieCount = 0;

    public static float zombieDeathTime = 15;
    
    private float posX;

    private float posY;

    public static bool gameWon;

    public static bool gameOver;

    private GameObject zombieRef;
    [SerializeField]
    private GameObject zombie;

    private float leftBound = -8.5f;

    private float rightBound = 8.5f;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
        playerJump();
        animatePlayer();
        if(GameObject.FindWithTag("jumpSound") != null){
            jumpSound = GameObject.FindWithTag("jumpSound").GetComponent<AudioSource>();
        }
        if(zombie == null){
            zombieDeathTime -= Time.deltaTime;
        }
        if(zombieDeathTime <= 0){
            Destroy(gameObject);
            gameOver = true;
        }
        if(transform.position.x < leftBound){
            transform.position = new Vector3(leftBound,transform.position.y,0f);
        }
        if(transform.position.x > rightBound){
            transform.position = new Vector3(rightBound,transform.position.y,0f);
        }
        
    }

    private void FixedUpdate(){
        if(isClimbing){
            mybody.gravityScale = 0;
            mybody.velocity = Vector2.zero;
            transform.position += new Vector3(0f,movementY,0f) * vertSpeed * Time.deltaTime;
            onGround = false;
            anim.SetBool(grounded,false);
        }
        else{
            mybody.gravityScale = 1;
            anim.SetBool(climb,false);
        }
        
        
    }

    void animatePlayer(){
        
        if(movementX > 0){
            anim.SetBool(walk, true);
            sr.flipX = false;
        }
        else if(movementX < 0){
            anim.SetBool(walk, true);
            sr.flipX = true;
        }
        else{
            anim.SetBool(walk, false);
        }

           
        
    }

    void playerMove(){
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;

        movementY = Input.GetAxisRaw("Vertical");
        if(nearLadder && movementY != 0){
            isClimbing = true;
            anim.SetBool(climb, true);
            anim.SetBool(jump, false);
        }
        else{
            anim.SetBool(climb, false);
        }
        
    }

    void playerJump(){
        if(Input.GetButtonDown("Jump") && onGround){
            
            onGround = false;
            anim.SetBool(grounded, false);
            mybody.AddForce(new Vector2(0f,jumpForce),ForceMode2D.Impulse);
            anim.SetBool(jump, true);
            jumpSound.Play();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Block")){
            onGround = true;
            anim.SetBool(jump, false);
            anim.SetBool(grounded,true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground")){
                onGround = false;
                anim.SetBool(grounded,false);
            }
    }
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Ladder")){
            nearLadder = true;
            anim.SetBool(onLadder, true);
        }
        if(collider.CompareTag("Death")){
            
            if(zombie && zombieCount == 0){
                posX= transform.position.x;
                posY= transform.position.y;
                Destroy(gameObject);
                zombieCount++;
                zombieRef = Instantiate(zombie);
                zombieRef.transform.position = new Vector3(posX, posY,0f);
            }
        }
        if(collider.CompareTag("GameWin")){
            Destroy(gameObject);
            gameWon = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("Ladder")){
            nearLadder = false;
            isClimbing = false;
            anim.SetBool(onLadder, false);
        }
    }
}
