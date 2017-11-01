using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("/***************************************************************/");
            Console.WriteLine("/* Welcome to Schwartz's Cerebral Cafe: Restaurant of the Mind */");
            Console.WriteLine("/***************************************************************/");

            Dictionary<string, double> menu = new Dictionary<string, double>();
            menu.Add("Stone Hands", 26.95);
            menu.Add("Dragon Feet", 14.95);
            menu.Add("Fatal Steps", 37.50);
            menu.Add("Killer Whales", 19.95);
            menu.Add("Deadly Elbows", 39.95);
            menu.Add("Torn Ligaments: The Art Of Dismemberment", 79.95);
            menu.Add("Snake Hips", 9.50);
            menu.Add("How To Win Friends And Influence People", 24.95);

            ArrayList cartBook = new ArrayList();
            ArrayList cartPrice = new ArrayList();

            Console.WriteLine("{0, -5}{1, -45}{2, 10:C}","SKU#", "Book", "Price");
            Console.WriteLine(new string('=', 60));
            int i = 0;
            foreach(KeyValuePair<string, double> book in menu)
            {
                i++;
                Console.WriteLine($"{i + ":", -5}{book.Key, -50}{book.Value, 10:C}");
            }

            Shop(menu, cartBook, cartPrice);

            Checkout(cartBook, cartPrice);

            Console.ReadLine();
        }

        private static void Checkout(ArrayList cartBook, ArrayList cartPrice)
        {
            int items = 0;
            double totalPrice = GetTotal(cartBook, cartPrice, ref items);
            Console.WriteLine("\nYour cart:");
            Console.WriteLine(new string('=', 60));
            for(int i = 0; i < cartPrice.Count; i++)
            {
                Console.WriteLine($"{cartBook[i],-50}{cartPrice[i],10:C}");
            }
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"{"Your total:", -50}{totalPrice, 10:C}");
            Console.WriteLine($"\nYour average item cost was: {totalPrice/items, 10:C}");
        }

        public static void Shop(Dictionary<string, double> menu, ArrayList cartBook, ArrayList cartPrice)
        {
            bool keepShopping = true;
            while(keepShopping)
            {
                GetBook("Please select a book from the list: ", menu, cartBook, cartPrice);
                keepShopping = DoAgain("Would you like to keep shopping? (y/n): ");
            }
        }

        private static void GetBook(string prompt, Dictionary<string, double> menu, ArrayList cartBook, ArrayList cartPrice)
        {
            Console.WriteLine(prompt);
            bool isValid = false;
            int number = 0;
            while (!isValid)
            {
                string input = Console.ReadLine();
                isValid = int.TryParse(input, out number);
            }
            int i = 0;
            foreach(KeyValuePair<string, double> book in menu)
            {
                i++;
                if(i == number)
                {
                    string result = book.ToString();
                    string thisBook = result.Substring(1, result.Length - 2);
                    string thisActualBook = thisBook.Substring(0, thisBook.IndexOf(','));
                    string price = thisBook.Substring(thisBook.IndexOf(',') + 1);
                    double thisPrice = double.Parse(price);
                    cartBook.Add(thisActualBook);
                    cartPrice.Add(thisPrice);
                    Console.WriteLine($"You chose {thisActualBook} which costs {thisPrice,0:C}.");
                }
            }
        }
       // private static void GetBook(string prompt, Dictionary<string, double> menu, ArrayList cartBook, ArrayList cartPrice)
       // {
       //     Console.WriteLine(prompt);
       //
       //     bool validbook = false;
       //     while (!validbook)
       //     {
       //         string input = Console.ReadLine();
       //
       //         if (menu.ContainsKey(input))
       //         {
       //             Console.WriteLine($"You chose {input} which costs {menu[input], 0:C}.");
       //             validbook = true;
       //             cartBook.Add(input);
       //             double price = menu[input];
       //             cartPrice.Add(price);
       //         }
       //         else
       //         {
       //             Console.WriteLine("Invalid input. Please enter the book title exactly as displayed.");
       //             validbook = false;
       //         }
       //     }
       // }

        private static double GetTotal(ArrayList cartBook, ArrayList cartPrice, ref int items)
        {
            double totalPrice = 0;
            items = 0;
            for (int i = 0; i < cartPrice.Count; i++)
            {
                totalPrice += Convert.ToDouble(cartPrice[i]);
                items++;
            }
            return totalPrice;
        }

        private static bool DoAgain(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            if (input == "y" || input == "yes")
            {
                return true;
            }
            else if(input == "n" || input == "no")
            {
                return false;
            }
            else
            {
                Console.Write("Invaid input. ");
                return DoAgain(prompt);
            }
        }
    }
}
