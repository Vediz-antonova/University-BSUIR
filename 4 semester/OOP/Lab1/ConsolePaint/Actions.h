#ifndef CONSOLEPAINT_ACTIONS_H
#define CONSOLEPAINT_ACTIONS_H

#include <stack>
#include <memory>
#include "Figure.h"
#include "Canvas.h"

struct ActionRecord {
    std::string actionType; // "add", "remove", "move"
    int figureId;
    int xOld, yOld;
    int xNew, yNew;
    std::shared_ptr<Figure> figure;
};

class Actions {
private:
    std::stack<ActionRecord> history;    // undo
    std::stack<ActionRecord> redoStack;  // redo

public:
    Actions();

    void addAction(ActionRecord action);
    void undo(Canvas& canvas);
    void redo(Canvas& canvas);
};

#endif //CONSOLEPAINT_ACTIONS_H
