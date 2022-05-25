public class DoNothingAction : IAction
{
   public Character Character { get; init; }
   public string Value { get; init; }
   
   public DoNothingAction(Character c) {
      Character = c;
      Value = "NOTHING";
   }
   
   public void execute() {
      print($"{Character.Name} did ");
      println_colr($"{Value}", ConsoleColor.DarkGray);
      Thread.Sleep(500);
   }
}