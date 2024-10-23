#include <iostream>
#include <cmath>

int main(){
    double R, p, q;
    std::cin >> R >> p >> q;

    double R0 = p/(2*sin(q));
    std::cout << (R <= R0 ? "YES" : "NO") << '\n';

    return 0;
}