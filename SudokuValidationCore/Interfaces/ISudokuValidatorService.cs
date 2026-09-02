namespace SudokuValidationCore.Interfaces;

public interface ISudokuValidatorService
{
    Task<bool> IsValidSudoku(string[][] board);
}
