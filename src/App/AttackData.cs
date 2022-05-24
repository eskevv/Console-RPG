public class AttackData
{
   public int DamageDone { get; init; }
   public bool Dodged { get; init; }
   public bool CriticalHit { get; init; }
   public int PierceDamage { get; init; }
   public int DamageBlocked {get; init; }

   public AttackData(IDamageCalculator calculator) {
      DamageDone = calculator.DamageDone;
      DamageBlocked = calculator.DamageBlocked;
      Dodged = calculator.Dodged;
      CriticalHit = calculator.CriticalHit;
      PierceDamage = calculator.PierceDamage;
   }
}