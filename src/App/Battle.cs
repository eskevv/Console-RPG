public class Battle
{
   public string Winners { get; private set; }

   private List<Hero> _hero_party;
   private List<Monster> _monster_party;

   private readonly Scoreboard _scoreboard;
   private readonly int _battle_number;

   private readonly IController _hero_controller;
   private readonly IController _monster_controller;

   private readonly ConsoleColor _friendly_color;
   private readonly ConsoleColor _enemy_color;

   public Battle(List<Hero> heroes, List<Monster> monsterparty, int battleNumber) {
      Winners = "None";

      _hero_party = heroes;
      _monster_party = monsterparty;
      
      _scoreboard = new Scoreboard(get_score_data(), 80, battleNumber + 1, NewGame.TotalRounds);
      _battle_number = battleNumber;

      _hero_controller = new PlayerController();
      _monster_controller = new AIController();

      _friendly_color = ConsoleColor.DarkBlue;
      _enemy_color = ConsoleColor.DarkRed;
   }

   // methods
   public void start() {
      while (true) {
         print_heading(_hero_controller);
         loop_party(_hero_party);
         if (Winners != "None") {
            break;
         }
         
         print_heading(_monster_controller);
         loop_party(_monster_party);
         if (Winners != "None") {
            break;
         }
      }
   }

   private void print_heading<T>(T controller) where T : IController {
      if (controller is PlayerController) {
         _scoreboard.update_scores(get_score_data(), _battle_number + 1);
         _scoreboard.print_board();
         return;
      } 

      if (controller is AIController) {
         println_separator(".", _scoreboard.Width, "MONSTERS TURN", ConsoleColor.DarkGray, ConsoleColor.DarkRed);
         println();
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
      string a = $"%1%{symbol_a}%0%It is %2%{c.Name}'s %0%turn %1%{symbol_b}";
      println_color(a, NewGame.DefaultColor, ConsoleColor.DarkGreen, color); 
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
         println_colr($"{c.Name} has been defeated!", ConsoleColor.DarkGreen);
      }

      if (characters.Count(x => x is Monster) > 0) {
         println_colr($"---{_monster_party.Count - 1} monster(s) remain---", ConsoleColor.DarkGreen);
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