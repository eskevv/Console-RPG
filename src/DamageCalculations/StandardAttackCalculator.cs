public class AttackCalculator : DamageCalculator
{
   private readonly IAttack _attack;

   public AttackCalculator(Character source, Character target, IAttack attack) : base(source, target, attack) {
      _attack = attack;
   }

   public override AttackData get_data() {
      _damage = get_raw_damage();
      if (_dodged = !get_hit_landed()) return new AttackData(_dodged);
      _criticalHit = get_is_critical();

      int amped = get_amped_damage();
      _damageBlocked = Math.Min(amped, _target.Armor);
      _damageDone = Math.Max(amped - _damageBlocked, 0);

      return new AttackData(_damageDone, _dodged, _criticalHit, _pierceDamage, _damageBlocked);
   }

   protected override int get_raw_damage() {
      return new Random().Next(_attack.Damage.min, _attack.Damage.max + 1);
   }

   protected override bool get_hit_landed() {
      float accuracy = _attack.Accuracy * 100;
      float evasion = _target.Evasion * 100;

      if (evasion > accuracy) {
         for (int x  = 0; x < 2; x++) {
            if (new Random().Next(0, 100) >= accuracy - 1) return false;
         }
         return true;
      }
      float chance = accuracy - evasion / (accuracy / evasion);
      return new Random().Next(0, 100) <= chance - 1;
   }

   protected override int get_amped_damage() {
      int core_damage = _criticalHit ? (int)(_damage * _source.CritMultiplier) : _damage;
      return core_damage;
   }

   protected override bool get_is_critical() {
      return new Random().Next(0, 100) <= _source.CritChance * 100 - 1;
   }
}
