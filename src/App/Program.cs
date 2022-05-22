global using static RapidUtils.ConsoleUtils;

Console.CursorVisible = false;
var random = new Random();

const int MONSTER_PARTIES = 2;
int[] monster_counts = new int[MONSTER_PARTIES] { 3, 5 };

List<Hero> get_heroes() {
   var _heroparty = new List<Hero>();

   var main_specials = new List<ISpecial>();
   main_specials.Add(new OmniSlash());
   
   _heroparty.Add(new Hero("LEE", 20, new Kick(), main_specials));

   return _heroparty;
}

List<Monster> get_monster_party(int count) {
   var monster_party = new List<Monster>();

   for (int m = 0; m < count; m++) {
      monster_party.Add(new Monster("SKELETON", 5, new BoneCrunch(), new List<ISpecial>()));
   }

   return monster_party;
}

List<List<Monster>> get_monsters() {
   var monsterparties = new List<List<Monster>>();

   for (int m = 0; m < MONSTER_PARTIES; m++) {
      int monster_count = monster_counts[m];
      monsterparties.Add(get_monster_party(monster_count));
   }

   monsterparties[MONSTER_PARTIES - 1].Add(the_uncoded_one());

   return monsterparties;
}

Monster the_uncoded_one() {
   return new Monster("THE UNCODED ONE", 15, new Unravling(), new List<ISpecial>());
}

void logging_tester() {
   ConsoleLogger logger = new ConsoleLogger(50, 0);


   
}

// var game = new NewGame(get_heroes(), get_monsters());
Console.Clear();
logging_tester();
// game.run();
println();

public class ConsoleLogger
{
   private int _x_pos;
   private int _y_pos;
   private List<string?> _stored_messages;

   public ConsoleLogger(int startX, int startY) {
      _x_pos = startX;
      _y_pos = startY;
      _stored_messages = new List<string?>();
   }

   private void store_log(string? s) {
      if (s != null) 
         _stored_messages.Add(s);
   }

   public void log(string? s) {
      store_log(s);

      if (_stored_messages.Count > Console.BufferHeight) {
         Console.BufferHeight += 1;
      }

      int iterations = Math.Min(_stored_messages.Count, 20);
      for (int a = 0; a < iterations; a++) {
         Console.SetCursorPosition(_x_pos, _y_pos + a);
         print(_stored_messages[_stored_messages.Count - (1 + a)]);
         Console.SetWindowPosition(0, 0);
      }
   }
}

