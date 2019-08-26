using System;
using NewsOfCSharp8.defaultinterfacesmember.Implementations;
using NewsOfCSharp8.defaultinterfacesmember.Interfaces;
using NewsOfCSharp8.patternmatching.Models;
using NewsOfCSharp8.readolymember.Models;

namespace NewsOfCSharp8
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReadOnlyMember();
            //DefaultMembersInterfaces();
            StaticMembersInterfaces();
        }

        /// <summary>
        /// Test di Read Only Member
        /// </summary>
        private static void ReadOnlyMember()
        {
            PointOld oldPoint = new PointOld()
            {
                X = 5,
                Y = 6
            };

            Console.WriteLine($"oldPoint.X: {oldPoint.X}"); // risulatto atteso 5 e stampa 5
            Console.WriteLine($"oldPoint.Y: {oldPoint.Y}"); // risulatto atteso 6 e stampa 6

            // l'override del metodo ToString (che non è readonly) modifica un valore della struttura PointOld
            // generando un valore inatteso
            Console.WriteLine($"oldPoint.ToString(): {oldPoint.ToString()}");

            Console.WriteLine($"oldPoint.X: {oldPoint.X}"); // risulatto atteso 5 e stampa 10
            Console.WriteLine($"oldPoint.Y: {oldPoint.Y}"); // risulatto atteso 6 e stampa 6


            PointNew newPoint = new PointNew()
            {
                X = 5,
                Y = 6
            };

            Console.WriteLine($"newPoint.X: {newPoint.X}"); // risulatto atteso 5 e stampa 5
            Console.WriteLine($"newPoint.Y: {newPoint.Y}"); // risulatto atteso 6 e stampa 6

            // l'override del metodo ToString (che èreadonly) non puo' modificare i valori della struttura
            // perchè genererebbe un errore di compilazione
            Console.WriteLine($"newPoint.ToString(): {newPoint.ToString()}");

            Console.WriteLine($"newPoint.X: {newPoint.X}"); // risulatto atteso 5 e stampa 5
            Console.WriteLine($"newPoint.Y: {newPoint.Y}"); // risulatto atteso 6 e stampa 6


        }

        private static void DefaultMembersInterfaces()
        {
            // DefaultMember non contiene la ridefinizione di DefaultMethod
            IDefaultMember defaultMemberInterface = new DefaultMember();

            // FullImplementation contiene la ridefinizione di DefaultMethod
            IDefaultMember fullImplementation = new FullImplementation();

            // come intuitiuvamente atteso lanciare questo metodo equivale a lanciare il metodo di default "definito nell'interfaccia" (fa ancora strano scriverlo)
            defaultMemberInterface.DefaultMethod("ciao mondo-default");

            // fullimplementation invece rifedinisce il metodo DefaultMethod e quindi viene lanciata la ridefinizione di DefaultMethod
            fullImplementation.DefaultMethod("ciao mondo-ridefinita");
        }

        private static void StaticMembersInterfaces()
        {
            IStaticMethodInterface staticDefault = new StaticMethodInterfaceDefault();
            IStaticMethodInterface staticImplementation = new StaticMethodInterfaceOther();

            staticDefault.PrintHello(); // print hello world
            staticImplementation.PrintHello(); // print hello world

            IStaticMethodInterface.ChangeHello("Ciao mondo");

            staticDefault.PrintHello(); // print ciao mondo
            staticImplementation.PrintHello(); // print ciao mondo
        }



        private static string PatternMatchingSwitch(LoggingLevel loggingLevel)
            => loggingLevel switch
            {

                LoggingLevel.Alert => "Alert",
                LoggingLevel.Warning => "Warning",
                LoggingLevel.Info => "Info",
                LoggingLevel.Debug => "Debug",
                _ => "Other"
            };


        private static string GetStateName(Address address)
            => address switch
            {
                { State: "IT", City: "Rome" } => "Italia"
            };

        private static string GetStateNameEx(Address address)
        {
            switch (address)
            {
                case { State: "IT", City: "Rome" }:
                    return "Italia;";
                default:
                    return "default";
            }
        }




    }
}