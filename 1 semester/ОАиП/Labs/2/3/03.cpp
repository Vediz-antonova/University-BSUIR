#include <iostream>

int main() {
    long long x, y;
    std::cin >> x >> y;

    if (x > y)
        y = 0;
    else if (y > x)
        x = 0;
    else if (x == y) {
        x = 0;
        y = 0;
    }

    std::cout << x << " " << y << '\n';

    long double a, b, c, k;
    std::cin >> a >> b >> c >> k;

    if (a > b) {
        a > c ? a -= k : c -= k;
    }
    else {
        b > c ? b -= k : c -= k;
    }
    std::cout << a << " " << b << " " << c;

    return 0;
}