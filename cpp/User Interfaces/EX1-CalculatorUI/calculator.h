#ifndef CONVERTER_H
#define CONVERTER_H

#include <QWidget>

class QLabel;
class QLineEdit;
class QSpinBox;
class QRadioButton;
class QPushButton;
class QGridLayout;
class QVBoxLayout;
class QHBoxLayout;
class QGroupBox;



class Calculator : public QWidget
{
    Q_OBJECT

    QGridLayout *layout;
    QVBoxLayout *leftPanel, *rightPanel, *decimalTextPanel, *decimalBoxPanel;
    QHBoxLayout *hbox;
    QLineEdit  *inputBox, *outputBox;
    QGroupBox *radioButtons;
    QRadioButton *cfRBut, *fcRBut;
    QSpinBox *decimalBox;
    QLabel *inputText, *outputText, *decimalText;
    QPushButton *convertBut;

    bool CtoF = true;
    short decimalPlaces;

public:
    Calculator(QWidget *parent = 0);
    ~Calculator();

private slots:
    void clickCfButton();
    void clickFcButton();
    void clickConvertButton();

};

#endif // CONVERTER_H
