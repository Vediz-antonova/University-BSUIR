#include "Triangle.h"
#include <iostream>
#include <cmath>

Triangle::Triangle(int id, int x, int y, const std::string& color, int sideA, int sideB, int sideC)
        : Figure(id, x, y, color), sideA(sideA), sideB(sideB), sideC(sideC) {}

void Triangle::draw(char (*grid)[40], int heightGrid, int widthGrid) const {
    if (sideA + sideB <= sideC || sideA + sideC <= sideB || sideB + sideC <= sideA) {
        std::cerr << "Invalid triangle sides!\n";
        return;
    }

    int x1 = getX();
    int y1 = getY();

    int x2 = x1 + sideA;
    int y2 = y1;

    double cosTheta = (sideA * sideA + sideB * sideB - sideC * sideC) / (2.0 * sideA * sideB);
    double sinTheta = std::sqrt(1 - cosTheta * cosTheta);
    int x3 = static_cast<int>(x1 + sideB * cosTheta);
    int y3 = static_cast<int>(y1 - sideB * sinTheta);

    auto isPointInTriangle = [&](int px, int py) {
        double areaFull = abs((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1));
        double area1 = abs((x1 - px) * (y2 - py) - (x2 - px) * (y1 - py));
        double area2 = abs((x2 - px) * (y3 - py) - (x3 - px) * (y2 - py));
        double area3 = abs((x3 - px) * (y1 - py) - (x1 - px) * (y3 - py));

        return (area1 + area2 + area3 == areaFull);
    };

    int minY = std::min(y1, std::min(y2, y3));
    int maxY = std::max(y1, std::max(y2, y3));
    int minX = std::min(x1, std::min(x2, x3));
    int maxX = std::max(x1, std::max(x2, x3));

    for (int py = minY; py <= maxY; ++py) {
        for (int px = minX; px <= maxX; ++px) {
            if (px >= 0 && px < widthGrid && py >= 0 && py < heightGrid && isPointInTriangle(px, py)) {
                grid[py][px] = '*';
            }
        }
    }
}
