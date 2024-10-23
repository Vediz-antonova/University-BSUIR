#include <iostream>

int main() {
    long double x, y;
    std::cin >> x >> y;

    std::cout << (x > y ? x : y) << '\n';

    bool b = x > y;
    std::cout << (b ? x : y) << '\n';

    return 0;
}