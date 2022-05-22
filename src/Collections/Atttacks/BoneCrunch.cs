public class BoneCrunch : IAttack
{
   public string Name { get; init; } = "BONE CRUNCH";
   public (int min, int max) Damage { get; init; }

   public BoneCrunch() {
      Damage = (0, 1);
   }
}