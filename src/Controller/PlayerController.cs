public class PlayerController : IController
{
   public PlayerController() { }

   // methods 
   private Character? get_target(List<Character> party) {
      var targets = new string[party.Count];
      for (int x = 0; x < targets.Length; x++) {
         targets[x] = party[x].Name;
      }
      var targets_menu = new Menu(targets, ConsoleColor.DarkRed, "*** { SELECT A TARGET } ***");

      while (true) {
         int item_num = targets_menu.collect_input();
         if (item_num != -1) {
            return party[item_num];
         }
         return null;
      }
   }

   private ISpecial? get_special_moves(List<ISpecial> specials) {
      var special_moves = new string[specials.Count];
      for (int x = 0; x < special_moves.Length; x++) {
         special_moves[x] = specials[x].Name;
      }
      var specials_menu = new Menu(special_moves, ConsoleColor.DarkMagenta, "*** { SPECIALS } ***");

      while (true) {
         int item_num = specials_menu.collect_input();
         if (item_num != -1) {
            return specials[item_num];
         }
         return null;
      }
   }

   public IAction get_command(Character c, List<Character> friendly, List<Character> enemies) {
      var actions_menu = new Menu(new string[] {"StandardAttack", "Special", "DoNothing"}, ConsoleColor.Blue, "*** { ACTIONS } ***");
      while (true) {
         int item_num = actions_menu.collect_input();
         switch (item_num) {
            case 0:
               Character? attack_target = get_target(enemies);
               if (attack_target != null) {
                  return new AttackAction(c, attack_target);
               }
               break;

            case 1:
               while (true) {
                  ISpecial? special = get_special_moves(c.Specials);
                  if (special != null) {
                     Character? new_target = get_target(enemies);
                     if (new_target != null) {
                        return new SpecialMove(c, new_target, special);
                     }
                  } else {
                     break;
                  }

               }
               break;

            case 2:
               return new DoNothing(c);
         }
      }
   }
}