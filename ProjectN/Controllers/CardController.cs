using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectN.Models;
using ProjectN.Parameter;
using ProjectN.Repository;

namespace ProjectN.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        /// <summary>
        /// 卡片資料操作
        /// </summary>
        private readonly CardRepository _cardRepository;

        /// <summary>
        /// 建構式
        /// </summary>
        public CardController()
        {
            this._cardRepository = new CardRepository();
        }

        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Card> GetList()
        {
            return this._cardRepository.GetList();
        }

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Card Get([FromRoute] int id)
        {
            var result = this._cardRepository.Get(id);
            if (result is null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return result;
        }

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="parameter">卡片參數</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Insert([FromBody] CardParameter parameter)
        {
            var result = this._cardRepository.Create(parameter);
            if (result > 0)
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
            var targetCard = this._cardRepository.Get(id);
            if (targetCard is null)
            {
                return NotFound();
            }

            var isUpdateSuccess = this._cardRepository.Update(id, parameter);
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
        public IActionResult Delete([FromRoute] int id)
        {
            this._cardRepository.Delete(id);
            return Ok();
        }
    }
}
