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

            ArrayList cartBooks = new ArrayList();
            ArrayList cartPrices = new ArrayList();

            Console.WriteLine("{0, -5}{1, -45}{2, 10:C}","SKU#", "Book", "Price");
            Console.WriteLine(new string('=', 60));
            int i = 0;
            foreach(KeyValuePair<string, double> book in menu)
            {
                i++;
                Console.WriteLine($"{i + ":", -5}{book.Key, -50}{book.Value, 10:C}");
            }

            Shop(menu, cartBooks, cartPrices, i);

            Checkout(cartBooks, cartPrices);

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
            double averagePrice = GetAverage(totalPrice, items);
            int lowIndex = GetLowestIndex(cartPrice);
            int highIndex = GetHighestIndex(cartPrice);
            Console.WriteLine($"\nYour average item cost was {averagePrice, 0:C}");
            Console.WriteLine($"The most expensive item in your cart is \"{cartBook[highIndex]}\" which costs {cartPrice[highIndex], 0:C}.");
            Console.WriteLine($"The cheapest item in your cart is \"{cartBook[lowIndex]}\" which costs {cartPrice[lowIndex],0:C}.");
        }

        private static void Shop(Dictionary<string, double> menu, ArrayList cartBook, ArrayList cartPrice, int numberOfOptions)
        {
            bool keepShopping = true;
            while(keepShopping)
            {
                GetBook("\nPlease select a book from the list: ", menu, cartBook, cartPrice, numberOfOptions);
                keepShopping = DoAgain("\nWould you like to keep shopping? (y/n): ");
            }
        }

        private static void GetBook(string prompt, Dictionary<string, double> menu, ArrayList cartBook, ArrayList cartPrice, int numberOfOptions)
        {

            bool isValid = false;
            int number = 0;
            while (!isValid)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                isValid = int.TryParse(input, out number);
                if(number < 1 || number > numberOfOptions)
                {
                    Console.Write("Invalid input. ");
                    isValid = false;
                }
            }
            int i = 0;
            foreach(KeyValuePair<string, double> book in menu)
            {
                i++;
                if(i == number)
                {
                    string result = book.ToString();
                    string noBrackets = result.Substring(1, result.Length - 2);
                    string thisBook = noBrackets.Substring(0, noBrackets.IndexOf(','));
                    string price = noBrackets.Substring(noBrackets.IndexOf(',') + 1);
                    double thisPrice = double.Parse(price);
                    cartBook.Add(thisBook);
                    cartPrice.Add(thisPrice);
                    Console.WriteLine($"\nYou chose \"{thisBook}\" which costs {thisPrice,0:C}.");
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

        private static double GetAverage(double price, int items)
        {
            return price / items;
        }

        private static int GetLowestIndex(ArrayList cartPrices)
        {
            int indexer = 0;
            double lowest = 100.0;
            for(int i = 0; i < cartPrices.Count; i++)
            {
                if((double)cartPrices[i] < lowest)
                {
                    lowest = (double)cartPrices[i];
                    indexer = i;
                }
            }
            return indexer;
        }

        private static int GetHighestIndex(ArrayList cartPrices)
        {
            int indexer = 0;
            double highest = 0.0;
            for(int i = 0; i < cartPrices.Count; i++)
            {
                if((double)cartPrices[i] > highest)
                {
                    highest = (double)cartPrices[i];
                    indexer = i;
                }
            }
            return indexer;
        }

        private static bool DoAgain(string prompt)
        {
            Console.Write(prompt);
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
