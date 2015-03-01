using MB.SharpTodo.Core;
using MB.SharpTodo.Core.Application;
using MB.SharpTodo.Core.Domain;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MB.SharpTodo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var kernel = new StandardKernel(new SharpTodoNinjectModule()))
            {
                if (args.Length == 0)
                {
                    WriteUsage();
                    return;
                }

                if (args[0] == "list")
                {
                    var onlyHot = false;

                    foreach (var arg in args.Skip(1))
                    {
                        if (arg == "-hot")
                        {
                            onlyHot = true;
                        }
                        else
                        {
                            WriteUsage();
                            return;
                        }
                    }

                    var service = kernel.Get<ISharpTodoService>();
                    IEnumerable<TodoItem> list = onlyHot
                        ? service.FindHotTodoItems()
                        : service.FindAllTodoItems();
                        
                    WriteList(list.ToList()); // force lazy loading

                }
                else if (args[0] == "add")
                {
                    if (args.Count() != 2)
                    {
                        WriteUsage();
                        return;
                    }

                    var description = args[1];

                    var service = kernel.Get<ISharpTodoService>();
                    
                    service.AddTodoItem(description);
                }

            }
        }

        private static void WriteUsage()
        {
            var executableName = System.IO.Path.GetFileName(Environment.GetCommandLineArgs()[0]);
            System.Console.WriteLine("usage:");
            System.Console.WriteLine("\t{0} list [-hot]", executableName);
            System.Console.WriteLine("\t{0} add <TODO_DESCRIPTION>", executableName);
        }

        private static void WriteList(IEnumerable<TodoItem> list)
        {
            foreach (var todoItem in list)
            {
                System.Console.Out.WriteLine(todoItem);
            }
        }

    }
}
