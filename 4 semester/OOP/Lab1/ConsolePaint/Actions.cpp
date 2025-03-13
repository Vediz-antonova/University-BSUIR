#include "Actions.h"

Actions::Actions() {}

void Actions::addAction(ActionRecord action) {
    history.push(std::move(action));
    while (!redoStack.empty()) {
        redoStack.pop();
    }
}

void Actions::undo(Canvas& canvas) {
    if (history.empty()) {
        std::cerr << "Undo operation failed: no actions to undo\n";
        return;
    }

    ActionRecord lastAction = std::move(history.top());
    history.pop();

    if (lastAction.actionType == "add") {
        canvas.removeFigure(lastAction.figureId);
    } else if (lastAction.actionType == "remove") {
        canvas.addFigure(lastAction.figure);
    } else if (lastAction.actionType == "move") {
        canvas.moveFigure(lastAction.figureId, lastAction.xOld - lastAction.xNew, lastAction.yOld - lastAction.yNew);
    }

    redoStack.push(std::move(lastAction));
}

void Actions::redo(Canvas& canvas) {
    if (redoStack.empty()) {
        std::cerr << "Redo operation failed: no actions to redo\n";
        return;
    }

    ActionRecord lastRedo = std::move(redoStack.top());
    redoStack.pop();

    if (lastRedo.actionType == "add") {
        canvas.addFigure(lastRedo.figure);
    } else if (lastRedo.actionType == "remove") {
        canvas.removeFigure(lastRedo.figureId);
    } else if (lastRedo.actionType == "move") {
        canvas.moveFigure(lastRedo.figureId, lastRedo.xNew - lastRedo.xOld, lastRedo.yNew - lastRedo.yOld);
    }

    history.push(std::move(lastRedo));
}