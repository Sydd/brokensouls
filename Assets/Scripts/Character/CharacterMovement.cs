using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterMovement : MonoBehaviour
{

    [Header("Life configuration")]

    public int life = 100;

    [Header("Walk and Run Configuration")]

    public float runSpeed;

    public System.Action DANCEKUMBIA;

    public void BAILACUMBIA()
    {

        DANCEKUMBIA?.Invoke();
    }

    public float secondsToMaxSpeed;
    public AnimationCurve speedCurve;



    [Header("Jump Configuration")]

    public float jumpForce;

    public float gravityMultiplier;

    public float secondsToMaxGravity;

    public float maxGravitySpeed;

    public AnimationCurve gravityCurve;

    [Header("Component")]

    public LayerMask ground,platform;

    [SerializeField]
    CharacterStatus movementState;

    float movingFactor;

    public Rigidbody2D body;

    [Header("Debug only")]
    public bool moreGravity;
    public Action OnDeath {get; set;}
    public Action OnHurt {get; set;}
    public Action OnIdle {get;set;}
    public Action OnAttack{get;set;}
    public Action  OnMove {get; set;}
    public Action OnPlayerRun;
    public Action OnPlayerJump;
    public Action OnPlayerFalling;
    public Action OnFlip{get; set;}

    bool facingRight = true;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    float walkElapsedTime,jumpElapsedTime;

    public CharacterStatus MovementState { get => movementState; set => movementState = value; }
    public float BodyVelocityX { get => body.velocity.x;}

    float crounchTimeElapsedTime;
    void Update(){
        crounchTimeElapsedTime += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (MovementState != CharacterStatus.DEAD) {

            if (movingFactor != 0){
                
                walkElapsedTime += Time.fixedDeltaTime;

                float speed = movingFactor * speedCurve.Evaluate(walkElapsedTime / secondsToMaxSpeed) * runSpeed;

                body.velocity = new Vector2 (speed,body.velocity.y);

                Flip(movingFactor);

            } else {
                

                body.velocity = new Vector2(0,body.velocity.y);
                //Deja de caminar, dejamos de checkear hace cuanto tiempo que camina.
                walkElapsedTime = 0;
            }

            if (moreGravity) AddGravity();

            CheckStatus();

        }
    }
    bool wasgrounded = false;
    public System.Action OnLand;

    void CheckStatus()
    {
        if (IsGrounded() && (Mathf.Abs(body.velocity.y) < 0.5f)){

            if (!wasgrounded)
            {
               // OnLand?.Invoke();
                wasgrounded = true;

            }

            //Si esta quieto.
            if (Mathf.Abs(body.velocity.x) < 0.1f)
            {
                if (MovementState != CharacterStatus.IDLE)
                {
                    ChangeState(CharacterStatus.IDLE);

                    if (OnIdle != null){
                        
                        OnIdle();
                    }
                }

            }

            else if((Mathf.Abs(body.velocity.x) > 0) && ( MovementState != CharacterStatus.WALK)) {
                
                ChangeState(CharacterStatus.WALK);

                if (OnMove !=null){
                    
                    OnMove();
                }
            }

        //si no esta saltando, pero tiene velocidad de caida o de salto, le asignamos salto.
        } else { 
            
                if (body.velocity.y < -0 && MovementState != CharacterStatus.FALLING) {

                    wasgrounded = false;
                    ChangeState(CharacterStatus.FALLING);
                    
                    if(OnPlayerFalling != null){

                        OnPlayerFalling();
                    }       
            }

        }
    }


    void Flip(float velocity){

            if (velocity > 0 && !facingRight) {

            facingRight = true;

            if (OnFlip != null){

                OnFlip();
            }

        } else if (velocity < 0 && facingRight){

        
            facingRight = false;

            if (OnFlip != null){

                OnFlip();
            }
        }
        
    }
    //ESTA FUNCION AGREGA GRAVEDAD PARA UNA CAIDA MAS REALISTA, O MAS DIVERTIDA.
    void AddGravity(){

        if ( body.velocity.y < 0 )
        {
            jumpElapsedTime += Time.fixedDeltaTime;
            
            float fallingSpeed = gravityMultiplier * gravityCurve.Evaluate( jumpElapsedTime / secondsToMaxGravity);

            body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y * fallingSpeed,maxGravitySpeed * -1f,maxGravitySpeed ));

        } else {
            jumpElapsedTime = 0;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position,Vector2.down,2,ground);
    }

    void Die(){

        ChangeState(CharacterStatus.DEAD);

        if(OnDeath != null) {
            Debug.Log("dead event");
            OnDeath();
        }
        
    }
 
     void ChangeState(CharacterStatus state)
    {

        if (MovementState == CharacterStatus.DEAD) return;
        

        MovementState = state;
    }

    public void Revive()
    {
        movementState = CharacterStatus.IDLE;
    }


    //Recibe el input.
    public void Move(float factor)
    {
        movingFactor = factor;
    }

    public float GetMovingFactor(){

        return movingFactor;
    }


    public void Hurt(int dmg){


        if (MovementState == CharacterStatus.DEAD)  return;
        
        ChangeState(CharacterStatus.HURT);

        life = life - dmg;


        if (life <= 0) {
            Die();
        } else {
            
            if (OnHurt != null ){
                
                OnHurt();
            
            }
        }

    }

    public void Jump()
    {

        if (IsGrounded() && MovementState != CharacterStatus.JUMP)
        {

            body.AddForce(new Vector2(0, jumpForce));

            ChangeState(CharacterStatus.JUMP);
            
            if (OnPlayerJump != null){

                OnPlayerJump();
            }
        }

    }

            
    public void UpdateState(){
        ChangeState(CharacterStatus.NONE);
    }
    public int GetHitPoints{
        get => life;
    }
    public void SetMovementSpeed(float speed){
        this.runSpeed = speed;
    }

    public void SetJumpForce(float jumpForce){
        this.jumpForce = jumpForce;
    }

    void OnDrawGizmos(){
        Gizmos.DrawRay(transform.position,Vector2.down);

    }

}
