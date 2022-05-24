public class LogPrinter
{
   private int _x_pos;
   private int _y_pos;
   private List<string?> _stored_messages;

   public LogPrinter(int startX, int startY) {
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

      int iterations = Math.Min(_stored_messages.Count, 20);
      for (int a = 0; a < iterations; a++) {
         Console.SetCursorPosition(_x_pos, _y_pos + a);
         print(_stored_messages[_stored_messages.Count - (1 + a)]);
         Console.SetWindowPosition(0, 0);
      }
   }
}