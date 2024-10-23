#include <iostream>
#include <iomanip>
#include <cmath>

int main() {
    long double h;
    std::cin >> h;

    std::cout << std::setprecision(9) << sqrt(2.0 * h / 9.8) << '\n';
    return 0;
}