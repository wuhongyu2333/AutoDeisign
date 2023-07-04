#include<QMessageBox>
#include<string>
#include<QtSql/QSqlDatabase>
#include<QtSql>
#include"member.h"


Member::Member()
{
    qDebug()<<"空对象";
    word="这是一个空的对象";
}

Member::Member(QString str)//传入的参数为文件名
{
    qDebug()<<"I AM HERE";
    qDebug()<<str;
    word="这里的对象已被赋值";


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
        qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblStdFlr *temp=new tblStdFlr();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->Height=query.value(2).toInt();
            mytblStdFlr.push_back(temp);
            qDebug() << "new tblStdFlr";
            qDebug() << query.value(0);
            qDebug() << query.value(1);
            qDebug() << query.value(2);
        }
    }


    //自然层
    query.prepare("SELECT * FROM tblFloor");
    if (!query.exec()) {
        qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblFloor *temp=new tblFloor();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->Name=query.value(2).toString();
            temp->StdFlrID=query.value(3).toInt();
            temp->LevelB=query.value(4).toFloat();
            mytblFloor.push_back(temp);
            qDebug() << "new tblFloor";
            qDebug() << query.value(0);
            qDebug() << query.value(1);
            qDebug() << query.value(2);
            qDebug() << query.value(3);
            qDebug() << query.value(4);
        }
    }

    //轴线
    query.prepare("SELECT * FROM tblAxis");
    if (!query.exec()) {
        qDebug() << "SQL error: " << query.lastError().text();
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
            qDebug() << "new tblAxis";
            qDebug() << query.value(0);
            qDebug() << query.value(1);
            qDebug() << query.value(2);
            qDebug() << query.value(3);
            qDebug() << query.value(4);
            qDebug() << query.value(5);
        }
    }

    //网格
    query.prepare("SELECT * FROM tblGrid");
    if (!query.exec()) {
        qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblGrid *temp=new tblGrid();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->StdFlrID=query.value(2).toInt();
            temp->Jt1ID=query.value(3).toInt();
            temp->Jt1ID=query.value(4).toInt();
            temp->AxisID=query.value(5).toInt();
            mytblGrid.push_back(temp);
            qDebug() << "new tblGrid";
            qDebug() << query.value(0);
            qDebug() << query.value(1);
            qDebug() << query.value(2);
            qDebug() << query.value(3);
            qDebug() << query.value(4);
            qDebug() << query.value(5);
        }
    }

    //节点
    query.prepare("SELECT * FROM tblJoint");
    if (!query.exec()) {
        qDebug() << "SQL error: " << query.lastError().text();
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
            qDebug() << "new tblJoint";
            qDebug() << query.value(0);
            qDebug() << query.value(1);
            qDebug() << query.value(2);
            qDebug() << query.value(3);
            qDebug() << query.value(4);
            qDebug() << query.value(5);
        }
    }

    //墙类型
    query.prepare("SELECT * FROM tblWallSect");
    if (!query.exec()) {
        qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblWallSect *temp=new tblWallSect();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->Mat=query.value(2).toInt();
            temp->Kind=query.value(3).toInt();
            temp->B=query.value(4).toInt();
            mytblWallSect.push_back(temp);
            qDebug() << "new tblWallSect";
            qDebug() << query.value(0);
            qDebug() << query.value(1);
            qDebug() << query.value(2);
            qDebug() << query.value(3);
            qDebug() << query.value(4);
        }
    }

    //墙布置
    query.prepare("SELECT * FROM tblWallSeg");
    if (!query.exec()) {
        qDebug() << "SQL error: " << query.lastError().text();
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
            mytblWallSeg.push_back(temp);
            qDebug() << "new tblWallSeg";
            qDebug() << query.value(0);
            qDebug() << query.value(1);
            qDebug() << query.value(2);
            qDebug() << query.value(3);
            qDebug() << query.value(4);
            qDebug() << query.value(5);
            qDebug() << query.value(6);
            qDebug() << query.value(7);
            qDebug() << query.value(8);
        }
    }

    //梁类型
    query.prepare("SELECT * FROM tblBeamSect");
    if (!query.exec()) {
        qDebug() << "SQL error: " << query.lastError().text();
    } else {
        while (query.next()) {
            tblBeamSect *temp=new tblBeamSect();
            temp->ID=query.value(0).toInt();
            temp->No_=query.value(1).toInt();
            temp->Name=query.value(2).toString();
            temp->Mat=query.value(3).toInt();
            temp->Kind=query.value(4).toInt();
            temp->ShapeVal=query.value(5).toInt();
            mytblBeamSect.push_back(temp);
            qDebug() << "new tblBeamSect";
            qDebug() << query.value(0);
            qDebug() << query.value(1);
            qDebug() << query.value(2);
            qDebug() << query.value(3);
            qDebug() << query.value(4);
            qDebug() << query.value(5);
        }
    }

    //梁布置
    query.prepare("SELECT * FROM tblWallSeg");
    if (!query.exec()) {
        qDebug() << "SQL error: " << query.lastError().text();
    } else {



    db.close();
}
