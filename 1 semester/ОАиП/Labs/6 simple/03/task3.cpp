#include <iostream>
#include <string>

int sum_row(int *arr, int k) {
    int sum = 0;
    for (int j = 0; j < k; j++) {
        sum += arr[j];
    }

    return sum;
}

void swap_rows(int **arr, int i, int j, int k) {
    int *temp = new int[k];
    for (int l = 0; l < k; l++) {
        temp[l] = arr[i][l];
        arr[i][l] = arr[j][l];
        arr[j][l] = temp[l];
    }

}

int main() {
    std::cout << "3. Sort the matrix so that the sum of the elements in "
                 "the first row is the minimum, and the maximum in the last row."
                 "\nCreated by Antonava Lidia 353504";

    int n, k;
    std::cout << "Enter size of array: (nxk): ";
    std::cin >> n >> k;

    while (n > 1000 || k > 1000) {
        std::cout << "Again!" << std::endl;
        std::cin >> n >> k;
        return 0;
    }

    int **arr = new int *[n];
    for (int i = 0; i < n; i++) {
        arr[i] = new int[k];
    }

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < k; j++) {
            std::cout << "Enter " << i << '|' << j << " element of array: ";
            std::cin >> arr[i][j];
        }
    }

    for (int i = 0; i < n - 1; i++) {
        int min_index = i;
        int min_sum = sum_row(arr[i], k);
        for (int j = i + 1; j < n; j++) {
            int curr_sum = sum_row(arr[j], k);
            if (curr_sum < min_sum) {
                min_index = j;
                min_sum = curr_sum;
            }
        }
        if (min_index != i) {
            swap_rows(arr, i, min_index, k);
        }
    }


    for (int i = 0; i < n; i++) {
        for (int j = 0; j < k; j++) {
            std::cout << arr[i][j] << ' ';
        }
        std::cout << std::endl;
    }

    for (int i = 0; i < n; i++) {
        delete[] arr[i];
    }
    delete[] arr;
}