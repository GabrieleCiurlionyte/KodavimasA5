using KodavimasA5.Helpers;

namespace KodavimasA5
{
    internal class Program
    {
        private static readonly Random random = new();

        static void Main(string[] args)
        {
            // Start the initial coding task
            ConsoleWriteHelper.StartCodingTask(random);

            string input;
            bool isUserContinuing = true;

            do
            {
                Console.WriteLine("Do you want to try again a new scenario?");
                Console.WriteLine("If yes: type 'y', if no: type 'n'");
                input = Console.ReadLine();

                if (input == "y" || input == "n")
                {
                    if (input == "n")
                    {
                        isUserContinuing = false;
                    }
                    else
                    {
                        ConsoleWriteHelper.StartCodingTask(random);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                }
            } while (isUserContinuing);

            Console.WriteLine("Task ended");
        }
    }
}
