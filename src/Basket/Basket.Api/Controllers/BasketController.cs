﻿using Basket.Api.Entities;
using Basket.Api.Repositories;
using Basket.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]/{userName}")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ICouponGrpcService _couponGrpcService;

        public BasketController(IBasketRepository basketRepository, ICouponGrpcService couponGrpcService)
        {
            _basketRepository = basketRepository;
            _couponGrpcService = couponGrpcService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBasket(string userName)
        {
            ShoppingCart cart = await _basketRepository.GetBasket(userName);
            return Ok(cart);
        }

        [HttpPost]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddBasketItem(string userName, ShoppingCartItem cartItem)
        {
            cartItem = await _couponGrpcService.GetPrice(cartItem);
            long result = await _basketRepository.UpdateShoppingCartItem(userName, cartItem);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            bool result = await _basketRepository.DeleteBasket(userName);
            return Ok(result);
        }
    }
}
