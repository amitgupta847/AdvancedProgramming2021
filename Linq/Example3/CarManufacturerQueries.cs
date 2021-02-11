using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Linq.Example3
{
  public class CarManufacturerQueries
  {
    public static List<Manufacturer> GetManufacturers()
    {
      return ProcessManufacturers();
    }

    public static List<Manufacturer> ProcessManufacturers()
    {
      string path = @"D:\microsoft_2021_target\AdvancedProgramming_2021\DataFiles\manufacturers.csv";

      var query = File.ReadAllLines(path).Where(line => line.Length > 1).Select(Manufacturer.Parse);

      return query.ToList();
    }

    //find out most fuel efficient cars with manufacturer head quarter
    public static void MostEfficientCarsWithManufHeadQuarter()
    {
      var cars = CarQueries.GetCars();
      var manufacturers = GetManufacturers();

      var query = from car in cars
                  join manuf in manufacturers
                  on car.Manufacturer equals manuf.Name
                  orderby car.Combined descending, car.Name ascending
                  select new
                  {
                    manuf.Headquarters,
                    car.Name,
                    car.Combined
                  };

      foreach (var record in query.Take(10))
      {
        Console.WriteLine($"{record.Headquarters,-12} {record.Name,-20} :{record.Combined,5}");
      }
    }

    //find out most fuel efficient cars with manufacturer head quarter
    public static void MostEfficientCarsWithManufHeadQuarter_ExMethod()
    {
      var cars = CarQueries.GetCars();
      var manufacturers = GetManufacturers();

      var query = cars.Join(manufacturers, car => car.Manufacturer, manuf => manuf.Name, (car, manuf) =>
             new
             {
               manuf.Headquarters,
               car.Name,
               car.Combined
             }).OrderByDescending(record => record.Combined).ThenBy(record => record.Name);


      foreach (var record in query.Take(10))
      {
        Console.WriteLine($"{record.Headquarters,-12} {record.Name,-20} :{record.Combined,5}");
      }
    }

    //find out most fuel efficient cars with manufacturer head quarter
    //slightly different way of composing join 
    public static void MostEfficientCarsWithManufHeadQuarter_ExMethodWithSelect()
    {
      var cars = CarQueries.GetCars();
      var manufacturers = GetManufacturers();

      var query = cars.Join(manufacturers, car => car.Manufacturer, manuf => manuf.Name, (car, manuf) =>
             new
             {
               car,
               manuf
             }).OrderByDescending(r => r.car.Combined).ThenBy(r => r.car.Name).
             Select(r =>
             new
             {
               r.manuf.Headquarters,
               r.car.Name,
               r.car.Combined
             });


      foreach (var record in query.Take(10))
      {
        Console.WriteLine($"{record.Headquarters,-12} {record.Name,-20} :{record.Combined,5}");
      }
    }


    //Composite Key: Join on more than one piece of information
    //Note: Propery name must be same in both the objects.

    //find out most fuel efficient cars with manufacturer head quarter
    public static void MostEfficientCarsWithManufHeadQuarter_CompositeJoin()
    {
      var cars = CarQueries.GetCars();
      var manufacturers = GetManufacturers();

      var query = from car in cars
                  join manuf in manufacturers
                  on new { car.Manufacturer, car.Year }
                  equals new { Manufacturer = manuf.Name, manuf.Year }
                  orderby car.Combined descending, car.Name ascending
                  select new
                  {
                    manuf.Headquarters,
                    car.Name,
                    car.Combined
                  };

      foreach (var record in query.Take(10))
      {
        Console.WriteLine($"{record.Headquarters,-12} {record.Name,-20} :{record.Combined,5}");
      }
    }

    //Extension method approach
    //Composite Key: Join on more than one piece of information
    //Note: Propery name must be same in both the objects.

    //find out most fuel efficient cars with manufacturer head quarter
    public static void MostEfficientCarsWithManufHeadQuarter_CompositeJoin_ExMethod()
    {
      var cars = CarQueries.GetCars();
      var manufacturers = GetManufacturers();

      var query = cars.Join(manufacturers,
                            car => new { car.Manufacturer, car.Year },
                            manuf => new { Manufacturer = manuf.Name, manuf.Year },
                            (car, manuf) =>
                                 new
                                 {
                                   manuf.Headquarters,
                                   car.Name,
                                   car.Combined
                                 }).OrderByDescending(record => record.Combined).ThenBy(record => record.Name);


      foreach (var record in query.Take(10))
      {
        Console.WriteLine($"{record.Headquarters,-12} {record.Name,-20} :{record.Combined,5}");
      }
    }


    //Grouping
    //find out count of cars for each manufacturer. 
    public static void CountCarsPerManufacturer()
    {
      var cars = CarQueries.GetCars();
      var manufacturers = GetManufacturers();

      var query = from car in cars
                  group car by car.Manufacturer;


      foreach (var result in query)
      {
        Console.WriteLine($"{result.Key,-20}" +
          $" has {result.Count(),5} Cars.");
      }
    }

    //lets see the cars that are inside of each grouping. 
    public static void ShowCarsPerManufacturer()
    {
      var cars = CarQueries.GetCars();
      var manufacturers = GetManufacturers();

      var query = from car in cars
                  group car by car.Manufacturer.ToUpper() into manufacturer
                  orderby manufacturer.Key
                  select manufacturer;


      foreach (var group in query)
      {
        Console.WriteLine($"{group.Key,-12} has {group.Count(),5} Cars.");

        //lets print best in rating first 2 cars
        foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
        {
          Console.WriteLine($"    {car.Name} : {car.Combined}");
        }
      }
    }

    public static void ShowCarsPerManufacturer_ExtensionMethod()
    {
      var cars = CarQueries.GetCars();
      var manufacturers = GetManufacturers();

      var query = cars.GroupBy(car => car.Manufacturer.ToUpper()).OrderBy(g => g.Key).Select(r=>r);
      
      //Note: Select function in above case is optional. Code will work fine even if there is no select

      foreach (var group in query)
      {
        Console.WriteLine($"{group.Key,-12} has {group.Count(),5} Cars.");

        //lets print best in rating first 2 cars
        foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
        {
          Console.WriteLine($"    {car.Name} : {car.Combined}");
        }
      }
    }



    //GroupJoin :  Pending
    //public static void ShowCarsPerManufacturer_ExtensionMethod()
    //{
    //  var cars = CarQueries.GetCars();
    //  var manufacturers = GetManufacturers();

    //  var query = cars.GroupBy(car => car.Manufacturer.ToUpper()).OrderBy(g => g.Key).Select(r => r);

    //  //Note: Select function in above case is optional. Code will work fine even if there is no select

    //  foreach (var group in query)
    //  {
    //    Console.WriteLine($"{group.Key,-12} has {group.Count(),5} Cars.");

    //    //lets print best in rating first 2 cars
    //    foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
    //    {
    //      Console.WriteLine($"    {car.Name} : {car.Combined}");
    //    }
    //  }
    //}



    //Aggregating Data
  }
}
