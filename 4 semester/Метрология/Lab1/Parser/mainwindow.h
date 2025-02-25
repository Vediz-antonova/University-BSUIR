#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QWidget>
#include <QFileDialog>
#include <QMessageBox>
#include "metricscalculator.h"

QT_BEGIN_NAMESPACE
namespace Ui {
class MainWindow;
}
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();
    void ParseAndDisplay();
    void Reset();

private slots:
    void on_chooseFileBtn_clicked();

private:
    MetricsCalculator* parser;
    int numOfOperatorsRows = 0;
    int numOfOperandsRows = 0;
    Ui::MainWindow *ui;
};
#endif // MAINWINDOW_H
