using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontroller : MonoBehaviour
{
    public float IceSpeedMultiplier = 1.2f;
    public float SpeedSwamp = 10;
    public float speed;
    //public float CooldownTime;
    public GameObject Projectile;
    public Transform BulletSpawnPoint;
    public bool IsOnIce = false;
    public bool IsOnSwamp = false;

    private Rigidbody2D _rigidbody2D;
    private bool _isFollowPlayer;
    private bool _isSooting;
    private bool _readyForShot = true;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    public void Start()
    {


        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = (moveInput.normalized * speed);
        CheckOnSwamp();
        CheckOnIce();

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shot();
        }
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    public void Shot()
    {
        if (_readyForShot)
        {
            var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target = new Vector3(target.x, target.y, transform.position.z);
            var heading = target - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;

            var bullet = Instantiate(Projectile, BulletSpawnPoint.position, Quaternion.identity);
            var bulletSpeed = bullet.GetComponent<Bullet>().speed;
            bullet.GetComponent<Rigidbody2D>().AddForce(heading.normalized * bulletSpeed);
            bullet.transform.right = heading.normalized;

            _readyForShot = false;
            StartCoroutine(ShotCooldown());
        }
    }

    private void CheckOnIce()
    {
        var lastRbVelocity = rb.velocity;

        if (IsOnIce)
            if (moveVelocity != Vector2.zero)
                rb.velocity = moveVelocity * IceSpeedMultiplier;
            else rb.velocity = lastRbVelocity;
        else rb.velocity = moveVelocity;
    }

    private void CheckOnSwamp()
    {
        if (IsOnSwamp)
        {
            moveVelocity -= moveVelocity.normalized * SpeedSwamp;
        }
    }

    private IEnumerator ShotCooldown()
    {
        var bullet = Instantiate(Projectile, BulletSpawnPoint.position, Quaternion.identity);
        var CooldownTime = bullet.GetComponent<Bullet>().CooldownTime;
        Destroy(bullet);
        yield return new WaitForSecondsRealtime(CooldownTime);
        _readyForShot = true;
    }
    private void StopFollowPlayer(bool condition)
    {
        if (!condition)
            _rigidbody2D.velocity = new Vector2(0, 0);
    }
}


//private void OnCollisionEnter2D(Collision2D collision)
//    {
//        //Debug.Log(collision.gameObject.name);
//    }

