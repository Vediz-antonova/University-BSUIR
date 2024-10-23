#include <iostream>
#include <limits> // Для очистки буфера ввода при неправильном вводе.

int main() {
    // Шапка.
    std::cout << "3. Find the minimum and maximum elements of the matrix and "
                 "if they are in different rows , swap these rows." << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';
    std::cout << "Option 1" << "\n\n";

    int n;
    bool validInput = false;

    // Проверка на ввод.
    do {
        std::cout << "Enter size of matrix (nxn): ";
        if (std::cin >> n) {
            if (n >= 0) {
                validInput = true;
            }
        } else {
            std::cout << "Invalid input. Please enter a valid integer." << '\n';
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        }
    } while (!validInput);

    double **arr = new double *[n];
    for (int i = 0; i < n; ++i) {
        arr[i] = new double[n];
    }

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            std::cout << "Enter element [" << i << "][" << j << "]: ";
            if (std::cin >> arr[i][j]) {
                validInput = true;
            } else {
                std::cout << "Invalid input. Please enter a valid integer." << std::endl;
                std::cin.clear();
                std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
                j--;
            }
        }
    }

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            std::cout << arr[i][j] << ' ';
        }
        std::cout << '\n';
    }

    double max = 0, min = 1000000;
    int maxi = 0, mini = 0;

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            if (arr[i][j] > max) {
                maxi = i;
                max = arr[i][j];
            }
            if (arr[i][j] < min) {
                mini = i;
                min = arr[i][j];
            }
        }
    }

    if (mini == maxi) {
        std::cout << max << ' ' << maxi << '\n';
        std::cout << min << ' ' << mini << '\n';
        std::cout << "The minimum and maximum elements are in the same row" << '\n';
    } else {
        double *temp = new double[n];

        for (int i = 0; i < n; ++i) {
            temp[i] = arr[maxi][i];
            arr[maxi][i] = arr[mini][i];
            arr[mini][i] = temp[i];
        }

        std::cout << max << ' ' << maxi << '\n';
        std::cout << min << ' ' << mini << '\n';

        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                std::cout << arr[i][j] << ' ';
            }
            std::cout << '\n';
        }
    }

    for (int i = 0; i < n; ++i) {
        delete[] arr[i];
    }
    delete[] arr;

    return 0;
}