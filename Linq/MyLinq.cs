using Linq.Example3;
using System;
using System.Collections.Generic;
using System.Text;

namespace Linq
{
  public static class MyLinq
  {

    //Learning: Streaming opeator understanding
    public static IEnumerable<double> Random()
    {
      var random = new Random();
      while (true)
      {
        yield return random.NextDouble();
      }
    }

    //Learning: understanding how LINQ works using yield
    public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
      foreach (var item in source)
      {
        if (predicate(item))
          yield return item;
      }
    }

    public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
    {
      foreach (var line in source)
      {
        var columns = line.Split(',');

        yield return new Car
        {
          Year = int.Parse(columns[0]),
          Manufacturer = columns[1],
          Name = columns[2],
          Displacement = double.Parse(columns[3]),
          Cylinders = int.Parse(columns[4]),
          City = int.Parse(columns[5]),
          Highway = int.Parse(columns[6]),
          Combined = int.Parse(columns[7])
        };
      }
    }

    //Learning: understanding how LINQ works, but using yield
    //public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    //{
    //  var result = new List<T>();

    //  foreach (var item in source)
    //  {
    //    if (predicate(item))
    //      result.Add(item);
    //  }

    //  return result;
    //}
  }
}
