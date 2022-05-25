public class AIController : IController
{
   private readonly Random r = new Random();

   private Character get_target(List<Character> enemies) {
      var possible_targets = enemies.Where(x => x.Health > 0).ToList();

      return enemies[r.Next(possible_targets.Count)];
   }

   public IAction get_command(Character c, List<Character> friendly, List<Character> enemies) {
      Thread.Sleep(1100);
      return r.Next(0, 8) switch {
         < 6 => new AttackAction(c, get_target(enemies), c.Attack),
         _ => new DoNothingAction(c),
      };
   }
}