using FluentAssertions;

namespace SudokuSolver.Tests;
public class SolveTests
{
    [Fact]
    public void DuplicateOptionsSolver_Should_complete_sudoku_field()
    {
        int[] sudokuField = {
  5, 3, 4, 0, 0, 8, 9, 1, 2,
  6, 7, 2, 1, 9, 5, 3, 4, 8,
  1, 9, 8, 3, 4, 2, 5, 6, 7,
  8, 5, 9, 0, 0, 1, 4, 2, 3,
  4, 2, 6, 8, 5, 3, 7, 9, 1,
  7, 1, 3, 9, 2, 4, 8, 5, 6,
  9, 6, 1, 5, 3, 7, 2, 8, 4,
  2, 8, 7, 4, 1, 9, 6, 3, 5,
  3, 4, 5, 2, 8, 6, 1, 7, 9
        };

        int[] sudokuField2 = {
  1, 2, 3, 0, 0, 6, 0, 0, 9,
  0, 0, 0, 0, 0, 0, 4, 0, 0,
  0, 0, 0, 0, 0, 0, 5, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 4, 0,
  0, 0, 0, 0, 0, 0, 0, 5, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0
        };
        
        var grid = new Grid();
        grid.FillGrid(grid.Cells, sudokuField2);
        Solve solve = new Solve();
        solve.DuplicateOptionsSolver(grid);
        grid.Cells[3].AvailableOptions.Should().BeEquivalentTo(new [] {4,5});
        grid.Cells[4].AvailableOptions.Should().BeEquivalentTo(new [] {4,5});
        grid.Cells[6].AvailableOptions.Should().BeEquivalentTo(new [] {7,8});
        grid.Cells[7].AvailableOptions.Should().BeEquivalentTo(new [] {7,8});

    }
}