#include <iostream>
#include "logic.h"

int GetIntegerInput() {
    int value = 0;
    bool isInputValid = false;

    while (!isInputValid) {
        isInputValid = true;

        std::string input;
        std::cin >> input;

        size_t index = 0;
        if (input[0] == '-') {
            index = 1;
        }

        for (; index < input.size(); ++index) {
            if (!isdigit(input[index])) {
                isInputValid = false;
                std::cout << "Invalid input. Please enter a valid integer: ";
                break;
            }
        }

        if (isInputValid) {
            value = std::stoi(input);
        }
    }

    return value;
}

int main() {
    // Шапка.
    std::cout << "3. Create an array of diagonal matrix elements having an even value and "
                 "calculate the product of all its elements." << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';

    std::cout << "Option 1" << '\n';


    std::cout << "Enter size of matrix: ";
    int n, k;
    n = GetIntegerInput();
    k = GetIntegerInput();

    std::cout << "Enter elements of matrix: \n";
    int **array = new int *[n];
    for (int i = 0; i < n; ++i) {
        array[i] = new int[k];
    }

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < k; ++j) {
            array[i][j] = GetIntegerInput();
        }
    }

    std::cout << "Diagonal elements: ";
    int size;
    int *result = diagonalElements(array, n, k, size);
    for(int i = 0; i < size; ++i){
        std::cout << result[i] << ' ';
    }

    std::cout << '\n';
    std::cout << "Odd elements: ";
    result = findOddElements(result, size, size);
    for(int i = 0; i < size; ++i){
        std::cout << result[i] << ' ';
    }

    std::cout << '\n';
    int ans = findMultiply(result, size);
    std::cout << "Multiply of array: " << ans << '\n';

    return 0;
}
