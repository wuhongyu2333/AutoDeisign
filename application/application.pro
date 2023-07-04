#-------------------------------------------------
#
# Project created by QtCreator 2023-05-24T14:06:15
#
#-------------------------------------------------

QT       += opengl
QT       += core gui
QT += sql

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = application
TEMPLATE = app


LIBS += -lopengl32
-lglu32\

# The following define makes your compiler emit warnings if you use
# any feature of Qt which as been marked as deprecated (the exact warnings
# depend on your compiler). Please consult the documentation of the
# deprecated API in order to know how to port your code away from it.
DEFINES += QT_DEPRECATED_WARNINGS

# You can also make your code fail to compile if you use deprecated APIs.
# In order to do so, uncomment the following line.
# You can also select to disable deprecated APIs only up to a certain version of Qt.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
        main.cpp \
        mainwindow.cpp \
    myopenglwidget.cpp \
    member.cpp

HEADERS += \
        mainwindow.h \
    myopenglwidget.h \
    member.h

FORMS += \
        mainwindow.ui

RESOURCES += \
    qdarkstyle/dark/darkstyle.qrc \
    qdarkstyle/light/lightstyle.qrc \
    styles/styles.qrc \
    icon.qrc
