using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeathSystems : MonoBehaviour
{
    public float _health;
    public int numberOfLives;

    public Image[] lives;

    public Sprite fullLives;
    public Sprite emptyLives;

    public void GetDamage(float smth)
    {        
        if (_health-smth <= 0)
        {
            _health = 0;
            ReloadScene();
        }
        else
        {
            _health -= smth;
        }
    }
    //                      Damage info
    //                       damage
    //                       DamageType
           

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (_health > numberOfLives)
        {
            _health = numberOfLives;
        }

        for (int i = 0; i < lives.Length; i++)
        {
            if (i < _health)
            {
                lives[i].sprite = fullLives;
            }
            else
            {
                lives[i].sprite = emptyLives;
            }


            if (i < numberOfLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }

        }


        
       
    }

    private void ReloadScene()
    {
        var scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }
}
