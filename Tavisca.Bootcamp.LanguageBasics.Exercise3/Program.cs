using System;
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
            int len = protein.Length;
            int[] TotlCalo = new int[len];

            //calculating calorie for each item
            for (int i = 0; i < len; i++) {
                TotlCalo[i] = (protein[i] + carbs[i]) * 5 + fat[i] * 9;
            }
            int[] mealPlan = new int[dietPlans.Length];

            for (int i = 0; i < dietPlans.Length; i++) {
                string plan = dietPlans[i];
    
                if (plan.Length == 0) {
                    mealPlan[i] = 0;
                    continue;
                }

                List<int> indx = new List<int>();
                List<int> indx2 = new List<int>();

                for (int j = 0; j < len; j++) indx.Add(j);

                int max, min;
                foreach (char ch in plan) {
                    switch(ch) {
                        case 'P':
                            max = FindMax(protein, indx);
                            indx = FindAllIndices(protein, indx, max);
                            break;
                        case 'p':
                            min = FindMin(protein, indx);
                            indx = FindAllIndices(protein, indx, min);
                            break;
                        case 'C':
                            max = FindMax(carbs, indx);
                            indx = FindAllIndices(carbs, indx, max);
                            break;
                        case 'c':
                            min = FindMin(carbs, indx);
                            indx = FindAllIndices(carbs, indx, min);
                            break;
                        case 'F':
                            max = FindMax(fat, indx);
                            indx = FindAllIndices(fat, indx, max);
                            break;
                        case 'f':
                            min = FindMin(fat, indx);
                            indx = FindAllIndices(fat, indx, min);
                            break;
                        case 'T':
                            max = FindMax(TotlCalo, indx);
                            indx = FindAllIndices(TotlCalo, indx, max);
                            break;
                        case 't':
                            min = FindMin(TotlCalo, indx);
                            indx = FindAllIndices(TotlCalo, indx, min);
                            break;
                    }
                }

                mealPlan[i] = indx[0];
            }

            return mealPlan;
        }

        public static List<int> FindAllIndices(int[] arr, List<int> pos, int elem) {
            List<int> indx = new List<int>();

            foreach (int i in pos) {
                if (arr[i] == elem) indx.Add(i);
            }

            return indx;
        }

        public static int FindMax(int[] arr, List<int> indx) {
            if (indx.Count == 1) return arr[indx[0]];

            int max = arr[indx[0]];

            for (int i = 1; i < indx.Count; i++) {
                if (arr[indx[i]] > max) max = arr[indx[i]];
            }

            return max;
        }
        public static int FindMin(int[] arr, List<int> indx) {
            if (indx.Count == 1) return arr[indx[0]];

            int min = arr[indx[0]];

            for (int i = 1; i < indx.Count; i++) {
                if (arr[indx[i]] < min) min = arr[indx[i]];
            }

            return min;
        } 

