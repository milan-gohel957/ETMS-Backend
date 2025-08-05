using ETMS.Service.DTOs;
using ETMS.Domain.Entities;
using ETMS.Repository.Repositories.Interfaces;
using ETMS.Service.Services.Interfaces;
using AutoMapper;
using ETMS.Service.Exceptions;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Service.Services;

public class ProjectService(IUnitOfWork unitOfWork, IMapper mapper) : IProjectService
{
    private IGenericRepository<Project>? _projectRepo;
    private IGenericRepository<Project> ProjectRepo => _projectRepo ??= unitOfWork.GetRepository<Project>();

    private IGenericRepository<UserProjectRole>? _userProjectRoleRepo;

    private IGenericRepository<UserProjectRole> UserProjectRoleRepo => _userProjectRoleRepo ??= unitOfWork.GetRepository<UserProjectRole>();

    private readonly IGenericRepository<Board> _boardRepository = unitOfWork.GetRepository<Board>();

    //Get user's project list just requires to be authorized
    public async Task<IEnumerable<ProjectDto>> GetUserProjectsAsync(int userId)
    {
        return mapper.Map<IEnumerable<ProjectDto>>((await UserProjectRoleRepo.GetAllWithIncludesAsync(upr => upr.UserId == userId, upr => upr.Project)).Select(p => p.Project));
    }

    public async Task<ProjectDto?> GetProjectByIdAsync(int projectId)
    {
        Project? project = await ProjectRepo.FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null) throw new ResponseException(EResponse.NotFound, "Project Not Found");

        return mapper.Map<ProjectDto>(project);
    }

    public async Task DeleteProjectAsync(int projectId)
    {
        bool isProjectExists = await ProjectRepo.ExistsAsync(projectId);
        if (!isProjectExists) throw new ResponseException(EResponse.NotFound, "Project Not Found");

        await ProjectRepo.SoftDeleteByIdAsync(projectId);

        IEnumerable<UserProjectRole> userProjectRoles = await UserProjectRoleRepo.GetAllAsync(upr => upr.ProjectId == projectId);
        UserProjectRoleRepo.SoftDeleteRange(userProjectRoles);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateProjectAsync(int id, UpdateProjectDto projectDto)
    {
        Project? dbProject = await ProjectRepo.GetByIdAsync(id) ?? throw new ResponseException(EResponse.NotFound, "Project Not Found");

        mapper.Map(projectDto, dbProject);

        ProjectRepo.Update(dbProject);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto projectDto)
    {
        Project addedProject = await ProjectRepo.AddAsync(mapper.Map<Project>(projectDto));
        await unitOfWork.SaveChangesAsync();
        //Project Creator is adming by default 
        await UserProjectRoleRepo.AddAsync(
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
