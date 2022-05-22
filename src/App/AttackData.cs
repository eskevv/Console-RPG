public class AttackData
{
   public int DamageDone { get; init; }
   public int DamageBlocked {get; init; }
   public bool Dodged { get; init; }

   public AttackData((int min, int max) damage, Character t) {
      var r = new Random();
      int min_damage = damage.min;
      int max_damage = damage.max;

      DamageDone = r.Next(min_damage, max_damage + 1); 
      
   }
}