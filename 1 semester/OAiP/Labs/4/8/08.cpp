#include <iostream>

int main() {
    // Перемножение матриц.
    int n;
    std::cin >> n;

    int **arr1 = new int *[n];

    for (int i = 0; i < n; ++i) {
        arr1[i] = new int[n];
    }

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            std::cin >> arr1[i][j];
        }
    }

    int m;
    std::cin >> m;

    int **arr2 = new int *[m];
    int **arr3 = new int *[n];

    for (int i = 0; i < m; ++i) {
        arr2[i] = new int[m];
    }

    for (int i = 0; i < n; ++i) {
        arr3[i] = new int[m];
    }

    for (int i = 0; i < m; ++i) {
        for (int j = 0; j < m; ++j) {
            std::cin >> arr2[i][j];
        }
    }

    for (int i = 0; i < m; ++i) {
        for (int j = 0; j < m; ++j) {
            arr3[i][j] = 0;
        }
    }

    if (n != m) {
        std::cout << "-1";
    }
    else {
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < m; ++j) {
                for (int k = 0; k < n; ++k) {
                    arr3[i][j] += arr1[i][k] * arr2[k][j];
                }

                if (j != n - 1) {
                    std::cout << arr3[i][j] << ' ';
                }
                else {
                    std::cout << arr3[i][j];
                }
            }
            std::cout << '\n';
        }
    }

    return 0;
}