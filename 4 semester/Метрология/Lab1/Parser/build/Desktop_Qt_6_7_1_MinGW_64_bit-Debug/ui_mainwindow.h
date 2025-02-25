/********************************************************************************
** Form generated from reading UI file 'mainwindow.ui'
**
** Created by: Qt User Interface Compiler version 6.7.1
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAINWINDOW_H
#define UI_MAINWINDOW_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QHBoxLayout>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QTableWidget>
#include <QtWidgets/QVBoxLayout>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_MainWindow
{
public:
    QWidget *centralwidget;
    QWidget *horizontalLayoutWidget;
    QHBoxLayout *horizontalLayout;
    QTableWidget *operatorsTable;
    QTableWidget *operandsTable;
    QWidget *verticalLayoutWidget;
    QVBoxLayout *verticalLayout;
    QLabel *dictLabel;
    QLabel *lengthLabel;
    QLabel *volumelabel;
    QPushButton *chooseFileBtn;
    QMenuBar *menubar;
    QStatusBar *statusbar;

    void setupUi(QMainWindow *MainWindow)
    {
        if (MainWindow->objectName().isEmpty())
            MainWindow->setObjectName("MainWindow");
        MainWindow->resize(753, 630);
        centralwidget = new QWidget(MainWindow);
        centralwidget->setObjectName("centralwidget");
        horizontalLayoutWidget = new QWidget(centralwidget);
        horizontalLayoutWidget->setObjectName("horizontalLayoutWidget");
        horizontalLayoutWidget->setGeometry(QRect(50, 0, 651, 381));
        horizontalLayout = new QHBoxLayout(horizontalLayoutWidget);
        horizontalLayout->setObjectName("horizontalLayout");
        horizontalLayout->setContentsMargins(0, 0, 0, 0);
        operatorsTable = new QTableWidget(horizontalLayoutWidget);
        if (operatorsTable->columnCount() < 3)
            operatorsTable->setColumnCount(3);
        operatorsTable->setObjectName("operatorsTable");
        QSizePolicy sizePolicy(QSizePolicy::Policy::Expanding, QSizePolicy::Policy::Ignored);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(0);
        sizePolicy.setHeightForWidth(operatorsTable->sizePolicy().hasHeightForWidth());
        operatorsTable->setSizePolicy(sizePolicy);
        operatorsTable->setSizeAdjustPolicy(QAbstractScrollArea::AdjustToContents);
        operatorsTable->setEditTriggers(QAbstractItemView::NoEditTriggers);
        operatorsTable->setColumnCount(3);

        horizontalLayout->addWidget(operatorsTable);

        operandsTable = new QTableWidget(horizontalLayoutWidget);
        if (operandsTable->columnCount() < 3)
            operandsTable->setColumnCount(3);
        operandsTable->setObjectName("operandsTable");
        QSizePolicy sizePolicy1(QSizePolicy::Policy::Expanding, QSizePolicy::Policy::Expanding);
        sizePolicy1.setHorizontalStretch(0);
        sizePolicy1.setVerticalStretch(0);
        sizePolicy1.setHeightForWidth(operandsTable->sizePolicy().hasHeightForWidth());
        operandsTable->setSizePolicy(sizePolicy1);
        operandsTable->setSizeAdjustPolicy(QAbstractScrollArea::AdjustToContents);
        operandsTable->setEditTriggers(QAbstractItemView::NoEditTriggers);
        operandsTable->setColumnCount(3);

        horizontalLayout->addWidget(operandsTable);

        verticalLayoutWidget = new QWidget(centralwidget);
        verticalLayoutWidget->setObjectName("verticalLayoutWidget");
        verticalLayoutWidget->setGeometry(QRect(50, 390, 581, 121));
        verticalLayout = new QVBoxLayout(verticalLayoutWidget);
        verticalLayout->setObjectName("verticalLayout");
        verticalLayout->setContentsMargins(0, 0, 0, 0);
        dictLabel = new QLabel(verticalLayoutWidget);
        dictLabel->setObjectName("dictLabel");
        QFont font;
        font.setPointSize(14);
        dictLabel->setFont(font);
        dictLabel->setWordWrap(false);

        verticalLayout->addWidget(dictLabel);

        lengthLabel = new QLabel(verticalLayoutWidget);
        lengthLabel->setObjectName("lengthLabel");
        lengthLabel->setFont(font);

        verticalLayout->addWidget(lengthLabel);

        volumelabel = new QLabel(verticalLayoutWidget);
        volumelabel->setObjectName("volumelabel");
        volumelabel->setFont(font);

        verticalLayout->addWidget(volumelabel);

        chooseFileBtn = new QPushButton(centralwidget);
        chooseFileBtn->setObjectName("chooseFileBtn");
        chooseFileBtn->setGeometry(QRect(50, 520, 171, 51));
        chooseFileBtn->setFont(font);
        MainWindow->setCentralWidget(centralwidget);
        menubar = new QMenuBar(MainWindow);
        menubar->setObjectName("menubar");
        menubar->setGeometry(QRect(0, 0, 753, 25));
        MainWindow->setMenuBar(menubar);
        statusbar = new QStatusBar(MainWindow);
        statusbar->setObjectName("statusbar");
        MainWindow->setStatusBar(statusbar);

        retranslateUi(MainWindow);

        QMetaObject::connectSlotsByName(MainWindow);
    } // setupUi

    void retranslateUi(QMainWindow *MainWindow)
    {
        MainWindow->setWindowTitle(QCoreApplication::translate("MainWindow", "MainWindow", nullptr));
        dictLabel->setText(QString());
        lengthLabel->setText(QString());
        volumelabel->setText(QString());
        chooseFileBtn->setText(QCoreApplication::translate("MainWindow", "\320\222\321\213\320\261\320\265\321\200\320\270\321\202\320\265 \321\204\320\260\320\271\320\273", nullptr));
    } // retranslateUi

};

namespace Ui {
    class MainWindow: public Ui_MainWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINWINDOW_H
