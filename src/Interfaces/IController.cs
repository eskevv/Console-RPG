public interface IController
{
   IAction get_command(Character c, List<Character> friendly, List<Character> enemies);
}