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
            var chanel1MaxProba = Sample95pctRule.GetMaxProbabilityFromFrequency((double)31 / (double)100, 1000);
            var chanel2MinProba = Sample95pctRule.GetMinProbabilityFromFrequency((double)40 / (double)100, 144);
            var chanel2MaxProba = Sample95pctRule.GetMaxProbabilityFromFrequency((double)40 / (double)100, 144);
            _testContext.WriteLine($"Selon la regle des 95% La probabilité qu'un auditeur regarde la chaine 1 se situe entre");
            _testContext.WriteLine($"Min : {chanel1MinProba.ToString()}");
            _testContext.WriteLine($"Max : {chanel1MaxProba.ToString()}");

            _testContext.WriteLine($"Selon la regle des 95% La probabilité qu'un auditeur regarde la chaine 2 se situe entre");
            _testContext.WriteLine($"Min : {chanel2MinProba.ToString()}");
            _testContext.WriteLine($"Max : {chanel2MaxProba.ToString()}");

            if (chanel2MaxProba < chanel1MinProba)
            {
                _testContext.WriteLine("La probabilité qu'un auditeur regarde la chaine 1 est plus elevée");
            }
            else if (chanel1MaxProba < chanel2MinProba)
            {
                _testContext.WriteLine("La probabilité qu'un auditeur regarde la chaine 2 est plus elevée");
            }
            else
            {
                _testContext.WriteLine("Il est impossible de savoir quelle chaine a la plus grande probabilité d'etre regardée");
            }
        }

        [TestMethod]
        public void Exercice14()
        {
            var sampleSize = 100;
            var p = new Probability(51, 100);

            var boyMinFrequency = Sample95pctRule.GetMinFrequencyFromProbability((double)p.Numerator / (double)p.Denominator, sampleSize);
            var boyMaxFrequency = Sample95pctRule.GetMaxFrequencyFromProbability((double)p.Numerator / (double)p.Denominator, sampleSize);

            _testContext.WriteLine($"Selon la regle des 95% un echantillon de taille {sampleSize} est considéré valable si la frequence de l'issue Avoir un Garcon est située entre:");
            _testContext.WriteLine($"Min Frequency : {boyMinFrequency.ToString()} Min Size {boyMinFrequency * sampleSize}");
            _testContext.WriteLine($"Max Frequency : {boyMaxFrequency.ToString()} Max Size {boyMaxFrequency * sampleSize}");


            sampleSize = 132;
            boyMinFrequency = Sample95pctRule.GetMinFrequencyFromProbability((double)p.Numerator / (double)p.Denominator, sampleSize);
            boyMaxFrequency = Sample95pctRule.GetMaxFrequencyFromProbability((double)p.Numerator / (double)p.Denominator, sampleSize);

            _testContext.WriteLine($"Selon la regle des 95% un echantillon de taille {sampleSize} est considéré valable si la frequence de l'issue Avoir un Garcon est située entre:");
            _testContext.WriteLine($"Min Frequency : {boyMinFrequency.ToString()} Min Size {boyMinFrequency * sampleSize}");
            _testContext.WriteLine($"Max Frequency : {boyMaxFrequency.ToString()} Max Size {boyMaxFrequency * sampleSize}");


            var villageBoyFrequency = (double)56 / (double)sampleSize;
            var reserveBoyFrequency = (double)46 / (double)sampleSize;

            //  POUR UNE PROBABILITE ET UNE TAILLE D ECHANTILLON DONNÉES
            //  SI ON CREE UN GRAND NOMBRE D ECHANTILLONS ON VERRA QUE
            //  95% DES FREQUENCES OBTENUES SE SITUENT DANS UNE PLAGE
            //  5% DES FREQUENCES OBTENUES SE SITUENT HORS PLAGE

            //  ON NOUS PRESENTE UN ECHANTILLON HORS PLAGE IL A DONC 5% DE CHANCES D ETRE VALIDE
            //  ON AFFIRME DONC QU IL EST INVALIDE AVEC UNE POSSIBILITÉ D ERREUR DE 5%

            _testContext.WriteLine($"On peut dire avec 5% de risque");
            if (boyMinFrequency <= villageBoyFrequency && villageBoyFrequency <= boyMaxFrequency)
            {
                _testContext.WriteLine($"L'echantillon concernant le nombre de garcons nés dans le village est valide");
            }
            else
            {
                _testContext.WriteLine($"L'echantillon concernant le nombre de garcons nés dans le village est invalide");
            }

            if (boyMinFrequency <= reserveBoyFrequency && reserveBoyFrequency <= boyMaxFrequency)
            {
                _testContext.WriteLine($"L'echantillon concernant le nombre de garcons nés dans la reserve est valide");
            }
            else
            {
                _testContext.WriteLine($"L'echantillon concernant le nombre de garcons nés dans la reserve est invalide");
            }

        }

        [TestMethod]
        public void Exercice15()
        {
            var sampleSize = 50;
            var p = new Probability(20, 100);

            var anomalyMinFrequency = Sample95pctRule.GetMinFrequencyFromProbability((double)p.Numerator / (double)p.Denominator, sampleSize);
            var anomalyMaxFrequency = Sample95pctRule.GetMaxFrequencyFromProbability((double)p.Numerator / (double)p.Denominator, sampleSize);

            _testContext.WriteLine($"Selon la regle des 95% un echantillon de taille {sampleSize} est considéré valable si la frequence de l'issue Avoir une anomalie est située entre:");
            _testContext.WriteLine($"Min Frequency : {anomalyMinFrequency.ToString()} Min Size {anomalyMinFrequency * sampleSize}");
            _testContext.WriteLine($"Max Frequency : {anomalyMaxFrequency.ToString()} Max Size {anomalyMaxFrequency * sampleSize}");


            var observedAnomalyFrequency = (double)13 / (double)sampleSize;

            //  POUR UNE PROBABILITE ET UNE TAILLE D ECHANTILLON DONNÉES
            //  SI ON CREE UN GRAND NOMBRE D ECHANTILLONS ON VERRA QUE
            //  95% DES FREQUENCES OBTENUES SE SITUENT DANS UNE PLAGE
            //  5% DES FREQUENCES OBTENUES SE SITUENT HORS PLAGE

            //  ON NOUS PRESENTE UN ECHANTILLON HORS PLAGE IL A DONC 5% DE CHANCES D ETRE VALIDE
            //  ON AFFIRME DONC QU IL EST INVALIDE AVEC UNE POSSIBILITÉ D ERREUR DE 5%

            _testContext.WriteLine($"On peut dire avec 5% de risque");
            if (anomalyMinFrequency <= observedAnomalyFrequency && observedAnomalyFrequency <= anomalyMaxFrequency)
            {
                _testContext.WriteLine($"L'echantillon concernant le nombre de peintures ayant un defaut est valide");
            }
            else
            {
                _testContext.WriteLine($"L'echantillon concernant le nombre de peintures ayant un defaut est invalide");
            }

            _testContext.WriteLine($"On pourrait dire avec 5% de risque");
            _testContext.WriteLine($"L'echantillon de {sampleSize} vehicules concernant le nombre de peintures ayant un defaut est invalide");
            _testContext.WriteLine($"Si le nombre de default excede {anomalyMaxFrequency * sampleSize} "); // 18 vehicules

        }

        [TestMethod]
        public void Exercice16()
        {
            var sampleSize = 100;
            var f = new Frequency(75, 100);
            var modalite = "guerir de la maladie";

            var recoveryMinProbability = Sample95pctRule.GetMinProbabilityFromFrequency((double)f.Numerator / (double)f.Denominator, sampleSize);
            var recoveryMaxProbability = Sample95pctRule.GetMaxProbabilityFromFrequency((double)f.Numerator / (double)f.Denominator, sampleSize);

            _testContext.WriteLine($"Selon la regle des 95% dans echantillon de taille {sampleSize} si la frequence observée de l'issue {modalite} est de {(double)f.Numerator / (double)f.Denominator} la probabilité de {modalite} est située entre:");
            _testContext.WriteLine($"Min Probability : {recoveryMinProbability.ToString()} Min Size {recoveryMinProbability * sampleSize}");
            _testContext.WriteLine($"Max Probability : {recoveryMaxProbability.ToString()} Max Size {recoveryMaxProbability * sampleSize}");


            var sampleSize2 = 100;
            var f2 = new Frequency(65, 100);
            var modalite2 = "guerir de la maladie";

            var recoveryMinProbability2 = Sample95pctRule.GetMinProbabilityFromFrequency((double)f2.Numerator / (double)f2.Denominator, sampleSize2);
            var recoveryMaxProbability2 = Sample95pctRule.GetMaxProbabilityFromFrequency((double)f2.Numerator / (double)f2.Denominator, sampleSize2);

            _testContext.WriteLine($"Selon la regle des 95% dans echantillon de taille {sampleSize2} si la frequence observée de l'issue {modalite2} est de {(double)f2.Numerator / (double)f2.Denominator} la probabilité de {modalite2} est située entre:");
            _testContext.WriteLine($"Min Probability : {recoveryMinProbability2.ToString()} Min Size {recoveryMinProbability2 * sampleSize2}");
            _testContext.WriteLine($"Max Probability : {recoveryMaxProbability2.ToString()} Max Size {recoveryMaxProbability2 * sampleSize2}");



            _testContext.WriteLine("Premier essai");

            if (recoveryMaxProbability2 < recoveryMinProbability)
            {
                _testContext.WriteLine("Le medicament 1 est plus efficace");
            }
            else if (recoveryMaxProbability < recoveryMinProbability2)
            {
                _testContext.WriteLine("Le medicament 2 est plus efficace");
            }
            else
            {
                _testContext.WriteLine("Il est impossible de savoir quel médicament est le plus efficace");
            }


            _testContext.WriteLine("Second essai");

            sampleSize = 1024;
            f = new Frequency(755, 1024);
            modalite = "guerir de la maladie";

            recoveryMinProbability = Sample95pctRule.GetMinProbabilityFromFrequency((double)f.Numerator / (double)f.Denominator, sampleSize);
            recoveryMaxProbability = Sample95pctRule.GetMaxProbabilityFromFrequency((double)f.Numerator / (double)f.Denominator, sampleSize);

            _testContext.WriteLine($"Selon la regle des 95% dans echantillon de taille {sampleSize} si la frequence observée de l'issue {modalite} est de {(double)f.Numerator / (double)f.Denominator} la probabilité de {modalite} est située entre:");
            _testContext.WriteLine($"Min Probability : {recoveryMinProbability.ToString()} Min Size {recoveryMinProbability * sampleSize}");
            _testContext.WriteLine($"Max Probability : {recoveryMaxProbability.ToString()} Max Size {recoveryMaxProbability * sampleSize}");

            sampleSize2 = 1024;
            f2 = new Frequency(690, 1024);
            modalite2 = "guerir de la maladie";

            recoveryMinProbability2 = Sample95pctRule.GetMinProbabilityFromFrequency((double)f2.Numerator / (double)f2.Denominator, sampleSize2);
            recoveryMaxProbability2 = Sample95pctRule.GetMaxProbabilityFromFrequency((double)f2.Numerator / (double)f2.Denominator, sampleSize2);

            _testContext.WriteLine($"Selon la regle des 95% dans echantillon de taille {sampleSize2} si la frequence observée de l'issue {modalite} est de {(double)f2.Numerator / (double)f2.Denominator} la probabilité de {modalite2} est située entre:");
            _testContext.WriteLine($"Min Probability : {recoveryMinProbability2.ToString()} Min Size {recoveryMinProbability2 * sampleSize2}");
            _testContext.WriteLine($"Max Probability : {recoveryMaxProbability2.ToString()} Max Size {recoveryMaxProbability2 * sampleSize2}");

            if (recoveryMaxProbability2 < recoveryMinProbability)
            {
                _testContext.WriteLine("Le medicament 1 est plus efficace");
            }
            else if (recoveryMaxProbability < recoveryMinProbability2)
            {
                _testContext.WriteLine("Le medicament 2 est plus efficace");
            }
            else
            {
                _testContext.WriteLine("Il est impossible de savoir quel médicament est le plus efficace");
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

    public class Frequency
    {
        public Frequency(int n, int d)
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
        public static double GetMinProbabilityFromFrequency(double frequency, int experiences)
        {
            return frequency - (1 / (Math.Sqrt(experiences)));

        }

        public static double GetMaxProbabilityFromFrequency(double frequency, int experiences)
        {
            return frequency + (1 / (Math.Sqrt(experiences)));
        }

        public static double GetMinFrequencyFromProbability(double probablility, int experiences)
        {
            return probablility - (1 / (Math.Sqrt(experiences)));

        }

        public static double GetMaxFrequencyFromProbability(double probablility, int experiences)
        {
            return probablility + (1 / (Math.Sqrt(experiences)));
        }
    }

}
