public class SpecialAction : AttackAction
{
   public ISpecial Special { get; set; }

   public SpecialAction(Character c, Character t, ISpecial special) : base(c, t) {
      Special = special;
      Value = special.Name;
      _atk_data = new SpecialAttackCalculator(c, special, t).get_data();
    }
}