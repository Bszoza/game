using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    private float horizontal;
    private float speed = 6f;
    private float jumpStr = 9f;
    private bool isFacingRight = true;
    public CoinMenager coinmenager;

    public bool isTouchingMovingPlatform = false;
    public Rigidbody2D platformRB;


    private bool isWallSliding;
    private float wallSlidingSpeed = 1f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    void Update()
    {
        //while (!IsGrounded()) { speed = 0f; }
        
            horizontal = Input.GetAxisRaw("Horizontal");
        //pobieram z klawiatury
         //czy gracz naciska a lub d albo <- lub ->
         //przyjmuje wartoœci -1, 0, 1


        if (Input.GetButtonDown("Jump") && isTouchingMovingPlatform)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStr*1.80f);
        }
        else if (Input.GetButtonDown("Jump") && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpStr);
        }
        //poruszam sie do gory o jumpstr
        else if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }

    }
    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

        if (IsGrounded())
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
      //  else
      //  {
      //       rb.velocity = new Vector2(horizontal * speed/2, rb.velocity.y);
      //  }
        if (isTouchingMovingPlatform)
        {
            rb.velocity = new Vector2((platformRB.velocity.x + horizontal*4), rb.velocity.y-4);
        }
        else 
        {
           rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
         }
      

    }//przemieszczam siê w stronê horizontal z prêdkoœci¹ po osi x, oraz z 
    //stala predkoscia na osi y

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }//sprawdzam czy kwadracik wlozony w postac dotyka jednostki ground
     //position: Pozycja œrodka okrêgu, którego kolizje chcesz wykryæ.
     //radius: Promieñ okrêgu.
     //layerMask (opcjonalny): Okreœla, które warstwy fizyczne bior¹ udzia³ w wykrywaniu kolizji. 
     //Jeœli nie zostanie podana, sprawdzane s¹ wszystkie warstwy.



    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, (float)(wallJumpingPower.y*0.5));
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }


    private void Flip() {
        if (isFacingRight && horizontal<0f || !isFacingRight && horizontal>0f) {
            isFacingRight = !isFacingRight;//zmieniam wartoœc bool po obrocie
            Vector3 localScale = transform.localScale;// Pobranie aktualnej skali lokalnej obiektu
            localScale.x *= -1f;// Zmiana skali wzd³u¿ osi X na -1, co spowoduje odwrócenie obiektu wzglêdem osi Y
            transform.localScale = localScale; // Ustawienie zmienionej skali lokalnej z powrotem na obiekcie
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coinmenager.coinCounter++;
        }

    }
}
