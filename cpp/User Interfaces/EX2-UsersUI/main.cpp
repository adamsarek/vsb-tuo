#include "evidence.h"

#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    Evidence w;
    w.show();
    return a.exec();
}
