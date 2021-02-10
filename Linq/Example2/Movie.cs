using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Linq.Example2
{
  class Movie
  {
    public string Title { get; set; }
    public float Rating { get; set; }

    private int _year;

    public int Year
    {
      get
      {
        Console.WriteLine($"Returning {_year} for {Title}");
        return _year;

      }
      set
      {
        _year = value;
      }
    }



    public static List<Movie> getMovies()
    {
      return new List<Movie>
      {
        new Movie { Title="The Dark Knight", Rating=8.9f, Year=2008},
        new Movie { Title="The King's Speech", Rating=8.0f, Year=2010},
        new Movie { Title="Casablance", Rating=8.5f, Year=1942},
        new Movie { Title="Star wats V", Rating=8.7f, Year=1980},
      };
    }

    public static void customFilter()
    {
      List<Movie> movies = Movie.getMovies();
      var query = movies.Filter(mov => mov.Year > 2000).Take(1);
      foreach (Movie movie in query)
      {
        Console.WriteLine(movie.Title);
      }
    }

    public static void Movies()
    {
      List<Movie> movies = Movie.getMovies();
      var query = from movie in movies where movie.Year > 2000 select movie;
      foreach (Movie movie in query)
      {
        Console.WriteLine(movie.Title);
      }
    }

    public static void MoviesInOrder()
    {
      List<Movie> movies = Movie.getMovies();
      var query = from movie in movies where movie.Year > 2000 select movie;

      //Learning - its always better to perform order by/sorting after filtering

      foreach (Movie movie in query.ToList().OrderByDescending(mov => mov.Rating))
      {
        Console.WriteLine(movie.Title);
      }
    }
  }
}
