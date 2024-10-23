#include <iostream>

void SortByInsert(double *arr, int size) {
    int current;
    for (int i = 1; i < size / 2; i++) {
        current = arr[i];
        for (int j = i - 1; j >= -1; j--) {
            if (j >= 0 && arr[j] > current) {
                arr[j + 1] = arr[j];
            } else {
                arr[j + 1] = current;
                break;
            }
        }
    }
}

void SortByChoice(double *arr, int size) {
    double min;
    int min_pos;
    for (int i = size/2; i < size - 1; i++) {
        min = arr[i];
        min_pos = i;
        for (int j = i + 1; j < size; j++) {
            if (arr[j] < min) {
                min = arr[j];
                min_pos = j;
            }
        }
        if (min_pos != i) {
            int temp = arr[i];
            arr[i] = arr[min_pos];
            arr[min_pos] = temp;
        }
    }
}

int main() {
    std::cout << "2. Sort the first half of the array"
                 "by insertion sorting, and the second half"
                 "by selection sorting."
                 "\nCreated by Antonava Lidia 353504";

    int n;
    std::cout << "Enter size of array: ";
    std::cin >> n;

    std::cout << n/2;

    double *array = new double[n];
    for (int i = 0; i < n; ++i) {
        std::cout << "Enter " << i << " element of array: ";
        std::cin >> array[i];
    }

    SortByInsert(array, n);
    SortByChoice(array, n);

    for (int i = 0; i < n; i++) {
        std::cout << array[i] << ' ';
    }

    return 0;
}