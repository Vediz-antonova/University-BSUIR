#include "Triangle.h"

Triangle::Triangle(int id, int x, int y, bool isFilling, int sideA, int sideB, int sideC)
        : Figure(id, x, y, isFilling), sideA(sideA), sideB(sideB), sideC(sideC) {}

std::string Triangle::info() const {
    return "Triangle. \n" + Figure::info() +
           ", SideA: " + std::to_string(sideA) +
           ", SideB: " + std::to_string(sideB) +
           ", SideC: " + std::to_string(sideC);
}

void Triangle::draw(char** grid, int heightGrid, int widthGrid) {
    if (sideA + sideB <= sideC || sideA + sideC <= sideB || sideB + sideC <= sideA) {
        std::cerr << "Invalid triangle sides!\n";
        return;
    }

    if (getIsFilling()){
        fill(grid, heightGrid, widthGrid);
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

    auto plotLine = [&](int x0, int y0, int x1, int y1) {
        int dx = abs(x1 - x0), dy = abs(y1 - y0);
        int sx = (x0 < x1) ? 1 : -1;
        int sy = (y0 < y1) ? 1 : -1;
        int err = dx - dy;

        while (true) {
            if (x0 >= 0 && x0 < widthGrid && y0 >= 0 && y0 < heightGrid) {
                grid[y0][x0] = '*';
            }
            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
        }
    };

    plotLine(x1, y1, x2, y2);
    plotLine(x2, y2, x3, y3);
    plotLine(x3, y3, x1, y1);
}

void Triangle::fill(char** grid, int heightGrid, int widthGrid) {
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


int Triangle::getSideA() const {
    return sideA;
}

int Triangle::getSideB() const {
    return sideB;
}

int Triangle::getSideC() const {
    return sideC;
}

void Triangle::saveToFile(std::ostream& os) const {
    os << "Triangle " << getId() << " " << getX() << " " << getY() << " " << getIsFilling() << " " <<
    sideA << " " << sideB << " " << sideC << "\n";
}

void Triangle::loadFromFile(std::istream& is) {
    is >> id >> x >> y >> isFilling >> sideA >> sideB >> sideC;
}