package sparkapp;


import java.util.List;
import java.io.File;
import java.util.Arrays;

import org.apache.commons.io.FileUtils;
import org.apache.spark.SparkConf;
import org.apache.spark.api.java.JavaPairRDD;
import org.apache.spark.api.java.JavaRDD;
import org.apache.spark.api.java.JavaSparkContext;
import scala.Tuple2;

public class JavaWordCount {
	
	public static void main(String[] args) {
		
		System.setProperty("hadoop.home.dir","C:\\hadoop" );
        // configure spark
        SparkConf sparkConf = new SparkConf().setAppName("SampleApp").setMaster("local").set("spark.cores.max", "10");
        // start a spark context
        JavaSparkContext sc = new JavaSparkContext(sparkConf);
        
        // provide path to input text file
        String path = "src/main/resources/sample.txt";
        String outputPath = "src/main/resources/output";
        FileUtils.deleteQuietly(new File(outputPath));
        
        // read text file to RDD
        JavaRDD<String> rdd = sc.textFile(path);
        
        // collect RDD for printing
        /*for(String line:rdd.collect()){
            System.out.println(line);
        }*/
        
        //put lines in a dataset
        JavaPairRDD<String, Integer> counts = rdd.flatMap(x -> Arrays.asList(x.split(" ")).iterator())
                .mapToPair(x -> new Tuple2<>(x, 1))
                .reduceByKey((x, y) -> x + y);
        
        //convert to a list/array and taking data only with @
        List<Tuple2<String, Integer>> finalCounts = counts.filter((x) -> x._1().contains("@"))
                .collect();

        //print two elements
        /*for(Tuple2<String, Integer> count: finalCounts)
                System.out.println(count._1() + " " + count._2());
        */
        
        counts.saveAsTextFile(outputPath);
		sc.close();
    }
	
}
