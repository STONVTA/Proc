using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public GameObject Projectile;
    public float CooldownTime;
    public float _health = 1;
    //public float stoppingDistance;
    //public float retreatDistance;

    private Rigidbody2D _rigidbody2D;
    //private float timeBtwShots;
    //public float startTimeBtwShots;

    private TriggerEnterScript _viewArea;
    private TriggerEnterScript _attackArea;
    private Transform _player;
    private bool _isFollowPlayer;
    private bool _isSooting;
    private bool _readyForShot = true;

    // Use this for initialization
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
           _player = GameObject.FindGameObjectWithTag("Player").transform;
        //timeBtwShots = startTimeBtwShots;
        _viewArea = transform.Find("ViewRange").GetComponent<TriggerEnterScript>();
        _viewArea.OnTriggered += (bool condition) =>
        {
            _isFollowPlayer = condition;
            StopFollowPlayer(condition);
        };

        _attackArea = transform.Find("AttackRange").GetComponent<TriggerEnterScript>();
        _attackArea.OnTriggered += (bool condition) =>
        {
            _isFollowPlayer = !condition;
            StopFollowPlayer(!condition);
            _isSooting = condition;
        };
    }
    public void GetDamage(float smth)
    {
        if (_health - smth <= 0)
        {
            _health = 0;
            Destroy(gameObject);
        }
        else
        {
            _health -= smth;
        }
    }

    private void Shot()
    {
        //Debug.Log("BOOM");
        var temp = transform.position;
        var player = _player.position;
        var direction = new Vector2(player.x - temp.x, player.y - temp.y);

        var bullet = Instantiate(Projectile, temp, Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, direction)));
        bullet.GetComponent<Rigidbody2D>().AddForce(direction.normalized * bullet.GetComponent<Rifle>().speed);
        bullet.transform.right = direction.normalized;

        _readyForShot = false;
        StartCoroutine(ShotCooldown());

    }

    private IEnumerator ShotCooldown()
    {
        yield return new WaitForSecondsRealtime(CooldownTime);
        _readyForShot = true;
    }

    private void StopFollowPlayer(bool condition)
    {
        if (!condition)
            _rigidbody2D.velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isFollowPlayer)
        {
            MovigToPlayer();
        }

        if (_isSooting)
        {
            if (_readyForShot)
            {
                Shot();
            }
        }
        //if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        //}
        //else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && (Vector2.Distance(transform.position, player.position) > retreatDistance))
        //{
        //    transform.position = this.transform.position;
        //}
        //else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        //}

        //if (timeBtwShots <= 0)
        //{
        //    Instantiate(projectile, transform.position, Quaternion.identity);
        //    timeBtwShots = startTimeBtwShots;
        //}
        //else
        //{
        //    timeBtwShots -= Time.deltaTime;
        //}


    }
    
    private void MovigToPlayer()
    {
        var localPosition = transform.position;
        var playerPosition = _player.position;
        var direction = new Vector2(playerPosition.x - localPosition.x, playerPosition.y - localPosition.y);

        _rigidbody2D.velocity = direction.normalized * Speed;
    }
}
