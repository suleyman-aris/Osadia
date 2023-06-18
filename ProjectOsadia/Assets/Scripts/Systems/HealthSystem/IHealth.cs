public interface IHealth
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }

    public int GetHealth()
    { return Health; }
    public float GetHealthPercentage()
    {
        return (float)Health / MaxHealth;
    }
}
