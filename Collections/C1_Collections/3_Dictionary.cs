using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.C1_Collections
{
  class Dictionaries
  {
    public static void StartMethod()
    {
      ReadCountries(@"D:\microsoft_2021_target\AdvancedProgramming_2021\DataFiles\Pop by Largest Final.csv");
    }


    //Showing usage of List to Dictionary, Sorted Dictionary and SortedList and finally custom key usage (i.e. object as key)
    public static void ReadCountries(string filePath)
    {
      CSVReader reader = new CSVReader(filePath);

      Country[] countries = reader.ReadFirstNCountries(10);

      //ToDictinary throws exception if there are more than 1 entry with same key in data.      //StringComparer.OrdinalIgnoreCase

      Dictionary<String, Country> dict = countries.ToDictionary(c => c.Name);

      foreach (var item in dict)
      {
        Console.WriteLine($"{item.Key,-20}  {item.Value}");
      }


      //data is not display in sorted order in above code, what if we want to have data in sorted order:
      //Use sorted dictionary

      //C# implementation of Sorted dictionary is based on Red Black tree and hence operation happens in O(log n) time instead of O(1) in normal dictionary.

      SortedDictionary<String, Country> sortedDict = new SortedDictionary<string, Country>(dict);
      Console.WriteLine("\nUsing Sorted Dictionary: Lets see if this time we get data in sorted order\n");
      foreach (var item in sortedDict)
      {
        Console.WriteLine($"{item.Key,-20}  {item.Value}");
      }

      //Disadvantage is that you get data sorted based on key, what if we want basedo n value?
      //SortedList is the answer (Its a dictionary though the name is sorted list, and its because underthe hood SortedList uses the LIST)

      //not good in term of scailing. O(n).. try not using it..
      SortedList<String, Country> sortedListDict = new SortedList<string, Country>(dict);
      Console.WriteLine("\nUsing Sorted List: Lets see if this time we get data (values) in sorted order\n");
      foreach (var item in sortedListDict)
      {
        Console.WriteLine($"{item.Key,-20}  {item.Value}");
      }


      //Using object as custom key
      //So far we used string as key, what if want country directly to be used as key.
      //Rule - key should be non modifiable

      //lets see
      Console.WriteLine("\nUsing Country object as key");

      //In that case you should override the Equals method and GetHashCode method.
      //TODO:
    }
  }
}
