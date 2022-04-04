
using System;

namespace Take5groceries
{
    class Program
    {
        static void Main(string[] args)
        {
            //declerations
            ConsoleKeyInfo cki;
            Item item = new Item();
            Cart cart = new Cart();
            //main menu
            string welcomeMsg = "|==================================|\n" +
                                "| * Welcome to Take 5 Groceries! * |\n" +
                                "|==================================|\n" +
                                "|    Start     |  Press <Enter>    |\n" +
                                "|--------------|-------------------|\n" +
                                "|    Cancel    |  Press <Esc>      |\n" +
                                "|--------------|-------------------|\n" +
                                "|    Exit      |  Press <F4>       |\n" +
                                "|==================================|\n\n" +
                                " Waiting for user input...";

            //creates an empty cart, displays main menu and waits for user input
            cart.newCart();
            Console.WriteLine(welcomeMsg);
            cki = Console.ReadKey(true);

            //while-loop to continuously run program (main logic)
            while (checkExit(cki).Equals("Exit") == false)
            {
                //declare input variables
                string iName = "";
                double iPrice = 0.0;
                //item counter
                int i = 0;

                //while-loop to add 5 items to cart or exit on user demand
                while (i < 5 && checkExit(cki).Equals("Enter") == true)
                {
                    string display;
                    int fillChars = 43;
                    string doubleLine = cart.addSpace(fillChars, "=");
                    //item name
                    display = "|" + doubleLine                            + "|\n" +
                              "|   Please enter item number {0}'s name :     |\n" +
                              "|" + doubleLine                            + "|\n";
                    Console.WriteLine(display, (i + 1));
                    iName = Console.ReadLine();

                    //item price
                    // 36 - base amount of characters to add to iName.Length
                    fillChars = iName.Length + 36;
                    doubleLine = cart.addSpace(fillChars, "=");
                    display = "|" + doubleLine +                                "|\n" +
                              "|   Please enter the price of the " + iName + "   |\n" +
                              "|" + doubleLine +                                "|\n";
                    Console.WriteLine(display);
                    //try-catch for string => double conversion
                    try
                    {
                        string priceInput = Console.ReadLine();
                        iPrice = Convert.ToDouble(priceInput);
                        fillChars = iName.Length + iPrice.ToString().Length + 36;
                        doubleLine = cart.addSpace(fillChars, "=");
                        Console.WriteLine(  "|" + doubleLine +                              "|\n" +
                                            "|  " + iName + " : R " + iPrice + cart.addSpace(29, " ") + "|\n" +
                                            "|" + doubleLine +                              "|\n" +
                                            "|  Press <Enter> to confirm item" + cart.addSpace(iName.Length + iPrice.ToString().Length + 5, " ") + "|\n" +
                                            "|  Press <Spacebar> to redo item" + cart.addSpace(iName.Length + iPrice.ToString().Length + 5, " ") + "|\n" +
                                            "|  Press <Esc> to cancel" + cart.addSpace(iName.Length + iPrice.ToString().Length + 13, " ") + "|\n" +
                                            "|" + doubleLine +                              "|\n");
                        cki = Console.ReadKey(true);
                        if (cki.Key == ConsoleKey.Enter)
                        {
                            //adds item to cart
                            item.newItem(iName, iPrice, i, cart);
                            i++;
                        } else if (cki.Key == ConsoleKey.Spacebar)
                        {
                            Console.WriteLine("Re-doing last item");
                            iPrice = 0.0;
                        } else if (cki.Key == ConsoleKey.Escape)
                        {
                            Console.WriteLine("Do I need to do anything here ?");
                        }
                    }
                    catch (Exception exE)
                    {
                        Console.WriteLine("|=====================================================|\n" +
                                          "|      Invalid Input Error :  See details below       |\n" +
                                          "|=====================================================|\n\n" +
                                          exE + "\n\n" +
                                          "|=========================================================|\n" +
                                          "|   Press <Enter> to re-add last item or <Esc> to cancel  |\n" +
                                          "|=========================================================|\n\n");
                        iPrice = 0.0;
                        cki = Console.ReadKey(true);
                    }
                }
                //checks for exit before output
                if (checkExit(cki).Equals("Enter") == true)
                {
                    //display cart
                    Console.WriteLine(cart.ToString());
                    Console.WriteLine("Press enter to return to main menu...");
                    Console.ReadLine();
                }
                Console.Clear();
                Console.WriteLine(welcomeMsg);
                cki = Console.ReadKey(true);
            }
        }
        public static string checkExit(ConsoleKeyInfo cki)
        {
            if (cki.Key == ConsoleKey.F4)
            {
                return "Exit";
            }
            else if (cki.Key == ConsoleKey.Escape)
            {
                return "Cancel";
            }
            else if (cki.Key == ConsoleKey.Enter)
            {
                return "Enter";
            }
            else if (cki.Key == ConsoleKey.Spacebar)
            {
                return "Enter";
            }
            else
            {
                return "Invalid option selected";
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
        private string[] itemNames = new string[5];
        private double[] itemPrices = new double[5];
        private double total;
        private double vatRate;
        private double totalVat;
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
        public string addSpace(int numOfSpaces, string fill)
        {
            string spaces = "";
            for (int i = 0; i < numOfSpaces; i++)
            {
                spaces = spaces + fill;
            }
            return spaces;
        }
        public override string ToString()
        {
            //receipt code formatted to resemble output
            string fill =                   "|------------------------------------------------|\n";
            string ends =                   "|================================================|\n";
            string output = ends +          "|     Thank you, here are your cart details      |\n" + fill;
            string column1, column2, line = "";
            int totalLineChars = fill.Length - 2;
            int col2Chars = 17;
            int usedChars = 0;
            for (int i = 0; i < 5; i++)
            {
                column1 = "|  " + itemNames[i];
                column2 = itemPrices[i] + "  |";

                usedChars = column2.Length - 2;

                column2 = "|  R" + addSpace((col2Chars - usedChars), ".") + column2;

                usedChars = column1.Length + column2.Length;
                //spacing maths
                int middle = 0;
                if (usedChars > totalLineChars)
                {
                    column1 = column1 + addSpace(totalLineChars - column1.Length, " ") + "|\n|";
                    middle = totalLineChars - column2.Length;
                }
                else
                {
                    middle = totalLineChars - usedChars + 1;
                }
                line = column1 + addSpace(middle, " ") + column2 + "\n";
                output = output + line;
            }
            totalVat = Math.Round((totalVat * 100.0)) / 100.0;
            total = Math.Round((total * 100.0)) / 100.0;
            output = output + fill;
            //total
            line = "|  Total" + addSpace(19, " ") + "|  R";
            output = output + line;
            usedChars = line.Length + (total + "").Length + 2;
            line = addSpace(totalLineChars - usedChars, ".") + total + "  |\n";
            output = output + line;
            //total vat
            line = "|  Total VAT" + addSpace(15, " ") + "|  R";
            output = output + line;
            usedChars = line.Length + (totalVat + "").Length + 2;
            line = addSpace(totalLineChars - usedChars, ".") + totalVat + "  |\n";
            output = output + line + ends;
            //grand total
            total = Math.Round(((totalVat + total) * 100.0)) / 100.0;
            line = "|  Grand Total" + addSpace(13, " ") + "|  R";
            output = output + line; usedChars = line.Length + (total + "").Length + 2;
            line = addSpace(totalLineChars - usedChars, ".") + total + "  |\n";
            output = output + line + ends;
            return output;
        }
    }
}
