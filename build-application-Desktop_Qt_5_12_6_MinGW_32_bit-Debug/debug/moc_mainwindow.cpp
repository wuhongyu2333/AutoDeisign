/****************************************************************************
** Meta object code from reading C++ file 'mainwindow.h'
**
** Created by: The Qt Meta Object Compiler version 67 (Qt 5.12.6)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../application/mainwindow.h"
#include <QtCore/qbytearray.h>
#include <QtCore/qmetatype.h>
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'mainwindow.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 67
#error "This file was generated using the moc from 5.12.6. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
QT_WARNING_PUSH
QT_WARNING_DISABLE_DEPRECATED
struct qt_meta_stringdata_MainWindow_t {
    QByteArrayData data[19];
    char stringdata0[398];
};
#define QT_MOC_LITERAL(idx, ofs, len) \
    Q_STATIC_BYTE_ARRAY_DATA_HEADER_INITIALIZER_WITH_OFFSET(len, \
    qptrdiff(offsetof(qt_meta_stringdata_MainWindow_t, stringdata0) + ofs \
        - idx * sizeof(QByteArrayData)) \
    )
static const qt_meta_stringdata_MainWindow_t qt_meta_stringdata_MainWindow = {
    {
QT_MOC_LITERAL(0, 0, 10), // "MainWindow"
QT_MOC_LITERAL(1, 11, 21), // "on_pushButton_clicked"
QT_MOC_LITERAL(2, 33, 0), // ""
QT_MOC_LITERAL(3, 34, 19), // "on_menuOpen_pressed"
QT_MOC_LITERAL(4, 54, 20), // "on_action1_triggered"
QT_MOC_LITERAL(5, 75, 20), // "on_action2_triggered"
QT_MOC_LITERAL(6, 96, 20), // "on_action3_triggered"
QT_MOC_LITERAL(7, 117, 20), // "on_action6_triggered"
QT_MOC_LITERAL(8, 138, 20), // "on_action7_triggered"
QT_MOC_LITERAL(9, 159, 20), // "on_action8_triggered"
QT_MOC_LITERAL(10, 180, 24), // "on_checkBox_stateChanged"
QT_MOC_LITERAL(11, 205, 4), // "arg1"
QT_MOC_LITERAL(12, 210, 26), // "on_checkBox_2_stateChanged"
QT_MOC_LITERAL(13, 237, 26), // "on_checkBox_4_stateChanged"
QT_MOC_LITERAL(14, 264, 26), // "on_checkBox_5_stateChanged"
QT_MOC_LITERAL(15, 291, 26), // "on_checkBox_3_stateChanged"
QT_MOC_LITERAL(16, 318, 31), // "on_comboBox_currentIndexChanged"
QT_MOC_LITERAL(17, 350, 23), // "on_pushButton_3_clicked"
QT_MOC_LITERAL(18, 374, 23) // "on_pushButton_2_clicked"

    },
    "MainWindow\0on_pushButton_clicked\0\0"
    "on_menuOpen_pressed\0on_action1_triggered\0"
    "on_action2_triggered\0on_action3_triggered\0"
    "on_action6_triggered\0on_action7_triggered\0"
    "on_action8_triggered\0on_checkBox_stateChanged\0"
    "arg1\0on_checkBox_2_stateChanged\0"
    "on_checkBox_4_stateChanged\0"
    "on_checkBox_5_stateChanged\0"
    "on_checkBox_3_stateChanged\0"
    "on_comboBox_currentIndexChanged\0"
    "on_pushButton_3_clicked\0on_pushButton_2_clicked"
};
#undef QT_MOC_LITERAL

static const uint qt_meta_data_MainWindow[] = {

 // content:
       8,       // revision
       0,       // classname
       0,    0, // classinfo
      16,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: name, argc, parameters, tag, flags
       1,    0,   94,    2, 0x08 /* Private */,
       3,    0,   95,    2, 0x08 /* Private */,
       4,    0,   96,    2, 0x08 /* Private */,
       5,    0,   97,    2, 0x08 /* Private */,
       6,    0,   98,    2, 0x08 /* Private */,
       7,    0,   99,    2, 0x08 /* Private */,
       8,    0,  100,    2, 0x08 /* Private */,
       9,    0,  101,    2, 0x08 /* Private */,
      10,    1,  102,    2, 0x08 /* Private */,
      12,    1,  105,    2, 0x08 /* Private */,
      13,    1,  108,    2, 0x08 /* Private */,
      14,    1,  111,    2, 0x08 /* Private */,
      15,    1,  114,    2, 0x08 /* Private */,
      16,    1,  117,    2, 0x08 /* Private */,
      17,    0,  120,    2, 0x08 /* Private */,
      18,    0,  121,    2, 0x08 /* Private */,

 // slots: parameters
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void, QMetaType::Int,   11,
    QMetaType::Void, QMetaType::Int,   11,
    QMetaType::Void, QMetaType::Int,   11,
    QMetaType::Void, QMetaType::Int,   11,
    QMetaType::Void, QMetaType::Int,   11,
    QMetaType::Void, QMetaType::QString,   11,
    QMetaType::Void,
    QMetaType::Void,

       0        // eod
};

void MainWindow::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        auto *_t = static_cast<MainWindow *>(_o);
        Q_UNUSED(_t)
        switch (_id) {
        case 0: _t->on_pushButton_clicked(); break;
        case 1: _t->on_menuOpen_pressed(); break;
        case 2: _t->on_action1_triggered(); break;
        case 3: _t->on_action2_triggered(); break;
        case 4: _t->on_action3_triggered(); break;
        case 5: _t->on_action6_triggered(); break;
        case 6: _t->on_action7_triggered(); break;
        case 7: _t->on_action8_triggered(); break;
        case 8: _t->on_checkBox_stateChanged((*reinterpret_cast< int(*)>(_a[1]))); break;
        case 9: _t->on_checkBox_2_stateChanged((*reinterpret_cast< int(*)>(_a[1]))); break;
        case 10: _t->on_checkBox_4_stateChanged((*reinterpret_cast< int(*)>(_a[1]))); break;
        case 11: _t->on_checkBox_5_stateChanged((*reinterpret_cast< int(*)>(_a[1]))); break;
        case 12: _t->on_checkBox_3_stateChanged((*reinterpret_cast< int(*)>(_a[1]))); break;
        case 13: _t->on_comboBox_currentIndexChanged((*reinterpret_cast< const QString(*)>(_a[1]))); break;
        case 14: _t->on_pushButton_3_clicked(); break;
        case 15: _t->on_pushButton_2_clicked(); break;
        default: ;
        }
    }
}

QT_INIT_METAOBJECT const QMetaObject MainWindow::staticMetaObject = { {
    &QMainWindow::staticMetaObject,
    qt_meta_stringdata_MainWindow.data,
    qt_meta_data_MainWindow,
    qt_static_metacall,
    nullptr,
    nullptr
} };


const QMetaObject *MainWindow::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->dynamicMetaObject() : &staticMetaObject;
}

void *MainWindow::qt_metacast(const char *_clname)
{
    if (!_clname) return nullptr;
    if (!strcmp(_clname, qt_meta_stringdata_MainWindow.stringdata0))
        return static_cast<void*>(this);
    return QMainWindow::qt_metacast(_clname);
}

int MainWindow::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QMainWindow::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 16)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 16;
    } else if (_c == QMetaObject::RegisterMethodArgumentMetaType) {
        if (_id < 16)
            *reinterpret_cast<int*>(_a[0]) = -1;
        _id -= 16;
    }
    return _id;
}
QT_WARNING_POP
QT_END_MOC_NAMESPACE
