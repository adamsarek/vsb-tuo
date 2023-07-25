#include "evidence.h"
#include <QLabel>
#include <QLineEdit>
#include <QHeaderView>
#include <QMenuBar>
#include <QWidget>
#include <QScrollArea>

Evidence::Evidence(QWidget *parent)
    : QMainWindow(parent)
{
    this->setMinimumWidth(500); // nastaveni minimalni sirky okna
    createMenu(); // zavolame funkci na pridani menu

    layout = new QVBoxLayout();

    table = new QTableView(); // tabulka
    fillTable(table); // naplnime ji daty

    // spojime SIGNAL kliknuti do tabulky se SLOTem personEdit - aktualne vytiskne krestni jmeno
    connect(table, SIGNAL(clicked(const QModelIndex &)), this, SLOT(personEdit(const QModelIndex &)));
    connect(table, SIGNAL(doubleClicked(const QModelIndex &)), this, SLOT(newWindow(const QModelIndex &)));

    layout->addWidget(table);

    //TODO QFormLayout na Jmeno, Prijmeni, Vek
    form = new QFormLayout();
    name_edit = new QLineEdit();
    name_edit->setFixedWidth(100);
    sname_edit = new QLineEdit();
    sname_edit->setFixedWidth(100);
    age_edit = new QLineEdit();
    age_edit->setFixedWidth(100);

    form->setFormAlignment(Qt::AlignHCenter);
    form->setLabelAlignment(Qt::AlignRight);
    form->addRow("Jméno:", name_edit);
    form->addRow("Příjmení:", sname_edit);
    form->addRow("Věk:", age_edit);

    layout->addLayout(form);

    tab = new QTabWidget(); // widget na zalozky
    tab->setSizePolicy(QSizePolicy::Expanding, QSizePolicy::Fixed);

    // tab1 na adresu
    tab1 = new QFrame();

    // nastavime tabu1 layout typu grid layout
    tab1_layout = new QGridLayout();
    ulice_edit = new QLineEdit();
    ulice_edit->setFixedWidth(100);
    cp_edit = new QLineEdit();
    cp_edit->setFixedWidth(50);
    mesto_edit = new QLineEdit();
    mesto_edit->setFixedWidth(100);
    psc_edit = new QLineEdit();
    psc_edit->setFixedWidth(50);

    tab1_layout->addWidget(new QLabel(QString("Ulice:")), 0, 0);
    tab1_layout->addWidget(new QLabel(QString("Město:")), 1, 0);
    tab1_layout->addWidget(new QLabel(QString("PSČ:")), 2, 0);
    tab1_layout->addWidget(new QLabel(QString("č.p.:")), 0, 2);
    tab1_layout->addWidget(ulice_edit, 0, 1);
    tab1_layout->addWidget(mesto_edit, 1, 1);
    tab1_layout->addWidget(psc_edit, 2, 1);
    tab1_layout->addWidget(cp_edit, 0, 3);
    tab1_layout->setSizeConstraint(QLayout::SetFixedSize);
    tab1->setLayout(tab1_layout);

    tab2 = new QFrame();
    tab2_layout = new QVBoxLayout();
    text = new QTextEdit();
    text->setFixedHeight(80);
    tab2_layout->addWidget(text);
    tab2->setLayout(tab2_layout);

    // pridame tab1 do zalozek
    tab->addTab(tab1, QString("Adresa"));
    tab->addTab(tab2, QString("Poznámka"));

    layout->addWidget(tab);

    // Tlacitka: Novy, Ulozit, Smazat
    but_layout = new QHBoxLayout();
    btn_new = new QPushButton("Nový");
    btn_save = new QPushButton("Uložit");
    btn_del = new QPushButton("Smazat");

    but_layout->addWidget(btn_new);
    but_layout->addWidget(btn_save);
    but_layout->addWidget(btn_del);

    layout->addLayout(but_layout);

    connect(btn_save, SIGNAL(clicked(bool)), this, SLOT(personSave()));
    connect(btn_new, SIGNAL(clicked(bool)), this, SLOT(personNew()));
    connect(btn_del, SIGNAL(clicked(bool)), this, SLOT(personDelete()));


    main_frame = new QFrame();
    main_frame->setLayout(layout);
    setCentralWidget(main_frame);
}

void Evidence::createMenu()
{
    filemenu = new QMenu("&Soubor");
    exit_act = new QAction("&Konec", this);
    filemenu->addAction(exit_act);

    connect(exit_act, SIGNAL(triggered()), this, SLOT(close()));

    menuBar()->addMenu(filemenu);
}

void Evidence::fillTable(QTableView* table)
{
    int nrow = 5;
    int ncol = 9;

    const char* cols[] = { "Jméno", "Příjmení", "Věk", "Adresa", "Ulice", "Město", "PSČ", "č.p.", "poznámka" };
    const char* names[] = { "Jan", "Pavel", "Tomas", "Lucie", "Renata" };
    const char* snames[] = { "Dub", "Buk", "Briza", "Smrkova", "Lipova" };
    const char* ages[] = { "33", "17", "28", "44", "39" };
    const char* addresses[] = { "Dlouha 20, Ostrava,Česká republika", "Kratka 13, Ostrava,Česká republika", "Strma 3, Ostrava,Česká republika", "Siroka 5, Ostrava,Česká republika", "U statku 2, Ostrava,Česká republika" };
    const char* streets[] = { "Dlouha 20", "Kratka 13", "Strma 3", "Siroka 5", "U statku 2" };
    const char* cities[] = { "Ostrava", "Ostrava", "Ostrava", "Ostrava", "Ostrava" };
    const char* PSCs[] = { "12345", "23456", "34567", "45678", "56789" };
    const char* CPs[] = { "20", "13", "3", "5", "2" };
    const char* descs[] = { "dfg", "afdg", "sadfs", "dfdsf", "dfgfg" };

    // vytvorime model, ktery naplnime daty a pote tento model priradime tabulce
    model = new QStandardItemModel( nrow, ncol, this );

    for (int r=0; r<ncol; r++) model->setHorizontalHeaderItem( r, new QStandardItem( QString(cols[r] )) ); // header tabulky

    for( int r=0; r<nrow; r++ )
    {
        QStandardItem *item_name = new QStandardItem(QString(names[r]));
        QStandardItem *item_sname = new QStandardItem(QString(snames[r]));
        QStandardItem *item_ages = new QStandardItem(QString(ages[r]));
        QStandardItem *item_addr = new QStandardItem(QString(addresses[r]));
        QStandardItem *item_street = new QStandardItem(QString(streets[r]));
        QStandardItem *item_city = new QStandardItem(QString(cities[r]));
        QStandardItem *item_psc = new QStandardItem(QString(PSCs[r]));
        QStandardItem *item_cp = new QStandardItem(QString(CPs[r]));
        QStandardItem *item_desc = new QStandardItem(QString(descs[r]));

        model->setItem(r, 0, item_name);
        model->setItem(r, 1, item_sname);
        model->setItem(r, 2, item_ages);
        model->setItem(r, 3, item_addr);
        model->setItem(r, 4, item_street);
        model->setItem(r, 5, item_city);
        model->setItem(r, 6, item_psc);
        model->setItem(r, 7, item_cp);
        model->setItem(r, 8, item_desc);
    }
    table->setModel(model);

    /* jelikoz jsme vytvorili model s 9 sloupci (vcetne adresy, mesta, atd.), ale nechceme v tabulce zobrazit vsechny udaje, nastavime tabulce, ze vybrane sloupce nema zobrazovat
     v nasem pripade od sloupce s indexem 4 (Ulice) az do konce */
    for(int i = 4; i < ncol; i++)
    {
        table->setColumnHidden(i,true);
    }

    // nastaveni chovani tabulky
    table->setSizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);
    table->horizontalHeader()->setSectionResizeMode(QHeaderView::Stretch);
    table->horizontalHeader()->setSectionResizeMode(2, QHeaderView::ResizeToContents);

}

Evidence::~Evidence()
{

}

/* ulozime novou osobu - hodnoty jsou zde nastaveny natvrdo */
void Evidence::personSave()
{
    /* ziskame pocet radku, pridame data a nastavime pocet radku o 1 vetsi */
    int currentrow = table->currentIndex().row();

    QStandardItem *item_name = new QStandardItem(name_edit->text());
    QStandardItem *item_sname = new QStandardItem(sname_edit->text());
    QStandardItem *item_ages = new QStandardItem(age_edit->text());
    QStandardItem *item_addr = new QStandardItem(ulice_edit->text() + ", " + mesto_edit->text() + "Česká Republika");
    QStandardItem *item_ulice = new QStandardItem(ulice_edit->text());
    QStandardItem *item_mesto = new QStandardItem(mesto_edit->text());
    QStandardItem *item_psc = new QStandardItem(psc_edit->text());
    QStandardItem *item_cp = new QStandardItem(cp_edit->text());
    QStandardItem *item_text = new QStandardItem(text->toPlainText());

    model->setItem(currentrow, 0, item_name);
    model->setItem(currentrow, 1, item_sname);
    model->setItem(currentrow, 2, item_ages);
    model->setItem(currentrow, 3, item_addr);
    model->setItem(currentrow, 4, item_ulice);
    model->setItem(currentrow, 5, item_mesto);
    model->setItem(currentrow, 6, item_psc);
    model->setItem(currentrow, 7, item_cp);
    model->setItem(currentrow, 8, item_text);

    //int r = model->rowCount();
    //model->setRowCount(r+1);
}

void Evidence::personNew()
{
    int r = model->rowCount();

    QStandardItem *item_name = new QStandardItem(name_edit->text());
    QStandardItem *item_sname = new QStandardItem(sname_edit->text());
    QStandardItem *item_ages = new QStandardItem(age_edit->text());
    QStandardItem *item_addr = new QStandardItem(ulice_edit->text() + ", " + mesto_edit->text() + "Česká Republika");
    QStandardItem *item_ulice = new QStandardItem(ulice_edit->text());
    QStandardItem *item_mesto = new QStandardItem(mesto_edit->text());
    QStandardItem *item_psc = new QStandardItem(psc_edit->text());
    QStandardItem *item_cp = new QStandardItem(cp_edit->text());
    QStandardItem *item_text = new QStandardItem(text->toPlainText());

    model->setItem(r, 0, item_name);
    model->setItem(r, 1, item_sname);
    model->setItem(r, 2, item_ages);
    model->setItem(r, 3, item_addr);
    model->setItem(r, 4, item_ulice);
    model->setItem(r, 5, item_mesto);
    model->setItem(r, 6, item_psc);
    model->setItem(r, 7, item_cp);
    model->setItem(r, 8, item_text);

    model->setRowCount(r+1);
}

void Evidence::personDelete()
{
    int currentrow = table->currentIndex().row();
    int r = model->rowCount();

    model->removeRow(currentrow);

    model->setRowCount(r-1);
}

void Evidence::personEdit(const QModelIndex& index)
{
    int r = index.row();
    name_edit->setText(model->item(r, 0)->text());
    sname_edit->setText(model->item(r,1)->text());
    age_edit->setText(model->item(r,2)->text());
    ulice_edit->setText(model->item(r, 4)->text());
    mesto_edit->setText(model->item(r, 5)->text());
    psc_edit->setText(model->item(r, 6)->text());
    cp_edit->setText(model->item(r, 7)->text());
    text->setText(model->item(r, 8)->text());
}

void Evidence::newWindow(const QModelIndex& index)
{
    int r = index.row();
    win = new QDialog();
    QVBoxLayout* winLayout = new QVBoxLayout();
    win->setWindowTitle("Detail");

    QGroupBox* groupBox = new QGroupBox(tr(""));
    winLayout->addWidget(groupBox);

    QPixmap* img1 = new QPixmap( QString::fromUtf8("../Evidence/profile.png") );
    QPixmap* img2 = new QPixmap( QString::fromUtf8("../Evidence/map.png") );
    QLabel* imgLabel1 = new QLabel();
    QLabel* imgLabel2 = new QLabel();
    imgLabel2->setFixedSize(400,300);
    imgLabel1->setPixmap(*img1);
    imgLabel2->setPixmap(*img2);

    QHBoxLayout* hbox = new QHBoxLayout();
    groupBox->setLayout(hbox);

    hbox->addWidget(imgLabel1);

    QFormLayout* newform = new QFormLayout();
    QLabel* lbl1 = new QLabel();
    lbl1->setText(model->item(r,0)->text());
    QLabel* lbl2 = new QLabel();
    lbl2->setText(model->item(r,1)->text());
    QLabel* lbl4 = new QLabel();
    lbl4->setText(model->item(r,2)->text());
    QLabel* lbl3 = new QLabel();
    lbl3->setText(model->item(r,4)->text() + ", " + model->item(r,5)->text() + ", Česká Republika");
    newform->setFormAlignment(Qt::AlignHCenter);
    newform->setFormAlignment(Qt::AlignVCenter);
    newform->setLabelAlignment(Qt::AlignRight);
    newform->addRow("Jméno:", lbl1);
    newform->addRow("Příjmení:", lbl2);
    newform->addRow("Věk:", lbl4);
    newform->addRow("Adresa:", lbl3);

    hbox->addLayout(newform);

    QGridLayout* grid = new QGridLayout();
    slider2 = new QSlider(Qt::Horizontal);

    connect(slider2, &QSlider::valueChanged, this, &Evidence::sl2changed);

    grid->addWidget(imgLabel2, 0, 0);
    grid->addWidget(slider2, 1, 0);

    winLayout->addLayout(grid);

    QLabel* lbl = new QLabel("Zoom");
    lbl->setAlignment(Qt::AlignHCenter);
    winLayout->addWidget(lbl);

    QVBoxLayout* vbox = new QVBoxLayout();
    QPushButton* close = new QPushButton("Zavřít");
    close->setFixedWidth(80);
    vbox->setAlignment(Qt::AlignHCenter);
    vbox->setMargin(15);
    vbox->addWidget(close);

    connect(close, SIGNAL(clicked()), this, SLOT(closeWin()));

    winLayout->addLayout(vbox);

    win->setLayout(winLayout);
    win->exec(); // modální okno
    //win->show(); // nemodální okno

}

void Evidence::closeWin()
{
    win->close();
}

void Evidence::sl1changed()
{
}

void Evidence::sl2changed()
{
    std::cout << "slider value: " << slider2->value() << std::endl;

}

