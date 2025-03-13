#include "Canvas.h"

Canvas::Canvas() {
    grid = new char*[height];
    for (int i = 0; i < height; ++i) {
        grid[i] = new char[width];
    }
    clearGrid();
}

Canvas::~Canvas() {
    for (int i = 0; i < height; ++i) {
        delete[] grid[i];
    }
    delete[] grid;
}

void Canvas::addFigure(std::shared_ptr<Figure> figure) {
    figures.push_back(std::move(figure));
}

void Canvas::removeFigure(int id) {
    figures.erase(
            std::remove_if(figures.begin(), figures.end(),
                           [id](const std::shared_ptr<Figure>& figure) {
                               return figure->getId() == id;
                           }),
            figures.end());
}

void Canvas::moveFigure(int id, int deltaX, int deltaY) {
    for (auto& figure : figures) {
        if (figure->getId() == id) {
            figure->move(deltaX, deltaY);
            break;
        }
    }
}

void Canvas::clearGrid() {
    for (int i = 0; i < height; ++i) {
        for (int j = 0; j < width; ++j) {
            if (i == 0 || i == height - 1) {
                grid[i][j] = '-';
            } else if (j == 0 || j == width - 1) {
                grid[i][j] = '|';
            } else {
                grid[i][j] = ' ';
            }
        }
    }
}

void Canvas::repaint() {
    clearGrid();

    for (const auto& figure : figures) {
        figure->draw(grid, height, width);
    }

    for (int i = 0; i < height; ++i) {
        for (int j = 0; j < width; ++j) {
            if (i == 0 || i == height - 1) {
                grid[i][j] = '-';
            } else if (j == 0 || j == width - 1) {
                grid[i][j] = '|';
            }
            std::cout << grid[i][j];
        }
        std::cout << "\n";
    }
}

const std::vector<std::shared_ptr<Figure>>& Canvas::getFigures() const {
    return figures;
}