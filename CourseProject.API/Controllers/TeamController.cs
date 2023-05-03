using CourseProject.Common.DTOs.Teams;
using CourseProject.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.API.Controllers;
[ApiController]
[Route("[controller]")]
public class TeamController : ControllerBase 
{
    private ITeamService TeamService { get; }
    public TeamController(ITeamService teamService)
    {
        TeamService = teamService;
    }
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateTeam(TeamCreate teamCreate)
    {
        var id = await TeamService.CreatTeamAsync(teamCreate);
        return Ok(id);
    }
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateTeam(TeamUpdate teamUpdate)
    {
        await TeamService.UpdateteamAsync(teamUpdate);
        return Ok();
    }
    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteTeam(TeamDelete teamDelete)
    {
        await TeamService.DeleteTeamAsync(teamDelete);
        return Ok();
    }
    [HttpGet]
    [Route("Get/{Id}")]
    public async Task<IActionResult> GetTeam(int Id)
    {
        var team = await TeamService.GetTeamAsync(Id);
        return Ok(team);
    }
    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetTeams(TeamGet teamGet)
    {
        var teams = await TeamService.GetTeamsAsync();
        return Ok(teams);
    }
}
