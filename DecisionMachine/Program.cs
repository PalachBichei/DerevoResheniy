using System.Diagnostics;

namespace DecisionMachine;

public static class Program
{
    static void Main(string[] args)
    {
        heal1:
        Console.Write("Челловек имеет несовместимые со спортом травмы(y/n): ");
        var health1 = Convert.ToBoolean((Console.ReadLine()=="n"));
        heal2:
        Console.Write("Какое главное физическое отличие человека(Если их нет написать отсутсвуют): ");
        var health2 = Convert.ToString(Console.ReadLine());
        tact:
        Console.Write("Человек хорошо разбирается в футболе(y/n):");
        var tactikk = Convert.ToBoolean((Console.ReadLine()=="y"));
        iq:
        Console.Write("Какое IQ имеет человек: ");
        var iqval = Convert.ToInt16(Console.ReadLine());
        prc:
        Console.Write("Сколько планирует зарабатывать игрок($): ");
        var money = Convert.ToInt32(Console.ReadLine());
        
        
        
        
        
       

        Console.WriteLine("----------------------------------------------------");

        var candidate = new Candidate
        {
            Price = money,
            IQ = iqval,
            HealthPok = health2,
            Health = health1,
            Tactik = tactikk,

           
        };

        var decisionTopNode = DecisionTree();

        decisionTopNode.Evaluate(candidate);

        decisionTopNode.PrintTree();


        static DecisionQuery<Candidate> DecisionTree()
        {
            var tactbol = new DecisionQuery<Candidate>
            {
                Title = "Человек силён в тактике:",
                Test = c => c.Tactik,
                Negative = new DecisionResult { Result = false },
                Positive = new DecisionResult { Result = true }
            };
            var tactiq = new DecisionQuery<Candidate>
            {
                Title = "Какое IQ у человека:",
                Test = c => c.IQ>=125,
                Negative = new DecisionResult { Result = false },
                Positive = tactbol
            };

            var needmoney = new DecisionQuery<Candidate>
            {
                Title = "Какую зп запросил игрок:",
                Test = c => c.Price<=100000,
                Negative = new DecisionResult { Result = false },
                Positive = new DecisionResult { Result = true }
            };
            var healtpok = new DecisionQuery<Candidate>
            {
                Title = "У игрока замечены выдающиеся навыки:",
                Test = c => c.HealthPok!="отсутсвуют",
                Negative = tactiq,
                Positive = needmoney
            };



            var healsiris = new DecisionQuery<Candidate>
            {
                Title = "Игрок полностью здоров?:",
                Test = c => c.Health,
                Negative = new DecisionResult { Result = false },
                Positive = healtpok
            };
            
            

            return healsiris;
        }
    }

    static void PrintTree(this DecisionQuery<Candidate> node, int depth = 0)
    {
        Console.WriteLine(new string(' ', depth * 4) + node.Title);
        if (node.Negative is DecisionQuery<Candidate> negative)
        {
            PrintTree(negative, depth + 1);
        }
        else
        {
            Console.WriteLine(new string('-', (depth + 1) * 4) + "Отказ");
        }

        if (node.Positive is DecisionQuery<Candidate> positive)
        {
            PrintTree(positive, depth + 1);
        }
        else
        {
            Console.WriteLine(new string('-', (depth + 1) * 4) + "Идёт");
        }
    }
}