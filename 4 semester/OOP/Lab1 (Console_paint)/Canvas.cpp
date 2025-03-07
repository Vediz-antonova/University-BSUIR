#include "Canvas.h"

Canvas::Canvas() {
    clearGrid();
}

void Canvas::addFigure(std::unique_ptr<Figure> figure) {
    figures.push_back(std::move(figure));
    repaint();
}

void Canvas::repaint() {
    clearGrid();

    for (const auto& figure : figures) {
        figure->draw(grid, height, width);
    }

    for (int i = 0; i < height; ++i) {
        for (int j = 0; j < width; ++j) {
            std::cout << grid[i][j];
        }
        std::cout << "\n";
    }
}

void Canvas::drawCanvas() {
    clearGrid();

    for (const auto& figure : figures) {
        figure->draw(grid, height, width);
    }

    for (int i = 0; i < height; ++i) {
        for (int j = 0; j < width; ++j) {
            std::cout << grid[i][j];
        }
        std::cout << "\n";
    }
}

void Canvas::clearGrid() {
    for (int i = 0; i < height; ++i) {
        for (int j = 0; j < width; ++j) {
            grid[i][j] = '.';
        }
    }
}

void Canvas::moveFigure(int id, int deltaX, int deltaY) {
    for (auto& figure : figures) {
        if (figure->getId() == id) {
            figure->move(deltaX, deltaY);
            break;
        }
    }

    repaint();
}

void Canvas::removeFigure(int id) {
    figures.erase(
            std::remove_if(figures.begin(), figures.end(),
                           [id](const std::unique_ptr<Figure>& figure) {
                               return figure->getId() == id;
                           }),
            figures.end());

    repaint();
}

const std::vector<std::unique_ptr<Figure>>& Canvas::getFigures() const {
    return figures;
}

void Canvas::clear() {
    figures.clear();
}