#include <iostream>
#include <iomanip>
#include "static_library.h"

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
    std::cout << "1. Solve a[i] * x + b[j] = 0." << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';
    std::cout << "Option 1" << "\n\n";

    const int N = 8;
    int *a = new int[N];
    int *b = new int[N];

    std::cout << "Enter " << N << " numbers for A" << std::endl;
    for (int i = 0; i < N; ++i) {
        a[i] = GetIntegerInput();
    }
    std::cout << "Enter " << N << " numbers for B" << std::endl;
    for (int i = 0; i < N; ++i) {
        b[i] = GetIntegerInput();
    }

    std::cout << std::setprecision(9);
    for (int i = 0; i < N; ++i) {
        for (int j = 0; j < N; ++j) {
            std::cout << "For a = " << a[i] << " and b = " <<
                      b[j] << " => x = " << LinearEquationFromStatic(a[i], b[j]) << '\n';
        }
    }

    return 0;
}
