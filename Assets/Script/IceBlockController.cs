using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockController : MonoBehaviour
{
    private static List<IceBlockController> Neighbors = new List<IceBlockController>();
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
            collision.GetComponent<Gamecontroller>().IsOnIce = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerTriggered = false;
            if (!Neighbors.Any(x => x.IsPlayerTriggered))
            {
                collision.GetComponent<Gamecontroller>().IsOnIce = false;
            }
        }
    }
}