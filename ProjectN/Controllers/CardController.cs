using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectN.Models;
using ProjectN.Parameter;
using ProjectN.Service.Dtos.Info;
using ProjectN.Service.Dtos.ResultModel;
using ProjectN.Service.Interface;

namespace ProjectN.Controllers
{
    /// <summary>
    /// 卡片管理
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICardService _cardService;

        /// <summary>
        /// 建構式
        /// </summary>
        public CardController(
            IMapper mapper,
            ICardService cardService)
        {
            this._mapper = mapper;
            this._cardService = cardService;
        }

        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public IEnumerable<CardViewModel> GetList(
            [FromQuery] CardSearchParameter parameter)
        {
            var info = this._mapper.Map<
                CardSearchParameter,
                CardSearchInfo>(parameter);

            var cards = this._cardService.GetList(info);

            var result = this._mapper.Map<
                IEnumerable<CardResultModel>,
                IEnumerable<CardViewModel>>(cards);

            return result;
        }

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <remarks>我是附加說明</remarks>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        /// <response code="200">回傳對應的卡片</response>
        /// <response code="404">找不到該編號的卡片</response>          
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CardViewModel), 200)]
        [Route("{id}")]
        public CardViewModel Get(
            [FromRoute] int id)
        {
            var card = this._cardService.Get(id);

            var result = this._mapper.Map<
                CardResultModel,
                CardViewModel>(card);

            return result;
        }

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="parameter">卡片參數</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Insert(
            [FromBody] CardParameter parameter)
        {
            // 一堆檢查
            if (parameter.Attack < 0)
            {
                return BadRequest("卡片的攻擊力不可為負數");
            }

            if (parameter.Health < 0)
            {
                return BadRequest("卡片的生命值不可為負數");
            }

            if (parameter.Cost < 0)
            {
                return BadRequest("卡片的使用成本不可為負數");
            }

            if (parameter.Description != null &&
                parameter.Description.Length > 30)
            {
                return BadRequest("卡片的敘述說明必須少於三十字");
            }

            if (string.IsNullOrWhiteSpace(parameter.Name))
            {
                return BadRequest("卡片的名稱不可為空白");
            }

            if (parameter.Name.Length > 15)
            {
                return BadRequest("卡片的名稱必須少於十五字");
            }

            // 用 AutoMapper 轉換 Model
            var info = this._mapper.Map<
                CardParameter,
                CardInfo>(parameter);

            // 呼叫 Service 層寫入資料
            var isInsertSuccess = this._cardService.Insert(info);
            if (isInsertSuccess)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        /// <summary>
        /// 更新卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="parameter">卡片參數</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] CardParameter parameter)
        {
            var targetCard = this._cardService.Get(id);
            if (targetCard is null)
            {
                return NotFound();
            }

            var info = this._mapper.Map<
                CardParameter,
                CardInfo>(parameter);

            var isUpdateSuccess = this._cardService.Update(id, info);
            if (isUpdateSuccess)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(
            [FromRoute] int id)
        {
            this._cardService.Delete(id);
            return Ok();
        }
    }
}
