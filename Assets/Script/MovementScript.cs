using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private float speed = 10.0f;
    private float rotSpeed = 100.0f;
    public GameObject character;
    private float translation;
    private float rotation;
    private bool isStood;
    private bool isIdle;
    private bool isCrouched;
    private bool isCrouching;
    private bool isStanding = true;
    Animator anim;
    private bool playingSound = false;
    public AudioSource walkingSound;
    public ParticleSystem footsteps;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            crouchToggle();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        getMovementInput();
        characterRotation();
        idleCheck();
        //basic movement walking
        if (!isIdle)
        {
            footsteps.Play();
            translation *= Time.deltaTime;
            if (isStanding)
            {
                standingMovement();
            }
            else if (isCrouched)
            {
                crouchedMovement();
            }

                
                
                
                

        }
        //basic idle
        if (isIdle)
        {
            footsteps.Stop();
            if (isStanding)
            {
                anim.ResetTrigger("IdleToWalk");
                anim.SetTrigger("WalkToIdle");
            }
            else if (isCrouched)
            {
                if(isCrouching)
                {
                    anim.SetTrigger("IdleToCrouching");
                    isCrouching = false;
                }
                else if(!isCrouching)
                {
                    anim.ResetTrigger("CrouchIdleToCrouch");
                    anim.SetTrigger("CrouchToCrouchIdle");
                }
                
            }

        }

        playWalkSound();


    }



    void playWalkSound()
    {
        if(!isIdle)
        {

            walkingSound.mute = false;
        }
        else if(isIdle)
        {
            walkingSound.mute = true;
        }
    }

    void idleCheck()
    {
        if(translation !=0)
        {
            isIdle = false;
        }
        else if (translation == 0)
        {
            isIdle = true;
        }
    }
    void crouchToggle()
    {
        if(isStanding)
        {
            setToCrouch();
        }
        if(isCrouched)
        {
            setToStand();
        }
        isCrouched = !isCrouched;
        isStanding = !isStanding;
    }
    void getMovementInput()
    {
        translation = Input.GetAxis("Vertical") * speed;
        rotation = Input.GetAxis("Horizontal") * rotSpeed;
    }
    void standingMovement()
    {
        character.transform.Translate(0, 0, translation);
        anim.ResetTrigger("WalkToIdle");
        anim.SetTrigger("IdleToWalk");
    }

    void setToCrouch()
    {
        ResetAllTriggers();
        anim.SetTrigger("WalkToIdle");
        anim.SetTrigger("IdleToCrouching");
        anim.SetTrigger("CrouchingToCrouchIdle");
    }
    void setToStand()
    {
        ResetAllTriggers();
        anim.SetTrigger("CrouchIdleToStanding");
        anim.SetTrigger("StandingToIdle");
        anim.SetTrigger("CrouchToCrouchIdle");
    }

    void crouchedMovement()
    {
        character.transform.Translate(0, 0, translation);
        anim.ResetTrigger("CrouchToCrouchIdle");
        anim.SetTrigger("CrouchIdleToCrouch");
    }   
    void characterRotation()
    {
        rotation *= Time.deltaTime;
        character.transform.Rotate(0, rotation, 0);
    }

    private void ResetAllTriggers()
    {
        foreach (var param in anim.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                anim.ResetTrigger(param.name);
            }
        }
    }

}
