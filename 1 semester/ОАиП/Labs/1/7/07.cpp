#include <iostream>
#include <cmath>

int main() {
    int x1, y1, x2, y2, x3, y3;

    std::cin >> x1 >> y1 >> x2 >> y2 >> x3 >> y3;

    double a = sqrt(pow(x1 - x2, 2) + pow(y1 - y2, 2));
    double b = sqrt(pow(x2 - x3, 2) + pow(y2 - y3, 2));
    double c = sqrt(pow(x1 - x3, 2) + pow(y1 - y3, 2));

    double P = a + b + c;
    double S = sqrt((P / 2) * (P / 2 - a) * (P / 2 - b) * (P / 2 - c));

    double ha = 2 * S / a;
    double hb = 2 * S / b;
    double hc = 2 * S / c;

    double r = 2 * S / P;
    double R = a * b * c / (4 * S);

    double Scircler = 3.14 * pow(r, 2);
    double ScircleR = 3.14 * pow(R, 2);
    double Ccircler = 2 * 3.14 * r;
    double CcircleR = 2 * 3.14 * R;

    double alpha = asin(a / (2 * R));
    double beta = asin(b / (2 * R));
    double gamma = asin(c / (2 * R));

    double S1 = 0.5 * b * c * sin(alpha);
    double S2 = a * b * c / (4 * R);

    double ma = 0.5 * sqrt(2 * pow(b, 2) + 2 * pow(c, 2) - pow(a, 2));
    double mb = 0.5 * sqrt(2 * pow(a, 2) + 2 * pow(c, 2) - pow(b, 2));
    double mc = 0.5 * sqrt(2 * pow(b, 2) + 2 * pow(a, 2) - pow(c, 2));

    double la = (sqrt(b * c * (b + c + a) * (b + c - a))) / (b + c);
    double lb = (sqrt(a * c * (a + c + b) * (a + c - b))) / (a + c);
    double lc = (sqrt(b * a * (b + a + c) * (b + a - c))) / (b + a);



    std::cout << "a = " << a << ", b = " << b << ", c = " << c << '\n';
    std::cout << "ha = " << ha << ", hb = " << hb << ", hc = " << hc << '\n';
    std::cout << "ma = " << ma << ", mb = " << mb << ", mc = " << mc << '\n';
    std::cout << "la = " << la << ", lb = " << lb << ", lc = " << lc << '\n';
    std::cout << "alpha = " << alpha << " radian, beta = " << beta << " radian, gamma = " << gamma << " radian" << '\n';
    std::cout << "alpha = " << alpha * 180 / 3.14 << " , beta = " << beta * 180 / 3.14 << ", gamma = " << gamma * 180 / 3.14 << '\n';
    std::cout << "r = " << r << ", R = " << R << '\n';
    std::cout << "r: S = " << Scircler << ", C = " << Ccircler << '\n';
    std::cout << "R: S = " << ScircleR << ", C = " << CcircleR << '\n';
    std::cout << "P = " << P << ", S = " << S << " || " << S1 << " || " << S2 << '\n';

    return 0;
}