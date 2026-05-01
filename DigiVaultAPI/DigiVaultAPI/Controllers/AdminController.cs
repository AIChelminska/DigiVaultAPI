using DigiVaultAPI.Features.Admin.Messages.Queries;
using DigiVaultAPI.Features.Admin.Messages.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DigiVaultAPI.Features.Courses.Messages.Queries;

namespace DigiVaultAPI.Controllers;

[ApiController]
[Authorize(Roles = "Worker")]
[Route("api/[controller]")]
public class AdminController(IMediator mediator) : ControllerBase
{
    [HttpGet("users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers()
        => Ok(await mediator.Send(new GetUsersQuery()));

    [HttpPost("users/set-as-active")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SetAsActiveUser([FromBody] SetAsActiveUserCommand command)
        { await mediator.Send(command); return NoContent(); }

    [HttpPost("users/set-as-not-active")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SetAsNotActiveUser([FromBody] SetAsNotActiveCommand command)
        { await mediator.Send(command); return NoContent(); }

    [HttpGet("courses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCourses([FromQuery] GetCoursesQuery query)
    => Ok(await mediator.Send(query));    

    [HttpGet("orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrders([FromQuery] GetOrdersQuery query)
    => Ok(await mediator.Send(query));

    [HttpGet("categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesQuery query)
    => Ok(await mediator.Send(query));

    [HttpPost("categories")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    { await mediator.Send(command); return StatusCode(201); }

    [HttpPut("categories/{idCategory}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateCategory([FromRoute] int idCategory, [FromBody] UpdateCategoryCommand command)
    { command.IdCategory = idCategory; await mediator.Send(command); return NoContent(); }

    [HttpDelete("categories/{idCategory}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCategory([FromRoute] int idCategory)
    { await mediator.Send(new DeleteCategoryCommand { IdCategory = idCategory }); return NoContent(); }


    [HttpGet("users/{idUser}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserById([FromRoute] int idUser)
    => Ok(await mediator.Send(new GetUserByIdQuery { IdUser = idUser }));

    [HttpPost("user")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    { await mediator.Send(command); return StatusCode(201); }

    [HttpPut("user/{idUser}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateUser([FromRoute] int idUser, [FromBody] UpdateUserCommand command)
    { command.IdUser = idUser; await mediator.Send(command); return NoContent(); }

    [HttpDelete("reviews/{idReview}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteReview([FromRoute] int idReview)
    { await mediator.Send(new DeleteReviewCommand { IdReview = idReview }); return NoContent(); }

    [HttpGet("courses/{idCourse}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCourseById([FromRoute] int idCourse)
    => Ok(await mediator.Send(new GetCourseByIdAdminQuery { IdCourse = idCourse }));

    [HttpDelete("courses/{idCourse}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCourse([FromRoute] int idCourse)
    { await mediator.Send(new DeleteCourseCommand { IdCourse = idCourse }); return NoContent(); }

    [HttpGet("orders/{idOrder}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrderById([FromRoute] int idOrder)
    => Ok(await mediator.Send(new GetOrderByIdAdminQuery { IdOrder = idOrder }));

    [HttpDelete("orders/{idOrder}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteOrder([FromRoute] int idOrder)
    { await mediator.Send(new DeleteOrderCommand { IdOrder = idOrder }); return NoContent(); }

    [HttpPut("orders/{idOrder}/courses/{idCourse}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateOrder([FromRoute] int idOrder, [FromRoute] int idCourse)
    { await mediator.Send(new RemoveCourseFromOrder { IdOrder = idOrder, IdCourse = idCourse }); return NoContent(); }

    [HttpGet("notifications")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotifications([FromQuery] GetNotificationsAdminQuery query)
    => Ok(await mediator.Send(query));

    [HttpGet("settings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSettings()
    => Ok(await mediator.Send(new GetSettingsQuery()));

}



