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
            menu.Add("Torn Ligaments: The Art of Dismemberment", 79.95);
            menu.Add("Snake Hips", 9.50);
            menu.Add("How To Win Friends And Influence People", 24.95);

            ArrayList cartBook = new ArrayList();
            ArrayList cartPrice = new ArrayList();

            Console.WriteLine("{0, -5}{1, -45}{2, 10}","SKU", "Book", "Price");
            Console.WriteLine("==========================================================================================");
            foreach(KeyValuePair<string, double> book in menu)
            {
                Console.WriteLine($"{book.Key, -50}{book.Value, 10:C}");
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
            Console.WriteLine("==================================================");
            for(int i = 0; i < cartPrice.Count; i++)
            {
                Console.WriteLine($"{cartBook[i],-40}{cartPrice[i],10:C}");
            }
            Console.WriteLine("==================================================");
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

            bool validbook = false;
            while (!validbook)
            {
                string input = Console.ReadLine();

                if (menu.ContainsKey(input))
                {
                    Console.WriteLine($"You chose {input} which costs {menu[input], 0:C}.");
                    validbook = true;
                    cartBook.Add(input);
                    double price = menu[input];
                    cartPrice.Add(price);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter the book title exactly as displayed.");
                    validbook = false;
                }
            }
        }

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
