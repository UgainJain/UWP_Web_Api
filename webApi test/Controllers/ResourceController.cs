using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using webApi_test.Data;
using webApi_test.Models;
using webApi_test.Models.DTO;
using webApi_test.ViewModels.Resource;
using webApi_test.ViewModels.ResourceType;

namespace webApi_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly ApplicationContext _applicationcontext;
        private readonly IMapper _mapper;

        public ResourceController(ApplicationContext applicationcontext, IMapper mapper)
        {
            _applicationcontext = applicationcontext;
            _mapper = mapper;
        }

        // GET: api/ResourceModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceModel>>> GetResources()
        {
            List<ResourceModel> temp = _applicationcontext.Resources.Include(x => x.Children).Where(x => x.parent == null).Select(x => new ResourceModel { Id = x.Id, Name = x.Name, TypeID = x.TypeID, Children = x.Children, Description = x.Description }).ToList();
            return Content(JsonConvert.SerializeObject(new { data = get_all(temp) }, Formatting.Indented), "application/json");
        }

        public List<ResourceModel> get_all(List<ResourceModel> list)
        {
            int z = 0;
            List<ResourceModel> lists = new List<ResourceModel>();

            if (list.Count > 0)
            {
                lists.AddRange(list);
            }

            foreach (ResourceModel x in list)
            {
                ResourceModel resourcedb = _applicationcontext.Resources.Include(y => y.Children).Where(y => y.Id == x.Id).Select(f => new ResourceModel {  Id = f.Id, Name = f.Name, TypeID = f.TypeID, Children = f.Children, Description = f.Description }).First();
                if (resourcedb.Children == null)
                {
                    z++;
                    continue;
                }
                List<ResourceModel> sub = resourcedb.Children.ToList();
                resourcedb.Children = get_all(sub);
                lists[z] = resourcedb;
                z++;
            }
            return lists;
        }


        [HttpGet]
        [Route("GetAll")]
        public ActionResult get_all([FromQuery] int TypeId, [FromQuery] int? ParentId) {
            List<ResourceModel> ResourceList = _applicationcontext.Resources.Where(x => x.TypeID == TypeId && x.parentId == ParentId).ToList();
            List<Resource_ViewModel> ResultList = new List<Resource_ViewModel>();
            foreach (var item in ResourceList)
            {
                ResultList.Add(_mapper.Map<Resource_ViewModel>(item));

            }
            return Ok(new { ResultingResources = ResultList });
        }



        // GET: api/ResourceModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceModel>> GetResourceModel(int id)
        {
            var resourceModel = await _applicationcontext.Resources.FindAsync(id);

            if (resourceModel == null)
            {
                return NotFound();
            }

            return resourceModel;
        }

        // PUT: api/ResourceModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResourceModel(int id, ResourceModel resourceModel)
        {
            if (id != resourceModel.Id)
            {
                return BadRequest();
            }

            _applicationcontext.Entry(resourceModel).State = EntityState.Modified;

            try
            {
                await _applicationcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //POST: api/ResourceModels
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ResourceModel>> PostResource(Resource_ViewModel resourcetobeadded)
        {
            var duplicateCheck = await _applicationcontext.Resources.Where(c => c.Name == resourcetobeadded.Name).FirstOrDefaultAsync();
            if (duplicateCheck == null)
            {
                try
                {
                   ResourceModel resource = new ResourceModel();
                    if (resourcetobeadded.parentId == -1)
                    {   
                       var resourceDto =_mapper.Map<Resource_DTO>(resourcetobeadded);
                        _applicationcontext.Resources.Add(_mapper.Map<ResourceModel>(resourceDto));
                        _applicationcontext.SaveChanges();
                    }
                    else
                    {
                        ResourceModel ParentResource = await _applicationcontext.Resources.Include(x => x.Children).Where(x => x.Id == resourcetobeadded.parentId).FirstAsync();
                        var resourceDto = _mapper.Map<Resource_DTO>(resourcetobeadded);
                        ParentResource.Children.Add(_mapper.Map<ResourceModel>(resourceDto));
                        _applicationcontext.SaveChanges();
                    }
                    return Ok(new { Message = "Resource Added" });
                }
                catch (Exception e)
                {
                    return BadRequest(new { Success = false, exception = e.Message });
                    throw;
                }
            }
            else
                return BadRequest(new { Success = false, message = "Duplicate value found" });

        }

        // DELETE: api/ResourceModels/5
        [HttpDelete]
        public async Task<ActionResult<ResourceModel>> DeleteResourceModel([FromQuery]int id)
        {
            var entityToDelete = await _applicationcontext.Resources.Include(resource => resource.Children).Where(resource => resource.Id == id).FirstOrDefaultAsync();
            if (entityToDelete == null)
            {
                return NotFound();
            }
            else
            {
                var ChildrenToDelete = entityToDelete.Children.FirstOrDefault();
                if (ChildrenToDelete != null)
                {
                    RecursiveDelete(entityToDelete);
                    _applicationcontext.SaveChanges();
                    return Ok("Resource Deleted");
                }
                else
                {
                    _applicationcontext.Remove(entityToDelete);
                    _applicationcontext.SaveChanges();
                    return Ok("Resource Deleted");
                }
            }
        }

        // Function to recursively delete the children of the Parent to be delete
        public bool RecursiveDelete(ResourceModel entityToDelete) {
            var resources = _applicationcontext.Resources.Include(c => c.Children).Where(c => c.Id == entityToDelete.Id).First();
            List<ResourceModel> ChildrensToDelete = resources.Children.ToList();
            try
            {
                foreach (ResourceModel resource in ChildrensToDelete)
                {
                    RecursiveDelete(resource);
                }
                _applicationcontext.Remove(entityToDelete);
                return true;
            }
            catch (Exception e) {
                return false;
                throw;
            }
        }

        private bool ResourceModelExists(int id)
        {
            return _applicationcontext.Resources.Any(e => e.Id == id);
        }
    }
}
