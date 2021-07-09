using System;
using System.Collections.Generic;

namespace lavenshteinTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string cevap = "";

            int tmpCounter = 0;
            Console.Write("İlk cümleyi girin: ");
            string ilkCumle = Console.ReadLine();

            List<string> cevaplar = new List<string>();

            List<string> cumleler = new List<string>();
            cumleler.Add("Merhaba");
            cumleler.Add("İletişim numarası");
            cumleler.Add("Mail adresi");
            cumleler.Add("Adın nedir");
            cumleler.Add("Muhasebe nedir?");
            cumleler.Add("Muhasebe hakkında bilgi almak");
            cumleler.Add("Hesap planı nedir");
            cumleler.Add("Muhasebe hakkında bilgi istiyorum");

            for (int i = 0; i < cumleler.Count; i++)
            {
                int mesafe = 0;
                int counter = 0;
                int mainCounter = 0;
                string[] words1 = ilkCumle.Split(' ', '.', ',');
                string[] words2 = cumleler[i].Split(' ', '.', ',');
                foreach (var word in words2)
                {
                    foreach (string word1 in words1)
                    {
                        mesafe = distance(word1.ToLower(), word.ToLower());
                        if (mesafe <= 1)
                        {
                            counter++;
                            Console.WriteLine("word1 : " + word1 + " - word2 : " + word + " - Mesafe : " + mesafe);
                        }
                    }

                    if (counter > 0)
                    {
                        mainCounter++;
                        counter = 0;
                    }
                }

                if (mainCounter > 0)
                {
                    Console.WriteLine("Benzer cümleler : " + ilkCumle + " - " + cumleler[i] + " - Main counter : " + mainCounter);
                    cevaplar.Add(cumleler[i]);
                    if (mainCounter >= tmpCounter)
                    {
                        tmpCounter = mainCounter;
                        cevap = cumleler[i];
                    }
                }

            }

            Console.WriteLine("En yakın cevap : " + cevap);
        }

        static int distance(string cumle1, string cumle2)
        {
            int len1 = cumle1.Length;
            int len2 = cumle2.Length;
            int[,] distanceMatris = new int[len1 + 1, len2 + 1];
            for (int i = 0; i < len1; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    distanceMatris[i, j] = 0;
                }
            }

            for (int i = 1; i <= len1; i++)
            {
                char c1 = cumle1[i - 1];
                for (int j = 1; j <= len2; j++)
                {
                    char c2 = cumle2[j - 1];
                    if (c1 == c2)
                    {
                        distanceMatris[i, j] = distanceMatris[i - 1, j - 1];
                    }
                    else
                    {
                        int del;
                        int add;
                        int subsitute;
                        int minimum;
                        del = distanceMatris[i - 1, j] + 1;
                        add = distanceMatris[i, j - 1] + 1;
                        subsitute = distanceMatris[i - 1, j - 1] + 1;
                        minimum = del;
                        if (add < minimum)
                        {
                            minimum = add;
                        }
                        if (subsitute < minimum)
                        {
                            minimum = subsitute;
                        }
                        distanceMatris[i, j] = minimum;
                    }
                }
            }
            return distanceMatris[len1, len2];
        }
    }
}
