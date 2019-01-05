using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PtcApi.Data;
using PtcApi.Model;

namespace PtcApi.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : BaseApiController
    {
        private PtcDbContext _ptcDbContext;
        public CategoryController(PtcDbContext ptcDbContext)
        {
            _ptcDbContext = ptcDbContext;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<Category> list = new List<Category>();

            try
            {
                
                    if (_ptcDbContext.Categories.Count() > 0)
                    {
                        // NOTE: Declare 'list' outside the using to avoid 
                        // it being disposed before it is returned.
                        list = _ptcDbContext.Categories.OrderBy(p => p.CategoryName).ToList();
                        ret = StatusCode(StatusCodes.Status200OK, list);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status404NotFound,
                                       "Can't Find Categories");
                    }
                
            }
            catch (Exception ex)
            {
                ret = HandleException(ex,
                     "Exception trying to get all Categories");
            }

            return ret;
        }
    }
}
