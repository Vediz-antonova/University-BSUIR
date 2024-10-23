#include <iostream>
#include <ctime>
#include <limits>

int main() {
    srand(time(0));

    // Шапка.
    std::cout << "2. Calculate how many elements of the matrix of size nxn "
                 "are located below the side diagonal." << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';
    std::cout << "Option 1" << "\n\n";

    int size;
    bool validInput = false;

    // Проверка на ввод.
    do {
        std::cout << "Enter size of matrix (nxn): ";
        if (std::cin >> size) {
            if (size >= 0) {
                validInput = true;
            }
        } else {
            std::cout << "Invalid input. Please enter a valid integer." << '\n';
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        }
    } while (!validInput);

    const int n = size;

    int arr[n][n];

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            arr[i][j] = rand() % 100;
        }
    }

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            std::cout << arr[i][j] << ' ';
        }
        std::cout << '\n';
    }

    int pos = n - 1, ans = 0;

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            if (j > pos) {
                ++ans;
            }
        }
        --pos;
    }

    std::cout << ans << '\n';

    return 0;
}