public class AttackAction : IAction
{
   // interface Properties
   public Character Character { get ; init ; }
   public string Value { get ; init; }

   // class properties
   public Character Target { get; init; }
   public IAttack Attack { get; init; }
   private readonly ConsoleColor _target_color;
   private readonly ConsoleColor _value_color;


   public AttackAction(Character c, Character t) {
      Character= c;
      Target = t;
      _target_color = t is Hero ? ConsoleColor.DarkBlue : ConsoleColor.DarkRed;
      _value_color = ConsoleColor.DarkGray;
      Attack = c.Attack;
      Value = $"{c.Attack.Name}";
   }

   protected void display_action() {
      print($"{Character.Name} used ");
      print_colr($"{Value} ", _value_color);
      println($"on {Target.Name}...");
   }

   protected void display_result(AttackData data) {
      print_colr($"{Value} ", _value_color);
      print($"dealt ");
      print_colr($"{data.DamageDone}* ", ConsoleColor.Magenta);
      println($"damage to {Target.Name}");
      Thread.Sleep(700);
      print_colr($"{Target.Name} ", _target_color);
      print($"is now at ");
      println_colr($"{Target.Health}/{Target.MaxHealth} HP", _target_color);
   }

   public virtual void execute() {
      var atk_data = new AttackData(Attack.Damage, Target);
      Target.Health = Math.Max(Target.Health - atk_data.DamageDone, 0);
      display_action();
      Thread.Sleep(1500);
      display_result(atk_data);
   }
}