#include <iostream>

long long GetValue(){
    long long a;
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
    std::cout << "4. Calculate the sum of even numbers from 1 to n" << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';

    long long n = 0, ans1 = 0;
    std::cout << "Enter n...";
    n = GetValue();

    for (long long i = 2; i <= n; i += 2){
            ans1 += i;
    }

    std::cout << "1. Sum is ";
    std::cout << ans1 << '\n';

    long long ans2 = 0;
    if (n % 2) --n;
    ans2 = (2 * 2 + 2 * (n / 2 - 1)) * n / 4;

    std::cout << "2. Sum is ";
    std::cout << ans2 <<'\n';

    return 0;
}
