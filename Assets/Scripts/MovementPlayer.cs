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

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    void Update()
    {
        //while (!IsGrounded()) { speed = 0f; }
        horizontal = Input.GetAxisRaw("Horizontal");//pobieram z klawiatury
        //czy gracz naciska a lub d albo <- lub ->
        //przyjmuje warto�ci -1, 0, 1

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
        Flip();


    }
    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else {
            rb.velocity = new Vector2(horizontal * speed/2, rb.velocity.y);
        }
        if (isTouchingMovingPlatform)
        {
            rb.velocity = new Vector2((platformRB.velocity.x + horizontal*4), rb.velocity.y-4);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }//przemieszczam si� w stron� horizontal z pr�dko�ci� po osi x, oraz z 
    //stala predkoscia na osi y

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }//sprawdzam czy kwadracik wlozony w postac dotyka jednostki ground
     //position: Pozycja �rodka okr�gu, kt�rego kolizje chcesz wykry�.
     //radius: Promie� okr�gu.
     //layerMask (opcjonalny): Okre�la, kt�re warstwy fizyczne bior� udzia� w wykrywaniu kolizji. 
     //Je�li nie zostanie podana, sprawdzane s� wszystkie warstwy.
    private void Flip() {
        if (isFacingRight && horizontal<0f || !isFacingRight && horizontal>0f) {
            isFacingRight = !isFacingRight;//zmieniam warto�c bool po obrocie
            Vector3 localScale = transform.localScale;// Pobranie aktualnej skali lokalnej obiektu
            localScale.x = -1f;// Zmiana skali wzd�u� osi X na -1, co spowoduje odwr�cenie obiektu wzgl�dem osi Y
            transform.localScale = localScale; // Ustawienie zmienionej skali lokalnej z powrotem na obiekcie
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin")) {
            Destroy(other.gameObject);
            coinmenager.coinCounter++;
        }
    }
}
