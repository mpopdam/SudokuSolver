using FluentAssertions;

namespace SudokuSolver.Tests;

public class SudokuSolverTests
{
    [Fact]
    public void When_creating_a_clump_from_a_collection_of_values_it_should_initialize_the_cells_()
    {
        // Arrange
        var clump = new Clump(new[]
        {
            new Cell(1),
            new Cell(2, 5),
            new Cell(3),
            new Cell(4),
            new Cell(5),
            new Cell(6),
            new Cell(7),
            new Cell(8),
            new Cell(9),
        });

        // Assert
        clump.Cells[1].Value.Should().Be(5);
        
        clump.Cells[2].Value.Should().Be(0);
        clump.Cells[2].AvailableOptions.Should().BeEquivalentTo(new []{1,2,3,4,6,7,8,9});
    }
}