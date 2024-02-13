using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private WeaponBase weapon;

    private Transform weaponTransform;
    private Vector2 moveVal;
    private Rigidbody2D rb;
    private Vector3 mousePosition;
    private SpriteRenderer weaponSpriteRenderer;

    private bool fireActivate = false;
    private float lastFireTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        weaponTransform = weapon.transform;
        weaponSpriteRenderer = weapon.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - weaponTransform.position).normalized;

        bool isFacingRight = mousePosition.x > transform.position.x;
        transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);

        RotateWeapon(direction, isFacingRight);
    }

    private void RotateWeapon(Vector2 direction, bool isFacingRight)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bool isWeaponAimedUp = (angle > 0 && angle < 180) || (angle < -180 && angle > -360);
        bool isWeaponAimedDown = (angle < 0 && angle > -180) || (angle > 180 && angle < 360);

        if (!isFacingRight)
            angle -= 180f;

        if (isWeaponAimedDown)
        {
            weaponSpriteRenderer.sortingLayerName = "WeaponDown";
        }
        else if (isWeaponAimedUp)
        {
            weaponSpriteRenderer.sortingLayerName = "WeaponUp";
        }

        weaponTransform.rotation = Quaternion.Euler(0, 0, angle);

        if (fireActivate)
        {
            weapon.Fire(direction);
        }
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
        fireActivate = input.isPressed;
    }
}
