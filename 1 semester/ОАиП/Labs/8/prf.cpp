#include <iostream>
#include <cmath>

int main() {
    long long **p = NULL, ch2 = 2;
    long long N;
    unsigned long long S;
    std::cin >> N;
    if (N == 1) {
        std::cout << 1 << "\n";
    } else if (N == 2) {
        std::cout << "NO\n";
    }

    if (N % 4 == 0) {
        p = (long long **) malloc((N) * sizeof(long long *));
        for (long long i = 0; i < N; i++) {
            p[i] = (long long *) malloc((N) * sizeof(long long));
        }


        for (long long i = 0; i < N / 4; i++) {
            for (long long j = 0; j < N / 4; j++) {
                p[i][j] = 1;
            }
        }
        for (long long i = 3 * N / 4; i < N; i++) {
            for (long long j = 0; j < N / 4; j++) {
                p[i][j] = 1;
            }
        }
        for (long long i = 0; i < N / 4; i++) {
            for (long long j = 3 * N / 4; j < N; j++) {
                p[i][j] = 1;
            }
        }
        for (long long i = 3 * N / 4; i < N; i++) {
            for (long long j = 3 * N / 4; j < N; j++) {
                p[i][j] = 1;
            }
        }

        for (int i = N / 4; i < 3 * N / 4; i++) {
            for (int j = N / 4; j < 3 * N / 4; j++) {
                p[i][j] = 1;
            }
        }
        for (long long i = 0, ch = 1; i < N; i++) {
            for (long long j = 0; j < N; j++) {
                if (p[i][j] == 1) {
                    p[i][j] = ch;
                } else if (p[i][j] == 0) {
                    p[i][j] = N * N - ch + 1;
                }
                ch++;
            }
        }
        for (long long i = 0; i < N; i++) {
            for (long long j = 0; j < N; j++) {
                std::cout << p[i][j] << " ";
            }
            std::cout << "\n";
        }
    }

    return 0;
}