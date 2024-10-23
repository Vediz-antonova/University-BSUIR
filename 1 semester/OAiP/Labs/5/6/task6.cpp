#include <iostream>

long long F (long long n) {
    if (n % 10 > 0) return (n % 10);
    else if (n == 0) return 0;
    else return F(n / 10);
}

long long S (long long p, long long q) {
    long long sum = 0;
    for (long long i = p; i <= q; ++i) {
        sum += F(i);
    }

    return sum;
}

int main () {
    long long p = 0, q = 0;
    while (true) {
        std::cin >> p >> q;
        if (p < 0 && q < 0) break;
        else {
            std::cout << S(p, q) << "\n";
        }
    }

    return 0;
}