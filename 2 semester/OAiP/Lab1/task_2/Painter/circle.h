#ifndef CIRCLE_H
#define CIRCLE_H

#include "figure.h"

class Circle : public Figure{
public:
    Circle(Figure *parent = nullptr);

    void draw(QPainter *painter) override;
    void paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget) ;

    void forPort(){}
};

#endif // CIRCLE_H
