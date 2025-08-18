namespace ETMS.Service.DTOs;

public class AddUsersToProjectDto
{
    public List<UserRoleDto> UserRoles { get; set; } = null!;
}

public class UserRoleDto
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}