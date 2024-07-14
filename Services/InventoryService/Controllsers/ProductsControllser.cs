using Microsoft.AspNetCore.Mvc;
using InventoryService.Models;
using InventoryService.Services;
using System.Threading.Tasks;

namespace InventoryService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        var createdProduct = await _productService.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id.ToString() }, createdProduct);
    }

    // PUT: api/Products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(string id, Product updatedProduct)
    {
        var product = await _productService.UpdateProductAsync(id, updatedProduct);

        if (product == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(string id)
    {
        var product = await _productService.GetProductAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // PATCH: api/Products/5/quantity
    [HttpPatch("{id}/quantity")]
    public async Task<IActionResult> UpdateProductQuantity(string id, [FromBody] int quantityChange)
    {
        var result = await _productService.UpdateProductQuantityAsync(id, quantityChange);

        if (!result)
            return BadRequest("Quantity cannot be negative or product not found.");

        return NoContent();
    }

    // DELETE: api/Products/quantity/5
    [HttpDelete("quantity/{historyId}")]
    public async Task<IActionResult> CancelQuantityChange(string historyId)
    {
        var result = await _productService.CancelQuantityChangeAsync(historyId);

        if (!result)
            return BadRequest("Quantity change could not be cancelled or product not found.");

        return NoContent();
    }
}

