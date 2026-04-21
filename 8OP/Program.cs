class Program
{
    static void Main()
    {
        Controller controller = new Controller();

        
        bool Running = true;
        while (Running)
        {
            ShowMenu();
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case(ConsoleKey.D1):
                    Button1();
                break;
                case(ConsoleKey.D2):
                    Button2();
                break;
                case(ConsoleKey.D3):
                    Button3();
                break;
                case(ConsoleKey.D4):
                    Button4();
                break;
                case(ConsoleKey.D5):
                    Button5();
                break;
                case(ConsoleKey.D6):
                    Button6();
                break;
                case(ConsoleKey.D7):
                    Button7();
                break;
                case(ConsoleKey.D8):
                    Button8();
                break;
                case(ConsoleKey.D9):
                    Button9();
                break;
                case(ConsoleKey.Escape):
                    Running = false;
                break;
            }
            Console.Clear();
        }

        void Button9()
        {
            Console.Clear();
            Console.WriteLine("ALL STUDENTS");
            foreach (Student s in controller.GetAllStudents())
            {
                Console.WriteLine($"ID: {s.id} | {s.FullName}");
            }

            Console.WriteLine("\nALL GROUPS");
            foreach (Group g in controller.GetAllGroups())
            {
                Console.WriteLine($"Group: {g.Name} | Students: {g.Amount}");
            }

            Console.WriteLine("\nALL DORMS");
            foreach (Dorm d in controller.GetAllDorms())
            {
                Console.WriteLine($"Dorm: {d.Name} | Total Capacity: {d.TotalSize}");
            }
            Pause();
        }

        void Button8()
        {
            Console.Clear();
            Console.WriteLine("8 - Search");
            Console.WriteLine("1 - Look for student");
            Console.WriteLine("2 - Look for student in group x");
            Console.WriteLine("3 - Look for student in dorm x");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Console.WriteLine("Input students full name eg. Sigma Bro");
            string FullName = Console.ReadLine();
            switch (keyInfo.Key)
            {
                case(ConsoleKey.D1):
                    SButton1();
                break;
                case(ConsoleKey.D2):
                    SButton2();
                break;
                case(ConsoleKey.D3):
                    SButton3();
                break;
            }

            void SButton1()
            {
                foreach (Student s in controller.SearchAllStudents(FullName))
                {
                    Console.WriteLine($"{s.FullName}, {s.id}");
                }
            }

            void SButton2()
            {
                Console.WriteLine("Input Dorm name");
                string groupName = Console.ReadLine();
                foreach (Student s in controller.SearchInGroup(groupName, FullName))
                {
                    Console.WriteLine($"{s.FullName}, {s.id}");
                }
            }

            void SButton3()
            {
                Console.WriteLine("Input Dorm name");
                string dormName = Console.ReadLine();
                foreach (Student s in controller.SearchInDorm(dormName, FullName))
                {
                    Console.WriteLine($"{s.FullName}, {s.id}");
                }
            }
        }

        void Button7()
        {
            Console.Clear();
            Console.WriteLine("7 - More dorm options");
            Console.WriteLine("1 - Show Dorm info");
            Console.WriteLine("2 - Modify Dorm");
            Console.WriteLine("3 - Remove Dorm");
            Console.WriteLine("4 - Delete Room from Dorm");
            Console.WriteLine("5 - Expel Student from Dorm");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Dorm dorm = GetDorm();
            switch (keyInfo.Key)
            {
                case(ConsoleKey.D1):
                    SButton1();
                break;
                case(ConsoleKey.D2):
                    SButton2();
                break;
                case(ConsoleKey.D3):
                    SButton3();
                break;
                case(ConsoleKey.D4):
                    SButton4();
                break;
                case(ConsoleKey.D5):
                    SButton5();
                break;
            }

            void SButton1()
            {
                Console.WriteLine($"{dorm.Info}");
                int count = 0;
                foreach (Room r in dorm.GetAllRooms())
                {
                    int freeSpots = r.Size - r.CurrentOccupancy;
                    Console.WriteLine($"{count} - Room {r.Name} | Capacity: {r.CurrentOccupancy}/{r.Size} | Free spots: {freeSpots}");
                    count++;
                }
                Pause();
            }

            void SButton2()
            {
                Console.WriteLine($"{dorm.Info}");
                Console.WriteLine("Input new name");
                string name = Console.ReadLine();
                dorm.ModifyName(name);
                Pause();
            }

            void SButton3()
            {
                Console.WriteLine($"{dorm.Info} has been deleted");
                controller.RemoveDorm(dorm);
                Pause();
            }

            void SButton4()
            {
                SButton1();
                Console.WriteLine("Input index to delete");
                string idx = Console.ReadLine();
                dorm.RemoveRoom(GetValidInt(idx));
                Pause();
            }

            void SButton5()
            {
                SButton1();
                Console.WriteLine("\nInput student FullName to delete:");
                string fullName = Console.ReadLine();

                List<Student> matchingStudents = dorm.SearchAllResidents(fullName);

                if (matchingStudents.Count == 0)
                {
                    Console.WriteLine("Error: No student with that name found in this dorm.");
                }
                else if (matchingStudents.Count > 1)
                {
                    Console.WriteLine("Warning: Multiple students found with that name.");
                    foreach(var s in matchingStudents)
                    {
                        Console.WriteLine($"ID: {s.id} | Name: {s.FullName}");
                    }
                    Console.WriteLine("Please use the 'More student options' menu to remove them by ID to be safe.");
                }
                else
                {
                    Student studentToRemove = matchingStudents[0];
                    dorm.RemoveStudent(studentToRemove);
                    Console.WriteLine($"\nSuccess: {studentToRemove.FullName} has been removed from {dorm.Name}.");
                }
                Pause();
            }

            Dorm GetDorm()
            {
                Console.WriteLine("\n");
                Dorm? dormReturn = null;
                while (dormReturn == null)
                {
                    Console.WriteLine("Input Dorm name to edit");
                    string name = Console.ReadLine();
                    Dorm? dorm = controller.GetDormByName(name);
                    if (dorm != null)
                    {
                        dormReturn = dorm;
                    }
                }
                return dormReturn;
            }
        }


        void Button6()
        {
            Console.Clear();
            Console.WriteLine("6 - More group options");
            Console.WriteLine("1 - Show group info");
            Console.WriteLine("2 - Modify group");
            Console.WriteLine("3 - Remove group");
            Console.WriteLine("4 - Delete student from group");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Group group = GetGroup();
            switch (keyInfo.Key)
            {
                case(ConsoleKey.D1):
                    SButton1();
                break;
                case(ConsoleKey.D2):
                    SButton2();
                break;
                case(ConsoleKey.D3):
                    SButton3();
                break;
                case(ConsoleKey.D4):
                    SButton4();
                break;
            }

            void SButton1()
            {
                Console.WriteLine($"{group.Info}");
                int count = 0;
                foreach (Student s in group.GetStudents)
                {
                    Console.WriteLine($"{count} - {s.FullName}");
                    count++;
                }
                Pause();
            }

            void SButton2()
            {
                Console.WriteLine($"{group.Info}");
                Console.WriteLine("Input new name");
                string name = Console.ReadLine();
                group.ModifyName(name);
                Pause();
            }

            void SButton3()
            {
                Console.WriteLine($"{group.Info} has been deleted");
                controller.RemoveGroup(group);
                Pause();
            }

            void SButton4()
            {
                SButton1();
                Console.WriteLine("Input index to delete");
                string idx = Console.ReadLine();
                group.RemoveStudent(GetValidInt(idx));
                Pause();
            }


            Group GetGroup()
            {
                Console.WriteLine("\n");
                Group? groupReturn = null;
                while (groupReturn == null)
                {
                    Console.WriteLine("Input group name to edit");
                    string name = Console.ReadLine();
                    Group? group = controller.GetGroupByName(name);
                    if (group != null)
                    {
                        groupReturn = group;
                    }
                }
                return groupReturn;
            }
        }

        void Button5()
        {
            Console.Clear();
            Console.WriteLine("5 - More student options");
            Console.WriteLine("1 - Modify student");
            Console.WriteLine("2 - Add student in group");
            Console.WriteLine("3 - Add student in room");
            Console.WriteLine("4 - Expel/Delete student completely");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Student student = GetStudent();
            switch (keyInfo.Key)
            {
                case(ConsoleKey.D1):
                    SButton1();
                break;
                case(ConsoleKey.D2):
                    SButton2();
                break;
                case(ConsoleKey.D3):
                    SButton3();
                break;
                case(ConsoleKey.D4):
                    SButton4();
                break;
            }

            void SButton1()
            {
                Console.WriteLine($"{student.FullName} ID: {student.id}");
                Console.WriteLine("\n");
                Console.WriteLine("What you want to edit");
                Console.WriteLine("1 - FullName");
                Console.WriteLine("2 - FirstName");
                Console.WriteLine("3 - LastName");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case(ConsoleKey.D1):
                        Console.WriteLine("Input new name");
                        string name = Console.ReadLine();
                        student.Modify(name);
                    break;
                    case(ConsoleKey.D2):
                        Console.WriteLine("Input new FirstName");
                        string Firstname = Console.ReadLine();
                        student.Modify(Firstname, true);
                    break;
                    case(ConsoleKey.D3):
                        Console.WriteLine("Input new LastName");
                        string Lastname = Console.ReadLine();
                        student.Modify(Lastname, false);
                    break;
                }
                Pause();
            }

            void SButton2()
            {
                Console.WriteLine("\n");
                Console.WriteLine("Input Group name");
                string Groupname = Console.ReadLine();
                controller.AssignStudentToGroup(student, Groupname);
                Pause();
            }

            void SButton3()
            {
                Console.WriteLine("\n");
                Console.WriteLine("Input Dorm name");
                string DormName = Console.ReadLine();
                Dorm dorm = controller.GetDormByName(DormName);
                int count = 0;
                foreach (Room r in dorm.GetAllRooms())
                {
                    Console.WriteLine($"{count} - Room: {r.Name}, Status: {r.IsFull}");
                    count++;
                }
                Console.WriteLine("Input room index");
                string Roomidx = Console.ReadLine();
                dorm.AddStudent(student, GetValidInt(Roomidx));
                Pause();
            }

            void SButton4()
            {
                Console.WriteLine($"\nAre you sure you want to completely expel {student.FullName}? (y/n)");
                if (Console.ReadLine().ToLower() == "y")
                {
                    controller.ExpelStudent(student.id);
                    Console.WriteLine("Student expelled from university, groups, and dorms.");
                }
                Pause();
            }

            Student GetStudent()
            {
                Console.WriteLine("\n");
                Student? studentReturn = null;
                while (studentReturn == null)
                {
                    Console.WriteLine("Input student id to edit");
                    string id = Console.ReadLine();
                    Student? student = controller.GetStudentById(GetValidInt(id));
                    if (student != null)
                    {
                        studentReturn = student;
                    }
                }
                return studentReturn;
            }
        }

        void Button4()
        {
            Console.Clear();
            Console.WriteLine("4 - Add new room to dorm");
            List<Dorm> dorms = controller.GetAllDorms();
            int count = 0;
            foreach(Dorm d in dorms)
            {
                Console.WriteLine($"{count} - {d.Name}");
                count++;
            }
            Console.WriteLine("Choose dorm to add(index)");
            string idx = Console.ReadLine();
            Console.WriteLine("Input name of room");
            string roomName = Console.ReadLine();
            Console.WriteLine("Choose size of room");
            string roomSize = Console.ReadLine();
            controller.AddRoomToDorm(roomName, GetValidInt(roomSize), GetValidInt(idx));
            Pause();
        }

        void Button3()
        {
            Console.Clear();
            Console.WriteLine("3 - Add new dorm");
            Console.WriteLine("Plese input name");
            string name = Console.ReadLine();
            controller.CreateDorm(name);
            Pause();
        }

        void Button2()
        {
            Console.Clear();
            Console.WriteLine("2 - Add new group");
            Console.WriteLine("Plese input name");
            string name = Console.ReadLine();
            controller.CreateGroup(name);
            Pause();
        }

        void Button1()
        {
            Console.Clear();
            Console.WriteLine("1 - Add new student");
            Console.WriteLine("Plese input firstName");
            string firstName = Console.ReadLine();
            Console.WriteLine("Plese input lastName");
            string lastName = Console.ReadLine();
            Console.WriteLine("Plese input id");
            string id = Console.ReadLine();
            controller.RegisterNewStudent(firstName, lastName, GetValidInt(id));
            Pause();
        }

        int GetValidInt(string input)
        {
            int validNumber;
            while (!int.TryParse(input, out validNumber))
            {
                Console.WriteLine("Error: Please enter numbers only. Try again:");
                input = Console.ReadLine();
            }
            return validNumber;
        }

        void ShowMenu()
        {
            Console.WriteLine("1 - Add new student");
            Console.WriteLine("2 - Add new group");
            Console.WriteLine("3 - Add new dorm");
            Console.WriteLine("4 - Add new room to dorm");
            Console.WriteLine("5 - More student options");
            Console.WriteLine("6 - More group options");
            Console.WriteLine("7 - More dorm options");
            Console.WriteLine("8 - search for student");
            Console.WriteLine("9 - View all university data (Lists)");
        }
        
        void Pause()
        {
            Console.WriteLine("Press any key to return...");
            Console.ReadKey(true);
        }
    }
}