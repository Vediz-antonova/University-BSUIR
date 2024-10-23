#pragma once

#include "functions2.h"
#include <iostream>
#include <limits>

void CheckInt(long long &k, std::string str) {
    bool validInput = false;
    do {
        std::cout << str;
        if (std::cin >> k) {
            if (k >= 0) {
                validInput = true;
            }
        } else {
            std::cout << "Error\n";
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        }
    } while (!validInput);
}

void CheckChoose(short &k, std::string str) {
    bool validInput = false;
    do {
        std::cout << str;
        if (std::cin >> k) {
            if (k == 0 || k == 1) {
                validInput = true;
            }
        } else {
            std::cout << "Error\n";
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        }
    } while (!validInput);
}

bool CheckString(std::string str) {
    for (int i = 0; i < str.size(); ++i) {
        if (str[i] >= 0 && str[i] <= 64) {
            return true;
        }
    }

    return false;
}

