#pragma once

#include "func_for_talk.h"
#include "functions2.h"
#include <iostream>

void FillingInTheStructure(Talk &sh, int i) {
    if (i != -1) {
        std::cout << "\n\nNumber: " << i;
    }

    std::cout << "\nEnter data...";
    std::cin >> sh.data;
    while (CheckString(sh.data)) {
        std::cout << "Error\n";
        std::cin >> sh.data;
    }

    std::cout << "\nEnter code...";
    std::cin >> sh.code;
    while (CheckString(sh.code)) {
        std::cout << "Error\n";
        std::cin >> sh.code;
    }

    std::cout << "\nEnter name...";
    std::cin >> sh.name;
    while (CheckString(sh.name)) {
        std::cout << "Error\n";
        std::cin >> sh.name;
    }

    CheckInt(sh.time, "\nEnter time...");

    std::cout << "\nEnter tarif...";
    std::cin >> sh.tarif;

    CheckInt(sh.city_number, "\nEnter city_number...");

    CheckInt(sh.phone_number, "\nEnter phone_number...");
}

void Show(const Talk &arr, int i) {
    std::cout << "\n\nNumber: " << i;
    std::cout << "\ndata: " << arr.data;
    std::cout << "\ncode: " << arr.code;
    std::cout << "\nname: " << arr.name;
    std::cout << "\ntime: " << arr.time;
    std::cout << "\ntarif: " << arr.tarif;
    std::cout << "\ncity_number: " << arr.city_number;
    std::cout << "\nphone_number: " << arr.phone_number;
}

void ChooseFromPreviousAnswers(Talk *array, int n) {
    if (n > 0) {
        short b;
        CheckChoose(b, "\nDo you want to choose from previous elements? (0-no or 1-yes)");

        if (b == 1) {
            for (int k = 0; k < n; ++k) {
                Show(array[k], k);
            }

            ll choose;
            CheckInt(choose, "\nChoose number: ");
            array[n] = array[choose];
        } else if (b == 0) {
            FillingInTheStructure(array[n], n);
        }

    } else {
        FillingInTheStructure(array[0], 0);
    }
}

bool Check(const Talk &sh, const Talk &stop) {
    if (sh.data == stop.data && sh.code == stop.code && sh.name == stop.name && sh.time == stop.time &&
        sh.tarif == stop.tarif && sh.city_number == stop.city_number && sh.phone_number == stop.phone_number) {
        return true;
    } else {
        return false;
    }
}

void AddElementsOfArray(Talk *array, int &n) {
    ll index;
    CheckInt(index, "\nHow many structures you want to add?...");

    for (int i = n; i < n + index; ++i) {
        ChooseFromPreviousAnswers(array, i);
    }

    n += index;
}

void DeleteElement(Talk *array, int &n, ll index) {
    for (int i = index; i < n; ++i) {
        array[i] = array[i + 1];
    }

    --n;
}

void ChangeElement(Talk *array, ll index) {
    FillingInTheStructure(array[index], index);
}

void Sort(Talk *array, int n) {
    for (int i = 0; i < n - 1; i++) {
        for (int j = 0; j < n - i - 1; j++) {
            if (array[j].name.size() > array[j + 1].name.size()) {
                std::swap(array[j].name, array[j + 1].name);
            }
        }
    }
}

void ShowAnswer(Talk *array, int n) {
    int i = 0, j = 1;
    ld ans;
    while (i < n) {
        ans = array[i].time * array[i].tarif;
        while (array[i].name == array[j].name) {
            ++j;
            ans += array[j].time * array[j].tarif;
        }

        std::cout << "In " << array[i].name << " cost is " << ans;

        i = j;
        j++;
    }
}

int Structure(Talk *array) {
    ll c, n;
    Talk stop;

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
            std::cout << "Error";
            return -1;
    }
}