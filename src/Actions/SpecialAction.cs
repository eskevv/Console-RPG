public class SpecialAction : AttackAction
{
   private readonly ISpecial _special;
   
   public SpecialAction(Character c, Character t, IAttack special) : base(c, t, special) {
      _special = (ISpecial)special;
    }

   public override void execute() {
      var _atk_data = new SpecialAttackCalculator(Character, _target, _special).get_data();
      _target.Health = Math.Max(_target.Health - _atk_data.DamageDone, 0);
      display_result(_atk_data);
   }
}