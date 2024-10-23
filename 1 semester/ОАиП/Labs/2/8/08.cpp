#include <iostream>
#include <cmath>
#include <complex>
#include <iomanip>

int main() {
    int N = 1;
    long double a = 0, b = 0, c = 0, p = 0, q = 0, D;
    long double x1, x2, x3, x4, y1, y2, k;
    std::complex<long double> xi1, ai, bi;
    int t;
//    std::cin >> t;
//    for(int i = 0; i < t; i++) {
    std::cout << "Number of solve" << '\n';
    std::cin >> N;

    switch (N) {
        case 1:
            //ax^4 + bx^2 + c = 0
            std::cout << "ax^4 + bx^2 + c = 0  => a, b, c = ?" << '\n';
            std::cin >> a >> b >> c;

            if (a == 0 && b == 0 && c == 0) {
                std::cout << "Infinite number of solutions\n";
            } else if (a == 0) {
                if (b == 0) {
                    std::cout << "There are no roots\n";
                } else if (b > 0) {
                    if (c < 0) {
                        x1 = -sqrtl(-c / b);
                        x2 = sqrtl(-c / b);
                        std::cout << ' ' << x1 << ' ' << x2;
                    } else if (c == 0) std::cout << '0';
                    else std::cout << "Complex Complex " << '\n';
                } else if (b < 0) {
                    if (c > 0) {
                        x1 = -sqrtl(-c / b);
                        x2 = sqrtl(-c / b);
                        std::cout << ' ' << x1 << ' ' << x2;
                    } else if (c == 0) std::cout << '0';
                    else std::cout << "Complex Complex " << '\n';
                }

            } else {
                D = b * b - 4 * a * c;

                if (D > 0) {
                    y1 = (-b + sqrtl(D)) / (2 * a);
                    y2 = (-b - sqrtl(D)) / (2 * a);

                    if (y1 > 0) {
                        x1 = sqrtl(y1);
                        x2 = -sqrtl(y1);
                        std::cout << ' ' << x1 << ' ' << x2;
                    } else if (y1 == 0) std::cout << '0';
                    else std::cout << "Complex Complex\n";

                    if (y2 > 0) {
                        x3 = sqrtl(y2);
                        x4 = -sqrtl(y2);
                        std::cout << ' ' << x3 << ' ' << x4;
                    } else if (y2 == 0) std::cout << '0';
                    else std::cout << "Complex Complex\n" << '\n';

                } else if (D == 0) {
                    y1 = -b / (2 * a);
                    if (y1 > 0) {
                        x1 = sqrtl(y1);
                        x2 = -sqrtl(y1);
                        std::cout << ' ' << x1 << ' ' << x2;
                    } else if (y1 == 0) std::cout << '0';
                    else std::cout << "Complex Complex" << '\n';
                } else std::cout << "Complex Complex Complex Complex" << '\n';
            }
            break;

        case 2:
            //ax^4 + bx^3 + cx^2 + bx + a = 0
            std::cout << "ax^4 + bx^3 + cx^2 + bx + a = 0  => a, b, c = ?" << '\n';

            std::cin >> a >> b >> c;
            if (a == 0 && b == 0 && c == 0) {
                std::cout << "Infinite number of solutions\n";
            } else if (a == 0) {
                if (b != 0 && c != 0) {
                    D = c * c - 4 * b * b;

                    if (D > 0) {
                        x1 = (-b + sqrtl(D)) / (2 * a);
                        x2 = (-b + sqrtl(D)) / (2 * a);
                        std::cout << ' ' << x1 << ' ' << x2;
                    } else if (D == 0) std::cout << (-b) / (2 * a) << '\n';
                    else std::cout << "Complex Complex" << '\n';
                } else if (c == 0 || b == 0) std::cout << "0\n";

            } else {
                D = b * b - 4 * a * (c - 2 * a);

                if (D > 0) {
                    y1 = (-b + sqrtl(D)) / (2 * a);
                    y2 = (-b - sqrtl(D)) / (2 * a);

                    if (y1 * y1 - 4 > 0) {
                        x1 = (y1 + sqrtl(y1 * y1 - 4)) / 2;
                        x2 = (y1 - sqrtl(y1 * y1 - 4)) / 2;
                        std::cout << ' ' << x1 << ' ' << x2;
                    } else if (y1 == 0) std::cout << '0';
                    else std::cout << "Complex Complex\n";

                    if (y2 * y2 - 4 > 0) {
                        x3 = (y2 + sqrtl(y2 * y2 - 4)) / 2;
                        x4 = (y2 - sqrtl(y2 * y2 - 4)) / 2;
                        std::cout << ' ' << x3 << ' ' << x4;
                    } else if (y2 == 0) std::cout << '0';
                    else std::cout << "Complex Complex" << '\n';

                } else if (D == 0) {
                    y1 = (-b + sqrtl(D)) / (2 * a);
                    if (y1 > 0) {
                        x1 = (y1 + sqrtl(y1 * y1 - 4)) / 2;
                        x2 = (y1 - sqrtl(y1 * y1 - 4)) / 2;
                        std::cout << ' ' << x1 << ' ' << x2;
                    } else if (y1 == 0) std::cout << '0';
                    else std::cout << "Complex Complex" << '\n';
                } else std::cout << "Complex Complex Complex Complex" << '\n';
            }
            break;

        case 3:
            //x^3 + px + q = 0
            std::cout << "x^3 + px + q = 0  => p, q = ?" << '\n';
            std::cin >> p >> q;

            k = q * q / 4.0 + p * p * p / 27.0;
            std::cout << sqrtl(k) << '\n';
            long double l = 2.0;

            ai = (-q / 2.0, sqrtl(k));
            std::cout << ai << '\n';
            bi = (-q / 2.0, -sqrtl(k));
            std::cout << bi << '\n';

            xi1 = (-pow(ai, 1 / 3) + pow(bi, 1 / 3) / l, (pow(ai, 1 / 3) - pow(bi, 1 / 3) * sqrtl(3) / l));
            std::cout << ' ' << xi1;
            std::cout << ' ' << std::conj(xi1);
            std::cout << ' ' << ai + bi;
            break;
    }
//    }

    return 0;
}
