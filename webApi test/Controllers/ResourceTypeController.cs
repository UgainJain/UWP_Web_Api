using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi_test.Data;
using webApi_test.Models;
using webApi_test.Models.DTO;
using webApi_test.ViewModels.ResourceType;

namespace webApi_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceTypeController : ControllerBase

    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        public ResourceTypeController(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;

        }

        //GET: api/ResourceType
        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAllResourceType()
        {
            try
            {
                var resourceTypes = _applicationContext.ResourceType.ToList();
                var ResourceTypesList = new List<ResorceTypesDTO>();
                if (resourceTypes.Count != 0)
                {
                    foreach (var type in resourceTypes)
                    {
                        ResorceTypesDTO resourceTypeDTO = _mapper.Map<ResorceTypesDTO>(type);
                        ResourceTypesList.Add(resourceTypeDTO);

                    }
                    return Ok(new { resourceTypes = ResourceTypesList });
                }
                else
                    return Ok(new { message = "No Resource Type Found" });
            }
            catch (Exception e)
            {
                return BadRequest(new { ExceptionEncountered = e.Message });
                throw;
            }
        }


        // GET: api/ResourceType/5
        [HttpGet]
        public ActionResult GetspecficResourcetype([FromQuery] string resTypeName)
        {
            if (resTypeName != null)
            {
                var foundresult = _mapper.Map<ResorceTypesDTO>(_applicationContext.ResourceType.Single(c => c.Name == resTypeName));
                if (foundresult.Name != null)
                {
                    return Ok(foundresult);
                }
                else
                    return BadRequest(new { message = "No value found for Given Resource type" });
            }
            else
                return BadRequest(new { message = "No value given in Query" });
        }

        // POST: api/ResourceType
        [HttpPost]
        public ActionResult PostResourceType(ResourceTypeViewModel resourceTypeView)
        {
            try
            {
                var duplicateCheck = _applicationContext.ResourceType.Where(c => c.Name == resourceTypeView.Name).FirstOrDefault();
                if (duplicateCheck == null)
                {
                    _applicationContext.Add(_mapper.Map<ResourceTypeModel>(resourceTypeView));
                    _applicationContext.SaveChanges();
                    return Ok(string.Format("New Resource Type '{0}' created", resourceTypeView.Name));
                }
                else
                {
                    return BadRequest(new { error = "Duplicate Entry Found" });

                }

            }
            catch (Exception e)
            {
                return BadRequest(new { ExceptionEncountered = e.Message });
                throw;
            }
        }

        // PUT: api/ResourceType/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public ActionResult Delete([FromQuery]int id)
        {
            var Entitytodelete = _applicationContext.ResourceType.Find(id);
            if (Entitytodelete != null)
            {
                try
                {
                    var Childcheck = _applicationContext.Resources.Where(x => x.TypeID == id).FirstOrDefault();
                    if (Childcheck == null)
                    {
                        _applicationContext.ResourceType.Remove(Entitytodelete);

                        _applicationContext.SaveChanges();

                        return Ok("Resource type deleted");
                    }
                    else {
                        return BadRequest(new { Message = "First Delete the Resources of Resource type" });
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(new { ExceptionEncoutered = e.Message });
                    throw;
                }
            }
            else
                return NotFound("Resource Type not found");
        }
    }
}
