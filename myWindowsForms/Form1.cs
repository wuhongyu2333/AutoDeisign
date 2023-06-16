using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.Geometry;
using Rhino.Collections;
using System.Drawing.Text;
using Rhino.UI;
using System.Threading;

namespace myWindowsForms
{
    public partial class Form1 : Form
    {

        //变量声明
        ObjRef outface;
        ObjRef inface;
        List<double> height = new List<double>();
        double dis = 0;
        double ba = 0;
        int num_col = 0;
        int num_corner = 0;
        List<List<Line>> column = new List<List<Line>>();
        List<List<Line>> beam1 = new List<List<Line>>();
        List<List<Line>> beam2 = new List<List<Line>>();
        List<List<Line>> wall = new List<List<Line>>();
        List<List<Line>> brace1 = new List<List<Line>>();
        List<List<Line>> brace2 = new List<List<Line>>();
        List<List<Guid>> ID_column = new List<List<Guid>>();
        List<List<Guid>> ID_beam1 = new List<List<Guid>>();
        List<List<Guid>> ID_beam2 = new List<List<Guid>>();
        List<List<Guid>> ID_wall = new List<List<Guid>>();
        List<List<Guid>> ID_brace1 = new List<List<Guid>>();
        List<List<Guid>> ID_brace2 = new List<List<Guid>>();

        List<int> waist = new List<int>();
        List<int> segment_arm = new List<int>();
        List<int> type_arm = new List<int>();
        List<int> segment_round = new List<int>();
        List<int> type_round = new List<int>();


        List<int> highlight_all = new List<int>();
        List<int> highlight_flr = new List<int>();
        bool highlight_column = true;
        bool highlight_beam1 = true;
        bool highlight_beam2 = true;
        bool highlight_wall = true;
        bool highlight_brace1 = true;
        bool highlight_brace2 = true;


        public int numflr;
        public List<List<float>> userLoad = new List<List<float>>();

        public Form1()
        {
            numflr = 10;
            List<List<float>> userLoad = new List<List<float>>();
            for (int i = 0; i < numflr; i++)
            {
                List<float> row = new List<float>();
                for (int j = 0; j < 7; j++)
                {
                    row.Add(0);
                }
                userLoad.Add(row);
            }
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle49 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle50 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button36 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.button26 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.panel10 = new System.Windows.Forms.Panel();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.panel9 = new System.Windows.Forms.Panel();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button5 = new System.Windows.Forms.Button();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.button4 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.button14 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel8 = new System.Windows.Forms.Panel();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.textBox35 = new System.Windows.Forms.TextBox();
            this.comboBox13 = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox12 = new System.Windows.Forms.ComboBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.textBox45 = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.textBox50 = new System.Windows.Forms.TextBox();
            this.textBox49 = new System.Windows.Forms.TextBox();
            this.textBox48 = new System.Windows.Forms.TextBox();
            this.textBox47 = new System.Windows.Forms.TextBox();
            this.button27 = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox11 = new System.Windows.Forms.ComboBox();
            this.comboBox10 = new System.Windows.Forms.ComboBox();
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.textBox42 = new System.Windows.Forms.TextBox();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.textBox43 = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label33 = new System.Windows.Forms.Label();
            this.textBox34 = new System.Windows.Forms.TextBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.textBox33 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.textBox32 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox31 = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.textBox30 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.textBox29 = new System.Windows.Forms.TextBox();
            this.textBox28 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.button28 = new System.Windows.Forms.Button();
            this.button29 = new System.Windows.Forms.Button();
            this.panel13 = new System.Windows.Forms.Panel();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panel14 = new System.Windows.Forms.Panel();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.button30 = new System.Windows.Forms.Button();
            this.button31 = new System.Windows.Forms.Button();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.panel15 = new System.Windows.Forms.Panel();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.button32 = new System.Windows.Forms.Button();
            this.button33 = new System.Windows.Forms.Button();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.panel16 = new System.Windows.Forms.Panel();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.button34 = new System.Windows.Forms.Button();
            this.button35 = new System.Windows.Forms.Button();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.panel17 = new System.Windows.Forms.Panel();
            this.dataGridView7 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.button37 = new System.Windows.Forms.Button();
            this.button38 = new System.Windows.Forms.Button();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.panel18 = new System.Windows.Forms.Panel();
            this.dataGridView8 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel17 = new System.Windows.Forms.TableLayoutPanel();
            this.button39 = new System.Windows.Forms.Button();
            this.button40 = new System.Windows.Forms.Button();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.panel19 = new System.Windows.Forms.Panel();
            this.dataGridView9 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.button41 = new System.Windows.Forms.Button();
            this.button42 = new System.Windows.Forms.Button();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.panel20 = new System.Windows.Forms.Panel();
            this.dataGridView10 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel19 = new System.Windows.Forms.TableLayoutPanel();
            this.button43 = new System.Windows.Forms.Button();
            this.button44 = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.panel13.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.tableLayoutPanel13.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            this.tableLayoutPanel14.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            this.tableLayoutPanel15.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.panel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView7)).BeginInit();
            this.tableLayoutPanel16.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.panel18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView8)).BeginInit();
            this.tableLayoutPanel17.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.panel19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView9)).BeginInit();
            this.tableLayoutPanel18.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.panel20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView10)).BeginInit();
            this.tableLayoutPanel19.SuspendLayout();
            this.panel12.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(948, 448);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(115)))), ((int)(((byte)(180)))));
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(940, 415);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "线框模型";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.panel2.Controls.Add(this.button36);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Location = new System.Drawing.Point(720, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 380);
            this.panel2.TabIndex = 17;
            // 
            // button36
            // 
            this.button36.Location = new System.Drawing.Point(11, 345);
            this.button36.Name = "button36";
            this.button36.Size = new System.Drawing.Size(175, 29);
            this.button36.TabIndex = 15;
            this.button36.Text = "下一步";
            this.button36.UseVisualStyleBackColor = true;
            this.button36.Click += new System.EventHandler(this.button36_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panel4);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 203);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "构件高亮";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tableLayoutPanel5);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.button18);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 21);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(194, 179);
            this.panel4.TabIndex = 13;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Controls.Add(this.button26, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.button25, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.button24, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.button23, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.button22, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.button21, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 77);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(194, 102);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // button26
            // 
            this.button26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button26.Image = global::myWindowsForms.Properties.Resources.roof;
            this.button26.Location = new System.Drawing.Point(138, 54);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(45, 45);
            this.button26.TabIndex = 5;
            this.toolTip1.SetToolTip(this.button26, "环带桁架");
            this.button26.UseVisualStyleBackColor = true;
            this.button26.Click += new System.EventHandler(this.button26_Click);
            // 
            // button25
            // 
            this.button25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button25.Image = global::myWindowsForms.Properties.Resources.joist;
            this.button25.Location = new System.Drawing.Point(73, 54);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(45, 45);
            this.button25.TabIndex = 4;
            this.toolTip1.SetToolTip(this.button25, "伸臂桁架");
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new System.EventHandler(this.button25_Click);
            // 
            // button24
            // 
            this.button24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button24.Image = global::myWindowsForms.Properties.Resources.brick_wall;
            this.button24.Location = new System.Drawing.Point(8, 54);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(47, 45);
            this.button24.TabIndex = 3;
            this.toolTip1.SetToolTip(this.button24, "核心筒墙");
            this.button24.UseVisualStyleBackColor = true;
            this.button24.Click += new System.EventHandler(this.button24_Click);
            // 
            // button23
            // 
            this.button23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button23.Image = global::myWindowsForms.Properties.Resources.beam__1_;
            this.button23.Location = new System.Drawing.Point(138, 3);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(45, 45);
            this.button23.TabIndex = 2;
            this.toolTip1.SetToolTip(this.button23, "主梁");
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // button22
            // 
            this.button22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button22.Image = global::myWindowsForms.Properties.Resources.beam;
            this.button22.Location = new System.Drawing.Point(73, 3);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(45, 45);
            this.button22.TabIndex = 1;
            this.toolTip1.SetToolTip(this.button22, "圈梁");
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // button21
            // 
            this.button21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button21.BackColor = System.Drawing.Color.Transparent;
            this.button21.Image = global::myWindowsForms.Properties.Resources.architecture;
            this.button21.Location = new System.Drawing.Point(9, 3);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(45, 45);
            this.button21.TabIndex = 0;
            this.toolTip1.SetToolTip(this.button21, "外框柱");
            this.button21.UseVisualStyleBackColor = false;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.comboBox1);
            this.panel7.Controls.Add(this.button19);
            this.panel7.Controls.Add(this.button20);
            this.panel7.Location = new System.Drawing.Point(73, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(121, 57);
            this.panel7.TabIndex = 14;
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.Items.AddRange(new object[] {
            "楼层选择"});
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button19
            // 
            this.button19.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button19.ForeColor = System.Drawing.Color.Black;
            this.button19.Image = global::myWindowsForms.Properties.Resources.up_arrow;
            this.button19.Location = new System.Drawing.Point(0, 32);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(60, 25);
            this.button19.TabIndex = 11;
            this.button19.Text = "上层";
            this.button19.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button20
            // 
            this.button20.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button20.ForeColor = System.Drawing.Color.Black;
            this.button20.Image = global::myWindowsForms.Properties.Resources.down_arrow;
            this.button20.Location = new System.Drawing.Point(61, 32);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(60, 25);
            this.button20.TabIndex = 12;
            this.button20.Text = "下层";
            this.button20.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button18
            // 
            this.button18.Image = global::myWindowsForms.Properties.Resources.city_buildings;
            this.button18.Location = new System.Drawing.Point(0, 0);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(58, 58);
            this.button18.TabIndex = 9;
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Location = new System.Drawing.Point(340, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 380);
            this.panel1.TabIndex = 16;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel4);
            this.groupBox3.Controls.Add(this.dataGridView3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(0, 172);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(360, 208);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "腰部桁架";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Controls.Add(this.button15, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.button16, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.button17, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 171);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(354, 34);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // button15
            // 
            this.button15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button15.Image = global::myWindowsForms.Properties.Resources.add;
            this.button15.Location = new System.Drawing.Point(3, 3);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(112, 28);
            this.button15.TabIndex = 0;
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button16.Image = global::myWindowsForms.Properties.Resources.delete;
            this.button16.Location = new System.Drawing.Point(121, 3);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(112, 28);
            this.button16.TabIndex = 1;
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button17
            // 
            this.button17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button17.Image = global::myWindowsForms.Properties.Resources.check;
            this.button17.Location = new System.Drawing.Point(239, 3);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(112, 28);
            this.button17.TabIndex = 2;
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView3.ColumnHeadersHeight = 30;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Column13,
            this.Column15,
            this.Column14,
            this.Column16});
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView3.Location = new System.Drawing.Point(3, 19);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowHeadersWidth = 15;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dataGridView3.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView3.RowTemplate.Height = 20;
            this.dataGridView3.Size = new System.Drawing.Size(354, 146);
            this.dataGridView3.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn1.FillWeight = 90F;
            this.dataGridViewTextBoxColumn1.HeaderText = "起始层";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 90F;
            this.dataGridViewTextBoxColumn2.HeaderText = "终止层";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 60;
            // 
            // Column13
            // 
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.Column13.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column13.HeaderText = "伸臂桁架";
            this.Column13.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.Column13.Name = "Column13";
            this.Column13.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column13.Width = 70;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "段数";
            this.Column15.Name = "Column15";
            this.Column15.Width = 45;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "环带桁架";
            this.Column14.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.Column14.Name = "Column14";
            this.Column14.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column14.Width = 70;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "段数";
            this.Column16.Name = "Column16";
            this.Column16.Width = 45;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.textBox9, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBox8, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel11, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel10, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel9, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBox4, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBox3, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBox2, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(348, 140);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // textBox9
            // 
            this.textBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox9.Location = new System.Drawing.Point(142, 108);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(11, 16);
            this.textBox9.TabIndex = 22;
            this.textBox9.Text = "0";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox8
            // 
            this.textBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.Location = new System.Drawing.Point(142, 61);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(11, 16);
            this.textBox8.TabIndex = 21;
            this.textBox8.Text = "0";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.textBox7);
            this.panel11.Controls.Add(this.trackBar4);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(159, 95);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(115, 42);
            this.panel11.TabIndex = 19;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox7.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(62)))), ((int)(((byte)(151)))));
            this.textBox7.Location = new System.Drawing.Point(0, 28);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(115, 14);
            this.textBox7.TabIndex = 7;
            this.textBox7.Text = "0";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // trackBar4
            // 
            this.trackBar4.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar4.Location = new System.Drawing.Point(0, 0);
            this.trackBar4.Maximum = 10000;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Size = new System.Drawing.Size(115, 45);
            this.trackBar4.TabIndex = 6;
            this.trackBar4.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar4.Scroll += new System.EventHandler(this.trackBar4_Scroll);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.textBox6);
            this.panel10.Controls.Add(this.trackBar2);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(159, 49);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(115, 40);
            this.panel10.TabIndex = 18;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox6.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(62)))), ((int)(((byte)(151)))));
            this.textBox6.Location = new System.Drawing.Point(0, 26);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(115, 14);
            this.textBox6.TabIndex = 7;
            this.textBox6.Text = "0";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // trackBar2
            // 
            this.trackBar2.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar2.Location = new System.Drawing.Point(0, 0);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(115, 45);
            this.trackBar2.TabIndex = 6;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.textBox5);
            this.panel9.Controls.Add(this.trackBar1);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(159, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(115, 40);
            this.panel9.TabIndex = 17;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox5.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(62)))), ((int)(((byte)(151)))));
            this.textBox5.Location = new System.Drawing.Point(0, 26);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(115, 14);
            this.textBox5.TabIndex = 7;
            this.textBox5.Text = "0";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // trackBar1
            // 
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar1.Location = new System.Drawing.Point(0, 0);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(115, 45);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 46);
            this.label1.TabIndex = 1;
            this.label1.Text = "核心筒柱数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 46);
            this.label2.TabIndex = 2;
            this.label2.Text = "角柱数";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(3, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 48);
            this.label3.TabIndex = 3;
            this.label3.Text = "基底标高(mm)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.Location = new System.Drawing.Point(280, 104);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(65, 23);
            this.textBox4.TabIndex = 13;
            this.textBox4.Text = "10000";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(280, 57);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(65, 23);
            this.textBox3.TabIndex = 12;
            this.textBox3.Text = "10";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(280, 11);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(65, 23);
            this.textBox2.TabIndex = 11;
            this.textBox2.Text = "20";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(142, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(11, 16);
            this.textBox1.TabIndex = 20;
            this.textBox1.Text = "0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel3.Location = new System.Drawing.Point(20, 20);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(300, 380);
            this.panel3.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(0, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 229);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "层高表";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.button6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button7, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button8, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 192);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(294, 34);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // button6
            // 
            this.button6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button6.Image = global::myWindowsForms.Properties.Resources.add;
            this.button6.Location = new System.Drawing.Point(3, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(91, 28);
            this.button6.TabIndex = 0;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button7.Image = global::myWindowsForms.Properties.Resources.delete;
            this.button7.Location = new System.Drawing.Point(100, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(91, 28);
            this.button7.TabIndex = 1;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button8.Image = global::myWindowsForms.Properties.Resources.check;
            this.button8.Location = new System.Drawing.Point(197, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(94, 28);
            this.button8.TabIndex = 2;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView2.ColumnHeadersHeight = 30;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column10,
            this.Column11,
            this.Column12});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView2.Location = new System.Drawing.Point(3, 21);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 15;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView2.RowTemplate.Height = 30;
            this.dataGridView2.Size = new System.Drawing.Size(294, 165);
            this.dataGridView2.TabIndex = 1;
            // 
            // Column10
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.Column10.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column10.FillWeight = 90F;
            this.Column10.HeaderText = "起始层";
            this.Column10.Name = "Column10";
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column10.Width = 93;
            // 
            // Column11
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            this.Column11.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column11.FillWeight = 90F;
            this.Column11.HeaderText = "终止层";
            this.Column11.Name = "Column11";
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column11.Width = 93;
            // 
            // Column12
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            this.Column12.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column12.HeaderText = "层高(mm)";
            this.Column12.Name = "Column12";
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column12.Width = 105;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.button5.ContextMenuStrip = this.contextMenuStrip2;
            this.button5.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Image = global::myWindowsForms.Properties.Resources.cube;
            this.button5.Location = new System.Drawing.Point(153, 91);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(140, 40);
            this.button5.TabIndex = 2;
            this.button5.Text = "选择核心筒";
            this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.button5, "核心筒");
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 26);
            this.contextMenuStrip2.Text = "清空";
            this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem2.Text = "取消选中";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.button4.ContextMenuStrip = this.contextMenuStrip1;
            this.button4.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Image = global::myWindowsForms.Properties.Resources.burj_khalifa;
            this.button4.Location = new System.Drawing.Point(6, 91);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(140, 40);
            this.button4.TabIndex = 1;
            this.button4.Text = "选择外立面";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.button4, "外立面");
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            this.contextMenuStrip1.Text = "清空";
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem1.Text = "取消选中";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 73);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "超高层结构体系";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.Controls.Add(this.button14, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.button9, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.button10, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.button13, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.button11, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.button12, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(294, 49);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // button14
            // 
            this.button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button14.Image = global::myWindowsForms.Properties.Resources._6;
            this.button14.Location = new System.Drawing.Point(248, 3);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(42, 43);
            this.button14.TabIndex = 5;
            this.toolTip1.SetToolTip(this.button14, "巨型结构");
            this.button14.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Image = global::myWindowsForms.Properties.Resources._1;
            this.button9.Location = new System.Drawing.Point(3, 3);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(43, 43);
            this.button9.TabIndex = 0;
            this.toolTip1.SetToolTip(this.button9, "框架核心筒");
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button10.Image = global::myWindowsForms.Properties.Resources._2;
            this.button10.Location = new System.Drawing.Point(52, 3);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(42, 43);
            this.button10.TabIndex = 1;
            this.toolTip1.SetToolTip(this.button10, "框架核心筒+伸臂桁架");
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button13.Image = global::myWindowsForms.Properties.Resources._5;
            this.button13.Location = new System.Drawing.Point(199, 3);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(42, 43);
            this.button13.TabIndex = 4;
            this.toolTip1.SetToolTip(this.button13, "外周斜交网筒+内筒");
            this.button13.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button11.Image = global::myWindowsForms.Properties.Resources._3;
            this.button11.Location = new System.Drawing.Point(101, 3);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(42, 43);
            this.button11.TabIndex = 2;
            this.toolTip1.SetToolTip(this.button11, "外周框筒+内筒");
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button12.Image = global::myWindowsForms.Properties.Resources._4;
            this.button12.Location = new System.Drawing.Point(150, 3);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(42, 43);
            this.button12.TabIndex = 3;
            this.toolTip1.SetToolTip(this.button12, "外周支撑筒+内筒");
            this.button12.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.tableLayoutPanel6);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(940, 415);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "荷载施加";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel6.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.button3, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(511, 376);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(426, 36);
            this.tableLayoutPanel6.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.button1.Image = global::myWindowsForms.Properties.Resources.add;
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 30);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.button3.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(257, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(166, 30);
            this.button3.TabIndex = 4;
            this.button3.Text = "下一步";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.button2.Image = global::myWindowsForms.Properties.Resources.delete;
            this.button2.Location = new System.Drawing.Point(130, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 30);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(115)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.GridColor = System.Drawing.Color.Black;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 20;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle22;
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.Size = new System.Drawing.Size(934, 370);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // Column1
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle12;
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "起始层";
            this.Column1.Name = "Column1";
            this.Column1.Width = 90;
            // 
            // Column2
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle13;
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "终止层";
            this.Column2.Name = "Column2";
            this.Column2.Width = 90;
            // 
            // Column3
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle14;
            this.Column3.HeaderText = "核心筒内恒载(KPa)";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle15;
            this.Column4.HeaderText = "核心筒内活载(KPa)";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle16;
            this.Column5.HeaderText = "核心筒内板厚(mm)";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle17;
            this.Column6.HeaderText = "核心筒外恒载(KPa)";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle18;
            this.Column7.HeaderText = "核心筒外活载(KPa)";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle19;
            this.Column8.HeaderText = "核心筒外板厚(mm)";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle20;
            this.Column9.HeaderText = "框线荷载(KPa)";
            this.Column9.Name = "Column9";
            this.Column9.Width = 151;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(115)))), ((int)(((byte)(180)))));
            this.tabPage3.Controls.Add(this.panel8);
            this.tabPage3.Controls.Add(this.button27);
            this.tabPage3.Controls.Add(this.panel6);
            this.tabPage3.Controls.Add(this.panel5);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(940, 415);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "前处理参数设置";
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // panel8
            // 
            this.panel8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.panel8.Controls.Add(this.groupBox8);
            this.panel8.Controls.Add(this.groupBox7);
            this.panel8.Location = new System.Drawing.Point(372, 20);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(254, 375);
            this.panel8.TabIndex = 21;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.tableLayoutPanel10);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox8.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox8.ForeColor = System.Drawing.Color.White;
            this.groupBox8.Location = new System.Drawing.Point(0, 281);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(254, 94);
            this.groupBox8.TabIndex = 1;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "风荷载信息";
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel10.Controls.Add(this.label34, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.label35, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.textBox35, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.comboBox13, 1, 1);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(248, 70);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label34.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label34.ForeColor = System.Drawing.Color.Black;
            this.label34.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label34.Location = new System.Drawing.Point(2, 0);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(144, 35);
            this.label34.TabIndex = 22;
            this.label34.Text = "基本风压";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label35.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label35.ForeColor = System.Drawing.Color.Black;
            this.label35.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label35.Location = new System.Drawing.Point(2, 35);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(144, 35);
            this.label35.TabIndex = 24;
            this.label35.Text = "地面粗糙度类别 ";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox35
            // 
            this.textBox35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox35.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox35.Location = new System.Drawing.Point(150, 4);
            this.textBox35.Margin = new System.Windows.Forms.Padding(2);
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new System.Drawing.Size(96, 26);
            this.textBox35.TabIndex = 23;
            this.textBox35.Text = "0.55";
            // 
            // comboBox13
            // 
            this.comboBox13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox13.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox13.FormattingEnabled = true;
            this.comboBox13.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D"});
            this.comboBox13.Location = new System.Drawing.Point(150, 40);
            this.comboBox13.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox13.Name = "comboBox13";
            this.comboBox13.Size = new System.Drawing.Size(96, 24);
            this.comboBox13.TabIndex = 66;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tableLayoutPanel9);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox7.ForeColor = System.Drawing.Color.White;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(254, 235);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "材料参数";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel9.Controls.Add(this.comboBox12, 1, 1);
            this.tableLayoutPanel9.Controls.Add(this.label50, 0, 5);
            this.tableLayoutPanel9.Controls.Add(this.label45, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.label49, 0, 4);
            this.tableLayoutPanel9.Controls.Add(this.label46, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.label48, 0, 3);
            this.tableLayoutPanel9.Controls.Add(this.textBox45, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.label47, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this.textBox50, 1, 5);
            this.tableLayoutPanel9.Controls.Add(this.textBox49, 1, 4);
            this.tableLayoutPanel9.Controls.Add(this.textBox48, 1, 3);
            this.tableLayoutPanel9.Controls.Add(this.textBox47, 1, 2);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 6;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(248, 211);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // comboBox12
            // 
            this.comboBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox12.FormattingEnabled = true;
            this.comboBox12.Items.AddRange(new object[] {
            "235",
            "345",
            "390",
            "420"});
            this.comboBox12.Location = new System.Drawing.Point(150, 40);
            this.comboBox12.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox12.Name = "comboBox12";
            this.comboBox12.Size = new System.Drawing.Size(96, 24);
            this.comboBox12.TabIndex = 71;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label50.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label50.ForeColor = System.Drawing.Color.Black;
            this.label50.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label50.Location = new System.Drawing.Point(2, 175);
            this.label50.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(144, 36);
            this.label50.TabIndex = 67;
            this.label50.Text = "柱箍筋类别 ";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label45.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label45.ForeColor = System.Drawing.Color.Black;
            this.label45.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label45.Location = new System.Drawing.Point(2, 0);
            this.label45.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(144, 35);
            this.label45.TabIndex = 48;
            this.label45.Text = "砼容重(kN/m3)";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label49.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label49.ForeColor = System.Drawing.Color.Black;
            this.label49.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label49.Location = new System.Drawing.Point(2, 140);
            this.label49.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(144, 35);
            this.label49.TabIndex = 66;
            this.label49.Text = "梁箍筋类别 ";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label46.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label46.ForeColor = System.Drawing.Color.Black;
            this.label46.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label46.Location = new System.Drawing.Point(2, 35);
            this.label46.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(144, 35);
            this.label46.TabIndex = 50;
            this.label46.Text = "钢构件钢材";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label48.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label48.ForeColor = System.Drawing.Color.Black;
            this.label48.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label48.Location = new System.Drawing.Point(2, 105);
            this.label48.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(144, 35);
            this.label48.TabIndex = 65;
            this.label48.Text = "钢截面净毛面积比";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox45
            // 
            this.textBox45.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox45.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox45.Location = new System.Drawing.Point(150, 4);
            this.textBox45.Margin = new System.Windows.Forms.Padding(2);
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new System.Drawing.Size(96, 26);
            this.textBox45.TabIndex = 66;
            this.textBox45.Text = "26";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label47.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label47.ForeColor = System.Drawing.Color.Black;
            this.label47.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label47.Location = new System.Drawing.Point(2, 70);
            this.label47.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(144, 35);
            this.label47.TabIndex = 64;
            this.label47.Text = "钢材容重(kN/m3)";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox50
            // 
            this.textBox50.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox50.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox50.Location = new System.Drawing.Point(150, 180);
            this.textBox50.Margin = new System.Windows.Forms.Padding(2);
            this.textBox50.Name = "textBox50";
            this.textBox50.Size = new System.Drawing.Size(96, 26);
            this.textBox50.TabIndex = 70;
            // 
            // textBox49
            // 
            this.textBox49.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox49.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox49.Location = new System.Drawing.Point(150, 144);
            this.textBox49.Margin = new System.Windows.Forms.Padding(2);
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new System.Drawing.Size(96, 26);
            this.textBox49.TabIndex = 69;
            // 
            // textBox48
            // 
            this.textBox48.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox48.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox48.Location = new System.Drawing.Point(150, 109);
            this.textBox48.Margin = new System.Windows.Forms.Padding(2);
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new System.Drawing.Size(96, 26);
            this.textBox48.TabIndex = 68;
            this.textBox48.Text = "0.9";
            // 
            // textBox47
            // 
            this.textBox47.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox47.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox47.Location = new System.Drawing.Point(150, 74);
            this.textBox47.Margin = new System.Windows.Forms.Padding(2);
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new System.Drawing.Size(96, 26);
            this.textBox47.TabIndex = 67;
            this.textBox47.Text = "83.5";
            // 
            // button27
            // 
            this.button27.Location = new System.Drawing.Point(782, 371);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(150, 29);
            this.button27.TabIndex = 20;
            this.button27.Text = "下一步";
            this.button27.UseVisualStyleBackColor = true;
            this.button27.Click += new System.EventHandler(this.button27_Click);
            // 
            // panel6
            // 
            this.panel6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.panel6.Controls.Add(this.groupBox6);
            this.panel6.Location = new System.Drawing.Point(646, 20);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(276, 340);
            this.panel6.TabIndex = 19;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tableLayoutPanel8);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox6.ForeColor = System.Drawing.Color.White;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(276, 340);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "地震信息";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel8.Controls.Add(this.comboBox11, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.comboBox10, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.comboBox9, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.comboBox8, 1, 3);
            this.tableLayoutPanel8.Controls.Add(this.textBox42, 1, 6);
            this.tableLayoutPanel8.Controls.Add(this.comboBox7, 1, 4);
            this.tableLayoutPanel8.Controls.Add(this.comboBox6, 1, 5);
            this.tableLayoutPanel8.Controls.Add(this.textBox43, 1, 7);
            this.tableLayoutPanel8.Controls.Add(this.label36, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.label44, 0, 8);
            this.tableLayoutPanel8.Controls.Add(this.label43, 0, 7);
            this.tableLayoutPanel8.Controls.Add(this.label42, 0, 6);
            this.tableLayoutPanel8.Controls.Add(this.label41, 0, 5);
            this.tableLayoutPanel8.Controls.Add(this.label40, 0, 4);
            this.tableLayoutPanel8.Controls.Add(this.label39, 0, 3);
            this.tableLayoutPanel8.Controls.Add(this.label38, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.label37, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.comboBox5, 1, 8);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 9;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(270, 316);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // comboBox11
            // 
            this.comboBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox11.FormattingEnabled = true;
            this.comboBox11.Items.AddRange(new object[] {
            "第一组",
            "第二组",
            "第三组"});
            this.comboBox11.Location = new System.Drawing.Point(191, 5);
            this.comboBox11.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox11.Name = "comboBox11";
            this.comboBox11.Size = new System.Drawing.Size(77, 24);
            this.comboBox11.TabIndex = 81;
            // 
            // comboBox10
            // 
            this.comboBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox10.FormattingEnabled = true;
            this.comboBox10.Items.AddRange(new object[] {
            "6(0.05g)",
            "7(0.10g)",
            "7(0.15g)",
            "8(0.20g)",
            "8(0.30g)",
            "9(0.40g)"});
            this.comboBox10.Location = new System.Drawing.Point(191, 40);
            this.comboBox10.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox10.Name = "comboBox10";
            this.comboBox10.Size = new System.Drawing.Size(77, 24);
            this.comboBox10.TabIndex = 80;
            // 
            // comboBox9
            // 
            this.comboBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.Items.AddRange(new object[] {
            "Ⅰ0",
            "Ⅰ1",
            "Ⅱ",
            "Ⅲ",
            "Ⅳ",
            "上海专用"});
            this.comboBox9.Location = new System.Drawing.Point(191, 75);
            this.comboBox9.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.Size = new System.Drawing.Size(77, 24);
            this.comboBox9.TabIndex = 79;
            // 
            // comboBox8
            // 
            this.comboBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Items.AddRange(new object[] {
            "0 特级",
            "1 一级",
            "2 二级",
            "3 三级",
            "4 四级",
            "5 不考虑"});
            this.comboBox8.Location = new System.Drawing.Point(191, 110);
            this.comboBox8.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.Size = new System.Drawing.Size(77, 24);
            this.comboBox8.TabIndex = 78;
            // 
            // textBox42
            // 
            this.textBox42.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox42.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox42.Location = new System.Drawing.Point(191, 214);
            this.textBox42.Margin = new System.Windows.Forms.Padding(2);
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new System.Drawing.Size(77, 26);
            this.textBox42.TabIndex = 73;
            this.textBox42.Text = "40";
            // 
            // comboBox7
            // 
            this.comboBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Items.AddRange(new object[] {
            "0 特级",
            "1 一级",
            "2 二级",
            "3 三级",
            "4 四级",
            "5 不考虑"});
            this.comboBox7.Location = new System.Drawing.Point(191, 145);
            this.comboBox7.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(77, 24);
            this.comboBox7.TabIndex = 77;
            // 
            // comboBox6
            // 
            this.comboBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Items.AddRange(new object[] {
            "0 特级",
            "1 一级",
            "2 二级",
            "3 三级",
            "4 四级",
            "5 不考虑"});
            this.comboBox6.Location = new System.Drawing.Point(191, 180);
            this.comboBox6.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(77, 24);
            this.comboBox6.TabIndex = 76;
            // 
            // textBox43
            // 
            this.textBox43.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox43.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox43.Location = new System.Drawing.Point(191, 249);
            this.textBox43.Margin = new System.Windows.Forms.Padding(2);
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new System.Drawing.Size(77, 26);
            this.textBox43.TabIndex = 74;
            this.textBox43.Text = "0.9";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label36.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label36.ForeColor = System.Drawing.Color.Black;
            this.label36.Location = new System.Drawing.Point(2, 0);
            this.label36.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(185, 35);
            this.label36.TabIndex = 45;
            this.label36.Text = "设计地震分组 ";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label44.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label44.ForeColor = System.Drawing.Color.Black;
            this.label44.Location = new System.Drawing.Point(2, 280);
            this.label44.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(185, 36);
            this.label44.TabIndex = 53;
            this.label44.Text = "抗震构造措施的抗震等级";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label43.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label43.ForeColor = System.Drawing.Color.Black;
            this.label43.Location = new System.Drawing.Point(2, 245);
            this.label43.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(185, 35);
            this.label43.TabIndex = 52;
            this.label43.Text = "周期折减系数";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label42.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label42.ForeColor = System.Drawing.Color.Black;
            this.label42.Location = new System.Drawing.Point(2, 210);
            this.label42.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(185, 35);
            this.label42.TabIndex = 51;
            this.label42.Text = "计算振型个数";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label41.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label41.ForeColor = System.Drawing.Color.Black;
            this.label41.Location = new System.Drawing.Point(2, 175);
            this.label41.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(185, 35);
            this.label41.TabIndex = 50;
            this.label41.Text = "剪力墙抗震等级 ";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label40.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label40.ForeColor = System.Drawing.Color.Black;
            this.label40.Location = new System.Drawing.Point(2, 140);
            this.label40.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(185, 35);
            this.label40.TabIndex = 49;
            this.label40.Text = "钢框架抗震等级";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label39.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label39.ForeColor = System.Drawing.Color.Black;
            this.label39.Location = new System.Drawing.Point(2, 105);
            this.label39.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(185, 35);
            this.label39.TabIndex = 48;
            this.label39.Text = "混凝土框架抗震等级";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label38.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(2, 70);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(185, 35);
            this.label38.TabIndex = 47;
            this.label38.Text = "场地类别  ";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label37.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label37.ForeColor = System.Drawing.Color.Black;
            this.label37.Location = new System.Drawing.Point(2, 35);
            this.label37.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(185, 35);
            this.label37.TabIndex = 46;
            this.label37.Text = "地震烈度 ";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox5
            // 
            this.comboBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "提供二级",
            "提供一级",
            "不改变",
            "降低一级",
            "降低二级"});
            this.comboBox5.Location = new System.Drawing.Point(191, 286);
            this.comboBox5.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(77, 24);
            this.comboBox5.TabIndex = 75;
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.panel5.Controls.Add(this.groupBox5);
            this.panel5.Location = new System.Drawing.Point(20, 20);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(332, 375);
            this.panel5.TabIndex = 18;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel7);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(332, 375);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "结构参数";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel7.Controls.Add(this.label33, 0, 9);
            this.tableLayoutPanel7.Controls.Add(this.textBox34, 1, 9);
            this.tableLayoutPanel7.Controls.Add(this.comboBox3, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.textBox33, 1, 8);
            this.tableLayoutPanel7.Controls.Add(this.label24, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.textBox32, 1, 7);
            this.tableLayoutPanel7.Controls.Add(this.comboBox2, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.textBox31, 1, 6);
            this.tableLayoutPanel7.Controls.Add(this.label32, 0, 8);
            this.tableLayoutPanel7.Controls.Add(this.textBox30, 1, 5);
            this.tableLayoutPanel7.Controls.Add(this.label25, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.textBox29, 1, 4);
            this.tableLayoutPanel7.Controls.Add(this.textBox28, 1, 3);
            this.tableLayoutPanel7.Controls.Add(this.label26, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.label28, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.label27, 0, 4);
            this.tableLayoutPanel7.Controls.Add(this.label29, 0, 5);
            this.tableLayoutPanel7.Controls.Add(this.label30, 0, 6);
            this.tableLayoutPanel7.Controls.Add(this.label31, 0, 7);
            this.tableLayoutPanel7.Controls.Add(this.comboBox4, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 10;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(326, 351);
            this.tableLayoutPanel7.TabIndex = 86;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label33.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label33.ForeColor = System.Drawing.Color.Black;
            this.label33.Location = new System.Drawing.Point(2, 315);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(191, 36);
            this.label33.TabIndex = 96;
            this.label33.Text = "年限的活荷载调整系数";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox34
            // 
            this.textBox34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox34.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox34.Location = new System.Drawing.Point(197, 320);
            this.textBox34.Margin = new System.Windows.Forms.Padding(2);
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new System.Drawing.Size(127, 26);
            this.textBox34.TabIndex = 82;
            this.textBox34.Text = "1";
            // 
            // comboBox3
            // 
            this.comboBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "1.1",
            "1.0",
            "0.9"});
            this.comboBox3.Location = new System.Drawing.Point(197, 75);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(127, 24);
            this.comboBox3.TabIndex = 85;
            // 
            // textBox33
            // 
            this.textBox33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox33.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox33.Location = new System.Drawing.Point(197, 284);
            this.textBox33.Margin = new System.Windows.Forms.Padding(2);
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new System.Drawing.Size(127, 26);
            this.textBox33.TabIndex = 80;
            this.textBox33.Text = "0.85";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(2, 0);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(191, 35);
            this.label24.TabIndex = 87;
            this.label24.Text = "结构体系";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox32
            // 
            this.textBox32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox32.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox32.Location = new System.Drawing.Point(197, 249);
            this.textBox32.Margin = new System.Windows.Forms.Padding(2);
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new System.Drawing.Size(127, 26);
            this.textBox32.TabIndex = 78;
            this.textBox32.Text = "20";
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "钢筋混凝土结构",
            "钢与砼混合结构",
            "钢结构"});
            this.comboBox2.Location = new System.Drawing.Point(197, 40);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(127, 24);
            this.comboBox2.TabIndex = 84;
            // 
            // textBox31
            // 
            this.textBox31.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox31.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox31.Location = new System.Drawing.Point(197, 214);
            this.textBox31.Margin = new System.Windows.Forms.Padding(2);
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new System.Drawing.Size(127, 26);
            this.textBox31.TabIndex = 76;
            this.textBox31.Text = "20";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label32.ForeColor = System.Drawing.Color.Black;
            this.label32.Location = new System.Drawing.Point(2, 280);
            this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(191, 35);
            this.label32.TabIndex = 95;
            this.label32.Text = "框架梁端负弯矩调幅系数";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox30
            // 
            this.textBox30.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox30.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox30.Location = new System.Drawing.Point(197, 179);
            this.textBox30.Margin = new System.Windows.Forms.Padding(2);
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new System.Drawing.Size(127, 26);
            this.textBox30.TabIndex = 74;
            this.textBox30.Text = "0";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label25.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(2, 35);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(191, 35);
            this.label25.TabIndex = 88;
            this.label25.Text = "结构主材";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox29
            // 
            this.textBox29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox29.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox29.Location = new System.Drawing.Point(197, 144);
            this.textBox29.Margin = new System.Windows.Forms.Padding(2);
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new System.Drawing.Size(127, 26);
            this.textBox29.TabIndex = 70;
            this.textBox29.Text = "0";
            // 
            // textBox28
            // 
            this.textBox28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox28.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox28.Location = new System.Drawing.Point(197, 109);
            this.textBox28.Margin = new System.Windows.Forms.Padding(2);
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new System.Drawing.Size(127, 26);
            this.textBox28.TabIndex = 73;
            this.textBox28.Text = "0";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(2, 70);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(191, 35);
            this.label26.TabIndex = 89;
            this.label26.Text = "结构重要性系数";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label28.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(2, 105);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(191, 35);
            this.label28.TabIndex = 90;
            this.label28.Text = "底框层数 ";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(2, 140);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(191, 35);
            this.label27.TabIndex = 91;
            this.label27.Text = "地下室层数";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.ForeColor = System.Drawing.Color.Black;
            this.label29.Location = new System.Drawing.Point(2, 175);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(191, 35);
            this.label29.TabIndex = 92;
            this.label29.Text = "与基础相连最大底标(mm)";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label30.ForeColor = System.Drawing.Color.Black;
            this.label30.Location = new System.Drawing.Point(2, 210);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(191, 35);
            this.label30.TabIndex = 93;
            this.label30.Text = "梁钢筋保护层厚度(mm)";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.Location = new System.Drawing.Point(2, 245);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(191, 35);
            this.label31.TabIndex = 94;
            this.label31.Text = "柱钢筋保护层厚度(mm)";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox4
            // 
            this.comboBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "框架结构",
            "框剪结构",
            "框筒结构",
            "筒中筒结构"});
            this.comboBox4.Location = new System.Drawing.Point(197, 5);
            this.comboBox4.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(127, 24);
            this.comboBox4.TabIndex = 67;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(115)))), ((int)(((byte)(180)))));
            this.tabPage4.Controls.Add(this.tableLayoutPanel12);
            this.tabPage4.Controls.Add(this.panel13);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(940, 415);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "截面初始化";
            this.tabPage4.Click += new System.EventHandler(this.tabPage4_Click);
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 5;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel12.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.textBox10, 1, 0);
            this.tableLayoutPanel12.Controls.Add(this.button28, 2, 0);
            this.tableLayoutPanel12.Controls.Add(this.button29, 4, 0);
            this.tableLayoutPanel12.Location = new System.Drawing.Point(20, 361);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 1;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(900, 36);
            this.tableLayoutPanel12.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 36);
            this.label4.TabIndex = 20;
            this.label4.Text = "工作路径";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox10
            // 
            this.textBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(115)))), ((int)(((byte)(180)))));
            this.textBox10.ForeColor = System.Drawing.Color.White;
            this.textBox10.Location = new System.Drawing.Point(93, 5);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(264, 26);
            this.textBox10.TabIndex = 21;
            // 
            // button28
            // 
            this.button28.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.button28.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button28.Location = new System.Drawing.Point(363, 3);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(84, 30);
            this.button28.TabIndex = 22;
            this.button28.Text = "打开";
            this.button28.UseVisualStyleBackColor = false;
            this.button28.Click += new System.EventHandler(this.button28_Click);
            // 
            // button29
            // 
            this.button29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button29.Location = new System.Drawing.Point(723, 3);
            this.button29.Name = "button29";
            this.button29.Size = new System.Drawing.Size(174, 29);
            this.button29.TabIndex = 23;
            this.button29.Text = "导入PKPM";
            this.button29.UseVisualStyleBackColor = true;
            // 
            // panel13
            // 
            this.panel13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.panel13.Controls.Add(this.groupBox10);
            this.panel13.Controls.Add(this.panel12);
            this.panel13.Location = new System.Drawing.Point(20, 20);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(900, 335);
            this.panel13.TabIndex = 19;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.tabControl2);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox10.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox10.ForeColor = System.Drawing.Color.White;
            this.groupBox10.Location = new System.Drawing.Point(0, 70);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(900, 265);
            this.groupBox10.TabIndex = 0;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "人工修改";
            // 
            // tabControl2
            // 
            this.tabControl2.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Controls.Add(this.tabPage8);
            this.tabControl2.Controls.Add(this.tabPage9);
            this.tabControl2.Controls.Add(this.tabPage10);
            this.tabControl2.Controls.Add(this.tabPage11);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl2.ItemSize = new System.Drawing.Size(90, 27);
            this.tabControl2.Location = new System.Drawing.Point(3, 21);
            this.tabControl2.Multiline = true;
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.Padding = new System.Drawing.Point(8, 4);
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(894, 241);
            this.tabControl2.TabIndex = 20;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.tabPage5.Controls.Add(this.panel14);
            this.tabPage5.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage5.Location = new System.Drawing.Point(4, 31);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(886, 206);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "核心筒外墙";
            this.tabPage5.Click += new System.EventHandler(this.tabPage5_Click);
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.dataGridView4);
            this.panel14.Controls.Add(this.tableLayoutPanel13);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel14.Location = new System.Drawing.Point(3, 3);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(302, 200);
            this.panel14.TabIndex = 2;
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.dataGridView4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView4.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11});
            this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView4.Location = new System.Drawing.Point(0, 0);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowHeadersVisible = false;
            this.dataGridView4.RowTemplate.Height = 23;
            this.dataGridView4.Size = new System.Drawing.Size(302, 161);
            this.dataGridView4.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle24.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle24;
            this.dataGridViewTextBoxColumn9.HeaderText = "起始层";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle25.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle25;
            this.dataGridViewTextBoxColumn10.HeaderText = "终止层";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle26.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle26;
            this.dataGridViewTextBoxColumn11.HeaderText = "墙厚(mm)";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 2;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel13.Controls.Add(this.button30, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.button31, 1, 0);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(0, 167);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(302, 33);
            this.tableLayoutPanel13.TabIndex = 4;
            // 
            // button30
            // 
            this.button30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button30.Image = global::myWindowsForms.Properties.Resources.add;
            this.button30.Location = new System.Drawing.Point(3, 3);
            this.button30.Name = "button30";
            this.button30.Size = new System.Drawing.Size(145, 27);
            this.button30.TabIndex = 0;
            this.button30.UseVisualStyleBackColor = true;
            this.button30.Click += new System.EventHandler(this.button30_Click_1);
            // 
            // button31
            // 
            this.button31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button31.Image = global::myWindowsForms.Properties.Resources.delete;
            this.button31.Location = new System.Drawing.Point(154, 3);
            this.button31.Name = "button31";
            this.button31.Size = new System.Drawing.Size(145, 27);
            this.button31.TabIndex = 1;
            this.button31.UseVisualStyleBackColor = true;
            this.button31.Click += new System.EventHandler(this.button31_Click_1);
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.tabPage6.Controls.Add(this.panel15);
            this.tabPage6.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage6.Location = new System.Drawing.Point(4, 31);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(886, 206);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "核心筒内墙";
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.dataGridView5);
            this.panel15.Controls.Add(this.tableLayoutPanel14);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel15.Location = new System.Drawing.Point(3, 3);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(302, 200);
            this.panel15.TabIndex = 1;
            // 
            // dataGridView5
            // 
            this.dataGridView5.AllowUserToAddRows = false;
            this.dataGridView5.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.dataGridView5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView5.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.dataGridView5.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView5.Location = new System.Drawing.Point(0, 0);
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.RowHeadersVisible = false;
            this.dataGridView5.RowTemplate.Height = 23;
            this.dataGridView5.Size = new System.Drawing.Size(302, 161);
            this.dataGridView5.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle28.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle28;
            this.dataGridViewTextBoxColumn3.HeaderText = "起始层";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle29.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle29.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle29;
            this.dataGridViewTextBoxColumn4.HeaderText = "终止层";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle30.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle30;
            this.dataGridViewTextBoxColumn5.HeaderText = "墙厚(mm)";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.ColumnCount = 2;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel14.Controls.Add(this.button32, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.button33, 1, 0);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(0, 167);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(302, 33);
            this.tableLayoutPanel14.TabIndex = 4;
            // 
            // button32
            // 
            this.button32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button32.Image = global::myWindowsForms.Properties.Resources.add;
            this.button32.Location = new System.Drawing.Point(3, 3);
            this.button32.Name = "button32";
            this.button32.Size = new System.Drawing.Size(145, 27);
            this.button32.TabIndex = 0;
            this.button32.UseVisualStyleBackColor = true;
            this.button32.Click += new System.EventHandler(this.button32_Click);
            // 
            // button33
            // 
            this.button33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button33.Image = global::myWindowsForms.Properties.Resources.delete;
            this.button33.Location = new System.Drawing.Point(154, 3);
            this.button33.Name = "button33";
            this.button33.Size = new System.Drawing.Size(145, 27);
            this.button33.TabIndex = 1;
            this.button33.UseVisualStyleBackColor = true;
            this.button33.Click += new System.EventHandler(this.button33_Click);
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.tabPage7.Controls.Add(this.panel16);
            this.tabPage7.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage7.Location = new System.Drawing.Point(4, 31);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(886, 206);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "圈梁";
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.dataGridView6);
            this.panel16.Controls.Add(this.tableLayoutPanel15);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel16.Location = new System.Drawing.Point(0, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(883, 206);
            this.panel16.TabIndex = 2;
            // 
            // dataGridView6
            // 
            this.dataGridView6.AllowUserToAddRows = false;
            this.dataGridView6.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.dataGridView6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle31.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView6.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle31;
            this.dataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView6.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.Column20,
            this.Column21,
            this.Column22,
            this.Column23,
            this.Column24,
            this.Column25});
            this.dataGridView6.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView6.Location = new System.Drawing.Point(0, 0);
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.RowHeadersVisible = false;
            this.dataGridView6.RowTemplate.Height = 23;
            this.dataGridView6.Size = new System.Drawing.Size(883, 161);
            this.dataGridView6.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle32.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle32;
            this.dataGridViewTextBoxColumn6.HeaderText = "起始层";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle33.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle33.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle33;
            this.dataGridViewTextBoxColumn7.HeaderText = "终止层";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle34.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle34;
            this.dataGridViewTextBoxColumn8.HeaderText = "截面类型";
            this.dataGridViewTextBoxColumn8.Items.AddRange(new object[] {
            "1：矩形",
            "2：工字形",
            "3：圆形",
            "4：正多边形",
            "5：槽形",
            "6：十字形",
            "7：箱型",
            "8：圆管",
            "9：双槽形",
            ""});
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn8.Width = 200;
            // 
            // Column20
            // 
            this.Column20.HeaderText = "B(mm)";
            this.Column20.Name = "Column20";
            this.Column20.Width = 80;
            // 
            // Column21
            // 
            this.Column21.HeaderText = "H(mm)";
            this.Column21.Name = "Column21";
            this.Column21.Width = 80;
            // 
            // Column22
            // 
            this.Column22.HeaderText = "U(mm)";
            this.Column22.Name = "Column22";
            this.Column22.Width = 80;
            // 
            // Column23
            // 
            this.Column23.HeaderText = "T(mm)";
            this.Column23.Name = "Column23";
            this.Column23.Width = 80;
            // 
            // Column24
            // 
            this.Column24.HeaderText = "D(mm)";
            this.Column24.Name = "Column24";
            this.Column24.Width = 80;
            // 
            // Column25
            // 
            this.Column25.HeaderText = "F(mm)";
            this.Column25.Name = "Column25";
            this.Column25.Width = 80;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 2;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel15.Controls.Add(this.button34, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.button35, 1, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(0, 173);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 1;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(883, 33);
            this.tableLayoutPanel15.TabIndex = 4;
            // 
            // button34
            // 
            this.button34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button34.Image = global::myWindowsForms.Properties.Resources.add;
            this.button34.Location = new System.Drawing.Point(3, 3);
            this.button34.Name = "button34";
            this.button34.Size = new System.Drawing.Size(435, 27);
            this.button34.TabIndex = 0;
            this.button34.UseVisualStyleBackColor = true;
            this.button34.Click += new System.EventHandler(this.button34_Click);
            // 
            // button35
            // 
            this.button35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button35.Image = global::myWindowsForms.Properties.Resources.delete;
            this.button35.Location = new System.Drawing.Point(444, 3);
            this.button35.Name = "button35";
            this.button35.Size = new System.Drawing.Size(436, 27);
            this.button35.TabIndex = 1;
            this.button35.UseVisualStyleBackColor = true;
            this.button35.Click += new System.EventHandler(this.button35_Click);
            // 
            // tabPage8
            // 
            this.tabPage8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.tabPage8.Controls.Add(this.panel17);
            this.tabPage8.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage8.Location = new System.Drawing.Point(4, 31);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(886, 206);
            this.tabPage8.TabIndex = 3;
            this.tabPage8.Text = "主梁";
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.dataGridView7);
            this.panel17.Controls.Add(this.tableLayoutPanel16);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel17.Location = new System.Drawing.Point(0, 0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(883, 206);
            this.panel17.TabIndex = 3;
            // 
            // dataGridView7
            // 
            this.dataGridView7.AllowUserToAddRows = false;
            this.dataGridView7.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.dataGridView7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle35.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle35.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle35.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView7.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle35;
            this.dataGridView7.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView7.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewComboBoxColumn1,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19});
            this.dataGridView7.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView7.Location = new System.Drawing.Point(0, 0);
            this.dataGridView7.Name = "dataGridView7";
            this.dataGridView7.RowHeadersVisible = false;
            this.dataGridView7.RowTemplate.Height = 23;
            this.dataGridView7.Size = new System.Drawing.Size(883, 161);
            this.dataGridView7.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn12
            // 
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle36.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle36;
            this.dataGridViewTextBoxColumn12.HeaderText = "起始层";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle37.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle37.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle37;
            this.dataGridViewTextBoxColumn13.HeaderText = "终止层";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewComboBoxColumn1
            // 
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle38.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewComboBoxColumn1.DefaultCellStyle = dataGridViewCellStyle38;
            this.dataGridViewComboBoxColumn1.HeaderText = "截面类型";
            this.dataGridViewComboBoxColumn1.Items.AddRange(new object[] {
            "1：矩形",
            "2：工字形",
            "3：圆形",
            "4：正多边形",
            "5：槽形",
            "6：十字形",
            "7：箱型",
            "8：圆管",
            "9：双槽形",
            ""});
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "B(mm)";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.Width = 80;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.HeaderText = "H(mm)";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.Width = 80;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.HeaderText = "U(mm)";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.Width = 80;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.HeaderText = "T(mm)";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.Width = 80;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.HeaderText = "D(mm)";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.Width = 80;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.HeaderText = "F(mm)";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.Width = 80;
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.ColumnCount = 2;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel16.Controls.Add(this.button37, 0, 0);
            this.tableLayoutPanel16.Controls.Add(this.button38, 1, 0);
            this.tableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel16.Location = new System.Drawing.Point(0, 173);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 1;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(883, 33);
            this.tableLayoutPanel16.TabIndex = 4;
            // 
            // button37
            // 
            this.button37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button37.Image = global::myWindowsForms.Properties.Resources.add;
            this.button37.Location = new System.Drawing.Point(3, 3);
            this.button37.Name = "button37";
            this.button37.Size = new System.Drawing.Size(435, 27);
            this.button37.TabIndex = 0;
            this.button37.UseVisualStyleBackColor = true;
            this.button37.Click += new System.EventHandler(this.button37_Click);
            // 
            // button38
            // 
            this.button38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button38.Image = global::myWindowsForms.Properties.Resources.delete;
            this.button38.Location = new System.Drawing.Point(444, 3);
            this.button38.Name = "button38";
            this.button38.Size = new System.Drawing.Size(436, 27);
            this.button38.TabIndex = 1;
            this.button38.UseVisualStyleBackColor = true;
            this.button38.Click += new System.EventHandler(this.button38_Click);
            // 
            // tabPage9
            // 
            this.tabPage9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.tabPage9.Controls.Add(this.panel18);
            this.tabPage9.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage9.Location = new System.Drawing.Point(4, 31);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(886, 206);
            this.tabPage9.TabIndex = 4;
            this.tabPage9.Text = "外框柱";
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.dataGridView8);
            this.panel18.Controls.Add(this.tableLayoutPanel17);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel18.Location = new System.Drawing.Point(0, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(883, 206);
            this.panel18.TabIndex = 3;
            // 
            // dataGridView8
            // 
            this.dataGridView8.AllowUserToAddRows = false;
            this.dataGridView8.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.dataGridView8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle39.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle39.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle39.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle39.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle39.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView8.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle39;
            this.dataGridView8.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView8.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewComboBoxColumn2,
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24,
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn27});
            this.dataGridView8.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView8.Location = new System.Drawing.Point(0, 0);
            this.dataGridView8.Name = "dataGridView8";
            this.dataGridView8.RowHeadersVisible = false;
            this.dataGridView8.RowTemplate.Height = 23;
            this.dataGridView8.Size = new System.Drawing.Size(883, 161);
            this.dataGridView8.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn20
            // 
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle40.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn20.DefaultCellStyle = dataGridViewCellStyle40;
            this.dataGridViewTextBoxColumn20.HeaderText = "起始层";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            // 
            // dataGridViewTextBoxColumn21
            // 
            dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle41.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle41.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn21.DefaultCellStyle = dataGridViewCellStyle41;
            this.dataGridViewTextBoxColumn21.HeaderText = "终止层";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewComboBoxColumn2
            // 
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle42.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewComboBoxColumn2.DefaultCellStyle = dataGridViewCellStyle42;
            this.dataGridViewComboBoxColumn2.HeaderText = "截面类型";
            this.dataGridViewComboBoxColumn2.Items.AddRange(new object[] {
            "1：矩形",
            "2：工字形",
            "3：圆形",
            "4：正多边形",
            "5：槽形",
            "6：十字形",
            "7：箱型",
            "8：圆管",
            "9：双槽形",
            ""});
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            this.dataGridViewComboBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.HeaderText = "B(mm)";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.Width = 80;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.HeaderText = "H(mm)";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.Width = 80;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.HeaderText = "U(mm)";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.Width = 80;
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.HeaderText = "T(mm)";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.Width = 80;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.HeaderText = "D(mm)";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.Width = 80;
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.HeaderText = "F(mm)";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.Width = 80;
            // 
            // tableLayoutPanel17
            // 
            this.tableLayoutPanel17.ColumnCount = 2;
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel17.Controls.Add(this.button39, 0, 0);
            this.tableLayoutPanel17.Controls.Add(this.button40, 1, 0);
            this.tableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel17.Location = new System.Drawing.Point(0, 173);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            this.tableLayoutPanel17.RowCount = 1;
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.Size = new System.Drawing.Size(883, 33);
            this.tableLayoutPanel17.TabIndex = 4;
            // 
            // button39
            // 
            this.button39.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button39.Image = global::myWindowsForms.Properties.Resources.add;
            this.button39.Location = new System.Drawing.Point(3, 3);
            this.button39.Name = "button39";
            this.button39.Size = new System.Drawing.Size(435, 27);
            this.button39.TabIndex = 0;
            this.button39.UseVisualStyleBackColor = true;
            this.button39.Click += new System.EventHandler(this.button39_Click);
            // 
            // button40
            // 
            this.button40.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button40.Image = global::myWindowsForms.Properties.Resources.delete;
            this.button40.Location = new System.Drawing.Point(444, 3);
            this.button40.Name = "button40";
            this.button40.Size = new System.Drawing.Size(436, 27);
            this.button40.TabIndex = 1;
            this.button40.UseVisualStyleBackColor = true;
            this.button40.Click += new System.EventHandler(this.button40_Click);
            // 
            // tabPage10
            // 
            this.tabPage10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.tabPage10.Controls.Add(this.panel19);
            this.tabPage10.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage10.Location = new System.Drawing.Point(4, 31);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(886, 206);
            this.tabPage10.TabIndex = 5;
            this.tabPage10.Text = "伸臂桁架";
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.dataGridView9);
            this.panel19.Controls.Add(this.tableLayoutPanel18);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel19.Location = new System.Drawing.Point(0, 0);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(883, 206);
            this.panel19.TabIndex = 3;
            // 
            // dataGridView9
            // 
            this.dataGridView9.AllowUserToAddRows = false;
            this.dataGridView9.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.dataGridView9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle43.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle43.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle43.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle43.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle43.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle43.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView9.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle43;
            this.dataGridView9.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView9.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn28,
            this.dataGridViewTextBoxColumn29,
            this.dataGridViewComboBoxColumn3,
            this.dataGridViewTextBoxColumn30,
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewTextBoxColumn32,
            this.dataGridViewTextBoxColumn33,
            this.dataGridViewTextBoxColumn34,
            this.dataGridViewTextBoxColumn35});
            this.dataGridView9.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView9.Location = new System.Drawing.Point(0, 0);
            this.dataGridView9.Name = "dataGridView9";
            this.dataGridView9.RowHeadersVisible = false;
            this.dataGridView9.RowTemplate.Height = 23;
            this.dataGridView9.Size = new System.Drawing.Size(883, 161);
            this.dataGridView9.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn28
            // 
            dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle44.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn28.DefaultCellStyle = dataGridViewCellStyle44;
            this.dataGridViewTextBoxColumn28.HeaderText = "起始层";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            // 
            // dataGridViewTextBoxColumn29
            // 
            dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle45.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle45.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn29.DefaultCellStyle = dataGridViewCellStyle45;
            this.dataGridViewTextBoxColumn29.HeaderText = "终止层";
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewComboBoxColumn3
            // 
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle46.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewComboBoxColumn3.DefaultCellStyle = dataGridViewCellStyle46;
            this.dataGridViewComboBoxColumn3.HeaderText = "截面类型";
            this.dataGridViewComboBoxColumn3.Items.AddRange(new object[] {
            "1：矩形",
            "2：工字形",
            "3：圆形",
            "4：正多边形",
            "5：槽形",
            "6：十字形",
            "7：箱型",
            "8：圆管",
            "9：双槽形",
            ""});
            this.dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
            this.dataGridViewComboBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn30
            // 
            this.dataGridViewTextBoxColumn30.HeaderText = "B(mm)";
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.Width = 80;
            // 
            // dataGridViewTextBoxColumn31
            // 
            this.dataGridViewTextBoxColumn31.HeaderText = "H(mm)";
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.Width = 80;
            // 
            // dataGridViewTextBoxColumn32
            // 
            this.dataGridViewTextBoxColumn32.HeaderText = "U(mm)";
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.Width = 80;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.HeaderText = "T(mm)";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.Width = 80;
            // 
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.HeaderText = "D(mm)";
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.Width = 80;
            // 
            // dataGridViewTextBoxColumn35
            // 
            this.dataGridViewTextBoxColumn35.HeaderText = "F(mm)";
            this.dataGridViewTextBoxColumn35.Name = "dataGridViewTextBoxColumn35";
            this.dataGridViewTextBoxColumn35.Width = 80;
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.ColumnCount = 2;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel18.Controls.Add(this.button41, 0, 0);
            this.tableLayoutPanel18.Controls.Add(this.button42, 1, 0);
            this.tableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel18.Location = new System.Drawing.Point(0, 173);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 1;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(883, 33);
            this.tableLayoutPanel18.TabIndex = 4;
            // 
            // button41
            // 
            this.button41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button41.Image = global::myWindowsForms.Properties.Resources.add;
            this.button41.Location = new System.Drawing.Point(3, 3);
            this.button41.Name = "button41";
            this.button41.Size = new System.Drawing.Size(435, 27);
            this.button41.TabIndex = 0;
            this.button41.UseVisualStyleBackColor = true;
            this.button41.Click += new System.EventHandler(this.button41_Click);
            // 
            // button42
            // 
            this.button42.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button42.Image = global::myWindowsForms.Properties.Resources.delete;
            this.button42.Location = new System.Drawing.Point(444, 3);
            this.button42.Name = "button42";
            this.button42.Size = new System.Drawing.Size(436, 27);
            this.button42.TabIndex = 1;
            this.button42.UseVisualStyleBackColor = true;
            this.button42.Click += new System.EventHandler(this.button42_Click);
            // 
            // tabPage11
            // 
            this.tabPage11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.tabPage11.Controls.Add(this.panel20);
            this.tabPage11.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage11.Location = new System.Drawing.Point(4, 31);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new System.Drawing.Size(886, 206);
            this.tabPage11.TabIndex = 6;
            this.tabPage11.Text = "环带桁架";
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.dataGridView10);
            this.panel20.Controls.Add(this.tableLayoutPanel19);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel20.Location = new System.Drawing.Point(0, 0);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(883, 206);
            this.panel20.TabIndex = 3;
            // 
            // dataGridView10
            // 
            this.dataGridView10.AllowUserToAddRows = false;
            this.dataGridView10.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.dataGridView10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle47.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle47.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle47.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle47.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle47.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle47.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView10.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle47;
            this.dataGridView10.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView10.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn36,
            this.dataGridViewTextBoxColumn37,
            this.dataGridViewComboBoxColumn4,
            this.dataGridViewTextBoxColumn38,
            this.dataGridViewTextBoxColumn39,
            this.dataGridViewTextBoxColumn40,
            this.dataGridViewTextBoxColumn41,
            this.dataGridViewTextBoxColumn42,
            this.dataGridViewTextBoxColumn43});
            this.dataGridView10.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView10.Location = new System.Drawing.Point(0, 0);
            this.dataGridView10.Name = "dataGridView10";
            this.dataGridView10.RowHeadersVisible = false;
            this.dataGridView10.RowTemplate.Height = 23;
            this.dataGridView10.Size = new System.Drawing.Size(883, 161);
            this.dataGridView10.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn36
            // 
            dataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle48.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn36.DefaultCellStyle = dataGridViewCellStyle48;
            this.dataGridViewTextBoxColumn36.HeaderText = "起始层";
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            // 
            // dataGridViewTextBoxColumn37
            // 
            dataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle49.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle49.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn37.DefaultCellStyle = dataGridViewCellStyle49;
            this.dataGridViewTextBoxColumn37.HeaderText = "终止层";
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            this.dataGridViewTextBoxColumn37.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewComboBoxColumn4
            // 
            dataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle50.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewComboBoxColumn4.DefaultCellStyle = dataGridViewCellStyle50;
            this.dataGridViewComboBoxColumn4.HeaderText = "截面类型";
            this.dataGridViewComboBoxColumn4.Items.AddRange(new object[] {
            "1：矩形",
            "2：工字形",
            "3：圆形",
            "4：正多边形",
            "5：槽形",
            "6：十字形",
            "7：箱型",
            "8：圆管",
            "9：双槽形",
            ""});
            this.dataGridViewComboBoxColumn4.Name = "dataGridViewComboBoxColumn4";
            this.dataGridViewComboBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn4.Width = 200;
            // 
            // dataGridViewTextBoxColumn38
            // 
            this.dataGridViewTextBoxColumn38.HeaderText = "B(mm)";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.Width = 80;
            // 
            // dataGridViewTextBoxColumn39
            // 
            this.dataGridViewTextBoxColumn39.HeaderText = "H(mm)";
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            this.dataGridViewTextBoxColumn39.Width = 80;
            // 
            // dataGridViewTextBoxColumn40
            // 
            this.dataGridViewTextBoxColumn40.HeaderText = "U(mm)";
            this.dataGridViewTextBoxColumn40.Name = "dataGridViewTextBoxColumn40";
            this.dataGridViewTextBoxColumn40.Width = 80;
            // 
            // dataGridViewTextBoxColumn41
            // 
            this.dataGridViewTextBoxColumn41.HeaderText = "T(mm)";
            this.dataGridViewTextBoxColumn41.Name = "dataGridViewTextBoxColumn41";
            this.dataGridViewTextBoxColumn41.Width = 80;
            // 
            // dataGridViewTextBoxColumn42
            // 
            this.dataGridViewTextBoxColumn42.HeaderText = "D(mm)";
            this.dataGridViewTextBoxColumn42.Name = "dataGridViewTextBoxColumn42";
            this.dataGridViewTextBoxColumn42.Width = 80;
            // 
            // dataGridViewTextBoxColumn43
            // 
            this.dataGridViewTextBoxColumn43.HeaderText = "F(mm)";
            this.dataGridViewTextBoxColumn43.Name = "dataGridViewTextBoxColumn43";
            this.dataGridViewTextBoxColumn43.Width = 80;
            // 
            // tableLayoutPanel19
            // 
            this.tableLayoutPanel19.ColumnCount = 2;
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel19.Controls.Add(this.button43, 0, 0);
            this.tableLayoutPanel19.Controls.Add(this.button44, 1, 0);
            this.tableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel19.Location = new System.Drawing.Point(0, 173);
            this.tableLayoutPanel19.Name = "tableLayoutPanel19";
            this.tableLayoutPanel19.RowCount = 1;
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel19.Size = new System.Drawing.Size(883, 33);
            this.tableLayoutPanel19.TabIndex = 4;
            // 
            // button43
            // 
            this.button43.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button43.Image = global::myWindowsForms.Properties.Resources.add;
            this.button43.Location = new System.Drawing.Point(3, 3);
            this.button43.Name = "button43";
            this.button43.Size = new System.Drawing.Size(435, 27);
            this.button43.TabIndex = 0;
            this.button43.UseVisualStyleBackColor = true;
            this.button43.Click += new System.EventHandler(this.button43_Click);
            // 
            // button44
            // 
            this.button44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button44.Image = global::myWindowsForms.Properties.Resources.delete;
            this.button44.Location = new System.Drawing.Point(444, 3);
            this.button44.Name = "button44";
            this.button44.Size = new System.Drawing.Size(436, 27);
            this.button44.TabIndex = 1;
            this.button44.UseVisualStyleBackColor = true;
            this.button44.Click += new System.EventHandler(this.button44_Click);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.panel12.Controls.Add(this.groupBox9);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(900, 64);
            this.panel12.TabIndex = 18;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.tableLayoutPanel11);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox9.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox9.ForeColor = System.Drawing.Color.White;
            this.groupBox9.Location = new System.Drawing.Point(0, 0);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(900, 64);
            this.groupBox9.TabIndex = 0;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "截面自动生成";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Controls.Add(this.checkBox2, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.checkBox1, 0, 0);
            this.tableLayoutPanel11.Location = new System.Drawing.Point(41, 24);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(395, 28);
            this.tableLayoutPanel11.TabIndex = 2;
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.ForeColor = System.Drawing.Color.Black;
            this.checkBox2.Location = new System.Drawing.Point(200, 3);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(192, 22);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "专家系统";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.ForeColor = System.Drawing.Color.Black;
            this.checkBox1.Location = new System.Drawing.Point(3, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(191, 22);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "规范测算";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.保存ToolStripMenuItem.Text = "保存";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 帮助文件ToolStripMenuItem
            // 
            this.帮助文件ToolStripMenuItem.Name = "帮助文件ToolStripMenuItem";
            this.帮助文件ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.帮助文件ToolStripMenuItem.Text = "帮助文件";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存ToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.帮助文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(948, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(948, 473);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "超高层结构智能设计";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView7)).EndInit();
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView8)).EndInit();
            this.tableLayoutPanel17.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView9)).EndInit();
            this.tableLayoutPanel18.ResumeLayout(false);
            this.tabPage11.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView10)).EndInit();
            this.tableLayoutPanel19.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        //PrivateFontCollection font = new PrivateFontCollection();
        //font.AddFontFile(Environment.CurrentDirectory + @"\1-Alibaba-PuHuiTi-Medium.TTF");
        //var r = font.Families[0].IsStyleAvailable(FontStyle.Regular);
        //var b = font.Families[0].IsStyleAvailable(FontStyle.Bold);
        //FontFamily myFontFamily = new FontFamily(font.Families[0].Name, font);
        //System.Drawing.Font myFont = new System.Drawing.Font(myFontFamily, (float)10.5, FontStyle.Bold);
        //this.button4.Font = myFont;

        //生成结构
        private void generateStructure()
        {
            Rhino.Geometry.Brep surf1 = outface.Brep();
            Rhino.Geometry.Brep surf2 = inface.Brep();
            column.Clear();
            beam1.Clear();
            beam2.Clear();
            wall.Clear();
            brace1.Clear();
            brace2.Clear();

            //List<int> waist = new List<int>();
            //List<int> segment_arm = new List<int>();
            //List<int> type_arm = new List<int>();
            //List<int> segment_round = new List<int>();
            //List<int> type_round = new List<int>();

            num_col = num_col >= 4 ? num_col : 4;
            num_col = num_col % 2 == 1 ? ++num_col : num_col;
            num_corner = num_corner >= 0 ? num_corner : 0;

            double tolerance = 0.1;

            double pin_x_min, pin_x_max, pin_y_min, pin_y_max, pin_x_cen, pin_y_cen;
            List<List<Point3d>> pout_all = new List<List<Point3d>>();
            List<List<Line>> waist_hor = new List<List<Line>>();

            for (int floor = 0; floor < height.Count; floor++)
            {
                List<Line> temp_beam1 = new List<Line>();
                List<Line> temp_beam2 = new List<Line>();
                List<Line> temp_wall = new List<Line>();

                List<Point3d> pout = new List<Point3d>();
                Plane pla = new Plane(new Point3d(0, 0, height[floor] + ba), new Vector3d(0, 0, 1));
                Rhino.Geometry.Intersect.Intersection.BrepPlane(surf1, pla, Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance, out Curve[] intersectionCurvesOut, out Point3d[] intersectionPointsOut);
                Rhino.Geometry.Intersect.Intersection.BrepPlane(surf2, pla, Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance, out Curve[] intersectionCurvesIn, out Point3d[] intersectionPointsIn);
                if (intersectionCurvesOut is null | intersectionCurvesIn is null)
                {
                    break;
                }

                //找到核心筒的四个角点
                List<Curve> in_div = new List<Curve>();
                in_div.AddRange(intersectionCurvesIn[0].DuplicateSegments());
                pin_x_min = in_div[0].PointAtStart.X;
                pin_x_max = in_div[0].PointAtStart.X;
                pin_y_min = in_div[0].PointAtStart.Y;
                pin_y_max = in_div[0].PointAtStart.Y;
                for (int j = 0; j < in_div.Count; j++)
                {
                    pin_x_min = pin_x_min < in_div[j].PointAtStart.X ? pin_x_min : in_div[j].PointAtStart.X;
                    pin_x_max = pin_x_max > in_div[j].PointAtStart.X ? pin_x_max : in_div[j].PointAtStart.X;
                    pin_y_min = pin_y_min < in_div[j].PointAtStart.Y ? pin_y_min : in_div[j].PointAtStart.Y;
                    pin_y_max = pin_y_max > in_div[j].PointAtStart.Y ? pin_y_max : in_div[j].PointAtStart.Y;
                }
                pin_x_cen = (pin_x_min + pin_x_max) / 2;
                pin_y_cen = (pin_y_min + pin_y_max) / 2;

                //找到外立面的八个角点
                List<double> corn1 = new List<double>();
                List<double> corn2 = new List<double>();
                List<double> corn3 = new List<double>();
                List<double> corn4 = new List<double>();
                Rhino.Geometry.Intersect.CurveIntersections intersections;
                intersections = Rhino.Geometry.Intersect.Intersection.CurveLine(intersectionCurvesOut[0], new Line(new Point3d(pin_x_min, pin_y_max, height[floor] + ba), new Vector3d(0, 1, 0)), 1, 1);
                for (int i = 0; i < intersections.Count; i++)
                {
                    if (intersections[i].PointA.Y >= pin_y_max)
                    {
                        corn1.Add(intersections[i].ParameterA);
                    }
                    else
                    {
                        corn4.Add(intersections[i].ParameterA);
                    }
                }
                intersections = Rhino.Geometry.Intersect.Intersection.CurveLine(intersectionCurvesOut[0], new Line(new Point3d(pin_x_max, pin_y_max, height[floor] + ba), new Vector3d(0, 1, 0)), 1, 1);
                for (int i = 0; i < intersections.Count; i++)
                {
                    if (intersections[i].PointA.Y >= pin_y_max)
                    {
                        corn2.Add(intersections[i].ParameterA);
                    }
                    else
                    {
                        corn3.Add(intersections[i].ParameterA);
                    }
                }
                intersections = Rhino.Geometry.Intersect.Intersection.CurveLine(intersectionCurvesOut[0], new Line(new Point3d(pin_x_min, pin_y_max, height[floor] + ba), new Vector3d(1, 0, 0)), 1, 1);
                for (int i = 0; i < intersections.Count; i++)
                {
                    if (intersections[i].PointA.X >= pin_x_max)
                    {
                        corn2.Add(intersections[i].ParameterA);
                    }
                    else
                    {
                        corn1.Add(intersections[i].ParameterA);
                    }
                }
                intersections = Rhino.Geometry.Intersect.Intersection.CurveLine(intersectionCurvesOut[0], new Line(new Point3d(pin_x_min, pin_y_min, height[floor] + ba), new Vector3d(1, 0, 0)), 1, 1);
                for (int i = 0; i < intersections.Count; i++)
                {
                    if (intersections[i].PointA.X >= pin_x_max)
                    {
                        corn3.Add(intersections[i].ParameterA);
                    }
                    else
                    {
                        corn4.Add(intersections[i].ParameterA);
                    }
                }
                for (int i = 0; i <= num_corner + 1; i++)
                {
                    pout.Add(intersectionCurvesOut[0].PointAt(corn1[0] + (corn1[1] - corn1[0]) / (num_corner + 1) * i));
                    pout.Add(intersectionCurvesOut[0].PointAt(corn2[0] + (corn2[1] - corn2[0]) / (num_corner + 1) * i));
                    pout.Add(intersectionCurvesOut[0].PointAt(corn3[0] + (corn3[1] - corn3[0]) / (num_corner + 1) * i));
                    pout.Add(intersectionCurvesOut[0].PointAt(corn4[0] + (corn4[1] - corn4[0]) / (num_corner + 1) * i));
                }
                int num_hor = (int)((num_col / 2 - 2) * (pin_x_max - pin_x_min) / (pin_x_max + pin_y_max - pin_x_min - pin_y_min));
                int num_ver = num_col / 2 - 2 - num_hor;

                for (int i = 1; i <= num_hor; i++)
                {
                    intersections = Rhino.Geometry.Intersect.Intersection.CurveLine(intersectionCurvesOut[0], new Line(new Point3d(pin_x_min + i * (pin_x_max - pin_x_min) / (num_hor + 1), pin_y_max, height[floor] + ba), new Vector3d(0, 1, 0)), 1, 1);
                    pout.Add(intersections[0].PointA);
                    pout.Add(intersections[1].PointA);
                }
                for (int i = 1; i <= num_ver; i++)
                {
                    intersections = Rhino.Geometry.Intersect.Intersection.CurveLine(intersectionCurvesOut[0], new Line(new Point3d(pin_x_max, pin_y_min + i * (pin_y_max - pin_y_min) / (num_ver + 1), height[floor] + ba), new Vector3d(1, 0, 0)), 1, 1);
                    pout.Add(intersections[0].PointA);
                    pout.Add(intersections[1].PointA);
                }

                //墙
                //int num_wall = 0;
                for (int i = 0; i <= num_ver + 1; i++)
                {
                    for (int j = 0; j <= num_hor; j++)
                    {
                        temp_wall.Add(new Line(new Point3d(pin_x_min + (double)j / (num_hor + 1) * (pin_x_max - pin_x_min), pin_y_min + (double)i / (num_ver + 1) * (pin_y_max - pin_y_min), height[floor] + ba), new Point3d(pin_x_min + (double)(j + 1) / (num_hor + 1) * (pin_x_max - pin_x_min), pin_y_min + (double)i / (num_ver + 1) * (pin_y_max - pin_y_min), height[floor] + ba)));
                        //wall.Insert(new Line(new Point3d(pin_x_min + (double)j / (num_hor + 1) * (pin_x_max - pin_x_min), pin_y_min + (double)i / (num_ver + 1) * (pin_y_max - pin_y_min), height[floor] + ba), new Point3d(pin_x_min + (double)(j + 1) / (num_hor + 1) * (pin_x_max - pin_x_min), pin_y_min + (double)i / (num_ver + 1) * (pin_y_max - pin_y_min), height[floor] + ba)), new GH_Path(floor, num_wall++), 0);
                    }
                }
                for (int i = 0; i <= num_hor + 1; i++)
                {
                    for (int j = 0; j <= num_ver; j++)
                    {
                        temp_wall.Add(new Line(new Point3d(pin_x_min + (double)i / (num_hor + 1) * (pin_x_max - pin_x_min), pin_y_min + (double)j / (num_ver + 1) * (pin_y_max - pin_y_min), height[floor] + ba), new Point3d(pin_x_min + (double)i / (num_hor + 1) * (pin_x_max - pin_x_min), pin_y_min + (double)(j + 1) / (num_ver + 1) * (pin_y_max - pin_y_min), height[floor] + ba)));
                        //wall.Insert(new Line(new Point3d(pin_x_min + (double)i / (num_hor + 1) * (pin_x_max - pin_x_min), pin_y_min + (double)j / (num_ver + 1) * (pin_y_max - pin_y_min), height[floor] + ba), new Point3d(pin_x_min + (double)i / (num_hor + 1) * (pin_x_max - pin_x_min), pin_y_min + (double)(j + 1) / (num_ver + 1) * (pin_y_max - pin_y_min), height[floor] + ba)), new GH_Path(floor, num_wall++), 0);
                    }
                }
                //排序
                double theta1;
                double theta2;
                Point3d p_temp;
                for (int i = 0; i < pout.Count; i++)
                {
                    for (int j = 0; j < pout.Count - i - 1; j++)
                    {
                        theta1 = Math.Acos((pout[j].X - pin_x_cen) / Math.Sqrt(Math.Pow(pout[j].X - pin_x_cen, 2) + Math.Pow(pout[j].Y - pin_y_cen, 2)));
                        theta2 = Math.Acos((pout[j + 1].X - pin_x_cen) / Math.Sqrt(Math.Pow(pout[j + 1].X - pin_x_cen, 2) + Math.Pow(pout[j + 1].Y - pin_y_cen, 2)));
                        theta1 = pout[j].Y >= pin_y_cen - 0.1 ? theta1 : 2 * Math.PI - theta1;//0.1是宽容度
                        theta2 = pout[j + 1].Y >= pin_y_cen - 0.1 ? theta2 : 2 * Math.PI - theta2;
                        if (theta1 > theta2)
                        {
                            p_temp = pout[j];
                            pout[j] = pout[j + 1];
                            pout[j + 1] = p_temp;
                        }
                    }
                }
                pout_all.Add(pout);

                //外圈梁
                for (int i = 0; i < pout.Count - 1; i++)
                {
                    temp_beam1.Add(new Line(pout[i], pout[i + 1]));
                    //beam1.Insert(new Line(pout[i], pout[i + 1]), new GH_Path(floor, i), 0);
                }
                temp_beam1.Add(new Line(pout[0], pout[pout.Count - 1]));
                //beam1.Insert(new Line(pout[0], pout[pout.Count - 1]), new GH_Path(floor, pout.Count - 1), 0);


                List<Line> temp_waist_hor = new List<Line>();

                //主梁
                for (int i = 0; i < pout.Count; i++)
                {
                    double pout_x = pout[i].X;
                    double pout_y = pout[i].Y;
                    double pout_z = pout[i].Z;
                    if ((Math.Abs(pout_x - pin_x_max) < tolerance || Math.Abs(pout_x - pin_x_min) < tolerance))
                    {
                        if (pout_y >= pin_y_max)
                        {
                            temp_waist_hor.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pout_x, pin_y_max, pout_z)));
                        }
                        else
                        {
                            temp_waist_hor.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pout_x, pin_y_min, pout_z)));
                        }
                    }
                    else if ((Math.Abs(pout_y - pin_y_max) < tolerance || Math.Abs(pout_y - pin_y_min) < tolerance))
                    {
                        if (pout_x >= pin_x_max)
                        {
                            temp_waist_hor.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pout_y, pout_z)));
                        }
                        else
                        {
                            temp_waist_hor.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pout_y, pout_z)));
                        }
                    }
                    if (pout_x >= pin_x_min && pout_x <= pin_x_max + tolerance && pout_y >= pin_y_max - tolerance)
                    {
                        temp_beam2.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pout_x, pin_y_max, pout_z)));
                        //beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pout_x, pin_y_max, pout_z)), new GH_Path(floor, i), 0);
                    }
                    else if (pout_x <= pin_x_min && pout_y <= pin_y_max + tolerance && pout_y >= pin_y_min - tolerance)
                    {
                        temp_beam2.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pout_y, pout_z)));
                        //beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pout_y, pout_z)), new GH_Path(floor, i), 0);
                    }
                    else if (pout_x >= pin_x_min - tolerance && pout_x <= pin_x_max + tolerance && pout_y <= pin_y_min)
                    {
                        temp_beam2.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pout_x, pin_y_min, pout_z)));
                        //beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pout_x, pin_y_min, pout_z)), new GH_Path(floor, i), 0);
                    }
                    else if (pout_x >= pin_x_max && pout_y >= pin_y_min - tolerance && pout_y <= pin_y_max + tolerance)
                    {
                        temp_beam2.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pout_y, pout_z)));
                        //beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pout_y, pout_z)), new GH_Path(floor, i), 0);
                    }
                    else if (pout_x > pin_x_max && pout_y > pin_y_max)
                    {
                        temp_beam2.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pin_y_max, pout_z)));
                        //beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pin_y_max, pout_z)), new GH_Path(floor, i), 0);
                    }

                    else if (pout_x < pin_x_min && pout_y > pin_y_max)
                    {
                        temp_beam2.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pin_y_max, pout_z)));
                        //beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pin_y_max, pout_z)), new GH_Path(floor, i), 0);
                    }

                    else if (pout_x < pin_x_min && pout_y < pin_y_min)
                    {
                        temp_beam2.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pin_y_min, pout_z)));
                        //beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pin_y_min, pout_z)), new GH_Path(floor, i), 0);
                    }

                    else if (pout_x > pin_x_max && pout_y < pin_y_min)
                    {
                        temp_beam2.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pin_y_min, pout_z)));
                        //beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pin_y_min, pout_z)), new GH_Path(floor, i), 0);
                    }

                }
                waist_hor.Add(temp_waist_hor);
                beam1.Add(temp_beam1);
                beam2.Add(temp_beam2);
                wall.Add(temp_wall);
            }
            //柱
            for (int i = 0; i < pout_all.Count - 1; i++)
            {
                List<Line> temp_column = new List<Line>();
                for (int j = 0; j < pout_all[0].Count; j++)
                {
                    temp_column.Add(new Line(pout_all[i][j], pout_all[i + 1][j]));
                    //column.Insert(new Line(pout_all[i][j], pout_all[i + 1][j]), new GH_Path(i, j), 0);
                }
                column.Add(temp_column);
            }
            //腰部桁架
            if (waist.Count > 0)
            {
                int temp_waist;
                int temp_segment;
                int temp_type = 0;
                for (int i = 0; i < waist.Count; i++)
                {
                    List<Line> temp_brace1 = new List<Line>();
                    temp_waist = waist[i];
                    if (temp_waist >= waist_hor.Count - 1)
                    {
                        break;
                    }
                    if (segment_arm.Count > i)
                    {
                        temp_segment = segment_arm[i];
                    }
                    else
                    {
                        temp_segment = (int)(waist_hor[temp_waist][0].Length / Math.Abs(waist_hor[temp_waist][0].FromZ - waist_hor[temp_waist + 1][0].FromZ));
                    }
                    if (type_arm.Count > i)
                    {
                        temp_type = type_arm[i];
                    }
                    for (int j = 0; j < waist_hor[temp_waist].Count; j++)
                    {
                        int num_brace = 0;
                        Line line_b = waist_hor[temp_waist][j];
                        Line line_u = waist_hor[temp_waist + 1][j];
                        List<Point3d> bot = new List<Point3d>();
                        List<Point3d> upp = new List<Point3d>();
                        if (temp_type == 1)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                temp_brace1.Add(new Line(bot[k], upp[k]));
                                //brace1.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count / 2; k++)
                            {
                                temp_brace1.Add(new Line(bot[k], upp[k + 1]));
                                //brace1.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = bot.Count / 2; k < bot.Count; k++)
                            {
                                temp_brace1.Add(new Line(bot[k], upp[k - 1]));
                                //brace1.Insert(new Line(bot[k], upp[k - 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                        }
                        if (temp_type == 2)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                temp_brace1.Add(new Line(bot[k], upp[k]));
                                //brace1.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count / 2; k++)
                            {
                                temp_brace1.Add(new Line(bot[k + 1], upp[k]));
                                //brace1.Insert(new Line(bot[k + 1], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = bot.Count / 2; k < bot.Count; k++)
                            {
                                temp_brace1.Add(new Line(bot[k - 1], upp[k]));
                                //brace1.Insert(new Line(bot[k - 1], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                        }
                        if (temp_type == 3)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                temp_brace1.Add(new Line(bot[k], upp[k]));
                                //brace1.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 0)
                                {
                                    temp_brace1.Add(new Line(bot[k], upp[k + 1]));
                                    //brace1.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    temp_brace1.Add(new Line(bot[k + 1], upp[k]));
                                    //brace1.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                        if (temp_type == 4)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                temp_brace1.Add(new Line(bot[k], upp[k]));
                                //brace1.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 1)
                                {
                                    temp_brace1.Add(new Line(bot[k], upp[k + 1]));
                                    //brace1.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    temp_brace1.Add(new Line(bot[k + 1], upp[k]));
                                    //brace1.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                        if (temp_type == 5)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < (2 * temp_segment); k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / (2 * temp_segment) * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / (2 * temp_segment) * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 1)
                                {
                                    temp_brace1.Add(new Line(bot[k], upp[k + 1]));
                                    //brace1.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    temp_brace1.Add(new Line(bot[k + 1], upp[k]));
                                    //brace1.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                        if (temp_type == 6)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < (2 * temp_segment); k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / (2 * temp_segment) * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / (2 * temp_segment) * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 0)
                                {
                                    temp_brace1.Add(new Line(bot[k], upp[k + 1]));
                                    //brace1.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    temp_brace1.Add(new Line(bot[k + 1], upp[k]));
                                    //brace1.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                    }

                    brace1.Add(temp_brace1);
                }
            }

            //环带桁架
            if (waist.Count > 0)
            {
                int temp_waist;
                int temp_segment = 0;
                int temp_type = 1;
                for (int i = 0; i < waist.Count; i++)
                {
                    List<Line> temp_brace2 = new List<Line>();
                    temp_waist = waist[i];
                    if (temp_waist >= waist_hor.Count - 1)
                    {
                        break;
                    }

                    if (segment_round.Count > i)
                    {
                        temp_segment = segment_round[i];
                    }
                    else
                    {
                        temp_segment = (int)(waist_hor[temp_waist][0].Length / Math.Abs(waist_hor[temp_waist][0].FromZ - waist_hor[temp_waist + 1][0].FromZ));
                    }
                    if (type_round.Count > i)
                    {
                        temp_type = type_round[i];
                    }

                    for (int j = 0; j < pout_all[0].Count; j++)
                    {
                        int num_brace = 0;
                        Line line_b = beam1[temp_waist][j];
                        Line line_u = beam1[temp_waist + 1][j];
                        //Line line_b = beam1[new GH_Path(temp_waist, j), 0];
                        //Line line_u = beam1[new GH_Path(temp_waist + 1, j), 0];
                        List<Point3d> bot = new List<Point3d>();
                        List<Point3d> upp = new List<Point3d>();
                        if (temp_type == 1)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_u.FromX + (double)k / temp_segment * (line_u.ToX - line_u.FromX), line_u.FromY + (double)k / temp_segment * (line_u.ToY - line_u.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                temp_brace2.Add(new Line(bot[k], upp[k]));
                                //brace2.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count / 2; k++)
                            {
                                temp_brace2.Add(new Line(bot[k], upp[k + 1]));
                                //brace2.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = bot.Count / 2; k < bot.Count; k++)
                            {
                                temp_brace2.Add(new Line(bot[k], upp[k - 1]));
                                //brace2.Insert(new Line(bot[k], upp[k - 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                        }
                        if (temp_type == 2)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                temp_brace2.Add(new Line(bot[k], upp[k]));
                                //brace2.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count / 2; k++)
                            {
                                temp_brace2.Add(new Line(bot[k + 1], upp[k]));
                                //brace2.Insert(new Line(bot[k + 1], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = bot.Count / 2; k < bot.Count; k++)
                            {
                                temp_brace2.Add(new Line(bot[k - 1], upp[k]));
                                //brace2.Insert(new Line(bot[k - 1], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                        }
                        if (temp_type == 3)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                temp_brace2.Add(new Line(bot[k], upp[k]));
                                //brace2.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 0)
                                {
                                    temp_brace2.Add(new Line(bot[k], upp[k + 1]));
                                    //brace2.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    temp_brace2.Add(new Line(bot[k + 1], upp[k]));
                                    //brace2.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                        if (temp_type == 4)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                temp_brace2.Add(new Line(bot[k], upp[k]));
                                //brace2.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 1)
                                {
                                    temp_brace2.Add(new Line(bot[k], upp[k + 1]));
                                    //brace2.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    temp_brace2.Add(new Line(bot[k + 1], upp[k]));
                                    //brace2.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                        if (temp_type == 5)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < (2 * temp_segment); k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / (2 * temp_segment) * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / (2 * temp_segment) * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 1)
                                {
                                    temp_brace2.Add(new Line(bot[k], upp[k + 1]));
                                    //brace2.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    temp_brace2.Add(new Line(bot[k + 1], upp[k]));
                                    //brace2.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                        if (temp_type == 6)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < (2 * temp_segment); k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / (2 * temp_segment) * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / (2 * temp_segment) * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 0)
                                {
                                    temp_brace2.Add(new Line(bot[k], upp[k + 1]));
                                    //brace2.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    temp_brace2.Add(new Line(bot[k + 1], upp[k]));
                                    //brace2.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                    }

                    brace2.Add(temp_brace2);
                }
            }
            this.comboBox1.SelectedIndex = 0;
            for (int i = 0; i < beam1.Count; i++)
            {
                this.comboBox1.Items.Add((i + 1).ToString());
                highlight_flr.Add(i + 1);
                highlight_all.Add(i + 1);
            }


            myPaint();
            myHighlight();

        }
        private void myPaint()
        {
            foreach (var i in ID_column)
            {
                foreach (var j in i)
                {
                    RhinoDoc.ActiveDoc.Objects.Delete(j, true);
                }
            }
            foreach (var i in ID_beam1)
            {
                foreach (var j in i)
                {
                    RhinoDoc.ActiveDoc.Objects.Delete(j, true);
                }
            }
            foreach (var i in ID_beam2)
            {
                foreach (var j in i)
                {
                    RhinoDoc.ActiveDoc.Objects.Delete(j, true);
                }
            }
            foreach (var i in ID_wall)
            {
                foreach (var j in i)
                {
                    RhinoDoc.ActiveDoc.Objects.Delete(j, true);
                }
            }
            foreach (var i in ID_brace1)
            {
                foreach (var j in i)
                {
                    RhinoDoc.ActiveDoc.Objects.Delete(j, true);
                }
            }
            foreach (var i in ID_brace2)
            {
                foreach (var j in i)
                {
                    RhinoDoc.ActiveDoc.Objects.Delete(j, true);
                }
            }
            RhinoDoc.ActiveDoc.Views.Redraw();
            ID_beam1.Clear();
            ID_beam2.Clear();
            ID_brace1.Clear();
            ID_brace2.Clear();
            ID_column.Clear();
            ID_wall.Clear();
            foreach (var i in column)
            {
                List<Guid> temp_list = new List<Guid>();
                foreach (var j in i)
                {
                    var tempID = RhinoDoc.ActiveDoc.Objects.AddLine(j);
                    RhinoDoc.ActiveDoc.Objects.Select(tempID);
                    temp_list.Add(tempID);
                }
                ID_column.Add(temp_list);
            }
            RhinoDoc.ActiveDoc.Views.Redraw();

            foreach (var i in beam1)
            {
                List<Guid> temp_list = new List<Guid>();
                foreach (var j in i)
                {
                    var tempID = RhinoDoc.ActiveDoc.Objects.AddLine(j);
                    RhinoDoc.ActiveDoc.Objects.Select(tempID);
                    temp_list.Add(tempID);
                }
                ID_beam1.Add(temp_list);
            }
            RhinoDoc.ActiveDoc.Views.Redraw();

            foreach (var i in beam2)
            {
                List<Guid> temp_list = new List<Guid>();
                foreach (var j in i)
                {
                    var tempID = RhinoDoc.ActiveDoc.Objects.AddLine(j);
                    RhinoDoc.ActiveDoc.Objects.Select(tempID);
                    temp_list.Add(tempID);
                }
                ID_beam2.Add(temp_list);
            }
            RhinoDoc.ActiveDoc.Views.Redraw();

            foreach (var i in wall)
            {
                List<Guid> temp_list = new List<Guid>();
                foreach (var j in i)
                {
                    var tempID = RhinoDoc.ActiveDoc.Objects.AddLine(j);
                    RhinoDoc.ActiveDoc.Objects.Select(tempID);
                    temp_list.Add(tempID);
                }
                ID_wall.Add(temp_list);
            }
            RhinoDoc.ActiveDoc.Views.Redraw();

            foreach (var i in brace1)
            {
                List<Guid> temp_list = new List<Guid>();
                foreach (var j in i)
                {
                    var tempID = RhinoDoc.ActiveDoc.Objects.AddLine(j);
                    RhinoDoc.ActiveDoc.Objects.Select(tempID);
                    temp_list.Add(tempID);
                }
                ID_brace1.Add(temp_list);
            }
            RhinoDoc.ActiveDoc.Views.Redraw();

            foreach (var i in brace2)
            {
                List<Guid> temp_list = new List<Guid>();
                foreach (var j in i)
                {
                    var tempID = RhinoDoc.ActiveDoc.Objects.AddLine(j);
                    RhinoDoc.ActiveDoc.Objects.Select(tempID);
                    temp_list.Add(tempID);
                }
                ID_brace2.Add(temp_list);
            }
            RhinoDoc.ActiveDoc.Views.Redraw();
        }
        private void myHighlight()
        {
            myHighlightColumn();
            myHighlightBeam1();
            myHighlightBeam2();
            myHighlightWall();
            myHighlightBrace1();
            myHighlightBrace2();
        }
        private void myHighlightColumn()
        {
            if (highlight_column)
            {
                foreach (var i in highlight_flr)
                {
                    try
                    {
                        foreach (var j in ID_column[i - 2])
                        {
                            RhinoDoc.ActiveDoc.Objects.Select(j);
                        }
                    }
                    catch { }
                }
            }
            RhinoDoc.ActiveDoc.Views.Redraw();
        }
        private void myHighlightBeam1()
        {
            if (highlight_beam1)
            {
                foreach (var i in highlight_flr)
                {
                    try
                    {
                        foreach (var j in ID_beam1[i - 1])
                        {
                            RhinoDoc.ActiveDoc.Objects.Select(j);
                        }
                    }
                    catch { }
                }
            }
            RhinoDoc.ActiveDoc.Views.Redraw();
        }
        private void myHighlightBeam2()
        {
            if (highlight_beam2)
            {
                foreach (var i in highlight_flr)
                {
                    try
                    {
                        foreach (var j in ID_beam2[i - 1])
                        {
                            RhinoDoc.ActiveDoc.Objects.Select(j);
                        }
                    }
                    catch { }
                }
            }
            RhinoDoc.ActiveDoc.Views.Redraw();
        }
        private void myHighlightWall()
        {
            if (highlight_wall)
            {
                foreach (var i in highlight_flr)
                {
                    try
                    {
                        foreach (var j in ID_wall[i - 1])
                        {
                            RhinoDoc.ActiveDoc.Objects.Select(j);
                        }
                    }
                    catch { }
                }
            }
            RhinoDoc.ActiveDoc.Views.Redraw();
        }
        private void myHighlightBrace1()
        {
            if (highlight_brace1)
            {
                foreach (var i in highlight_flr)
                {
                    try
                    {
                        foreach (var j in ID_brace1[i - 1])
                        {
                            RhinoDoc.ActiveDoc.Objects.Select(j);
                        }
                    }
                    catch { }
                }
            }
            RhinoDoc.ActiveDoc.Views.Redraw();
        }
        private void myHighlightBrace2()
        {
            if (highlight_brace2)
            {
                foreach (var i in highlight_flr)
                {
                    try
                    {
                        foreach (var j in ID_brace2[i - 1])
                        {
                            RhinoDoc.ActiveDoc.Objects.Select(j);
                        }
                    }
                    catch { }
                }
            }
            RhinoDoc.ActiveDoc.Views.Redraw();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (this.dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("未选中任何单元格", "提示：", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataGridViewCell cell in this.dataGridView1.SelectedCells)
                {
                    this.dataGridView1.Rows.RemoveAt(cell.RowIndex);
                }
            }

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null)
                {
                    if (e.ColumnIndex < 2)
                    {
                        if (int.TryParse(cell.Value.ToString(), out int value) && value > 0)
                        {
                            cell.Value = value.ToString();
                        }
                        else
                        {
                            cell.Value = null;
                            MessageBox.Show("楼层号需为正整数", "提示：", MessageBoxButtons.OK);
                            return;
                        }
                    }
                    else
                    {
                        if (float.TryParse(cell.Value.ToString(), out float value) && value >= 0)
                        {
                            cell.Value = value.ToString();
                        }
                        else
                        {
                            cell.Value = null;
                            MessageBox.Show("参数需为非负实数", "提示：", MessageBoxButtons.OK);
                            return;

                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value == null || dataGridView1.Rows[i].Cells[1].Value == null)
                {
                    MessageBox.Show("楼层号不能为空", "提示：", MessageBoxButtons.OK);
                    this.dataGridView1.Rows[i].Selected = true;
                    return;
                }
                if (int.TryParse(dataGridView1.Rows[i].Cells[0].Value.ToString(), out int value1) && int.TryParse(dataGridView1.Rows[i].Cells[1].Value.ToString(), out int value2))
                {
                    if (value1 > value2)
                    {
                        MessageBox.Show("起始楼层号不能大于终止楼层号", "提示：", MessageBoxButtons.OK);
                        this.dataGridView1.Rows[i].Selected = true;
                        return;
                    }
                }
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int.TryParse(this.dataGridView1.Rows[i].Cells[0].Value.ToString(), out int value1);
                int.TryParse(this.dataGridView1.Rows[i].Cells[1].Value.ToString(), out int value2);
                for (int j = value1; j <= value2; j++)
                {

                }
            }
            this.tabControl1.SelectedIndex = 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var go = new GetObject();
            go.SetCommandPrompt("Select an object");
            go.EnablePreSelect(true, true);
            go.GetMultiple(1, 0);
            outface = go.Object(0);
            if (outface != null)
            {
                this.button4.BackColor = Color.FromArgb(255, 195, 0);
                button4.Update();
            }

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            outface = null;
            this.button4.BackColor = Color.FromArgb(0, 0, 0, 0);
            button4.Update();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var go = new GetObject();
            go.SetCommandPrompt("Select an object");
            go.EnablePreSelect(true, true);
            go.GetMultiple(1, 0);
            inface = go.Object(0);
            if (outface != null)
            {
                this.button5.BackColor = Color.FromArgb(255, 195, 0);
                button5.Update();
            }
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            outface = null;
            this.button5.BackColor = Color.FromArgb(0, 0, 0, 0);
            button5.Update();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (outface == null || inface == null)
            {
                MessageBox.Show("请先选择外立面和核心筒", "提示：", MessageBoxButtons.OK);
                return;
            }
            if (this.dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("请输入楼层表", "提示：", MessageBoxButtons.OK);
                return;
            }
            height.Clear();
            for (int i = 0; i < this.dataGridView2.Rows.Count; i++)
            {
                for (int j = int.Parse(this.dataGridView2.Rows[i].Cells[0].Value.ToString()); j <= int.Parse(this.dataGridView2.Rows[i].Cells[1].Value.ToString()); j++)
                {
                    if (height.Count == 0)
                    {
                        height.Add((double.Parse(this.dataGridView2.Rows[i].Cells[2].Value.ToString())) / 1000);
                    }
                    else
                    {
                        height.Add(height[height.Count - 1] + (double.Parse(this.dataGridView2.Rows[i].Cells[2].Value.ToString())) / 1000);
                    }
                }
            }
            generateStructure();
            MessageBox.Show("生成完毕");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.Rows.Count == 0)
            {
                this.dataGridView2.Rows.Add();
                this.dataGridView2.Rows[0].Cells[0].Value = 1;
                this.dataGridView2.Rows[0].Cells[0].ReadOnly = true;
            }
            else
            {
                if (this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[1].Value == null)
                {
                    MessageBox.Show("请输入终止层", "提示：", MessageBoxButtons.OK);
                    return;
                }
                if (int.Parse(this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[1].Value.ToString()) < int.Parse(this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[0].Value.ToString()))
                {
                    MessageBox.Show("终止层层号不得小于起始层", "提示：", MessageBoxButtons.OK);
                    return;
                }
                if (this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[2].Value == null)
                {
                    MessageBox.Show("请输入层高", "提示：", MessageBoxButtons.OK);
                    return;
                }
                this.dataGridView2.Rows.Add();
                var a = this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 2].Cells[1].Value;
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[0].Value = int.Parse(this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 2].Cells[1].Value?.ToString()) + 1;
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[0].ReadOnly = true;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.SelectedCells.Count == 0)
            {
                MessageBox.Show("未选中任何单元格", "提示：", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataGridViewCell cell in this.dataGridView2.SelectedCells)
                {
                    this.dataGridView2.Rows.RemoveAt(cell.RowIndex);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.dataGridView3.Rows.Add();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button36_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 1;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            highlight_flr.Clear();
            if (this.button18.BackColor == Color.Transparent)
            {
                this.button18.BackColor = Color.FromArgb(255, 195, 0);
                foreach (var i in highlight_all)
                {
                    highlight_flr.Add(i);
                }
            }
            else
            {
                this.button18.BackColor = Color.Transparent;
            }
            myHighlight();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (this.dataGridView3.SelectedCells.Count == 0)
            {
                MessageBox.Show("未选中任何单元格", "提示：", MessageBoxButtons.OK);
                return;
            }
            else
            {
                foreach (DataGridViewCell cell in this.dataGridView3.SelectedCells)
                {
                    if (cell.RowIndex < 0)
                    {
                        MessageBox.Show("未选中任何单元格", "提示：", MessageBoxButtons.OK);
                        return;
                    }
                    this.dataGridView3.Rows.RemoveAt(cell.RowIndex);
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (this.dataGridView3.SelectedCells.Count == 0)
            {
                MessageBox.Show("此结构将不含腰部桁架", "提示：", MessageBoxButtons.OK);
                return;
            }
            else
            {
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    if (dataGridView3.Rows[i].Cells[0].Value == null || dataGridView3.Rows[i].Cells[1].Value == null)
                    {
                        MessageBox.Show("楼层号不能为空", "提示：", MessageBoxButtons.OK);
                        this.dataGridView3.Rows[i].Selected = true;
                        return;
                    }
                    if (int.TryParse(dataGridView3.Rows[i].Cells[0].Value.ToString(), out int value1) && int.TryParse(dataGridView3.Rows[i].Cells[1].Value.ToString(), out int value2))
                    {
                        if (value1 > value2)
                        {
                            MessageBox.Show("起始楼层号不能大于终止楼层号", "提示：", MessageBoxButtons.OK);
                            this.dataGridView3.Rows[i].Selected = true;
                            return;
                        }
                    }
                }
            }
            waist.Clear();
            segment_arm.Clear();
            type_arm.Clear();
            segment_round.Clear();
            type_round.Clear();
            for (int i = 0; i < this.dataGridView3.Rows.Count; i++)
            {
                for (int j = int.Parse(this.dataGridView3.Rows[i].Cells[0].Value.ToString()); j <= int.Parse(this.dataGridView3.Rows[i].Cells[1].Value.ToString()); j++)
                {
                    waist.Add(j);
                    type_arm.Add(int.Parse(this.dataGridView3.Rows[i].Cells[2].Value.ToString()));
                    segment_arm.Add(int.Parse(this.dataGridView3.Rows[i].Cells[3].Value.ToString()));
                    type_round.Add(int.Parse(this.dataGridView3.Rows[i].Cells[4].Value.ToString()));
                    segment_round.Add(int.Parse(this.dataGridView3.Rows[i].Cells[5].Value.ToString()));
                }
                //waist=waist.Distinct().ToList();
            }
            generateStructure();
        }



        private void button19_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex < this.comboBox1.Items.Count - 1)
            {
                RhinoDoc.ActiveDoc.Objects.UnselectAll();
                RhinoDoc.ActiveDoc.Views.Redraw();
                this.comboBox1.SelectedIndex += 1;
                highlight_flr.Clear();
                highlight_flr.Add(this.comboBox1.SelectedIndex);
                myHighlight();
            }
            else
            {
                MessageBox.Show("当前为顶层", "提示：", MessageBoxButtons.OK);
                return;
            }

        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex > 0)
            {
                RhinoDoc.ActiveDoc.Objects.UnselectAll();
                RhinoDoc.ActiveDoc.Views.Redraw();
                this.comboBox1.SelectedIndex -= 1;
                highlight_flr.Clear();
                highlight_flr.Add(this.comboBox1.SelectedIndex);
                myHighlight();
            }
            else
            {
                MessageBox.Show("当前为底层", "提示：", MessageBoxButtons.OK);
                return;
            }
        }
        private void button21_Click(object sender, EventArgs e)
        {
            RhinoDoc.ActiveDoc.Objects.UnselectAll();
            RhinoDoc.ActiveDoc.Views.Redraw();
            if (this.button21.BackColor == Color.Transparent)
            {
                this.button21.BackColor = Color.FromArgb(255, 195, 0);
                highlight_column = true;
            }
            else
            {
                this.button21.BackColor = Color.Transparent;
                highlight_column = false;
            }
            myHighlight();
        }
        private void button22_Click(object sender, EventArgs e)
        {
            RhinoDoc.ActiveDoc.Objects.UnselectAll();
            RhinoDoc.ActiveDoc.Views.Redraw();
            if (this.button22.BackColor == Color.Transparent)
            {
                this.button22.BackColor = Color.FromArgb(255, 195, 0);
                highlight_beam1 = true;
            }
            else
            {
                this.button22.BackColor = Color.Transparent;
                highlight_beam1 = false;
            }
            myHighlight();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            RhinoDoc.ActiveDoc.Objects.UnselectAll();
            RhinoDoc.ActiveDoc.Views.Redraw();
            if (this.button23.BackColor == Color.Transparent)
            {
                this.button23.BackColor = Color.FromArgb(255, 195, 0);
                highlight_beam2 = true;
            }
            else
            {
                this.button23.BackColor = Color.Transparent;
                highlight_beam2 = false;
            }
            myHighlight();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            RhinoDoc.ActiveDoc.Objects.UnselectAll();
            RhinoDoc.ActiveDoc.Views.Redraw();
            if (this.button24.BackColor == Color.Transparent)
            {
                this.button24.BackColor = Color.FromArgb(255, 195, 0);
                highlight_wall = true;
            }
            else
            {
                this.button24.BackColor = Color.Transparent;
                highlight_wall = false;
            }
            myHighlight();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            RhinoDoc.ActiveDoc.Objects.UnselectAll();
            RhinoDoc.ActiveDoc.Views.Redraw();
            if (this.button25.BackColor == Color.Transparent)
            {
                this.button25.BackColor = Color.FromArgb(255, 195, 0);
                highlight_brace1 = true;
            }
            else
            {
                this.button25.BackColor = Color.Transparent;
                highlight_brace1 = false;
            }
            myHighlight();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            RhinoDoc.ActiveDoc.Objects.UnselectAll();
            RhinoDoc.ActiveDoc.Views.Redraw();
            if (this.button26.BackColor == Color.Transparent)
            {
                this.button26.BackColor = Color.FromArgb(255, 195, 0);
                highlight_brace2 = true;
            }
            else
            {
                this.button26.BackColor = Color.Transparent;
                highlight_brace2 = false;
            }
            myHighlight();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RhinoDoc.ActiveDoc.Objects.UnselectAll();
            RhinoDoc.ActiveDoc.Views.Redraw();
            highlight_flr.Clear();
            if (int.TryParse(this.comboBox1.SelectedItem.ToString(), out int value))
            {
                highlight_flr.Add(value);
            }
            myHighlight();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }


        private Thread thread_generate = new Thread(() => { });
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.textBox5.Text = this.trackBar1.Value.ToString();
            num_col = this.trackBar1.Value;
            generateStructure();
            //if (thread_generate.IsAlive)
            //{
            //    thread_generate.Abort();

            //}
            //thread_generate = new Thread(() => { generateStructure(); });
            //thread_generate.Start();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }



        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            this.textBox6.Text = this.trackBar2.Value.ToString();
            num_corner = this.trackBar2.Value;
            generateStructure();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            this.textBox7.Text = this.trackBar4.Value.ToString();
            ba = (double)this.trackBar4.Value / 1000;
            generateStructure();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox10.Text = openFileDialog.FileName;
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 3;
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void button30_Click(object sender, EventArgs e)
        {
            this.dataGridView4.Rows.Add();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (this.dataGridView4.SelectedCells.Count == 0)
            {
                MessageBox.Show("未选中任何单元格", "提示：", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataGridViewCell cell in this.dataGridView4.SelectedCells)
                {
                    this.dataGridView4.Rows.RemoveAt(cell.RowIndex);
                }
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            this.dataGridView5.Rows.Add();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (this.dataGridView5.SelectedCells.Count == 0)
            {
                MessageBox.Show("未选中任何单元格", "提示：", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataGridViewCell cell in this.dataGridView5.SelectedCells)
                {
                    this.dataGridView5.Rows.RemoveAt(cell.RowIndex);
                }
            }
        }

        private void button30_Click_1(object sender, EventArgs e)
        {
            this.dataGridView4.Rows.Add();
        }

        private void button31_Click_1(object sender, EventArgs e)
        {
            if (this.dataGridView4.SelectedCells.Count == 0)
            {
                MessageBox.Show("未选中任何单元格", "提示：", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataGridViewCell cell in this.dataGridView4.SelectedCells)
                {
                    this.dataGridView4.Rows.RemoveAt(cell.RowIndex);
                }
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            this.dataGridView6.Rows.Add();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (this.dataGridView6.SelectedCells.Count == 0)
            {
                MessageBox.Show("未选中任何单元格", "提示：", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataGridViewCell cell in this.dataGridView6.SelectedCells)
                {
                    this.dataGridView6.Rows.RemoveAt(cell.RowIndex);
                }
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            this.dataGridView7.Rows.Add();
        }

        private void button38_Click(object sender, EventArgs e)
        {
            if (this.dataGridView7.SelectedCells.Count == 0)
            {
                MessageBox.Show("未选中任何单元格", "提示：", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataGridViewCell cell in this.dataGridView7.SelectedCells)
                {
                    this.dataGridView7.Rows.RemoveAt(cell.RowIndex);
                }
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            this.dataGridView8.Rows.Add();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            if (this.dataGridView8.SelectedCells.Count == 0)
            {
                MessageBox.Show("未选中任何单元格", "提示：", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataGridViewCell cell in this.dataGridView8.SelectedCells)
                {
                    this.dataGridView8.Rows.RemoveAt(cell.RowIndex);
                }
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            this.dataGridView9.Rows.Add();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            if (this.dataGridView9.SelectedCells.Count == 0)
            {
                MessageBox.Show("未选中任何单元格", "提示：", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataGridViewCell cell in this.dataGridView9.SelectedCells)
                {
                    this.dataGridView9.Rows.RemoveAt(cell.RowIndex);
                }
            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            this.dataGridView10.Rows.Add();
        }

        private void button44_Click(object sender, EventArgs e)
        {
            if (this.dataGridView10.SelectedCells.Count == 0)
            {
                MessageBox.Show("未选中任何单元格", "提示：", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataGridViewCell cell in this.dataGridView10.SelectedCells)
                {
                    this.dataGridView10.Rows.RemoveAt(cell.RowIndex);
                }
            }
        }
    }
}
