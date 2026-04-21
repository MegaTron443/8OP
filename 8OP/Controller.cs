public class Controller
{
    private readonly List<Student> _allStudents = new List<Student>();
    private readonly List<Group> _groups = new List<Group>();
    private readonly List<Dorm> _dorms = new List<Dorm>();

    public Controller()
    {
    }

    public Student RegisterNewStudent(string firstName, string lastName, int id)
    {
        if (_allStudents.Any(s => s.id == id))
        {
            throw new ArgumentException($"A student with ID {id} already exists.");
        }

        Student newStudent = new Student(firstName, lastName, id);
        _allStudents.Add(newStudent);
        return newStudent;
    }

    public List<Student> GetAllStudents() => _allStudents;

    public List<Group> GetAllGroups() => _groups;

    public List<Dorm> GetAllDorms() => _dorms;

    public Group CreateGroup(string name)
    {
        Group group = new Group(name);
        _groups.Add(group);
        return group;
    }

    public void RemoveGroup(Group group)
    {
        _groups.Remove(group);
    }

    public Dorm CreateDorm(string name)
    {
        Dorm dorm = new Dorm(name);
        _dorms.Add(dorm);
        return dorm;
    }

    public void RemoveDorm(Dorm dorm)
    {
        _dorms.Remove(dorm);
    }

    public void AssignStudentToGroup(int studentId, string groupName)
    {
        Student? student = _allStudents.FirstOrDefault(s => s.id == studentId);
        Group? group = _groups.FirstOrDefault(g => g.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));

        if (student == null) 
        {
            throw new Exception("Student not found.");
        }
        if (group == null) 
        {
            throw new Exception("Group not found.");
        }

        group.AddStudent(student);
    }

    public void AssignStudentToGroup(Student student, string groupName)
    {
        Group? group = _groups.FirstOrDefault(g => g.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));
        if (student == null) 
        {
            throw new Exception("Student not found.");
        }
        if (group == null) 
        {
            throw new Exception("Group not found.");
        }

        group.AddStudent(student);
    }

    public Group? GetGroupByName(string name)
    {
        return _groups.FirstOrDefault(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
    

    public void ExpelStudent(int studentId)
    {
        Student? student = _allStudents.FirstOrDefault(s => s.id == studentId);
        if (student == null) 
        {
            throw new Exception("Student not found.");
        }

        foreach(Group g in _groups)
        {
            g.RemoveStudent(student);
        }

        foreach(Dorm d in _dorms)
        {
            d.RemoveStudent(student);
        }

        _allStudents.Remove(student);
    }

    public Student? GetStudentById(int id)
    {
        return _allStudents.FirstOrDefault(s => s.id == id);
    }

    public Dorm? GetDormByName(string name)
    {
        return _dorms.FirstOrDefault(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }


    public void AddRoomToDorm(string name, int size, int index) => AddRoomToDorm(new Room(name, size), index);

    public void AddRoomToDorm(Room room, int index)
    {
        int count = 0;
        foreach(Dorm d in _dorms)
        {
            if(count == index)
            {
                d.AddRoom(room);
                break;
            }
            count++;
        }
    }

    public List<Student> SearchAllStudents(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<Student>();

        return _allStudents.Where(s => 
            s.FullName.Contains(query, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }

    public List<Student> SearchInGroup(string groupName, string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<Student>();

        Group? group = _groups.FirstOrDefault(g => g.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));
        
        if (group == null) return new List<Student>();

        return group.GetStudents.Where(s => 
            s.FullName.Contains(query, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }

    public List<Student> SearchInDorm(string dormName, string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<Student>();

        Dorm? dorm = _dorms.FirstOrDefault(d => d.Name.Equals(dormName, StringComparison.OrdinalIgnoreCase));
        
        if (dorm == null) return new List<Student>();

        return dorm.GetAllRooms()
            .SelectMany(room => room.GetStudents)
            .Where(s => s.FullName.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}