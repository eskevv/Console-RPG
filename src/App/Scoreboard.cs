public class Scoreboard
{
   private List<ScoreData> _heroes_scores;
   private List<ScoreData> _monsters_scores;

   private int _width;
   private int _round;
   private int _total_rounds;
   private ConsoleColor _decorator_color;
   private ConsoleColor _symbol_color;

   public Scoreboard((List<ScoreData> heroes, List<ScoreData> monsters) scores, int width, int round, int totalRounds) {
      _heroes_scores = scores.heroes;
      _monsters_scores = scores.monsters;
      _width = width;
      _round = round;
      _total_rounds = totalRounds;
      _decorator_color = ConsoleColor.DarkYellow;
      _symbol_color = ConsoleColor.DarkGray;
   }

   public void update_scores((List<ScoreData> heroes, List<ScoreData> monsters) scores, int round) {
      _heroes_scores = scores.heroes;
      _monsters_scores = scores.monsters;
      _round = round;
   }

   private string[] get_health_blanks((int c, int m) health) {
      var blanks = new string[] {"", ""};

      for (int x = int_digits(health.c); x < 3; x++) {
         blanks[0] += " ";
      }
      for (int x = int_digits(health.m); x < 3; x++) {
         blanks[1] += " ";
      }

      return blanks;
   }

   private void print_data(ScoreData c, string side, int index) {
      ConsoleColor side_color = side == "right" ? ConsoleColor.DarkRed : ConsoleColor.DarkBlue;
      string[] health_blanks = get_health_blanks(c.Health);

      if (side == "right") {
         int string_decs_length = 16;
         int start = _width - c.Name.Length - string_decs_length;
         print_separator(" ", start);
      }
      if (side == "left") {
         print($"{c.Name} ");
      }
      if (side == "right") {
         print($"{c.Name} ");
         print_colr($"[", _symbol_color);
         print_colr($"{index + 1}", ConsoleColor.DarkMagenta);
         print_colr($"] ", _symbol_color);
      }

      print_colr($"{health_blanks[0]}", ConsoleColor.DarkGray);
      print_colr($"{c.Health.current}", side_color);
      print($"/ ");
      print_colr($"{health_blanks[1]}", ConsoleColor.DarkGray);
      println_colr($"{c.Health.max} HP", side_color);
      
   }

   private int get_board_height() {
      int separators = 4;
      int total_party_height = _heroes_scores.Count + _monsters_scores.Count;
      
      return separators + total_party_height;
   }

   public void print_board() {
      print_separator(" ", _width - (int_digits(_round) + 11 + int_digits(_total_rounds)));
      println_colr($"BATTLE# {_round} / {_total_rounds}", _decorator_color);
      println_separator("#", _width, " BATTLE ", _symbol_color, _decorator_color);
      for (int c = 0; c < _heroes_scores.Count; c++) {
         print_data(_heroes_scores[c], "left", c);
      }

      println_separator("=", _width, " VS ", _symbol_color, _decorator_color);
      for (int c = 0; c < _monsters_scores.Count; c++) {
         print_data(_monsters_scores[c], "right", c);
      }

      println_separator("#", _width, _symbol_color);
   }
}