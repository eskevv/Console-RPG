public class Unravling : IAttack
{
   public string Name { get; init; }
   public (int min, int max) Damage { get; init; }
   public float Accuracy { get; init; }
   public DamageType Type { get; init; }

   public Unravling() {
      Name = "UNRAVLING";
      Damage = (2, 3);
      Accuracy = 0.8f;
      Type = DamageType.Decoding;
   }
}