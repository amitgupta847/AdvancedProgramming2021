using System;
using System.Collections.Generic;
using System.Text;

namespace Linq.Example3
{
  public class Manufacturer
  {
    public string Name { get; set; }
    public string Headquarters { get; set; }
    public int Year { get; set; }


    public static Manufacturer Parse(string data)
    {
      string[] columns = data.Split(",");
      return new Manufacturer
      {
        Name = columns[0],
        Headquarters = columns[1],
        Year = int.Parse(columns[2])
      };
    }
  }
}
