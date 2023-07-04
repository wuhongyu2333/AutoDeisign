#ifndef MYOPENGLWIDGET_H
#define MYOPENGLWIDGET_H

#include<QOpenGLWidget>
#include<QOpenGLFunctions_4_2_Core>
#include<vector>
#include"member.h"


class MyOpenglWidget : public QOpenGLWidget,QOpenGLFunctions_4_2_Core
{
    Q_OBJECT
public:
    float dx;
    float dy;
    float rot_x;
    float rot_y;
    float rot_z;
    float dscale;
    QPoint currentPos;
    QPoint lastPos;
    bool moveModel=false;
    bool rotateModel=false;
    QString sPath;
    Member structureMember;
    std::map<QString,bool> viewKind;
    QString viewFlr;

    explicit MyOpenglWidget(QWidget *parent = nullptr);
    void clear();
    void reset();
    void zoom();
    void setpath(QString str);//让myopenglwidget类获得文件路径
    void getMember(QString str,MainWindow *myMainwindow);//让myopenglwidget类根据文件路径读取其中的构件成员

protected:
    bool show;
    virtual void initializeGL();
    virtual void resizeGL(int w, int h);
    virtual void paintGL();
    virtual void resizeEvent(QResizeEvent *event);
    virtual void keyPressEvent(QKeyEvent *event);
    virtual void wheelEvent(QWheelEvent *event);
    virtual void mousePressEvent(QMouseEvent *event);
    virtual void mouseMoveEvent(QMouseEvent *event);

signals:

public slots:


};

#endif // MYOPENGLWIDGET_H
