#include "Rectangle.h"

Rectangle::Rectangle(int id, int x, int y, bool isFilling, int width, int height)
        : Figure(id, x, y, isFilling), width(width), height(height) {}

std::string Rectangle::info() const {
    return "Rectangle. \n" + Figure::info() +
    ", Width: " + std::to_string(width) +
    ", Height: " + std::to_string(height);
}

void Rectangle::draw(char** grid, int heightGrid, int widthGrid) {
    if (getIsFilling()){
        fill(grid, heightGrid, widthGrid);
        return;
    }

    for (int i = 0; i < height; ++i) {
        for (int j = 0; j < width; ++j) {
            int newX = getX() + j;
            int newY = getY() + i;
            if ((i == 0 || i == height - 1 || j == 0 || j == width - 1) &&
                newX >= 0 && newX < widthGrid && newY >= 0 && newY < heightGrid) {
                grid[newY][newX] = '*';
            }
        }
    }
}

void Rectangle::fill(char** grid, int heightGrid, int widthGrid) {
    for (int i = 0; i < height; ++i) {
        for (int j = 0; j < width; ++j) {
            int newX = getX() + j;
            int newY = getY() + i;
            if (newX >= 0 && newX < widthGrid && newY >= 0 && newY < heightGrid) {
                grid[newY][newX] = '*';
            }
        }
    }

    setIsFilling(true);
}

int Rectangle::getWidth() const {
    return width;
}

int Rectangle::getHeight() const {
    return height;
}

void Rectangle::saveToFile(std::ostream& os) const {
    os << "Rectangle " << getId() << " " << getX() << " " << getY() << " " << getIsFilling() << " " << width << " " << height << "\n";
}

void Rectangle::loadFromFile(std::istream& is) {
    is >> id >> x >> y >> isFilling >> width >> height;
}