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
    QString removeComments(const QString& code) const;
    QString removeTextInQuotes(const QString& code) const;
    QVector<QPair<QString, int>> countTextInQuotes(const QString& code) const;
    QVector<QPair<QString, int>> countBinaryOperators(const QString& code) const;
    QVector<QPair<QString, int>> countKeywords(const QString& code) const;
    QVector<QPair<QString, int>> countOperands(const QString& code) const;
    QVector<QPair<QString, int>> countVariables(const QString& code) const;
};

#endif // METRICSCALCULATOR_H
