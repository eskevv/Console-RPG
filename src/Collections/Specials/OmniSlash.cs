public class OmniSlash : ISpecial
{
   public string Name { get; init; } = "OMNI-SLASH";
   public (int min, int max) Damage { get; init; } = (4, 5);
   public int SPCost { get; init; } = 2;
   public int RechargeTime { get; set; } = 2;
}