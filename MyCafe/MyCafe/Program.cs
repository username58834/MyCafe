using System.Threading.Channels;

namespace MyCafe
{
    internal class Program
    {
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
                "|__________________________|\n\n"
                );
        }

        static void Loop()
        {
            int ans = 0;

            while (true)
            {
                              

                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;                
                    Console.WriteLine("// Menu omitted to save screen space");  

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Enter your choice: ");
                    ans = int.Parse(Console.ReadLine());

                    if (ans < 0 || ans > 7) {
                        throw new Exception("Enter an integer between 0 and 7");
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                }

                if (ans == 0)
                {
                    Environment.Exit(0);
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
