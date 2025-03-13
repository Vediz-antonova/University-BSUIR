#include <gtest/gtest.h>
#include "../Rectangle.h"

TEST(RectangleTest, ConstructorAndGetters) {
    Rectangle rectangle(1, 10, 10, true, 4, 2);

    EXPECT_EQ(rectangle.getId(), 1);
    EXPECT_EQ(rectangle.getX(), 10);
    EXPECT_EQ(rectangle.getY(), 10);
    EXPECT_EQ(rectangle.getWidth(), 4);
    EXPECT_EQ(rectangle.getHeight(), 2);
    EXPECT_TRUE(rectangle.getIsFilling());
}

TEST(RectangleTest, Move) {
    Rectangle rectangle(1, 10, 10, true, 4, 2);

    rectangle.move(5, -3);

    EXPECT_EQ(rectangle.getX(), 15);
    EXPECT_EQ(rectangle.getY(), 7);
}

TEST(RectangleTest, DrawWithinBounds) {
    const int width = 20, height = 10;
    char** grid;
    grid = new char*[height];
    for (int i = 0; i < height; ++i) {
        grid[i] = new char[width];
    }

    for (int i = 0; i < height; ++i)
        for (int j = 0; j < width; ++j)
            grid[i][j] = ' ';

    Rectangle rectangle(1, 5, 5, true, 4, 2);
    rectangle.draw(grid, height, width);

    EXPECT_EQ(grid[5][5], '*');
    EXPECT_EQ(grid[6][5], '*');
    EXPECT_EQ(grid[5][8], '*');
}


