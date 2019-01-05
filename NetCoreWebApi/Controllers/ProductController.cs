using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PtcApi.Model;
using PtcApi.Services;

namespace PtcApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : BaseApiController
    {
        private readonly IProducts _productsRepository;
        public ProductController(IProducts productsRepository)
        {
            _productsRepository = productsRepository;
        }

        // GET: api/Products
        [HttpGet]
        [Authorize(Policy = "CanAccessProducts")]
        public IActionResult Get()
        {
            try
            {
                var products = _productsRepository.GetAll();

                if (products.Any())
                {
                    return Ok(products);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Can't Find Products");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Exception trying to get all products.");
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = _productsRepository.GetById(id);

                if (product != null)
                {
                    return Ok(product);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Can't Find Product");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Exception trying to get product Id: " + id + ".");
            }
        }

        // POST: api/Products
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
           try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _productsRepository.AddProduct(product);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Exception trying to create a new product.");
            }
        }

        // PUT: api/Products
        [HttpPut]
        public IActionResult Put([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Try catch useful in case of an unexsiting Id
            try
            {
                _productsRepository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Exception trying to update a product.");
            }

            return Ok("Product Updated");
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                //1) Get the object
                var product = _productsRepository.GetById(id);
                if (product == null)
                {
                    return NotFound("Record not found...");
                }

                //2) Delete the object
                _productsRepository.Delete(id);
                return Ok("Product deleted");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Exception trying to delete product: " + id + ".");
            }
        }
    }
}
