#include <iostream>
#include <cmath>

int main() {
    double x, D;
    std::cin >> x >> D;

    double b = x + D;
    double A = D * x / b;

    std::cout << (A * A + b * cos(x)) / (D * D * D + (A + D - b)) << '\n';

    return 0;
}