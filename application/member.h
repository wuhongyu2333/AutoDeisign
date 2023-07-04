#ifndef MEMBER_H
#define MEMBER_H
#include<QString>
#include<vector>
#include<map>
#include"mainwindow.h"


class tblStdFlr{
public:
    int ID;
    int No_;
    int Height;
};

class tblFloor{
public:
    int ID;
    int No_;
    QString Name;
    int StdFlrID;
    float LevelB;
};

class tblAxis{
public:
    int ID;
    int No_;
    int StdFlrID;
    QString Name;
    int Jt1ID;
    int Jt2ID;
};

class tblGrid{
public:
    int ID;
    int No_;
    int StdFlrID;
    int Jt1ID;
    int Jt2ID;
    int AxisID;
};

class tblJoint{
public:
    int ID;
    int No_;
    int StdFlrID;
    float X;
    float Y;
    int HDiff;
};

class tblWallSect{
public:
    int ID;
    int No_;
    int Mat;
    int Kind;
    int B;
};

class tblWallSeg{
public:
    int ID;
    int No_;
    int StdFlrID;
    int SectID;
    int GridID;
    int Ecc;
    int HDiff1;
    int HDiff2;
    int HDiffB;

    float value;
};

class tblBeamSect{
public:
    int ID;
    int No_;
    QString Name;
    int Mat;
    int Kind;
    QString ShapeVal;
};

class tblBeamSeg{
public:
    int ID;
    int No_;
    int StdFlrID;
    int SectID;
    int GridID;
    int Ecc;
    int HDiff1;
    int HDiff2;

    float value;
};

class tblSubBeam{
public:
    int ID;
    int No_;
    int StdFlrID;
    int SectID;
    float X1;
    float Y1;
    float Z1;
    float X2;
    float Y2;
    float Z2;
    int Gird1ID;
    int Gird2ID;

    float value;
};

class tblColSect{
public:
    int ID;
    int No_;
    QString Name;
    int Mat;
    int Kind;
    QString ShapeVal;
};

class tblColSeg{
public:
    int ID;
    int No_;
    int StdFlrID;
    int SectID;
    int JtID;
    int EccX;
    int EccY;
    float Rotation;
    int HDiffB;

    float value;
};

class tblBraceSect{
public:
    int ID;
    int No_;
    QString Name;
    int Mat;
    int Kind;
    QString ShapeVal;
};

class tblBraceSeg{
public:
    int ID;
    int No_;
    int StdFlrID;
    int SectID;
    int Jt1ID;
    int Jt2ID;
    int EccX1;
    int EccY1;
    int HDiff1;
    int EccX2;
    int EccY2;
    int HDiff2;
    float Rotation;

    float value;
};



class Member{
public:

    //读取信息
    std::vector<tblStdFlr*> mytblStdFlr;
    std::vector<tblFloor*> mytblFloor;
    std::vector<tblAxis*> mytblAxis;
    std::vector<tblGrid*> mytblGrid;
    std::vector<tblJoint*> mytblJoint;
    std::vector<tblWallSect*> mytblWallSect;
    std::vector<tblWallSeg*> mytblWallSeg;
    std::vector<tblBeamSect*> mytblBeamSect;
    std::vector<tblBeamSeg*> mytblBeamSeg;
    std::vector<tblSubBeam*> mytblSubBeam;
    std::vector<tblColSect*> mytblColSect;
    std::vector<tblColSeg*> mytblColSeg;
    std::vector<tblBraceSect*> mytblBraceSect;
    std::vector<tblBraceSeg*> mytblBraceSeg;

    //处理信息
    float bound;
    float x_max=0;
    float x_min=0;
    float y_max=0;
    float y_min=0;
    int num_flr=0;
    std::map<int,std::vector<tblGrid*>> std_Grid;
    std::map<int,std::vector<tblJoint*>> std_Joint;
    std::map<int,std::vector<tblWallSeg*>> std_Wall;
    std::map<int,std::vector<tblBeamSeg*>> std_Beam;
    std::map<int,std::vector<tblSubBeam*>> std_SubBeam;
    std::map<int,std::vector<tblColSeg*>> std_Col;
    std::map<int,std::vector<tblBraceSeg*>> std_Brace;
    std::map<int,std::vector<int>> Grid_Joint;
    std::map<int,std::vector<float>> Joint_Loc;
    std::map<int,int> std_Height;


    Member();
    Member(QString str,MainWindow *myMainwindow);
    ~Member();
};
#endif // MEMBER_H
