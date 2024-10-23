#include <iostream>
#include <iomanip>

int main() {
    long double a, b, c, d;
    std::cin >> a >> b >> c >> d;

    long double z;
    if (c != 0) {
        if (c >= d && a < d)
            z = a + b / c;
        else if (c < d && a >= d)
            z = a - b / c;
        else z = 0;

        std::cout << std::setprecision(20) << z << '\n';
    }
    else std::cout << "ERROR" << '\n';

    return 0;
}
