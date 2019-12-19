using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.ApiStore.Services.Base;
using Store.ApiStore.VewModels.Customer;

namespace Store.ApiStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(CustomerGetModel[]))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<ActionResult<CustomerGetModel[]>> GetAll()
        {

            return await _employeeService.GetByAll();
        }


        [HttpGet("removed")]
        [ProducesResponseType(200, Type = typeof(CustomerGetModel[]))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<ActionResult<CustomerGetModel[]>> GetAllRemoved()
        {

            return await _employeeService.GetAllRemoved();
        }

        [HttpGet("{guid}")]
        [ProducesResponseType(200, Type = typeof(CustomerGetModel))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult<CustomerGetModel>> GetById(Guid guid)
        {

            return await _employeeService.GetById(guid);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CustomerPostModel))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<ActionResult<CustomerGetModel>> Create([FromBody] CustomerPostModel model)
        {

            var newGuid = await _employeeService.Create(model);

            return StatusCode((int)HttpStatusCode.Created, newGuid);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<NoContentResult> Update([FromBody] CustomerPutModel model)
        {
            await _employeeService.Update(model);

            return NoContent();
        }

        [HttpDelete("{guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(409, Type = typeof(string))]
        public async Task<NoContentResult> Delete(Guid guid)
        {
            await _employeeService.Delete(guid);

            return NoContent();
        }

        [HttpPost("{guid}/restore")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(409, Type = typeof(string))]
        public async Task<NoContentResult> Restore(Guid guid)
        {
            await _employeeService.Restore(guid);

            return NoContent();
        }
    }
}