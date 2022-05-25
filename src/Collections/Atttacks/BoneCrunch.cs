public class BoneCrunch : IAttack
{
   public string Name { get; init; }
   public (int min, int max) Damage { get; init; }
   public float Accuracy { get; init; }
   public DamageType Type { get; init; }

   public BoneCrunch() {
      Name = "BONE CRUNCH";
      Damage = (1, 2);
      Accuracy = 0.9f;
      Type = DamageType.Physical;
   }
}