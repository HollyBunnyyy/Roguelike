public class Health
{
    private float _amount;
    public float Amount => _amount;

    public Health( float amount )
    {
        _amount = amount;

    }

    public void Heal( float amountToHeal )
    {
        _amount += amountToHeal;
    }

    public void Damage( float amountToDamage )
    {
        _amount -= amountToDamage;
    }
}
