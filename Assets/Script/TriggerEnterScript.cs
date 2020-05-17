using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterScript : MonoBehaviour
{
    private Transform player;
    public event Action<bool> OnTriggered;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag(player.tag))
        {
            //TODO: Start moving
            if (OnTriggered != null)
                OnTriggered(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag(player.tag))
        {
            if (OnTriggered != null)
                OnTriggered(false);

        }
    }
}
