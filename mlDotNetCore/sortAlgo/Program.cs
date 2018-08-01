using System;

class Program
{
    static void Main(string[] args)
    {

        Program o = new Program();
        o.QuickSortTest();
        Console.Read();
    }

    void QuickSortTest()
    {
        int[] number = { 89, 76, 45, 92, 67, 12, 99 };
        QuickSort(number, 0, number.Length - 1);
        //Sorted array
        foreach (int num in number)
        {
            Console.WriteLine("{0}", num);
        }
    }
    void QuickSort(int[] arr, int left, int right)
    {
        // For Recusrion  
        if (left < right)
        {
            int pivot = Partition(arr, left, right);

            //left numbers
            if (pivot > 1)
                QuickSort(arr, left, pivot - 1);

            //right numbers
            if (pivot + 1 < right)
                QuickSort(arr, pivot + 1, right);
        }
    }

    static int Partition(int[] numbers, int left, int right)
    {
        int pivot = numbers[left];

        while (true)
        {

            while (numbers[left] < pivot)
                left++;
            while (numbers[right] > pivot)
                right--;
            if (left < right)
            {
                int temp = numbers[right];
                numbers[right] = numbers[left];
                numbers[left] = temp;
            }
            else
            {
                return right;
            }
        }
    }

    void BubbleSort()
    {
        int[] number = { 89, 76, 45, 92, 67, 12, 99 };
        bool flag = true;
        int temp;
        int numLength = number.Length;

        //sorting an array
        for (int i = 1; (i <= (numLength - 1)) && flag; i++)
        {
            flag = false;
            for (int j = 0; j < (numLength - 1); j++)
            {
                if (number[j + 1] < number[j])
                {
                    temp = number[j];
                    number[j] = number[j + 1];
                    number[j + 1] = temp;
                    flag = true;
                }
            }
        }

        //Sorted array
        foreach (int num in number)
        {
            Console.WriteLine("{0}", num);
        }

    }
}
