namespace lib;

public class TaskTwo
{
    //Буду использовать бианрный поиск для решения задачи 2
    static public int FindIndex(int[] array, int target)
    {
        if (array == null) {
            return -1;
        }

        int left = 0;
        int rigth = array.Length - 1;
        int middle = 0;

        while(left <= rigth) {
            middle = left + (rigth - left) / 2;
            if (array[middle] == target)
            {
                return middle;
            } 
            else if(array[middle] < target) {
                left = middle + 1;
            }
            else {
                rigth = middle - 1;
            }
        }

        return -1;
    }
}

