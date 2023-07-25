#include <QLabel>
#include <QLineEdit>
#include <QTableView>
#include <QHBoxLayout>
#include <QVBoxLayout>
#include <QString>
#include <QButtonGroup>
#include <QRadioButton>
#include <QSpinBox>
#include <QGroupBox>
#include <QPushButton>
#include <QMessageBox>

#include "calculator.h"

#define MIN_BUT_WIDTH 30
#define MIN_BUT_HEIGHT 30
#define MAX_BUT_WIDTH 50
#define MAX_BUT_HEIGHT 50
#define NORMAL_FONT_SIZE 8
#define BIG_FONT_SIZE 18

Calculator::Calculator(QWidget *parent)
    : QWidget(parent)
{

    // vytvoreni hlavniho Grid Layoutu - rozmisteni komponent v okne
    layout = new QGridLayout;

    //panel pro radiobuttony
    hbox = new QHBoxLayout();
    radioButtons = new QGroupBox("Convertor direction",this);
    cfRBut = new QRadioButton(QString("C -> F"),radioButtons);
    cfRBut->setChecked(true);
    fcRBut = new QRadioButton(QString("F -> C"),radioButtons);
    hbox->addWidget(cfRBut);
    hbox->addWidget(fcRBut);
    radioButtons->setLayout(hbox);
    layout->addWidget(radioButtons, 0, 0, 1, 4);
    connect(cfRBut,SIGNAL(clicked()),this,SLOT(clickCfButton()));
    connect(fcRBut,SIGNAL(clicked()),this,SLOT(clickFcButton()));

    // Left panel for input
    leftPanel = new QVBoxLayout;
    inputText = new QLabel(this);
    inputText->setText("Input");
    leftPanel->addWidget(inputText);
    inputBox = new QLineEdit("0",this);
    inputBox->setReadOnly(false);
    inputBox->setAlignment( Qt::AlignRight);
    leftPanel->addWidget(inputBox);
    layout->addLayout(leftPanel, 1, 0, 1, 2);

    // Right panel for output
    rightPanel = new QVBoxLayout;
    outputText = new QLabel(this);
    outputText->setText("Output");
    rightPanel->addWidget(outputText);
    outputBox = new QLineEdit("",this);
    outputBox->setReadOnly(true);
    outputBox->setAlignment( Qt::AlignRight);
    rightPanel->addWidget(outputBox);
    layout->addLayout(rightPanel, 1, 2, 1, 2);

    // Panel for decimal settings
    decimalTextPanel = new QVBoxLayout;
    decimalText = new QLabel(this);
    decimalText->setText("Decimal places");
    decimalTextPanel->addWidget(decimalText);
    layout->addLayout(decimalTextPanel, 2, 0, 1, 3);
    decimalBoxPanel = new QVBoxLayout;
    decimalBox = new QSpinBox(this);
    decimalBox->setMinimum(0);
    decimalBox->setMaximum(3);
    decimalBox->setValue(1);
    decimalBoxPanel->addWidget(decimalBox);
    layout->addLayout(decimalBoxPanel, 2, 3, 1, 1);
    connect(decimalBox,SIGNAL(changed()),this,SLOT(changeDecimalBox()));

    // Convert
    convertBut = new QPushButton("Convert",this);
    convertBut->setMinimumSize(MIN_BUT_WIDTH, MIN_BUT_HEIGHT);
    convertBut->setFont(QFont("SanSerif", NORMAL_FONT_SIZE, QFont::Normal));
    connect(convertBut,SIGNAL(clicked()),this,SLOT(clickConvertButton()));
    layout->addWidget(convertBut, 3, 0, 1, 4);

    setLayout( layout );
}

Calculator::~Calculator()
{

}

void Calculator::clickCfButton(){
    CtoF = true;
}

void Calculator::clickFcButton(){
    CtoF = false;
}

void Calculator::clickConvertButton(){
    double input = inputBox->text().toDouble();
    double output;
    if(CtoF){
        output = (9.0 / 5.0) * input + 32;
    }
    else{
        output = (5.0 / 9.0) * (input - 32);
    }
    outputBox->setText(QString::number(output,'f',decimalBox->text().toShort()));
}
