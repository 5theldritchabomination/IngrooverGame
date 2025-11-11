using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1f;
    public Sprite idleSprite;
    public Sprite movingSprite;
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer spriteRenderer;

    private float animationTimer = 0f;
    public float frameTime = 0.5f;
    private bool showingIdle = true;
    public GameController gameControllerObj;

    public PlayerController playerObj;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite;
        gameControllerObj = FindFirstObjectByType<GameController>();
        playerObj = FindFirstObjectByType<PlayerController>();
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.y += speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);

        animationTimer += Time.deltaTime;
        if (animationTimer >= frameTime)
        {
            showingIdle = !showingIdle;
            spriteRenderer.sprite = showingIdle ? idleSprite : movingSprite;
            animationTimer = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
            Die();
        }
        projectile projectile = other.gameObject.GetComponent<projectile>();
        if (projectile != null)
        {
            Die();
            gameControllerObj.AddScore(+1);
        }
        
        if (other.CompareTag("Down_border"))
        {
            
            playerObj.ChangeHealth(-1);
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
