using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{

    [SerializeField] private float speed;

    [HideInInspector] public Vector2 direction;

    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();   
    }

    private void Start()
    {
        rb2D.velocity = direction * speed;
    }
}
