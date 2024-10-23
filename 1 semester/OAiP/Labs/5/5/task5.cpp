#include <iostream>
#include "logic.h"

int GetIntegerInput() {
    int value = 0;
    bool isInputValid = false;

    while (!isInputValid) {
        isInputValid = true;

        std::string input;
        std::cin >> input;

        bool isNegative = false;
        size_t index = 0;
        if (input[0] == '-') {
            isNegative = true;
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
    std::cout << "5. Create an array of odd elements of even columns and calculate the "
                 "arithmetic mean of its elements." << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';

    int n, k;
    std::cin >> n >> k;
    n = GetIntegerInput();
    k = GetIntegerInput();

    int **array = new int *[n];
    for (int i = 0; i < n; ++i) {
        array[i] = new int[k];
    }

    for(int i = 0; i < n; ++i){
        for(int j = 0; j < k; ++j){
            array[i][j] = GetIntegerInput();
        }
    }


    NewArray(array, n, k);
    double average = AverageMean(array, n, k);
    std::cout << '\n' << average << '\n';

    return 0;
}
