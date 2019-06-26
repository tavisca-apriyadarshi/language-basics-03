using System;
using System.Collections.Generic;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int menuLength = protein.Length;
            int[] TotalCalories = new int[menuLength];
            int[] mealPlan = new int[dietPlans.Length];

            //calculating calorie for each item
            for (int i = 0; i < menuLength; i++) {
                TotalCalories[i] = (protein[i] + carbs[i]) * 5 + fat[i] * 9;
            }

            for (int i = 0; i < dietPlans.Length; i++) {
                string plan = dietPlans[i];
    
                if (plan.Length == 0) {
                    mealPlan[i] = 0;
                    continue;
                }

                List<int> indexPositions = new List<int>();

                for (int j = 0; j < menuLength; j++) indexPositions.Add(j);

                int maximum, minimum;
                foreach (char ch in plan) {
                    switch(ch) {
                        case 'P':
                            maximum = FindMax(protein, indexPositions);
                            indexPositions = FindAllIndices(protein, indexPositions, maximum);
                            break;
                        case 'p':
                            minimum = FindMin(protein, indexPositions);
                            indexPositions = FindAllIndices(protein, indexPositions, minimum);
                            break;
                        case 'C':
                            maximum = FindMax(carbs, indexPositions);
                            indexPositions = FindAllIndices(carbs, indexPositions, maximum);
                            break;
                        case 'c':
                            minimum = FindMin(carbs, indexPositions);
                            indexPositions = FindAllIndices(carbs, indexPositions, minimum);
                            break;
                        case 'F':
                            maximum = FindMax(fat, indexPositions);
                            indexPositions = FindAllIndices(fat, indexPositions, maximum);
                            break;
                        case 'f':
                            minimum = FindMin(fat, indexPositions);
                            indexPositions = FindAllIndices(fat, indexPositions, minimum);
                            break;
                        case 'T':
                            maximum = FindMax(TotalCalories, indexPositions);
                            indexPositions = FindAllIndices(TotalCalories, indexPositions, maximum);
                            break;
                        case 't':
                            minimum = FindMin(TotalCalories, indexPositions);
                            indexPositions = FindAllIndices(TotalCalories, indexPositions, minimum);
                            break;
                    }
                }

                mealPlan[i] = indexPositions[0];
            }

            return mealPlan;
        }

        public static List<int> FindAllIndices(int[] arr, List<int> pos, int elem) {
            List<int> indexPositions = new List<int>();

            foreach (int i in pos) {
                if (arr[i] == elem) indexPositions.Add(i);
            }

            return indexPositions;
        }

        public static int FindMax(int[] arr, List<int> indexPositions) {
            if (indexPositions.Count == 1) return arr[indexPositions[0]];

            int maximum = arr[indexPositions[0]];

            for (int i = 1; i < indexPositions.Count; i++) {
                if (arr[indexPositions[i]] > maximum) maximum = arr[indexPositions[i]];
            }

            return maximum;
        }
        
        public static int FindMin(int[] arr, List<int> indexPositions) {
            if (indexPositions.Count == 1) return arr[indexPositions[0]];

            int minimum = arr[indexPositions[0]];

            for (int i = 1; i < indexPositions.Count; i++) {
                if (arr[indexPositions[i]] < minimum) minimum = arr[indexPositions[i]];
            }

            return minimum;
        }
    }
} 

