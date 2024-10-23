#include <iostream>

long int power(long int a, long int n){
    long int b = a;

    if (n == 0) return 1;
    for(long int i = 1; i < n; ++i)
        a *= b;

    return a;
}

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

int main() {
    std::cout << "9. Find a - 2a + 4a - ..." << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';

    long int b, q = -2.0, S = 0.0;
    long int n;
    std::cout << "Enter a and n..." << '\n';
    b = GetValue();
    n = GetValue();

    S = b * (1 - power(q, n)) / (1 - q);

    std::cout << "Sum is ";
    std::cout << S << '\n';

    return 0;
}