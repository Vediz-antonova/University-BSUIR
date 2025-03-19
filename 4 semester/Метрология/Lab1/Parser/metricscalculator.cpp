#include "metricscalculator.h"
#include <QRegularExpression>
#include <QStack>
#include <QtMath>

MetricsCalculator::MetricsCalculator(const QString &code)
    : code(removeTextInQuotes(removeComments(code)))
{
    initOperators();
    calculateOperators();
    initOperands();
    calculateOperands();

    removeGotoLabels();
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
    result.replace(QRegularExpression("\"[^\"]*\""), "\"\"");
    result.replace(QRegularExpression("`[^`]*`"), "``");
    result.replace(QRegularExpression("'[^']*'"), "''");
    return result;
}

QVector<QPair<QString, int>> MetricsCalculator::countTextInQuotes() const {
    QVector<QPair<QString, int>> count;
    int doubleQuotesCount = code.count(QRegularExpression("\".*?\""));
    int singleQuotesCount = code.count(QRegularExpression("'.*?'"));
    int backticksCount = code.count(QRegularExpression("`.*?`"));
    count.append(qMakePair(QString("\"\""), doubleQuotesCount));
    count.append(qMakePair(QString("''"), singleQuotesCount));
    count.append(qMakePair(QString("``"), backticksCount));
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
        {"{ }", 0}, {"( )", 0}, {"[ ]", 0},
        {"if", 0}, /*{"else", 0},*/ {"for", 0}, {"break", 0}, {"continue", 0},
        {"switch", 0}, {"case", 0}, {"default", 0}, {"return", 0}, {"select", 0},
        {"chan", 0}, {"const", 0}, {"struct", 0}, {"type", 0}, {"var", 0},
        {"defer", 0}, {"fallthrough", 0}, {"func", 0}, {"go", 0}, {"goto", 0},
        {"import", 0}, {"interface", 0}, {"map", 0}, {"package", 0}, {"range", 0}
    };
}

void MetricsCalculator::initOperands() {
    operandsCount= {{"true", 0}, {"false", 0}, {"iota", 0}, {"nil", 0}};
}

QVector<QPair<QString, int>> MetricsCalculator::countBinaryOperators() const {
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
        {".", code.count(QRegularExpression("\\."))},
        {"{ }", code.count(QRegularExpression("\\{"))},
        {"( )", code.count(QRegularExpression("\\("))},
        {"[ ]", code.count(QRegularExpression("\\["))}
    };
    return binaryOperators;
}

QVector<QPair<QString, int>> MetricsCalculator::countKeywords() const {
    QVector<QPair<QString, int>> keywordsCount = {
        {"if", code.count(QRegularExpression("\\bif\\b"))},
        //{"else", code.count(QRegularExpression("\\belse\\b"))},
        {"for", code.count(QRegularExpression("\\bfor\\b"))},
        {"switch", code.count(QRegularExpression("\\bswitch\\b"))},
        {"case", code.count(QRegularExpression("\\bcase\\b"))},
        {"return", code.count(QRegularExpression("\\breturn\\b"))},
        {"break", code.count(QRegularExpression("\\bbreak\\b"))},
        {"continue", code.count(QRegularExpression("\\bcontinue\\b"))},
        {"default", code.count(QRegularExpression("\\bdefault\\b"))},
        {"select", code.count(QRegularExpression("\\bselect\\b"))},
        {"chan", code.count(QRegularExpression("\\bchan\\b"))},
        {"const", code.count(QRegularExpression("\\bconst\\b"))},
        {"struct", code.count(QRegularExpression("\\bstruct\\b"))},
        {"type", code.count(QRegularExpression("\\btype\\b"))},
        {"var", code.count(QRegularExpression("\\bvar\\b"))},
        {"defer", code.count(QRegularExpression("\\bdefer\\b"))},
        {"fallthrough", code.count(QRegularExpression("\\bfallthrough\\b"))},
        {"func", code.count(QRegularExpression("\\bfunc\\b"))},
        {"go", code.count(QRegularExpression("\\bgo\\b"))},
        {"goto", code.count(QRegularExpression("\\bgoto\\b"))},
        {"import", code.count(QRegularExpression("\\bimport\\b"))},
        {"interface", code.count(QRegularExpression("\\binterface\\b"))},
        {"map", code.count(QRegularExpression("\\bmap\\b"))},
        {"package", code.count(QRegularExpression("\\bpackage\\b"))},
        {"range", code.count(QRegularExpression("\\brange\\b"))}
    };
    return keywordsCount;
}

/*QVector<QPair<QString, int>> MetricsCalculator::findBranches(QVector<QPair<QString, int>> binaryOperators) {
    QRegularExpression parenthesesPattern(R"(\([^()]+\))");
    int totalMatches = code.count(parenthesesPattern);

    int controlStructuresCount = 0;
    for (const auto& pair : binaryOperators) {
        if (pair.first == "if" || pair.first == "for" || pair.first == "switch" || pair.first == "goto") {
            controlStructuresCount += pair.second;
        }
    }

    int parenthesesExpressionsCount = totalMatches - controlStructuresCount;

    for (auto& pair : binaryOperators) {
        if (pair.first == "( )") {
            pair.second = parenthesesExpressionsCount;
            break;
        }
    }

    return binaryOperators;
}*/

void MetricsCalculator::countFunctionCalls(QVector<QPair<QString, int>> &binaryOperators) {
    QRegularExpression funcCallRegex(R"(\b([a-zA-Z_][a-zA-Z0-9_]*)\s*\((?!\)))");
    QRegularExpression funcDeclRegex(R"(\bfunc\s+([a-zA-Z_][a-zA-Z0-9_]*)\s*\()");

    QSet<QString> declaredFunctions;
    QRegularExpressionMatchIterator declMatches = funcDeclRegex.globalMatch(code);
    while (declMatches.hasNext()) {
        QString funcName = declMatches.next().captured(1);
        declaredFunctions.insert(funcName);
    }

    QRegularExpressionMatchIterator funcMatches = funcCallRegex.globalMatch(code);

    while (funcMatches.hasNext()) {
        QString funcName = funcMatches.next().captured(1);

        if (declaredFunctions.remove(funcName)) {
            continue;
        }

        for (auto& pair : binaryOperators) {
            if (pair.first == "( )") {
                pair.second--;
                break;
            }
        }

        bool found = false;
        for (auto &op : operatorsCount) {
            if (op.first == funcName) {
                op.second++;
                found = true;
                break;
            }

        }
        if (!found) {
            operatorsCount.append({funcName, 1});
        }
    }
}

void MetricsCalculator::calculateOperators() {
    QVector<QPair<QString, int>> binaryOperators = countBinaryOperators();
    countFunctionCalls(binaryOperators);

    QVector<QPair<QString, int>> keywords = countKeywords();

    for (const auto& pair : binaryOperators) {
        for (auto& opPair : operatorsCount) {
            if (opPair.first == pair.first) {
                opPair.second += pair.second;
                break;
            }
        }
    }

    for (const auto& pair : keywords) {
        for (auto& opPair : operatorsCount) {
            if (opPair.first == pair.first) {
                opPair.second += pair.second;
                break;
            }
        }
    }
}

QVector<QPair<QString, int>> MetricsCalculator::getOperatorsCount() const {
    return operatorsCount;
}

QVector<QPair<QString, int>> MetricsCalculator::getOperandsCount() const {
    return operandsCount;
}

bool MetricsCalculator::isOperator(const QString &word) const {
    for (const auto& op : operatorsCount) {
        if (op.first == word) {
            return true;
        }
    }
    return false;
}

void MetricsCalculator::calculateOperands() {
    QVector<QPair<QString, int>> operands = {
        {"true", code.count(QRegularExpression("\\btrue\\b"))},
        {"false", code.count(QRegularExpression("\\bfalse\\b"))},
        {"iota", code.count(QRegularExpression("\\biota\\b"))},
        {"nil", code.count(QRegularExpression("\\bnil\\b"))}
    };

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

    countNumericConstants();
    findOperands();
}

void MetricsCalculator::countNumericConstants() {
    QRegularExpression constRegex(R"(\b-?\d+(\.\d+)?\b)");
    QRegularExpressionMatchIterator constMatches = constRegex.globalMatch(code);

    while (constMatches.hasNext()) {
        QString constant = constMatches.next().captured();

        bool found = false;
        for (auto &op : operandsCount) {
            if (op.first == constant) {
                op.second++;
                found = true;
                break;
            }
        }
        if (!found) {
            operandsCount.append({constant, 1});
        }
    }
}

void MetricsCalculator::findOperands() {
    QVector<QPair<QString, int>> declarOp;
    QRegularExpression varAssignRegex(R"(\b([a-zA-Z_][a-zA-Z0-9_]*)(?:\s*,\s*[a-zA-Z_][a-zA-Z0-9_]*)*\s*:=)");
    QVector<QString> nakedVars;

    QRegularExpressionMatchIterator varAssignMatches = varAssignRegex.globalMatch(code);
    while (varAssignMatches.hasNext()) {
        QString match = varAssignMatches.next().captured();
        QString varName = "";

        for (const QChar &c : match) {
            if (c.isLetterOrNumber() || c == '_') {
                varName += c;
            } else if (c == ',' || c == ':') {
                if (!varName.isEmpty()) {
                    nakedVars.append(varName);
                    declarOp.append({varName, 1});
                    varName.clear();
                }
            }
        }
        if (!varName.isEmpty()) {
            nakedVars.append(varName);
            declarOp.append({varName, 1});
        }
    }

    QMap<QString, int> countVar;
    for (const QString &var : nakedVars) {
        QRegularExpression varUseRegex("\\b" + QRegularExpression::escape(var) + "\\b");
        countVar[var] = code.count(varUseRegex);
    }

    for (auto it = countVar.begin(); it != countVar.end(); ++it) {
        int totalCount = it.value();
        int declaredCount = 0;

        for (const auto &declared : declarOp) {
            if (it.key() == declared.first) {
                declaredCount += declared.second;
            }
        }

        int finalCount = totalCount - declaredCount;
        if (finalCount > 0) {
            operandsCount.append({it.key(), finalCount});
        }
    }

    ///////////////////////////////

    QVector<QString> operatorsList;
    for (const auto& op : operatorsCount) {
        operatorsList.append(op.first);
    }

    QVector<QPair<QString, int>> filteredOperands;
    for (const auto &operand : operandsCount) {
        if (!operatorsList.contains(operand.first)) {
            filteredOperands.append(operand);
        }
    }

    operandsCount = filteredOperands;
}

void MetricsCalculator::removeGotoLabels() {
    QRegularExpression labelRegex(R"(\bgoto\s+([a-zA-Z_][a-zA-Z0-9_]*)\s*\()");

    QRegularExpressionMatchIterator labelMatches = labelRegex.globalMatch(code);
    QSet<QString> labels;
    while (labelMatches.hasNext()) {
        QString label = labelMatches.next().captured(1);
        labels.insert(label);
    }

    QVector<QPair<QString, int>> filteredOperators;
    for (const auto &op : operatorsCount) {
        if (!labels.contains(op.first)) {
            filteredOperators.append(op);
        }
    }
    operatorsCount = filteredOperators;

    QVector<QPair<QString, int>> filteredOperands;
    for (const auto &operand : operandsCount) {
        if (!labels.contains(operand.first)) {
            filteredOperands.append(operand);
        }
    }
    operandsCount = filteredOperands;
}
