#include <iostream>
#include <iomanip>

int main() {
    long long a1, a100;
    std::cin >> a1 >> a100;

    long long d = (a100 - a1) / (100.0 - 1.0);

    std::cout << std::setprecision(10) << d << "\n";
    std::cout << std::setprecision(10) << (2.0 * a1 + d * (1.0 + 10.0 - 1.0)) * 70.0 / 2.0 << "\n";

    return 0;
}