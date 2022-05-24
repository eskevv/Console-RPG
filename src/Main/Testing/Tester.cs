namespace Tester;

public class Application
{
   public static void run() {
      int t = 2;
      println_color("%0%in red, %2%in blue, %2%in yellow", ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Yellow);
      println_color($"%0%in red, {t} still red, %1%in blue", ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Yellow);

   }
}