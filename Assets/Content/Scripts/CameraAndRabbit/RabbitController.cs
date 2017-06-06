using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    public float speed = 1f;
    Rigidbody2D myBody = null;
    public AnimateController animateController = null;
    SpriteRenderer spriteRenderer = null;
    Transform parent = null;

    public bool isVulnerable;
    public float invulnerabilityTime;
    private float _invulnerabilityTime;

    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;

    void Awake()
    {
        LevelController.SetRabbit(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        isVulnerable = true;
        invulnerabilityTime = 4f;
        _invulnerabilityTime = invulnerabilityTime;

        myBody = this.GetComponent<Rigidbody2D>();
        animateController = this.GetComponent<AnimateController>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        LevelController.current.setStartPosition(transform.position);

        this.parent = this.transform.parent;

        Physics2D.IgnoreLayerCollision(12, 10, true);
    }
	
	// Update is called once per frame
	void Update()
    {
        checkVulnerabilityDuration();
    }

    void FixedUpdate()
    {
        CameraConfig cameraWhichFollowsUs = LevelController.current.cameraWhichLooksForRabbit;

        if (this.isVulnerable)
        {
            Physics2D.IgnoreLayerCollision(11, 10, false);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(11, 10, true);
        }


        float value = Input.GetAxis("Horizontal");
            if (Mathf.Abs(value) > 0)
            {
                cameraWhichFollowsUs.playSoundRabbitWalks();
                moveBody(value);
                animateController.animate("run", true);
            }
            else
            {
                cameraWhichFollowsUs.stopSoundRabbitWalks();
                animateController.animate("run", false);
            }

            if (value < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (value > 0)
            {
                spriteRenderer.flipX = false;
            }

            Vector3 from = transform.position + Vector3.up * 0.3f;
            Vector3 to = transform.position + Vector3.down * 0.1f;
            int layer_id = 1 << LayerMask.NameToLayer("GroundLayer");
            //Перевіряємо чи проходить лінія через Collider з шаром Ground
            RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
            if (hit)
            {
                isGrounded = true;

                //Перевіряємо чи ми опинились на платформі
                if (hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null)
                {
                    //Приліпаємо до платформи
                    SetNewParent(this.transform, hit.transform);
                }
            }
            else
            {
                isGrounded = false;

                SetNewParent(this.transform, this.parent);
            }

            //Намалювати лінію (для розробника)
            Debug.DrawLine(from, to, Color.red);

            //HeroRabit::FixedUpdate
            //Якщо кнопка тільки що натислась
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                this.JumpActive = true;
            }
            if (this.JumpActive)
            {
                //Якщо кнопку ще тримають
                if (Input.GetButton("Jump"))
                {
                    this.JumpTime += Time.deltaTime;
                    if (this.JumpTime < this.MaxJumpTime)
                    {
                        Vector2 vel = myBody.velocity;
                        vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                        myBody.velocity = vel;
                    }
                }
                else
                {
                    this.JumpActive = false;
                    this.JumpTime = 0;
                }
            }

            if (this.isGrounded)
            {
                cameraWhichFollowsUs.playSoundRabbitFalls();
                animateController.animate("jump", false);
            }
            else
            { animateController.animate("jump", true); }
    }

    private void checkVulnerabilityDuration()
    {
        if (isVulnerable)
        {
            return;
        }
        else
        {
            _invulnerabilityTime -= Time.deltaTime;
            if (_invulnerabilityTime <= 0)
            {
                _invulnerabilityTime = invulnerabilityTime;
                isVulnerable = true;
                this.gameObject.GetComponent<FlashObject>().stopBlinking();
            }
        }
    }

    void moveBody(float howMuch)
    {
        Vector2 vel = myBody.velocity;
        vel.x = howMuch * speed;
        myBody.velocity = vel;
    }

    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            obj.transform.parent = new_parent;
        }
    }

}
