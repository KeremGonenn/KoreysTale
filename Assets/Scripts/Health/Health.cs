using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 10;
    public int currentHealth;
    private Animator anim;
    private bool dead;
    public HealthBar healthBar;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakenDamage(int damage)
    {

        if (currentHealth > 0)
        {
            //Karakter Hasar
            anim.SetTrigger("hurt");
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            StartCoroutine(Invunerability());
        }
        else
        {
            //Karakter Ölüm
            if (!dead)
            {
                anim.SetTrigger("die");
                //Oyuncu
                if (GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().enabled = false;
                }

                //Düþman
                if (GetComponent<EnemyPatrol>() != null)
                {
                    GetComponent<EnemyPatrol>().enabled = false;
                }

                if (GetComponent<MeleeEnemy>() != null)
                {
                    GetComponent<MeleeEnemy>().enabled = false;
                }

                dead = true;
            }
        }
    }
    public void AddHealth(int _value)
    {
        currentHealth += _value;
        healthBar.SetHealth(currentHealth);
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        //Dokunulmazlýk Süresi
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

}

    
