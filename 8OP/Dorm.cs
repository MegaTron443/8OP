public class Dorm
{
    public string Name { get; private set; }
    private readonly List<Room> _rooms = new List<Room>();
    public int TotalSize => _rooms.Sum(r => r.Size);

    public string Info { get => $"{Name}, Size: {TotalSize}";}
    public Dorm(string name)
    {
        Name = name;
    }

    public List<Room> GetAllRooms() => _rooms;

    public void AddRoom(Room room)
    {
        if (room != null && !_rooms.Contains(room))
        {
            _rooms.Add(room);
        }
    }

    public void AddRoom(string name, int size) => AddRoom(new Room(name, size));

    public List<Student> SearchAllResidents(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<Student>();
        return _rooms.SelectMany(room => room.Search(query)).ToList();
    }

    public void RemoveRoom(int index)
    {
        int count = 0;
        foreach(Room r in _rooms)
        {
            if(count == index)
            {
                _rooms.Remove(r);
                break;
            }
            count++;
        }
    }

    public void RemoveStudent(Student student)
    {
        foreach(Room r in _rooms)
        {
            r.RemoveStudent(student);
        }
    }

    public void AddStudent(Student student, int index)
    {
        int count = 0;
        foreach (Room r in _rooms)
        {
            if(count == index)
            {
                r.AddStudent(student);
                break;
            }
            count++;
        }
    }

    public void ModifyName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
    }
}