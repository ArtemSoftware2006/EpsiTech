

using lib;

int count = 0;

Console.WriteLine("Enter array length: ");

count = Convert.ToInt32(Console.ReadLine());
int[] array = new int[count];

Console.WriteLine("Enter array elements: (press Enter after each element)");
for (int i = 0; i < array.Length; i++)
{
    array[i] = Convert.ToInt32(Console.ReadLine());
}

Console.WriteLine("Enter target: ");

int target = 0;
target = Convert.ToInt32(Console.ReadLine());

Console.WriteLine(TaskTwo.FindIndex(array, target));       
