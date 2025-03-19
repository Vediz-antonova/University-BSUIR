#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    ui->operatorsTable->horizontalHeader()->setStretchLastSection(true);
    ui->operandsTable->horizontalHeader()->setStretchLastSection(true);
    ui->operatorsTable->verticalHeader()->setVisible(false);
    ui->operandsTable->verticalHeader()->setVisible(false);

    ui->operatorsTable->setHorizontalHeaderLabels(QStringList() << "j"
                                                                << "Оператор"
                                                                << "f1j");
    ui->operandsTable->setHorizontalHeaderLabels(QStringList() << "i"
                                                               << "Операнд"
                                                               << "f2i");
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::ParseAndDisplay()
{
    QVector<QPair<QString, int>> operators = parser->getOperatorsCount();
    QVector<QPair<QString, int>> operands = parser->getOperandsCount();
    int n1, n2, N1 = 0, N2 = 0, n, N, V;

    for (const auto &i : operators)
    {
        if (i.second == 0) {
            continue;
        }

        QTableWidgetItem *item0 = new QTableWidgetItem(QString::number(numOfOperatorsRows + 1));
        QTableWidgetItem *item1 = new QTableWidgetItem(i.first);
        QTableWidgetItem *item2 = new QTableWidgetItem(QString::number(i.second));

        ui->operatorsTable->insertRow(numOfOperatorsRows);
        ui->operatorsTable->setItem(numOfOperatorsRows, 0, item0);
        ui->operatorsTable->setItem(numOfOperatorsRows, 1, item1);
        ui->operatorsTable->setItem(numOfOperatorsRows, 2, item2);
        N1 += i.second;
        numOfOperatorsRows++;
    }
    for (const auto &i : operands)
    {
        if (i.second == 0) {
            continue;
        }

        QTableWidgetItem *item1 = new QTableWidgetItem(i.first);
        QTableWidgetItem *item2 = new QTableWidgetItem(QString::number(i.second));
        QTableWidgetItem *item0 = new QTableWidgetItem(QString::number(numOfOperandsRows + 1));
        ui->operandsTable->insertRow(numOfOperandsRows);
        ui->operandsTable->setItem(numOfOperandsRows, 0, item0);
        ui->operandsTable->setItem(numOfOperandsRows, 1, item1);
        ui->operandsTable->setItem(numOfOperandsRows, 2, item2);
        N2 += i.second;
        numOfOperandsRows++;
    }

    n1 = numOfOperatorsRows;
    n2 = numOfOperandsRows;
    n = n1 + n2;
    N = N1 + N2;
    V = N * log2(n);

    QTableWidgetItem *item_n1 = new QTableWidgetItem(QString("n1 = ") + QString::number(n1));
    QTableWidgetItem *item_n2 = new QTableWidgetItem(QString("n2 = ") + QString::number(n2));
    QTableWidgetItem *item_N1 = new QTableWidgetItem(QString("N1 = ") + QString::number(N1));
    QTableWidgetItem *item_N2 = new QTableWidgetItem(QString("N2 = ") + QString::number(N2));

    ui->operatorsTable->insertRow(numOfOperatorsRows);
    ui->operatorsTable->setItem(numOfOperatorsRows, 0, item_n1);
    ui->operatorsTable->setItem(numOfOperatorsRows, 2, item_N1);

    ui->operandsTable->insertRow(numOfOperandsRows);
    ui->operandsTable->setItem(numOfOperandsRows, 0, item_n2);
    ui->operandsTable->setItem(numOfOperandsRows, 2, item_N2);

    ui->operandsTable->resizeColumnsToContents();
    ui->operatorsTable->resizeColumnsToContents();
    ui->operatorsTable->horizontalHeader()->setStretchLastSection(true);
    ui->operandsTable->horizontalHeader()->setStretchLastSection(true);

    ui->dictLabel->setText(QString("Словарь программы n = " + QString::number(n1) + " + " + QString::number(n2) + " = " + QString::number(n)));
    ui->lengthLabel->setText(QString("Длина программы N = " + QString::number(N1) + " + " + QString::number(N2) + " = " + QString::number(N)));
    ui->volumelabel->setText(QString("Объем программы V = " + QString::number(N) + " * log₂" + QString::number(n) + " = " + QString::number(V)));
}

void MainWindow::Reset()
{
    numOfOperatorsRows = 0;
    numOfOperandsRows = 0;
    ui->operatorsTable->clear();
    ui->operandsTable->clear();
    ui->operatorsTable->setHorizontalHeaderLabels(QStringList() << "j"
                                                                << "Оператор"
                                                                << "f1j");
    ui->operandsTable->setHorizontalHeaderLabels(QStringList() << "i"
                                                               << "Операнд"
                                                               << "f2i");

    ui->dictLabel->clear();
    ui->lengthLabel->clear();
    ui->volumelabel->clear();
}

void MainWindow::on_chooseFileBtn_clicked()
{
    Reset();
    QString filePath = QFileDialog::getOpenFileName(nullptr, "Выберите .go файл", QDir::homePath());

    if (filePath.isEmpty())
        return;

    QFile file(filePath);
    if (!file.open(QIODevice::ReadOnly | QIODevice::Text)) {
        QMessageBox::critical(this, "Error", "Не удалось открыть файл");
        return;
    }

    QTextStream in(&file);
    QString fileContent = in.readAll();

    parser = new MetricsCalculator(fileContent);

    file.close();
    ParseAndDisplay();
}
