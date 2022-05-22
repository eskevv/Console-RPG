public class DoNothing : IAction
{
   public Character Character { get; init; }
   public string Value { get; init; }
   
   public DoNothing(Character c) {
      Character = c;
      Value = "NOTHING";
   }
   
   public void execute() {
      print($"{Character.Name} did ");
      println_colr($"{Value}", ConsoleColor.DarkGray);
   }
}