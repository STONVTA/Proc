using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour {

    public float speed;
    public int damage;

    private Transform player;
    private Vector2 target;


    //public float Rifle { get; private set; }

    // Use this for initialization
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //  target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    private void Update()
    {




    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hittedObject = collision.gameObject;
        if (hittedObject.CompareTag("Player"))
        {
            hittedObject.GetComponent<HeathSystems>().GetDamage(damage);
        }
        Destroy(gameObject);

    }
}

