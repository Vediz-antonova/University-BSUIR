#include <iostream>
#include <limits>

int main() {
    // Шапка.
    std::cout << "1. The entered array of k numbers should be transformed as follows: "
                 "negative values should be moved to the beginning, preserving their "
                 "relative position." << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';
    std::cout << "Option 1" << "\n\n";

    int k, n = 0;
    bool validInput = false;

    // Проверка на ввод.
    do {
        std::cout << "Enter size of array: ";
        if (std::cin >> k) {
            if (k >= 0) {
                validInput = true;
            }
        } else {
            std::cout << "Invalid input. Please enter a valid integer." << '\n';
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        }
    } while (!validInput);

    const int size = k;
    int arr[size];

    for (int i = 0; i < size; ++i) {
        std::cout << "Enter element " << i << ": ";
        if (std::cin >> arr[i]) {
            validInput = true;
        } else {
            std::cout << "Invalid input. Please enter a valid integer." << std::endl;
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
            i--;
        }
    }

    for (int i = 0; i < size; ++i) {
        if (arr[i] >= 0) {
            ++n;
        }
    }

    int ans[size];
    int posn = 0, posp = size - n;

    for (int i = 0; i < size; ++i) {
        if (arr[i] < 0) {
            ans[posn++] = arr[i];
        } else if (arr[i] >= 0) {
            ans[posp++] = arr[i];
        }
    }

    for (int i = 0; i < size; ++i) {
        std::cout << ans[i] << ' ';
    }

    return 0;
}