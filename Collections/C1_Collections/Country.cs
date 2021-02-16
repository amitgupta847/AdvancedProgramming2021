using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.C1_Collections
{
  public class Country
  {
    public string Name { get; }
    public string Code { get; }
    public string Region { get; }
    public int Population { get; }

    public Country(string name, string code, string region, int population)
    {
      Name = name;
      Code = code;
      Region = region;
      Population = population;
    }

    public override string ToString()
    {
      return $"Name: {Name}, Code: {Code}";
    }
  }

  public class CountryEqualityComparor : IEqualityComparer<Country>, IComparer<Country>
  {
    public int Compare(Country x, Country y)
    {
      return x.Name.CompareTo(y.Name);
    }

    public bool Equals(Country x, Country y)
    {
      return x.Name == y.Name;
    }

    public int GetHashCode([DisallowNull] Country obj)
    {
      return StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Name);
    }
  }
}
