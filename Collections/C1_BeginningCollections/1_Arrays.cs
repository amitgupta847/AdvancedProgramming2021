using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.C1_BeginningCollections
{
  class Arrays
  {
    public static void ReadCountries(string filePath)
    {
      CSVReader reader = new CSVReader(filePath);

      Country[] countries = reader.ReadFirstNCountries(10);
      foreach (Country country in countries)
      {
        Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
      }
    }

  }
}
