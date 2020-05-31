using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    TimeFlow timeFlow;
    [SerializeField]
    Transform gunContainer = null;
    [SerializeField]
    Transform firePosition = null;
    [SerializeField]
    GameObject bulletPrefab = null;
    [SerializeField]
    float fireVelocity = 1f;
    float lastShot = 0f;
    public float shotDelay = 0.2f;

    [SerializeField] string shotSound = "";
    private void Awake()
    {
        timeFlow = GetComponent<TimeFlow>();
        lastShot = Time.time;
    }
    public bool CanFire()
    {
        return Time.time - lastShot > shotDelay;
    }

    public void FireGun(Vector2 targetPos)
    {
        PointGun(targetPos);
        FireBullet();
        lastShot = Time.time;
        if (shotSound != "") AudioManager.instance.Play(shotSound);
    }

    public void PointGun(Vector2 targetPos)
    {
        gunContainer.up = targetPos - (Vector2)transform.position;
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = firePosition.position;
        bullet.transform.rotation = gunContainer.rotation;
        bullet.GetComponent<Rigidbody2D>().velocity = timeFlow.GetVelocity(gunContainer.up * fireVelocity);
    }
}
