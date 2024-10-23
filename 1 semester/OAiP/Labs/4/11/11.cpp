#include <iostream>
#include <ctime>
#include <limits>

char **RandField(int n, int **arr) {
    int pos = 0;

    for (int i = 0; i < n; ++i) {
        pos = 0;
        for (int j = 0; j < n; ++j) {
            if (n / 2 > pos) {
                arr[i][j] = rand() % 2;
                if (arr[i][j] == 1) {
                    ++pos;
                }
            } else {
                arr[i][j] = 0;
            }
        }
    }

    char **ans = (char **) malloc(n * sizeof(char *));
    for (int i = 0; i < n; ++i) {
        ans[i] = (char *) malloc(n * sizeof(char));
    }

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            ans[i][j] = arr[i][j] % 2 == 0 ? '.' : '*';
        }
    }

    return ans;
}

char Table(char **arr, int size, int n, int t) {
    --size;
    int temp = 0;

    if (t == 0) {
        if (n == 0)
            for (int i = 0; i < 2; ++i) {
                for (int j = 0; j < 2; ++j) {
                    if (i == 0 && j == 0) {
                        continue;
                    }
                    if (arr[i][j] == '*') {
                        ++temp;
                    }
                }
            }
        else if (n == size) {
            for (int i = size; i > size - 2; --i) {
                for (int j = 0; j < 2; ++j) {
                    if (i == size && j == 0) {
                        continue;
                    }
                    if (arr[i][j] == '*') {
                        ++temp;
                    }
                }
            }
        } else {
            for (int i = n - 1; i < n + 2; ++i) {
                for (int j = 0; j < 2; ++j) {
                    if (i == n && j == 0) {
                        continue;
                    }
                    if (arr[i][j] == '*') {
                        ++temp;
                    }
                }
            }
        }
    } else if (t == size) {
        if (n == 0) {
            for (int i = 0; i < 2; ++i) {
                for (int j = size; j > size - 2; --j) {
                    if (i == 0 && j == size) {
                        continue;
                    }
                    if (arr[i][j] == '*') {
                        ++temp;
                    }
                }
            }
        } else if (n == size) {
            for (int i = size; i > size - 2; --i) {
                for (int j = size; j > size - 2; --j) {
                    if (i == size && j == size) {
                        continue;
                    }
                    if (arr[i][j] == '*') {
                        ++temp;
                    }
                }
            }
        } else {
            for (int i = n - 1; i < n + 2; ++i) {
                for (int j = size; j > size - 2; --j) {
                    if (i == n && j == size) {
                        continue;
                    }
                    if (arr[i][j] == '*') {
                        ++temp;
                    }
                }
            }
        }

    } else if (n == size) {
        for (int i = size; i > size - 2; --i) {
            for (int j = t - 1; j < t + 2; ++j) {
                if (i == size && j == t) {
                    continue;
                }
                if (arr[i][j] == '*') {
                    ++temp;
                }
            }
        }
    } else if (n == 0) {
        for (int i = 0; i < 2; ++i) {
            for (int j = t - 1; j < t + 2; ++j) {
                if (i == 0 && j == t) {
                    continue;
                }
                if (arr[i][j] == '*') {
                    ++temp;
                }
            }
        }
    } else {
        for (int i = n - 1; i < n + 2; ++i) {
            for (int j = t - 1; j < t + 2; ++j) {
                if (i == n && j == t) {
                    continue;
                }
                if (arr[i][j] == '*') {
                    ++temp;
                }
            }
        }
    }

    return temp + '0';
}

void Check(int n, char **arr) {
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            if (arr[i][j] == '.') {
                arr[i][j] = Table(arr, n, i, j);
            }
        }
    }
}

void Show(int n, char **arr) {
    std::cout << '\n';
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            std::cout << arr[i][j] << ' ';
        }
        std::cout << '\n';
    }
}

int main() {
    srand(time(0));

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

    int **arr = (int **) malloc(n * sizeof(int *));
    for (int i = 0; i < n; ++i) {
        arr[i] = (int *) malloc(n * sizeof(int));
    }

    char **ans = RandField(n, arr);
    Show(n, ans);
    Check(n, ans);
    Show(n, ans);

    for (int i = 0; i < n; ++i) {
        free(arr[i]);
    }
    free(arr);
    for (int i = 0; i < n; ++i) {
        free(ans[i]);
    }
    free(ans);

    return 0;
}