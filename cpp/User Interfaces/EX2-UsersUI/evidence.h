#ifndef EVIDENCE_H
#define EVIDENCE_H

#include <iostream>
#include <QWidget>
#include <QTableView>
#include <QHBoxLayout>
#include <QStandardItemModel>
#include <QTabWidget>
#include <QTextEdit>
#include <QFrame>
#include <QGroupBox>
#include <QFormLayout>
#include <QHBoxLayout>
#include <QPushButton>
#include <QLineEdit>
#include <QMenu>
#include <QAction>
#include <QMainWindow>
#include <QDialog>
#include <QLabel>

class Evidence : public QMainWindow
{
    Q_OBJECT

public:
    Evidence(QWidget *parent = 0);

    QMenu* filemenu;
    QAction* exit_act;

    QFrame* main_frame;

    QTableView* table;
    QVBoxLayout* layout;
    QStandardItemModel* model;
    QTabWidget *tab;

    QFormLayout* form;
    QLineEdit* name_edit;
    QLineEdit* sname_edit;
    QLineEdit* age_edit;

    QWidget* tab1;
    QGridLayout* tab1_layout;
    QLineEdit* ulice_edit;
    QLineEdit* cp_edit;
    QLineEdit* mesto_edit;
    QLineEdit* psc_edit;

    QFrame* tab2;
    QVBoxLayout* tab2_layout;
    QTextEdit* text;

    QHBoxLayout* but_layout;
    QPushButton* btn_new;
    QPushButton* btn_save;
    QPushButton* btn_del;

    QSlider* slider2;

    QDialog* win;

    ~Evidence();

private:
   void fillTable(QTableView* table);
   void createMenu();

private slots:
   void personSave();
   void personNew();
   void personDelete();
   void personEdit(const QModelIndex& index);
   void newWindow(const QModelIndex& index);
   void sl1changed();
   void sl2changed();
   void closeWin();
};

#endif // EVIDENCE_H
