public class Hero : Character
{
   public Hero(string name, int health, IAttack attack, List<ISpecial> specials) : base(name, health, attack, specials) { }
}
