namespace Game;

public static class Application
{
   const int MONSTER_PARTIES = 2;
   static int[] monster_counts = new int[MONSTER_PARTIES] { 3, 5 };

   public static List<Hero> get_heroes() {
      var _heroparty = new List<Hero>();

      var main_specials = new List<ISpecial>();
      main_specials.Add(new OmniSlash());
      
      _heroparty.Add(new Hero("LEE", 20, new Kick(), main_specials));
      _heroparty.Add(new Hero("CLOUD", 15, new Punch(), main_specials));

      return _heroparty;
   }

   public static List<Monster> get_monster_party(int count) {
      var monster_party = new List<Monster>();

      for (int m = 0; m < count; m++) {
         monster_party.Add(new Monster("SKELETON", 5, new BoneCrunch(), new List<ISpecial>()));
      }

      return monster_party;
   }

   public static List<List<Monster>> get_monsters() {
      var monsterparties = new List<List<Monster>>();

      for (int m = 0; m < MONSTER_PARTIES; m++) {
         int monster_count = monster_counts[m];
         monsterparties.Add(get_monster_party(monster_count));
      }

      monsterparties[MONSTER_PARTIES - 1].Add(the_uncoded_one());

      return monsterparties;
   }

   public static Monster the_uncoded_one() {
      return new Monster("THE UNCODED ONE", 15, new Unravling(), new List<ISpecial>());
   }

   public static void run() {
      var game = new NewGame(get_heroes(), get_monsters());
      game.run();
   }
}
