using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsManagement.Application.Features.Products.Commands;
using ProductsManagement.Application.Features.Products.Commands.CreateProduct;
using ProductsManagement.Application.Features.Products.Commands.DeleteProductById;
using ProductsManagement.Application.Features.Products.Commands.UpdateProduct;
using ProductsManagement.Application.Features.Products.Queries.GetAllProducts;
using ProductsManagement.Application.Features.Products.Queries.GetProductById;
using ProductsManagement.Application.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.WebApi.Controllers.v1
{
    [Route("api/")]
    public class ProductController : BaseApiController
    {
        /// <summary>
        /// Get All products paginated list filtered by title or description or not.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Return products paginated list with filter criteria.</returns>
        [HttpGet]
        [Route("products", Name = "GetAllProductsList")]
        public async Task<IActionResult> GetAllProductsList([FromQuery] GetAllProductsParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllProductsQuery() { Title = filter.Title, Description = filter.Description, PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        /// <summary>
        /// Get product details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return product details.</returns>
        [HttpGet]
        [Route("product/{id}", Name = "GetProductDetails")]
        public async Task<IActionResult> GetProductDetails(int id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
        }

        /// <summary>
        /// Create new product.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Return whether product created successfully or not</returns>
        [HttpPost]
        [Route("product", Name = "CreatNewProduct")]
        [Authorize]
        public async Task<IActionResult> CreatNewProduct(CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns>Return whether product updated successfully or not</returns>
        [HttpPut]
        [Route("product/{id}", Name = "UpdateProduct")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommand command)
        {
            if (id != command.ProductId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return whether product deleted successfully or not</returns>
        [HttpDelete]
        [Route("product/{id}", Name = "DeleteProduct")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteProductByIdCommand { Id = id }));
        }
    }
}
