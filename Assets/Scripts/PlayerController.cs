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

    private ForceBarController forceBar;
    private HealthBarController healthBar;
    private StaminaController staminaBar;
    private SystemController system;
    
    void Start()
    {
        system = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SystemController>();
        angle = GetComponentInChildren<Transform>().GetChild(0).transform;        
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarController>();
        forceBar = GameObject.FindGameObjectWithTag("BarForce").GetComponent<ForceBarController>();
        staminaBar = GameObject.FindGameObjectWithTag("Stamina").GetComponent<StaminaController>();
    }


    void Update()
    {
        PlayerMoviment();
        ShootingAngleController();
        BarForceController(); 
        UtilitiesResources();   
    }

    void PlayerMoviment()
    {
        void ConsumeStamina(float cost) { staminaBar.ChangeStaminaBarValue(cost);  }
        void Move(float playerSpeed) { 
            transform.Translate(playerSpeed * Time.deltaTime, 0f, 0f);
            transform.Translate(new Vector3(0, 180, 0));
        }        

        if (Input.GetKey(KeyCode.LeftArrow)) { Move(-playerSpeed); ConsumeStamina(playerSpeed); }
        if (Input.GetKey(KeyCode.RightArrow)) { Move(playerSpeed); ConsumeStamina(playerSpeed); }
    }

    void ShootingAngleController()
    {
        void SetAngle(int value) { angle.Rotate(new Vector3(0, 0, Time.deltaTime * value)); }

        if (Input.GetKey(KeyCode.UpArrow)) { SetAngle(angleDirection); }
        if (Input.GetKey(KeyCode.DownArrow)) { SetAngle(-angleDirection); }
    }

    void BarForceController()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            forceBar.ChangeShotForce();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {            
            forceBar.ResetForceBarValue();
            system.timer.enabled = false;            
            ThrowBullet();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            system.timer.enabled = false;
            system.canStartRoundTimer = false;
            system.timer.StopCoroutine("StartTimer");            
        }
    }

    void UtilitiesResources()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            system.PassRound();
            staminaBar.ChangeStaminaBarValue(100);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            healthBar.ChangeHealthBarValue(25);
            staminaBar.ChangeStaminaBarValue(-25);
        }
    }
    void ThrowBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnBullet.position, angle.rotation);        
        bullet.GetComponent<Rigidbody2D>().velocity = spawnBullet.up * bulletSpeed * forceBar.lastForce;
        //StartCoroutine(IgnoreSelfCollision(1f, bullet));
    } 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damageable") || collision.gameObject.CompareTag("Player"))
        {            
            healthBar.ChangeHealthBarValue(-10);
        }
    }

    IEnumerator IgnoreSelfCollision(float seconds, GameObject bullet)
    {
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        yield return new WaitForSeconds(seconds);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
    }
}
