#include <iostream>
#include <iomanip>

int main() {
    long double b1;
    std::cin >> b1;

    std::cout << std::setprecision(8) << b1 / (1.0 - 1.0 / (1.0 + 1.0));

    return 0;
}