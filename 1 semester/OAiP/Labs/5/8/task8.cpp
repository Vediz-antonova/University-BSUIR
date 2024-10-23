#include <iostream>

long long g(long long n) {
    long long k = (n + 1) / 2;
    return (n ? (k * k + g(n / 2)) : 0);
}

int main() {
    int t;
    std::cin >> t;

    for (int k = 0; k < t; ++k) {
        int n;
        std::cin >> n;

        std::cout << g(n) << '\n';
    }

    return 0;
}