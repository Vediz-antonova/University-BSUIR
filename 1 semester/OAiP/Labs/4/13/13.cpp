#include <iostream>
#include <limits>

void GenerateOddMagicSquare(int n, int **&arr) {
    int i = 0;
    int j = n / 2;

    for (int num = 1; num < n * n + 1; ++num) {
        arr[i][j] = num;

        int newi = (i - 1 + n) % n;
        int newj = (j + 1) % n;

        if (arr[newi][newj] != 0) {
            i = (i + 1) % n;
        } else {
            i = newi;
            j = newj;
        }
    }
}

void GenerateDoublyEvenMagicSquare(int n, int **&arr) {
    int row = n;
    int col = n;
    int num = 0;

    for (int i = 0; i < row; ++i) {
        for (int j = 0; j < col; ++j) {
            arr[i][j] = num;
            num++;
        }
    }

    num = 1;
    for (int i = row; i > 0; --i) {
        for (int j = col; 0 < j; --j) {
            if ((j > n / 4 && j <= (n / 4) * 3 && i < n / 4) ||
                (j > n / 4 && j <= (n / 4) * 3 && i >= (n / 4) * 3)) {
                arr[i - 1][j - 1] = num;
            } else if (i <= (n / 4) * 3 && i > n / 4) {
                arr[i - 1][j - 1] = num;
            }
            num++;
        }
    }
}

void GenerateEvenMagicSquare(int n, int **&arr) {
    bool c = true;
    int num = 1;

    for (int cul = n / 4, Row = 0; num <= n * n / 4; cul++, num++, Row--) {
        if (arr[0][n / 2 - 1] > 0 && c) {
            Row = 1;
            cul = n / 2 - 1;
            c = false;
        } else if (arr[Row][cul] > 0) {
            Row += 2;
            cul--;
        }
        arr[Row][cul] = num;
        if (Row == 0) { Row = n / 2; }
        if (cul == n / 2 - 1) { cul = -1; }
    }
    for (int cul = n / 2, SupportCol = 0; SupportCol < n / 2; ++cul, ++SupportCol) {
        for (int i = n / 2, SupportRow = 0; SupportRow < n / 2; ++i, ++SupportRow) {
            arr[cul][i] = arr[SupportCol][SupportRow];
            arr[cul][i] += n * n / 4;
        }
    }

    for (int Col = n / 2, w = 0; w < n / 2; Col++, w++) {
        for (int i = n / 2, e = 0; e < n / 2; i++, e++) {
            arr[Col][i] = arr[w][e];
            arr[Col][i] += n * n / 4;
        }
    }

    for (int q = n / 2, w = 0; w < n / 2; q++, w++) {
        for (int i = 0, e = 0; e < n / 2; i++, e++) {
            arr[q][i] = arr[w][e];
            arr[q][i] += 3 * n * n / 4;
        }
    }

    for (int q = 0, w = 0; w < n / 2; q++, w++) {
        for (int i = n / 2, e = 0; e < n / 2; i++, e++) {
            arr[q][i] = arr[w][e];
            arr[q][i] += 2 * n * n / 4;
        }
    }

    int t = n / 4;
    if (n > 9) {
        for (int q = 0, w = n / 2; w < n; q++, w++) {
            for (int i = n - ((n - 6) / 4), r = 0; r < t; i++, r++) {
                std::swap(arr[q][i], arr[w][i]);
                std::swap(arr[q][r], arr[w][r]);
            }
        }
        std::swap(arr[t][0], arr[n - 1 - t][0]);
        std::swap(arr[t][t], arr[n - 1 - t][t]);
    } else if (n == 6) {
        std::swap(arr[0][0], arr[3][0]);
        std::swap(arr[1][1], arr[4][1]);
        std::swap(arr[2][0], arr[5][0]);
    }
}

int main() {
    while (true) {
        std::cout << "Enter N:";
        int n;
        bool validInput = false;

        // Проверка на ввод.
        do {
            std::cout << "Enter size of matrix (nxn): ";
            if (std::cin >> n) {
                if (n > 0) {
                    validInput = true;
                }
            } else {
                std::cout << "Invalid input. Please enter a valid integer." << '\n';
                std::cin.clear();
                std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
            }
        } while (!validInput);

        int **MagicSquare = (int **) calloc(n, sizeof(int *));
        for (int i = 0; i < n; ++i) {
            MagicSquare[i] = (int *) calloc(n, sizeof(int));
        }

        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                MagicSquare[i][j] = 0;
            }
        }

        if (n % 2 != 0) {
            GenerateOddMagicSquare(n, MagicSquare);
        } else if (n % 4 == 0) {
            GenerateDoublyEvenMagicSquare(n, MagicSquare);
        } else if (n % 2 == 0 && n % 4 != 0) {
            GenerateEvenMagicSquare(n, MagicSquare);
        }

        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                std::cout << MagicSquare[i][j] << '\t';
            }
            std::cout << '\n';
        }

        for (int i = 0; i < n; ++i) {
            free(MagicSquare[i]);
        }
        free(MagicSquare);

        return 0;
    }
}