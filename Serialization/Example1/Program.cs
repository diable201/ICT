using System;

namespace Example1
{
    class Student
    {
        public string name;
        public double gpa;
    }

    class Student2
    {
        string name;
        double gpa;

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public void SetGPA(double gpa)
        {
            if (gpa > 0)
            {
                this.gpa = gpa;
            }
        }

        public double GetGPA()
        {
            return gpa;
        }
    }

    class Student3
    {
        public string Name { get; set; }

        double _gpa;
        public double Gpa 
        { 
            set
            {
                if (value > 0)
                {
                    _gpa = value;
                }
            }

            get
            {
                return _gpa;
            }

        }

        public override string ToString()
        {
            return Name + " " + Gpa;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            F4();
        }

        static void F4()
        {
            Student3 s = new Student3() {Name = "John", Gpa = 3.5 };
            Console.WriteLine(s);
        }


        static void F3()
        {
            Student3 s = new Student3();
            s.Gpa = 4;
            s.Name = "Bob";
            Console.WriteLine(s);
        }

        static void F2()
        {
            Student2 s = new Student2();
            s.SetGPA(3);
            s.SetName("Bob");
            Console.WriteLine(s.GetGPA());
        }

        static void F1()
        {
            Student s = new Student();
            s.gpa = 3;
            s.name = "Bob";

            s.gpa = 2;
            Console.WriteLine(s.gpa);

        }

    }
}