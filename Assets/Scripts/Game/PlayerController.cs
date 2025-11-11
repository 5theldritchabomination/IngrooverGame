using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movementX;
    public int maxHealth = 5;
    public GameObject projectilePrefab;
    int currentHealth;
    public int health { get { return currentHealth; }}
    Vector2 movementVector;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        currentHealth = maxHealth;

    }
    void OnMove (InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementVector = new Vector2(0, 1f);

    }

    void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            Launch();
        }
    }
    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX, 0.0f);
        rb.AddForce(movement);

    }
    public void ChangeHealth (int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth);
    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);
        projectile projectile = projectileObject.GetComponent<projectile>();
        projectile.Launch(movementVector, 300);

    }

    void OnEscape(InputValue value)
    {
        if (value.isPressed)
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                EscapeUI.Instance.Show();
            } 
            else
            {
                Time.timeScale = 1;
                EscapeUI.Instance.Hide();
            }
        }
    }
    

}
