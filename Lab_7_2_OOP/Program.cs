using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Lab_7_2_OOP
{
    class User : IComparable<User>, IComparer<User>
    {
        public string Surname;
        public string Group;
        public int Course;
        public string Username;
        public string Password;
        public string RegData;
        public int TicketN;

        public User(string surname, string group, int course, string username, string password, string regdata, int ticketn)
        {
            Surname = surname;
            Group = group;
            Course = course;
            Username = username;
            Password = password;
            RegData = regdata;
            TicketN = ticketn;
        }

        public void Show()
        {
            Console.WriteLine(String.Format("| {0,-18} | {1,-7} | {2,-4} | {3,-19} | {4,-16} | {5,-15} | {6,-8} |", Surname, Group, Course, Username, Password, RegData, TicketN));
        }

        public int CompareTo(User comparePart)
        {
            if (comparePart == null) return 1;
            else return this.TicketN.CompareTo(comparePart.TicketN);
        }


        public int Compare(User obj1, User obj2)
        {
            User p1 = obj1 as User;
            User p2 = obj2 as User;
            if (p1.Course > p2.Course && Convert.ToInt32(p1.RegData.Substring(6)) > Convert.ToInt32(p2.RegData.Substring(6))) return 1;
            if (p1.Course < p2.Course && Convert.ToInt32(p1.RegData.Substring(6)) < Convert.ToInt32(p2.RegData.Substring(6))) return -1;
            return 0;
        }


    }

    class Program
    {
        static void WriteDB(List<User> users)
        {
            string textRow;
            StreamWriter file = new StreamWriter("output.txt");
            foreach (User u in users)
            {
                textRow = u.Surname + ";" + u.Group + ";" + Convert.ToString(u.Course) + ";" + u.Username + ";" +
                    u.Password + ";" + u.RegData + ";" + Convert.ToString(u.TicketN);

                file.WriteLine(textRow);
            }
            file.Close();
        }


        static List<User> ReadBD()
        {
            string sNameFile, textRow;
            string pSurname, pGroup, sCourse, pUsername, pPassword, pRegData, sTicketN;
            int pTicketN, pCourse;
            int i, ip;

            List<User> patients = new List<User>();
            StreamReader file = new StreamReader("output.txt");
            while (file.Peek() >= 0)
            {
                pSurname = ""; pGroup = ""; sCourse = ""; pUsername = ""; pPassword = ""; pRegData = ""; sTicketN = "";
                textRow = file.ReadLine();
                i = textRow.IndexOf(';') - 1;
                for (int j = 0; j <= i; j++) pSurname = pSurname + textRow[j];
                ip = i + 2;
                i = textRow.IndexOf(';', ip) - 1;
                for (int j = ip; j <= i; j++) pGroup = pGroup + textRow[j];
                ip = i + 2;
                i = textRow.IndexOf(';', ip) - 1;
                for (int j = ip; j <= i; j++) sCourse = sCourse + textRow[j];
                ip = i + 2;
                i = textRow.IndexOf(';', ip) - 1;
                for (int j = ip; j <= i; j++) pUsername = pUsername + textRow[j];
                ip = i + 2;
                i = textRow.IndexOf(';', ip) - 1;
                for (int j = ip; j <= i; j++) pPassword = pPassword + textRow[j];
                ip = i + 2;
                i = textRow.IndexOf(';', ip) - 1;
                for (int j = ip; j <= i; j++) pRegData = pRegData + textRow[j];
                ip = i + 2;
                for (int j = ip; j <= textRow.Length - 1; j++) sTicketN = sTicketN + textRow[j];

                pTicketN = Convert.ToInt32(sTicketN);
                pCourse = Convert.ToInt32(sCourse);
                patients.Add(new User(pSurname, pGroup, pCourse, pUsername, pPassword, pRegData, pTicketN));
            }
            file.Close();
            return patients;

        }


        static void AddUser(List<User> users)
        {
            Console.Write("Прiзвище: ");
            string surname = Console.ReadLine();
            Console.Write("Група: ");
            string group = Console.ReadLine();
            Console.Write("Курс: ");
            int course = Convert.ToInt32(Console.ReadLine());
            Console.Write("Логiн: ");
            string login = Console.ReadLine();
            Console.Write("Пароль: ");
            string password = Console.ReadLine();
            Console.Write("Дата реєстрацiї: ");
            string regdata = Console.ReadLine();
            Console.Write("Номер студентського квитка: ");
            int ticketn = Convert.ToInt32(Console.ReadLine());

            users.Add(new User(surname, group, course, login, password, regdata, ticketn));
            WriteDB(users);
        }

        static void EditUser(List<User> users)
        {
            Console.Write("Введiть номер студентського квитка студента, якого бажаєте редагувати: ");
            int ticketn = Convert.ToInt32(Console.ReadLine());
            if (users.All(b => b.TicketN != ticketn))
            {
                Console.WriteLine("Такого студента не iснує!");
                return;
            }

            Console.WriteLine("");
            Console.WriteLine("Оберiть параметр, який бажаєте редагувати: ");
            Console.WriteLine("Прiзвище - 1");
            Console.WriteLine("Група - 2");
            Console.WriteLine("Курс - 3");
            Console.WriteLine("Логiн - 4");
            Console.WriteLine("Пароль - 5");
            Console.WriteLine("Дата реєстрацiї - 6");
            Console.WriteLine("Номер студентського квитка - 7");
            Console.WriteLine("Назад - 0");

            Console.Write("Ваш вибiр: ");
            int k = Convert.ToInt32(Console.ReadLine());

            if (k == 1)
            {
                Console.Write("Нове прiзвище: ");
                string surname = Console.ReadLine();
                users.FindAll(s => s.TicketN == ticketn).ForEach(x => x.Surname = surname);
                WriteDB(users);
            }
            else if (k == 2)
            {
                Console.Write("Нова група: ");
                string group = Console.ReadLine();
                users.FindAll(s => s.TicketN == ticketn).ForEach(x => x.Group = group);
                WriteDB(users);
            }
            else if (k == 3)
            {
                Console.Write("Новий курс: ");
                int course = Convert.ToInt32(Console.ReadLine());
                users.FindAll(s => s.TicketN == ticketn).ForEach(x => x.Course = course);
                WriteDB(users);
            }
            else if (k == 4)
            {
                Console.Write("Новий логiн: ");
                string username = Console.ReadLine();
                users.FindAll(s => s.TicketN == ticketn).ForEach(x => x.Username = username);
                WriteDB(users);
            }
            else if (k == 5)
            {
                Console.Write("Новий пароль: ");
                string password = Console.ReadLine();
                users.FindAll(s => s.TicketN == ticketn).ForEach(x => x.Password = password);
                WriteDB(users);
            }
            else if (k == 6)
            {
                Console.Write("Нова дата реєстрацiї: ");
                string regdata = Console.ReadLine();
                users.FindAll(s => s.TicketN == ticketn).ForEach(x => x.RegData = regdata);
                WriteDB(users);
            }
            else if (k == 7)
            {
                Console.Write("Новий номер студентського квитка: ");
                int ticket = Convert.ToInt32(Console.ReadLine());
                users.FindAll(s => s.TicketN == ticket).ForEach(x => x.TicketN = ticket);
                WriteDB(users);
            }
            else if (k == 0) return;
        }

        static void RemoveUser(List<User> users)
        {
            Console.Write("Введiть номер студентського квитка студента, якого бажаєте видалити: ");
            int ticketn = Convert.ToInt32(Console.ReadLine());
            if (users.All(b => b.TicketN != ticketn))
            {
                Console.WriteLine("Такого студента не iснує!");
                return;
            }
            var itemToDelete = users.Where(x => x.TicketN == ticketn).Select(x => x).First();
            users.Remove(itemToDelete);
            WriteDB(users);
        }

        static void Main(string[] args)
        {
            List<User> users = ReadBD();

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Оберiть: ");
                Console.WriteLine("Додати запис - 1");
                Console.WriteLine("Редагувати запис - 2");
                Console.WriteLine("Видалити запис - 3");
                Console.WriteLine("Сортування за номером студентського квитка - 4");
                Console.WriteLine("Сортування за курсом та датою реєстрацiї - 5");
                Console.WriteLine("Вийти - 0");

                Console.Write("Ваш вибiр: ");
                int k = Convert.ToInt32(Console.ReadLine());

                if (k == 1) AddUser(users);
                else if (k == 2) EditUser(users);
                else if (k == 3) RemoveUser(users);
                else if (k == 4)
                {
                    users.Sort();
                    Console.WriteLine("+--------------------+---------+------+---------------------+------------------+-----------------+----------+");
                    Console.WriteLine("|      Прiзвище      |  Група  | Курс |        Логiн        |      Пароль      | Дата реєстрацiї | № Квитка |");
                    Console.WriteLine("+--------------------+---------+------+---------------------+------------------+-----------------+----------+");
                    foreach (User aPart in users)
                    {
                        aPart.Show();
                    }
                    Console.WriteLine("+--------------------+---------+------+---------------------+------------------+-----------------+----------+");
                }
                else if (k == 5)
                {
                    List<User> sorted = users.OrderBy(p => p.Compare(p, p)).ToList();

                    Console.WriteLine("+--------------------+---------+------+---------------------+------------------+-----------------+----------+");
                    Console.WriteLine("|      Прiзвище      |  Група  | Курс |        Логiн        |      Пароль      | Дата реєстрацiї | № Квитка |");
                    Console.WriteLine("+--------------------+---------+------+---------------------+------------------+-----------------+----------+");
                    foreach (User aPart in sorted)
                    {
                        aPart.Show();
                    }

                    Console.WriteLine("+--------------------+---------+------+---------------------+------------------+-----------------+----------+");
                }
                else if (k == 0) break;
            }
        }
    }
}
