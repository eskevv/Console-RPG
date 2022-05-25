public abstract class DamageCalculator
{
   protected int _damage;
   protected bool _dodged;
   protected bool _criticalHit;
   protected int _pierceDamage;
   protected int _damageBlocked;
   protected int _damageDone;

   protected readonly Character _source;
   protected readonly Character _target; 
   protected readonly IAttack _ability; 

   public DamageCalculator(Character source, Character target, IAttack ability) {
      _source = source;
      _target = target;
      _ability = ability;
   }

   public abstract AttackData get_data();
   protected abstract int get_raw_damage();
   protected abstract bool get_hit_landed();
   protected abstract int get_amped_damage();
   protected abstract bool get_is_critical();
}
