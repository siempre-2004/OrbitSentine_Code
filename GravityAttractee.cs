using UnityEngine;

public class Attractees : MonoBehaviour
{
    public float speed = 5f; 
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveRight();
        GravityHandler.attractees.Add(rb); 
    }

    void Update()
    {
        MoveRight();
    }

    void MoveRight()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void OnDestroy()
    {
        GravityHandler.attractees.Remove(rb); 
    }
}
