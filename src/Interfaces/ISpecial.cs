public interface ISpecial
{
   public string Name { get; init; }
   public (int min, int max) Damage { get; init; }
   public int SPCost { get; init; }
   public int RechargeTime { get; set; }
}