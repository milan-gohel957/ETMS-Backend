using System.ComponentModel;

namespace ETMS.Domain.Enums;

public class Enums
{
    public enum EResponse
    {
        OK = 200,
        Created = 201,
        Accepted = 202,
        BadRequest = 400,
        Unauthorized = 401,
        PaymentRequired = 402,
        Forbidden = 403,
        NotFound = 404,
        InternalServerError = 500
    }

    public enum ETypeRequestResponse
    {
        Default = 0,
        ResponseWithData = 1
    }

    public enum StatusEnum
    {
        Pending = 1,
        Completed = 2
    }
        
    public enum RoleEnum
    {
        Admin = 1,
        [Description("Program Manager")]
        ProgramManager = 2,
        [Description("Project Manager")]
        ProjectManager = 3,
        [Description("Team Lead")]
        TeamLead = 4,
        [Description("Senior Developer")]
        SeniorDeveloper = 5,
        [Description("Junior Developer")]
        JuniorDeveloper = 6,
        [Description("User")]
        User = 7,
    }

    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null) return string.Empty;

        var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        return attr?.Description ?? value.ToString();
    }
}
