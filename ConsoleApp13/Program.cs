

//List<string> ints = new List<string> { "Hello", "Nazim", "Nazim", "Tello" };


//Nomrsinde 7 reqemi 3 ve 3 den artiq islenmis butun telebelri cixart

//ints.Where(i => i.Count(d => d == 'l') == 2).ToList().ForEach(a => Console.WriteLine(a));


//var text = ints.FirstOrDefault(i => i == "Nazim");

//var text = ints.SingleOrDefault(i => i == "Nazim","Kabab");


//Console.WriteLine(text);

//ints.Where(i=>i.EndsWith("im")).ToList().ForEach(t=>Console.WriteLine(t));


//using ConsoleApp13;

//List<Doctor> doctors = new List<Doctor>()
//{
//    new Doctor("Nazim","Nazimli",Time.seher),
//    new Doctor("Fazil", "Fazilli", Time.gunorta),
//    new Doctor("Qilman", "Hekim", Time.seher),
//    new Doctor("Qilman", "Hekim", Time.axsam),
//    new Doctor("Elman", "Hekim", Time.axsam),
//    new Doctor("Nerman", "Hekim", Time.axsam)
//};
////doctors.ForEach(doctor => Console.WriteLine(doctor));


//doctors.Where(doctor => doctor.Time == Time.axsam)
//       .ToList()
//       .ForEach(doc=>doc.Time = Time.gunorta);

//doctors.ForEach(doctor => Console.WriteLine(doctor));





//-----Single Responsibilty-------


//class Employee
//{
//    public string Name { get; set; }
//    public string Surname { get; set; }
//    public DateOnly DateOfBirth { get; set; }


//    public void PrintTimeSheetReport()
//    {
//        // do something...
//    }
//}







//class TimeSheetReport
//{
//    public void Print(Employee employee)
//    {
//        // do something...
//    }
//}

//class Employee
//{
//    public string Name { get; set; }
//    public string Surname { get; set; }
//    public DateOnly DateOfBirth { get; set; }
//}





//-----------Open/Closed prinsipi --------------------

//class Product
//{
//    public Guid Id { get; set; }
//    public string Name { get; set; }
//    public decimal Price { get; set; }
//    public decimal Weight { get; set; }
//}


//class Order
//{
//    private List<Product> items = new();
//    private string shipping = default;

//    public decimal GetTotal() => items.Sum(p => p.Price);
//    public decimal GetTotalWeight() => items.Sum(p => p.Weight);
//    public void SetShippingType(string type) => shipping = type;


//    public decimal GetShippingCost()
//    {
//        if (shipping == "ground")
//        {
//            // Free ground delivery on big orders
//            if (GetTotal() > 100)
//                return 0;

//            // $1.5 per kilogram, but $10 minumum
//            return Math.Max(10, GetTotalWeight() * 1.5M);
//        }

//        if (shipping == "air")
//        {
//            // $3 per kilogram, but $20 minumum
//            return Math.Max(20, GetTotalWeight() * 3);
//        }

//        throw new ArgumentException(nameof(shipping));
//    }


//    public DateTime GetShippingDate()
//    {
//        if (shipping == "ground")
//        {
//            return DateTime.Now.AddDays(7);
//        }

//        if (shipping == "air")
//        {
//            return DateTime.Now.AddDays(7);
//        }

//        throw new ArgumentException(nameof(shipping));
//    }
//}





class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
}


interface IShipping
{
    decimal GetCost(Order order);
    DateTime GetDate(Order order);
}


class Ground : IShipping
{
    public decimal GetCost(Order order)
    {
        // Free ground delivery on big orders
        if (order.GetTotal() > 100)
            return 0;

        // $1.5 per kilogram, but $10 minumum
        return Math.Max(10, order.GetTotalWeight() * 1.5M);
    }

    public DateTime GetDate(Order order) => DateTime.Now.AddDays(7);
}



class Air : IShipping
{
    public decimal GetCost(Order order)
    {
        // $3 per kilogram, but $20 minumum
        return Math.Max(20, order.GetTotalWeight() * 3);
    }

    public DateTime GetDate(Order order) => DateTime.Now.AddDays(2);
}

class Order
{
    private List<Product> items = new();
    private IShipping shipping;

    public decimal GetTotal() => items.Sum(p => p.Price);
    public decimal GetTotalWeight() => items.Sum(p => p.Weight);
    public void SetShippingType(IShipping type) => shipping = type;

    public decimal GetShippingCost() => shipping.GetCost(this);
    public DateTime GetShippingDate() => shipping.GetDate(this);
}



class Program
{
    static void Main()
    {
        Order order = new();
        order.SetShippingType(new Air());
        Console.WriteLine(order.GetShippingDate());
    }
}

