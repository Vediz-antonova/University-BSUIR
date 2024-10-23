#include <iostream>
#include <cmath>

int main(){
    std::cout << "3. Calculate y=sin(x)-cos(x) on the segment [a, b]" << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';

    const double pi = 3.1415926535;
    int A = 0, M = 20;
    double B = pi / 2.0, H = (B - A) / M, x = 0.0;

    for (int i = A; i <= B; ++i){
        x = A + i * H;

        std::cout << i << ". y = ";
        std::cout << sin(x) - cos(x) << '\n';
    }

    return 0;
}