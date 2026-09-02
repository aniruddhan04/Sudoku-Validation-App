using Microsoft.AspNetCore.Mvc;
using SudokuValidationCore.Interfaces;

namespace SudokuValidationApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class SudokuValidationController : ControllerBase
{
    private readonly ILogger<SudokuValidationController> _logger;
    private readonly ISudokuValidatorService _sudokuValidatorService;

    public SudokuValidationController(ILogger<SudokuValidationController> logger,ISudokuValidatorService sudokuValidatorService)
    {
        _logger = logger;
        _sudokuValidatorService = sudokuValidatorService;
    }

    [HttpPost("ValidateSudokuBoard")]
    public async Task<bool> ValidateSudokuBoard(string[][] board)
    {
        try
        {
            var validationResult = await _sudokuValidatorService.IsValidSudoku(board);
            return validationResult;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while validating the Sudoku board.");
            return false;
        }
    }
}
