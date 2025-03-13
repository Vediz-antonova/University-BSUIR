#include <gtest/gtest.h>
#include "../Canvas.h"
#include "../Rectangle.h"

TEST(CanvasTest, AddFigure) {
    Canvas canvas;
    auto rectangle = std::make_unique<Rectangle>(1, 5, 5, true, 4, 2);

    canvas.addFigure(std::move(rectangle));

    EXPECT_EQ(canvas.getFigures().size(), 1);
}

TEST(CanvasTest, MoveFigure) {
    Canvas canvas;
    auto rectangle = std::make_unique<Rectangle>(1, 5, 5, true, 4, 2);
    canvas.addFigure(std::move(rectangle));

    canvas.moveFigure(1, 3, 2);
    const auto& figures = canvas.getFigures();
    EXPECT_EQ(figures.front()->getX(), 8);
}
