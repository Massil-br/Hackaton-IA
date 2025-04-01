namespace src
{
    public class Upgrades
    {
        public int ClickDamageBonus { get; private set; } = 0;
        public float AllyDPS { get; private set; } = 0f;

        public void BuyClickDamage()
        {
            ClickDamageBonus += 1;
        }

        public void BuyAllyDPS(float amount)
        {
            AllyDPS += amount;
        }

        public float GetClickDamage(float baseDamage)
        {
            return baseDamage + ClickDamageBonus;
        }

        public float GetAllyDPS()
        {
            return AllyDPS;
        }
    }
}
