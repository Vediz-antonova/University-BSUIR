#include <gtest/gtest.h>
#include "../CanvasSerializer.h"

TEST(SerializerTest, SaveToFileWithEmptyCanvas) {
    Canvas canvas;

    std::stringstream buffer;
    std::streambuf* oldCerr = std::cerr.rdbuf(buffer.rdbuf());

    CanvasSerializer::saveToFile(canvas, "nonexistent_directory/empty_canvas.txt");

    std::cerr.rdbuf(oldCerr);

    EXPECT_EQ(buffer.str(), "Error: Unable to open file for saving: nonexistent_directory/empty_canvas.txt\n");
}

TEST(SerializerTest, LoadFromFileWithEmptyCanvas) {
    Canvas canvas;

    std::stringstream buffer;
    std::streambuf* oldCerr = std::cerr.rdbuf(buffer.rdbuf());

    CanvasSerializer::loadFromFile(canvas, "nonexistent_directory/empty_canvas.txt");

    std::cerr.rdbuf(oldCerr);

    EXPECT_EQ(buffer.str(), "Error: Unable to open file for loading: nonexistent_directory/empty_canvas.txt\n");
}

TEST(SerializerTest, SaveAndLoadCanvas) {
    Canvas canvas;
    auto circle = std::make_unique<Circle>(1, 10, 10, true, 5);
    canvas.addFigure(std::move(circle));

    CanvasSerializer::saveToFile(canvas, "test_canvas.txt");

    Canvas newCanvas;
    CanvasSerializer::loadFromFile(newCanvas, "test_canvas.txt");

    EXPECT_EQ(newCanvas.getFigures().size(), 1);
}
