#include "Rhombus.h"

Rhombus::Rhombus(int id, int x, int y, bool isFilling, int diagonal1, int diagonal2)
        : Figure(id, x, y, isFilling), diagonal1(diagonal1), diagonal2(diagonal2) {}

std::string Rhombus::info() const {
    return "Rhombus. \n" + Figure::info() +
           ", Diagonal1: " + std::to_string(diagonal1) +
           ", Diagonal2: " + std::to_string(diagonal2);
}

void Rhombus::draw(char** grid, int heightGrid, int widthGrid) {
    if (getIsFilling()){
        fill(grid, heightGrid, widthGrid);
        return;
    }

    int x1 = getX();
    int y1 = getY() - diagonal1 / 2;

    int x2 = getX() + diagonal2 / 2;
    int y2 = getY();

    int x3 = getX();
    int y3 = getY() + diagonal1 / 2;

    int x4 = getX() - diagonal2 / 2;
    int y4 = getY();

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
    plotLine(x3, y3, x4, y4);
    plotLine(x4, y4, x1, y1);
}

void Rhombus::fill(char** grid, int heightGrid, int widthGrid) {
    int x1 = getX();
    int y1 = getY() - diagonal1 / 2;

    int x2 = getX() + diagonal2 / 2;
    int y2 = getY();

    int x3 = getX();
    int y3 = getY() + diagonal1 / 2;

    int x4 = getX() - diagonal2 / 2;
    int y4 = getY();

    auto interpolateX = [](int x0, int y0, int x1, int y1, int y) -> int {
        if (y0 == y1) return x0;
        return x0 + (x1 - x0) * (y - y0) / (y1 - y0);
    };

    for (int py = y1; py <= y2; ++py) {
        if (py < 0 || py >= heightGrid) continue;
        int xLeft = interpolateX(x1, y1, x4, y4, py);
        int xRight = interpolateX(x1, y1, x2, y2, py);

        for (int px = xLeft; px <= xRight; ++px) {
            if (px >= 0 && px < widthGrid) {
                grid[py][px] = '*';
            }
        }
    }

    for (int py = y2; py <= y3; ++py) {
        if (py < 0 || py >= heightGrid) continue;
        int xLeft = interpolateX(x3, y3, x4, y4, py);
        int xRight = interpolateX(x3, y3, x2, y2, py);

        for (int px = xLeft; px <= xRight; ++px) {
            if (px >= 0 && px < widthGrid) {
                grid[py][px] = '*';
            }
        }
    }
}

int Rhombus::getDiagonal1() const {
    return diagonal1;
}

int Rhombus::getDiagonal2() const {
    return diagonal2;
}

void Rhombus::saveToFile(std::ostream& os) const {
    os << "Rhombus " << getId() << " " << getX() << " " << getY() << " " << getIsFilling() << " " << diagonal1 << " " << diagonal2 << "\n";
}

void Rhombus::loadFromFile(std::istream& is) {
    is >> id >> x >> y >> isFilling >> diagonal1 >> diagonal2;
}