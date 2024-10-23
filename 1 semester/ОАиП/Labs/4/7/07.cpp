#include<iostream>
#include <vector>

int main() {
    // Все числа между минимумом и максимумом заменить `0` и
    // если их слишком много, убрать `0`.
    std::vector<int> array;

    int num;
    while (std::cin >> num) {
        array.push_back(num);
    }

    long long mini = 0, maxi = 0;
    for (int i = 1; i < array.size(); ++i) {
        if (array[mini] > array[i]) {
            mini = i;
        }
        if (array[maxi] < array[i]) {
            maxi = i;
        }
    }

    for (long long i = std::min(mini, maxi) + 1; i < std::max(mini, maxi); i++) {
        array[i] = 0;
    }

    long long number = 0;
    for (int i = 0; i < array.size(); ++i) {
        if (array[i] == 0) {
            number++;
        }
    }

    long double result[10000];
    long long pos = 0;
    if (number * 2 > array.size()) {
        for (int i = 0; i < array.size(); ++i) {
            if (array[i] != 0) {
                result[pos++] = array[i];
            }
        }

        for (int i = 0; i < pos; ++i) {
            std::cout << result[i] << ' ';
        }
    }
    else {
        for (int i = 0; i < array.size(); ++i) {
            std::cout << array[i] << ' ';
        }
    }

    return 0;
}