namespace Game;

public class Application
{
   static int[] monster_counts = new int[2] { 3, 5 };

   Dictionary<string, List<ISpecial>> hero_specials = new Dictionary<string, List<ISpecial>>();

   public Application() {
      hero_specials["LEE"] = new List<ISpecial>() {
         new OmniSlash()
      };

      hero_specials["CLOUD"] = new List<ISpecial>() {
         new BladeBeam()
      };
   }

   public List<Hero> create_heroes() {
      return new List<Hero>() {
         new Hero("LEE", 20, new Kick(), hero_specials["LEE"]),
         new Hero("CLOUD", 15, new Punch(), hero_specials["CLOUD"]),
      };
   }

   public List<Monster> create_monster_party(int count) {
      var monster_party = new List<Monster>();

      for (int m = 0; m < count; m++) {
         monster_party.Add(new Monster("SKELETON", 5, new BoneCrunch(), new List<ISpecial>()));
      }

      return monster_party;
   }

   public List<List<Monster>> create_all_monsters() {
      var monsterparties = new List<List<Monster>>();

      for (int m = 0; m < monster_counts.Length; m++) {
         int monster_count = monster_counts[m];
         monsterparties.Add(create_monster_party(monster_count));
      }

      monsterparties[monster_counts.Length - 1].Add(new Monster("THE UNCODED ONE", 15, new Unravling(), new List<ISpecial>()));

      return monsterparties;
   }

   public void run() {
      var game = new NewGame(create_heroes(), create_all_monsters());
      game.run();
   }
}
