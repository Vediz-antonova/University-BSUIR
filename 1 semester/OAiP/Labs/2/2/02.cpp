#include <iostream>

int main() {
    long x, y, z;
    std::cin >> x >> y >> z;

    if (x > 0 && y > 0 && z > 0) {
        if ((x + y) > z && (y + z) > x && (x + z) > y)
            std::cout << "YES";
        else std::cout << "NO";
    }
    else std::cout << "NO";
    return 0;
}