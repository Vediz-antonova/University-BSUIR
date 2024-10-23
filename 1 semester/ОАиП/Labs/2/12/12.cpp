#include <iostream>

int main() {
    long double a, b, c, x, y;
    std::cin >> a >> b >> c >> x >> y;

    if (a >= 0 && b >= 0 && c >= 0 && x >= 0 && y >= 0) {
        if ((a <= x && (b <= y || c <= y)) || (b <= x && (a <= y || c <= y)) || (c <= x && (b <= y || a <= y)))
            std::cout << "YES" << '\n';
        else std::cout << "NO" << '\n';
    }
    else std::cout << "Incorrect input" << '\n';

    return 0;
}