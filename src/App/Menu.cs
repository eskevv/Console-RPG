public class Menu
{
   private int _cursor_location;
   private bool[] _cursor_holders;
   private string[] _menu_items;
   private string[] _help_items;
   private string[] _default_help;
   private string? _title;
   private bool _help_enabled;
   private ConsoleColor _cursor_color;

   public Menu(string[] menuItems, ConsoleColor cursorColor, string? title, string[]? helpItems = null) {
      _cursor_location = 0;
      _help_enabled = false;
      _menu_items = menuItems;
      _cursor_holders = new bool[_menu_items.Length];
      _cursor_color = cursorColor;
      _title = title;
      _default_help = new string[] {
         "[ UP    ] = SCROLL UP",
         "[ DOWN  ] = SCROLL DOWN",
         "[ ENTER ] = SELECT ITEM",
         "[ SPACE ] = TOGGLE HELP"
      }; 
                                          
      _help_items = helpItems == null ? _default_help : helpItems;
   }

   private void clear_menu(bool exiting = false, bool help = false) {
      int help_lines = _help_enabled ? _help_items.Length + 1 : 0;
      int title_lines = _title != null ? 1 : 0;
      int menu_lines = _menu_items.Length;

      if (help) {
         clear_input(start: 0, end: -1, lines: help_lines);
         clear_input(start: 0, end: 2, lines: menu_lines);
         return;
      }

      if (exiting) {
         clear_input(start: 0, end: -1, lines: menu_lines + help_lines + title_lines);
         return;
      }
      
      Console.SetCursorPosition(0, Console.CursorTop - (_menu_items.Length + help_lines));
   }

   private void display_menu() {
      for (int i = 0; i < _cursor_holders.Length; i++) {
         _cursor_holders[i] = false;
      }
      _cursor_holders[_cursor_location] = true;

      for (int i = 0; i < _cursor_holders.Length; i++) {
         string left_arrow = _cursor_holders[i] ? "-->" : "   ";
         string right_arrow = _cursor_holders[i] ? "<--" : "   ";

         print_colr($"{left_arrow} ", _cursor_color);
         print($"{_menu_items[i]}");
         println_colr($" {right_arrow} ", _cursor_color);
      }
   }

   private void display_help() {
      if (!_help_enabled)
         return;
      println_separator("-", 80, ConsoleColor.DarkGray);
      foreach (var item in _help_items) {
         println(item);
      }
   }

   private void set_arrow(int direction) {
      if (direction > 0) {
         _cursor_location = Math.Min(_cursor_location + direction, _menu_items.Length - 1);
      } else if (direction < 0) {
         _cursor_location = Math.Max(_cursor_location + direction, 0);
      }
   }

   public int collect_input() {
      println_colr(_title, _cursor_color);
      while (true) {
         display_menu();
         display_help();

         ConsoleKeyInfo key_pressed = get_key();
         if (key_pressed.Key == ConsoleKey.UpArrow) {
            set_arrow(-1);
         }
         if (key_pressed.Key == ConsoleKey.DownArrow) {
            set_arrow(1);
         }
         if (key_pressed.Key == ConsoleKey.Enter) {
            clear_menu(exiting: true);
            return _cursor_location;
         }
         if (key_pressed.Key == ConsoleKey.Spacebar) {
            clear_menu(help: _help_enabled);
            _help_enabled = !_help_enabled;
            continue;
         }
         if (key_pressed.Key == ConsoleKey.Escape) {
            clear_menu(exiting: true);
            return -1;
         }

         clear_menu();
      } 
   }
}