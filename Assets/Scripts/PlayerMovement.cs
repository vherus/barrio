using UnityEngine;
using Barrio.Enums;
using Barrio.Core;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementForce = 5f;

    [SerializeField]
    private float jumpForce = 5f;

    private float movementX;
    private bool isGrounded;

    private Rigidbody2D body;
    private SpriteRenderer sRenderer;
    private Animator anim;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        HandleMovement();
        HandleJumpKeyPress();
        HandleFall();
    }

    private void HandleFall() {
        if (body.velocity.y < 0) {
            SetIsFalling(true);
            return;
        }

        SetIsFalling(false);
    }

    private void HandleJumpKeyPress() {
        if (Input.GetButton(Barrio.Enums.Animation.Jump.ToString()) && isGrounded) {
            PlayerJump();
        }
    }

    private void PlayerJump() {
        isGrounded = false;
        SetIsJumping(true);
        body.velocity = new Vector2(0f, 0f);
        body.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    private void HandleMovement() {
        movementX = Input.GetAxisRaw(Axis.Horizontal.ToString());

        if (movementX == 0) {
            StopWalking();
            return;
        }

        StartWalking();
    }

    private void StopWalking() {
        SetIsWalking(false);
    }

    private void StartWalking() {
        FaceInputDirection();
        SetIsWalking(true);

        transform.position += new Vector3(movementX, 0f, 0f) * movementForce * Time.deltaTime;
    }

    private void FaceInputDirection() {
        sRenderer.flipX = movementX < 0;
    }

    private void SetIsWalking(bool isWalking) {
        anim.SetBool(Barrio.Enums.Animation.Walk.ToString(), isWalking);
    }

    private void SetIsJumping(bool isJumping) {
        if (isJumping) {
            SetIsFalling(false);
        }

        anim.SetBool(Barrio.Enums.Animation.Jump.ToString(), isJumping);
    }

    private void SetIsFalling(bool isFalling) {
        if (isFalling) {
            SetIsJumping(false);
        }

        anim.SetBool(Barrio.Enums.Animation.Fall.ToString(), isFalling);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        TaggedCollider tc = new TaggedCollider(collision.collider);

        if ((tc.IsSolidBlock() || tc.IsQuestionBlock()) && IsPlayerAboveCollider(collision)) {
            GroundPlayer();
            return;
        }
        
        if (tc.IsGround()) {
            GroundPlayer();
        }
    }

    private bool IsPlayerAboveCollider(Collision2D collision) {
        Bounds cBounds = collision.collider.bounds;
        float colliderPos = cBounds.extents.y + cBounds.center.y;
        float playerPos = transform.position.y;

        return playerPos > colliderPos;
    }

    private void GroundPlayer() {
        isGrounded = true;
        SetIsJumping(false);
        SetIsFalling(false);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        TaggedCollider tc = new TaggedCollider(collider);

        if (tc.IsBottom()) {
            Destroy(gameObject);
        }
    }
}
