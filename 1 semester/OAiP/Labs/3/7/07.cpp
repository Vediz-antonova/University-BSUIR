#include <iostream>
#include <iomanip>
#include <cmath>

const long double e = 2.718281828;

long double power(long double a, long int n) {
    long double b = a;

    if (n == 0) return 1;
    for (long int i = 1; i < n; ++i)
        a *= b;

    return a;
}

long int factorial(long int a) {
    long int ans = 1;

    if (a == 0)
        return 1;

    for (long int i = 1; i <= a; ++i)
        ans *= i;

    return ans;
}

long double Abs(long double x) {
    return (x >= 0 ? x : -x);
}

long double Sin(long double x) {
    bool o = 0;
    while (x > 2 * M_PI) {
        x -= 2 * M_PI;
    }
    if (x > M_PI) {
        x -= M_PI;
        o = 1;
    }
    while (x < -2 * M_PI) {
        x += 2 * M_PI;
    }
    if (x < -M_PI) {
        x += M_PI;
        o = 1;
    }

    long double s = 0.0;
    for (int n = 0; n < 6; ++n) {
        s += power(-1, n) * power(x, 2 * n + 1) / factorial(2 * n + 1);
    }

    return (o == 0 ? s : -s);
}

long double Cos(long double x) {
    bool o = 0;
    while (x > 2 * M_PI) {
        x -= 2 * M_PI;
    }
    if (x > M_PI) {
        x -= M_PI;
        o = 1;
    }
    while (x < -2 * M_PI) {
        x += 2 * M_PI;
    }
    if (x < -M_PI) {
        x += M_PI;
        o = 1;
    }

    long double s = 0.0;
    for (int n = 0; n < 6; ++n) {
        s += power(-1, n) * power(x, 2 * n) / factorial(2 * n);
    }

    return (o == 0 ? s : -s);
}

long double Ln(long double x) {
    long double k = 0.0;
    while (x >= 2) {
        x /= e;
        k++;
    }

    long double result = 0.0;
    for (int n = 1; n <= 6; ++n) {
        result += power(-1, n + 1) / n * power(x - 1, n);
    }

    return result + k;
}

int main() {
    std::cout << "Find sin(x), cos(x) and ln|x| and find min of them" << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';

    long double x;
    std::cout << "x = ";
    std::cin >> x;

    std::cout << std::setprecision(15);
    long double c = Ln(Abs(x));

    long double d = logl(x);

//    while (x > 100000000000000 * M_PI) {
//        x -= 100000000000000 * M_PI;
//    }

    while (x > 1000000 * M_PI) {
        x -= 1000000 * M_PI;
    }
    long double a = Sin(x), b = Cos(x);
    std::cout << "sin(x) = " << a << ";   " << "cos(x) = " << b << ";   " << "ln(x) = " << c;

    std::cout << "\n\n";
    std::cout << "sin(x) = " << sinl(x) << ";   " << "cos(x) = " << cosl(x) << ";   " << "ln(x) = " << d << "\n";

    std::cout << "min: " << std::min(a, std::min(b, c));
}