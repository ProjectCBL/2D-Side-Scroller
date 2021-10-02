using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public Rigidbody2D rb;
    public float bulletSpeed = 40.0f;
    [Range(0, 5.0f)] public float maxBulletLifeSpan = 3.0f;

    [SerializeField] private float currentLifeSpan = 0.0f;

    // Update is called once per frame
    void Update()
    {
        currentLifeSpan += Time.deltaTime;
        DestroyBulletIfExceedsLifeSpan();
    }

    public void PropellBullet()
    {
        rb.AddForce(
            Vector2.right * transform.localScale.x * bulletSpeed,
            ForceMode2D.Impulse
        );
    }

    private void DestroyBulletIfExceedsLifeSpan()
    {
        if (currentLifeSpan >= maxBulletLifeSpan) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }

}
