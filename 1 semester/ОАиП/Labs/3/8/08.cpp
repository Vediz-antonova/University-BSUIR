#include <iostream>

int power(int a, int n){
   int b = a;

   if (n == 0) return 1;
   for(int i = 1; i < n; ++i)
       a *= b;

   return a;
}

int main() {
    std::cout << "8. Find all Armstrong numbers up to n" << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';

    int arm = 0;
    std::cin >> arm;
    int a = 1, k = 0, b = 0, l = 0;

    while(a < arm){
        l = 0;
        k = 1;
        b = a;

        while (b >= 10){
            ++k;
            b /= 10;
        }

        for (int i = 1; i <= k; ++i) {
            l += power((a % power(10, i)) / power(10, i-1), k);
        }

        if (l == a)
            std::cout << a << '\n';

        a++;
    }

    return 0;
}