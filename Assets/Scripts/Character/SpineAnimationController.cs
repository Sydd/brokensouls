using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;


public class SpineAnimationController : MonoBehaviour
{

        [Header("Configuration")]

        public float minSpeedWalkingAnimation = 0.3f;

        public float maxSpeedWalkingAnimation = 1.3f;

        CharacterMovement playerMovement;

       // PlayerAtack playerAtack;

        SkeletonAnimation skeletonAnimation;


        public List<PlayerMovementAnimation> movementAnimations;
    

         SkeletonUtility skeleton;


         Bone bone;

        CharacterStatus lockAnim;

        void Awake(){

                if ( playerMovement == null){

                        playerMovement = GetComponentInParent<CharacterMovement>();

                }

                skeletonAnimation = GetComponent<SkeletonAnimation>();



                // eventos de movimiento

                playerMovement.OnPlayerJump += OnPlayerJump;

                playerMovement.OnPlayerRun += OnPlayerWalk;

                playerMovement.OnMove += OnPlayerWalk;

                playerMovement.OnIdle += OnPlayerIdle;

                playerMovement.OnPlayerFalling += OnFalling;

                playerMovement.OnDeath += OnPlayerDie;

                playerMovement.OnHurt += OnPlayerHurt;
                
                playerMovement.OnFlip += OnFlip;

                playerMovement.OnLand += OnPlayerLand;

                playerMovement.DANCEKUMBIA += Delayed;


    }

    void Delayed()
    {
        Invoke("OnBailaCumbia", 1f);
    }

    void Start(){

      

                skeletonAnimation.AnimationState.Complete += AnimationCompleteHandler;
                
                skeletonAnimation.AnimationState.Event += HandleEvent;


                lockAnim = CharacterStatus.NONE;


        }

        void Update(){


                if (playerMovement.MovementState == CharacterStatus.WALK){

                        skeletonAnimation.AnimationState.GetCurrent(0).TimeScale = Mathf.Clamp(
                                Mathf.Abs(playerMovement.BodyVelocityX),minSpeedWalkingAnimation, maxSpeedWalkingAnimation);
                        
                }
        }

        void HandleEvent (Spine.TrackEntry trackEntry, Spine.Event e) {

        }

        void AnimationCompleteHandler(TrackEntry track)
         {


                if ((track.TrackIndex == 1 && track.IsComplete))
                {
            skeletonAnimation.state.SetEmptyAnimation(1, 0);
                }
        //si termino la animacion que bloqueamos, quitamos el candado.
                if (lockAnim != CharacterStatus.NONE && track.Animation.Name == GetMovementAnimationName(lockAnim).name){
                        lockAnim = CharacterStatus.NONE;
                        playerMovement.UpdateState();
                        
                }

        }

        TrackEntry SetAnimation(AnimationReferenceAsset animation, bool loop)
        {
                if (lockAnim != CharacterStatus.NONE) {
                        return  null;
                }
                return skeletonAnimation.AnimationState.SetAnimation(0, animation, loop);
        }

        TrackEntry SetAnimation(int layer, AnimationReferenceAsset animation, bool loop)
        {
                return skeletonAnimation.AnimationState.SetAnimation(layer, animation, loop);
        }

        public TrackEntry AddAnimation(int layer, AnimationReferenceAsset animation, bool loop)
        {
                return skeletonAnimation.AnimationState.AddAnimation(layer, animation, loop, 0f);
        }



         AnimationReferenceAsset GetMovementAnimationName(CharacterStatus state){
                 
                 foreach (PlayerMovementAnimation animclass in movementAnimations)
                 {
                     if (animclass.state == state){
                             return animclass.animation;
                     }
                 }      

                Debug.LogError(state.ToString() + "Animation not found");

                return null;
         }



        void OnPlayerWalk(){

                //targetBone.transform.localPosition = new Vector3(2,1,0);

                
            SetAnimation(GetMovementAnimationName(CharacterStatus.WALK),true);

        }

        void OnBailaCumbia()
        {
            SetAnimation(GetMovementAnimationName(CharacterStatus.CUMBIA), true);
            lockAnim = CharacterStatus.FALLING;
        }
        //MODIFICAR ESTO:
        void OnPlayerRun(){
                //skeletonAnimation.state.TimeScale = 3;
        }

        void OnPlayerIdle(){
                
                SetAnimation(GetMovementAnimationName(CharacterStatus.IDLE),true);
        }

                
        void OnPlayerJump(){


                if (lockAnim != CharacterStatus.NONE) return;
                SetAnimation(GetMovementAnimationName(CharacterStatus.JUMP),false);

                lockAnim = CharacterStatus.JUMP;
        }

         void OnFlip()
        {
                //targetBone.transform.position = new Vector3(targetBone.transform.position.x * -1,targetBone.transform.position.y,1);

                transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,1);

               // skeletonAnimation.Skeleton.ScaleX = facingRight ? 1f : -1f;

        }

        void OnFalling(){
                SetAnimation(GetMovementAnimationName(CharacterStatus.FALLING),true);
        
        }

        void OnPlayerLand()
        {
                SetAnimation(1,GetMovementAnimationName(CharacterStatus.LAND), false);
               //lockAnim = CharacterStatus.LAND;

    }

    void OnPlayerHurt(){
        }

        void OnPlayerDie(){     
                SetAnimation(GetMovementAnimationName(CharacterStatus.DEAD),false);
                //SetAnimation(1,GetAttackingAnimationName(PlayerAttackState.NONE),false);
        }
        
}


[System.Serializable]
public class PlayerMovementAnimation{

        public CharacterStatus state;

        public AnimationReferenceAsset animation;

}