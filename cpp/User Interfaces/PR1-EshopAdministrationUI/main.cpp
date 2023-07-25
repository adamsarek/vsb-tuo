#include "project.h"

#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    Project w;
    w.show();
    return a.exec();
}
