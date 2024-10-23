#include <iostream>

int main(){
    std::cout << "1. Calculate the sum of 30 terms of the form (a - b)^2, where a and b depend on number of term" << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';

    int i = 0;
    long double N = 0;

    while(i <= 30){
        N += (i % 2 ? (i - i * i) * (i - i * i) : (i / 2 - i * i * i) * (i / 2 - i * i * i));

        ++i;
    }

    std::cout << "Sum is ";
    std::cout << N << '\n';

    return 0;
}
