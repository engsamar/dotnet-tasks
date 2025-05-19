namespace Task03;

class Program
{
    class Student
    {
        public int studentId;
        public string name;
        int age;
        List<Course> courses;

        public Student(int studentId, string name, int age)
        {
            this.studentId = studentId;
            this.name = name;
            this.age = age;
            courses = new();
        }

        public bool Enroll(Course course)
        {
            this.courses.Add(course);
            return true;
        }

        public string PrintDetails()
        {
            return $"Student {studentId} {name} {age}";
        }
    }

    class Instructor
    {
        public int instructorId;
        string name;
        string specialization;

        public Instructor(int instructorId, string name, string specialization)
        {
            this.instructorId = instructorId;
            this.name = name;
            this.specialization = specialization;
        }

        public string PrintDetails()
        {
            return $"Instructor {instructorId} {name} {specialization}";
        }
    }

    class Course
    {
        public int courseId;
        string title;
        Instructor instructor;

        public Course(int courseId, string title, Instructor instructor)
        {
            this.courseId = courseId;
            this.title = title;
            this.instructor = instructor;
        }

        public string PrintDetails()
        {
            return $"Course {courseId} {title} {instructor.PrintDetails()}";
        }
    }

    class StudentManager
    {
        public List<Student> students;
        public List<Course> courses;
        public List<Instructor> instructors;

        public StudentManager()
        {
            this.students = new();
            this.courses = new();
            this.instructors = new();
        }

        public bool AddStudent(Student student)
        {
            this.students.Add(student);
            return true;
        }

        public bool AddCourse(Course course)
        {
            if (courses.Any(c => c.courseId == course.courseId))
                return false;
            this.courses.Add(course);
            return true;
        }

        public bool AddInstructor(Instructor instructor)
        {
            if (instructors.Any(i => i.instructorId == instructor.instructorId))
                return false;
            this.instructors.Add(instructor);
            return true;
        }

        public Student? FindStudent(int studentId)
        {
            return this.students.FirstOrDefault(s => s.studentId == studentId);
        }
        
       
        public Course? FindCourse(int courseId)
        {
            return this.courses.FirstOrDefault(c => c.courseId == courseId);
        }

        public Instructor? FindInstructor(int instructorId)
        {
            return this.instructors.FirstOrDefault(i => i.instructorId == instructorId);
        }

        public bool EnrollStudentInCourse(int studentId, int courseId)
        {
            var student = FindStudent(studentId);
            var course = FindCourse(courseId);

            if (student != null && course != null)
            {
                student.Enroll(course);
                return true;
            }
            return false;
        }
    }

    static void Main(string[] args)
    {
        StudentManager manager = new();
        bool close = true;
        List<string> menu = new List<string>();
        Console.WriteLine(" ***--- Student Management System ---***");
        menu.Add(" 1 - Add student");
        menu.Add(" 2 - Add instructor");
        menu.Add(" 3 - Add course");
        menu.Add(" 4 - Enroll Student in Course");
        menu.Add(" 5 - Show All Students");
        menu.Add(" 6 - Show All Courses");
        menu.Add(" 7 - Show All Instructors");
        menu.Add(" 8 - Find Instructor by ID");
        menu.Add(" 9 - Find Student by ID");
        menu.Add(" 10 - Find Course by ID");
        menu.Add(" 11 - Check if Student is Enrolled in Course");
        menu.Add(" 12 - Get Instructor by Course Name");
        menu.Add(" 0 - Quit");

        do
        {
            Console.WriteLine("---- Please choose one options from the menu ----");
            for (int j = 0; j < menu.Count; j++)
            {
                Console.WriteLine(menu[j]);
            }

            Console.WriteLine("Enter your choice: ");
            string? choice = Console.ReadLine().Trim();

            switch (choice)
            {
                case "1":
                    AddStudent(manager);
                    break;
                case "2":
                    AddInstructor(manager);
                    break;
                case "3":
                    AddCourse(manager);
                    break;
                case "4":
                    EnrollStudent(manager);
                    break;
                case "5":
                    ShowAllStudents(manager);
                    break;
                case "6":
                    ShowAllCourses(manager);
                    break;
                case "7":
                    ShowAllInstructors(manager);
                    break;
                case "8":
                    FindInstructor(manager);
                    break;
                case "9":
                    FindStudent(manager);
                    break;
                case "10":
                    FindCourse(manager);
                    break;
                case "11":
                    CheckEnrollment(manager);
                    break;
                case "12":
                    GetInstructorByCourse(manager);
                    break;
                case "0":
                    close = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;

            }
        } while (close);
        
    }
    
    static void AddStudent(StudentManager manager)
    {
        Console.Write("Enter Student Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Student Age: ");
        string age = Console.ReadLine();
        Student student = new Student(manager.students.Count+1, name, Convert.ToInt32(age) );
        manager.AddStudent(student );
        
    }

    static void AddCourse(StudentManager manager)
    {
        Console.Write("Enter course title: ");
        string? title = Console.ReadLine();
        Console.Write("Enter instructor id: ");
        string instructorId = Console.ReadLine();
        //find instructor by name
        Instructor? instructor = manager.FindInstructor(Convert.ToInt32(instructorId));
        Course course = new Course(manager.courses.Count+1, title, instructor );
        manager.AddCourse(course );
        
    }
    
    static void AddInstructor(StudentManager manager)
    {
        Console.Write("Enter instructor name: ");
        string? title = Console.ReadLine();
        Console.Write("Enter instructor specialization: ");
        string specialization = Console.ReadLine();

        Instructor instructor = new Instructor(manager.instructors.Count+1, title, specialization );
        manager.AddInstructor(instructor );
        
    }
    
    //Enroll
    static void EnrollStudent(StudentManager manager)
    {
        Console.Write("Enter student id: ");
        string studentId = Console.ReadLine();
        Console.Write("Enter course id: ");
        string courseId = Console.ReadLine();
        manager.EnrollStudentInCourse(Convert.ToInt32(studentId), Convert.ToInt32(courseId));
        
    }
    
    static void ShowAllStudents(StudentManager manager)
    {
        for (int i=0;i< manager.students.Count ;i++)
        {
            Console.WriteLine(manager.students[i].PrintDetails());
        }
       
    }
    
    static void ShowAllCourses(StudentManager manager)
    {
        for (int i=0;i< manager.courses.Count ;i++)
        {
            Console.WriteLine(manager.courses[i].PrintDetails());
        }
        
    }
    
    static void ShowAllInstructors(StudentManager manager)
    {
        for (int i=0;i< manager.instructors.Count ;i++)
        {
            Console.WriteLine(manager.instructors[i].PrintDetails());
        }
        
    }
    
    static void FindStudent(StudentManager manager)
    {
        Console.Write("Enter student id: ");
        string studentId = Console.ReadLine();
        Student? student = manager.FindStudent(Convert.ToInt32(studentId));
        if (student != null)
            Console.WriteLine(student.PrintDetails());
        else
            Console.WriteLine("Student not found");
        
    }
    static void FindCourse(StudentManager manager)
    {
        Console.Write("Enter course id: ");
        string courseId = Console.ReadLine();
        Course? course = manager.FindCourse(Convert.ToInt32(courseId));
        if (course != null)
            Console.WriteLine(course.PrintDetails());
        else
            Console.WriteLine("Course not found");
        
    }
    static void FindInstructor(StudentManager manager)
    {
        Console.Write("Enter instructor id: ");
        string instructorId = Console.ReadLine();
        Instructor? instructor = manager.FindInstructor(Convert.ToInt32(instructorId));
        if (instructor != null)
            Console.WriteLine(instructor.PrintDetails());
        else
            Console.WriteLine("Instructor not found");
        
    }
    
    static void CheckEnrollment(StudentManager manager)
    {
        Console.Write("Enter student id: ");
        string studentId = Console.ReadLine();
        Console.Write("Enter course id: ");
        string courseId = Console.ReadLine();
        Student? student = manager.FindStudent(Convert.ToInt32(studentId));
        Course? course = manager.FindCourse(Convert.ToInt32(courseId));
        if (student != null && course != null)
            Console.WriteLine(student.Enroll(course) ? "Enrolled" : "Not Enrolled");
        else
            Console.WriteLine("Student or Course not found");
        
    }
    static void GetInstructorByCourse(StudentManager manager)
    {
        Console.Write("Enter course id: ");
        string courseId = Console.ReadLine();
        Course? course = manager.FindCourse(Convert.ToInt32(courseId));
        if (course != null)
            Console.WriteLine(course.PrintDetails());
        else
            Console.WriteLine("Course not found");
        
    }
    static void GetStudentByCourse(StudentManager manager)
    {
        Console.Write("Enter course id: ");
        string courseId = Console.ReadLine();
        Course? course = manager.FindCourse(Convert.ToInt32(courseId));
        if (course != null)
            Console.WriteLine(course.PrintDetails());
        else
            Console.WriteLine("Course not found");
        
    }

    
}