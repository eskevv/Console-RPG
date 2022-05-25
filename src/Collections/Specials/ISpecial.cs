public interface ISpecial : IAttack
{
   (int min, int max) Pierce { get; init; }
   int SPCost { get; init; }
   int RechargeTime { get; init; }
}