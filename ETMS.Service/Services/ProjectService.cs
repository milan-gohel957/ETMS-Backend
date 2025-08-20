using ETMS.Service.DTOs;
using ETMS.Domain.Entities;
using ETMS.Repository.Repositories.Interfaces;
using ETMS.Service.Services.Interfaces;
using AutoMapper;
using ETMS.Service.Exceptions;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Service.Services;

public class ProjectService(IUnitOfWork unitOfWork, IMapper mapper, IPermissionService permissionService) : IProjectService
{
    private IGenericRepository<Project> _projectRepository = unitOfWork.GetRepository<Project>();

    private IGenericRepository<UserProjectRole> _userProjectRoleRepository = unitOfWork.GetRepository<UserProjectRole>();
    private IGenericRepository<User> _userRepository = unitOfWork.GetRepository<User>();

    private readonly IGenericRepository<Board> _boardRepository = unitOfWork.GetRepository<Board>();

    //Get user's project list just requires to be authorized
    public async Task<IEnumerable<ProjectDto>> GetUserProjectsAsync(int userId)
    {
        var projects = (await _userProjectRoleRepository.GetAllWithIncludesAsync(upr => upr.UserId == userId, includes: upr => upr.Project)).Select(p => p.Project);
        return mapper.Map<IEnumerable<ProjectDto>>(projects);
    }

    public async Task<ProjectDto?> GetProjectByIdAsync(int projectId, int userId)
    {
        Project? project = await _projectRepository.FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null) throw new ResponseException(EResponse.NotFound, "Project Not Found");

        bool hasPermission = await permissionService.HasPermissionAsync(userId, projectId, "Projects.Get");
        if (!hasPermission)
            throw new ResponseException(EResponse.Forbidden, "User Can't get this project.");

        return mapper.Map<ProjectDto>(project);
    }

    public async Task DeleteProjectAsync(int projectId, int userId)
    {
        bool isProjectExists = await _projectRepository.ExistsAsync(projectId);
        if (!isProjectExists) throw new ResponseException(EResponse.NotFound, "Project Not Found");

        bool hasPermission = await permissionService.HasPermissionAsync(userId, projectId, "Projects.Delete");
        if (!hasPermission)
            throw new ResponseException(EResponse.Forbidden, "User Can't get this project.");

        await _projectRepository.SoftDeleteByIdAsync(projectId);

        IEnumerable<UserProjectRole> userProjectRoles = await _userProjectRoleRepository.GetAllAsync(upr => upr.ProjectId == projectId);
        _userProjectRoleRepository.SoftDeleteRange(userProjectRoles);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateProjectAsync(int projectId, UpdateProjectDto projectDto)
    {
        Project? dbProject = await _projectRepository.GetByIdAsync(projectId) ?? throw new ResponseException(EResponse.NotFound, "Project Not Found");

        bool hasPermission = await permissionService.HasPermissionAsync(projectDto.CreatedByUserId, projectId, "Projects.Delete");
        if (!hasPermission)
            throw new ResponseException(EResponse.Forbidden, "User Can't get this project.");

        mapper.Map(projectDto, dbProject);

        _projectRepository.Update(dbProject);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserDto>> GetProjectMembers(int projectId, int userId)
    {
        Project? project = await _projectRepository.GetByIdAsync(projectId) ?? throw new ResponseException(EResponse.NotFound, $"Project with Id {projectId} not found.");

        bool hasPermission = await permissionService.HasPermissionAsync(userId, projectId, "Projects.Read");
        if (!hasPermission)
            throw new ResponseException(EResponse.Forbidden, "User Can't get this project.");

        IEnumerable<User?> projectMembers = (await _userProjectRoleRepository.GetAllWithIncludesAsync(upr => upr.ProjectId == projectId, includes: upr => upr.User!)).Select(upr => upr.User);

        return mapper.Map<IEnumerable<UserDto>>(projectMembers);
    }

    public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto projectDto)
    {
        Project addedProject = await _projectRepository.AddAsync(mapper.Map<Project>(projectDto));
        await unitOfWork.SaveChangesAsync();

        //Project Creator is adming by default 
        await _userProjectRoleRepository.AddAsync(
            new()
            {
                ProjectId = addedProject.Id,
                UserId = projectDto.CreatedByUserId,
                RoleId = (int)RoleEnum.Admin
            });
        if (projectDto.IsAddDefaultBoards)
        {
            await _boardRepository.AddRangeAsync(GetDefaultBoards(projectDto.CreatedByUserId, addedProject.Id));
        }
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<ProjectDto>(addedProject);
    }
    public async Task AddUsersToProject(int projectId, AddUsersToProjectDto addUsersToProjectDto)
    {
        // TODO: Check that if user has permission to add the users in the project 

        // First, check if the project exists.
        Project project = await _projectRepository.GetByIdAsync(projectId)
            ?? throw new ResponseException(EResponse.NotFound, $"Project With Id {projectId} not found");

        //Check all user exists 
        var newUserIdsToAdd = addUsersToProjectDto.UserRoles.Select(u => u.UserId);
        bool allUsersExist = newUserIdsToAdd.All(id => _userRepository.Table.Any(u => u.Id == id));
        if (!allUsersExist)
            throw new ResponseException(EResponse.NotFound, "Some of the Users Doesn't exists");

        // Get existing users for the project to avoid duplicates.
        var existingUserIds = (await _userProjectRoleRepository
            .GetAllAsync(upr => upr.ProjectId == projectId))
            .Select(upr => upr.UserId);

        // Filter out users who are already in the project.
        var newUsersToAdd = addUsersToProjectDto.UserRoles
            .Where(ur => !existingUserIds.Contains(ur.UserId))
            .Select(ur => new UserProjectRole()
            {
                RoleId = ur.RoleId,
                UserId = ur.UserId,
                ProjectId = projectId
            });

        if (newUsersToAdd.Any())
        {
            await _userProjectRoleRepository.AddRangeAsync(newUsersToAdd);
            await unitOfWork.SaveChangesAsync();
        }
    }


    public static IEnumerable<Board> GetDefaultBoards(int createdbyUserId, int projectId)
    {
        var now = DateTime.UtcNow;
        return new List<Board>
            {
                new Board
                {
                    Name = "To Do",
                    Description = "Tasks yet to be started",
                    ColorCode = "#129dfd", // Blue
                    CreatedAt = now,
                    UpdatedAt = now,
                    CreatedByUserId = createdbyUserId,
                    ProjectId = projectId
                },
                new Board
                {
                    Name = "In Progress",
                    Description = "Tasks being worked on",
                    ColorCode = "#f5a623", // Orange-like
                    CreatedAt = now,
                    UpdatedAt = now,
                    CreatedByUserId = createdbyUserId,
                    ProjectId = projectId
                },
                new Board
                {
                    ProjectId = projectId,
                    Name = "In Review",
                    Description = "Tasks under review",
                    ColorCode = "#a259ff", // Purple
                    CreatedAt = now,
                    UpdatedAt = now,
                    CreatedByUserId = createdbyUserId
                },
                new Board
                {
                    ProjectId = projectId,
                    Name = "Completed",
                    Description = "Tasks that are done",
                    ColorCode = "#00c48c", // Green
                    CreatedAt = now,
                    UpdatedAt = now,
                    CreatedByUserId = createdbyUserId
                },
                new Board
                {
                    ProjectId = projectId,
                    Name = "Backlog",
                    Description = "Future or deferred tasks",
                    ColorCode = "#eb5757", // Red
                    CreatedAt = now,
                    UpdatedAt = now,
                    CreatedByUserId = createdbyUserId
                }
            };

    }
}
