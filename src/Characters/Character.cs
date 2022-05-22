public abstract class Character
{
   public string Name { get; set; }
   public int Health { get; set; }
   public int MaxHealth { get; set; }
   public IAttack Attack { get; set; }
   public List<ISpecial> Specials { get; set; }

   public Character(string name, int health, IAttack attack, List<ISpecial> specials) {
      Name = name;
      Health = health;
      MaxHealth = health;
      Attack = attack;
      Specials = specials;
   }
}
