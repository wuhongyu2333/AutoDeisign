/********************************************************************************
** Form generated from reading UI file 'mainwindow.ui'
**
** Created by: Qt User Interface Compiler version 5.14.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAINWINDOW_H
#define UI_MAINWINDOW_H

#include <QtCore/QVariant>
#include <QtGui/QIcon>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QCheckBox>
#include <QtWidgets/QComboBox>
#include <QtWidgets/QFrame>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QHBoxLayout>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenu>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QRadioButton>
#include <QtWidgets/QSpacerItem>
#include <QtWidgets/QTextBrowser>
#include <QtWidgets/QVBoxLayout>
#include <QtWidgets/QWidget>
#include "myopenglwidget.h"

QT_BEGIN_NAMESPACE

class Ui_MainWindow
{
public:
    QAction *action1;
    QAction *action2;
    QAction *action3;
    QAction *action4;
    QAction *action5;
    QAction *action6;
    QAction *action7;
    QAction *action8;
    QWidget *centralWidget;
    QGridLayout *gridLayout;
    QHBoxLayout *horizontalLayout_6;
    QVBoxLayout *verticalLayout_3;
    QVBoxLayout *verticalLayout;
    QComboBox *comboBox;
    QHBoxLayout *horizontalLayout;
    QCheckBox *checkBox;
    QHBoxLayout *horizontalLayout_2;
    QCheckBox *checkBox_2;
    QHBoxLayout *horizontalLayout_3;
    QCheckBox *checkBox_4;
    QHBoxLayout *horizontalLayout_4;
    QCheckBox *checkBox_5;
    QHBoxLayout *horizontalLayout_5;
    QCheckBox *checkBox_3;
    QFrame *line;
    QVBoxLayout *verticalLayout_2;
    QRadioButton *radioButton;
    QRadioButton *radioButton_2;
    QRadioButton *radioButton_3;
    QRadioButton *radioButton_4;
    QSpacerItem *verticalSpacer;
    QTextBrowser *textBrowser;
    QPushButton *pushButton;
    MyOpenglWidget *openGLWidget;
    QMenuBar *menuBar;
    QMenu *menuOpen;
    QMenu *menuMode;
    QMenu *menuSolid;

    void setupUi(QMainWindow *MainWindow)
    {
        if (MainWindow->objectName().isEmpty())
            MainWindow->setObjectName(QString::fromUtf8("MainWindow"));
        MainWindow->resize(800, 680);
        MainWindow->setMinimumSize(QSize(0, 0));
        QIcon icon;
        icon.addFile(QString::fromUtf8(":/icons/camera-wireless.svg"), QSize(), QIcon::Normal, QIcon::Off);
        MainWindow->setWindowIcon(icon);
        MainWindow->setAutoFillBackground(true);
        MainWindow->setStyleSheet(QString::fromUtf8("QWidget{\n"
"font: 12pt \"\345\271\274\345\234\206\";\n"
"}"));
        action1 = new QAction(MainWindow);
        action1->setObjectName(QString::fromUtf8("action1"));
        action2 = new QAction(MainWindow);
        action2->setObjectName(QString::fromUtf8("action2"));
        action3 = new QAction(MainWindow);
        action3->setObjectName(QString::fromUtf8("action3"));
        action4 = new QAction(MainWindow);
        action4->setObjectName(QString::fromUtf8("action4"));
        action5 = new QAction(MainWindow);
        action5->setObjectName(QString::fromUtf8("action5"));
        action6 = new QAction(MainWindow);
        action6->setObjectName(QString::fromUtf8("action6"));
        action7 = new QAction(MainWindow);
        action7->setObjectName(QString::fromUtf8("action7"));
        action8 = new QAction(MainWindow);
        action8->setObjectName(QString::fromUtf8("action8"));
        centralWidget = new QWidget(MainWindow);
        centralWidget->setObjectName(QString::fromUtf8("centralWidget"));
        centralWidget->setEnabled(true);
        centralWidget->setLayoutDirection(Qt::LeftToRight);
        gridLayout = new QGridLayout(centralWidget);
        gridLayout->setSpacing(6);
        gridLayout->setContentsMargins(11, 11, 11, 11);
        gridLayout->setObjectName(QString::fromUtf8("gridLayout"));
        horizontalLayout_6 = new QHBoxLayout();
        horizontalLayout_6->setSpacing(6);
        horizontalLayout_6->setObjectName(QString::fromUtf8("horizontalLayout_6"));
        verticalLayout_3 = new QVBoxLayout();
        verticalLayout_3->setSpacing(6);
        verticalLayout_3->setObjectName(QString::fromUtf8("verticalLayout_3"));
        verticalLayout = new QVBoxLayout();
        verticalLayout->setSpacing(6);
        verticalLayout->setObjectName(QString::fromUtf8("verticalLayout"));
        comboBox = new QComboBox(centralWidget);
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->addItem(QString());
        comboBox->setObjectName(QString::fromUtf8("comboBox"));
        comboBox->setMaximumSize(QSize(200, 16777215));

        verticalLayout->addWidget(comboBox);

        horizontalLayout = new QHBoxLayout();
        horizontalLayout->setSpacing(6);
        horizontalLayout->setObjectName(QString::fromUtf8("horizontalLayout"));
        checkBox = new QCheckBox(centralWidget);
        checkBox->setObjectName(QString::fromUtf8("checkBox"));
        checkBox->setStyleSheet(QString::fromUtf8(""));

        horizontalLayout->addWidget(checkBox);


        verticalLayout->addLayout(horizontalLayout);

        horizontalLayout_2 = new QHBoxLayout();
        horizontalLayout_2->setSpacing(6);
        horizontalLayout_2->setObjectName(QString::fromUtf8("horizontalLayout_2"));
        checkBox_2 = new QCheckBox(centralWidget);
        checkBox_2->setObjectName(QString::fromUtf8("checkBox_2"));
        checkBox_2->setStyleSheet(QString::fromUtf8(""));

        horizontalLayout_2->addWidget(checkBox_2);


        verticalLayout->addLayout(horizontalLayout_2);

        horizontalLayout_3 = new QHBoxLayout();
        horizontalLayout_3->setSpacing(6);
        horizontalLayout_3->setObjectName(QString::fromUtf8("horizontalLayout_3"));
        checkBox_4 = new QCheckBox(centralWidget);
        checkBox_4->setObjectName(QString::fromUtf8("checkBox_4"));
        checkBox_4->setStyleSheet(QString::fromUtf8(""));

        horizontalLayout_3->addWidget(checkBox_4);


        verticalLayout->addLayout(horizontalLayout_3);

        horizontalLayout_4 = new QHBoxLayout();
        horizontalLayout_4->setSpacing(6);
        horizontalLayout_4->setObjectName(QString::fromUtf8("horizontalLayout_4"));
        checkBox_5 = new QCheckBox(centralWidget);
        checkBox_5->setObjectName(QString::fromUtf8("checkBox_5"));
        checkBox_5->setStyleSheet(QString::fromUtf8(""));

        horizontalLayout_4->addWidget(checkBox_5);


        verticalLayout->addLayout(horizontalLayout_4);

        horizontalLayout_5 = new QHBoxLayout();
        horizontalLayout_5->setSpacing(6);
        horizontalLayout_5->setObjectName(QString::fromUtf8("horizontalLayout_5"));
        checkBox_3 = new QCheckBox(centralWidget);
        checkBox_3->setObjectName(QString::fromUtf8("checkBox_3"));
        checkBox_3->setStyleSheet(QString::fromUtf8(""));

        horizontalLayout_5->addWidget(checkBox_3);


        verticalLayout->addLayout(horizontalLayout_5);


        verticalLayout_3->addLayout(verticalLayout);

        line = new QFrame(centralWidget);
        line->setObjectName(QString::fromUtf8("line"));
        line->setFrameShape(QFrame::HLine);
        line->setFrameShadow(QFrame::Sunken);

        verticalLayout_3->addWidget(line);

        verticalLayout_2 = new QVBoxLayout();
        verticalLayout_2->setSpacing(6);
        verticalLayout_2->setObjectName(QString::fromUtf8("verticalLayout_2"));
        radioButton = new QRadioButton(centralWidget);
        radioButton->setObjectName(QString::fromUtf8("radioButton"));

        verticalLayout_2->addWidget(radioButton);

        radioButton_2 = new QRadioButton(centralWidget);
        radioButton_2->setObjectName(QString::fromUtf8("radioButton_2"));

        verticalLayout_2->addWidget(radioButton_2);

        radioButton_3 = new QRadioButton(centralWidget);
        radioButton_3->setObjectName(QString::fromUtf8("radioButton_3"));

        verticalLayout_2->addWidget(radioButton_3);

        radioButton_4 = new QRadioButton(centralWidget);
        radioButton_4->setObjectName(QString::fromUtf8("radioButton_4"));

        verticalLayout_2->addWidget(radioButton_4);


        verticalLayout_3->addLayout(verticalLayout_2);

        verticalSpacer = new QSpacerItem(20, 20, QSizePolicy::Minimum, QSizePolicy::Expanding);

        verticalLayout_3->addItem(verticalSpacer);

        textBrowser = new QTextBrowser(centralWidget);
        textBrowser->setObjectName(QString::fromUtf8("textBrowser"));
        QSizePolicy sizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(1);
        sizePolicy.setHeightForWidth(textBrowser->sizePolicy().hasHeightForWidth());
        textBrowser->setSizePolicy(sizePolicy);
        textBrowser->setStyleSheet(QString::fromUtf8(""));

        verticalLayout_3->addWidget(textBrowser);

        pushButton = new QPushButton(centralWidget);
        pushButton->setObjectName(QString::fromUtf8("pushButton"));

        verticalLayout_3->addWidget(pushButton);


        horizontalLayout_6->addLayout(verticalLayout_3);

        openGLWidget = new MyOpenglWidget(centralWidget);
        openGLWidget->setObjectName(QString::fromUtf8("openGLWidget"));
        openGLWidget->setEnabled(true);
        QSizePolicy sizePolicy1(QSizePolicy::Preferred, QSizePolicy::Expanding);
        sizePolicy1.setHorizontalStretch(0);
        sizePolicy1.setVerticalStretch(100);
        sizePolicy1.setHeightForWidth(openGLWidget->sizePolicy().hasHeightForWidth());
        openGLWidget->setSizePolicy(sizePolicy1);
        openGLWidget->setBaseSize(QSize(0, 0));
        openGLWidget->setAutoFillBackground(true);

        horizontalLayout_6->addWidget(openGLWidget);

        horizontalLayout_6->setStretch(0, 1);
        horizontalLayout_6->setStretch(1, 4);

        gridLayout->addLayout(horizontalLayout_6, 0, 0, 1, 1);

        MainWindow->setCentralWidget(centralWidget);
        menuBar = new QMenuBar(MainWindow);
        menuBar->setObjectName(QString::fromUtf8("menuBar"));
        menuBar->setGeometry(QRect(0, 0, 800, 22));
        menuOpen = new QMenu(menuBar);
        menuOpen->setObjectName(QString::fromUtf8("menuOpen"));
        menuMode = new QMenu(menuBar);
        menuMode->setObjectName(QString::fromUtf8("menuMode"));
        menuSolid = new QMenu(menuBar);
        menuSolid->setObjectName(QString::fromUtf8("menuSolid"));
        MainWindow->setMenuBar(menuBar);

        menuBar->addAction(menuOpen->menuAction());
        menuBar->addAction(menuMode->menuAction());
        menuBar->addAction(menuSolid->menuAction());
        menuOpen->addAction(action1);
        menuOpen->addSeparator();
        menuOpen->addAction(action2);
        menuOpen->addSeparator();
        menuOpen->addAction(action3);
        menuMode->addAction(action4);
        menuMode->addSeparator();
        menuMode->addAction(action5);
        menuSolid->addAction(action6);
        menuSolid->addSeparator();
        menuSolid->addAction(action8);
        menuSolid->addSeparator();
        menuSolid->addAction(action7);

        retranslateUi(MainWindow);

        QMetaObject::connectSlotsByName(MainWindow);
    } // setupUi

    void retranslateUi(QMainWindow *MainWindow)
    {
        MainWindow->setWindowTitle(QCoreApplication::translate("MainWindow", "Structure Model Visualization Program", nullptr));
        action1->setText(QCoreApplication::translate("MainWindow", "\346\211\223\345\274\200YJK\346\225\260\346\215\256\346\226\207\344\273\266", nullptr));
        action2->setText(QCoreApplication::translate("MainWindow", "\346\211\223\345\274\200PKPM\346\225\260\346\215\256\346\226\207\344\273\266", nullptr));
        action3->setText(QCoreApplication::translate("MainWindow", "\346\211\223\345\274\200SAP2000\346\226\207\344\273\266", nullptr));
        action4->setText(QCoreApplication::translate("MainWindow", "\345\256\236\344\275\223\346\250\241\345\274\217", nullptr));
        action5->setText(QCoreApplication::translate("MainWindow", "\347\272\277\346\241\206\346\250\241\345\274\217", nullptr));
        action6->setText(QCoreApplication::translate("MainWindow", "\351\207\215\347\275\256", nullptr));
        action7->setText(QCoreApplication::translate("MainWindow", "\346\270\205\347\251\272", nullptr));
        action8->setText(QCoreApplication::translate("MainWindow", "\345\261\205\344\270\255", nullptr));
        comboBox->setItemText(0, QCoreApplication::translate("MainWindow", "ALL", nullptr));
        comboBox->setItemText(1, QCoreApplication::translate("MainWindow", "1", nullptr));
        comboBox->setItemText(2, QCoreApplication::translate("MainWindow", "2", nullptr));
        comboBox->setItemText(3, QCoreApplication::translate("MainWindow", "3", nullptr));
        comboBox->setItemText(4, QCoreApplication::translate("MainWindow", "4", nullptr));
        comboBox->setItemText(5, QCoreApplication::translate("MainWindow", "5", nullptr));
        comboBox->setItemText(6, QCoreApplication::translate("MainWindow", "6", nullptr));
        comboBox->setItemText(7, QCoreApplication::translate("MainWindow", "7", nullptr));
        comboBox->setItemText(8, QCoreApplication::translate("MainWindow", "8", nullptr));
        comboBox->setItemText(9, QCoreApplication::translate("MainWindow", "9", nullptr));
        comboBox->setItemText(10, QCoreApplication::translate("MainWindow", "10", nullptr));
        comboBox->setItemText(11, QCoreApplication::translate("MainWindow", "11", nullptr));
        comboBox->setItemText(12, QCoreApplication::translate("MainWindow", "12", nullptr));
        comboBox->setItemText(13, QCoreApplication::translate("MainWindow", "13", nullptr));
        comboBox->setItemText(14, QCoreApplication::translate("MainWindow", "14", nullptr));
        comboBox->setItemText(15, QCoreApplication::translate("MainWindow", "15", nullptr));

        checkBox->setText(QCoreApplication::translate("MainWindow", "\346\242\201", nullptr));
        checkBox_2->setText(QCoreApplication::translate("MainWindow", "\346\237\261", nullptr));
        checkBox_4->setText(QCoreApplication::translate("MainWindow", "\345\242\231", nullptr));
        checkBox_5->setText(QCoreApplication::translate("MainWindow", "\346\235\277", nullptr));
        checkBox_3->setText(QCoreApplication::translate("MainWindow", "\346\224\257\346\222\221", nullptr));
        radioButton->setText(QCoreApplication::translate("MainWindow", "\345\272\224\345\212\233\346\257\224", nullptr));
        radioButton_2->setText(QCoreApplication::translate("MainWindow", "\344\275\215\347\247\273\346\257\224", nullptr));
        radioButton_3->setText(QCoreApplication::translate("MainWindow", "\346\267\267\345\207\235\345\234\237\345\274\272\345\272\246", nullptr));
        radioButton_4->setText(QCoreApplication::translate("MainWindow", "\346\236\204\344\273\266\346\210\252\351\235\242\347\247\257", nullptr));
        pushButton->setText(QCoreApplication::translate("MainWindow", "\346\270\205\347\251\272\346\226\207\346\234\254\346\241\206", nullptr));
        menuOpen->setTitle(QCoreApplication::translate("MainWindow", "\346\211\223\345\274\200\346\226\207\344\273\266", nullptr));
        menuMode->setTitle(QCoreApplication::translate("MainWindow", "\346\230\276\347\244\272\346\250\241\345\274\217", nullptr));
        menuSolid->setTitle(QCoreApplication::translate("MainWindow", "\350\256\276\347\275\256", nullptr));
    } // retranslateUi

};

namespace Ui {
    class MainWindow: public Ui_MainWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINWINDOW_H
