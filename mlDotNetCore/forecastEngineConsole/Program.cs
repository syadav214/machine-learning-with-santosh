using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        //Console.Write("Enter First Number:");
        //string a = Console.ReadLine();
        //Console.Write("Enter Second Number:");
        //string b = Console.ReadLine();
        //int c = Convert.ToInt32(a) +  Convert.ToInt32(b);
        //   Console.WriteLine("Sum:"+c.ToString());

        Program o = new Program();
        o.Start();
        Console.Read();

    }

    void Start()
    {
        List<Person> lstPredictPerson = new List<Person>();
        double outVal = 0;
        int outShortVal = 0;
        Console.Write("Enter Height:");
        var height = double.TryParse(Console.ReadLine(), out outVal) == true ? outVal : 0;
        Console.Write("Enter Weight:");
        var weight = double.TryParse(Console.ReadLine(), out outVal) == true ? outVal : 0;
        Console.Write("Enter FootSize:");
        var footSize = double.TryParse(Console.ReadLine(), out outVal) == true ? outVal : 0;
        Console.Write("Enter Age:");
        int age = int.TryParse(Console.ReadLine(), out outShortVal) == true ? outShortVal : 0;

        lstPredictPerson.Add(new Person
        {
            Height = height,
            Weight = weight,
            FootSize = footSize,
            Age = age
        });


        List<Person> lstPerson = new List<Person>();

        //training data. 
        lstPerson.Add(new Person { Sex = "male", Height = 6, Weight = 90, FootSize = 12, Age = 25 });
        lstPerson.Add(new Person { Sex = "male", Height = 5.92, Weight = 95, FootSize = 11, Age = 30 });
        lstPerson.Add(new Person { Sex = "male", Height = 5.58, Weight = 85, FootSize = 12, Age = 40 });
        lstPerson.Add(new Person { Sex = "male", Height = 5.92, Weight = 82, FootSize = 10, Age = 20 });
        lstPerson.Add(new Person { Sex = "male", Height = 5, Weight = 50, FootSize = 6, Age = 15 });
        lstPerson.Add(new Person { Sex = "male", Height = 5, Weight = 40, FootSize = 5, Age = 10 });
        lstPerson.Add(new Person { Sex = "male", Height = 2, Weight = 20, FootSize = 3, Age = 4 });

        lstPerson.Add(new Person { Sex = "female", Height = 5.5, Weight = 75, FootSize = 8, Age = 40 });
        lstPerson.Add(new Person { Sex = "female", Height = 5.42, Weight = 65, FootSize = 7, Age = 30 });
        lstPerson.Add(new Person { Sex = "female", Height = 5.75, Weight = 75, FootSize = 9, Age = 20 });
        lstPerson.Add(new Person { Sex = "female", Height = 2, Weight = 15, FootSize = 2, Age = 3 });

        lstPerson.Add(new Person { Sex = "transgender", Height = 4, Weight = 100, FootSize = 5, Age = 35 });
        lstPerson.Add(new Person { Sex = "transgender", Height = 4.10, Weight = 75, FootSize = 8, Age = 30 });
        lstPerson.Add(new Person { Sex = "transgender", Height = 5.42, Weight = 95, FootSize = 7, Age = 25 });
        lstPerson.Add(new Person { Sex = "transgender", Height = 5.50, Weight = 75, FootSize = 9, Age = 20 });

        TrainClassifier(lstPerson, lstPredictPerson);
        //output would be transgender.
        //Console.WriteLine(classifier.Classify(new double[] { 4, 150, 12 }));
    }



    void TrainClassifier(List<Person> lstPerson, List<Person> lstPredictPerson)
    {
        try
        {
            List<MeanPerson> lstMeanPerson = new List<MeanPerson>();

            //calc data
            var results = (from singlePerson in lstPerson
                           group singlePerson by singlePerson.Sex into g
                           select new { Sex = g.Key, Count = g.Count() }).ToList();


            for (int j = 0; j < results.Count; j++)
            {
                var selectedSex = results[j].Sex;

                var commonSetData = from commonSet in lstPerson
                                    where commonSet.Sex == selectedSex
                                    select commonSet;


                if (commonSetData.Any())
                {
                    var sumHeight = commonSetData.Sum(a => a.Height);
                    var sumWeight = commonSetData.Sum(a => a.Weight);
                    var sumFootSize = commonSetData.Sum(a => a.FootSize);
                    var sumAge = commonSetData.Sum(a => a.Age);
                    var noOfRecords = commonSetData.Count();

                    lstMeanPerson.Add(new MeanPerson
                    {
                        Sex = selectedSex,
                        HeightMean = sumHeight > 0 ? sumHeight / noOfRecords : 0,
                        WeightMean = sumWeight > 0 ? sumWeight / noOfRecords : 0,
                        FootSizeMean = sumFootSize > 0 ? sumFootSize / noOfRecords : 0,
                        AgeMean = sumAge > 0 ? sumAge / noOfRecords : 0
                    });

                }
            }



            //int a = 1;
            //for (int i = 1; i < 4; i++)
            //{
            //    row[a] = Helper.Mean(SelectRows(table, i, string.Format("{0} = '{1}'",
            //                         table.Columns[0].ColumnName, results[j].Name)));
            //    // row[++a] = Helper.Variance(SelectRows(table, i,
            //    //            string.Format("{0} = '{1}'",
            //    //            table.Columns[0].ColumnName, results[j].Name)));
            //    a++;
            //}

            var height = lstPredictPerson[0].Height;
            var weight = lstPredictPerson[0].Weight;
            var footSize = lstPredictPerson[0].FootSize;
            var age = lstPredictPerson[0].Age;

            List<Height> llstHeight = new List<Height>();
            List<Weight> llstWeight = new List<Weight>();
            List<FootSize> llstFootSize = new List<FootSize>();
            List<Age> llstAge = new List<Age>();

            foreach (var x in lstMeanPerson)
            {
                var diffH = x.HeightMean - height;
                var diffW = x.WeightMean - weight;
                var diffFS = x.FootSizeMean - footSize;
                var diffA = x.AgeMean - age;

                diffH = diffH > 0 ? diffH : diffH * -1;
                diffW = diffW > 0 ? diffW : diffW * -1;
                diffFS = diffFS > 0 ? diffFS : diffFS * -1;
                diffA = diffA > 0 ? diffA : diffA * -1;

                llstHeight.Add(new Height { Sex = x.Sex, diff = diffH });
                llstWeight.Add(new Weight { Sex = x.Sex, diff = diffW });
                llstFootSize.Add(new FootSize { Sex = x.Sex, diff = diffFS });
                llstAge.Add(new Age { Sex = x.Sex, diff = diffA });

                //Console.WriteLine("Sex:{0}, H:{1}, W:{2}, F:{3}, A:{4}", x.Sex, x.HeightMean, x.WeightMean, x.FootSizeMean,x.AgeMean);
            }

            var minH = (from singleVal in llstHeight
                        where singleVal.diff == llstHeight.Min(a => a.diff)
                        select singleVal.Sex).FirstOrDefault().ToString();

            var minW = (from singleVal in llstWeight
                        where singleVal.diff == llstWeight.Min(a => a.diff)
                        select singleVal.Sex).FirstOrDefault().ToString();

            var minFS = (from singleVal in llstFootSize
                         where singleVal.diff == llstFootSize.Min(a => a.diff)
                         select singleVal.Sex).FirstOrDefault().ToString();

            var minA = (from singleVal in llstAge
                        where singleVal.diff == llstAge.Min(a => a.diff)
                        select singleVal.Sex).FirstOrDefault().ToString();

            List<PredictSex> llstPredictSex = new List<PredictSex>();
            llstPredictSex.Add(new PredictSex { Sex = minH, diff = llstHeight.Min(a => a.diff) });
            llstPredictSex.Add(new PredictSex { Sex = minW, diff = llstWeight.Min(a => a.diff) });
            llstPredictSex.Add(new PredictSex { Sex = minFS, diff = llstFootSize.Min(a => a.diff) });
            llstPredictSex.Add(new PredictSex { Sex = minA, diff = llstAge.Min(a => a.diff) });

            var predict = (from singleSex in llstPredictSex
                           group singleSex by singleSex.Sex into g
                           select new { Name = g.Key, Count = g.Count() }).ToList();

            //Console.WriteLine(minH);
            //Console.WriteLine(minW);
            //Console.WriteLine(minFS);

            if (predict.Any())
            {
                var bestOfAll = (from singlePredict in predict
                                 where singlePredict.Count == predict.Max(a => a.Count)
                                 select singlePredict.Name).FirstOrDefault().ToString();
                Console.WriteLine(bestOfAll);
            }
            else
            {

                var minDiffAll = (from singleVal in llstPredictSex
                                  where singleVal.diff == llstPredictSex.Min(a => a.diff)
                                  select singleVal.Sex).FirstOrDefault().ToString();
                Console.WriteLine(minDiffAll);
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}


//static string Classify(double[] obj)
//{
//    Dictionary<string,double> score = new Dictionary<string,double>();

//    var results = (from myRow in dataSet.Tables[0].AsEnumerable()
//                   group myRow by myRow.Field<string>(
//                            dataSet.Tables[0].Columns[0].ColumnName) into g
//                   select new { Name = g.Key, Count = g.Count() }).ToList();

//    for (int i = 0; i < results.Count; i++)
//    {
//        List<double> subScoreList = new List<double>();
//        int a = 1, b = 1;
//        for (int k = 1; k < dataSet.Tables["Gaussian"].Columns.Count; k = k + 2)
//        {
//            double mean = Convert.ToDouble(dataSet.Tables["Gaussian"].Rows[i][a]);
//            double variance = Convert.ToDouble(dataSet.Tables["Gaussian"].Rows[i][++a]);
//            double result = Helper.NormalDist(obj[b - 1], mean, Helper.SquareRoot(variance));
//            subScoreList.Add(result);
//            a++; b++;
//        }

//        double finalScore = 0;
//        for (int z = 0; z < subScoreList.Count; z++)
//        {
//            if (finalScore == 0)
//            {
//                finalScore = subScoreList[z];
//                continue;
//            }

//            finalScore = finalScore * subScoreList[z];
//        }

//        score.Add(results[i].Name, finalScore * 0.5);
//    }

//    double maxOne = score.Max(c => c.Value);
//    var name = (from c in score
//                where c.Value == maxOne
//                select c.Key).First();

//    return name;
//}



public class Person
{
    public string Sex { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public double FootSize { get; set; }
    public int Age { get; set; }
}

public class MeanPerson
{
    public string Sex { get; set; }
    public double HeightMean { get; set; }
    public double WeightMean { get; set; }
    public double FootSizeMean { get; set; }
    public int AgeMean { get; set; }
}

public class Height
{
    public string Sex { get; set; }
    public double diff { get; set; }
}

public class Weight
{
    public string Sex { get; set; }
    public double diff { get; set; }
}

public class FootSize
{
    public string Sex { get; set; }
    public double diff { get; set; }
}

public class Age
{
    public string Sex { get; set; }
    public double diff { get; set; }
}

public class PredictSex
{
    public string Sex { get; set; }
    public double diff { get; set; }
}