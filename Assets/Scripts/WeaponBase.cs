using Cinemachine;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected float fireRate = 1f;

    [SerializeField] protected GameObject muzzleEffect;
    [SerializeField] protected GameObject projectile;

    [SerializeField] protected Transform shootPoint;
    
    protected float lastFireTime;

    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = FindAnyObjectByType<CinemachineImpulseSource>();
    }

    public virtual void Fire(Vector2 direction)
    {
        if (lastFireTime + fireRate < Time.time)
        {
            impulseSource.GenerateImpulse();
            lastFireTime = Time.time;
            Attack(direction);
        }
    }

    protected virtual void Attack(Vector2 direction)
    {

    }

    protected virtual void EnableMuzzle()
    {
        muzzleEffect.SetActive(true);
        Invoke(nameof(HideMuzzleFlash), 0.1f);
    }

    private void HideMuzzleFlash()
    {
        muzzleEffect.SetActive(false);
    }
}
