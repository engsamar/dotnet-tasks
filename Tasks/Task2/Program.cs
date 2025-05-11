namespace Task2;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        bool close = true;
        
        List<string> menu = new List<string>();
        menu.Add(" P - Print numbers");
        menu.Add(" A - Add a number");
        menu.Add(" M - Display mean of the numbers");
        menu.Add(" S - Display the smallest number");
        menu.Add(" L - Display the largest number");
        menu.Add(" I - search for a number in the list and if found display the index");
        menu.Add(" R - Remove number from list if exist");
        menu.Add(" D - Remove duplicated number from list");
        menu.Add(" C - clearing out the list (make it empty again)");
        menu.Add(" Q - Quit");

        do
        {
            Console.WriteLine("---- Please choose one options from the menu ----");
            for (int j = 0; j < menu.Count; j++)
            {
                Console.WriteLine(menu[j]);
            }

            Console.WriteLine("Enter your choice: ");
            string? choice = Console.ReadLine().ToLower().Trim();

            switch (choice)
            {
                case "p":
                    if (numbers.Count == 0)
                        Console.WriteLine("The list is empty");
                    else
                        Console.WriteLine( string.Join(" ", numbers) );
                    break;

                case "a":
                    Console.WriteLine("Enter numbers to add seperated by comma ex (1,3,4): ");
                    string? inputs = Console.ReadLine();
                    List<string> items = new List<string>(inputs.Split(','));

                    for (int i = 0; i < items.Count; i++)
                    {
                        if (int.TryParse(items[i].Trim(), out int numToAdd))
                        {
                            numbers.Add(numToAdd);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter an integer.");
                            continue;
                        }
                    }

                    Console.WriteLine($"{inputs} added to list");
                    break;


                case "m":
                    if (numbers.Count == 0)
                        Console.WriteLine("Unable to calculate the average - no data");
                    else
                    {
                        int total = 0;
                        for (int i = 0 ;i < numbers.Count ;i++)
                        {
                            total += numbers[i];
                        }
                        double avg = double.Round(total / numbers.Count,2);
                        Console.WriteLine($"The mean/average of numbers in list is {avg}");
                    }
                       
                    break;

                case "s":
                    if (numbers.Count == 0)
                        Console.WriteLine("Unable to determine the smallest number - list is empty");
                    else
                    {
                        int smallest = numbers[0];

                        for (int i = 0 ;i < numbers.Count ;i++)
                        {
                            if (numbers[i] < smallest)
                            {
                                smallest = numbers[i];
                            }
                        }
                        Console.WriteLine($"The smallest number is {smallest}");
                    }
                    break;

                case "l":
                    if (numbers.Count == 0)
                        Console.WriteLine("Unable to determine the largest number - list is empty");
                    else
                    {
                        int largest = numbers[0];
                        for (int i = 0 ;i < numbers.Count ;i++)
                        {
                            if (numbers[i] > largest)
                            {
                                largest = numbers[i];
                            }
                        }
                        Console.WriteLine($"The largest number is {largest}");
                    }
                       
                    break;
                
                case "i":
                    if (numbers.Count == 0)
                        Console.WriteLine("Unable to search number - list is empty");
                    else
                    {
                        Console.WriteLine("Please enter a number to search:");
                        int searchNumber = Convert.ToInt32(Console.ReadLine());
                        int index = -1;
                        for (int i = 0 ;i < numbers.Count ;i++)
                        {
                            if (numbers[i] == searchNumber)
                            {
                                searchNumber = numbers[i];
                                index = i;
                            }
                            
                        }
                        if (index != -1)
                            Console.WriteLine($"The index of number {searchNumber} is {index}");
                        else
                            Console.WriteLine($"The number {searchNumber} is not found in the list");
                    }
                       
                    break;
                
                case "c":
                    numbers.Clear();
                    Console.WriteLine("All numbers cleared.");
                    break;
                
                case "d":
                    for (int i = 0; i < numbers.Count; i++)
                    {
                        for (int j = i + 1; j < numbers.Count; j++)
                        {
                            if (numbers[i] == numbers[j])
                            {
                                numbers.RemoveAt(j);
                                j--;
                            }
                        }
                    }
                    Console.WriteLine("All duplicated numbers are cleared.");
                    break;
                
                case "r":
                    if (numbers.Count == 0)
                        Console.WriteLine("Unable to delete number - list is empty");
                    else
                    {
                        Console.WriteLine("Please enter a number to delete:");
                        int deleteNumber = Convert.ToInt32(Console.ReadLine());
                        int index = -1;
                        for (int i = 0 ;i < numbers.Count ;i++)
                        {
                            if (numbers[i] == deleteNumber)
                            {
                                deleteNumber = numbers[i];
                                index = i;
                                numbers.Remove(deleteNumber);
                            }
                            
                        }
                        if (index != -1)
                            Console.WriteLine($"The index of number {deleteNumber} is {index} are removed from list");
                        else
                            Console.WriteLine($"The number {deleteNumber} is not found in the list");
                    }
                       
                    break;

                case "q":
                    Console.WriteLine("Goodbye");
                    close = false;
                    break;

                default:
                    Console.WriteLine("Unknown selection, please try again.");
                    break;
            }

        } while (close);
    }
}