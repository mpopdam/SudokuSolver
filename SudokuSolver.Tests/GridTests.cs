using FluentAssertions;

namespace SudokuSolver.Tests;

public class GridTests
{
    [Fact]
    public void When_grid_is_initialized_grid_cells_should_contain_no_duplicate_ids()
    {
        // Arrange
        var grid = new Grid();

        // Act
        var cells = grid.Cells;

        // Assert
        for (int i = 0; i < 81; i++)
        {
            var cell = cells[i];
            cell.Id.Should().Be(i);
        }
    }

    [Fact]
    public void When_blocks_are_initialized_they_should_contain_the_correct_values()
    {
        // Given
        var grid = new Grid();

        int[][] checkBlockArray =
        {
            new[] {0, 1, 2, 9, 10, 11, 18, 19, 20},
            new[] {3, 4, 5, 12, 13, 14, 21, 22, 23},
            new[] {6, 7, 8, 15, 16, 17, 24, 25, 26},
            new[] {27, 28, 29, 36, 37, 38, 45, 46, 47},
            new[] {30, 31, 32, 39, 40, 41, 48, 49, 50},
            new[] {33, 34, 35, 42, 43, 44, 51, 52, 53},
            new[] {54, 55, 56, 63, 64, 65, 72, 73, 74},
            new[] {57, 58, 59, 66, 67, 68, 75, 76, 77},
            new[] {60, 61, 62, 69, 70, 71, 78, 79, 80},
        };

        // When
        var cells = grid.Cells;
        List<Clump> clumps = new List<Clump>();

        for (int i = 0; i < 9; i++)
        {
            clumps.Add(new Block(i, cells));
        }

        // Then
        for (int i = 0; i < clumps.Count; i++)
        {
            var cellsToCheck = clumps[i].Cells;
            foreach (var cell in cellsToCheck)
            {
                checkBlockArray[i].Should().Contain(cell.Id);
            }
        }
    }

    [Fact]
    public void When_the_sudoku_is_done_it_should_have_filled_all_possible_cells()
    {
        // Given
        var grid = new Grid();
        int[] sudokuField =
        {
            5, 3, 0, 0, 0, 8, 9, 0, 2,
            6, 0, 2, 1, 0, 5, 0, 4, 8,
            0, 9, 0, 3, 4, 2, 5, 0, 0,
            8, 0, 9, 0, 0, 1, 0, 2, 3,
            0, 2, 0, 8, 5, 3, 7, 0, 1,
            0, 1, 3, 0, 2, 4, 0, 5, 6,
            9, 6, 0, 5, 0, 0, 2, 8, 0,
            2, 8, 0, 4, 0, 9, 6, 0, 0,
            3, 0, 0, 0, 8, 6, 0, 7, 9
        };

        // When
        grid.FillGrid(sudokuField);

        // Then
        foreach (var cell in grid.Cells)
        {
            if (cell.Value == 0)
            {
                cell.AvailableOptions.Should().NotHaveCount(1);
            }
        }
    }

    [Fact]
    public void Clumps_should_never_have_one_empty_cell()
    {
        // Given
        var grid = new Grid();
        int[] sudokuField =
        {
            5, 3, 0, 0, 0, 8, 9, 0, 2,
            6, 0, 2, 1, 0, 5, 0, 4, 8,
            0, 9, 0, 3, 4, 2, 5, 0, 0,
            8, 0, 9, 0, 0, 1, 0, 2, 3,
            0, 2, 0, 8, 5, 3, 7, 0, 1,
            0, 1, 3, 0, 2, 4, 0, 5, 6,
            9, 6, 0, 5, 0, 0, 2, 8, 0,
            2, 8, 0, 4, 0, 9, 6, 0, 0,
            3, 0, 0, 0, 8, 6, 0, 7, 9
        };

        // When
        grid.FillGrid(sudokuField);

        // Then
        foreach (Clump clump in grid.Clumps)
        {
            int AmtOfEmptyCells = 0;
            foreach (Cell cell in clump.Cells)
            {
                if (cell.IsEmpty)
                {
                    AmtOfEmptyCells++;
                }
            }

            AmtOfEmptyCells.Should().NotBe(1);
        }
    }

    [Fact]
    public void the_rows_should_contain_correct_cells()
    {
        {
            // Given
            var grid = new Grid();

            int[][] checkRowArray =
            {
                new[] {0, 1, 2, 3, 4, 5, 6, 7, 8},
                new[] {9, 10, 11, 12, 13, 14, 15, 16, 17},
                new[] {18, 19, 20, 21, 22, 23, 24, 25, 26},
                new[] {27, 28, 29, 30, 31, 32, 33, 34, 35},
                new[] {36, 37, 38, 39, 40, 41, 42, 43, 44},
                new[] {45, 46, 47, 48, 49, 50, 51, 52, 53},
                new[] {54, 55, 56, 57, 58, 59, 60, 61, 62},
                new[] {63, 64, 65, 66, 67, 68, 69, 70, 71},
                new[] {72, 73, 74, 75, 76, 77, 78, 79, 80},
            };

            // When
            var cells = grid.Cells;
            List<Clump> clumps = new List<Clump>();

            for (int i = 0; i < 9; i++)
            {
                clumps.Add(new Row(i, cells));
            }

            // Then
            for (int i = 0; i < clumps.Count; i++)
            {
                var cellsToCheck = clumps[i].Cells;
                foreach (var cell in cellsToCheck)
                {
                    checkRowArray[i].Should().Contain(cell.Id);
                }
            }
        }
    }

    [Fact]
    public void the_columns_should_contain_correct_cells()
    {
        // Given
        var grid = new Grid();

        int[][] checkColumnArray =
        {
            new[] {0, 9, 18, 27, 36, 45, 54, 63, 72},
            new[] {1, 10, 19, 28, 37, 46, 55, 64, 73},
            new[] {2, 11, 20, 29, 38, 47, 56, 65, 74},
            new[] {3, 12, 21, 30, 39, 48, 57, 66, 75},
            new[] {4, 13, 22, 31, 40, 49, 58, 67, 76},
            new[] {5, 14, 23, 32, 41, 50, 59, 68, 77},
            new[] {6, 15, 24, 33, 42, 51, 60, 69, 78},
            new[] {7, 16, 25, 34, 43, 52, 61, 70, 79},
            new[] {8, 17, 26, 35, 44, 53, 62, 71, 80},
        };

        // When
        var cells = grid.Cells;
        List<Clump> clumps = new List<Clump>();

        for (int i = 0; i < 9; i++)
        {
            clumps.Add(new Column(i, cells));
        }

        // Then
        for (int i = 0; i < clumps.Count; i++)
        {
            var cellsToCheck = clumps[i].Cells;
            foreach (var cell in cellsToCheck)
            {
                checkColumnArray[i].Should().Contain(cell.Id);
            }
        }
    }
}