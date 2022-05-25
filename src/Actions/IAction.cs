public interface IAction
{
   Character Character { get; init; }
   string Value { get; init; }

   void execute();
}