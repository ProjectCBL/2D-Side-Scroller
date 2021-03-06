using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public Rigidbody2D rb;
    public float bulletSpeed = 40.0f;
    [Range(0, 5.0f)] public float maxBulletLifeSpan = 3.0f;

    [SerializeField] private float currentLifeSpan = 0.0f;

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
        string[] tagCollection = new string[] { "Caterpillar", "Yellow Bee", "Blue Bee" };

        if(tagCollection.Contains(collision.tag))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else if(collision.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }

}
