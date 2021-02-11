using Collections.C1_BeginningCollections;
using Collections.C2_ConcurrentCollections;
using System;
using System.IO;
using System.Reflection;

namespace Collections
{
  class Program
  {
    static void Main(string[] args)
    {
      //PlayWithFiles();

      //SimulateOrder1.Start();
      C2_ConcurrentCollections.Ex1_TShirtShop.SimulateShop.Start();

      Console.WriteLine("Press any key to quit!");
      Console.ReadLine();
    }

    public static void PlayWithOrders()
    {
    }






      public static void PlayWithFiles()
    {
      string filePath = GetPopulationCSVFilePath();
      // Arrays.ReadCountries(filePath);
      //Lists.ReadCountries(filePath);
      //Lists.ReadCountriesInBatches(filePath);
      //Lists.ReadCountriesInReverseOrderInBatches(filePath);
    }

    private static string GetPopulationCSVFilePath()
    {
      string strExeFilePath = Assembly.GetExecutingAssembly().Location;
      string strWorkPath = Path.GetDirectoryName(strExeFilePath);
      string csvPath = Path.Combine(strWorkPath, @"C1_BeginningCollections\Pop by Largest Final.csv");
      return csvPath;
    }
  }
}



/*
 
//This will give us the full name path of the executable file:
//i.e. C:\Program Files\MyApplication\MyApplication.exe
string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
//This will strip just the working path name:
//C:\Program Files\MyApplication
string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath)

//C:\Program Files\MyApplication\Settings.xml
string strSettingsXmlFilePath = System.IO.Path.Combine(strWorkPath, "Settings.xml");



var localDir = Assembly.GetExecutingAssembly().GetDirectoryPath();

var localDir = typeof(DaoTests).Assembly.GetDirectoryPath();

public static string GetDirectoryPath(this Assembly assembly)
{
  string filePath = new Uri(assembly.CodeBase).LocalPath;
  return Path.GetDirectoryName(filePath);
}

*/