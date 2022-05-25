public class AttackAction : IAction
{
   // interface Properties
   public Character Character { get ; init ; }
   public string Value { get ; init; }

   // class properties
   protected readonly Character _target;
   private readonly ConsoleColor _target_color;
   private readonly ConsoleColor _value_color;
   private readonly ConsoleColor _crit_color;
   protected AttackData _atk_data;

   public AttackAction(Character c, Character t) {
      Character= c;
      _target = t;
      _target_color = t is Hero ? ConsoleColor.DarkBlue : ConsoleColor.DarkRed;
      _crit_color = c is Hero ? ConsoleColor.Magenta : ConsoleColor.DarkRed;
      _value_color = ConsoleColor.DarkGray;
      _atk_data = new AttackCalculator(c, c.Attack, t).get_data();

      Value = $"{c.Attack.Name}";
   }

   protected void display_result(AttackData data) {
      string a = $"{Character.Name} used %0%{Value} %1%on {_target.Name}...";
      println_color(a, _value_color, NewGame.DefaultColor);
      Thread.Sleep(1100);

      if (_atk_data.Dodged) {
         string dodged = $"%0%{Value} missed the target...";
         println_color(dodged, ConsoleColor.Red);
      }

      if (_atk_data.CriticalHit) {
         string crit = $"%0%A CRITICAL HIT!";
         println_color(crit, _crit_color);
      }
      string b = $"%0%{Value} %1%dealt %0%{data.DamageDone}* %1%damage to %2%{_target.Name}";
      println_color(b, _value_color, NewGame.DefaultColor, ConsoleColor.Magenta);
      Thread.Sleep(900);

      string c = $"%0%{_target.Name} %1%is now at %0%{_target.Health}/{_target.MaxHealth} HP";
      println_color(c, _target_color, NewGame.DefaultColor);
      Thread.Sleep(1400);

   }

   public virtual void execute() {
      _target.Health = Math.Max(_target.Health - _atk_data.DamageDone, 0);
      display_result(_atk_data);
   }
}