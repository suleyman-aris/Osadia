using UnityEngine;

public class ChangeHealth : MonoBehaviour,IHealth,IHealable,IDamageable
{
    // bu kod ileride deðiþecek ve/ya büyük ihtimalle ortadan kalkacak, sadece demonstrasyon için yazýldý.
    // Edit: bu Class bir manager Class haline dönüþebilir.

    //Canýmýzý görmek için ayýrdýðým deðiþken
    //[SerializeField]public static float health;

    //Hasar ve Can yenileme miktarlarý, bunlar büyük ihtimalle ayrý-
    //bir interface'e alýnacak ve ordaki bir metodun argümanlarý þeklinde kullanýlacak.

    [SerializeField]private int damageAmount = 20;
    [SerializeField]private int healAmount = 10;
    [SerializeField]private int maxHealth = 100;
    public int Health { get; set; }
    public int MaxHealth { get; set; }

    public delegate void HealthChanger();
    public static event HealthChanger OnDeath;

    private void Awake()
    {
        Health = maxHealth;
        MaxHealth = maxHealth;
        //Event Atamalarý
        RunManager.OnHealed += Heal;
        RunManager.OnDamaged += TakeDamage;
        RunManager.OnShowHealth += ShowHealth;
    }
    private void ShowHealth()
    {
        Debug.Log("Can : " + Health);
        Debug.Log("Can Yüzdesi : " + (float)Health / MaxHealth);
    }
    public float GetHealthAndPercentage(bool isPercentage)
    {
        if(isPercentage)
        {
            return (float)Health/MaxHealth;
        }
        else
        {
            return (float)Health; 
        }
    }
    private void TakeDamage()
    {    
        Health -= damageAmount;
        Debug.Log("Hasar Alýndý : " + damageAmount);
        if(Health == 0)
            OnDeath();
    }

    private void Heal()
    {
        if (!RunEnder.isDead && Health < 150f)
        {   Health += healAmount;
            Debug.Log("Ýyileþildi : " + healAmount);
        }
    }
}
