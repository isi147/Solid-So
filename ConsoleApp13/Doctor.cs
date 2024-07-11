namespace ConsoleApp13;

public class Doctor
{
    private static int Staticid = 0;
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Time Time { get; set; }
    public Doctor(string name, string surname, Time time)
    {

        Id = ++Staticid; 
        Name = name;
        Surname = surname;
        Time = time;
    }

    public Doctor()
    {

    }
    public override string ToString()
       => $"Id:{Id}.Name:{Name} - Surname:{Surname} - Time:{Time}\n";

}
