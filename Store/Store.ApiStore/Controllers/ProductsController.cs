using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.ApiStore.Services.Base;
using Store.ApiStore.VewModels;
using Store.ApiStore.VewModels.Product;

namespace Store.ApiStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpPost("search")]
        [ProducesResponseType(200, Type = typeof(ResponceModel<ProductGetModel>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<ActionResult<ResponceModel<ProductGetModel>>> Search([FromBody] RequestModel sortSearchModel)
        {

            return await _productService.Search(sortSearchModel);
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ProductGetModel[]))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<ActionResult<ProductGetModel[]>> GetAll()
        {

            return await _productService.GetByAll();
        }


        [HttpGet("removed")]
        [ProducesResponseType(200, Type = typeof(ProductGetModel[]))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<ActionResult<ProductGetModel[]>> GetAllRemoved()
        {

            return await _productService.GetAllRemoved();
        }

        [HttpGet("{guid}")]
        [ProducesResponseType(200, Type = typeof(ProductGetModel))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult<ProductGetModel>> GetById(Guid guid)
        {
            var result = await _productService.GetById(guid);
            return result;
        }

        [HttpPost]
        //[RequestFormLimits(MultipartBodyLengthLimit = 9999999)]
        [ProducesResponseType(201, Type = typeof(ProductPostModel))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<ActionResult<Guid>> Create([FromBody] ProductPostModel model)
        {
            var newGuid = await _productService.Create(model);

            return StatusCode((int)HttpStatusCode.Created, newGuid);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<NoContentResult> Update([FromBody] ProductPutModel model)
        {

            await _productService.Update(model);

            return NoContent();
        }

        [HttpDelete("{guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(409, Type = typeof(string))]
        public async Task<NoContentResult> Delete(Guid guid)
        {
            await _productService.Delete(guid);

            return NoContent();
        }

        [HttpPost("{guid}/restore")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(409, Type = typeof(string))]
        public async Task<NoContentResult> Restore(Guid guid)
        {
            await _productService.Restore(guid);

            return NoContent();
        }

        [HttpGet("images")]
        public async Task<IActionResult> BannerImageAsync(string fileUrl)
        {
            if (string.IsNullOrWhiteSpace(fileUrl))
                return BadRequest();

            var directory = Directory.GetCurrentDirectory();
            var fullFileName = Path.Combine(directory, "Images", fileUrl);

            if (!System.IO.File.Exists(fileUrl))
                await _productService.CreateImageFromDb(fileUrl);

            return PhysicalFile(fullFileName, "image/png");
        }

    }

}