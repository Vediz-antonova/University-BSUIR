#pragma once

#include "func_for_shop.h"
#include "functions1.h"
#include <iostream>

int main() {
    Shop *array = new Shop[10000];
    int n = Structure(array);
    if(n == -1){
        return 0;
    }

    int choose, ind;
    Shop param;

    while(true) {
        CheckInt(choose, "\nMenu: "
                         "\n\t1)Show array of structure"
                         "\n\t2)Add new structures"
                         "\n\t3)Find element with some parametr"
                         "\n\t4)Delete or change some element"
                         "\n\t5)Sort array"
                         "\n\t6)Close");
        switch (choose) {
            case 1:
                for (int i = 0; i < n; ++i) {
                    Show(array[i], i);
                }
                break;

            case 2:
                AddElementsOfArray(array, n);
                break;

            case 3:
                std::cout << "Enter parametr: ";
                FillingInTheStructure(param, -1);
                FindElementAndOutput(array, n, param);
                break;

            case 4:
                CheckInt(ind, "Enter index: ");

                DeleteElementOrChange(array, n, ind);
                break;

            case 5:
                SortByInsert(array, n);

                short b;
                CheckChoose(b, "Do you want to see sort array? (0 or 1)");
                if (b) {
                    for (int k = 0; k < n; ++k) {
                        Show(array[k], k);
                    }
                }

                std::cout << "\n\nShop with sale = 5% :...\n";
                Sort(array, n);
                break;

            case 6:
                return 0;

            default:
                return 0;
        }
    }

}