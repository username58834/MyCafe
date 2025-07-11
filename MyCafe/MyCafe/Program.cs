using System.Threading.Channels;

namespace MyCafe
{
    internal class Item
    {
        string description = "";
        double price = 0;

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
    }
    internal class Program
    {
        static Item[] items = Array.Empty<Item>();
        static void CreateMenu()
        {
            Console.WriteLine(
                " __________________________\n" +
                "|  ______________________  |\n" +
                "| |                      | |\n" +
                "| | MyCafe               | |\n" +
                "| | -------------------- | |\n" +
                "| | 1. Add Item          | |\n" +
                "| | 2. Remove Item       | |\n" +
                "| | 3. Add tip           | |\n" +
                "| | 4. Display Bill      | |\n" +
                "| | 5. Clear All         | |\n" +
                "| | 6. Save to file      | |\n" +
                "| | 7. Load from file    | |\n" +
                "| | 0. Exit              | |\n" +
                "| |                      | |\n" +
                "|  ----------------------  |\n" +
                "|__________________________|\n"
                );
        }
        static void AddItem()
        {
            string description = "";
            double price = 0;
            while (true)
            {
                try
                {
                    Console.Write("\nEnter description: ");
                    description = Console.ReadLine();

                    if(description.Length < 3 || description.Length > 20)
                    {
                        throw new Exception("Write from 3 to 20 characters");
                    }

                    while (true)
                    {
                        try
                        {
                            Console.Write("Enter price: ");
                            price = double.Parse(Console.ReadLine());

                            if (price <= 0)
                            {
                                throw new Exception("Price must be >0");
                            }

                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message + "\n");
                        }
                    }

                    Array.Resize(ref items, items.Length + 1);
                    items[items.Length - 1] = new Item();
                    items[items.Length - 1].Description = description;
                    items[items.Length - 1].Price = price;

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Add item was successful.");
        }
        static void Loop()
        {
            int ans = 0;

            while (true)
            {                          
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;                
                    Console.WriteLine("\n// Menu omitted to save screen space");  

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Enter your choice: ");
                    ans = int.Parse(Console.ReadLine());

                    if (ans < 0 || ans > 7) {
                        throw new Exception("Enter an integer between 0 and 7");
                    }

                    if (ans == 0)
                    {
                        Console.WriteLine("Good-bye and thanks for using this program.");
                        Environment.Exit(0);
                    } else if(ans == 1)
                    {
                        AddItem();
                    }

                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                }                
            }
        }
        static void Main(string[] args)
        {
            
            CreateMenu();
            Loop();
        }
    }
}
