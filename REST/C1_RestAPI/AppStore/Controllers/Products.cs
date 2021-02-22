using AppStore.Classes;
using AppStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppStore.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProductsController : ControllerBase
  {

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ShopContext _shopContext;

    public ProductsController(ILogger<WeatherForecastController> logger, ShopContext shopContext)
    {
      _logger = logger;
      this._shopContext = shopContext;

      //one way to check if database exist with data or not, if not then perform the seed else no action
      _shopContext.Database.EnsureCreated();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts([FromQuery] ProductQueryParameters queryParameters)
    {
      IQueryable<Product> products = _shopContext.Products;

      if (queryParameters.MinPrice != null && queryParameters.MaxPrice != null)
      {
        products = products.Where(prod => prod.Price >= queryParameters.MinPrice &&
          prod.Price <= queryParameters.MaxPrice);
      }

      if (!string.IsNullOrEmpty(queryParameters.Sku))
      {
        products = products.Where(prod => prod.Sku.Equals(queryParameters.Sku));
      }

      if (!string.IsNullOrEmpty(queryParameters.Name))
      {
        products = products.Where(prod => prod.Name.ToLower().Contains(queryParameters.Name.ToLower()));

      }

      if (!string.IsNullOrEmpty(queryParameters.SortBy))
      {
        if (typeof(Product).GetProperty(queryParameters.SortBy) != null)
        {
          products = products.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);
        }
      }

      products = products.Skip(queryParameters.Size * (queryParameters.Page - 1))
              .Take(queryParameters.Size);

      var finalProducts = await products.ToListAsync();
      return Ok(finalProducts);
    }

    [HttpGet("{id:int}")]  // if user doesn't pass id as int he will get not found resource as error - 404. 
    public async Task<ActionResult<Product>> Get(int id)
    {
      var product = await _shopContext.Products.FindAsync(id);

      if (product == null)
        return NotFound();

      return Ok(product);
    }
  }
}
