using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTargetBullet : MonoBehaviour, IBullet
{
    string ENEMY = "Enemy";
    public float bulletDamage;

    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.gameObject.CompareTag(ENEMY)) return;

        if (coli.gameObject.GetComponent<IDamageable>() != null)
            coli.gameObject.GetComponent<IDamageable>().GetDamage(bulletDamage, transform);

        gameObject.SetActive(false);
    }

    public void GetInitDamage(float damage)
    {
        bulletDamage = damage;
    }
}
