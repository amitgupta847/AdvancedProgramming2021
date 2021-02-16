using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Linq;

namespace Linq.Example1
{

  class Files
  {

    //Write a program that will list the five largest files inside of a directory. 
    public static void ShowLargeFilesWithLINQ(string path)
    {
      DirectoryInfo dir = new DirectoryInfo(path);
      var query = dir.GetFiles().OrderByDescending(f => f.Length).Take(5);
      
      //Amit- since there is a order by clause, so query will execute for all the files and then it will take  among that.
      foreach (var file in query)
      {
        Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
      }

    }


    public static void ShowLargeFilesWithoutLINQ(string path)
    {
      DirectoryInfo dir = new DirectoryInfo(path);

      FileInfo[] files = dir.GetFiles();
      Array.Sort(files, new FileInfoComparer());
      for (int i = 0; i < 5; i++)
      {
        FileInfo file = files[i];
        Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
      }
    }

    public class FileInfoComparer : IComparer<FileInfo>
    {
      public int Compare(FileInfo x, FileInfo y)
      {
        return y.Length.CompareTo(x.Length);
      }


    }

  }
}
