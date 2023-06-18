using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header ("SpikeHead �zellikleri")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTimer;
    private Vector3 destination;
    private bool attacking;
    private Vector3[] directions = new Vector3[4];


    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        //E�er sald�r�yorsa konuma hareket ettir
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }
    private void CheckForPlayer()
    {
        //Spikehead karakteri g�r�nce takip etmesi
        CalculateDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range; //Sa� y�n
        directions[1] = -transform.right * range; //Sol y�n
        directions[2] = transform.up * range; //Yukar� y�n
        directions[3] = -transform.up * range; //A�a�� y�n
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakenDamage(damage);
        }
        Stop();
    }
}
