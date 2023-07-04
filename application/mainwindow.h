#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include<QKeyEvent>

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = 0);
    ~MainWindow();
    virtual void keyPressEvent(QKeyEvent *event);

    void addMyText(QString str);
    void clearMyComboBox();
    void addMyMyComboBox(QString str);

private slots:
    void on_pushButton_clicked();
    void on_menuOpen_pressed();
    void on_action1_triggered();
    void on_action2_triggered();
    void on_action3_triggered();
    void on_action6_triggered();
    void on_action7_triggered();
    void on_action8_triggered();    
    void on_checkBox_stateChanged(int arg1);
    void on_checkBox_2_stateChanged(int arg1);
    void on_checkBox_4_stateChanged(int arg1);
    void on_checkBox_5_stateChanged(int arg1);
    void on_checkBox_3_stateChanged(int arg1);
    void on_comboBox_currentIndexChanged(const QString &arg1);
    void on_pushButton_3_clicked();
    void on_pushButton_2_clicked();

private:
    Ui::MainWindow *ui;
};

#endif // MAINWINDOW_H
