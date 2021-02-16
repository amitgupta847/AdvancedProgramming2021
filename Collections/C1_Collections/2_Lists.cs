using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.C1_Collections
{
  class Lists
  {
    public static void ReadCountries(string filePath)
    {
      CSVReader reader = new CSVReader(filePath);

      List<Country> countries = reader.ReadAllCountries();
      foreach (Country country in countries.Take(10))
      {
        Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
      }
    }


    //intention of below code is to show the usage of for loop.
    //not all the things can be achieved with foreach loop
    public static void ReadCountriesInBatches(string filePath)
    {
      CSVReader reader = new CSVReader(filePath);

      IList<Country> countries = reader.ReadAllCountries();

      Console.WriteLine("Enter the number of countries to display> 10");
      int number = int.Parse(Console.ReadLine());

      for (int i = 0; i < countries.Count; i++)
      {
        if (i != 0 && i % number == 0)
        {
          Console.WriteLine("Hit return to continue, anything else to quit");

          string userInput = Console.ReadLine();
          if (userInput != "")
            break;
        }

        Country country = countries[i];
        Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
      }
    }

    //intention of below code is to show the usage of for loop.
    //not all the things can be achieved with foreach loop
    public static void ReadCountriesInReverseOrderInBatches(string filePath)
    {
      CSVReader reader = new CSVReader(filePath);

      List<Country> countries = reader.ReadAllCountries();

      Console.WriteLine("Enter the number of countries to display> 10");
      int number = int.Parse(Console.ReadLine());
      
      int defaultIndex = 0;
      for (int i = countries.Count - 1; i > 0; i--)
      {
        if (defaultIndex != 0 && defaultIndex % number == 0)
        {
          Console.WriteLine("Hit return to continue, anything else to quit");

          string userInput = Console.ReadLine();
          if (userInput != "")
            break;
        }

        Country country = countries[i];
        Console.WriteLine($"{++defaultIndex}: {PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
      }
    }


    //Remember: RemoveAt() method in list is expensive also it shift elements so better to do in reverse order,i.e. start from last index .
    //same for Inserting items.
  }
}
