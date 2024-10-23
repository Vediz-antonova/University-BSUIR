#include <iostream>
#include "logic.h"

bool isValidInput(double &number) {
    char input[20];
    std::cin >> input;

    int i = 0;
    bool wasDot = false;
    while (input[i]) {
        if (input[i] < '0' || input[i] > '9') {
            if (input[i] == '.' && (wasDot == false)) {
                wasDot = true;
            } else {
                return false;
            }
        }
        ++i;
    }

    number = atoi(input);
    return true;
}

int main() {
    // Шапка.
    std::cout << "4. Count the number of zeros in the matrix and then flip it." << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';

    std::cout << "Enter size of matrix: ";
    int n, k, numOfO = 0;
    std::cin >> n >> k;
    while (n <= 0 || k <= 0) {
        std::cout << "Invalid input. Please enter a valid number." << std::endl;
        std::cin >> n >> k;
    }

    double **array = new double *[n];
    for (int i = 0; i < n; ++i) {
        array[i] = new double[k];
    }

    std::cout << "Enter elements of matrix: \n";
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < k; ++j) {
            while (true) {
                if (isValidInput(array[i][j])) {
                    break;
                } else {
                    std::cout << "Invalid input. Please enter a valid number." << std::endl;
                }
            }
        }
    }

    int countOfO = CountOfO(array, n, k);
    std::cout << "Count of 0: " << countOfO << '\n';

    std::cout << "Coordinates of '0' elements: \n";
    int size;
    coordinates *xy = CoordinatesOfO(array, n, k, size);
    for (int i = 0; i < size; ++i) {
        std::cout << "x: " << xy[i].x << " y: " << xy[i].y << "\n";
    }

    std::cout << "Reverse array: \n";
    double **newarr = Reverse(array, n, k);
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < k; ++j) {
            std::cout << newarr[i][j] << ' ';
        }
        std::cout << '\n';
    }

    return 0;
}
