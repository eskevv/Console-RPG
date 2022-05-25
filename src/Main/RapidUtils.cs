namespace RapidUtils;
using static RapidUtils.ParseUtils;

public static class ConsoleUtils
{
   // console quick prints and input
   public static void print_color(string s, params ConsoleColor[] colors) {
      ConsoleColor current = Console.ForegroundColor;
      string[] items = s.substring_patterns("%", "%", x => char.IsDigit(x));
      int[] color_order = s.pattern_contents("%", "%", x => char.IsDigit(x));
      int[] indexes = s.pattern_indexes("%", "%", x => char.IsDigit(x));

      if (!indexes.Contains(0)) {
         print(s.Substring(0, indexes[0]));
      }

      items.in_each((i, x) => print_colr(i, colors[color_order[x]]));
      Console.ForegroundColor = current;
   }

   public static void println_color(string s, params ConsoleColor[] colors) {
      ConsoleColor current = Console.ForegroundColor;
      string[] items = s.substring_patterns("%", "%", x => char.IsDigit(x));
      int[] color_order = s.pattern_contents("%", "%", x => char.IsDigit(x));
      int[] indexes = s.pattern_indexes("%", "%", x => char.IsDigit(x));

      if (!indexes.Contains(0)) {
         print(s.Substring(0, indexes[0]));
      }

      items.in_each((i, x) => print_colr(i, colors[color_order[x]]));
      Console.ForegroundColor = current;
      Console.WriteLine();
   }

   public static void print(object? s) {
      System.Console.Write(s);
   }

   public static void print_colr(object? s, ConsoleColor color) {
      ConsoleColor current = Console.ForegroundColor;
      Console.ForegroundColor = color;
      System.Console.Write(s);
      Console.ForegroundColor = current;
   }

   public static void println(object? s) {
      System.Console.WriteLine(s);
   }

   public static void println_colr(object? s, ConsoleColor color) {
      ConsoleColor current = Console.ForegroundColor;
      Console.ForegroundColor = color;
      System.Console.WriteLine(s);
      Console.ForegroundColor = current;
   }
   
   public static void println() {
      System.Console.WriteLine();
   }

   public static string? readln(string? prompt = null) {
      System.Console.Write(prompt);
      return Console.ReadLine();
   }
  
   public static string? readln_colr(string? prompt = null, ConsoleColor color = ConsoleColor.Black) {
      ConsoleColor current = Console.ForegroundColor;
      Console.ForegroundColor = color;
      System.Console.Write(prompt);
      Console.ForegroundColor = current;
      return Console.ReadLine();
   }

   public static ConsoleKeyInfo read_key() {
      return Console.ReadKey(true);
   }

   // print dividers
   public static void print_separator(string s, int spaces) {
      for (int x = 0; x < spaces; x++) {
         Console.Write(s);
      }
   }

   public static void print_separator(string s, int spaces, ConsoleColor color) {
      for (int x = 0; x < spaces; x++) {
         print_colr(s, color);
      }
   }

   public static void print_separator(string s, int spaces, string decorator) {
      int decorator_position = (spaces / 2) - (decorator.Length / 2);
      spaces -= decorator.Length % 2 == 0 ? decorator.Length - 1 : decorator.Length - 2;

      for (int x = 0; x < spaces; x++) {
         if (x == decorator_position) {
            Console.Write(decorator);
            continue;
         }
         Console.Write(s);
      }
   }

   public static void print_separator(string s, int spaces, string decorator, ConsoleColor color_dec) {
      int decorator_position = (spaces / 2) - (decorator.Length / 2);
      spaces -= decorator.Length % 2 == 0 ? decorator.Length - 1 : decorator.Length - 2;

      for (int x = 0; x < spaces; x++) {
         if (x == decorator_position) {
            print_colr(decorator, color_dec);
            continue;
         }
         Console.Write(s);
      }
   }

   public static void print_separator(string s, int spaces, string decorator, ConsoleColor color_s, ConsoleColor color_dec) {
      int decorator_position = (spaces / 2) - (decorator.Length / 2);
      spaces -= decorator.Length % 2 == 0 ? decorator.Length - 1 : decorator.Length - 2;

      for (int x = 0; x < spaces; x++) {
         if (x == decorator_position) {
            print_colr(decorator, color_dec);
            continue;
         }
         print_colr(s, color_s);
      }
   }

   public static void println_separator(string s, int spaces, string decorator, ConsoleColor color_dec) {
      print_separator(s, spaces, decorator, color_dec);
      Console.WriteLine();
   }

   public static void println_separator(string s, int spaces, string decorator, ConsoleColor color_s, ConsoleColor color_dec) {
      print_separator(s, spaces, decorator, color_s, color_dec);
      Console.WriteLine();
   }

   public static void println_separator(string s, int spaces, string decorator) {
      print_separator(s, spaces, decorator);
      Console.WriteLine();
   }

   public static void println_separator(string s, int spaces) {
      print_separator(s, spaces);
      Console.WriteLine();
   }

   public static void println_separator(string s, int spaces, ConsoleColor color) {
      print_separator(s, spaces, color);
      Console.WriteLine();
   }

   // clear console
   public static void clear_input(int lines = 1) {
      int initial_top = Console.CursorTop - lines;
      Console.SetCursorPosition(0, Console.CursorTop - lines);

      for (int l = 0; l < lines; l++) {
         for (int i = 0; i < Console.WindowWidth; i++)  {
            Console.Write(" ");
         } 
         if (l < lines - 1) {
            Console.SetCursorPosition(0, Console.CursorTop + 1);
         }
      }
      
      Console.SetCursorPosition(0, initial_top);
   }

   public static void clear_input(int start, int end, int lines = 1) {
      int initial_top = Console.CursorTop - lines;
      Console.SetCursorPosition(start, Console.CursorTop - lines);
      end = end == -1 ? Console.WindowWidth - 50: end + 1;
      for (int l = 0; l < lines; l++) {
         for (int i = 0; i < end; i++)  {
            Console.Write(" ");
         }
         if (l < lines - 1) {
            Console.SetCursorPosition(start, Console.CursorTop + 1);
         }
      }
      Console.SetCursorPosition(0, initial_top);
   }

   public static void clear_console() {
      System.Console.Clear();
   }

   // collections
   public static bool in_collection<T>(this T obj, params T[] args) where T : IEnumerable<T> {
      return args.Contains(obj);
   }

   public static void in_each<T>(this IEnumerable<T> ie, Action<T, int> action) {
      int i = 0;
      foreach (var e in ie) {
         action(e, i++);
      }
   }

   // math
   public static double pow(this double i, double pow) {
      double answer = i;
      for (int x = 0; x < pow - 1; x++) {
         answer *= i;
      }
      return pow == 0 ? 1 : answer;
   }

   public static int int_digits(int number) {
      int total_spaces = 1;
      double base_ten = 10;

      while (number >= base_ten.pow(total_spaces)) {
         total_spaces++;
      }
      return total_spaces;
   }
}
