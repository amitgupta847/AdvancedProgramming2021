using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.C1_BeginningCollections
{
  class Dictionaries
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
  }
}
