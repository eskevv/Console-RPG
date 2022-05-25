public interface IAttack
{
   string Name { get; init; }
   (int min, int max) Damage { get; init; }
   float Accuracy { get; init; }
   DamageType Type { get; init; }
}