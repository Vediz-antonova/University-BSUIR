#include <iostream>
#include <iomanip>

long int GetValue(){
    long int a;
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

int main(){
    std::cout << "2. Find the sum of the series d(n) = (1/2)^n + (1/3)^n" << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';

    long double sum = 0.0, t2 = 2.0, t3 = 3.0;
    long int n = 0;
    long double d = 1.0 / t2 + 1.0 / t3;

    std::cout << "Enter the number of members..." << '\n';
    n = GetValue();

    do {
        sum += d;
        t2 *= 2;
        t3 *= 3;
        d = 1 / t2 + 1 / t3;
        n--;
    } while (d >= 1e-3 && n > 0);

    std::cout << "\nSum is ";
    std::cout << std::setprecision(10);
    std::cout << sum << '\n';

    return 0;
}