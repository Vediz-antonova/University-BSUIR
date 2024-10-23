#include <iostream>

int main() {
    int h1, min1, h2, min2;

    std::cin >> h1 >> min1 >> h2 >> min2;

    int h3 = 0, min3 = 0;

    if (h1 <= h2) {
        h3 = h2 - h1;
        if (min2 >= min1)
            min3 = min2 - min1;
        else {
            min3 = 60 - min1 + min2;
            h3--;
        }
    }
    else {
        h3 = 24 - h1 + h2;
        if (min2 >= min1)
            min3 = min2 - min1;
        else {
            min3 = 60 - min1 + min2;
            h3--;
        }
    }

    std::cout << h3 << ' ' << min3 << '\n';

    return 0;
}