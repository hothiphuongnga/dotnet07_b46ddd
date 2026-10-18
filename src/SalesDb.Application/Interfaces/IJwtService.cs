namespace SalesDb.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(int employeeId, string name, string role);
}