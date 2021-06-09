using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public Collider flap;
    public Collider player1;
    public GameObject flapObject;
    private bool isInRange = false;
    private bool hasCrowbar = false;
    private bool hasPlayedAudio = false;
    public AudioSource audioSource;
    public AudioClip missingCrowbar;
    public AudioClip endAudio;
    public GameObject eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange && Input.GetButtonDown("Interact"))
        {
            if(hasCrowbar)
            {        
                audioSource.PlayOneShot(endAudio);
                eventSystem.SetActive(true);
                flapObject.SetActive(false);         
            }
            if(!hasCrowbar && !hasPlayedAudio)
            {
                audioSource.PlayOneShot(missingCrowbar);
                hasPlayedAudio = true;
                //Tell player they need a crowbar
            }             
            //Destroy object, camera goes into hole, fade to black;
        }


    }

    private void OnTriggerEnter(Collider player1)
    {
        isInRange = true;
    }

    private void OnTriggerExit(Collider player1)
    {
        isInRange = false;
    }

    public void GiveCrowbar()
    {
        hasCrowbar = true;

    }
}
