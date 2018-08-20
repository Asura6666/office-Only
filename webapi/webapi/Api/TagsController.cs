using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.Services;
using webapi.Services.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Api
{
    [Route("api/[controller]")]
    public class TagsController : Controller
    {
        /// <summary>
        /// 创建tag
        /// </summary>
        /// <param name="webapiService"></param>
        /// <param name="dto">CreateTagDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateTag([FromServices]IWebapiService webapiService, [FromBody]CreateTagDto dto)
        {
            return Json(webapiService.CreateTag(dto));
        }

        /// <summary>
        /// 删除tag(非物理删除)
        /// </summary>
        /// <param name="webapiService"></param>
        /// <param name="dto">DeleteTagDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteTag([FromServices]IWebapiService webapiService, [FromBody]DeleteTagDto dto)
        {
            return Json(webapiService.DeleteTag(dto));
        }

        /// <summary>
        /// 修改tag
        /// </summary>
        /// <param name="webapiService"></param>
        /// <param name="dto">AlterTagDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Alter")]
        public IActionResult AlterTag([FromServices]IWebapiService webapiService, [FromBody]AlterTagDto dto)
        {
            return Json(webapiService.AlterTag(dto));
        }

        /// <summary>
        /// 选择列出tag
        /// </summary>
        /// <param name="webapiService"></param>
        /// <param name="dto">ListTagDto</param>
        /// <returns></returns>
        [HttpGet]
        [Route("TagList")]
        public IActionResult ListTag([FromServices]IWebapiService webapiService)
        {
            return Json(webapiService.ListTag());
        }
       
    }
}
