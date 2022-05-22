public class Kick : IAttack
{
   public string Name { get; init; } = "Kick";
   public (int min, int max) Damage { get; init; }

   public Kick() {
      Damage = (1, 2);
   }
}