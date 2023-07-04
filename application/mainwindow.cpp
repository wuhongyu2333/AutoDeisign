#include "mainwindow.h"
#include "ui_mainwindow.h"
#include<QFile>
#include<QTextStream>
#include<QFileDialog>
#include<QMessageBox>
#include<string>
#include<QtSql/QSqlDatabase>
#include<QtSql>
#include<map>
#include<vector>
#include<QTime>
#include<QDate>

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    //setFixedSize(this->height(),this->height());
    ui->setupUi(this);
    //QFile f(":qdarkstyle/dark/darkstyle.qss");
    //QFile f(":qdarkstyle/light/lightstyle.qss");
    //QFile f(":/AMOLED.qss");
    //QFile f(":/Aqua.qss");
    //QFile f(":/ConsoleStyle.qss");
    //QFile f(":/ElegantDark.qss");//还可以
    QFile f(":/MacOS.qss");//还可以
    //QFile f(":/ManjaroMix.qss");
    //QFile f(":/MaterialDark.qss");
    //QFile f(":/NeonButtons.qss");
    //QFile f(":/Ubuntu.qss");//还可以

    if (!f.exists())   {
        printf("Unable to set stylesheet, file not found\n");
    }
    else   {
        f.open(QFile::ReadOnly | QFile::Text);
        QTextStream ts(&f);
        qApp->setStyleSheet(ts.readAll());
    }
    ui->openGLWidget->setFocus();
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::keyPressEvent(QKeyEvent *event)
{
    if(event->key()==Qt::Key_Enter) {
        ui->openGLWidget->setFocus();
    }
}

void MainWindow::addMyText(QString str)
{
    ui->textBrowser->append(str);
}

void MainWindow::clearMyComboBox()
{
    ui->comboBox->clear();
}

void MainWindow::addMyMyComboBox(QString str)
{
    ui->comboBox->addItem(str);
}

void MainWindow::on_pushButton_clicked()
{
    ui->textBrowser->clear();
}

void MainWindow::on_menuOpen_pressed()
{
    ui->textBrowser->setStyleSheet("QTextBrowser#myTextBrowser { font-size: 18px; }");
    ui->textBrowser->insertPlainText("123");
}

void MainWindow::on_action1_triggered()//YJK
{
    QString sPath = QFileDialog::getOpenFileName(this,"选择YJK数据文件","C:/Users/wuhongyu2333/Desktop/Visualization-based-on-QT/YJKdemo1","*.ydb");
    if(sPath.length() == 0) {
        QMessageBox::information(NULL, tr("警告！"), tr("未选择任何数据文件"));
    }
    else {
        ui->textBrowser->setStyleSheet("QTextBrowser { font-size: 12px; }");
        ui->textBrowser->append(QDate::currentDate().toString(Qt::ISODate)+" "+QTime::currentTime().toString()+"  开始读取YJK文件："+sPath);
        int wt=int(ui->textBrowser->width()/7);
        QString str(wt,'-');
        ui->textBrowser->append(str);
        ui->openGLWidget->setpath(sPath);
        ui->openGLWidget->getMember(sPath,this);
        ui->textBrowser->append(str);

    }
}

void MainWindow::on_action2_triggered()
{
    QString sPath = QFileDialog::getOpenFileName(this,"选择PKPM数据文件",".","*");
    if(sPath.length() == 0) {
        QMessageBox::information(NULL, tr("警告！"), tr("未选择任何数据文件"));
    }
    else {
        ui->textBrowser->setStyleSheet("QTextBrowser { font-size: 12px; }");
        ui->textBrowser->append(QTime::currentTime().toString()+"  读取PKPM文件“"+sPath+"”成功!");
        int wt=int(ui->textBrowser->width()/7);
        QString str(wt,'-');
        ui->textBrowser->append(str);
    }
}

void MainWindow::on_action3_triggered()
{
    QString sPath = QFileDialog::getOpenFileName(this,"选择SAP2000数据文件",".","*");
    if(sPath.length() == 0) {
        QMessageBox::information(NULL, tr("警告！"), tr("未选择任何数据文件"));
    }
    else {
        ui->textBrowser->setStyleSheet("QTextBrowser { font-size: 12px; }");
        ui->textBrowser->append(QTime::currentTime().toString()+"  读取SAP2000文件“"+sPath+"”成功!");
        int wt=int(ui->textBrowser->width()/7);
        QString str(wt,'-');
        ui->textBrowser->append(str);
    }
}

void MainWindow::on_action7_triggered()
{
    ui->openGLWidget->clear();
}

void MainWindow::on_action6_triggered()
{
    ui->openGLWidget->reset();
}

void MainWindow::on_action8_triggered()
{
    ui->openGLWidget->zoom();
}

void MainWindow::on_checkBox_stateChanged(int arg1)//梁
{
    Q_UNUSED(arg1);
    if(ui->checkBox->isChecked() == true)
    {
        //qDebug()<<"梁被选中";
        ui->openGLWidget->viewKind["beam"]=true;
        ui->openGLWidget->update();
    }
    else{
        //qDebug()<<"梁未被选中";
        ui->openGLWidget->viewKind["beam"]=false;
        ui->openGLWidget->update();
    }


    if(ui->checkBox_2->isChecked() == true)
    {
        //qDebug()<<"柱被选中";
        ui->openGLWidget->viewKind["column"]=true;
        ui->openGLWidget->update();
    }
    else{
        //qDebug()<<"柱未被选中";
        ui->openGLWidget->viewKind["column"]=false;
        ui->openGLWidget->update();
    }

    if(ui->checkBox_3->isChecked() == true)
    {
        //qDebug()<<"支撑被选中";
        ui->openGLWidget->viewKind["brace"]=true;
        ui->openGLWidget->update();
    }
    else{
        //qDebug()<<"支撑未被选中";
        ui->openGLWidget->viewKind["brace"]=false;
        ui->openGLWidget->update();
    }

    if(ui->checkBox_4->isChecked() == true)
    {
        //qDebug()<<"墙被选中";
        ui->openGLWidget->viewKind["wall"]=true;
        ui->openGLWidget->update();
    }
    else{
        //qDebug()<<"墙未被选中";
        ui->openGLWidget->viewKind["wall"]=false;
        ui->openGLWidget->update();
    }

    if(ui->checkBox_5->isChecked() == true)
    {
        //qDebug()<<"板被选中";
        ui->openGLWidget->viewKind["slab"]=true;
        ui->openGLWidget->update();
    }
    else{
        //qDebug()<<"板未被选中";
        ui->openGLWidget->viewKind["slab"]=false;
        ui->openGLWidget->update();
    }

}

void MainWindow::on_checkBox_2_stateChanged(int arg1)
{
    MainWindow::on_checkBox_stateChanged(arg1);
}

void MainWindow::on_checkBox_4_stateChanged(int arg1)
{
        MainWindow::on_checkBox_stateChanged(arg1);
}

void MainWindow::on_checkBox_5_stateChanged(int arg1)
{
        MainWindow::on_checkBox_stateChanged(arg1);
}

void MainWindow::on_checkBox_3_stateChanged(int arg1)
{
        MainWindow::on_checkBox_stateChanged(arg1);
}

void MainWindow::on_comboBox_currentIndexChanged(const QString &arg1)
{
    Q_UNUSED(arg1);
    //qDebug()<<ui->comboBox->currentText();
    ui->openGLWidget->viewFlr=ui->comboBox->currentText();
    ui->openGLWidget->update();
}

void MainWindow::on_pushButton_3_clicked()
{
    if(ui->comboBox->currentIndex()>0){
        int temp=ui->comboBox->currentIndex();
        ui->comboBox->setCurrentIndex(temp-1);
    }
    else{
       QMessageBox::information(NULL, tr("警告！"), tr("当前为全楼模型！"));
    }
}

void MainWindow::on_pushButton_2_clicked()
{
    if(ui->comboBox->currentIndex()<ui->comboBox->count()-1){
        int temp=ui->comboBox->currentIndex();
        ui->comboBox->setCurrentIndex(temp+1);
    }
    else{
        QMessageBox::information(NULL, tr("警告！"), tr("本层是最高层！"));
    }
}
