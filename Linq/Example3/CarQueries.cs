using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Linq.Example3
{
  public class CarQueries
  {

    //extension method syntax
    public static List<Car> ProcessCars1(string path)
    {
      var query = File.ReadAllLines(path).
                      Skip(1).
                      Where(line => line.Length > 1).
                      Select(line => Car.Parse(line));

      return query.ToList();
    }

    //using ToCar() as extension method
    public static List<Car> ProcessCars2(string path)
    {
      //ToCar has been implemented to support Deferred execution.
      var query = File.ReadAllLines(path).Skip(1).Where(line => line.Length > 1).ToCar();

      return query.ToList();
    }


    public static List<Car> ProcessCars3(string path)
    {
      var query = File.ReadAllLines(path).Skip(1).Where(line => line.Length > 1).Select(Car.Parse);

      return query.ToList();
    }

    //query syntax
    public static List<Car> ProcessCars(string path)
    {
      var query = from line in File.ReadLines(path).Skip(1)
                  where line.Length > 1
                  select Car.Parse(line);

      return query.ToList();
    }

    public static List<Car> GetCars()
    {
      string path = @"D:\microsoft_2021_target\AdvancedProgramming_2021\DataFiles\fuel.csv";
      List<Car> cars = CarQueries.ProcessCars2(path);
      return cars;
    }

    //extension method
    public static void PrintCarsRating()
    {
      List<Car> cars = GetCars();

      var query = cars.Where(car => car.Manufacturer == "BMW" && car.Year == 2016).
        OrderByDescending(c => c.Combined).ThenBy(c => c.Name).Select(c => c);

      //order by rating and then by name in order to break the tie between two having same rating
      foreach (Car c in query.Take(10))
      {
        Console.WriteLine($"{c.Name,-20} : {c.Combined,5:N0}");
      }
    }

    //query syntax
    public static void PrintCarsRatingQuerySyntax()
    {
      List<Car> cars = GetCars();

      var query = from car in cars
                  orderby car.Combined descending, car.Name ascending
                  select car;

      //order by rating and then by name in order to break the tie between two having same rating
      foreach (Car c in query.Take(10))
      {
        Console.WriteLine($"{c.Name,-20} : {c.Combined,5:N0}");
      }
    }

    //extension method
    public static void PrintCarsStatistics()
    {
      List<Car> cars = GetCars();

      //do not use First() as it will throw exception if criteria produces empty sequence.

      //Car car = cars.Where(car => car.Manufacturer == "BMWNoMatch" && car.Year == 2016).
      //  OrderByDescending(c => c.Combined).ThenBy(c => c.Name).Select(c => c).First();

      //use FirstOrDefault
      Car car = cars.Where(car => car.Manufacturer == "BM22WNoMatch" && car.Year == 2016).
        OrderByDescending(c => c.Combined).ThenBy(c => c.Name).Select(c => c).FirstOrDefault();

      if (car != null)
        Console.WriteLine($"{car.Name,-20} : {car.Combined,5:N0}");
      else
        Console.WriteLine($"No car found with provided criteria");
    }


    //Showing usage of SelectMany
    //Select many transform a collection of collections, a sequence of sequences, into a single sequence.
    public static void PrintCarNameUsingSelectMany()
    {
      List<Car> cars = GetCars();
      var result = cars.Take(5).SelectMany(c => c.Name).OrderBy(c => c);
      foreach (var item in result)
      {
        Console.WriteLine(item);
      }
    }
   
  }
}
