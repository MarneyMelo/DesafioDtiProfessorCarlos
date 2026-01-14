using System;
using System.Linq;

namespace gradeManagement.Models;

public class Student
{
    public string Name { get; set; } = string.Empty;
    public double[] Grades { get; set; } = new double[5];
    public double Attendance { get; set; }

    // Average of grades calculated using LINQ. Returns 0 if no grades.
    public double AverageGrades => Grades.Length > 0 ? Grades.Average() : 0;
}
