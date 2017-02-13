using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace MathsLab.Echantillonage
{
    [TestClass]
    public class SampleSimulator
    {
        private static TestContext _testContext;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            _testContext = context;
        }

        [TestMethod]
        public void Assert_Random_Works()
        {
            var rnd = new Random();

            int x;
            for (int i = 0; i < 500; i++)
            {
                x = rnd.Next(0, 1000);
                _testContext.WriteLine(x.ToString());
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x <= 1000);
            }

        }

        [TestMethod]
        public void Assert_Ranges_Are_Correctly_Set()
        {
            var events = new List<Event>()
            {
                new Event("a",new Probability(1,3)),
                new Event("b",new Probability(1,3)),
                new Event("c",new Probability(1,3)),
            };

            var d = new Drawing(events);


            Assert.AreEqual(d.Ranges.Count, 3);
            var index = 0;
            foreach (var r in d.Ranges)
            {
                _testContext.WriteLine($"Min {r.Min} Max {r.Max}");
                Assert.AreEqual(r.Event, events[index]);
                index++;
            }
        }


        /// <summary>
        /// 2nd SEQUENCE 8 EX 10
        /// </summary>
        [TestMethod]
        public void Exercice10()
        {

            var nombreEchantillons = 10;
            var nombreTirages = 20;

            for (int i = 0; i < nombreEchantillons; i++)
            {
                var events = new List<Event>()
                    {
                        new Event("Toucher la cible",new Probability(8,10)),
                        new Event("Ne pas Toucher la cible",new Probability(2,10)),
                    };

                var d = new Drawing(events);

                for (int j = 0; j < nombreTirages; j++)
                {
                    d.Start();
                }
                _testContext.WriteLine($"Resultats de l'Echantillon {i}");
                foreach (var e in d.Events)
                {
                    _testContext.WriteLine
                      (
                        $"Name : {e.Name} Probability : {e.Probability.Numerator}/{e.Probability.Denominator} SampleSize : {e.SampleSize} Frequency : {(double)e.SampleSize / (double)nombreTirages}"
                      );
                }
            }





        }

        [TestMethod]
        public void Exercice11()
        {

            var nombreEchantillons = 10;
            var nombreTirages = 1000000;
            List<Event> events;
            Drawing d;
            for (int i = 0; i < nombreEchantillons; i++)
            {
                events = new List<Event>()
                    {
                        new Event("Tirer une boule blanche",new Probability(3,5)),
                        new Event("Tirer une boule rouge",new Probability(1,5)),
                        new Event("Tirer une boule noire",new Probability(1,5))
                    };

                d = new Drawing(events);

                for (int j = 0; j < nombreTirages; j++)
                {
                    d.Start();
                }
                _testContext.WriteLine($"Resultats de l'Echantillon {i}");
                foreach (var e in d.Events)
                {
                    _testContext.WriteLine
                      (
                        $"Name : {e.Name} Probability : {e.Probability.Numerator}/{e.Probability.Denominator} SampleSize : {e.SampleSize} Frequency : {(double)e.SampleSize / (double)nombreTirages}"
                      );
                }
            }





        }

        [TestMethod]
        public void Exercice12()
        {

            var nombreEchantillons = 10;
            var nombreTirages = 1000000;
            List<Event> events;
            Drawing d;
            for (int i = 0; i < nombreEchantillons; i++)
            {
                events = new List<Event>()
                    {
                        new Event("Obtenir 2",new Probability(1,16)),   // 11
                        new Event("Obtenir 3",new Probability(2,16)),   // 12 21
                        new Event("Obtenir 4",new Probability(3,16)),   // 13 31 22
                        new Event("Obtenir 5",new Probability(4,16)),   // 14 41 23 32 
                        new Event("Obtenir 6",new Probability(3,16)),   // 24 42 33
                        new Event("Obtenir 7",new Probability(2,16)),   // 34 43
                        new Event("Obtenir 8",new Probability(1,16)),   // 44
                    };

                d = new Drawing(events);

                for (int j = 0; j < nombreTirages; j++)
                {
                    d.Start();
                }
                _testContext.WriteLine($"Resultats de l'Echantillon {i}");
                foreach (var e in d.Events)
                {
                    _testContext.WriteLine
                      (
                        $"Name : {e.Name} Probability : {e.Probability.Numerator}/{e.Probability.Denominator} SampleSize : {e.SampleSize} Frequency : {(double)e.SampleSize / (double)nombreTirages}"
                      );
                }
            }

        }

        [TestMethod]
        public void Exercice13()
        {
            var chanel1MinProba = Sample95pctRule.GetMinProbabilityFromFrequency((double)31 / (double)100, 1000);
            var chanel2MinProba = Sample95pctRule.GetMinProbabilityFromFrequency((double)40 / (double)100, 144);

            _testContext.WriteLine($"Selon la regle des 95% La probabilité qu'un auditeur regarde la chaine 1 se situe entre");
            _testContext.WriteLine($"Min : {Sample95pctRule.GetMinProbabilityFromFrequency((double)31/ (double)100,1000).ToString()}");
            _testContext.WriteLine($"Max : {Sample95pctRule.GetMaxProbabilityFromFrequency((double)31 / (double)100, 1000).ToString()}");
            
            _testContext.WriteLine($"Selon la regle des 95% La probabilité qu'un auditeur regarde la chaine 2 se situe entre");
            _testContext.WriteLine($"Min : {Sample95pctRule.GetMinProbabilityFromFrequency((double)40 / (double)100, 144).ToString()}");
            _testContext.WriteLine($"Max : {Sample95pctRule.GetMaxProbabilityFromFrequency((double)40 / (double)100, 144).ToString()}");

            if (chanel1MinProba > chanel2MinProba)
            {
                _testContext.WriteLine("La probabilité qu'un auditeur regarde la chaine 1 est plus elevée");
            }
            else
            {
                _testContext.WriteLine("La probabilité qu'un auditeur regarde la chaine 2 est plus elevée");
            }
        }


    }

    /// <summary>
    /// 
    /// </summary>
    public class Event
    {
        public Event(string n, Probability p)
        {
            Name = n;
            Probability = p;
        }
        public string Name { get; set; }
        public Probability Probability { get; set; }

        //Nombres de fois que l'evenement s'est réalisé.
        public int SampleSize { get; set; }
    }

    /// <summary>
    /// Nice to have reduce the fractions using pgcd
    /// </summary>
    public class Probability
    {
        public Probability(int n, int d)
        {
            Numerator = n;
            Denominator = d;
        }
        public int Numerator { get; set; }
        public int Denominator { get; set; }
    }

    /// <summary>
    /// Tirage
    /// </summary>
    public class Drawing
    {
        public Drawing(List<Event> e)
        {
            Events = e;
            Ranges = new List<Range>();
            Rnd = new Random();
            UniverseSize = GetUniverseSize();
            SetEventsProbabilitiesOnCommonDenominator();
            BindARangeToEachEvent();
        }

        public Random Rnd { get; set; }

        public List<Event> Events { get; set; }

        public List<Range> Ranges { get; set; }

        /// <summary>
        /// nombre d'issues possibles de l'evenement Univers.
        /// </summary>
        public int UniverseSize { get; set; }

        public int GetUniverseSize()
        {
            return Events
                .Select(e => e.Probability.Denominator)
                .Aggregate((a, b) =>
                {
                    return a * b;
                }
            );
        }

        public void SetEventsProbabilitiesOnCommonDenominator()
        {
            Events.ForEach(e =>
            {
                e.Probability.Numerator = e.Probability.Numerator * (UniverseSize / e.Probability.Denominator);
                e.Probability.Denominator = UniverseSize;
            });
        }

        public void BindARangeToEachEvent()
        {
            int currentMin = 0;
            foreach (var e in Events)
            {
                Ranges.Add(new Range(currentMin, e.Probability.Numerator + currentMin, e));
                currentMin += e.Probability.Numerator;
            }
        }

        public void Start()
        {
            var result = Rnd.Next(0, UniverseSize);
            Ranges.FirstOrDefault(r => (r.Min <= result && result < r.Max)).Event.SampleSize++;
        }

        public class Range
        {
            public Range(int min, int max, Event e)
            {
                Min = min;
                Max = max;
                Event = e;
            }

            public Event Event { get; set; }
            public int Min { get; set; }
            public int Max { get; set; }
        }
    }

    public static class Sample95pctRule
    {
        public static double GetMinProbabilityFromFrequency(double frequency , int experiences)
        {
            return frequency - (1 / (Math.Sqrt(experiences)));

        }

        public static double GetMaxProbabilityFromFrequency(double frequency, int experiences)
        {
            return frequency + (1 / (Math.Sqrt(experiences)));
        }
    }

}
