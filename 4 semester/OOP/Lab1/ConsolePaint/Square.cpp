#include "Square.h"

Square::Square(int id, int x, int y, bool isFilling, int length)
        : Figure(id, x, y, isFilling), length(length) {}

std::string Square::info() const {
    return "Square. \n" + Figure::info() + ", Length: " + std::to_string(length);
}

void Square::draw(char** grid, int heightGrid, int widthGrid) {
    if (getIsFilling()){
        fill(grid, heightGrid, widthGrid);
        return;
    }

    for (int i = 0; i < length; ++i) {
        for (int j = 0; j < length; ++j) {
            int newX = getX() + j;
            int newY = getY() + i;
            if ((i == 0 || i == length - 1 || j == 0 || j == length - 1) &&
                newX >= 0 && newX < widthGrid && newY >= 0 && newY < heightGrid) {
                grid[newY][newX] = '*';
            }
        }
    }
}

void Square::fill(char** grid, int heightGrid, int widthGrid) {
    for (int i = 0; i < length; ++i) {
        for (int j = 0; j < length; ++j) {
            int newX = getX() + j;
            int newY = getY() + i;
            if (newX >= 0 && newX < widthGrid && newY >= 0 && newY < heightGrid) {
                grid[newY][newX] = '*';
            }
        }
    }

    setIsFilling(true);
}

int Square::getLength() const {
    return length;
}

void Square::saveToFile(std::ostream& os) const {
    os << "Square " << getId() << " " << getX() << " " << getY() << " " << getIsFilling() << " " << length << "\n";
}

void Square::loadFromFile(std::istream& is) {
    is >> id >> x >> y >> isFilling >> length;
}