using System;

namespace Take5groceries
{
    class Program
    {
        static void Main(string[] args)
        {
            string exit = "n";
            Item item = new Item();
            Cart cart = new Cart();
            cart.newCart();
            Console.WriteLine("Press enter to continue...or type 'exit' to exit at any time");
            exit = Console.ReadLine();
            while (exit.Contains("exit") == false)
            {
                Console.WriteLine("Welcome to Take 5 Groceries!\n\n");
                string iName;
                double iPrice;
                int i = 0;
                    while(i < 5 && exit.Equals("exit") == false)
                {
                    string temp;
                    Console.WriteLine("Please enter an item name : ");
                    temp = Console.ReadLine();
                    if (temp.Contains("exit"))
                    {
                        exit = "exit";
                        break;
                    }
                    iName = temp;
                    Console.WriteLine("Please enter the price of the " + iName +" : ");
                    try
                    {
                        temp = Console.ReadLine();
                        if (temp.Contains("exit"))
                        {
                            exit = "exit";
                            break;
                        }
                        iPrice = Convert.ToDouble(temp);
                        item.newItem(iName, iPrice, i, cart);
                        i++;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Oops, it seems as if you have entered an invalid price, please try again");
                        i--;
                    }
                }
                if (exit.Contains("exit")==false)
                {
                    Console.WriteLine(cart.ToString());
                }
                Console.WriteLine("\nWould you like to continue ? y/n : ");
                exit = Console.ReadLine();
                if (exit.Contains("n"))
                {
                    exit = "exit";
                    break;
                }
            }
        }
    }
    public class Item
    {
        public void newItem(string itmName, double itmPrice, int index, Cart cart)
        {
            cart.addToCart(itmName, itmPrice, index);
        }
    }
    public class Cart
    {
        string[] itemNames = new string[5];
        double[] itemPrices = new double[5];
        double total;
        double vatRate;
        double totalVat;
        public void newCart()
        {
            totalVat = 0;
            total = 0;
            vatRate = 0.14;
            for (int i = 0; i < 5; i++)
            {
                itemNames[i] = "";
                itemPrices[i] = 0;
            }
        }
        public void addToCart(string itemName, double itemPrice, int index)
        {
            itemNames[index] = itemName;
            itemPrices[index] = itemPrice;
            total = total + itemPrice;
            totalVat = total * vatRate;
        }
        public override string ToString()
        {
            totalVat = Math.Round((totalVat*100.0))/100.0;
            string output = "\n\nThank you, here is your cart details :\n";
            for (int i = 0; i < 5; i++)
            {
                output = output + itemNames[i] + "\t\t: R " + itemPrices[i] + "\n";
            }
            output = output + "----------------------\n";
            output = output + "Total       : R " + total + "\n";
            output = output + "Total VAT   : R " + totalVat + "\n";
            output = output + "----------------------\n";
            output = output + "Grand Total : R\t" + (totalVat + total )+ "\n";
            return output;
        }
    }
}
