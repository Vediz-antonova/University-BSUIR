#include <iostream>

void GenerateOdd(int n, int **&square, int posi = 0, int posj = 0, int num = 0) {
    int i = posi, j = posj + n / 2;

    for (int pos = num * n * n + 1; pos < num * n * n + n * n + 1; ++pos) {
        square[i][j] = pos;

        int newI = i > posi ? i - 1 : posi + n - 1;
        int newJ = j < posj + n - 1 ? j + 1 : posj;

        if (square[newI][newJ] == 0) {
            i = newI;
            j = newJ;
        } else {
            i = i < posi + n - 1 ? i + 1 : posi;
        }
    }
}

void reflectRow(int **&array, int row, int n, int start, int end) {
    for (int i = start; i < end + n / 2; ++i) {
        std::swap(array[row][i], array[row][n - 1 - i]);
    }
}

void reflectColumn(int **&array, int col, int n, int start, int end) {
    for (int i = start; i < end + n / 2; ++i) {
        std::swap(array[i][col], array[n - 1 - i][col]);
    }
}

void GenerateDoublyEven(int n, int **&square) {
//    int num = 1;
//    for (int i = 0; i < n; ++i) {
//        for (int j = 0; j < n; ++j) {
//            square[i][j] = num++;
//        }
//    }

    int **temp = new int *[n];
    for (int i = 0; i < n; ++i) {
        temp[i] = new int[n];

        for (int j = 0; j < n; ++j) {
            temp[i][j] = 0;
        }
    }

    for (int i = 0; i < n / 4; ++i) {
        for (int j = 0; j < n / 4; ++j) {
            temp[i][j] = 1;
        }

        for (int j = 3 * n / 4; j < n; ++j) {
            temp[i][j] = 1;
        }
    }

    for (int i = 3 * n / 4; i < n; ++i) {
        for (int j = 0; j < n / 4; ++j) {
            temp[i][j] = 1;
        }

        for (int j = 3 * n / 4; j < n; ++j) {
            temp[i][j] = 1;
        }
    }

    for (int i = n / 4; i < 3 * n / 4; ++i) {
        for (int j = n / 4; j < 3 * n / 4; ++j) {
            temp[i][j] = 1;
        }
    }

    int pos = 1;
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            if (temp[i][j] == 1) {
                square[i][j] = pos;
            } else if (temp[i][j] == 0) {
                square[i][j] = n * n - pos + 1;
            }
            pos++;
        }
    }

//    for (int posI = 0; posI < n / 4; posI++) {
//        for (int posJ = 0; posJ < n / 4; posJ++) {
//
//            for (int i = 0; i < 4; ++i) {
//                std::swap(square[posI * 4 + 1][posJ * 4 + i], square[posI * 4 + 2][posJ * 4 + i]);
//            }
//            reflectRow(square, posI * 4 + 1, n);
//            reflectRow(square, posI * 4 + 3, n);
//            reflectColumn(square, posJ * 4 + 1, n);
//            reflectColumn(square, posJ * 4 + 2, n);
//            reflectRow(square, posI * 4 + 2, n);
//            reflectRow(square, posI * 4 + 3, n);
//
//        }
//    }
}

void GenerateEven(int n, int **&square) {
    GenerateOdd(n / 2, square, 0, 0, 0);
    GenerateOdd(n / 2, square, n / 2, n / 2, 1);
    GenerateOdd(n / 2, square, 0, n / 2, 2);
    GenerateOdd(n / 2, square, n / 2, 0, 3);

    for (int i = 0; i < n / 2; i++) {
        for (int j = 0; j < n / 4; ++j) {
            if (i == n / 4) {
                std::swap(square[i][j + 1], square[i + n / 2][j + 1]);
                continue;
            }
            std::swap(square[i][j], square[i + n / 2][j]);
        }
    }

    for (int i = 0; i < n / 2; i++) {
        for (int j = n - 1; j > n - n / 4; --j) {
            std::swap(square[i][j], square[i + n / 2][j]);
        }
    }
}

void PrintSquare(int n, int **square) {
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            std::cout << square[i][j] << ' ';
        }
        std::cout << std::endl;
    }
}

int main() {
    int n;
    std::cin >> n;

    int **MagicSquare = new int *[n];
    for (int i = 0; i < n; ++i) {
        MagicSquare[i] = new int[n];

        for (int j = 0; j < n; j++) {
            MagicSquare[i][j] = 0;
        }
    }

    if (n == 2) {
        std::cout << "NO";
        return 0;
    }

    if (n % 2) {
        GenerateOdd(n, MagicSquare);
    } else if (n % 4 == 0) {
        GenerateDoublyEven(n, MagicSquare);
    } else {
        GenerateEven(n, MagicSquare);
    }

    PrintSquare(n, MagicSquare);
//    std::cout << std::endl;
//    int *sum = new int[n]{0};
//    for (int i = 0; i < n; ++i) {
//        for (int j = 0; j < n; ++j) {
//            sum[i] += MagicSquare[i][j];
//        }
//    }
//
//    for (int i = 0; i < n; ++i) {
//        std::cout << sum[i] << ' ';
//    }

    return 0;
}