#include <iostream>
#include <iomanip>
#include <math.h>

int main() {
    long double k, x, y;
    long double l;
    std::cin >> k >> x >> y;

    long double* r = new long double[4];

    long double min;

    if (k < 0) {
        x = -x;
        y = -y;
        k = -k;
    }
    if ((x <= k && (y == 0 || y == k)) || (y <= k && (x == 0 || x == k))) {
        //на
        l = 0;
    }
    else if ((x > 0 && y > 0 && x < k && y < k) || (x < 0 && y < 0 && x > k && y > k)) {
        //внутри
        r[0] = sqrtl((x - 0) * (x - 0));
        r[1] = sqrtl((x - k) * (x - k));
        r[2] = sqrtl((y - 0) * (y - 0));
        r[3] = sqrtl((y - k) * (y - k));

        min = r[0];
        for (int i = 0; i < 4; i++)
            if (min > r[i])
                min = r[i];

        l = min;
    }
    else {
        //снаружи
        if (x > k && y > k) {
            l = sqrtl((y - k) * (y - k) + (x - k) * (x - k));
        }
        else if (x > k && (y > 0 && y < k || y < 0 && y > k)) {
            l = sqrtl((x - k) * (x - k));
        }
        else if (y > k && (x > 0 && x < k || x < 0 && x > k)) {
            l = sqrtl((y - k) * (y - k));
        }
        else if (y > k && x < 0) {
            l = sqrtl((y - k) * (y - k) + (x - 0) * (x - 0));
        }
        else if (x > k && y < 0) {
            l = sqrtl((y - 0) * (y - 0) + (x - k) * (x - k));
        }
        else if (x < 0 && y < 0) {
            l = sqrtl((y - 0) * (y - 0) + (x - 0) * (x - 0));
        }
        else if (y < 0 && x > 0 && x < k) {
            l = sqrtl((y - 0) * (y - 0));
        }
        else if (x < 0 && y > 0 && y < k) {
            l = sqrtl((x - 0) * (x - 0));
        }
    }

    std::cout << std::setprecision(22) << l << '\n';

    return 0;
}