#include <iostream>
#include <iomanip>

int main() {
    long double a, t, v0;
    std::cin >> a >> t >> v0;

    std::cout << std::setprecision(20) << v0 * t + a * t * t / 2.0 << '\n';
    return 0;
}