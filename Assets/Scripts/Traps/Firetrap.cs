using System.Collections;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] protected int damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; //Tuzak tetiklenmesi
    private bool active; //Tuzak aktif ve hasar atma hali

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                //Tuza�� �al��t�r
                StartCoroutine(ActiveFiretrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakenDamage(damage);
            }
        }
    }
    private IEnumerator ActiveFiretrap()
    {
        //Gecikmeyi bekler ve rengi ayarlar
        triggered = true;
        spriteRend.color = Color.red;

        //Gecikmeyi bekle ve animasyonu ba�lat
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;//Do�ru renge geri d�nd�r
        active = true;
        anim.SetBool("activated", true);

        //X saniye kadar bekle ve tuza�� deaktif hale getir
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
