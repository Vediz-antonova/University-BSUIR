#include "task1.h"

void task1() {
    // Шапка.
    std::cout << "1. Find number of groups with five digits in string." << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';
    std::cout << "Option 1" << "\n\n";

    char ch;
    char input[80];
    int count = 0;
    bool inGroup = false;

    int n;
    std::cout << "Enter length of string: ";
    n = getchar();

    std::cout << "Enter a string of groups of 0 and 1 separated by spaces:";
    int i = 0;
    while ((ch = getchar()) != '\n' && i < n) {
        if (ch == '0' || ch == '1') {
            inGroup = true;
        } else if (inGroup) {
            inGroup = false;
            count++;
        }
        input[i++] = ch;
    }
    input[i] = '\0';

    int fiveCount = 0;
    for (i = 0; input[i] != '\0'; ++i) {
        if (input[i] == '0' || input[i] == '1') {
            inGroup = true;
        } else if (inGroup) {
            inGroup = false;
            int digitCount = 0;

            while (input[i] == '0' || input[i] == '1') {
                digitCount++;
                ++i;
            }

            if (digitCount == 5) {
                fiveCount++;
            }
        }
    }

    std::cout << "Source text: " << input << std::endl;
    std::cout << "Number of groups with five digits: " << fiveCount << std::endl;
}
