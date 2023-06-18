
public interface IHealable : IHealth
{
   public void Heal(int healAmount)
   {
        Health += healAmount;
        if (Health > MaxHealth) Health = MaxHealth;
   }    
}
