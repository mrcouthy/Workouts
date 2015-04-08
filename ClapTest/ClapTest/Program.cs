using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLAP;
using CLAP.Validation;

namespace ClapTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Parser.Run<ClapApp, ClapApp2>(args);//use ClapTest.exe ClapApp.foo -bar:a -count:3
            Parser.Run<TClap>(args);
          //  Parser.Run<ClapApp>(args);
        }

        public enum OptionEnum
        {
            a, b, Option2
        }

        class ClapApp
        {
            //default so ClapTest.exe -bar:hello -count:9 works
            //no need of ClapTest.exe foo -bar:hello -count:9
            [Verb(IsDefault = true)]
            public static void Foo(string bar, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine("This parser kicks {0}", bar);
                }
            }


            [Verb]
            public static void Foo1(
                [Parameter(Required = true)] string bar,
                [Parameter(Default = 15)] int count
                )
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine("This parser kicks {0}", bar);
                }
            }

            [Empty]
            public static void Apple()
            {
                Console.WriteLine("yellow");
            }

            //[Empty, Help] use this if help to be displayed on empty
            [Help]
            public static void Help(string help)
            {
                // this is an empty handler that prints
                // the automatic help string to the console.

                Console.WriteLine(help);
            }

            //eg ClapTest.exe -Log:a
            [Global]
            public static void Log(string message)
            {
                Console.WriteLine("Log");
            }

            //eg ClapTest.exe foo /Debug
            [Global(Aliases = "d", Description = "Launch a debugger")]
            public static void Debug()
            {
                // this is a global parameter handler.
                // it works for any verb.
                Console.WriteLine("Debug");
                Debugger.Launch();
            }

            [Verb]
            public static void Print(string text, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(text);
                }
            }

            [Verb]
            public static void Foo(
                [Parameter(Aliases = "t", Description = "A string parameter with an additional alias")]
        string text,

                [Parameter(Default = 5, Description = "An int parameter with a default")]
        int number,

                [MoreThan(10)]
        [LessThan(100)]
        [Parameter(Default = 42.3, Description = "A double parameter with validation and a default value")]
        double percent,

        [Parameter(Description = "A bool parameter, which can be used as a switch")]
        bool verbose,

        [Parameter(Description = "An enum parameter")]
        OptionEnum option,

        [Parameter(Description = "An array of strings")]
        string[] array)
            {
                Console.WriteLine("text = {0}", text);
                Console.WriteLine("number = {0}", number);
                Console.WriteLine("percent = {0}", percent);
                Console.WriteLine("verbose = {0}", verbose);
                Console.WriteLine("option = {0}", option);
                Console.WriteLine("array = [{0}]", string.Join(",", array));
            }

            [Error]
            public static void HandleError(ExceptionContext context)
            {
                Console.WriteLine("Hola hoho Error :" + context.Exception.Message);
            }

        }

        internal class ClapApp2
        {
            //Auto Alias: ClapTest.exe foo -b:aaa -c:20 works too
            //default so ClapTest.exe -bar:hello -count:9 works
            //no need of ClapTest.exe foo -bar:hello -count:9
            [Verb(IsDefault = true)]
            public static void Foo(string bar, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine("Ola oho Clap2 {0}", bar);
                }
            }
        }

        public class TClap
        {
            [Empty]
            public static void Foo()
            {
                Console.WriteLine("T3");
            }

            /// <summary>
            /// .exe -workingpath:a
            /// </summary>
            /// <param name="workingpath"></param>
            [Verb(IsDefault = true)]
            public static void Foo(string workingpath)
            {
                Console.WriteLine("WorkingFolder is {0}", workingpath);
            }

            /// <summary>
            /// .exe Append
            /// </summary>
            [Verb]
            public static void Append()
            {
                Console.WriteLine("T3 append");
            }


            [Verb]
            public static void test(string workingpath)
            {
                Console.WriteLine("WorkingFolder is {0}", workingpath);
            }


            [Error]
            public static void HandleError(ExceptionContext context)
            {
                Console.WriteLine("Use help option eg '*.exe help' for Help. Err :" + context.Exception.Message);
            }


            //[Empty, Help] use this if help to be displayed on empty
            [Help]
            public static void Help(string help)
            {
                // this is an empty handler that prints
                // the automatic help string to the console.
                Console.WriteLine(help);
            }

        }
    }
}
