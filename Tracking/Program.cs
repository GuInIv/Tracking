using System;
using System.Text;

namespace Tracking
{
    class Program
    {
        static void Main()
        {
            var commandExecutor = new CommandExecutor();
            var data = new StringBuilder();
            while (true)
            {
                Console.WriteLine("Чтобы выйти из программы введите :q");
                string inputString = Console.ReadLine();
                if (inputString == ":q")
                    break;

                if (commandExecutor.IsCommandAdd(inputString))
                {
                    while (inputString != ":q")
                    {
                        while (inputString != ":t")
                        {
                            data.AppendLine(inputString);
                            inputString = Console.ReadLine();
                        }
                        commandExecutor.InputData = data.ToString();
                        data.Clear();
                        commandExecutor.Execute();
                        Console.WriteLine("Чтобы выйти из цикла добавления пользователей введите :q");
                        inputString = Console.ReadLine();
                    }
                }
                else
                {
                    commandExecutor.InputData = inputString;
                    commandExecutor.Execute();
                }
            }
        }
    }
}