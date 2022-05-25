public class PlayerController : IController
{
   // methods 
   private Character? targets_menu(List<Character> party) {
      var targets = new string[party.Count];
      for (int x = 0; x < targets.Length; x++) {
         targets[x] = party[x].Name;
      }

      var targets_menu = new Menu(targets, ConsoleColor.DarkRed, "*** { SELECT A TARGET } ***");
      int item_num = targets_menu.collect_input();
      if (item_num == -1) return null;
      return party[item_num];

   }

   private ISpecial? specials_menu(List<ISpecial> specials) {
      var special_moves = new string[specials.Count];
      for (int x = 0; x < special_moves.Length; x++) {
         special_moves[x] = specials[x].Name;
      }
      
      var specials_menu = new Menu(special_moves, ConsoleColor.DarkMagenta, "*** { SPECIALS } ***");
      int item_num = specials_menu.collect_input();
      if (item_num == -1) return null;
      return specials[item_num];
   }

   private AttackAction? get_attacking_action(Character c, List<Character> enemies) {
      Character? target = targets_menu(enemies);
      if (target == null) return null;
      
      return new AttackAction(c, target, c.Attack);
   }

   private SpecialAction? get_special_action(Character c, List<Character> enemies) {
      while (true) {
         ISpecial? special = specials_menu(c.Specials);
         if (special == null) return null;

         Character? new_target = targets_menu(enemies);
         if (new_target == null) continue;

         return new SpecialAction(c, new_target, special);
      }
   }

   public IAction get_command(Character c, List<Character> friendly, List<Character> enemies) {
      var actions_menu = new Menu(new string[] {"StandardAttack", "Special", "DoNothing"}, ConsoleColor.Blue, "*** { ACTIONS } ***");
      while (true) {
         int menu_item = actions_menu.collect_input();
         switch (menu_item) {
         case 0:
            AttackAction? attack = get_attacking_action(c, enemies);
            if (attack != null) return attack;
            break;
         case 1:
            SpecialAction? special = get_special_action(c, enemies);
            if (special != null) return special;
            break;
         case 2:
            return new DoNothingAction(c);
         }
      }
   }
}