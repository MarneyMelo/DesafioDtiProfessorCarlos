using gradeManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace gradeManagement.Services;

public class GradeCalculator
{
    //Computes the average grade for each of the 5 subjects
    public double[] CalculateAveragesBySubject(List<Student> students)
    {
        double[] averages = new double[5];
        if (students.Count == 0) return averages;

        for (int i = 0; i < 5; i++)
        {
            // LINQ
            averages[i] = students.Average(a => a.Grades[i]);
        }
        return averages;
    }

    // Returns students with individual average above the overall class average
    public List<string> GetAboveAverageStudents(List<Student> students)
    {
        if (students.Count == 0) return new List<string>();

        double overallClassAverage = students.Average(a => a.AverageGrades);

        return students
            .Where(a => a.AverageGrades > overallClassAverage)
            .Select(a => a.Name)
            .ToList();
    }

    // Returns students with attendance below 75%
    public List<string> GetLowAttendanceStudents(List<Student> students)
    {
        return students
            .Where(a => a.Attendance < 75)
            .Select(a => a.Name)
            .ToList();
    }
}