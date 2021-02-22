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
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
      var products = await _shopContext.Products.ToListAsync();
      return Ok(products);
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
