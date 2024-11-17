﻿using KodavimasA5.Helpers;

ConsoleWriteHelper.StartCodingTask();

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
            ConsoleWriteHelper.StartCodingTask();
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
    }
} while (isUserContinuing);

Console.WriteLine("Task ended");
