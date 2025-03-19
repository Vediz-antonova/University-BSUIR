#include <QString>
#include <QVector>
#include <QPair>

#ifndef METRICSCALCULATOR_H
#define METRICSCALCULATOR_H

class MetricsCalculator
{
public:
    MetricsCalculator(const QString &code);

    QVector<QPair<QString, int>> getOperatorsCount() const;
    QVector<QPair<QString, int>> getOperandsCount() const;

private:
    QString code;
    QVector<QPair<QString, int>> operatorsCount;
    QVector<QPair<QString, int>> operandsCount;

    void initOperators();
    void initOperands();
    void calculateOperators();
    void calculateOperands();
    void countFunctionCalls(QVector<QPair<QString, int>> &binaryOperators);

    void countNumericConstants();
    void findOperands();

    QString removeComments(const QString& code) const;
    QString removeTextInQuotes(const QString& code) const;
    QVector<QPair<QString, int>> countTextInQuotes() const;
    QVector<QPair<QString, int>> countBinaryOperators() const;
    QVector<QPair<QString, int>> countKeywords() const;

    bool isOperator(const QString &word) const;

    void removeGotoLabels();
};

#endif // METRICSCALCULATOR_H
