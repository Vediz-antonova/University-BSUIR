#include <iostream>
#include <iomanip>
#include <cmath>

long double power(long double a, long int n){
    long double b = a;

    if (n == 0) return 1;
    for(long int i = 1; i < n; ++i)
        a *= b;

    return a;
}

long int factorial(long int a){
    long int ans = 1;

    if(a == 0)
        return 1;

    for(long int i = 1; i <= a; ++i)
        ans *= i;

    return ans;
}

int main(){
    std::cout << "5. Find the values of the functions S(x) and Y(x) and compare them" << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';
    std::cout << "Option 1" << '\n';

    long int n = 0, t = 0;
    long double x = 0.0, y = 0.0, s = 0.0;

    std::cout << "Enter n and number of operations" << '\n';
    while (true) {
        std::cin >> n >> t;

        if (std::cin.fail()){
            std::cin.clear();
            std::cin.ignore(32767,'\n');
            std::cout << "Oops, that input is invalid. Please try again.\n";
        }
        else {
            if(n > 0 && t > 0) {
                std::cin.ignore(32767, '\n');
                break;
            }
            else std::cout << "Oops, that input is invalid. Please try again.\n";
        }
    }

    for(long int k = 0; k < t; ++k){
        std::cout << "x = {0.1, ..., 1.0}\n";
        while (true) {
            std::cin >> x;

            if (std::cin.fail()){
                std::cin.clear();
                std::cin.ignore(32767,'\n');
                std::cout << "Oops, that input is invalid. Please try again.\n";
            }
            else {
                if(x >= 0.1 && x <= 1.0) {
                    std::cin.ignore(32767, '\n');
                    break;
                }
                else std::cout << "Oops, that input is invalid. Please try again.\n";
            }
        }

        s = 0.0;
        y = sin(x);

        for (int i = 0; i < n; ++i) {
            s += power(-1, i) * power(x, 2 * i + 1) / factorial(2 * i + 1);
        }

        std::cout << std::setprecision(10);
        std::cout << "S(x) = " << s << "\nY(x) = " << y << '\n';

        std::cout << "S(x) " << (s > y ? ">" : "<") << " Y(x)" << '\n';
    }

    return 0;
}
