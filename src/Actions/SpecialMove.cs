public class SpecialMove : AttackAction, IAction
{
   public ISpecial Special { get; set; }

   public SpecialMove(Character c, Character t, ISpecial special) : base(c, t) {
      Special = special;
      Value = special.Name;
    }

   public override void execute() {
      var atk_data = new AttackData(Special.Damage, Target);
      Target.Health = Math.Max(Target.Health - atk_data.DamageDone, 0);
      display_action();
      Thread.Sleep(1500);
      display_result(atk_data);
   }
}