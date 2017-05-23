using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    public float speed = 1f;
    Rigidbody2D myBody = null;
    Animator animator = null;
    SpriteRenderer spriteRenderer = null;
    Transform parent = null;

    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;

    // Use this for initialization
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        LevelController.current.setStartPosition(transform.position);

        this.parent = this.transform.parent;
    }
	
	// Update is called once per frame
	void Update()
    {

    }

    void FixedUpdate()
    {
        float value = Input.GetAxis("Horizontal");
        if(Mathf.Abs(value) > 0)
        {
            moveBody(value);
            animate("run", true);
        }
        else
        {animate("run", false);}

        if(value < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(value > 0)
        {
            spriteRenderer.flipX = false;
        }

        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("GroundLayer");
        //Перевіряємо чи проходить лінія через Collider з шаром Ground
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if(hit)
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

        if(this.isGrounded)
        {animate("jump", false);}
        else
        {animate("jump", true);}
    }

    void moveBody(float howMuch)
    {
        Vector2 vel = myBody.velocity;
        vel.x = howMuch * speed;
        myBody.velocity = vel;
    }

    void animate(string what, bool toAnimate)
    {animator.SetBool(what, toAnimate);}

    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            obj.transform.parent = new_parent;
        }
    }
}
