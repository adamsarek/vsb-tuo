#include "project.h"
#include "ui_project.h"

Project::Project(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::Project)
{
    ui->setupUi(this);

    ui->productsAddCategoryComboBox->addItem("");
    ui->categoriesAddCategoryComboBox->addItem("");
    ui->categoriesRemoveCategoryComboBox->addItem("");
}

Project::~Project()
{
    delete ui;
}


void Project::on_productsAddResetButton_clicked()
{
    ui->productsAddCategoryComboBox->setCurrentIndex(0);
    ui->productsAddNameBox->clear();
    ui->productsAddCodeBox->clear();
    ui->productsAddPriceBox->clear();
    ui->productsAddWeightBox->clear();
    ui->productsAddDateBox->clear();
    ui->productsAddColorBox->clear();
    ui->productsAddDescriptionBox->clear();
    ui->productsAddHideTrueRadio->setChecked(true);
}

void Project::on_productsSearchResetButton_clicked()
{
    ui->productsSearchCodeBox->clear();
}

void Project::on_productsRemoveResetButton_clicked()
{
    ui->productsRemoveCodeBox->clear();
}

void Project::on_categoriesAddResetButton_clicked()
{
    ui->categoriesAddCategoryComboBox->setCurrentIndex(0);
    ui->categoriesAddNameBox->clear();
    ui->categoriesAddDescriptionBox->clear();
    ui->categoriesAddHideTrueRadio->setChecked(true);
}

void Project::on_ordersAddResetButton_clicked()
{
    ui->ordersAddProductComboBox->setCurrentIndex(0);
    ui->ordersAddQuantityBox->clear();
    ui->ordersAddFirstNameBox->clear();
    ui->ordersAddLastNameBox->clear();
    ui->ordersAddAddressBox->clear();
    ui->ordersAddPhoneBox->clear();
}

void Project::on_ordersSearchResetButton_clicked()
{
    ui->ordersSearchIDBox->clear();
}

void Project::on_ordersRemoveResetButton_clicked()
{
    ui->ordersRemoveIDBox->clear();
}

void Project::on_categoriesAddAddButton_clicked()
{
    Category *category = new Category();
    category->categoryID = ui->categoriesAddCategoryComboBox->currentIndex() - 1;
    category->name = ui->categoriesAddNameBox->text();
    category->description = ui->categoriesAddDescriptionBox->toPlainText();
    category->hide = ui->categoriesAddHideTrueRadio->isChecked();

    // Add to vector
    categories->append(category);

    // Add to comboBoxes
    ui->productsAddCategoryComboBox->addItem(category->name);
    ui->categoriesAddCategoryComboBox->addItem(category->name);
    ui->categoriesRemoveCategoryComboBox->addItem(category->name);
}

void Project::on_categoriesRemoveRemoveButton_clicked()
{
    int categoryID = ui->categoriesRemoveCategoryComboBox->currentIndex() - 1;
    if(categoryID >= 0){
        categories->removeAt(categoryID);
        ui->productsAddCategoryComboBox->removeItem(categoryID + 1);
        ui->categoriesAddCategoryComboBox->removeItem(categoryID + 1);
        ui->categoriesRemoveCategoryComboBox->removeItem(categoryID + 1);
    }
    ui->productsAddCategoryComboBox->setCurrentIndex(0);
    ui->categoriesAddCategoryComboBox->setCurrentIndex(0);
    ui->categoriesRemoveCategoryComboBox->setCurrentIndex(0);
}
