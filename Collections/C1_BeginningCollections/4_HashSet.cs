
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.C1_BeginningCollections
{
  class HashSet
  {
    public static void StartMethod()
    {
      ReadCountries(@"D:\microsoft_2021_target\AdvancedProgramming_2021\DataFiles\Pop by Largest Final.csv");
    }


    //Showing usage of HashSet, Sorted hashset and their methods like UnionWith or IntersectWith. (Do note that LINQ also provide those methods with name Union and Intersect).
    public static void ReadCountries(string filePath)
    {
      CSVReader reader = new CSVReader(filePath);

      Country[] countries = reader.ReadFirstNCountries(10);

      //ToDictinary throws exception if there are more than 1 entry with same key in data.      //StringComparer.OrdinalIgnoreCase

      HashSet<Country> set = new HashSet<Country>(new CountryEqualityComparor());

      //adding china - we will see that the China from excel sheet will not be added in this set.
      set.Add(new Country("China", "CHN", "Asia", 150000));

      foreach (Country cnt in countries)
      {
        set.Add(cnt);
      }


      foreach (var item in set)
      {
        Console.WriteLine($"{item.Name,-20}  {item.Code}");
      }


      //what if we want item in sorted order
      //Use Sorted Set
      Console.WriteLine("\n\n Lets see how we can get set items in sorted order.\n");
      SortedSet<Country> sortedSet = new SortedSet<Country>(new CountryEqualityComparor());
      //adding china - we will see that the China from excel sheet will not be added in this set.
      sortedSet.Add(new Country("China", "CHN", "Asia", 150000));

      foreach (Country cnt in countries)
      {
        sortedSet.Add(cnt);
      }

      foreach (var item in sortedSet)
      {
        Console.WriteLine($"{item.Name,-20}  {item.Code}");
      }

      //TODO: You can explore following set operation 
      //      set.UnionWith
      //      set.IntersectWith
    }
  }
}
