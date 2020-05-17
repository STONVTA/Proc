using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Swamp : MonoBehaviour
{
    private static List<Swamp> Neighbors = new List<Swamp>();
    public bool IsPlayerTriggered { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Neighbors.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerTriggered = true;
            collision.GetComponent<Gamecontroller>().IsOnSwamp = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerTriggered = false;
            if (!Neighbors.Any(x => x.IsPlayerTriggered))
            {
                collision.GetComponent<Gamecontroller>().IsOnSwamp = false;
            }
        }
    }
}
