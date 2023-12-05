using StoreManagementApiCA.Domain.Entities;

namespace StoreManagementApiCA.Application.Common.Models;

public class IdentityAppUser
{
    public string? Id { get; init; }

    public string? Name { get; init; }
    public string? Email { get; init; }
}
