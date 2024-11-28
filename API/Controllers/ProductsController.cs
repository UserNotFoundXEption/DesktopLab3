using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductsDbContext _context;

    public ProductsController(ProductsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return _context.products.ToList();
    }

    [HttpGet("{name}")]
    public ActionResult<Product> GetProduct(string name)
    {
        var product = _context.products.Find(name);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    [HttpPost]
    public ActionResult<Product> PostProduct(Product product)
    {
        _context.products.Add(product);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetProduct), new { name = product.Name }, product);
    }

    [HttpPut("{name}")]
    public IActionResult PutFilm(string name, Product product)
    {
        if (name != product.Name)
        {
            return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{name}")]
    public IActionResult DeleteFilm(string name)
    {
        var product = _context.products.Find(name);
        if (product == null)
        {
            return NotFound();
        }

        _context.products.Remove(product);
        _context.SaveChanges();

        return NoContent();
    }
}
