#include <iostream>
#include <cmath>

int main() {
    long double a, z, x, y;
    int n;
    std::cin >> a >> x >> n;

    if(z < 0) x = z*z;
    else if(z >= 0) x = z+1;

    y = a * log(1 + powl(x, 1.0 / 5.0));

    switch(n){
        case 1:
            y += cos(2*x + 1) * cos(2*x + 1);
            break;
        case 2:
            y += cos(x*x*x + 1) * cos(x*x*x + 1);
            break;
        case 3:
            y += cos(x/3 + 1) * cos(x/3 + 1);
            break;
    }

    std::cout << y;

    return 0;
}