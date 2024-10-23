#pragma once

#include "func_for_talk.h"
#include "functions2.h"
#include <iostream>

int main() {
    Talk *array = new Talk[10000];
    int n = Structure(array);
    if(n == -1){
        return 0;
    }

    ll choose, ind;
    Talk param;

    while (true) {
        CheckInt(choose, "\nMenu: "
                         "\n\t1)Show array of structure"
                         "\n\t2)Add new structures"
                         "\n\t3)Find element with some parametr"
                         "\n\t4)Delete or change some element"
                         "\n\t5)Sort array ans see total amount of conversations per city"
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
                CheckInt(ind, "Enter index: ");
                DeleteElement(array, n, ind);
                break;

            case 4:
                CheckInt(ind, "Enter index: ");
                ChangeElement(array, ind);
                break;

            case 5:
                Sort(array, n);
                for(int i = 0; i < n; ++i){
                    Show(array[i], i);
                }

                std::cout << "\n\nThe total amount of conversations per city: ";

                ShowAnswer(array, n);
                break;

            case 6:
                return 0;

            default:
                return 0;
        }
    }

    return 0;
}