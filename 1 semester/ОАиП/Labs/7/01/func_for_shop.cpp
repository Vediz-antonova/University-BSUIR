#pragma once

#include "func_for_shop.h"
#include "functions1.h"
#include <iostream>

void FillingInTheStructure(Shop &sh, int i) {
    if (i != -1) {
        std::cout << "\n\nNumber: " << i;
    }

    std::cout << "\nEnter first_name...";
    std::cin >> sh.first_name;
    while (CheckString(sh.first_name)) {
        std::cout << "Error\n";
        std::cin >> sh.first_name;
    }

    std::cout << "\nEnter middle_name...";
    std::cin >> sh.middle_name;
    while (CheckString(sh.middle_name)) {
        std::cout << "Error\n";
        std::cin >> sh.middle_name;
    }

    std::cout << "\nEnter last_name...";
    std::cin >> sh.last_name;
    while (CheckString(sh.last_name)) {
        std::cout << "Error\n";
        std::cin >> sh.last_name;
    }

    std::cout << "\nEnter address...";
    std::cin >> sh.address;
    while (CheckString(sh.address)) {
        std::cout << "Error\n";
        std::cin >> sh.address;
    }

    CheckInt(sh.sale, "\nEnter sale...");
}

void Show(const Shop &arr, int i) {
    std::cout << "\n\nNumber: " << i;
    std::cout << "\nFirst_name: " << arr.first_name;
    std::cout << "\nMiddle_name: " << arr.middle_name;
    std::cout << "\nLast_name: " << arr.last_name;
    std::cout << "\nAddress: " << arr.address;
    std::cout << "\nSale: " << arr.sale;
}

void ChooseFromPreviousAnswers(Shop *array, int n) {
    if (n > 0) {
        short b;
        CheckChoose(b, "\nDo you want to choose from previous elements? (0-no or 1-yes)");

        if (b == 1) {
            for (int k = 0; k < n; ++k) {
                Show(array[k], k);
            }

            int choose;
            CheckInt(choose, "\nChoose number: ");
            array[n] = array[choose];
        } else if (b == 0) {
            FillingInTheStructure(array[n], n);
        }

    } else {
        FillingInTheStructure(array[0], 0);
    }
}

bool Check(const Shop &sh, const Shop &stop) {
    if (sh.first_name == stop.first_name && sh.middle_name == stop.middle_name && sh.last_name == stop.last_name &&
        sh.address == stop.address && sh.sale == stop.sale) {
        return true;
    } else {
        return false;
    }
}

void SortByInsert(Shop *arr, int size) {
    Shop temp;
    for (int i = 1; i < size / 2; i++) {
        temp = arr[i];
        for (int j = i - 1; j >= -1; j--) {
            if (j >= 0 && arr[j].sale > temp.sale) {
                arr[j + 1] = arr[j];
            } else {
                arr[j + 1] = temp;
                break;
            }
        }
    }
}

void Sort(Shop *arr, int size) {
    bool was = false;
    for (int i = 0; i < size && arr[i].sale <= 5; ++i) {
        if (arr[i].sale == 5) {
            was = true;
            Show(arr[i], i);
        }
    }

    if (!was) {
        std::cout << "\nNo one";
    }
}

void AddElementsOfArray(Shop *array, int &n) {
    int index;
    CheckInt(index, "\nHow many structures you want to add?...");

    for (int i = n; i < n + index; ++i) {
        ChooseFromPreviousAnswers(array, i);
    }

    n += index;
}

void FindElementAndOutput(Shop *array, int n, const Shop &param) {
    for (int i = 0; i < n; i++) {
        if (array[i].first_name == param.first_name || array[i].middle_name == param.middle_name
            || array[i].last_name == param.last_name || array[i].address == param.address || array[i].sale
                                                                                             == param.sale) {
            Show(array[i], i);
        }
    }
}

void DeleteElementOrChange(Shop *array, int &n, int index) {
    short b;
    CheckChoose(b, "\nChoose what you want to do?"
                   "\t0) Delete"
                   "\t1)Change");

    if (b == 0) {
        for (int i = index; i < n; ++i) {
            array[i] = array[i + 1];
        }
        --n;
    } else {
        FillingInTheStructure(array[index], index);
    }
}

int Structure(Shop *array) {
    int c, n;
    Shop stop;

    std::cout << "Choose:";
    CheckInt(c, "\n1)Size = n"
                "\n2)No stop-marker"
                "\n3)While you want");
    switch (c) {
        case 1:
            CheckInt(n, "\nEnter size of array...");

            for (int i = 0; i < n; ++i) {
                ChooseFromPreviousAnswers(array, i);
            }

            return n;

        case 2:
            n = 0;
            std::cout << "\nEnter stop-marker...";
            FillingInTheStructure(stop, 0);

            FillingInTheStructure(array[0], 0);

            while (!Check(array[n], stop)) {
                ChooseFromPreviousAnswers(array, n++);
            }

            return n;

        case 3:
            while (true) {
                short b;
                CheckChoose(b, "\nDo you want to complete? (0 or 1)");

                if (b == 1) {
                    ChooseFromPreviousAnswers(array, n++);
                } else {
                    break;
                }
            }

            return n;

        default:
            std::cout << "Error choose";
            return -1;
    }

}