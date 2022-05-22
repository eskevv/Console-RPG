public interface IAttack
{
   public string Name { get; init; }
   public (int min, int max) Damage { get; init; }
}