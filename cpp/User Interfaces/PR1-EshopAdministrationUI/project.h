#ifndef PROJECT_H
#define PROJECT_H

#include <QMainWindow>

QT_BEGIN_NAMESPACE
namespace Ui { class Project; }
QT_END_NAMESPACE

class Project : public QMainWindow
{
    Q_OBJECT

public:
    Project(QWidget *parent = nullptr);
    ~Project();

private slots:
    void on_productsAddResetButton_clicked();

    void on_productsSearchResetButton_clicked();

    void on_productsRemoveResetButton_clicked();

    void on_categoriesAddResetButton_clicked();

    void on_ordersAddResetButton_clicked();

    void on_ordersSearchResetButton_clicked();

    void on_ordersRemoveResetButton_clicked();

    void on_categoriesAddAddButton_clicked();

    void on_categoriesRemoveRemoveButton_clicked();

private:
    Ui::Project *ui;

    struct Category{
        int categoryID = -1;
        QString name = "";
        QString description = "";
        bool hide = true;
    };

    QVector<Category*> *categories = new QVector<Category*>();
};
#endif // PROJECT_H
