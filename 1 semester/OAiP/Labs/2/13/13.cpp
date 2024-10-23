#include <iostream>

int main() {
    system("chcp 65001");

    long long k;
    std::cin >> k;

    std::cout << "Мы нашли " << k << " гриб";

    if (k % 100 >= 15 || k % 100 <= 10){
        if((k % 10) >= 2 && (k % 10) <= 4) std::cout << "а";
        else if((k % 10) >= 5 || (k % 10) == 0) std::cout << "ов";
    }
    else std::cout << "ов";

    std::cout << " в лесу";

    return 0;
}