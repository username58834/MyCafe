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
        static int tipType = 3;
        static double tipValue = 0;
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

        static string RightAlign(string line, int spaces)
        {
            for (int j = 0; j <= spaces; j++)
            {
                line += " ";
            }
            return line;
        }
        static void RemoveAtIndex(int x)
        {
            for(int i = x; i < items.Length - 1; i++)
            {
                items[i] = items[i + 1];
            }
            Array.Resize(ref items, items.Length - 1);
        }        

        static int FindLongest()
        {
            int maxSize = 5;
            foreach(Item item in items)
            {
                if(item.Price.ToString("F2").Length > maxSize)
                {
                    maxSize = item.Price.ToString("F2").Length;
                }
            }
            return maxSize;
        }

        static double NetTotal()
        {
            double sum = 0;
            foreach (Item item in items)
            {
                sum += item.Price;
            }
            return sum;
        }

        static double GetTip()
        {
            if (tipType == 1)
            {
                return NetTotal() * tipValue / 100.0;
            } else if(tipType == 2)
            {
                return tipValue;
            }
            else
            {
                return 0;
            }
        }
        static void AddTip()
        {
            Console.WriteLine(
                        $"\nNet Total: ${NetTotal():F2}\n"+
                        "1 - Tip Percentage\n" +
                        "2 - Tip Amount\n" +
                        "3 - No Tip"
                        );
            while (true) {
                try
                {
                    Console.Write("Enter Tip Method: ");
                    tipType = int.Parse(Console.ReadLine());
                    if (tipType < 1 || tipType > 3)
                    {
                        throw new Exception("Enter a number between 1 to 3");
                    }

                    if(tipType == 1 || tipType == 2)
                    {
                        while (true)
                        {
                            try
                            {
                                if (tipType == 1)
                                {
                                    Console.Write("Enter tip percentage: ");
                                }
                                else
                                {
                                    Console.Write("Enter tip amount: ");
                                }

                                tipValue = double.Parse(Console.ReadLine());
                                if (tipValue <= 0)
                                {
                                    throw new Exception("Enter a number >0");
                                }

                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message + "\n");
                            }
                        }
                    }

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                }
            }
        }

        static void DisplayBill()
        {
            string line;
            int maxSize = Math.Max(FindLongest(), (NetTotal() + GetTip() + NetTotal() * 5 / 100).ToString("F2").Length) + 1;
            int ans = 0;

            line = "\nDescription         ";
            line = RightAlign(line, maxSize - 4);
            line += "  Price\n---------------------  ";
            for (int j = 0; j <= maxSize; j++)
            {
                line += "-";
            }
            Console.WriteLine(line);


            for (int i = 1; i <= items.Length; i++)
            {
                line = items[i - 1].Description;
                for (int j = 1; j <= 20 - items[i - 1].Description.Length; j++)
                {
                    line += " ";
                }
                line += "  ";

                for (int j = 0; j <= maxSize - items[i - 1].Price.ToString("F2").Length; j++)
                {
                    line += " ";
                }
                line += "$" + items[i - 1].Price.ToString("F2");

                Console.WriteLine(line);
            }

            line = "---------------------  ";
            for (int j = 0; j <= maxSize; j++)
            {
                line += "-";
            }
            Console.WriteLine(line);

            line = "            Net Total ";
            line = RightAlign(line, maxSize - NetTotal().ToString("F2").Length);
            line += $"${NetTotal():F2}\n";

            line += "           Tip Amount ";
            line = RightAlign(line, maxSize - GetTip().ToString("F2").Length);
            line += $"${GetTip():F2}\n";

            line += "           GST Amount ";
            line = RightAlign(line, maxSize - (NetTotal() * 5 / 100).ToString("F2").Length);
            line += $"${NetTotal() * 5 / 100:F2}\n";

            line += "         Total Amount ";
            line = RightAlign(line, maxSize - (NetTotal() + GetTip() + NetTotal() * 5 / 100).ToString("F2").Length);
            line += $"${(NetTotal() + GetTip() + NetTotal() * 5 / 100):F2}\n";

            Console.WriteLine(line);
        }
        static void AddItem()
        {
            string description = "";
            double price = 0;
            while (true)
            {
                try
                {
                    if (items.Length >= 5)
                    {
                        throw new Exception("You cannot order more than 5 items.");
                    }
                    Console.Write("\nEnter description: ");
                    description = Console.ReadLine();

                    if (description.Length < 3 || description.Length > 20)
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

                    if(tipType != 3)
                    {
                        AddTip();
                    }
                    
                    Console.WriteLine("Add item was successful.");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (items.Length >= 5) break;
                }
            }            
        }

        static void RemoveItem()
        {
            string line;
            int maxSize = FindLongest() + 1;
            int ans = 0;

            line = "\nItemNo  Description         ";
            line = RightAlign(line, maxSize - 5);
            line += "  Price\n------  -----------           ";
            for (int j = 0; j <= maxSize; j++)
            {
                line += "-";
            }

            Console.WriteLine(line);


            for (int i = 1; i <= items.Length; i++)
            {
                line = "     " + i + "  " + items[i - 1].Description;
                for (int j = 1; j <= 20 - items[i - 1].Description.Length; j++)
                {
                    line += " ";
                }
                line += "  ";

                for (int j = 1; j <= maxSize - items[i - 1].Price.ToString("F2").Length; j++)
                {
                    line += " ";
                }
                line += "$" + items[i - 1].Price.ToString("F2");

                Console.WriteLine(line);
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the item number to remove or 0 to cancel: ");
                    ans = int.Parse(Console.ReadLine());
                    if (ans < 0 || ans > items.Length)
                    {
                        throw new Exception($"Enter a number between 0 to {items.Length}");
                    }

                    if (ans > 0)
                    {
                        RemoveAtIndex(ans - 1);
                        Console.WriteLine("Remove item was successful");
                    }
                    else
                    {
                        Console.WriteLine("Removing canceled");
                    }

                    if(items.Length == 0)
                    {
                        tipType = 3;
                        tipValue = 0;
                    }
                    else if (tipType != 3)
                    {
                        AddTip();
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                }
            }
        }

        static void ClearAll()
        {
            while (items.Length > 0)
            {
                RemoveAtIndex(0);
            }

            tipType = 3;
            tipValue = 0;
        }

        static void Save()
        {
            string fileName;
            while (true)
            {
                try
                {
                    Console.Write("\nEnter the file path to save items to: ");
                    fileName = Console.ReadLine();
                    if (fileName.Split('.')[0].Length < 1 || fileName.Split('.')[0].Length > 10)
                    {
                        throw new Exception("File name must contain 1 to 10 letters.");
                    }

                    using (FileStream file = File.Create(fileName))
                    {
                        StreamWriter writter = new StreamWriter(file);
                        foreach (Item item in items)
                        {
                            writter.WriteLine($"{item.Description},{item.Price}");
                        }
                        writter.Close();
                    }
                    Console.WriteLine($"Write to file {fileName} was successful.");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                }
            }
        }

        static void Load()
        {
            ClearAll();
            string fileName, line;

            while (true)
            {
                try
                {
                    Console.Write("\nEnter the path to load items from: ");
                    fileName = Console.ReadLine();
                    if (fileName.Split('.')[0].Length < 1 || fileName.Split('.')[0].Length > 10)
                    {
                        throw new Exception("File name must contain 1 to 10 letters.");
                    }

                    using (FileStream file = File.OpenRead(fileName))
                    {
                        StreamReader reader = new StreamReader(file);
                        while ((line = reader.ReadLine()) != null)
                        {
                            Array.Resize(ref items, items.Length + 1);
                            items[items.Length - 1] = new Item();
                            items[items.Length - 1].Description = line.Split(',')[0];
                            items[items.Length - 1].Price = double.Parse(line.Split(',')[1]);
                        }
                        
                        reader.Close();
                    }

                    if(items.Length > 5)
                    {
                        throw new Exception("The number of items cannot exceed 5.");
                    }
                    Console.WriteLine($"Read from {fileName} was successful.");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                }
            }
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
                    } else if (ans == 1)
                    {
                        AddItem();
                    } else if (ans == 2)
                    {
                        if (items.Length > 0) RemoveItem();
                        else throw new Exception("You have not ordered anything yet.");
                    } else if (ans == 3)
                    {
                        if (items.Length > 0) AddTip();
                        else throw new Exception("There are no items in the bill to add tip for.");
                    } else if (ans == 4)
                    {
                        if (items.Length > 0) DisplayBill();
                        else throw new Exception("There are no items in the bill to desplay.");
                    } else if (ans == 5)
                    {
                        if (items.Length > 0) {
                            ClearAll();
                            Console.WriteLine("All items have been cleared.");
                        }
                        else throw new Exception("There are no items in the bill to clear.");
                    } else if(ans == 6)
                    {
                        if (items.Length > 0) Save();
                        else throw new Exception("There are no items in the bill to save.");
                    } else if (ans == 7)
                    {
                        Load();
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
