using SudokuValidationCore.Interfaces;

namespace SudokuValidationInfrastructure.Services;

public class SudokuValidationService : ISudokuValidatorService
{
    public SudokuValidationService() { }
    public async Task<bool> IsValidSudoku(string[][] board)
    {
        string[][] sBoard = board, removedDots = new string[9][];

        HashSet<string> uniqueRow = [], colNonRepeatDigits = [];

        List<int> colDigitsCount = [.. Enumerable.Repeat(0, 9)], colNonRepeatDigitsCount = [.. Enumerable.Repeat(0, 9)], uniqueRowCount = [];

        bool rowValid = false, colValid = false, subGridValid = true;

        for (int col = 0; col < 9; col++)
        {
            for (int row = 0; row < 9; row++)
            {
                if (sBoard[row][col] != ".")
                {
                    colDigitsCount[col] += 1;
                }
            }
        }

        for (int col = 0; col < 9; col++)
        {
            for (int row = 0; row < 9; row++)
            {
                if (sBoard[row][col] != "." && colNonRepeatDigits.Add(sBoard[row][col]))
                {
                    colNonRepeatDigitsCount[col] += 1;
                }
            }
            colNonRepeatDigits.Clear();
        }

        for (int col = 0; col < 9; col++)
        {
            for (int row = 0; row < 9; row++)
            {
                if ("123456789.".Contains(sBoard[row][col])
                    && colDigitsCount[col] == colNonRepeatDigitsCount[col])
                {
                    colValid = true;
                }
                else
                {
                    colValid = false;
                    break;
                }
            }
        }

        for (int row = 0; row < 9; row++)
        {
            removedDots[row] = sBoard[row].Where(cell => cell != ".").ToArray();
        }

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < removedDots[row].Length; col++)
            {
                uniqueRow.Add(removedDots[row][col]);
            }
            uniqueRowCount.Add(uniqueRow.Count);

            uniqueRow.Clear();
        }

        for (int row = 0; row < removedDots.Length; row++)
        {
            for (int col = 0; col < removedDots[row].Length; col++)
            {
                if ("123456789.".Contains(sBoard[row][col])
                    && removedDots[row].Length == uniqueRowCount[row])
                {
                    rowValid = true;
                }
                else
                {
                    rowValid = false;
                    break;
                }
            }
        }

        for (int row = 0; row < 9; row += 3)
        {
            for (int col = 0; col < 9; col += 3)
            {
                HashSet<string> subGrid = [];

                for (int subRow = row; subRow < row + 3; subRow++)
                {
                    for (int subCol = col; subCol < col + 3; subCol++)
                    {
                        if (sBoard[subRow][subCol] != "."
                            && !subGrid.Add(sBoard[subRow][subCol]))
                        {
                            subGridValid = false;
                        }
                    }
                }
            }
        }

        if (rowValid && colValid && subGridValid)
        {
            Console.WriteLine("\nOutput: true");
            return true;
        }
        else
        {
            Console.WriteLine("\nOutput: false");
            return false;
        }
    }
}
