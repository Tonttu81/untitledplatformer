using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Variables

    // Walking / moving left/right & up/down
    Vector3 playerMovement;
    Vector3 verticalMovement;
    public float moveSpeed;
    public float airSpeed;
    public bool isGrounded;

    // Boosting
    Vector2 pointerDir;
    Vector2 lastDir;
    public float boostForce;
    public float trampolineForce;
    public float freezeTime;
    public float platformStickingTime;
    [SerializeField]private float timer;
    private float range = 0.8f;
    private float force;
    private float gravity;
    bool hitTarget;
    bool frozen;
    bool boosted;
    bool stuck;
    bool timerEnded;
    public bool horizontalTrigger;
    public bool upTrigger;

    // Components
    public LayerMask targetMask;
    public Camera cam;
    Sounds sounds;
    Rigidbody2D rb2D;

    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sounds = GetComponent<Sounds>();
        timer = freezeTime;
        force = boostForce;
        gravity = rb2D.gravityScale;
    }

    void Update()
    {
        Pointer();

        if (frozen)
        {
            timer -= 1f * Time.deltaTime;
            if (timer <= 0)
            {
                Thaw();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded && !horizontalTrigger)
        {
            GroundMovement();
        }
        else if (horizontalTrigger && !timerEnded)
        {
            VerticalMovement();
        }
        else if (isGrounded && timerEnded)
        {
            GroundMovement();
        }
        else if (!isGrounded && upTrigger)
        {
            GroundMovement();
        }
        else if (!isGrounded)
        {
            AirMovement();
        }
        
        if (hitTarget)
        {
            Boost();
        }
    }

    void GroundMovement()
    {
        playerMovement = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * 100 * Time.deltaTime, rb2D.velocity.y);
        rb2D.velocity = playerMovement;
    }
    void AirMovement()
    {
        playerMovement = new Vector3(Input.GetAxisRaw("Horizontal") * airSpeed * Time.deltaTime, 0f);
        rb2D.velocity += new Vector2(playerMovement.x, 0f);
    }
    void VerticalMovement()
    {
        verticalMovement = new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * 100 * Time.deltaTime);
        rb2D.velocity = verticalMovement;
    }

    void Pointer()
    {
        pointerDir = new Vector2(Input.GetAxisRaw("PointerHorizontal"), Input.GetAxisRaw("PointerVertical")).normalized;
        RaycastHit2D pointerRay = Physics2D.Raycast(transform.position, pointerDir, range, targetMask);
        Debug.DrawRay(transform.position, pointerDir * range, Color.red, 1f);
        if (pointerRay.collider != null)
        {
            if (boosted == false)
            {
                Freeze();
                lastDir = pointerDir;
                if (stuck)
                {
                    timer = 0f;
                }
                if (pointerRay.collider.tag == "Trampoline")
                {
                    force = trampolineForce;
                    Shake.Instance.ShakeCamera(45f, 0.07f);
                    sounds.PlayTrampolineSound();
                }
                else
                {
                    force = boostForce;
                    Shake.Instance.ShakeCamera(10f, 0.05f);
                    sounds.PlayBoostSound();
                }
            }
        }
        else if (pointerRay.collider == null)
        {
            boosted = false;
        }
    }

    void Boost()
    {
        boosted = true;
        rb2D.velocity = new Vector2(0f, 0f);
        rb2D.AddForce(-lastDir * force * 100 * Time.deltaTime, ForceMode2D.Impulse);
        hitTarget = false;
    }

    void Freeze()
    {
        rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        frozen = true;
    }

    void Thaw()
    {
        rb2D.constraints = RigidbodyConstraints2D.None;
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        SetGravity();
        frozen = false;
        timer = freezeTime;
        hitTarget = true;
        stuck = false;
        timerEnded = true;
    }

    public void SetGravity()
    {
        rb2D.gravityScale = gravity;
    }

    public void HitStickyPlatform()
    {
        stuck = true;
        lastDir = pointerDir;
        timer = platformStickingTime;
        rb2D.velocity = new Vector2(0f, 0f);
        rb2D.gravityScale = 0f;
        frozen = true;
        timerEnded = false;
    }
}
