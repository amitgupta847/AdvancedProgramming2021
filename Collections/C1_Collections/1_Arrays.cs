using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.C1_Collections
{
  class Arrays
  {

    public static void StartMethod()
    {
      //EqualityCheck();
    }

    public static void ReadCountries(string filePath)
    {
      CSVReader reader = new CSVReader(filePath);

      Country[] countries = reader.ReadFirstNCountries(10);
      foreach (Country country in countries)
      {
        Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
      }
    }



    public static void EqualityCheck()
    {
      DateTime[] bankHol1 = { new DateTime(2021,1,1),
    new DateTime(2021,4,2),
    new DateTime(2021,4,5)};

      DateTime[] bankHol2 = { new DateTime(2021,1,1),
    new DateTime(2021,4,2),
    new DateTime(2021,4,5)};

      //
      Console.WriteLine($"Equality check for two arrays reference type will turn false even though they contain same values. Lets see: {bankHol1 == bankHol2}");


      Console.WriteLine("If you want to see if two arrays contains same elements then use SequenceEqual method");
      Console.WriteLine(bankHol1.SequenceEqual(bankHol2));


    }
  }
}
