using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{ 
    public string team;
    public bool havePlayed=false;

    public float playerSpeed = 1.5f;
    public int angleDirection = 20;    
    public Transform angle;

    public Transform spawnBullet;
    public GameObject bulletPrefab;

    public float shootForce = 0;
    public float bulletSpeed = 10;

    private BarForceController barForce;
    private LifeBarController lifeBar;
    private StaminaController stamina;
    private SystemController system;
    
    void Start()
    {
        angle = GetComponentInChildren<Transform>();
        stamina = GetComponent<StaminaController>();
        lifeBar = GetComponent<LifeBarController>();
        barForce = GetComponent<BarForceController>();
    }


    void Update()
    {
        PlayerMoviment();
        ChangeShootingAngle();
        BarForceController();
        AttackRosources();
        UtilitiesResources();   
    }

    void PlayerMoviment()
    {
        void Move(float playerSpeed) { transform.Translate(playerSpeed * Time.deltaTime, 0f, 0f); }


        if (Input.GetKey(KeyCode.LeftArrow)) { Move(-playerSpeed); }
        if (Input.GetKey(KeyCode.RightArrow)) { Move(playerSpeed); }
    }

    void ChangeShootingAngle()
    {
        void SetAngle(int value) { angle.Rotate(new Vector3(0, 0, Time.deltaTime * value)); }

        if (Input.GetKey(KeyCode.UpArrow)) { SetAngle(angleDirection); }
        if (Input.GetKey(KeyCode.DownArrow)) { SetAngle(-angleDirection); }
    }

    void BarForceController()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            barForce.ChangeShotForce();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {            
            barForce.ResetForceBarValue();
            //matchControll.timer.enabled = false;            
            ThrowBullet();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            system.timer.enabled = false;
            //matchControll.StopCoroutine("RoundTimer");
            //matchControll.canStartRoundTimer = false;
        }
    }

    void UtilitiesResources()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //matchControll.PassRound();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            lifeBar.ChangeValue(0.10f);
        }
    }

    void AttackRosources()
    {
        
    }

    void ThrowBullet()
    {
        //GameObject bullet = Instantiate(bulletPrefab, spawnBullet.position, aim.rotation);        
        //bullet.GetComponent<Rigidbody2D>().velocity = spawnBullet.up * bulletSpeed * barForce.lastForce;
        //StartCoroutine(IgnoreSelfCollision(1f, bullet));
    }

    void Stamina()
    {
        //if (transform.position)
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damageable") || collision.gameObject.CompareTag("Player"))
        {            
            lifeBar.ChangeValue(-1f);

            if (lifeBar.GetComponent<Slider>().value == 0)
            {
                //matchControll.isMatchOver = true;
            }
        }
    }

    IEnumerator IgnoreSelfCollision(float seconds, GameObject bullet)
    {
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        yield return new WaitForSeconds(seconds);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
    }
}
