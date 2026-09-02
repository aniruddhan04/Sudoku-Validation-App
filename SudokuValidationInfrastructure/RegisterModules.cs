using Microsoft.Extensions.DependencyInjection;
using SudokuValidationCore.Interfaces;
using SudokuValidationInfrastructure.Services;

namespace SudokuValidationInfrastructure;

public static class RegisterModules
{
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<ISudokuValidatorService, SudokuValidationService>();
    }
}
