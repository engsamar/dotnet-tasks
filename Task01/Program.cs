using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Task01
{
    internal class Program
    {
//        Islam's Carpet Cleaning Service
//    Charges:
//        $25 per small
//        $35 per large
//    Sales tax rate is 6%
//    Estimates are valid for 30 days

//    Prompt the user for the number of small and large rooms they would like cleaned
//    and provide an estimate 


        static void Main(string[] args)
        {
            Console.WriteLine("Enter Number of small carpets");
            string? smallNumber = Console.ReadLine();


            Console.WriteLine("Enter Number of large carpets");
            string? largeNumber = Console.ReadLine();

            double? smallPrice = 25 * Convert.ToDouble(smallNumber);

            double? largePrice = 35 * Convert.ToDouble(largeNumber);

            double? priceWithoutTax = smallPrice + largePrice;

            double? tax = priceWithoutTax * 0.06;

            double? totalPrice = priceWithoutTax * tax;

            Console.WriteLine("*** Estimate for carpet cleaning service ***");
            Console.WriteLine($"Price per small room: 25 * {smallNumber} = {smallPrice}");
            Console.WriteLine($"Price per small room: 35 * {largeNumber} = {largePrice}");
            Console.WriteLine($"Cost : {priceWithoutTax}");
            Console.WriteLine($"Tax: ${tax}");
            Console.WriteLine("===============================");
            Console.WriteLine($"Total estimate: ${totalPrice}");
            Console.WriteLine("This estimate is valid for 30 days");




        }
    }
}
