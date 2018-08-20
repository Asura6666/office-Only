using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;
using webapi.Services.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Api
{
    [Route("api/[controller]")]
    public class GoodsController : Controller
    {
        /// <summary>
        ///  添加商品
        /// </summary>
        /// <param name="webapiService"></param>
        /// <param name="dto">CreateGoodDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateGood([FromServices]IWebapiService webapiService,[FromBody]ReceiveCreateGoodDto dto)
        {
            return Json(webapiService.CreateGood(new CreateGoodDto {Name=dto.Name,Number=dto.Number,Description=dto.Description,Price=dto.Price,TagsId=dto.TagsId }));
        }

        /// <summary>
        /// 删除商品(非物理删除)
        /// </summary>
        /// <param name="webapiService"></param>
        /// <param name="dto">DeleteGoodDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteGood([FromServices]IWebapiService webapiService, [FromBody]DeleteGoodDto dto)
        {
            return Json(webapiService.DeleteGood(dto));
        }

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="webapiService"></param>
        /// <param name="dto">AlterGoodDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Alter")]
        public IActionResult AlterGood([FromServices]IWebapiService webapiService, [FromBody]AlterGoodDto dto)
        {
            return Json(webapiService.AlterGood(dto));
        }

        /// <summary>
        /// 上架商品
        /// </summary>
        /// <param name="webapiService"></param>
        /// <param name="dto">OnlineGoodDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("OnlineGood")]
        public IActionResult OnlineGood([FromServices]IWebapiService webapiService, [FromBody]OnlineGoodDto dto)
        {
            return Json(webapiService.OnlineGood(dto));
        }

        /// <summary>
        /// 下架商品
        /// </summary>
        /// <param name="webapiService"></param>
        /// <param name="dto">OnlineGoodDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("OfflineGood")]
        public IActionResult OfflineGood([FromServices]IWebapiService webapiService, [FromBody]OnlineGoodDto dto)
        {
            return Json(webapiService.OfflineGood(dto));
        }
        /// <summary>
        /// 显示商品详情
        /// </summary>
        /// <param name="webapiService"></param>
        /// <param name="dto">ShowGoodDetailDto</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ShowDetail")]
        public IActionResult ShowGoodDetail([FromServices]IWebapiService webapiService, [FromQuery]ReceiveShowGoodDetailDto dto)
        {
            return Json(webapiService.ShowGoodDetail(new ShowGoodDetailDto { Id = dto.Id }));
        }
        /// <summary>
        /// 列出指定商品
        /// </summary>
        /// <param name="webapiService"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListGood")]
        public IActionResult ListGood([FromServices]IWebapiService webapiService, ListReceiveDto dto)
        {
            return Json(webapiService.ListGood(dto));
        } 

    }
}
