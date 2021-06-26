using AutoMapper;
using Inventory_Management.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Inventory_Management.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Product[]>> GetProducts()
        {
            try
            {
                return await _repository.GetProductsAync();

            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot get products");

            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByID(int id)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var product = await _repository.GetProductByIDAsync(id);
                if (product == null) return NotFound("Invalid ID");
                return product;

            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot get product");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductModel productModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                if (!ModelState.IsValid) return BadRequest();
                var productTocreate = _mapper.Map<Product>(productModel);
                var result = await _repository.AddProductAsync(productTocreate);
                return result;
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot Create product");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var itemToDelete = await _repository.GetProductByIDAsync(id);
                if (itemToDelete == null) return NotFound("Invalid ID");
                var result=await _repository.DeleteProductByIDAsync(id);
                return result ? Ok() : StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");

            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot Delete product");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct(Product changesItem)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                
                var itemToUpdate = await _repository.GetProductByIDAsync(changesItem.Id);
                if (itemToUpdate == null) return NotFound("InvalidId");
                var result =await _repository.UpdateProductAsync(changesItem);
                return result;

            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot Update product");
            }
        }
    }
}
