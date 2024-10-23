#include <iostream>
#include <cmath>

long GetValue(){
    long a;
    while (true) {
        std::cin >> a;

        if (std::cin.fail()){
            std::cin.clear();
            std::cin.ignore(32767,'\n');
            std::cout << "Oops, that input is invalid. Please try again.\n";
        }
        else {
            if(a > 0) {
                std::cin.ignore(32767, '\n');
                return a;
            }
            else std::cout << "Oops, that input is invalid. Please try again.\n";
        }
    }
}

int main() {
    std::cout << "6. Find the value of the function" << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';
    std::cout << "Option 1" << '\n';

    double ans = 0.0, steps = 0.0, min = 0.0;
    long parts;

    parts = GetValue();

    if(parts != 0) {
        steps = 1.0 / (parts-1);
        for (double i = 0; i <= 1; i += steps) {
            double t = sin(i) + cos(i) - 2 * atan(i);
            std::cout << t << ' ';
            if (t < min) {
                min = t;
                ans = i;
            }
        }
    }

    std::cout << "\nStep: " << steps << "\nRoot: y = " << min << ", x = " << ans;
    return 0;
}