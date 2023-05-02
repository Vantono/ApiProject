using CourseProject.Common.DTOs.Teams;
using CourseProject.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Common.Interfaces;

public interface ITeamService
{
    Task<int> CreatTeamAsync(TeamCreate teamCreate);
    Task UpdateteamAsync(TeamUpdate updateTeam);
    Task<List<TeamGet>> GetTeamsAsync();
    Task<TeamGet> GetTeamAsync(int teamId);
    Task DeleteTeamAsync(TeamDelete teamDelete);
}
