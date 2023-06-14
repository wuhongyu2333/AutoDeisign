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

namespace myWindowsForms
{
    public partial class Form1 : Form
    {

        //变量声明
        ObjRef outface;
        ObjRef inface;
        List<double> height = new List<double>();
        List<List<Line>> column = new List<List<Line>>();
        List<List<Line>> beam1 = new List<List<Line>>();
        List<List<Line>> beam2 = new List<List<Line>>();
        List<List<Line>> wall = new List<List<Line>>();
        List<List<Line>> brace1 = new List<List<Line>>();
        List<List<Line>> brace2 = new List<List<Line>>();

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle91 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle92 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle93 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle94 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle95 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle96 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
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
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
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
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel9 = new System.Windows.Forms.Panel();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.panel10 = new System.Windows.Forms.Panel();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.panel11 = new System.Windows.Forms.Panel();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
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
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
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
            this.button36.Location = new System.Drawing.Point(11, 343);
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
            this.groupBox3.Location = new System.Drawing.Point(0, 178);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(360, 202);
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
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 165);
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
            dataGridViewCellStyle91.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle91.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle91.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle91.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle91.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle91.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle91.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle91;
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
            this.dataGridView3.RowTemplate.Height = 30;
            this.dataGridView3.Size = new System.Drawing.Size(354, 143);
            this.dataGridView3.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle92.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle92;
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
            this.Column13.HeaderText = "伸臂桁架";
            this.Column13.Name = "Column13";
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
            this.Column14.Name = "Column14";
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
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 46);
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
            this.label2.Size = new System.Drawing.Size(98, 46);
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
            this.label3.Size = new System.Drawing.Size(98, 48);
            this.label3.TabIndex = 3;
            this.label3.Text = "基底标高";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(280, 11);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(65, 23);
            this.textBox2.TabIndex = 11;
            this.textBox2.Text = "10";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(280, 57);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(65, 23);
            this.textBox3.TabIndex = 12;
            this.textBox3.Text = "0";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.Location = new System.Drawing.Point(280, 104);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(65, 23);
            this.textBox4.TabIndex = 13;
            this.textBox4.Text = "0";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            dataGridViewCellStyle93.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle93.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle93.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle93.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle93.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle93.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle93.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle93;
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
            this.dataGridView2.RowTemplate.Height = 30;
            this.dataGridView2.Size = new System.Drawing.Size(294, 165);
            this.dataGridView2.TabIndex = 1;
            // 
            // Column10
            // 
            dataGridViewCellStyle94.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle94.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle94.ForeColor = System.Drawing.Color.Black;
            this.Column10.DefaultCellStyle = dataGridViewCellStyle94;
            this.Column10.FillWeight = 90F;
            this.Column10.HeaderText = "起始层";
            this.Column10.Name = "Column10";
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column10.Width = 93;
            // 
            // Column11
            // 
            dataGridViewCellStyle95.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle95.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle95.ForeColor = System.Drawing.Color.Black;
            this.Column11.DefaultCellStyle = dataGridViewCellStyle95;
            this.Column11.FillWeight = 90F;
            this.Column11.HeaderText = "终止层";
            this.Column11.Name = "Column11";
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column11.Width = 93;
            // 
            // Column12
            // 
            dataGridViewCellStyle96.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle96.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle96.ForeColor = System.Drawing.Color.Black;
            this.Column12.DefaultCellStyle = dataGridViewCellStyle96;
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
            this.tabPage2.Controls.Add(this.panel6);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(940, 415);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "荷载施加";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel5);
            this.panel6.Controls.Add(this.button3);
            this.panel6.Location = new System.Drawing.Point(431, 370);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(470, 30);
            this.panel6.TabIndex = 6;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.button2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(310, 30);
            this.panel5.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.Image = global::myWindowsForms.Properties.Resources.add;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 30);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button2_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.Image = global::myWindowsForms.Properties.Resources.delete;
            this.button2.Location = new System.Drawing.Point(156, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 30);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Right;
            this.button3.Image = global::myWindowsForms.Properties.Resources.check;
            this.button3.Location = new System.Drawing.Point(320, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 30);
            this.button3.TabIndex = 4;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
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
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 20;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.Size = new System.Drawing.Size(934, 361);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "起始层";
            this.Column1.Name = "Column1";
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "终止层";
            this.Column2.Name = "Column2";
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "核心筒内恒载(KPa)";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "核心筒内活载(KPa)";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "核心筒内板厚(mm)";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "核心筒外恒载(KPa)";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "核心筒外活载(KPa)";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "核心筒外板厚(mm)";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "框线荷载(KPa)";
            this.Column9.Name = "Column9";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(940, 415);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "截面初始化";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(940, 415);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "前处理参数设置";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.tabPage4.Click += new System.EventHandler(this.tabPage4_Click);
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
            // panel9
            // 
            this.panel9.Controls.Add(this.textBox5);
            this.panel9.Controls.Add(this.trackBar1);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(141, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(133, 40);
            this.panel9.TabIndex = 17;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.White;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox5.Location = new System.Drawing.Point(0, 24);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(133, 16);
            this.textBox5.TabIndex = 7;
            // 
            // trackBar1
            // 
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar1.Location = new System.Drawing.Point(0, 0);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(133, 45);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.textBox6);
            this.panel10.Controls.Add(this.trackBar2);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(141, 49);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(133, 40);
            this.panel10.TabIndex = 18;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.White;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox6.Location = new System.Drawing.Point(0, 24);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(133, 16);
            this.textBox6.TabIndex = 7;
            // 
            // trackBar2
            // 
            this.trackBar2.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar2.Location = new System.Drawing.Point(0, 0);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(133, 45);
            this.trackBar2.TabIndex = 6;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.textBox7);
            this.panel11.Controls.Add(this.trackBar4);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(141, 95);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(133, 42);
            this.panel11.TabIndex = 19;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.White;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox7.Location = new System.Drawing.Point(0, 26);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(133, 16);
            this.textBox7.TabIndex = 7;
            // 
            // trackBar4
            // 
            this.trackBar4.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar4.Location = new System.Drawing.Point(0, 0);
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Size = new System.Drawing.Size(133, 45);
            this.trackBar4.TabIndex = 6;
            this.trackBar4.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(107, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(28, 16);
            this.textBox1.TabIndex = 20;
            this.textBox1.Text = "0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox8
            // 
            this.textBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.Location = new System.Drawing.Point(107, 61);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(28, 16);
            this.textBox8.TabIndex = 21;
            this.textBox8.Text = "0";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox9
            // 
            this.textBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox9.Location = new System.Drawing.Point(107, 108);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(28, 16);
            this.textBox9.TabIndex = 22;
            this.textBox9.Text = "0";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
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

            double ba = 0;
            double dis = 0;
            int num_col = 4;
            int num_corner = 0;

            List<int> waist = new List<int>();
            List<int> segment = new List<int>();
            List<int> type = new List<int>();
            List<int> segment_round = new List<int>();
            List<int> type_round = new List<int>();


            num_col = num_col >= 4 ? num_col : 4;
            num_col = num_col % 2 == 1 ? ++num_col : num_col;
            num_corner = num_corner >= 0 ? num_corner : 0;

            num_col = 12;
            num_corner = 2;

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
                int temp_segment = 0;
                int temp_type = 1;
                for (int i = 0; i < waist.Count; i++)
                {
                    List<Line> temp_brace1 = new List<Line>();
                    temp_waist = waist[i];
                    if (temp_waist >= waist_hor.Count - 1)
                    {
                        break;
                    }

                    if (segment.Count > i)
                    {
                        temp_segment = segment[i];
                    }
                    else
                    {
                        temp_segment = (int)(waist_hor[temp_waist][0].Length / Math.Abs(waist_hor[temp_waist][0].FromZ - waist_hor[temp_waist + 1][0].FromZ));
                    }
                    if (type.Count > i)
                    {
                        temp_type = type[i];
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
            foreach (var i in column)
            {
                foreach (var j in i)
                {
                    RhinoDoc.ActiveDoc.Objects.AddLine(j);
                    RhinoDoc.ActiveDoc.Objects.Select(RhinoDoc.ActiveDoc.Objects.AddLine(j));
                }
            }
            RhinoDoc.ActiveDoc.Views.Redraw();

            foreach (var i in beam1)
            {
                foreach (var j in i)
                {
                    RhinoDoc.ActiveDoc.Objects.AddLine(j);
                    RhinoDoc.ActiveDoc.Objects.Select(RhinoDoc.ActiveDoc.Objects.AddLine(j));
                }
            }
            RhinoDoc.ActiveDoc.Views.Redraw();

            foreach (var i in beam2)
            {
                foreach (var j in i)
                {
                    RhinoDoc.ActiveDoc.Objects.AddLine(j);
                    RhinoDoc.ActiveDoc.Objects.Select(RhinoDoc.ActiveDoc.Objects.AddLine(j));
                }
            }
            RhinoDoc.ActiveDoc.Views.Redraw();

            foreach (var i in wall)
            {
                foreach (var j in i)
                {
                    RhinoDoc.ActiveDoc.Objects.AddLine(j);
                    RhinoDoc.ActiveDoc.Objects.Select(RhinoDoc.ActiveDoc.Objects.AddLine(j));
                }
            }
            RhinoDoc.ActiveDoc.Views.Redraw();

            foreach (var i in brace1)
            {
                foreach (var j in i)
                {
                    RhinoDoc.ActiveDoc.Objects.AddLine(j);
                    RhinoDoc.ActiveDoc.Objects.Select(RhinoDoc.ActiveDoc.Objects.AddLine(j));
                }
            }
            RhinoDoc.ActiveDoc.Views.Redraw();

            foreach (var i in brace2)
            {
                foreach (var j in i)
                {
                    RhinoDoc.ActiveDoc.Objects.AddLine(j);
                    RhinoDoc.ActiveDoc.Objects.Select(RhinoDoc.ActiveDoc.Objects.AddLine(j));
                }
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
                        foreach (var j in column[i-2])
                        {
                            RhinoDoc.ActiveDoc.Objects.Select(RhinoDoc.ActiveDoc.Objects.AddLine(j));
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
                        foreach (var j in beam1[i - 1])
                        {
                            RhinoDoc.ActiveDoc.Objects.Select(RhinoDoc.ActiveDoc.Objects.AddLine(j));
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
                        foreach (var j in beam2[i - 1])
                        {
                            RhinoDoc.ActiveDoc.Objects.Select(RhinoDoc.ActiveDoc.Objects.AddLine(j));
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
                        foreach (var j in wall[i - 1])
                        {
                            RhinoDoc.ActiveDoc.Objects.Select(RhinoDoc.ActiveDoc.Objects.AddLine(j));
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
                        foreach (var j in brace1[i - 1])
                        {
                            RhinoDoc.ActiveDoc.Objects.Select(RhinoDoc.ActiveDoc.Objects.AddLine(j));
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
                        foreach (var j in brace2[i - 1])
                        {
                            RhinoDoc.ActiveDoc.Objects.Select(RhinoDoc.ActiveDoc.Objects.AddLine(j));
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
                        height.Add(double.Parse(this.dataGridView2.Rows[i].Cells[2].Value.ToString()));
                    }
                    else
                    {
                        height.Add(height[height.Count - 1] + double.Parse(this.dataGridView2.Rows[i].Cells[2].Value.ToString()));
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
    }
}
