#include <iostream>

int main() {
    long long x, y;
    std::cin >> x >> y;

    if (x == 0 || y == 0)
        if (x != 0 || y != 0)
            std::cout << "YES";
        else std::cout << "ERROR";
    else if (x % y == 0 || y % x == 0) {
        std::cout << "YES";
    }
    else std::cout << "NO";

    return 0;
}