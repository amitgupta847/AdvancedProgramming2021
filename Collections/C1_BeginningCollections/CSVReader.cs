using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.C1_BeginningCollections
{
  class CSVReader
  {
    private string _csvFilePath = "";

    public CSVReader(string csvFilePath)
    {
      _csvFilePath = csvFilePath;
    }

    public List<Country> ReadAllCountries()
    {
      List<Country> countries = new List<Country>();

      using (StreamReader sr = new StreamReader(_csvFilePath))
      {
        //read the header and ignore it so that stream points to next line.
        sr.ReadLine();
        string csvLine;
        while ((csvLine = sr.ReadLine()) != null)
        {
          Country country = ReadCountryFromcsvLine(csvLine);
          countries.Add(country);
        }
      }

      return countries;
    }


    public Country[] ReadFirstNCountries(int nCountries)
    {
      Country[] countries = new Country[nCountries];
      using (StreamReader sr = new StreamReader(_csvFilePath))
      {
        //read the header and ignore it so that stream points to next line.
        sr.ReadLine();
        
        for (int i = 0; i < nCountries; i++)
        {
          string csvLine = sr.ReadLine();
          if (csvLine == null)
            break;
          countries[i] = ReadCountryFromcsvLine(csvLine);
        }
      }
      return countries;

    }


    public Country ReadCountryFromcsvLine(string csvLine)
    {
      string[] parts = csvLine.Split(",");
      int population;
      int.TryParse(parts[3], out population);
      return new Country(parts[0], parts[1], parts[2],population );
    }
  }
}
