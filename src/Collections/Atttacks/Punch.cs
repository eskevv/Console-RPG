public class Punch : IAttack
{
   public string Name { get; init; }
   public (int min, int max) Damage { get; init; }
   public float Accuracy { get; init; }
   public DamageType Type { get; init; }

   public Punch() {
      Name = "PUNCH";
      Damage = (1, 1);
      Accuracy = 0.8f;
      Type = DamageType.Physical;
   }
}