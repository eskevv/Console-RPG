public class Punch : IAttack
{
   public string Name { get; init; } = "PUNCH";
   public (int min, int max) Damage { get; init; }

   public Punch() {
      Damage = (1, 1);
   }
}