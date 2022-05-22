public class Monster : Character
{
   public Monster(string name, int health, IAttack attack, List<ISpecial> specials) : base(name, health, attack, specials) { }
}