using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    [SerializeField] float speedRun =5f;

    [SerializeField] float speedJump = 10f;

    [SerializeField] float ClimbingSpeed = 3f;

    [SerializeField] Vector2 deathKick = new Vector2(20f, 20f);

    [SerializeField] GameObject myBullet;

    [SerializeField] GameObject gun;
    Vector2 moveValue;

    SpriteRenderer myRender;
    Rigidbody2D rigidbodyPlayer;

    CapsuleCollider2D capsuleColliderPlayer;

    BoxCollider2D myFeetCollider;

    Animator myAnimator;

    private bool alive = true;

    float currentGravityScale;
    // Start is called before the first frame update
    void Start()
    {
        myRender = GetComponent<SpriteRenderer>();
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        capsuleColliderPlayer = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        rigidbodyPlayer.constraints = RigidbodyConstraints2D.FreezeRotation;
        currentGravityScale = rigidbodyPlayer.gravityScale;
    }

    

    // Update is called once per frame
    void Update()
    {
        if(!this.alive){
            return;
        }
        Run();
        Flip();
        Climbing();
        Die();
    }

 
    void OnMove(InputValue value){
        moveValue = value.Get<Vector2>();
        
    }

    void OnJump(InputValue value){
        if(!this.alive){
            return;
        }

        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return;
        }
        if(value.isPressed){

            rigidbodyPlayer.velocity += new Vector2(0f, speedJump);
        }
    }

    void OnFire(){
         if(!this.alive){
            return;
        }

        Instantiate(myBullet, gun.transform.position , Quaternion.identity);
    }

    void Run(){
        if(!this.alive){
            return;
        }
        Vector2 playerVelocity = new Vector2(moveValue.x * speedRun, rigidbodyPlayer.velocity.y);
        if(moveValue.x != 0 ){
            myAnimator.SetBool("isRunning", true);
        }else{
            myAnimator.SetBool("isRunning", false);
        }
       
          rigidbodyPlayer.velocity = playerVelocity;
        
        
    }

    void Flip(){
         if(moveValue.x <0){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }else if (moveValue.x >0){
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void Climbing(){

        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            rigidbodyPlayer.gravityScale = currentGravityScale;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        Run(); 
            Vector2 climbingVelocity = new Vector2(moveValue.x, moveValue.y * ClimbingSpeed);  
            rigidbodyPlayer.velocity = climbingVelocity;
            rigidbodyPlayer.gravityScale = 0f;
            
            bool isPlayerHasVerticalSpeed = Mathf.Abs(rigidbodyPlayer.velocity.y) > Mathf.Epsilon;
            if (isPlayerHasVerticalSpeed) {
             myAnimator.SetBool("isClimbing", true);
             
            }   

            
    }

    public void Die(){
        if(capsuleColliderPlayer.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazard")) || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazard"))){
             this.alive = false;
            myAnimator.SetTrigger("Dying");
            myRender.color = new Color(1f, 0.30196078f, 0.30196078f);
            rigidbodyPlayer.velocity = deathKick;
           
        }
        
    }

    
}
