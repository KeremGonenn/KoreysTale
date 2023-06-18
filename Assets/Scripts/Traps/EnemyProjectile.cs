using System.Collections;
using UnityEngine;

public class EnemyProjectile : EnemyDamage //Her dokundu�unda hasar verecek
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakenDamage(damage);
        }
        gameObject.SetActive(false);         //Herhangi bir objeye �arpt���nda deaktif ol
    }
}
