public class Battle
{
   private List<Hero> _hero_party;
   private List<Monster> _monster_party;

   public string Winners { get; private set; }
   private readonly Scoreboard _scoreboard;
   private readonly int _battle_number;

   private readonly IController _hero_controller;
   private readonly IController _monster_controller;

   private readonly ConsoleColor _friendly_color;
   private readonly ConsoleColor _enemy_color;

   public Battle(List<Hero> heroes, List<Monster> monsterparty, int battleNumber) {
      _hero_party = heroes;
      _monster_party = monsterparty;
      Winners = "None";
      _battle_number = battleNumber;
      _scoreboard = new Scoreboard(get_score_data(), 80, battleNumber + 1, NewGame.TotalRounds);

      _hero_controller = new PlayerController();
      _monster_controller = new AIController();

      _friendly_color = ConsoleColor.DarkBlue;
      _enemy_color = ConsoleColor.DarkRed;
   }

   // methods
   public void start() {
      while (true) {
         _scoreboard.update_scores(get_score_data(), _battle_number + 1);
         _scoreboard.print_board();
         loop_party(_hero_party);
         if (Winners != "None") {
            break;
         }
         println_separator(".", _scoreboard.Width, "ENEMY TURN", ConsoleColor.DarkRed);
         println();

         loop_party(_monster_party);
         if (Winners != "None") {
            break;
         }
      }
   }

   private (List<ScoreData>, List<ScoreData>) get_score_data() {
      List<ScoreData> heroes = new List<ScoreData>();
      List<ScoreData> monsters = new List<ScoreData>();

      foreach (var item in _hero_party) {
         heroes.Add(new ScoreData((item.Health, item.MaxHealth), item.Name));
      }
      
      foreach (var item in _monster_party) {
         monsters.Add(new ScoreData((item.Health, item.MaxHealth), item.Name));
      }

      return (heroes, monsters);
   }

   // Game Flow
   private void loop_party<T>(List<T> party) where T : Character {
      foreach (var member in party) {
         if (member.Health == 0) continue;
         
         display_turn(member);
         get_action(member).execute();
         
         List<Character> dead_members = all_deaths();
         announce_deaths(dead_members);
         remove_dead_monsters(dead_members);

         println();
         calculate_winners();
         if (Winners != "None") {
            break;
         }
      }
   }

   private void display_turn(Character c) {
      ConsoleColor color = c is Hero ? _friendly_color : _enemy_color;
      string symbol_a = c is Hero ? ">>> " : "";
      string symbol_b = c is Hero ? "<<< " : "";
      print_colr($"{symbol_a}", ConsoleColor.DarkGreen);
      print($"It is ");
      print_colr($"{c.Name}'s ", color );
      print($"turn ");
      println_colr($"{symbol_b}", ConsoleColor.DarkGreen);
   }  

   private IAction get_action(Character c) {
      if (c is Hero ) {
         return  _hero_controller.get_command(c, _hero_party.Select(x => (Character)x).ToList(), _monster_party.Select(x => (Character)x).ToList());
      }
      if (c is Monster ) {
         return  _monster_controller.get_command(c, _monster_party.Select(x => (Character)x).ToList(), _hero_party.Select(x => (Character)x).ToList());
      }

      return new DoNothingAction(c);
   }

   private void calculate_winners() {
      if (_hero_party.Count(x => x.Health > 0) == 0) {
         Winners = "Monsters";
      }
      if (_monster_party.Count == 0) {
         Winners = "Heroes";
      }
   }

   // Party Removements
   private List<Character> check_deaths<T>(List<T> party) where T : Character {
      var dead_members = party.Where(x => x.Health <= 0).Select(x => (Character)x).ToList();
      return dead_members;
   }
   
   private List<Character> all_deaths() {
      List<Character> dead_members = new List<Character>();

      dead_members.AddRange(check_deaths(_monster_party));
      dead_members.AddRange(check_deaths(_hero_party));

      return dead_members;
   }

   private void announce_deaths(List<Character> characters) {
      foreach (var c in characters) {
         println_colr($"{c.Name} has been defeated!", ConsoleColor.DarkYellow);
      }

      if (characters.Count(x => x is Monster) > 0) {
         println_colr($"---{_monster_party.Count - 1} monster(s) remain---", ConsoleColor.DarkYellow);
      }
   }

   private void remove_dead_monsters(List<Character> dead_members) {
      foreach (var item in dead_members) {
         if (item is Monster)  {
            _monster_party.Remove((Monster)item);
         }
      }
   }
}