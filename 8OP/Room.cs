public class Room
{
    public string Name { get; private set; }
    public int Size { get; private set; }
    private readonly List<Student> _residents =  new List<Student>();
    public int CurrentOccupancy => _residents.Count;
    public bool IsFull => CurrentOccupancy >= Size;

    public List<Student> GetStudents => _residents;

    public Room(string name, int size)
    {
        Name = name;
        Size = size;
    }

    public void AddStudent(Student student)
    {
        if (!IsFull && student != null && !_residents.Contains(student))
        {
            _residents.Add(student);
        }
    }

    public void RemoveStudent(Student student)
    {
        _residents.Remove(student);
    }

    public List<Student> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<Student>();

        return _residents.Where(s => 
            s.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase) || 
            s.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }
}