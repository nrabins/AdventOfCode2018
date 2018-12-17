using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2018.Days;

namespace AdventOfCode2018.Days
{
    public class Day14 : AdventProblem2<string, long, string, string, long>
    {
        protected override string InputFilePath => "Inputs/Day14.txt";
        protected override long Part1ParseInputFile()
        {
            return Part1Parse(File.ReadAllText(InputFilePath));
        }

        protected override string Part2ParseInputFile()
        {
            return Part2Parse(File.ReadAllText(InputFilePath));
        }

        public override long Part1Parse(string rawInput)
        {
            return long.Parse(rawInput);
        }

        public override string Part2Parse(string rawInput)
        {
            return rawInput;
        }

        public override string Part1(long input)
        {
            var recipes = new LinkedList<int>();
            var elf1 = recipes.AddFirst(3);
            var elf2 = recipes.AddLast(7);

            var minRecipes = input + 10;

            //Debug.WriteLine(PrintRecipes(recipes, elf1, elf2));

            while (recipes.Count < minRecipes)
            {
                // add recipes to end
                var newRecipes = (elf1.Value + elf2.Value).ToString().ToCharArray().Select(c => int.Parse(c.ToString()));
                foreach (var newRecipe in newRecipes)
                {
                    recipes.AddLast(newRecipe);
                }

                // move elves along

                var delta1 = elf1.Value + 1;
                for (var i = 0; i < delta1; i++)
                {
                    elf1 = elf1.Next ?? recipes.First;
                }

                var delta2 = elf2.Value + 1;
                for (var i = 0; i < delta2; i++)
                {
                    elf2 = elf2.Next ?? recipes.First;
                }

                //Debug.WriteLine(PrintRecipes(recipes, elf1, elf2));
            }

            var node = recipes.First;
            for (var count = 0; node != null && count < input; count++)
            {
                node = node.Next;
            }

            var sb = new StringBuilder();
            for (var i = 0; i < 10; i++)
            {
                sb.Append(node.Value);
                node = node.Next;
            }

            var output = sb.ToString();
            return output;
        }

        public override long Part2(string input)
        {
            var recipes = new LinkedList<int>();
            var elf1 = recipes.AddFirst(3);
            var elf2 = recipes.AddLast(7);

            var desiredDigits = input.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();

            var currentRecipeStart = recipes.First;

            var iterations = 1000000000;
            while (iterations-- > 0)
            {
                // add recipes to end
                var newRecipes = (elf1.Value + elf2.Value).ToString().ToCharArray().Select(c => int.Parse(c.ToString()));
                foreach (var newRecipe in newRecipes)
                {
                    var newNode = recipes.AddLast(newRecipe);
                }

                // move elves along
                var delta1 = elf1.Value + 1;
                for (var i = 0; i < delta1; i++)
                {
                    elf1 = elf1.Next ?? recipes.First;
                }

                var delta2 = elf2.Value + 1;
                for (var i = 0; i < delta2; i++)
                {
                    elf2 = elf2.Next ?? recipes.First;
                }

                // see if our new additions fulfill the desired input
                while (currentRecipeStart != recipes.Last)
                {
                    var currentRecipe = new List<int>();
                    var currentRecipeNode = currentRecipeStart;
                    while (currentRecipeNode != recipes.Last)
                    {
                        currentRecipe.Add(currentRecipeNode.Value);
                        currentRecipeNode = currentRecipeNode.Next;
                    }
                    currentRecipe.Add(currentRecipeNode.Value);

                    // if match, we're done!
                    if (StartsWith(currentRecipe, desiredDigits))
                        return CalculateLeft(recipes, currentRecipeStart);

                    // if we could match if the right digits were added, continue adding digits
                    if (CouldMatch(currentRecipe, desiredDigits))
                    {
                        break;
                    }
                    
                    // otherwise, advance the current node (if possible)
                    if (currentRecipeStart != recipes.Last)
                    {
                        currentRecipeStart = currentRecipeStart.Next;
                    }
                }
            }

            throw new Exception("Exceeded max iterations");
        }

        public bool StartsWith(List<int> currentRecipe, int[] desiredDigits)
        {
            if (desiredDigits.Length > currentRecipe.Count)
                return false;

            for (var i = 0; i < desiredDigits.Length; i++)
            {
                if (currentRecipe[i] != desiredDigits[i])
                    return false;
            }

            return true;
        }

        public bool CouldMatch(List<int> currentRecipe, int[] desiredDigits)
        {
            if (desiredDigits.Length < currentRecipe.Count)
                return false;

            for (var i = 0; i < currentRecipe.Count; i++)
            {
                if (currentRecipe[i] != desiredDigits[i])
                    return false;
            }

            return true;
        }

        private long CalculateLeft(LinkedList<int> recipes, LinkedListNode<int> currentRecipeStart)
        {
            var currentNode = currentRecipeStart;
            var count = 0;
            while (currentNode != recipes.First)
            {
                count++;
                currentNode = currentNode.Previous;
            }

            return count;
        }

        private string GetRecipeStr(LinkedList<int> recipes, LinkedListNode<int> elf1, LinkedListNode<int> elf2)
        {
            var sb = new StringBuilder();
            for (var recipe = recipes.First; recipe != null; recipe = recipe.Next)
            {
                if (elf1.Equals(elf2))
                {
                    throw new Exception("Elves shouldn't be the same, right?");
                }
                if (recipe.Equals(elf1))
                    sb.Append($"({recipe.Value})");
                else if (recipe.Equals(elf2))
                    sb.Append($"[{recipe.Value}]");
                else
                    sb.Append(recipe.Value);

                sb.Append("  ");
            }

            return sb.ToString();
        }

        private string GetPart2RecipeStr(LinkedList<int> recipes, LinkedListNode<int> recipeStart)
        {
            var sb = new StringBuilder();
            for (var recipe = recipes.First; recipe != null; recipe = recipe.Next)
            {
                if (recipe.Equals(recipeStart))
                    sb.Append($"({recipe.Value})");
                else
                    sb.Append(recipe.Value);

                sb.Append("  ");
            }

            return sb.ToString();
        }

    }
}
