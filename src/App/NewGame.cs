public class NewGame
{
   private List<Hero> _hero_party;
   private List<List<Monster>> _monster_parties;
   private Battle _battle;

   private int _battle_number = 0;

   public static int TotalRounds { get; set; }

   public NewGame(List<Hero> hero_party, List<List<Monster>> monsterparties) {
      _hero_party = hero_party;
      _monster_parties = monsterparties;
      TotalRounds = monsterparties.Count;
      _battle = new Battle(_hero_party, monsterparties[_battle_number], _battle_number);
   }

   // methods
   public void run() {
      while (true) {
         _battle.start();

         if (_battle.Winners == "Heroes") {
            if (_battle_number == _monster_parties.Count - 1) {
               println_colr("*** The heroes have won, and the Uncoded One has been defeated! ***", ConsoleColor.DarkGreen);
               break;
            } else {
               _battle = new Battle(_hero_party, _monster_parties[++_battle_number], _battle_number);
               println_colr($"Proceeding to ROUND# {_battle_number + 1}...", ConsoleColor.DarkYellow);
               Thread.Sleep(3800);
               Console.Clear();
            }
         } else if (_battle.Winners == "Monsters") {
            println_colr("*** The heroes lost, and the Uncoded One's forces have prevailed... ***", ConsoleColor.DarkRed);
            break;
         }
      }
   }
}
