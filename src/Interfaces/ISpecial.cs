public interface ISpecial
{
   string Name { get; init; }
   (int min, int max) Damage { get; init; }
   (int min, int max) Pierce { get; init; }
   float Accuracy { get; init; }
   int SPCost { get; init; }
   int RechargeTime { get; init; }
   DamageType Type { get; init; }
}