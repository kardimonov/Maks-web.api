using Microsoft.AspNetCore.Mvc;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.Orders;

namespace GolovinskyAPI.Web.Controllers
{
    /// <summary>
    /// Все что связано с оформлением заказа
    /// </summary>
    /// <returns></returns>
    [Produces("application/json")]
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository repo;

        public OrderController(IOrderRepository repository)
        {
            repo = repository;
        }
        /*
         Для создания заказов при первом сбросе позиции в корзину (сделать это можно путем нажатия на значок корзины, который имеется на картинке на главной странице сайта, 
         либо во всплывающем окне на большой картинке) создается заголовок заказа.
         Для этого используется процедура
        [dbo].[sp_AddNewOrder]
        */
        /// <summary>
        /// Создание заказа при добавлении первого товара
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/Order
        [HttpPost]
        public IActionResult Post([FromBody]NewOrderInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = repo.AddNewOrder(model);
            //if(res.Ord_No == null)
            //{
            //    return Ok(new { Message = "Не верный id пользователя", Status = false });
            //}
            return Ok(res);
        }

        /// <summary>
        /// Добавление товара в корзину
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/api/addtocart/")]
        public IActionResult AddToCart([FromBody] NewOrderItemInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var res = repo.AddItemToCart(model);
            if (res) 
            {
                return Ok(new { Message = "Товар добавлен в корзину", Result = true });
            } 
            return Ok(new { Message = "Не верные параметры. Товар не добавлне в корзину.", Result = false });
        }
        
        // изменить количество единиц в корзине
        /// <summary>
        /// Изменить количество единиц в корзине
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/api/order/changeqty/")]
        public IActionResult ChangeQty([FromBody] NewOrderItemInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var res = repo.ChangeQty(model);
            
            return Ok(new { Result = res });
        }

        /* В самом конце формирования заказа запускается процедура
          [dbo].[sp_OrderAsSMS], где пользователь в форме указывает адрес доставки 
        */
        /// <summary>
        /// Сохранение товара
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
      //  [Authorize]
        [HttpPost("/api/order/save/")]
        public IActionResult SaveOrder([FromBody] NewOrderShippingInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var res = repo.SaveOrder(model);
            
            return Ok(new { Result = res });
        }
    }
}