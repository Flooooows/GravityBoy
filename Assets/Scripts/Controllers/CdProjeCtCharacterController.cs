using CdProjeCt.EventTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class CdProjeCtCharacterController : MonoBehaviour
{

    // add speed control
    [SerializeField]
    int speed = 0;
    [SerializeField]
    int jumpForce = 0;

    [SerializeField]
    Transform groundCheckA = null;
    [SerializeField]
    Transform groundCheckB = null;
    [SerializeField]
    LayerMask groundLayers = 0;
    [SerializeField]
    Animator animator = null;

    [SerializeField]
    bool moveInWorldSpace = false;

    [SerializeField]
    float deathAnimationTime = 0;


    [Header("No Gravity")]
    [SerializeField]
    int noGravitySpeed = 0;
    [SerializeField]
    int gravityFallingAfterNoGravity = 0;
    [SerializeField]
    int noGravityMaxStamina = 0;
    [SerializeField]
    int noGravityRegen = 0;
    [SerializeField]
    int noGravityConsume = 0;

    [Header("Events")]
    public FloatEvent onNoGravityStaminaChange;

    public UnityEvent onGravity;

    public UnityEvent onNoGravity;


    float horizontalSpeed = 0f;
    float jumpSpeed = 0f;
    float verticalSpeed = 0f;
    bool isGrounded = false;
    bool isFlipped = false;
    bool isDying = false;

    float initialGravity;
    bool isNoGravity = false;
    int noGravityStamina;
    bool fallingFromNoGravity = false;

    Transform myTransform;
    Rigidbody2D myRigidbody;

    float horizontalInput;
    float verticalInput;
    float jumpInput;

    public float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }
    public float JumpInput { get => jumpInput; set => jumpInput = value; }
    public float VerticalInput { get => verticalInput; set => verticalInput = value; }
    public float GravityInput { get; set; } = 0f;

    private int NoGravityStamina
    {
        get => noGravityStamina;
        set
        {
            noGravityStamina = value;
            onNoGravityStaminaChange.Invoke(value);
        }
    }
    public bool IsDying { get => isDying; set => isDying = value; }

    IEnumerator consumeNoGravityCoroutine = null;
    IEnumerator regenNoGravityCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        this.HorizontalInput = 1f;
        myTransform = transform;
        isFlipped = myTransform.right.x < 0;
        myRigidbody = GetComponent<Rigidbody2D>();
        initialGravity = myRigidbody.gravityScale;
        NoGravityStamina = noGravityMaxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
        CheckGround();
        CheckFlip();
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        if (animator == null)
        {
            return;
        }
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontalSpeed));
        animator.SetFloat("VerticalSpeed", myRigidbody.velocity.y);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsDying", isDying);
    }

    private void FixedUpdate()
    {
        if (!isDying)
        {
            if (isNoGravity)
            {
                NoGravityMove();
            }
            else
            {
                Move();
            }
        }

    }

    void CheckFlip()
    {
        if ((horizontalSpeed > 0.001f && isFlipped) || (!isFlipped && horizontalSpeed < -0.001f))
            UpdateFlip();
    }

    void UpdateFlip()
    {
        isFlipped = !isFlipped;
        float rotationAngle = isFlipped ? 180f : -180f;
        myTransform.Rotate(new Vector3(0f, rotationAngle, 0f), Space.Self);
    }

    void ReadInput()
    {
        if (isDying) return;
        if (moveInWorldSpace)
        {
            if (GravityInput > 0.001f)
            {
                if (!isNoGravity)
                {
                    DisableGravity();
                }
            }
            else if (isNoGravity)
            {
                EnableGravity();
            }
        }
        if (isNoGravity)
        {
            horizontalSpeed = horizontalInput * noGravitySpeed;
            verticalSpeed = verticalInput * noGravitySpeed;
        }
        else
        {
            horizontalSpeed = horizontalInput * speed;
            verticalSpeed = verticalInput * speed;
        }
        if (!moveInWorldSpace)
            horizontalSpeed = horizontalSpeed * Mathf.Sign(myTransform.right.x);
        jumpSpeed = jumpInput * jumpForce;
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckA.position, groundCheckB.position, groundLayers);
    }

    private void Move()
    {
        myRigidbody.gravityScale = fallingFromNoGravity ? gravityFallingAfterNoGravity : initialGravity;
        bool shouldJump = (jumpSpeed > 0f) && isGrounded;
        if (shouldJump)
            myTransform.gameObject.GetComponent<PlayerSoundManager>().playJumpSound();
        float yVelocity = shouldJump ? jumpSpeed : myRigidbody.velocity.y;
        myRigidbody.velocity = new Vector2(horizontalSpeed, yVelocity);
    }

    private void NoGravityMove()
    {
        myRigidbody.gravityScale = 0f;
        myRigidbody.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }

    IEnumerator NoGravityTimeCoroutine()
    {
        while (NoGravityStamina != 0)
        {
            if (NoGravityStamina - noGravityConsume < 0)
                NoGravityStamina = 0;
            else
                NoGravityStamina -= noGravityConsume;
            yield return new WaitForSeconds(0.1f);
        }
        EnableGravity();
    }

    IEnumerator NoGravityRegenCoroutine()
    {
        while (!isGrounded)
        {
            fallingFromNoGravity = true;
            yield return new WaitForFixedUpdate();
        }
        fallingFromNoGravity = false;
        while (NoGravityStamina != noGravityMaxStamina)
        {
            if (NoGravityStamina + noGravityRegen > noGravityMaxStamina)
                NoGravityStamina = noGravityMaxStamina;
            else
                NoGravityStamina += noGravityRegen;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void EnableGravity()
    {
        if (consumeNoGravityCoroutine != null)
        {
            StopCoroutine(consumeNoGravityCoroutine);
            consumeNoGravityCoroutine = null;
        }
        onGravity.Invoke();
        isNoGravity = false;
        regenNoGravityCoroutine = NoGravityRegenCoroutine();
        StartCoroutine(regenNoGravityCoroutine);
    }

    private void DisableGravity()
    {
        if (regenNoGravityCoroutine != null)
        {
            StopCoroutine(regenNoGravityCoroutine);
            regenNoGravityCoroutine = null;
        }
        onNoGravity.Invoke();
        isNoGravity = true;
        consumeNoGravityCoroutine = NoGravityTimeCoroutine();
        StartCoroutine(consumeNoGravityCoroutine);
    }

    public void DeathAnimationAndDestroy()
    {
        PlayerSoundManager source = myTransform.gameObject.GetComponent<PlayerSoundManager>();
        if(source != null)
            source.playDeathSound();
        isDying = true;
        myRigidbody.velocity = new Vector2(0, 0);
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(deathAnimationTime);
        Pooling.Release(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (groundCheckA == null || groundCheckB == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundCheckA.position, new Vector2(groundCheckB.position.x, groundCheckA.position.y));
        Gizmos.DrawLine(new Vector2(groundCheckB.position.x, groundCheckA.position.y), groundCheckB.position);
        Gizmos.DrawLine(groundCheckB.position, new Vector2(groundCheckA.position.x, groundCheckB.position.y));
        Gizmos.DrawLine(new Vector2(groundCheckA.position.x, groundCheckB.position.y), groundCheckA.position);
    }
}
