using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static Dictionary<string, List<Recommendation>> productRecommendations = new Dictionary<string, List<Recommendation>>();

    static void Main(string[] args)
    {
        Init();

        Console.Write("Enter your preference: ");
        var displayMode = Console.ReadLine();

        if (displayMode.ToLower().Equals("top"))
            Console.WriteLine("\nBest matches");

        foreach (var datakey in productRecommendations)
        {
            string person = datakey.Key;

            var matches = TopMatches(person);

            if (displayMode.ToLower().Equals("all"))
            {
                Console.WriteLine("\nBest match for: {0}", person);
                Console.WriteLine("\nPerson          Pearson Score");//16 is lenght from Person to Pearson
            }

            foreach (var item in matches)
            {
                if (displayMode.ToLower().Equals("top"))
                {
                    Console.WriteLine("\n{0} to {1} at {2} ", person, item.Name, item.Rating.ToString("#0.00000"));
                    break;
                }
                else
                {
                    var spaceLen = 16 - item.Name.Length;
                    var spaceChar = "";
                    for (int i = 1; i <= spaceLen; i++)
                    {
                        spaceChar += " ";
                    }

                    Console.WriteLine("{0}{1}{2}", item.Name, spaceChar, item.Rating.ToString("#0.00000"));
                }
            }
        }

        Console.WriteLine("\nPress any key");
        Console.ReadKey();
    }

    static void Init()
    {
        List<Recommendation> list = new List<Recommendation>();
        list.Add(new Recommendation() { Name = "Wile E Coyote", Rating = 4.5 });
        list.Add(new Recommendation() { Name = "Bugs Bunny", Rating = 2.5 });
        list.Add(new Recommendation() { Name = "Elmer Fudd", Rating = 5.0 });
        list.Add(new Recommendation() { Name = "Foghorn Leghorn", Rating = 2.0 });
        productRecommendations.Add("Rohan", list);


        list = new List<Recommendation>();
        list.Add(new Recommendation() { Name = "Wile E Coyote", Rating = 5.0 });
        list.Add(new Recommendation() { Name = "Bugs Bunny", Rating = 3.5 });
        list.Add(new Recommendation() { Name = "Elmer Fudd", Rating = 1.0 });
        list.Add(new Recommendation() { Name = "Foghorn Leghorn", Rating = 3.5 });
        list.Add(new Recommendation() { Name = "Daffy Duck", Rating = 1.0 });
        productRecommendations.Add("Rahul", list);

        list = new List<Recommendation>();
        list.Add(new Recommendation() { Name = "Wile E Coyote", Rating = 1.0 });
        list.Add(new Recommendation() { Name = "Bugs Bunny", Rating = 3.5 });
        list.Add(new Recommendation() { Name = "Elmer Fudd", Rating = 5.0 });
        list.Add(new Recommendation() { Name = "Foghorn Leghorn", Rating = 4.0 });
        list.Add(new Recommendation() { Name = "Daffy Duck", Rating = 4.0 });
        productRecommendations.Add("Adam", list);

        list = new List<Recommendation>();
        list.Add(new Recommendation() { Name = "Bugs Bunny", Rating = 3.5 });
        list.Add(new Recommendation() { Name = "Elmer Fudd", Rating = 4.0 });
        list.Add(new Recommendation() { Name = "Foghorn Leghorn", Rating = 5.0 });
        list.Add(new Recommendation() { Name = "Daffy Duck", Rating = 2.5 });
        productRecommendations.Add("Katy", list);

        list = new List<Recommendation>();
        list.Add(new Recommendation() { Name = "Wile E Coyote", Rating = 4.5 });
        list.Add(new Recommendation() { Name = "Bugs Bunny", Rating = 5.0 });
        list.Add(new Recommendation() { Name = "Foghorn Leghorn", Rating = 3.0 });
        productRecommendations.Add("Jessica", list);


    }

    static IList<Recommendation> TopMatches(string name)
    {
        // grab of list of products that *excludes* the item we're searching for
        var sortedList = productRecommendations.Where(x => x.Key != name);

        sortedList.OrderByDescending(x => x.Key);

        List<Recommendation> recommendations = new List<Recommendation>();

        // go through the list and calculate the Pearson score for each product
        foreach (var entry in sortedList)
        {
            recommendations.Add(new Recommendation() { Name = entry.Key, Rating = CalculatePearsonCorrelation(name, entry.Key) });
        }

        recommendations = recommendations.OrderByDescending(x => x.Rating).ToList();

        return recommendations;
    }

    static double CalculatePearsonCorrelation(string product1, string product2)
    {
        List<Recommendation> shared_items = new List<Recommendation>();

        // collect a list of products have have reviews in common
        foreach (var item in productRecommendations[product1])
        {
            if (productRecommendations[product2].Where(x => x.Name == item.Name).Count() != 0)
            {
                shared_items.Add(item);
            }
        }

        if (shared_items.Count == 0)
        {
            // they have nothing in common exit with a zero
            return 0;
        }

        // sum up all the preferences
        double product1_review_sum = 0.00f;
        double product2_review_sum = 0.00f;

        // sum up the squares
        double product1_rating_square = 0f;
        double product2_rating_square = 0f;

        //sum up the products
        double critics_product_sum = 0f;

        //temp ratings
        double temp1_rating = 0f;
        double temp2_rating = 0f;

        foreach (Recommendation item in shared_items)
        {
            temp1_rating = productRecommendations[product1].Where(x => x.Name == item.Name).FirstOrDefault().Rating;
            temp2_rating = productRecommendations[product2].Where(x => x.Name == item.Name).FirstOrDefault().Rating;

            product1_review_sum += temp1_rating;
            product2_review_sum += temp2_rating;

            product1_rating_square += Math.Pow(temp1_rating, 2);
            product2_rating_square += Math.Pow(temp2_rating, 2);


            critics_product_sum += temp1_rating * temp2_rating;
        }


        //calculate pearson score
        double pearson_relative_sum = critics_product_sum - (product1_review_sum * product2_review_sum / shared_items.Count);

        //square of sum
        double product1_review_sum_square = Math.Pow(product1_review_sum, 2);
        double product2_review_sum_square = Math.Pow(product2_review_sum, 2);

        double product1_finalVal = product1_rating_square - product1_review_sum_square / shared_items.Count;
        double product2_finalVal = product2_rating_square - product2_review_sum_square / shared_items.Count;

        //density
        double density = (double)Math.Sqrt(product1_finalVal * product2_finalVal);

        if (density == 0)
            return 0;

        var pearson_score = pearson_relative_sum / density;
        return pearson_score;
    }
}

public class Recommendation
{
    public string Name { get; set; }
    public double Rating { get; set; }
}
