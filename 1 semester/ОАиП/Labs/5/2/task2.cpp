#include <iostream>

#include "shared_library.h"

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
    std::cout << "2. Check if there is an a[i] among the array a for which sin(a[i]) > 0." << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';
    std::cout << "Option 1" << "\n\n";

    int n;
    std::cout << "Enter size of array: ";
    std::cin >> n;
    while(n <= 0){
        std::cout << "Invalid input. Please enter a valid number." << std::endl;
        std::cin >> n;
    }

    double *array = new double[n];
    for (int i = 0; i < n; ++i) {
        std::cout << "Enter element " << i << ": ";
        while (true) {
            if (isValidInput(array[i])) {
                break;
            } else {
                std::cout << "Invalid input. Please enter a valid number." << std::endl;
            }
        }
    }

    bool b = hasPositiveSin(array, 0, n);
    std::cout << (b ? "Yes" : "No");

    return 0;
}