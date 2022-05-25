public struct AttackData
{
   public int DamageDone { get; init; }
   public bool Dodged { get; init; }
   public bool CriticalHit { get; init; }
   public int PierceDamage { get; init; }
   public int DamageBlocked {get; init; }

   public AttackData(int damage, bool dodged, bool critical, int pierce, int blocked) {
      DamageDone = damage;
      Dodged = dodged;
      CriticalHit = critical;
      PierceDamage = pierce;
      DamageBlocked = blocked;
   }

   public AttackData(bool dodged) {
      DamageDone = 0;
      Dodged = dodged;
      CriticalHit = false;
      PierceDamage = 0;
      DamageBlocked = 0;

   }
}