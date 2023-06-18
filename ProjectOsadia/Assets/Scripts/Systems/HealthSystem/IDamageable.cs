public interface IDamageable : IHealth
{   public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;
        if (Health < 0) Health = 0;
    }
}
