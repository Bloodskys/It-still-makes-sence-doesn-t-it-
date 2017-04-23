using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 2f;
    public bool facingRight = true;
    public float jumpForce;
    private Rigidbody2D rb;
    private Animator anim;
    private static Vector3 START_POS = new Vector3(-11.2f, -1.08f, 0);
    private bool lastWalkState;
    private bool lastJumpState;
    private bool lastFallState;
    private bool lastCrouchState;
    private Vector2 boxColliderOffset;
    private Vector2 crouchBoxColliderOffset;
    private Vector2 circleColliderOffset;
    private Vector2 crouchCircleColliderOffset;
    private float circleColliderRadius;
    private float crouchCircleColliderRadius;
    private static GameObject player;
    private static GameObject ghost;
    public static GameObject Player
    {
        get
        {
            return player;
        }
    }
    enum State
    {
        Idle,
        Walk,
        Jump,
        Fall,
        Crouch
    }
    State state;
    // Use this for initialization
    void Start ()
    {
        boxColliderOffset = GetComponent<BoxCollider2D>().offset;
        crouchBoxColliderOffset = boxColliderOffset + new Vector2(0, -.4f);
        circleColliderOffset = GetComponent<CircleCollider2D>().offset;
        crouchCircleColliderOffset = circleColliderOffset + new Vector2(0, .19f);
        circleColliderRadius = GetComponent<CircleCollider2D>().radius;
        crouchCircleColliderRadius = circleColliderRadius - 0.1f;
        if (player == null)
        {
            player = gameObject;
        }
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SetState(State.Idle);
        if (ghost == null)
        {
            ghost = Instantiate(gameObject);
            Vector3 ghostPos = player.transform.position;
            ghostPos.x -= 24;
            ghost.transform.position = ghostPos;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        float move = 1f;//*/ Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
		//Crouch 
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<BoxCollider2D>().offset = crouchBoxColliderOffset;
            GetComponent<CircleCollider2D>().offset = crouchCircleColliderOffset;
            GetComponent<CircleCollider2D>().radius = crouchCircleColliderRadius;
            SetState(State.Crouch);
        }
        else
        {
            GetComponent<BoxCollider2D>().offset = boxColliderOffset;
            GetComponent<CircleCollider2D>().offset = circleColliderOffset;
            GetComponent<CircleCollider2D>().radius = circleColliderRadius;
            #region Walk
            if (Mathf.Abs(move) > 0 && lastWalkState == false)
            {
                SetState(State.Walk);
            }
            else if (Mathf.Abs(move) == 0 && lastWalkState == true)
            {
                SetState(State.Idle);
            }
            #endregion
            #region Jump
            if (rb.velocity.y > 0 && lastJumpState == false)
            {
                SetState(State.Jump);
            }
            else if (rb.velocity.y < 0 && lastFallState == false)
            {
                SetState(State.Fall);
            }
            else if (rb.velocity.y == 0 && (lastFallState || lastJumpState))
            {
                SetState(State.Idle);
            }
            #endregion
        }

        if (player.transform.position.x >= 11)
        {
            ghost.SetActive(true);
            Vector3 ghostPos = player.transform.position;
            ghostPos.x -= 24;
            ghost.transform.position = ghostPos;
            if (player.transform.position.x >= 12)
            {
                player.transform.position = ghost.transform.position;
                ghostPos = player.transform.position;
                ghostPos.x -= 24;
                ghost.transform.position = ghostPos;
                Globals.RaiseMapComplete(null, null);
            }
        }
        else
        {
            ghost.SetActive(false);
        }
    }

    void SetState(State state)
    {
        switch (state)
        {
            case State.Crouch:
                anim.SetBool("Walk", false);
                anim.SetBool("Jump", false);
                anim.SetBool("Fall", false);
                anim.SetBool("Crouch", true);
                break;
            case State.Fall:
                anim.SetBool("Walk", false);
                anim.SetBool("Jump", false);
                anim.SetBool("Fall", true);
                anim.SetBool("Crouch", false);
                break;
            case State.Idle:
                anim.SetBool("Walk", false);
                anim.SetBool("Jump", false);
                anim.SetBool("Fall", false);
                anim.SetBool("Crouch", false);
                break;
            case State.Jump:
                anim.SetBool("Walk", false);
                anim.SetBool("Jump", true);
                anim.SetBool("Fall", false);
                anim.SetBool("Crouch", false);
                break;
            case State.Walk:
                anim.SetBool("Walk", true);
                anim.SetBool("Jump", false);
                anim.SetBool("Fall", false);
                anim.SetBool("Crouch", false);
                break;
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public static void Die()
    {
        player.transform.position = START_POS;
        Globals.Instance.ShakeCam(0.2f);
    }
}
