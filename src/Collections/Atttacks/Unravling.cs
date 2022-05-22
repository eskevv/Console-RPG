public class Unravling : IAttack
{
   public string Name { get; init; } = "UNRAVLING";
   public (int min, int max) Damage { get; init; }

   public Unravling() {
      Damage = (0, 2);
   }
}