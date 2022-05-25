public class Kick : IAttack
{
   public string Name { get; init; }
   public (int min, int max) Damage { get; init; }
   public float Accuracy { get; init; }
   public DamageType Type { get; init; }

   public Kick() {
      Name = "KICK";
      Damage = (1, 3);
      Accuracy = 0.95f;
      Type = DamageType.Physical;
   }
}