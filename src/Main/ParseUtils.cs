namespace RapidUtils;

public static class ParseUtils
{
   public static string[] substring_patterns(this string content, string subA, string subB, Func<char, bool> f) {
      var substrings = new List<string>();
      int[] indexes = pattern_indexes(content, subA, subB, f);

      for (int x = 0; x < indexes.Length; x++) {
         int start = split_indexer(content, indexes[x], subA, subB, f);
         if (start == -1) continue;

         int end = x != indexes.Length - 1 ? indexes[x + 1] - start : content.Length - start;

         substrings.Add(content.Substring(start, end));
      }
      
      return substrings.ToArray();
   }

   private static int split_indexer(string s, int startIndex, string subA, string subB, Func<char, bool> f) {
      int start = s.IndexOf(subA, startIndex);
      int end = s.IndexOf(subB, start + 1);

      if (contains_only(s, start + 1, end - 1 , f)) {
         return end + 1;
      }

      return -1;
   }

   public static int[] pattern_indexes(string s, string subA, string subB, Func<char, bool> f) {
      var indexes = new List<int>();

      int startIndex = s.IndexOf(subA);
      while (true) {
         if (startIndex == -1) break;
         int end = s.IndexOf(subB, startIndex + subA.Length);
         if (end == -1) break;

         if (s.contains_only(start: startIndex + subA.Length, end: end - 1, f)) {
            indexes.Add(startIndex);
         }

         startIndex = s.IndexOf(subA, end + 1);
      }

      return indexes.ToArray();
   }

   public static int[] pattern_contents(this string s, string subA, string subB, Func<char, bool> f) {
      var contents = new List<int>();
      int start_index = s.IndexOf(subA);
      while (true) {
         if (start_index == -1) break;
         int end = s.IndexOf(subB, start_index + subA.Length);
         if (end == -1) break;

         if (s.contains_only(start: start_index + subA.Length, end: end - 1, f)) {
            contents.Add(to_int(s.Substring(start_index + subA.Length, end - start_index - 1)));
         }

         start_index = s.IndexOf(subA, end + 1);
      }

      return contents.ToArray();
   }

   public static bool contains_only(this string s, int start, int end, Func<char, bool> f) {
      if (end < start) return false;

      for (int x = start; x <= end; x++) {
         if (!f(s[x])) {
            return false;
         }
      }
      return true;
   }

   public static bool contains_only(this string s, int start, Func<char, bool> f) {
      for (int x = start; x < s.Length; x++) {
         if (!f(s[x])) {
            return false;
         }
      }
      return true;
   }

   public static int to_int(string? s) {
      return Int32.TryParse(s, out int number) == false ? 0 : number;
   }
}