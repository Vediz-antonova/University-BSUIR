#include <iostream>

void bubbleSort(int arr[], int n) {
    for (int i = 0; i < n - 1; ++i) {
        for (int j = 0; j < n - i - 1; ++j) {
            if (arr[j] > arr[j + 1]) {
                int temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
            }
        }
    }
}

int main() {
    // Шапка.
    std::cout << "1. A string of characters consisting of numbers "
                 "separated by spaces is given.\n"
                 "Display the numbers of this row in ascending order "
                 "of their values." << '\n';
    std::cout << "Created by Antonava Lidia 353504\n";

    std::string input;
    bool c = 1;
    while (true) {
        std::cout << "Enter string: ";
        std::getline(std::cin, input);

        const char *charArray = input.c_str();
        int size = 0;
        for (int i = 0; i < input.length(); ++i) {
            if (charArray[i] == ' ') {
                ++size;
            }
        }
        ++size;

        int *numbers = new int[size];
        char *end;
        for (int i = 0; i < size; ++i) {
            numbers[i] = std::strtol(charArray, &end, 10);
            charArray = end + 1;
        }

        bubbleSort(numbers, size);

        std::cout << "Sort numbers: ";
        for (int i = 0; i < size; ++i) {
            std::cout << numbers[i] << " ";
        }
        std::cout << "Do you want to repeat? (0 - no, 1 - yes) ";
        std::cin >> c;
        if (!c) {
            std::cout << "Exit";
            return 0;
        }
        std::cin.ignore();
    }
}
