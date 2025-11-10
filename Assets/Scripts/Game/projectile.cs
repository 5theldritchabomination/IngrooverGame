using UnityEngine;
using UnityEngine.InputSystem;

public class projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        Vector2 defaultDirection = new Vector2(0,1);
        rigidbody2d.AddForce(defaultDirection * force);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.Die();
        }
        Debug.Log("Projectile collision with " + other.gameObject);
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
