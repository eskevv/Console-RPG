public class BladeBeam : ISpecial
{
   public string Name { get; init; }
   public (int min, int max) Damage { get; init; }
   public float Accuracy { get; init; }
   public DamageType Type { get; init; }
   public (int min, int max) Pierce { get; init; }
   public int RechargeTime { get; init; }
   public int SPCost { get; init; }

   public BladeBeam() {
      Name = "BLADE-BEAM";
      Damage = (5, 9);
      Accuracy = 0.8f;
      Type = DamageType.Fire;
      Pierce = (2, 3);
      RechargeTime = 2;
      SPCost = 2;
   }
}