public class OmniSlash : ISpecial
{
   public string Name { get; init; }
   public (int min, int max) Damage { get; init; }
   public float Accuracy { get; init; }
   public DamageType Type { get; init; }
   public (int min, int max) Pierce { get; init; }
   public int RechargeTime { get; init; }
   public int SPCost { get; init; }

   public OmniSlash() {
      Name = "OMNI-SLASH";
      Damage = (5, 6);
      Accuracy = 0.75f;
      Type = DamageType.Fire;
      Pierce = (1, 2);
      RechargeTime = 2;
      SPCost = 2;
   }
}