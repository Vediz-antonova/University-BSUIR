#include <gtest/gtest.h>
#include <sstream>
#include "../Actions.h"
#include "../Circle.h"

TEST(ActionsTest, UndoWithoutActions) {
    Canvas canvas;
    Actions actions;

    std::stringstream buffer;
    std::streambuf* oldCerr = std::cerr.rdbuf(buffer.rdbuf());

    actions.undo(canvas);

    std::cerr.rdbuf(oldCerr);

    EXPECT_EQ(buffer.str(), "Undo operation failed: no actions to undo\n");
}

TEST(ActionsTest, RedoWithoutActions) {
    Canvas canvas;
    Actions actions;

    std::stringstream buffer;
    std::streambuf* oldCerr = std::cerr.rdbuf(buffer.rdbuf());

    actions.redo(canvas);

    std::cerr.rdbuf(oldCerr);

    EXPECT_EQ(buffer.str(), "Redo operation failed: no actions to redo\n");
}

TEST(ActionsTest, UndoAfterAddingFigure) {
    Canvas canvas;
    Actions actions;

    auto circle = std::make_shared<Circle>(1, 10, 10, true, 5);
    actions.addAction({"add", 1, 0, 0, 0, 0, circle});
    actions.undo(canvas);

    EXPECT_EQ(canvas.getFigures().size(), 0);
}

TEST(ActionsTest, RedoAfterUndo) {
    Canvas canvas;
    Actions actions;

    auto circle = std::make_shared<Circle>(1, 10, 10, true, 5);
    actions.addAction({"add", 1, 0, 0, 0, 0, circle});
    actions.undo(canvas);
    actions.redo(canvas);

    EXPECT_EQ(canvas.getFigures().size(), 1);
    EXPECT_EQ(canvas.getFigures().front()->getId(), 1);
}
