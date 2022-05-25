public class SpecialAction : AttackAction
{
   public ISpecial Special { get; set; }

   public SpecialAction(Character c, Character t, ISpecial special) : base(c, t) {
      Special = special;
      Value = special.Name;
    }

    public override void execute() {
       _atk_data = new SpecialAttackCalculator(Character, Special, _target).get_data();
      base.execute();
   }
}