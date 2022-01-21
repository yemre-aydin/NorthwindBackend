using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using IProductService = Business.Abstract.IProductService;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]

        public IActionResult GetList()
        {
            var result = _productService.GetList();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("getcategory")]
        public IActionResult Get(int productId)
        {
            var result = _productService.GetById(productId );
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getbyId")]
        public IActionResult GetByCategory(int categoryId)
        {
            var result = _productService.GetByCategory();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);

        }

    }
}
