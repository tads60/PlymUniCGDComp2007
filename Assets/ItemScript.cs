using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject thisItem;
    public Collider item;
    public Collider player;
    public GameObject endpoint;
    public bool isInRange = false;
    public AudioSource audioSource;
    public AudioClip crowBarPickUp;

    // Start is called before the first frame update
    void Start()
    {
        item = this.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetButtonDown("Interact"))
        {
            audioSource.PlayOneShot(crowBarPickUp);
            thisItem.SetActive(false);
            endpoint.GetComponent<EndGame>().GiveCrowbar();
        }
        
        thisItem.transform.Rotate(Vector3.forward * (Time.deltaTime*30));
    }

    private void OnTriggerEnter(Collider player)
    {
        isInRange = true;
    }

    private void OnTriggerExit(Collider player)
    {
        isInRange = false;
    }

}

