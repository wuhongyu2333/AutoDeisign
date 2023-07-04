#include "myopenglwidget.h"
#include <QResizeEvent>
#include<GL/glu.h>
#include<windows.h>
//include<glut.h>
#include<QDebug>
#include"member.h"
#include<random>
#include"mainwindow.h"

/*
static const char* VERTEX_SHADER_CODE =
        "#version 330 \n"
        "layout(location = 0) in vec3 posVertex;\n"
        "void main() {\n"
        "  gl_Position = vec4(posVertex, 1.0f);\n"
        "}\n";

static const char* FRAGMENT_SHADER_CODE =
        "#version 330\n"
        "out vec4 fragColor;\n"
        "void main() {\n"
        "  fragColor = vec4(1.0f, 0.0f, 0.0f, 1.0f);\n"
        "}\n";

float vertices[]={
    -0.5,-0.5,0,
    -0.5,0.5,0,
    0.5,-0.5,0,
    0.5,-0.5,0,
    0.5,0.5,0,
    -0.5,0.5,0
};
//创建VBA和VAO对象
unsigned int VBO,VAO;
GLfloat m_xRot=5.0f;
*/

MyOpenglWidget::MyOpenglWidget(QWidget *parent)
    : QOpenGLWidget(parent),dx(0),dy(0),rot_x(0),rot_y(0),rot_z(0),dscale(1.2)
{
    viewKind.insert({"beam",true});
    viewKind.insert({"column",true});
    viewKind.insert({"wall",true});
    viewKind.insert({"brace",true});
    viewKind.insert({"slab",true});
    viewFlr="ALL";
    show=true;
}

void MyOpenglWidget::clear()
{
    show=false;
    repaint();
}

void MyOpenglWidget::reset()
{
    dx=0;
    dy=0;
    dscale=1;
    rot_x=0;
    rot_y=0;
    rot_z=0;
    show=true;
    update();
}

void MyOpenglWidget::zoom()
{
    dx=0;
    dy=0;
    show=true;
    update();
}

void MyOpenglWidget::setpath(QString str)
{
    sPath=str;
}

void MyOpenglWidget::getMember(QString str,MainWindow *myMainwindow)
{
    Member temp(str,myMainwindow);
    structureMember=temp;
}

void MyOpenglWidget::resizeEvent(QResizeEvent *event)
{
    int w=event->size().width();
    int h=event->size().height();
    int s=w<h?w:h;
    resize(s,s);
    QWidget::resizeEvent(event);
}

void MyOpenglWidget::keyPressEvent(QKeyEvent *event)
{
    switch (event->key()) {
    case Qt::Key_Up:
    {
        dy+=0.03;
        update();
        break;
    }
    case Qt::Key_Down:
    {
        dy-=0.03;
        update();
        break;
    }
    case Qt::Key_Left:
    {
        dx-=0.03;
        update();
        break;
    }
    case Qt::Key_Right:
    {
        dx+=0.03;
        update();
        break;
    }
    }
}

void MyOpenglWidget::wheelEvent(QWheelEvent *event)
{
    dscale+=float(event->angleDelta().y())/1000;
    dscale=dscale>0.01?dscale:0.01;
    update();
}

void MyOpenglWidget::mousePressEvent(QMouseEvent *event)
{
    currentPos=event->pos();
    lastPos=event->pos();
    //qDebug()<<"鼠标被按下";
    moveModel=false;
    rotateModel=false;
    if(event->button()==Qt::LeftButton){
        //qDebug()<<"左键被按下";
        moveModel=true;
    }
    else if(event->button()==Qt::RightButton){
        rotateModel=true;
    }
    update();
}

void MyOpenglWidget::mouseMoveEvent(QMouseEvent *event)
{
    currentPos=event->pos();
    if(moveModel){
        //qDebug()<<"左键被按下";
        dx+=(float(currentPos.x()-lastPos.x())/10000);
        dy-=(float(currentPos.y()-lastPos.y())/10000);
        update();
    }
    else if(rotateModel){
        //qDebug()<<currentPos.x()-lastPos.x()/2;
        rot_x+=(float(currentPos.x()-lastPos.x())/2);
        rot_y+=(float(currentPos.y()-lastPos.y())/2);
        update();
    }

}

void MyOpenglWidget::initializeGL()
{
    initializeOpenGLFunctions();//为当前环境初始化opengl功能
    glShadeModel(GL_SMOOTH);
    glClearDepth(1);
    glEnable(GL_DEPTH_TEST);
    glDepthFunc(GL_LESS);
    //glDepthFunc(GL_LEQUAL);
    glHint(GL_PERSPECTIVE_CORRECTION_HINT,GL_NICEST);


    /*
    glGenVertexArrays(1,&VAO);
    glGenBuffers(1,&VBO);

    //绑定VAO和VBO对象
    glBindVertexArray(VAO);
    glBindBuffer(GL_ARRAY_BUFFER,VBO);

    //为当前绑定到target的缓冲区对象创建一个新的数据存储
    //如果data不是NULL，则使用来自此指针的数据初始化数据存储
    glBufferData(GL_ARRAY_BUFFER,sizeof(vertices),vertices,GL_STATIC_DRAW);

    //告知显卡如何解析缓冲里的属性值
    glVertexAttribPointer(0,3,GL_FLOAT,GL_FALSE,3*sizeof(float),(void*)0);

    //开启VAO管理的第一个属性值
    glEnableVertexAttribArray(0);
    glBindBuffer(GL_ARRAY_BUFFER,0);
    glBindVertexArray(0);
    glPolygonMode(GL_FRONT_AND_BACK,GL_LINE);
    */

}

void MyOpenglWidget::resizeGL(int w, int h)
{
    //glViewport(0, 0, w, h);
}

void MyOpenglWidget::paintGL()
{
    glClearColor(0.45,0.45,0.45,0);
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();

    //gluLookAt(1,1,1,0,0,0,0.5,0.5,0.5);
    //抗锯齿命令
    glEnable(GL_LINE_SMOOTH);
    glEnable(GL_POINT_SMOOTH);
    glEnable(GL_POLYGON_SMOOTH);
    glHint(GL_LINE_SMOOTH_HINT, GL_NICEST);
    glHint(GL_POINT_SMOOTH_HINT, GL_NICEST);
    glHint(GL_POLYGON_SMOOTH_HINT, GL_NICEST);
    if(show){
//        qDebug() <<"bound"<<structureMember.bound;
//        qDebug() <<"x_max"<<structureMember.x_max;
//        qDebug() <<"x_min"<<structureMember.x_min;
//        qDebug() <<"y_max"<<structureMember.y_max;
//        qDebug() <<"y_min"<<structureMember.y_min;
//        qDebug() <<"rot_x"<<rot_x;
//        qDebug() <<"rot_y"<<rot_y;

        glRotatef(60,0,0,1);
        glRotatef(-330,1,0,0);
        glRotatef(-225,0,1,0);

        glTranslatef(dx-(structureMember.x_max+structureMember.x_min)/(2*structureMember.bound),dy-(structureMember.y_max+structureMember.y_min)/(2*structureMember.bound),0);
        glScalef(dscale,dscale,dscale);
        //glRotatef(rot_x,1,0,0);
        //glRotatef(rot_y,0,1,0);
        //        glRotatef(60,0,0,1);
        //        glRotatef(-330,1,0,0);
        //        glRotatef(-225,0,1,0);


        glBegin(GL_LINES);
        for(int i=0;i<structureMember.mytblFloor.size();i++){
            if(viewFlr=="ALL"|| viewFlr.toInt()==i+1){
                tblFloor* current_flr=structureMember.mytblFloor[i];
                float current_levelB=current_flr->LevelB/structureMember.bound;
                float current_height=structureMember.std_Height[current_flr->StdFlrID]/structureMember.bound;
                //qDebug() << "current_levelB"<<current_levelB;
                //qDebug() << "current_height"<<current_height;


                //梁
                if(viewKind["beam"])
                {
                    std::vector<tblBeamSeg*> current_beam_group=structureMember.std_Beam[current_flr->StdFlrID];
                    for(int j=0;j<current_beam_group.size();j++){
                        tblBeamSeg* current_beam=current_beam_group[j];
                        float temp_color=current_beam->value;
                        glColor3f(0.7-temp_color,1-temp_color,0.3-temp_color);
                        //glcolor3f(temp_color,temp_color,temp_color);
                        int current_grid=current_beam->GridID;
                        std::vector<int> current_jointgroup=structureMember.Grid_Joint[current_grid];
                        glVertex3f(structureMember.Joint_Loc[current_jointgroup[0]][0]/structureMember.bound,structureMember.Joint_Loc[current_jointgroup[0]][1]/structureMember.bound,current_levelB+current_height);
                        glVertex3f(structureMember.Joint_Loc[current_jointgroup[1]][0]/structureMember.bound,structureMember.Joint_Loc[current_jointgroup[1]][1]/structureMember.bound,current_levelB+current_height);
                    }
                }

                //柱
                if(viewKind["column"])
                {
                    std::vector<tblColSeg*> current_col_group=structureMember.std_Col[current_flr->StdFlrID];
                    for(int j=0;j<current_col_group.size();j++){
                        tblColSeg* current_col=current_col_group[j];
                        float temp_color=current_col->value;
                        glColor3f(temp_color,0.5-temp_color,1-temp_color);
                        int current_joint=current_col->JtID;
                        glVertex3f(structureMember.Joint_Loc[current_joint][0]/structureMember.bound,structureMember.Joint_Loc[current_joint][1]/structureMember.bound,current_levelB);
                        glVertex3f(structureMember.Joint_Loc[current_joint][0]/structureMember.bound,structureMember.Joint_Loc[current_joint][1]/structureMember.bound,current_levelB+current_height);
                    }
                }
                //支撑
                if(viewKind["brace"])
                {
                    glColor3f(0.8,0.8,0.8);
                    std::vector<tblBraceSeg*> current_brace_group=structureMember.std_Brace[current_flr->StdFlrID];
                    for(int j=0;j<current_brace_group.size();j++){
                        tblBraceSeg* current_brace=current_brace_group[j];
                        int current_joint_1=current_brace->Jt1ID;
                        int current_joint_2=current_brace->Jt2ID;
                        int current_h1=current_brace->HDiff1/structureMember.bound;
                        int current_h2=current_brace->HDiff2/structureMember.bound;
                        glVertex3f(structureMember.Joint_Loc[current_joint_1][0]/structureMember.bound,structureMember.Joint_Loc[current_joint_1][1]/structureMember.bound,current_levelB+current_h1);
                        glVertex3f(structureMember.Joint_Loc[current_joint_2][0]/structureMember.bound,structureMember.Joint_Loc[current_joint_2][1]/structureMember.bound,current_levelB+current_h2);
                    }
                }
                //墙的边线
                if(viewKind["wall"])
                {
                    glColor4f(0.8,0.6,0.3,0.7);
                    std::vector<tblWallSeg*> current_wall_group=structureMember.std_Wall[current_flr->StdFlrID];
                    for(int j=0;j<current_wall_group.size();j++){
                        tblWallSeg* current_wall=current_wall_group[j];
                        int current_grid=current_wall->GridID;
                        std::vector<int> current_jointgroup=structureMember.Grid_Joint[current_grid];

                        glVertex3f(structureMember.Joint_Loc[current_jointgroup[0]][0]/structureMember.bound,structureMember.Joint_Loc[current_jointgroup[0]][1]/structureMember.bound,current_levelB);
                        glVertex3f(structureMember.Joint_Loc[current_jointgroup[1]][0]/structureMember.bound,structureMember.Joint_Loc[current_jointgroup[1]][1]/structureMember.bound,current_levelB);
                        glVertex3f(structureMember.Joint_Loc[current_jointgroup[0]][0]/structureMember.bound,structureMember.Joint_Loc[current_jointgroup[0]][1]/structureMember.bound,current_levelB+current_height);
                        glVertex3f(structureMember.Joint_Loc[current_jointgroup[1]][0]/structureMember.bound,structureMember.Joint_Loc[current_jointgroup[1]][1]/structureMember.bound,current_levelB+current_height);
                        glVertex3f(structureMember.Joint_Loc[current_jointgroup[0]][0]/structureMember.bound,structureMember.Joint_Loc[current_jointgroup[0]][1]/structureMember.bound,current_levelB);
                        glVertex3f(structureMember.Joint_Loc[current_jointgroup[0]][0]/structureMember.bound,structureMember.Joint_Loc[current_jointgroup[0]][1]/structureMember.bound,current_levelB+current_height);
                        glVertex3f(structureMember.Joint_Loc[current_jointgroup[1]][0]/structureMember.bound,structureMember.Joint_Loc[current_jointgroup[1]][1]/structureMember.bound,current_levelB);
                        glVertex3f(structureMember.Joint_Loc[current_jointgroup[1]][0]/structureMember.bound,structureMember.Joint_Loc[current_jointgroup[1]][1]/structureMember.bound,current_levelB+current_height);
                    }
                }
            }
        }
        glGetError();
        glEnd();
        glFlush();

        if(viewKind["wall"])
        {
            glBegin(GL_QUADS);
            for(int i=0;i<structureMember.mytblFloor.size();i++){
                if(viewFlr=="ALL"|| viewFlr.toInt()==i+1){
                    tblFloor* current_flr=structureMember.mytblFloor[i];
                    float current_levelB=current_flr->LevelB/structureMember.bound;
                    float current_height=structureMember.std_Height[current_flr->StdFlrID]/structureMember.bound;
//                    qDebug() << "current_levelB"<<current_levelB;
//                    qDebug() << "current_height"<<current_height;

                    //墙


                    std::vector<tblWallSeg*> current_wall_group=structureMember.std_Wall[current_flr->StdFlrID];
                    for(int j=0;j<current_wall_group.size();j++){
                        tblWallSeg* current_wall=current_wall_group[j];
                        float temp_color=current_wall->value;
                        glColor4f(temp_color,temp_color,temp_color,0.3);
                        int current_grid=current_wall->GridID;
                        std::vector<int> current_jointgroup=structureMember.Grid_Joint[current_grid];
                        glColor4f(0.5,0.5,0.5,0.3);
                        glVertex3f(structureMember.Joint_Loc[current_jointgroup[0]][0]/structureMember.bound,structureMember.Joint_Loc[current_jointgroup[0]][1]/structureMember.bound,current_levelB);
                        glVertex3f(structureMember.Joint_Loc[current_jointgroup[1]][0]/structureMember.bound,structureMember.Joint_Loc[current_jointgroup[1]][1]/structureMember.bound,current_levelB);
                        glVertex3f(structureMember.Joint_Loc[current_jointgroup[1]][0]/structureMember.bound,structureMember.Joint_Loc[current_jointgroup[1]][1]/structureMember.bound,current_levelB+current_height);
                        glVertex3f(structureMember.Joint_Loc[current_jointgroup[0]][0]/structureMember.bound,structureMember.Joint_Loc[current_jointgroup[0]][1]/structureMember.bound,current_levelB+current_height);
                    }
                }
            }
        }
        glGetError();
        glEnd();
        glFlush();
    }
}


