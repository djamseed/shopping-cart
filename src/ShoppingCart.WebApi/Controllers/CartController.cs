namespace ShoppingCart.WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ShoppingCart.Application.Dtos.Requests;
    using ShoppingCart.Application.Dtos.Responses;
    using ShoppingCart.Application.Services;
    using ShoppingCart.WebApi.Validators;

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CartController : ControllerBase
    {
        private readonly IShoppingCartService service;

        public CartController(IShoppingCartService service)
        {
            this.service = service;
        }

        // POST /api/v1/cart
        [HttpPost(Name ="CreateCart")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateCartRequest request)
        {
            var validationResult = new CreateCartValidator().Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = await service.CreateCart(request);

            return CreatedAtRoute("GetCart", new{ cartId = response.CartId }, response);
        }

        // GET /api/v1/cart/{cartId}
        [HttpGet("{cartId}", Name = "GetCart")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CartResponse>> Get(Guid cartId)
        {
            var response = await service.GetCart(cartId);

            return response == null ? (ActionResult<CartResponse>)NotFound() : (ActionResult<CartResponse>)Ok(response);
        }

        // POST /api/v1/cart/{cartId}
        [HttpPost("{cartId}", Name = "AddItem")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CartResponse>> Post(Guid cartId, [FromBody] AddItemRequest request)
        {
            var validationResult = new AddItemValidator().Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var cart = await service.GetCart(cartId);

            if (cart == null)
            {
                return NotFound();
            }

            var response = await service.AddItemToCart(cartId, request);

            return CreatedAtRoute("GetCart", new { cartId }, response);
        }

        // PUT /api/v1/cart/{cartId}
        [HttpPut("{cartId}", Name ="UpdateItemQuantity")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(Guid cartId, [FromBody] AdjustQuantityRequest request)
        {
            var validationResult = new AdjustQuantityValidator().Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var cart = await service.GetCart(cartId);

            if (cart == null)
            {
                return NotFound();
            }

            await service.UpdateItemQuantity(cartId, request);

            return NoContent();
        }

        [HttpDelete("{cartId}", Name = "RemoveItem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid cartId, [FromBody] RemoveItemRequest request)
        {
            var validationResult = new RemoveItemValidator().Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var cart = await service.GetCart(cartId);

            if (cart == null)
            {
                return NotFound();
            }

            await service.RemoveItemFromCart(cartId, request);

            return NoContent();
        }


        // DELETE /api/v1/cart/{cartId}
        [HttpDelete("{cartId}/clear", Name = "ClearCart")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid cartId)
        {
            var cart = await service.GetCart(cartId);

            if (cart == null)
            {
                return NotFound();
            }

            await service.ClearCart(cartId);

            return NoContent();
        }
    }
}
