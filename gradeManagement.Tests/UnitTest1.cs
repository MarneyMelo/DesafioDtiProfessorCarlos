using Xunit;
using gradeManagement.Models;
using gradeManagement.Services;
using System.Collections.Generic;

namespace gradeManagement.Tests;

public class GradeCalculatorTests
{
    private readonly GradeCalculator _calculator = new GradeCalculator();

    [Fact]
    public void CalculateAveragesBySubject_ShouldHandleMultipleStudents()
    {
        // Arrange
        var students = new List<Student>
        {
            new Student { Name = "Alice", Grades = new double[] { 10, 8, 9, 7, 6 } },
            new Student { Name = "Bob", Grades = new double[] { 6, 4, 5, 3, 2 } }
        };

        // Act - The averages should be (10+6)/2 = 8, (8+4)/2 = 6 ...
        var result = _calculator.CalculateAveragesBySubject(students);

        // Assert
        Assert.Equal(8.0, result[0]); // Average Subject 1
        Assert.Equal(6.0, result[1]); // Average Subject 2
        Assert.Equal(4.0, result[4]); // Average Subject 5
    }

    [Fact]
    public void GetLowAttendanceStudents_ShouldIdentifyStudentsBelow75Percent()
    {
        // Arrange - Pedro has exactly 75% (should not be listed), Maria has 74.9% (should be listed)
        var students = new List<Student>
        {
            new Student { Name = "Pedro", Attendance = 75.0 },
            new Student { Name = "Maria", Attendance = 74.9 }
        };

        // Act
        var result = _calculator.GetLowAttendanceStudents(students);

        // Assert
        Assert.Single(result); // Just one person on list
        Assert.Contains("Maria", result);
    }

    [Fact]
    public void GetAboveAverageStudents_ShouldReturnOnlyThoseAboveClassAverage()
    {
        // Arrange - Class average sholud be 7
        var students = new List<Student>
        {
            new Student { Name = "Star ", Grades = new double[] { 8, 8, 8, 8, 8 } }, // Average 8
            new Student { Name = "Regular", Grades = new double[] { 6, 6, 6, 6, 6 } }   // Average 6
        };

        // Act
        var result = _calculator.GetAboveAverageStudents(students);

        // Assert
        Assert.Contains("Star ", result);
        Assert.DoesNotContain("Regular", result);
    }


    [Fact]
    public void CalculateAveragesBySubject_WithEmptyList_ShouldNotThrowException()
    {
        // Arrange
        var emptyList = new List<Student>();

        // Act
        var result = _calculator.CalculateAveragesBySubject(emptyList);

        // Assert
        Assert.NotNull(result);
        Assert.All(result, average => Assert.Equal(0, average));
    }
}

