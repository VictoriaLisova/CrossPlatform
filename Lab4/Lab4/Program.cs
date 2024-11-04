using System;
using System.IO;
using System.Reflection;
using System.Text;
using Lab4Classlib;
using McMaster.Extensions.CommandLineUtils;

public class Program
{
    public static void Main(string[] args)
    {
        var app = new CommandLineApplication();
        app.Name = "Lab4";
        app.Description = "This is an application for lab 4";
        app.HelpOption("-h|--help");

        var rootDir = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent;

        var pathInputFileLab1 = CreatePath(Path.Combine(rootDir.FullName, "Lab1", "bin", "Debug", "net6.0", "input"), "input_1.txt");
        var pathInputFileLab2 = CreatePath(Path.Combine(rootDir.FullName, "Lab2", "bin", "Debug", "net6.0", "input"), "input_1.txt");
        var pathInputFileLab3 = CreatePath(Path.Combine(rootDir.FullName, "Lab3", "bin", "Debug", "net6.0", "input"), "input.txt");

        // команда для виведення автора та версії
        app.Command("version", (command) =>
        {
            command.Description = "Info about author and version";
            command.HelpOption("-h|--help");

            command.OnExecute(() =>
            {
                var authorAttr = Assembly.GetExecutingAssembly().GetCustomAttributes<AssemblyCompanyAttribute>().ToArray()[0];
                var author = authorAttr.Company ?? "Author";
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                Console.WriteLine($"Author = {author}\nVersion = {version}");
                return 0;
            });
        });

        // команда запуску з обов'язковими lab1, lab2 або lab3
        app.Command("run", (command) =>
        {
            command.Description = "Run lab";
            command.HelpOption("-h|--help");
            var labNameOption = command.Argument("[name] <optional>", "Lab name").IsRequired();
            var inputOption = command.Option("-i|--input <inputFileName>", "input file", CommandOptionType.SingleValue);
            var outputOption = command.Option("-o|--output <outputFileName>", "output file", CommandOptionType.SingleValue);

            command.OnExecute(() =>
            {
                var name = !string.IsNullOrWhiteSpace(labNameOption.Value) ? labNameOption.Value : "allowed argument lab1, lab2 or lab3";

                var inputValue = inputOption.Value();
                var outputValue = outputOption.Value();
                string inputPath = "";
                string outputPath = "";
                string pathToEnvFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "env.txt");

                // вибір шляху до файлу з вхідними даними
                if (!string.IsNullOrEmpty(inputValue))
                {
                    inputPath = CreatePath(labNameOption.Value, inputValue);
                }
                else if((string.IsNullOrEmpty(inputValue) || !File.Exists(inputPath)) && File.Exists(pathToEnvFile))
                {
                    using (StreamReader sr = new StreamReader(pathToEnvFile))
                    {
                        string labName = sr.ReadLine().Split('=')[1];
                        if (labName.ToLower() == name.ToLower())
                        {
                            inputPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, labName), "input.txt");
                        }
                    }
                }
                else if ((string.IsNullOrEmpty(inputValue) || !File.Exists(inputPath)) && !File.Exists(pathToEnvFile)
                                && (!File.Exists(pathInputFileLab1) || !File.Exists(pathInputFileLab2) || !File.Exists(pathInputFileLab3)))
                {
                    Console.WriteLine("Can`t find input file");
                }

                outputPath = !string.IsNullOrWhiteSpace(outputValue) ? outputValue : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.txt");

                // запуск лабораторних
                switch (name)
                {
                    case "lab1":
                        RunLab(new Lab1() { PathToInputFile = string.IsNullOrEmpty(inputPath) ? pathInputFileLab1 : inputPath, PathToOutputFile = outputPath });
                        break;
                    case "lab2":
                        RunLab(new Lab2() { PathToInputFile = string.IsNullOrEmpty(inputPath) ? pathInputFileLab2 : inputPath, PathToOutputFile = outputPath });
                        break;
                    case "lab3":
                        RunLab(new Lab3() { PathToInputFile = string.IsNullOrEmpty(inputPath) ? pathInputFileLab3 : inputPath, PathToOutputFile = outputPath });
                        break;
                    default:
                        Console.WriteLine($"Don`t have {name} program to run");
                        return 1;
                }
                return 0;
            });
        });

        // команда шляху до папки
        app.Command("set-path", (command) =>
        {
            command.Description = "Path to folder with input and output files";
            var path = command.Option("-p|--path <pathToFile>", "path to folder with file", CommandOptionType.SingleValue).IsRequired();
            command.OnExecute(() =>
            {
                if (path.HasValue())
                {
                    // встановити змінну середовища
                    if (!File.Exists("env.txt"))
                    {
                        var path_to_file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "env.txt");
                        FileStream fs = File.Create(path_to_file);
                        fs.Close();
                    }
                    using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "env.txt")))
                    {
                        sw.WriteLine($"LAB_PATH={path.Value()}");
                    }
                }
                return 0;
            });
        });

        // запуск програми
        app.OnExecute(() =>
        {
            return 0;
        });

        try
        {
            app.Execute(args);
        }
        catch(CommandParsingException ex)
        {
            Console.WriteLine("Allowed commands");
            Console.WriteLine("version - show author name and package version");
            Console.WriteLine("run lab1, lab2, lab3 <-i|--input> <-o|--output> - lab name to run with option input and output file");
            Console.WriteLine("set-path <-p|--path> - path to folder with input and output files");
        }

        
    }
    
    // формування шляху до файлу
    static string CreatePath(string dir, string file)
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dir, file);
    }

    // запуск лабораторної
    static void RunLab(ILabNumber lab)
    {
        lab.Run();
    }
}
