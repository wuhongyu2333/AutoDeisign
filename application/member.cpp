#include<QMessageBox>
#include<string>
#include<QtSql/QSqlDatabase>
#include<QtSql>
#include<cmath>
#include"member.h"
#include"mainwindow.h"

const float ratio=1.1;

Member::Member()
{
    //qDebug()<<"此处是一个空的structure member对象";
}

Member::Member(QString str,MainWindow *myMainwindow)//传入的参数为文件名
{
//    qDebug()<<"这里的structure member对象已被赋值";
//    qDebug()<<str;

    //开始将数据文件读入member类
    //连接
    QSqlDatabase db=QSqlDatabase::addDatabase("QSQLITE");
    db.setDatabaseName(str);
    if (!db.open()) {
        qDebug() << "Can't open database: " << db.lastError().text();
    }
    QSqlQuery query;

    //开始查询
    //标准层
    query.prepare("SELECT * FROM tblStdFlr");
    if (!query.exec()) {
        //QMessageBox::critical(this,"Error!","未能成功打开模型文件");
        //qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblStdFlr *temp=new tblStdFlr();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->Height=query.value(2).toInt();
            mytblStdFlr.push_back(temp);

            //处理
            std_Height[temp->ID]=temp->Height;

//            qDebug() << "new tblStdFlr";
//            qDebug() << query.value(0);
//            qDebug() << query.value(1);
//            qDebug() << query.value(2);
        }
    }


    //自然层
    query.prepare("SELECT * FROM tblFloor");
    if (!query.exec()) {
        //QMessageBox::critical(this,"SQL error:",query.lastError().text());
        //qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            num_flr++;
            tblFloor *temp=new tblFloor();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->Name=query.value(2).toString();
            temp->StdFlrID=query.value(3).toInt();
            temp->LevelB=query.value(4).toFloat();
            bound=bound>ratio*qAbs(temp->LevelB)?bound:ratio*qAbs(temp->LevelB);
            mytblFloor.push_back(temp);
//            qDebug() << "new tblFloor";
//            qDebug() << query.value(0);
//            qDebug() << query.value(1);
//            qDebug() << query.value(2);
//            qDebug() << query.value(3);
//            qDebug() << query.value(4);
        }
    }

    //轴线
    query.prepare("SELECT * FROM tblAxis");
    if (!query.exec()) {        
        //QMessageBox::critical(this,"SQL error:",query.lastError().text());
        //qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblAxis *temp=new tblAxis();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->StdFlrID=query.value(2).toInt();
            temp->Jt1ID=query.value(3).toInt();
            temp->Jt1ID=query.value(4).toInt();
            temp->Name=query.value(5).toString();
            mytblAxis.push_back(temp);
//            qDebug() << "new tblAxis";
//            qDebug() << query.value(0);
//            qDebug() << query.value(1);
//            qDebug() << query.value(2);
//            qDebug() << query.value(3);
//            qDebug() << query.value(4);
//            qDebug() << query.value(5);
        }
    }

    //网格
    query.prepare("SELECT * FROM tblGrid");
    if (!query.exec()) {
        //QMessageBox::critical(this,"SQL error:",query.lastError().text());
        //qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblGrid *temp=new tblGrid();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->StdFlrID=query.value(2).toInt();
            temp->Jt1ID=query.value(3).toInt();
            temp->Jt2ID=query.value(4).toInt();
            temp->AxisID=query.value(5).toInt();
            mytblGrid.push_back(temp);

            //处理
            if(!std_Grid.count(temp->StdFlrID)){
                std::vector<tblGrid*> temp_grid;
                std_Grid.insert({temp->StdFlrID,temp_grid});
            }
            std_Grid[temp->StdFlrID].push_back(temp);

            std::vector<int> joint_couple;
            joint_couple.push_back(temp->Jt1ID);
            joint_couple.push_back(temp->Jt2ID);
            Grid_Joint.insert({temp->ID,joint_couple});

//            qDebug() << "new tblGrid";
//            qDebug() << query.value(0);
//            qDebug() << query.value(1);
//            qDebug() << query.value(2);
//            qDebug() << query.value(3);
//            qDebug() << query.value(4);
//            qDebug() << query.value(5);
        }
    }

    //节点
    query.prepare("SELECT * FROM tblJoint");
    if (!query.exec()) {
        //QMessageBox::critical(this,"SQL error:",query.lastError().text());
        //qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblJoint *temp=new tblJoint();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->StdFlrID=query.value(2).toInt();
            temp->X=query.value(3).toFloat();
            temp->Y=query.value(4).toFloat();
            temp->HDiff=query.value(5).toInt();
            mytblJoint.push_back(temp);

            bound=bound>ratio*qAbs(temp->X)?bound:ratio*qAbs(temp->X);
            bound=bound>ratio*qAbs(temp->Y)?bound:ratio*qAbs(temp->Y);

            x_max=x_max==0?temp->X:x_max;
            x_max=x_max>temp->X?x_max:temp->X;
            x_min=x_min==0?temp->X:x_min;
            x_min=x_min<temp->X?x_min:temp->X;
            y_max=y_max==0?temp->Y:y_max;
            y_max=y_max>temp->Y?y_max:temp->Y;
            y_min=y_min==0?temp->Y:y_min;
            y_min=y_min<temp->Y?y_min:temp->Y;

            //处理
            if(!std_Joint.count(temp->StdFlrID)){
                std::vector<tblJoint*> temp_joint;
                std_Joint.insert({temp->StdFlrID,temp_joint});
            }
            std_Joint[temp->StdFlrID].push_back(temp);

            std::vector<float> temp_loc;
            temp_loc.push_back(temp->X);
            temp_loc.push_back(temp->Y);
            Joint_Loc.insert({temp->ID,temp_loc});

//            qDebug() << "new tblJoint";
//            qDebug() << query.value(0);
//            qDebug() << query.value(1);
//            qDebug() << query.value(2);
//            qDebug() << query.value(3);
//            qDebug() << query.value(4);
//            qDebug() << query.value(5);
        }
    }

    //墙类型
    query.prepare("SELECT * FROM tblWallSect");
    if (!query.exec()) {
        //QMessageBox::critical(this,"SQL error:",query.lastError().text());
        //qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblWallSect *temp=new tblWallSect();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->Mat=query.value(2).toInt();
            temp->Kind=query.value(3).toInt();
            temp->B=query.value(4).toInt();
            mytblWallSect.push_back(temp);
//            qDebug() << "new tblWallSect";
//            qDebug() << query.value(0);
//            qDebug() << query.value(1);
//            qDebug() << query.value(2);
//            qDebug() << query.value(3);
//            qDebug() << query.value(4);
        }
    }

    //墙布置
    query.prepare("SELECT * FROM tblWallSeg");
    if (!query.exec()) {
        //QMessageBox::critical(this,"SQL error:",query.lastError().text());
        //qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblWallSeg *temp=new tblWallSeg();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->StdFlrID=query.value(2).toInt();
            temp->SectID=query.value(3).toInt();
            temp->GridID=query.value(4).toInt();
            temp->Ecc=query.value(5).toInt();
            temp->HDiff1=query.value(6).toInt();
            temp->HDiff2=query.value(7).toInt();
            temp->HDiffB=query.value(8).toInt();
            temp->value=0.3;
            mytblWallSeg.push_back(temp);

            //处理
            if(!std_Wall.count(temp->StdFlrID)){
                std::vector<tblWallSeg*> temp_wall;
                std_Wall.insert({temp->StdFlrID,temp_wall});
            }
            std_Wall[temp->StdFlrID].push_back(temp);

//            qDebug() << "new tblWallSeg";
//            qDebug() << query.value(0);
//            qDebug() << query.value(1);
//            qDebug() << query.value(2);
//            qDebug() << query.value(3);
//            qDebug() << query.value(4);
//            qDebug() << query.value(5);
//            qDebug() << query.value(6);
//            qDebug() << query.value(7);
//            qDebug() << query.value(8);
        }
    }

    //梁类型
    query.prepare("SELECT * FROM tblBeamSect");
    if (!query.exec()) {
        //QMessageBox::critical(this,"SQL error:",query.lastError().text());
        //qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblBeamSect *temp=new tblBeamSect();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->Name=query.value(2).toString();
            temp->Mat=query.value(3).toInt();
            temp->Kind=query.value(4).toInt();
            temp->ShapeVal=query.value(5).toString();
            mytblBeamSect.push_back(temp);
//            qDebug() << "new tblBeamSect";
//            qDebug() << query.value(0);
//            qDebug() << query.value(1);
//            qDebug() << query.value(2);
//            qDebug() << query.value(3);
//            qDebug() << query.value(4);
//            qDebug() << query.value(5);
        }
    }

    //梁布置
    query.prepare("SELECT * FROM tblBeamSeg");
    if (!query.exec()) {
        //QMessageBox::critical(this,"SQL error:",query.lastError().text());
        //qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblBeamSeg *temp=new tblBeamSeg();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->StdFlrID=query.value(2).toInt();
            temp->SectID=query.value(3).toInt();
            temp->GridID=query.value(4).toInt();
            temp->Ecc=query.value(5).toInt();
            temp->HDiff1=query.value(6).toInt();
            temp->HDiff2=query.value(7).toInt();
            temp->value=0.6;
            mytblBeamSeg.push_back(temp);

            //处理
            if(!std_Beam.count(temp->StdFlrID)){
                std::vector<tblBeamSeg*> temp_beam;
                std_Beam.insert({temp->StdFlrID,temp_beam});
            }
            std_Beam[temp->StdFlrID].push_back(temp);


//            qDebug() << "new tblBeamSeg";
//            qDebug() << query.value(0);
//            qDebug() << query.value(1);
//            qDebug() << query.value(2);
//            qDebug() << query.value(3);
//            qDebug() << query.value(4);
//            qDebug() << query.value(5);
//            qDebug() << query.value(6);
//            qDebug() << query.value(7);

        }
    }

    //次梁
    query.prepare("SELECT * FROM tblSubBeam");
    if (!query.exec()) {
        //QMessageBox::critical(this,"SQL error:",query.lastError().text());
        //qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblSubBeam *temp=new tblSubBeam();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->StdFlrID=query.value(2).toInt();
            temp->SectID=query.value(3).toInt();
            temp->X1=query.value(4).toFloat();
            temp->Y1=query.value(5).toFloat();
            temp->Z1=query.value(6).toFloat();
            temp->X2=query.value(7).toFloat();
            temp->Y2=query.value(8).toFloat();
            temp->Z2=query.value(9).toFloat();
            temp->Gird1ID=query.value(10).toInt();
            temp->Gird2ID=query.value(11).toInt();
            mytblSubBeam.push_back(temp);

            //处理
            if(!std_SubBeam.count(temp->StdFlrID)){
                std::vector<tblSubBeam*> temp_subbeam;
                std_SubBeam.insert({temp->StdFlrID,temp_subbeam});
            }
            std_SubBeam[temp->StdFlrID].push_back(temp);

//            qDebug() << "new tblSubBeam";
//            qDebug() << query.value(0);
//            qDebug() << query.value(1);
//            qDebug() << query.value(2);
//            qDebug() << query.value(3);
//            qDebug() << query.value(4);
//            qDebug() << query.value(5);
//            qDebug() << query.value(6);
//            qDebug() << query.value(7);
//            qDebug() << query.value(8);
//            qDebug() << query.value(9);
//            qDebug() << query.value(10);
//            qDebug() << query.value(11);
        }
    }

    //柱类型
    query.prepare("SELECT * FROM tblColSect");
    if (!query.exec()) {
        //QMessageBox::critical(this,"SQL error:",query.lastError().text());
        //qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblColSect *temp=new tblColSect();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->Name=query.value(2).toString();
            temp->Mat=query.value(3).toInt();
            temp->Kind=query.value(4).toInt();
            temp->ShapeVal=query.value(5).toString();
            mytblColSect.push_back(temp);
//            qDebug() << "new tblColSect";
//            qDebug() << query.value(0);
//            qDebug() << query.value(1);
//            qDebug() << query.value(2);
//            qDebug() << query.value(3);
//            qDebug() << query.value(4);
//            qDebug() << query.value(5);
        }
    }

    //柱布置
    query.prepare("SELECT * FROM tblColSeg");
    if (!query.exec()) {
        //QMessageBox::critical(this,"SQL error:",query.lastError().text());
        //qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblColSeg *temp=new tblColSeg();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->StdFlrID=query.value(2).toInt();
            temp->SectID=query.value(3).toInt();
            temp->JtID=query.value(4).toInt();
            temp->EccX=query.value(5).toInt();
            temp->EccY=query.value(6).toInt();
            temp->Rotation=query.value(7).toFloat();
            temp->HDiffB=query.value(8).toInt();
            temp->value=0.9;
            mytblColSeg.push_back(temp);

            //处理
            if(!std_Col.count(temp->StdFlrID)){
                std::vector<tblColSeg*> temp_col;
                std_Col.insert({temp->StdFlrID,temp_col});
            }
            std_Col[temp->StdFlrID].push_back(temp);

//            qDebug() << "new tblColSeg";
//            qDebug() << query.value(0);
//            qDebug() << query.value(1);
//            qDebug() << query.value(2);
//            qDebug() << query.value(3);
//            qDebug() << query.value(4);
//            qDebug() << query.value(5);
//            qDebug() << query.value(6);
//            qDebug() << query.value(7);
        }
    }

    //支撑类型
    query.prepare("SELECT * FROM tblBraceSect");
    if (!query.exec()) {
        //QMessageBox::critical(this,"SQL error:",query.lastError().text());
        //qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblBraceSect *temp=new tblBraceSect();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->Name=query.value(2).toString();
            temp->Mat=query.value(3).toInt();
            temp->Kind=query.value(4).toInt();
            temp->ShapeVal=query.value(5).toString();
            mytblBraceSect.push_back(temp);
//            qDebug() << "new tblBraceSect";
//            qDebug() << query.value(0);
//            qDebug() << query.value(1);
//            qDebug() << query.value(2);
//            qDebug() << query.value(3);
//            qDebug() << query.value(4);
//            qDebug() << query.value(5);
        }
    }

    //支撑布置
    query.prepare("SELECT * FROM tblBraceSeg");
    if (!query.exec()) {
        //QMessageBox::critical(this,"SQL error:",query.lastError().text());
        //qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblBraceSeg *temp=new tblBraceSeg();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->StdFlrID=query.value(2).toInt();
            temp->SectID=query.value(3).toInt();
            temp->Jt1ID=query.value(4).toInt();
            temp->Jt2ID=query.value(5).toInt();
            temp->EccX1=query.value(6).toInt();
            temp->EccY1=query.value(7).toInt();
            temp->HDiff1=query.value(8).toInt();
            temp->EccX2=query.value(9).toInt();
            temp->EccY2=query.value(10).toInt();
            temp->HDiff2=query.value(11).toInt();
            temp->Rotation=query.value(12).toFloat();
            //temp->value=rand() / float(RAND_MAX);
            temp->value=0.4;
            mytblBraceSeg.push_back(temp);

            //处理
            if(!std_Brace.count(temp->StdFlrID)){
                std::vector<tblBraceSeg*> temp_brace;
                std_Brace.insert({temp->StdFlrID,temp_brace});
            }
            std_Brace[temp->StdFlrID].push_back(temp);

//            qDebug() << "new tblBraceSeg";
//            qDebug() << query.value(0);
//            qDebug() << query.value(1);
//            qDebug() << query.value(2);
//            qDebug() << query.value(3);
//            qDebug() << query.value(4);
//            qDebug() << query.value(5);
//            qDebug() << query.value(6);
//            qDebug() << query.value(7);
//            qDebug() << query.value(8);
//            qDebug() << query.value(9);
//            qDebug() << query.value(10);
//            qDebug() << query.value(11);
//            qDebug() << query.value(12);
        }
    }

    db.close();
    myMainwindow->addMyText(QDate::currentDate().toString(Qt::ISODate)+" "+QTime::currentTime().toString()+"  读取YJK文件成功！");
    myMainwindow->clearMyComboBox();
    myMainwindow->addMyMyComboBox("ALL");
    for(int f=1;f<=num_flr;f++){
        myMainwindow->addMyMyComboBox(QString::number(f));
    }
    //qDebug() <<"自然层数量"<< num_flr;
}

Member::~Member()
{

}
