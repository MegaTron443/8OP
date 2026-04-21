public class Student
{
    public int id {get;}
    public string FirstName {get; private set;}
    public string LastName {get; private set;}
    public string FullName { get => $"{FirstName} {LastName}"; }

    public Student(string firstName, string lastName, int ID)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        FirstName = firstName;
        LastName = lastName;
        this.id = ID;
    }

    public Student(string fullName, int id) : this(
        fullName.Split(' ', 2)[0], 
        fullName.Split(' ', 2).Length > 1 ? fullName.Split(' ', 2)[1] : string.Empty,
        id
    )
    {
    }

    public void Modify(string fullName) => Modify(
        fullName.Split(' ', 2)[0], 
        fullName.Split(' ', 2).Length > 1 ? fullName.Split(' ', 2)[1] : string.Empty
        );


    public void Modify(string firstName, string lastName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        FirstName = firstName;
        LastName = lastName;
    }

    public void Modify(string name, bool first)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (first)
        {
            FirstName = name;
            return;
        }
        LastName = name;
    }
}