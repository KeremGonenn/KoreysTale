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
                //Tuzaðý Çalýþtýr
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

        //Gecikmeyi bekle ve animasyonu baþlat
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;//Doðru renge geri döndür
        active = true;
        anim.SetBool("activated", true);

        //X saniye kadar bekle ve tuzaðý deaktif hale getir
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
