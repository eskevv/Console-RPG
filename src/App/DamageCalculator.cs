public interface IDamageCalculator
{
   public int Damage { get; }
   public bool Dodged { get; }
   public bool CriticalHit { get; }
   public int PierceDamage { get; }
   public int DamageBlocked {get; }
   public int DamageDone { get; }

   public Character Source { get; }
   public Character Target { get; }

   public AttackData get_data();
}

public class SpecialAttackCalculator : IDamageCalculator
{
   public int Damage { get; private set; }
   public bool Dodged { get; private set; }
   public bool CriticalHit { get; private set; }
   public int PierceDamage { get; private set; }
   public int DamageBlocked {get; private set; }
   public int DamageDone { get; private set; }

   public Character Source { get; init; }
   public Character Target { get; init; }

   private readonly ISpecial _special;

   public SpecialAttackCalculator(Character source, ISpecial special, Character target) {
      Source = source;
      Target = target;
      _special = special;
   }

   private bool calculate_hit_landed() {
      float accuracy = _special.Accuracy * 100;
      float evasion = Target.Evasion * 100;

      if (evasion > accuracy) {
         for (int x  = 0; x < 2; x++) {
            if (new Random().Next(0, 100) >= accuracy - 1) return false;
         }
         return true;
      }
      float chance = accuracy - evasion / (accuracy / evasion);
      println_colr($"LOG | CHANCE TO HIT: {chance}", ConsoleColor.DarkGreen);
      return new Random().Next(0, 100) <= chance - 1;
   }

   private int get_total_damage() {
      int buffed_damage =  CriticalHit ? (int)(Damage * Source.CritMultiplier) : Damage;
      int pierce_roll = new Random().Next(_special.Pierce.min, _special.Pierce.max + 1);

      PierceDamage = CriticalHit ? (int)(pierce_roll * Source.CritMultiplier) : pierce_roll;
      int target_defence = Target.Armor > 0 ? Target.Armor - (int)((float)(PierceDamage / Target.Armor) * PierceDamage) : 0;

      DamageBlocked = Math.Min(buffed_damage, target_defence);
      println_colr($"LOG | BuffedDamage: {buffed_damage}, Defence: {target_defence}, Pierce: {PierceDamage}, DamageBlocked: {DamageBlocked}\n", ConsoleColor.DarkGreen);

      return Math.Max(buffed_damage - target_defence, 0);
   }

   public AttackData get_data() {
      Damage = new Random().Next(_special.Damage.min, _special.Damage.max + 1);
      if (Dodged = !calculate_hit_landed()) return new AttackData(this);
      CriticalHit = new Random().Next(0, 100) <= Source.CritChance * 100 - 1;

      DamageDone = get_total_damage();

      return new AttackData(this);
   } 
}

public class AttackCalculator : IDamageCalculator
{
   public int Damage { get; private set; }
   public bool Dodged { get; private set; }
   public bool CriticalHit { get; private set; }
   public int PierceDamage { get; private set; }
   public int DamageBlocked {get; private set; }
   public int DamageDone { get; private set; }

   public Character Source { get; init; }
   public Character Target { get; init; }

   private readonly IAttack _attack;

   public AttackCalculator(Character source, IAttack attack, Character target) {
      Source = source;
      Target = target;
      _attack = attack;
   }

   private bool calculate_hit_landed() {
      float accuracy = _attack.Accuracy * 100;
      float evasion = Target.Evasion * 100;

      if (evasion > accuracy) {
         for (int x  = 0; x < 2; x++) {
            if (new Random().Next(0, 100) >= accuracy - 1) return false;
         }
         return true;
      }

      float chance = accuracy - evasion / (accuracy / evasion);
      return new Random().Next(0, 100) <= chance - 1;
   }

   public AttackData get_data() {
      Damage = new Random().Next(_attack.Damage.min, _attack.Damage.max + 1);
      if (Dodged = !calculate_hit_landed()) return new AttackData(this);

      CriticalHit = new Random().Next(0, 100) <= Source.CritChance * 100 - 1;
      int buffed_damage =  CriticalHit ? (int)(Damage * Source.CritMultiplier) : Damage;
      
      DamageBlocked = Math.Min(buffed_damage, Target.Armor);
      
      DamageDone = Math.Max(buffed_damage - Target.Armor, 0);

      return new AttackData(this);
   } 
}

