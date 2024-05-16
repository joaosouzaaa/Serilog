using Microsoft.AspNetCore.Mvc;
using SerilogAPI.DataTransferObjects.Person;
using SerilogAPI.Interfaces.Services;
using SerilogAPI.Settings.NotificationSettings;

namespace SerilogAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PersonController(IPersonService personService) : ControllerBase
{
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task AddAsync([FromBody] PersonSave personSave) =>
        personService.AddAsync(personSave);

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task UpdateAsync([FromBody] PersonUpdate personUpdate) =>
        personService.UpdateAsync(personUpdate);

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task DeleteAsync([FromQuery] int id) =>
        personService.DeleteAsync(id);

    [HttpGet("get-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<PersonResponse?> GetByIdAsync([FromQuery] int id) =>
        personService.GetByIdAsync(id);

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PersonResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<List<PersonResponse>> GetAllAsync() =>
        personService.GetAllAsync();
}
