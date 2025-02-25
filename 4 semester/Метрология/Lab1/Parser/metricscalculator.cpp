#include "metricscalculator.h"
#include <QRegularExpression>
#include <QStack>
#include <QtMath>

MetricsCalculator::MetricsCalculator(const QString &code)
    : code(removeComments(code))
{
    initOperators();
    initOperands();
    calculateOperators();
    calculateOperands();
}

QString MetricsCalculator::removeComments(const QString& code) const {
    QString result;
    bool inSingleLineComment = false;
    bool inMultiLineComment = false;

    for (int i = 0; i < code.length(); ++i) {
        QChar ch = code[i];

        if (inSingleLineComment) {
            if (ch == '\n') {
                inSingleLineComment = false;
                result += ch;
            }
        } else if (inMultiLineComment) {
            if (ch == '*' && i + 1 < code.length() && code[i + 1] == '/') {
                inMultiLineComment = false;
                ++i;
            }
        } else {
            if (ch == '/' && i + 1 < code.length() && code[i + 1] == '/') {
                inSingleLineComment = true;
                ++i;
            } else if (ch == '/' && i + 1 < code.length() && code[i + 1] == '*') {
                inMultiLineComment = true;
                ++i;
            } else {
                result += ch;
            }
        }
    }

    return result;
}

QString MetricsCalculator::removeTextInQuotes(const QString& code) const {
    QString result = code;
    result.replace(QRegularExpression("\".*?\""), "\"\"");
    result.replace(QRegularExpression("'.*?'"), "''");
    return result;
}

QVector<QPair<QString, int>> MetricsCalculator::countTextInQuotes(const QString& code) const {
    QVector<QPair<QString, int>> count;
    int doubleQuotesCount = code.count(QRegularExpression("\".*?\""));
    int singleQuotesCount = code.count(QRegularExpression("'.*?'"));
    count.append(qMakePair(QString("\"\""), doubleQuotesCount));
    count.append(qMakePair(QString("''"), singleQuotesCount));
    return count;
}

void MetricsCalculator::initOperators() {
    operatorsCount = {
        {"=", 0}, {"+=", 0}, {"-=", 0}, {"*=", 0}, {"/=", 0}, {"%=", 0},
        {"&=", 0}, {"|=", 0}, {"^=", 0}, {"<<=", 0}, {">>=", 0},
        {"+", 0}, {"-", 0}, {"*", 0}, {"/", 0}, {"%", 0},
        {"++", 0}, {"--", 0},
        {"==", 0}, {"!=", 0}, {"<", 0}, {"<=", 0}, {">", 0}, {">=", 0},
        {"&&", 0}, {"||", 0}, {"!", 0},
        {"&", 0}, {"|", 0}, {"^", 0}, {"&^", 0}, {"<<", 0}, {">>", 0},
        {".", 0}, {"&", 0}, {"*", 0},
        {"if", 0}, {"else", 0}, {"for", 0}, {"break", 0}, {"continue", 0},
        {"switch", 0}, {"case", 0}, {"default", 0}, {"return", 0}
    };
}

void MetricsCalculator::initOperands() {
    operandsCount= {{"true", 0}, {"false", 0}};
}

QVector<QPair<QString, int>> MetricsCalculator::countBinaryOperators(const QString& code) const {
    QVector<QPair<QString, int>> binaryOperators = {
        {"+", code.count(QRegularExpression("\\+(?!=)"))},
        {"-", code.count(QRegularExpression("-(?!=)"))},
        {"*", code.count(QRegularExpression("(?<!\\*)\\*(?![*=])"))},
        {"/", code.count(QRegularExpression("/(?!=)"))},
        {"%", code.count(QRegularExpression("%(?!=)"))},
        {"<<", code.count(QRegularExpression("<<"))},
        {">>", code.count(QRegularExpression(">>"))},
        {">", code.count(QRegularExpression("(?<![=>])>(?![>=])"))},
        {"<", code.count(QRegularExpression("(?<!<)<(?![=<])"))},
        {"=", code.count(QRegularExpression("(?<![=!><+\\-*/%])=(?!=)"))},
        {"!=", code.count(QRegularExpression("!="))},
        {"==", code.count(QRegularExpression("(?!=)==(?!=)"))},
        {"&&", code.count(QRegularExpression("&&"))},
        {"||", code.count(QRegularExpression("\\|\\|"))},
        {"!", code.count(QRegularExpression("!(?!=)"))},
        {"+=", code.count(QRegularExpression("\\+="))},
        {"-=", code.count(QRegularExpression("-="))},
        {"*=", code.count(QRegularExpression("\\*="))},
        {"/=", code.count(QRegularExpression("/="))},
        {"%=", code.count(QRegularExpression("%="))},
        {"&=", code.count(QRegularExpression("&="))},
        {"|=", code.count(QRegularExpression("\\|="))},
        {"^=", code.count(QRegularExpression("\\^="))},
        {"<<=", code.count(QRegularExpression("<<="))},
        {">>=", code.count(QRegularExpression(">>="))},
        {"++", code.count(QRegularExpression("\\+\\+"))},
        {"--", code.count(QRegularExpression("--"))},
        {".", code.count(QRegularExpression("\\."))}
    };
    return binaryOperators;
}

QVector<QPair<QString, int>> MetricsCalculator::countKeywords(const QString& code) const {
    QVector<QPair<QString, int>> keywordsCount = {
        {"if", code.count(QRegularExpression("\\bif\\b"))},
        {"else", code.count(QRegularExpression("\\belse\\b"))},
        {"for", code.count(QRegularExpression("\\bfor\\b"))},
        {"switch", code.count(QRegularExpression("\\bswitch\\b"))},
        {"case", code.count(QRegularExpression("\\bcase\\b"))},
        {"return", code.count(QRegularExpression("\\breturn\\b"))},
        {"break", code.count(QRegularExpression("\\bbreak\\b"))},
        {"continue", code.count(QRegularExpression("\\bcontinue\\b"))},
        {"default", code.count(QRegularExpression("\\bdefault\\b"))}
    };
    return keywordsCount;
}


QVector<QPair<QString, int>> MetricsCalculator::countOperands(const QString& code) const {
    QVector<QPair<QString, int>> operandsCount;
    QRegularExpression numberRegex("\\b\\d+\\b");
    QRegularExpressionMatchIterator numberMatches = numberRegex.globalMatch(code);

    while (numberMatches.hasNext()) {
        QRegularExpressionMatch match = numberMatches.next();
        QString number = match.captured();
        bool found = false;
        for (auto& pair : operandsCount) {
            if (pair.first == number) {
                pair.second++;
                found = true;
                break;
            }
        }
        if (!found) {
            operandsCount.append(qMakePair(number, 1));
        }
    }

    return operandsCount;
}

QVector<QPair<QString, int>> MetricsCalculator::countVariables(const QString& code) const {
    QVector<QPair<QString, int>> variablesCount;
    QRegularExpression varRegex("\\b\\w+(?=\\s*=)");
    QRegularExpressionMatchIterator varMatches = varRegex.globalMatch(code);

    while (varMatches.hasNext()) {
        QRegularExpressionMatch match = varMatches.next();
        QString varName = match.captured();
        bool found = false;
        for (auto& pair : variablesCount) {
            if (pair.first == varName) {
                pair.second++;
                found = true;
                break;
            }
        }
        if (!found) {
            variablesCount.append(qMakePair(varName, 1));
        }
    }

    return variablesCount;
}

void MetricsCalculator::calculateOperators() {
    QVector<QPair<QString, int>> binaryOperators = countBinaryOperators(code);
    QVector<QPair<QString, int>> keywords = countKeywords(code);

    for (const auto& pair : binaryOperators) {
        bool found = false;
        for (auto& opPair : operatorsCount) {
            if (opPair.first == pair.first) {
                opPair.second += pair.second;
                found = true;
                break;
            }
        }
        if (!found) {
            operatorsCount.append(pair);
        }
    }

    for (const auto& pair : keywords) {
        bool found = false;
        for (auto& opPair : operatorsCount) {
            if (opPair.first == pair.first) {
                opPair.second += pair.second;
                found = true;
                break;
            }
        }
        if (!found) {
            operatorsCount.append(pair);
        }
    }
}

void MetricsCalculator::calculateOperands() {
    QVector<QPair<QString, int>> textInQuotes = countTextInQuotes(code);
    QVector<QPair<QString, int>> operands = countOperands(code);
    QVector<QPair<QString, int>> variables = countVariables(code);

    for (const auto& pair : textInQuotes) {
        bool found = false;
        for (auto& opPair : operandsCount) {
            if (opPair.first == pair.first) {
                opPair.second += pair.second;
                found = true;
                break;
            }
        }
        if (!found) {
            operandsCount.append(pair);
        }
    }

    for (const auto& pair : operands) {
        bool found = false;
        for (auto& opPair : operandsCount) {
            if (opPair.first == pair.first) {
                opPair.second += pair.second;
                found = true;
                break;
            }
        }
        if (!found) {
            operandsCount.append(pair);
        }
    }

    for (const auto& pair : variables) {
        bool found = false;
        for (auto& opPair : operandsCount) {
            if (opPair.first == pair.first) {
                opPair.second += pair.second;
                found = true;
                break;
            }
        }
        if (!found) {
            operandsCount.append(pair);
        }
    }
}

QVector<QPair<QString, int>> MetricsCalculator::getOperatorsCount() const {
    return operatorsCount;
}

QVector<QPair<QString, int>> MetricsCalculator::getOperandsCount() const {
    return operandsCount;
}
