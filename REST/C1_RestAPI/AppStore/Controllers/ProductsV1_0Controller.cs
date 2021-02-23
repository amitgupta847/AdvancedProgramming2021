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
  [ApiVersion("1.0")]
  [ApiController]
  //[Route("v{v:apiVersion}/products")]
  [Route("products")]
  //[Route("v{v:apiVersion}/[controller]")]
  public class ProductsV1_0Controller : ControllerBase
  {

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ShopContext _shopContext;

    public ProductsV1_0Controller(ILogger<WeatherForecastController> logger, ShopContext shopContext)
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


      if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
      {
        products = products.Where(p => p.Sku.ToLower().Contains(queryParameters.SearchTerm.ToLower()) ||
                                       p.Name.ToLower().Contains(queryParameters.SearchTerm.ToLower()));
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
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
      var product = await _shopContext.Products.FindAsync(id);

      if (product == null)
        return NotFound();

      return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
    {
      _shopContext.Products.Add(product);
      await _shopContext.SaveChangesAsync();
      return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
    {
      if (id != product.Id)
        return BadRequest();

      _shopContext.Entry(product).State = EntityState.Modified;

      try
      {
        await _shopContext.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException ex)//What if product is already deleted...
      {
        if (_shopContext.Products.Find(id) == null)
          return NotFound();

        throw;
      }

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
      var product = await _shopContext.Products.FindAsync(id);

      if (product == null)
        return NotFound();

      _shopContext.Products.Remove(product);
      await _shopContext.SaveChangesAsync();

      return product;
    }


    [HttpPost]
    [Route("Delete")]
    public async Task<IActionResult> DeleteMultiple([FromQuery] int[] ids)
    {
      var products = new List<Product>();
      foreach (var id in ids)
      {
        var product = await _shopContext.Products.FindAsync(id);

        if (product == null)
        {
          return NotFound();
        }

        products.Add(product);
      }

      _shopContext.Products.RemoveRange(products);
      await _shopContext.SaveChangesAsync();

      return Ok(products);
    }
  }

}
