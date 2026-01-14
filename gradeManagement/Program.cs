using gradeManagement.Models;
using gradeManagement.Services;
using System;
using System.Globalization; // For robust data input
using System.Text;

List<Student> studentsList = new List<Student>();
GradeCalculator calculator = new GradeCalculator();

Console.WriteLine("Enter student data in the format: \n " +
    "<Name_Student> <grade1> <grade2> <grade3> <grade4> <grade5> <Attendance%> \n" +
    "Enter '-1' to finish:");

// Read student data until the user enters '-1'
while (true)
{
    string input = Console.ReadLine();

    if (input == "-1") break;

    try
    {
        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Student student = new Student();

        student.Name = parts[0];

        // Parse grades
        for (int i = 0; i < 5; i++)
        {
            student.Grades[i] = double.Parse(parts[i + 1], CultureInfo.InvariantCulture);
        }

        // Parse attendance (remove '%')
        string cleanAttendance = parts[6].Replace("%", "");
        student.Attendance = double.Parse(cleanAttendance, CultureInfo.InvariantCulture);

        studentsList.Add(student);
    }
    catch (Exception)
    {
        Console.WriteLine("Error reading data. Try again or enter -1 to exit.");
    }
}
if (studentsList.Any()) // if (studentsList.Count > 0) works too
{
    // Console.WriteLine("Name, Average and Attendance by Student");
    foreach (var student in studentsList)
    {
        Console.WriteLine($"{student.Name} {student.AverageGrades:F0} {student.Attendance}%");
    }
    //Console.WriteLine("Class Average for Each Subject");
    var averagesClass = calculator.CalculateAveragesBySubject(studentsList);
    Console.WriteLine(string.Join(" ", averagesClass.Select(m => m.ToString("F0"))));

    //Console.WriteLine("Students Above Class Average");
    var aboveAverage = calculator.GetAboveAverageStudents(studentsList);
    Console.WriteLine(aboveAverage.Any() ? string.Join(", ", aboveAverage) : "");

    // Console.WriteLine("Students with Less Than 75% Attendance");
    var lowAttendance = calculator.GetLowAttendanceStudents(studentsList);
    Console.WriteLine(lowAttendance.Any() ? string.Join(", ", lowAttendance) : "");
}
