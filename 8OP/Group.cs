public class Group
{
    public string Name {get; private set;}
    private readonly List<Student> _students = new List<Student>();
    public int Amount => _students.Count;
    
    public string Info {get => $"{Name}, {Amount}";}

    public List<Student> GetStudents => _students;

    public Group(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
    }

    public void ModifyName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
    }

    public void AddStudent(Student student)
    {
        if (student != null && !_students.Contains(student))
        {
            _students.Add(student);
        }
    }

    public void RemoveStudent(int index)
    {   
        int count = 0;
        foreach (Student s in _students)
        {
            if(count == index)
            {
                _students.Remove(s);
                break;
            }
            count++;
        }
    }

    public void RemoveStudent(Student student)
    {
        _students.Remove(student);
    }

    public void RemoveStudent(string firstName, string lastName)
    {
        Student? studentToRemove = _students.FirstOrDefault(s => 
            s.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) && 
            s.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

        if (studentToRemove != null)
        {
            _students.Remove(studentToRemove);
        }
    }

    public List<Student> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<Student>();

        return _students.Where(s => 
            s.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase) || 
            s.LastName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            s.FullName.Contains(query, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }

}