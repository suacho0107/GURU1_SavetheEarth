using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    Bullet bullet;

    [SerializeField]
    Transform firePos;

    [SerializeField] ParticleSystem flash;

    public float damage = 1f;
    float shotsPerSec = 2f;
    float nextShotTime;

    public void Shoot()
    {
        // Time.time: 현재 프레임의 시작 지점
        if (nextShotTime <= Time.time)
        {
            Bullet _bullet = Instantiate(bullet, firePos.position, firePos.rotation);
            _bullet.damage = damage;

            flash.Play();

            nextShotTime = Time.time + (1 / shotsPerSec);
        }
    }
}
