#include <iostream>

void BubbleSort (int* arr, int size) {
    int temp;
    for (int i = 0; i < size - 1; i++) {
        for (int j = 0; j < size - i - 1; j++) {
            if (arr[j] < arr[j + 1]) {
                temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
            }
        }
    }
}

int main() {
    std::cout << "1. Sort by bubble in reverse order."
                 "\nCreated by Antonava Lidia 353504";

    int n;
    std::cout << "Enter size of array: ";
    std::cin >> n;

    int* array = new int[n];
    for(int i = 0; i < n; ++i){
        std::cout << "Enter " << i << " element of array: ";
        std::cin >> array[i];
    }

    BubbleSort(array, n);

    for (int i = 0; i < n; i++) {
        std::cout << array[i] << ' ';
    }

    return 0;
}