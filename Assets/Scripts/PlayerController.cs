using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform weapon;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private Transform shootPoint;

    [SerializeField] SpriteRenderer spriteRenderer;

    private Vector2 moveVal;
    private Rigidbody2D rb;
    private Vector3 mousePosition;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - transform.position).normalized;

        bool isFacingRight = mousePosition.x > transform.position.x;
        transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bool isWeaponAimedUp = (angle > 0 && angle < 180) || (angle < -180 && angle > -360);
        bool isWeaponAimedDown = (angle < 0 && angle > -180) || (angle > 180 && angle < 360);

        if (!isFacingRight)
            angle -= 180f;


        if (isWeaponAimedDown)
        {
            spriteRenderer.sortingLayerName = "WeaponDown";
        }
        else if (isWeaponAimedUp)
        {
            spriteRenderer.sortingLayerName = "WeaponUp";
        }

        weapon.rotation = Quaternion.Euler(0, 0, angle);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

    }

    private void FixedUpdate()
    {
        rb.velocity = moveVal * moveSpeed;
    }

    private void OnMove(InputValue input)
    {
        moveVal = input.Get<Vector2>();
    }

    private void OnFire(InputValue input)
    {
        impulseSource.GenerateImpulse();
    }
}
