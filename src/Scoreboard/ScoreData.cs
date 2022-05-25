public struct ScoreData
{
   public string Name { get; init; }
   public (int current, int max) Health { get; init; }

   public ScoreData((int, int) health, string name) {
      Health = health;
      Name = name;
   }
}