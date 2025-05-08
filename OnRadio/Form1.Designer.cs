using Microsoft.Win32;

namespace OnRadio
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Redactor_R = new System.Windows.Forms.PictureBox();
            this.picBox_equalizer_icon = new System.Windows.Forms.PictureBox();
            this.bt_effect4 = new System.Windows.Forms.Button();
            this.bt_effect3 = new System.Windows.Forms.Button();
            this.bt_effect2 = new System.Windows.Forms.Button();
            this.bt_effect1 = new System.Windows.Forms.Button();
            this.vrema = new System.Windows.Forms.Label();
            this.picBox_record_bt = new System.Windows.Forms.PictureBox();
            this.picBox_sound_icon = new System.Windows.Forms.PictureBox();
            this.colorSlider1 = new MB.Controls.ColorSlider();
            this.lab_sound_level = new System.Windows.Forms.Label();
            this.Combo_lang = new System.Windows.Forms.ComboBox();
            this.comBox_search = new System.Windows.Forms.ComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.Tburl = new System.Windows.Forms.TextBox();
            this.TBname = new System.Windows.Forms.TextBox();
            this.picPly = new System.Windows.Forms.PictureBox();
            this.Localise_set = new System.Windows.Forms.PictureBox();
            this.PicBox_logo2 = new System.Windows.Forms.PictureBox();
            this.picserch = new System.Windows.Forms.PictureBox();
            this.bt_eject = new System.Windows.Forms.PictureBox();
            this.bt_eject1 = new System.Windows.Forms.PictureBox();
            this.bt_pause = new System.Windows.Forms.PictureBox();
            this.bt_stop = new System.Windows.Forms.PictureBox();
            this.bt_previous = new System.Windows.Forms.PictureBox();
            this.bt_next = new System.Windows.Forms.PictureBox();
            this.PicBox_logo = new System.Windows.Forms.PictureBox();
            this.picBox_collapse_icon = new System.Windows.Forms.PictureBox();
            this.bt_play = new System.Windows.Forms.PictureBox();
            this.picExit = new System.Windows.Forms.PictureBox();
            this.picSave = new System.Windows.Forms.PictureBox();
            this.picFdialog = new System.Windows.Forms.PictureBox();
            this.picDown = new System.Windows.Forms.PictureBox();
            this.picUp = new System.Windows.Forms.PictureBox();
            this.picDel = new System.Windows.Forms.PictureBox();
            this.picAdd = new System.Windows.Forms.PictureBox();
            this.AxWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bt_favorit = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.KOZ = new System.Windows.Forms.PictureBox();
            this.bt_re_favorit = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lab_vsego = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.lab_name_station = new System.Windows.Forms.Label();
            this.lab_URL = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Radio = new System.Windows.Forms.NotifyIcon(this.components);
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.Timer2 = new System.Windows.Forms.Timer(this.components);
            this.t = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.timer5 = new System.Windows.Forms.Timer(this.components);
            this.timer6 = new System.Windows.Forms.Timer(this.components);
            this.bgnThread = new System.ComponentModel.BackgroundWorker();
            this.timer7 = new System.Windows.Forms.Timer(this.components);
            this.StatusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Redactor_R)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_equalizer_icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_record_bt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_sound_icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Localise_set)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_logo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picserch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_eject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_eject1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_pause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_stop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_previous)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_next)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_collapse_icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_play)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFdialog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AxWindowsMediaPlayer1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KOZ)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.StatusStrip1, "StatusStrip1");
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.StatusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.SizingGrip = false;
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.Redactor_R);
            this.panel1.Controls.Add(this.picBox_equalizer_icon);
            this.panel1.Controls.Add(this.bt_effect4);
            this.panel1.Controls.Add(this.bt_effect3);
            this.panel1.Controls.Add(this.bt_effect2);
            this.panel1.Controls.Add(this.bt_effect1);
            this.panel1.Controls.Add(this.vrema);
            this.panel1.Controls.Add(this.picBox_record_bt);
            this.panel1.Controls.Add(this.picBox_sound_icon);
            this.panel1.Controls.Add(this.colorSlider1);
            this.panel1.Controls.Add(this.lab_sound_level);
            this.panel1.Name = "panel1";
            // 
            // Redactor_R
            // 
            this.Redactor_R.BackgroundImage = global::OnRadio.Properties.Resources.Redactor64;
            resources.ApplyResources(this.Redactor_R, "Redactor_R");
            this.Redactor_R.Name = "Redactor_R";
            this.Redactor_R.TabStop = false;
            this.ToolTip1.SetToolTip(this.Redactor_R, resources.GetString("Redactor_R.ToolTip"));
            this.Redactor_R.Click += new System.EventHandler(this.Redactor_R_Click);
            this.Redactor_R.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Redactor_R_MouseDown);
            this.Redactor_R.MouseEnter += new System.EventHandler(this.Redactor_R_MouseEnter);
            this.Redactor_R.MouseLeave += new System.EventHandler(this.Redactor_R_MouseLeave);
            this.Redactor_R.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Redactor_R_MouseUp);
            // 
            // picBox_equalizer_icon
            // 
            resources.ApplyResources(this.picBox_equalizer_icon, "picBox_equalizer_icon");
            this.picBox_equalizer_icon.Image = global::OnRadio.Properties.Resources.radio_online;
            this.picBox_equalizer_icon.Name = "picBox_equalizer_icon";
            this.picBox_equalizer_icon.TabStop = false;
            // 
            // bt_effect4
            // 
            resources.ApplyResources(this.bt_effect4, "bt_effect4");
            this.bt_effect4.Name = "bt_effect4";
            this.ToolTip1.SetToolTip(this.bt_effect4, resources.GetString("bt_effect4.ToolTip"));
            this.bt_effect4.UseVisualStyleBackColor = true;
            this.bt_effect4.Click += new System.EventHandler(this.button4_Click);
            this.bt_effect4.MouseEnter += new System.EventHandler(this.button4_MouseEnter);
            this.bt_effect4.MouseLeave += new System.EventHandler(this.button4_MouseLeave);
            // 
            // bt_effect3
            // 
            resources.ApplyResources(this.bt_effect3, "bt_effect3");
            this.bt_effect3.Name = "bt_effect3";
            this.ToolTip1.SetToolTip(this.bt_effect3, resources.GetString("bt_effect3.ToolTip"));
            this.bt_effect3.UseVisualStyleBackColor = true;
            this.bt_effect3.Click += new System.EventHandler(this.button3_Click);
            this.bt_effect3.MouseEnter += new System.EventHandler(this.button3_MouseEnter);
            this.bt_effect3.MouseLeave += new System.EventHandler(this.button3_MouseLeave);
            // 
            // bt_effect2
            // 
            resources.ApplyResources(this.bt_effect2, "bt_effect2");
            this.bt_effect2.Name = "bt_effect2";
            this.ToolTip1.SetToolTip(this.bt_effect2, resources.GetString("bt_effect2.ToolTip"));
            this.bt_effect2.UseVisualStyleBackColor = true;
            this.bt_effect2.Click += new System.EventHandler(this.button2_Click);
            this.bt_effect2.MouseEnter += new System.EventHandler(this.button2_MouseEnter);
            this.bt_effect2.MouseLeave += new System.EventHandler(this.button2_MouseLeave);
            // 
            // bt_effect1
            // 
            this.bt_effect1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.bt_effect1, "bt_effect1");
            this.bt_effect1.Name = "bt_effect1";
            this.ToolTip1.SetToolTip(this.bt_effect1, resources.GetString("bt_effect1.ToolTip"));
            this.bt_effect1.UseVisualStyleBackColor = false;
            this.bt_effect1.Click += new System.EventHandler(this.button1_Click);
            this.bt_effect1.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
            this.bt_effect1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            // 
            // vrema
            // 
            resources.ApplyResources(this.vrema, "vrema");
            this.vrema.BackColor = System.Drawing.Color.Transparent;
            this.vrema.ForeColor = System.Drawing.Color.DarkGreen;
            this.vrema.Name = "vrema";
            // 
            // picBox_record_bt
            // 
            resources.ApplyResources(this.picBox_record_bt, "picBox_record_bt");
            this.picBox_record_bt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_record_bt.Name = "picBox_record_bt";
            this.picBox_record_bt.TabStop = false;
            this.ToolTip1.SetToolTip(this.picBox_record_bt, resources.GetString("picBox_record_bt.ToolTip"));
            this.picBox_record_bt.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // picBox_sound_icon
            // 
            resources.ApplyResources(this.picBox_sound_icon, "picBox_sound_icon");
            this.picBox_sound_icon.BackgroundImage = global::OnRadio.Properties.Resources.kmixdocked_mute_4470;
            this.picBox_sound_icon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_sound_icon.Name = "picBox_sound_icon";
            this.picBox_sound_icon.TabStop = false;
            this.picBox_sound_icon.Click += new System.EventHandler(this.pictureBox1_Click);
            this.picBox_sound_icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.picBox_sound_icon.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.picBox_sound_icon.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.picBox_sound_icon.MouseHover += new System.EventHandler(this.pictureBox1_MouseHover);
            this.picBox_sound_icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // colorSlider1
            // 
            resources.ApplyResources(this.colorSlider1, "colorSlider1");
            this.colorSlider1.BackColor = System.Drawing.Color.Transparent;
            this.colorSlider1.BarInnerColor = System.Drawing.Color.RoyalBlue;
            this.colorSlider1.BarOuterColor = System.Drawing.Color.DodgerBlue;
            this.colorSlider1.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.colorSlider1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.colorSlider1.ElapsedInnerColor = System.Drawing.Color.LawnGreen;
            this.colorSlider1.LargeChange = ((uint)(5u));
            this.colorSlider1.Name = "colorSlider1";
            this.colorSlider1.SmallChange = ((uint)(1u));
            this.colorSlider1.ThumbInnerColor = System.Drawing.Color.DimGray;
            this.colorSlider1.ThumbOuterColor = System.Drawing.Color.Silver;
            this.colorSlider1.ThumbPenColor = System.Drawing.Color.DimGray;
            this.colorSlider1.ThumbRoundRectSize = new System.Drawing.Size(20, 20);
            this.colorSlider1.ThumbSize = 20;
            this.ToolTip1.SetToolTip(this.colorSlider1, resources.GetString("colorSlider1.ToolTip"));
            this.colorSlider1.Value = 10;
            this.colorSlider1.ValueChanged += new System.EventHandler(this.colorSlider1_ValueChanged);
            this.colorSlider1.MouseEnter += new System.EventHandler(this.colorSlider1_MouseEnter);
            this.colorSlider1.MouseLeave += new System.EventHandler(this.colorSlider1_MouseLeave);
            this.colorSlider1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.colorSlider1_MouseUp);
            // 
            // lab_sound_level
            // 
            resources.ApplyResources(this.lab_sound_level, "lab_sound_level");
            this.lab_sound_level.ForeColor = System.Drawing.Color.Black;
            this.lab_sound_level.Name = "lab_sound_level";
            // 
            // Combo_lang
            // 
            resources.ApplyResources(this.Combo_lang, "Combo_lang");
            this.Combo_lang.BackColor = System.Drawing.SystemColors.Menu;
            this.Combo_lang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combo_lang.FormattingEnabled = true;
            this.Combo_lang.Items.AddRange(new object[] {
            resources.GetString("Combo_lang.Items"),
            resources.GetString("Combo_lang.Items1"),
            resources.GetString("Combo_lang.Items2")});
            this.Combo_lang.Name = "Combo_lang";
            this.ToolTip1.SetToolTip(this.Combo_lang, resources.GetString("Combo_lang.ToolTip"));
            this.Combo_lang.SelectedIndexChanged += new System.EventHandler(this.Combo_lang_SelectedIndexChanged);
            this.Combo_lang.SelectionChangeCommitted += new System.EventHandler(this.Combo_lang_SelectionChangeCommitted);
            this.Combo_lang.MouseLeave += new System.EventHandler(this.Combo_lang_MouseLeave);
            // 
            // comBox_search
            // 
            resources.ApplyResources(this.comBox_search, "comBox_search");
            this.comBox_search.DropDownWidth = 1;
            this.comBox_search.FormattingEnabled = true;
            this.comBox_search.Name = "comBox_search";
            this.comBox_search.Sorted = true;
            this.ToolTip1.SetToolTip(this.comBox_search, resources.GetString("comBox_search.ToolTip"));
            this.comBox_search.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comBox_search.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            this.comBox_search.Click += new System.EventHandler(this.comboBox1_Click);
            this.comBox_search.Leave += new System.EventHandler(this.comboBox1_Leave);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.listBox1, "listBox1");
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Name = "listBox1";
            this.ToolTip1.SetToolTip(this.listBox1, resources.GetString("listBox1.ToolTip"));
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            this.listBox1.MouseEnter += new System.EventHandler(this.listBox1_MouseEnter);
            this.listBox1.MouseLeave += new System.EventHandler(this.listBox1_MouseLeave);
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.listBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.listBox2, "listBox2");
            this.listBox2.ForeColor = System.Drawing.Color.Blue;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Name = "listBox2";
            this.ToolTip1.SetToolTip(this.listBox2, resources.GetString("listBox2.ToolTip"));
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            this.listBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox2_KeyDown);
            this.listBox2.MouseEnter += new System.EventHandler(this.listBox2_MouseEnter);
            this.listBox2.MouseLeave += new System.EventHandler(this.listBox2_MouseLeave);
            // 
            // Tburl
            // 
            resources.ApplyResources(this.Tburl, "Tburl");
            this.Tburl.Name = "Tburl";
            this.ToolTip1.SetToolTip(this.Tburl, resources.GetString("Tburl.ToolTip"));
            this.Tburl.DoubleClick += new System.EventHandler(this.Tburl_DoubleClick);
            this.Tburl.Enter += new System.EventHandler(this.Tburl_Enter);
            this.Tburl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tburl_KeyPress);
            // 
            // TBname
            // 
            resources.ApplyResources(this.TBname, "TBname");
            this.TBname.Name = "TBname";
            this.ToolTip1.SetToolTip(this.TBname, resources.GetString("TBname.ToolTip"));
            this.TBname.TextChanged += new System.EventHandler(this.TBname_TextChanged);
            this.TBname.DoubleClick += new System.EventHandler(this.TBname_DoubleClick);
            this.TBname.Enter += new System.EventHandler(this.TBname_Enter);
            this.TBname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBname_KeyPress);
            // 
            // picPly
            // 
            resources.ApplyResources(this.picPly, "picPly");
            this.picPly.BackgroundImage = global::OnRadio.Properties.Resources.ply;
            this.picPly.Name = "picPly";
            this.picPly.TabStop = false;
            this.ToolTip1.SetToolTip(this.picPly, resources.GetString("picPly.ToolTip"));
            this.picPly.Click += new System.EventHandler(this.picPly_Click);
            this.picPly.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPly_MouseDown);
            this.picPly.MouseEnter += new System.EventHandler(this.picPly_MouseEnter);
            this.picPly.MouseLeave += new System.EventHandler(this.picPly_MouseLeave);
            this.picPly.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picPly_MouseUp);
            // 
            // Localise_set
            // 
            resources.ApplyResources(this.Localise_set, "Localise_set");
            this.Localise_set.BackgroundImage = global::OnRadio.Properties.Resources.localisa;
            this.Localise_set.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Localise_set.Name = "Localise_set";
            this.Localise_set.TabStop = false;
            this.ToolTip1.SetToolTip(this.Localise_set, resources.GetString("Localise_set.ToolTip"));
            this.Localise_set.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Localise_set_MouseDown);
            this.Localise_set.MouseEnter += new System.EventHandler(this.Localise_set_MouseEnter);
            this.Localise_set.MouseLeave += new System.EventHandler(this.Localise_set_MouseLeave);
            this.Localise_set.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Localise_set_MouseUp);
            // 
            // PicBox_logo2
            // 
            resources.ApplyResources(this.PicBox_logo2, "PicBox_logo2");
            this.PicBox_logo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBox_logo2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PicBox_logo2.ErrorImage = global::OnRadio.Properties.Resources.no_image;
            this.PicBox_logo2.Image = global::OnRadio.Properties.Resources.no_image;
            this.PicBox_logo2.InitialImage = global::OnRadio.Properties.Resources.no_image;
            this.PicBox_logo2.Name = "PicBox_logo2";
            this.PicBox_logo2.TabStop = false;
            this.ToolTip1.SetToolTip(this.PicBox_logo2, resources.GetString("PicBox_logo2.ToolTip"));
            this.PicBox_logo2.DoubleClick += new System.EventHandler(this.pictureBox5_DoubleClick);
            // 
            // picserch
            // 
            this.picserch.BackColor = System.Drawing.Color.Transparent;
            this.picserch.BackgroundImage = global::OnRadio.Properties.Resources.SERCH;
            resources.ApplyResources(this.picserch, "picserch");
            this.picserch.Name = "picserch";
            this.picserch.TabStop = false;
            this.ToolTip1.SetToolTip(this.picserch, resources.GetString("picserch.ToolTip"));
            this.picserch.Click += new System.EventHandler(this.picserch_Click);
            this.picserch.MouseEnter += new System.EventHandler(this.picserch_MouseEnter);
            this.picserch.MouseLeave += new System.EventHandler(this.picserch_MouseLeave);
            // 
            // bt_eject
            // 
            this.bt_eject.BackColor = System.Drawing.Color.Transparent;
            this.bt_eject.BackgroundImage = global::OnRadio.Properties.Resources.button_eject;
            resources.ApplyResources(this.bt_eject, "bt_eject");
            this.bt_eject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_eject.Name = "bt_eject";
            this.bt_eject.TabStop = false;
            this.ToolTip1.SetToolTip(this.bt_eject, resources.GetString("bt_eject.ToolTip"));
            this.bt_eject.Click += new System.EventHandler(this.bt_eject_Click);
            this.bt_eject.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bt_eject_MouseDown);
            this.bt_eject.MouseEnter += new System.EventHandler(this.bt_eject_MouseEnter);
            this.bt_eject.MouseLeave += new System.EventHandler(this.bt_eject_MouseLeave);
            this.bt_eject.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_eject_MouseUp);
            // 
            // bt_eject1
            // 
            this.bt_eject1.BackColor = System.Drawing.Color.Transparent;
            this.bt_eject1.BackgroundImage = global::OnRadio.Properties.Resources.button_eject1;
            resources.ApplyResources(this.bt_eject1, "bt_eject1");
            this.bt_eject1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_eject1.Name = "bt_eject1";
            this.bt_eject1.TabStop = false;
            this.ToolTip1.SetToolTip(this.bt_eject1, resources.GetString("bt_eject1.ToolTip"));
            this.bt_eject1.Click += new System.EventHandler(this.bt_eject1_Click);
            this.bt_eject1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bt_eject1_MouseDown);
            this.bt_eject1.MouseEnter += new System.EventHandler(this.bt_eject1_MouseEnter);
            this.bt_eject1.MouseLeave += new System.EventHandler(this.bt_eject1_MouseLeave);
            this.bt_eject1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_eject1_MouseUp);
            // 
            // bt_pause
            // 
            this.bt_pause.BackColor = System.Drawing.Color.Transparent;
            this.bt_pause.BackgroundImage = global::OnRadio.Properties.Resources.button_pause;
            resources.ApplyResources(this.bt_pause, "bt_pause");
            this.bt_pause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_pause.Name = "bt_pause";
            this.bt_pause.TabStop = false;
            this.ToolTip1.SetToolTip(this.bt_pause, resources.GetString("bt_pause.ToolTip"));
            this.bt_pause.Click += new System.EventHandler(this.bt_pause_Click);
            this.bt_pause.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bt_pause_MouseDown);
            this.bt_pause.MouseEnter += new System.EventHandler(this.bt_pause_MouseEnter);
            this.bt_pause.MouseLeave += new System.EventHandler(this.bt_pause_MouseLeave);
            this.bt_pause.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_pause_MouseUp);
            // 
            // bt_stop
            // 
            this.bt_stop.BackColor = System.Drawing.Color.Transparent;
            this.bt_stop.BackgroundImage = global::OnRadio.Properties.Resources.button_stop;
            resources.ApplyResources(this.bt_stop, "bt_stop");
            this.bt_stop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_stop.Name = "bt_stop";
            this.bt_stop.TabStop = false;
            this.ToolTip1.SetToolTip(this.bt_stop, resources.GetString("bt_stop.ToolTip"));
            this.bt_stop.Click += new System.EventHandler(this.bt_stop_Click);
            this.bt_stop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bt_stop_MouseDown);
            this.bt_stop.MouseEnter += new System.EventHandler(this.bt_stop_MouseEnter);
            this.bt_stop.MouseLeave += new System.EventHandler(this.bt_stop_MouseLeave);
            this.bt_stop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_stop_MouseUp);
            // 
            // bt_previous
            // 
            this.bt_previous.BackColor = System.Drawing.Color.Transparent;
            this.bt_previous.BackgroundImage = global::OnRadio.Properties.Resources.button_previous;
            resources.ApplyResources(this.bt_previous, "bt_previous");
            this.bt_previous.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_previous.Name = "bt_previous";
            this.bt_previous.TabStop = false;
            this.ToolTip1.SetToolTip(this.bt_previous, resources.GetString("bt_previous.ToolTip"));
            this.bt_previous.Click += new System.EventHandler(this.bt_previous_Click);
            this.bt_previous.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bt_previous_MouseDown);
            this.bt_previous.MouseEnter += new System.EventHandler(this.bt_previous_MouseEnter);
            this.bt_previous.MouseLeave += new System.EventHandler(this.bt_previous_MouseLeave);
            this.bt_previous.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_previous_MouseUp);
            // 
            // bt_next
            // 
            this.bt_next.BackColor = System.Drawing.Color.Transparent;
            this.bt_next.BackgroundImage = global::OnRadio.Properties.Resources.button_next;
            resources.ApplyResources(this.bt_next, "bt_next");
            this.bt_next.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_next.Name = "bt_next";
            this.bt_next.TabStop = false;
            this.ToolTip1.SetToolTip(this.bt_next, resources.GetString("bt_next.ToolTip"));
            this.bt_next.Click += new System.EventHandler(this.bt_next_Click);
            this.bt_next.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bt_next_MouseDown);
            this.bt_next.MouseEnter += new System.EventHandler(this.bt_next_MouseEnter);
            this.bt_next.MouseLeave += new System.EventHandler(this.bt_next_MouseLeave);
            this.bt_next.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_next_MouseUp);
            // 
            // PicBox_logo
            // 
            resources.ApplyResources(this.PicBox_logo, "PicBox_logo");
            this.PicBox_logo.BackColor = System.Drawing.Color.Transparent;
            this.PicBox_logo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBox_logo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PicBox_logo.Name = "PicBox_logo";
            this.PicBox_logo.TabStop = false;
            this.ToolTip1.SetToolTip(this.PicBox_logo, resources.GetString("PicBox_logo.ToolTip"));
            this.PicBox_logo.Click += new System.EventHandler(this.PictureBox8_Click);
            // 
            // picBox_collapse_icon
            // 
            this.picBox_collapse_icon.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picBox_collapse_icon.BackgroundImage = global::OnRadio.Properties.Resources._39_b;
            resources.ApplyResources(this.picBox_collapse_icon, "picBox_collapse_icon");
            this.picBox_collapse_icon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_collapse_icon.Name = "picBox_collapse_icon";
            this.picBox_collapse_icon.TabStop = false;
            this.ToolTip1.SetToolTip(this.picBox_collapse_icon, resources.GetString("picBox_collapse_icon.ToolTip"));
            this.picBox_collapse_icon.Click += new System.EventHandler(this.pictureBox2_Click);
            this.picBox_collapse_icon.MouseEnter += new System.EventHandler(this.pictureBox2_MouseEnter);
            this.picBox_collapse_icon.MouseLeave += new System.EventHandler(this.pictureBox2_MouseLeave);
            // 
            // bt_play
            // 
            this.bt_play.BackColor = System.Drawing.Color.Transparent;
            this.bt_play.BackgroundImage = global::OnRadio.Properties.Resources.button_play;
            resources.ApplyResources(this.bt_play, "bt_play");
            this.bt_play.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_play.Name = "bt_play";
            this.bt_play.TabStop = false;
            this.ToolTip1.SetToolTip(this.bt_play, resources.GetString("bt_play.ToolTip"));
            this.bt_play.Click += new System.EventHandler(this.bt_play_Click);
            this.bt_play.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bt_play_MouseDown);
            this.bt_play.MouseEnter += new System.EventHandler(this.bt_play_MouseEnter);
            this.bt_play.MouseLeave += new System.EventHandler(this.bt_play_MouseLeave);
            this.bt_play.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_play_MouseUp);
            // 
            // picExit
            // 
            resources.ApplyResources(this.picExit, "picExit");
            this.picExit.BackgroundImage = global::OnRadio.Properties.Resources.exit;
            this.picExit.Name = "picExit";
            this.picExit.TabStop = false;
            this.ToolTip1.SetToolTip(this.picExit, resources.GetString("picExit.ToolTip"));
            this.picExit.MouseEnter += new System.EventHandler(this.picExit_MouseEnter);
            this.picExit.MouseLeave += new System.EventHandler(this.picExit_MouseLeave);
            this.picExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picExit_MouseUp);
            // 
            // picSave
            // 
            resources.ApplyResources(this.picSave, "picSave");
            this.picSave.BackgroundImage = global::OnRadio.Properties.Resources.Checked32;
            this.picSave.Name = "picSave";
            this.picSave.TabStop = false;
            this.ToolTip1.SetToolTip(this.picSave, resources.GetString("picSave.ToolTip"));
            this.picSave.Click += new System.EventHandler(this.picSave_Click);
            this.picSave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picSave_MouseDown);
            this.picSave.MouseEnter += new System.EventHandler(this.picSave_MouseEnter);
            this.picSave.MouseLeave += new System.EventHandler(this.picSave_MouseLeave);
            this.picSave.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picSave_MouseUp);
            // 
            // picFdialog
            // 
            resources.ApplyResources(this.picFdialog, "picFdialog");
            this.picFdialog.BackgroundImage = global::OnRadio.Properties.Resources.Picture32;
            this.picFdialog.Name = "picFdialog";
            this.picFdialog.TabStop = false;
            this.ToolTip1.SetToolTip(this.picFdialog, resources.GetString("picFdialog.ToolTip"));
            this.picFdialog.Click += new System.EventHandler(this.picFdialog_Click);
            this.picFdialog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picFdialog_MouseDown);
            this.picFdialog.MouseEnter += new System.EventHandler(this.picFdialog_MouseEnter);
            this.picFdialog.MouseLeave += new System.EventHandler(this.picFdialog_MouseLeave);
            this.picFdialog.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picFdialog_MouseUp);
            // 
            // picDown
            // 
            resources.ApplyResources(this.picDown, "picDown");
            this.picDown.BackgroundImage = global::OnRadio.Properties.Resources.Symbol_Down32;
            this.picDown.Name = "picDown";
            this.picDown.TabStop = false;
            this.ToolTip1.SetToolTip(this.picDown, resources.GetString("picDown.ToolTip"));
            this.picDown.Click += new System.EventHandler(this.picDown_Click);
            this.picDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picDown_MouseDown);
            this.picDown.MouseEnter += new System.EventHandler(this.picDown_MouseEnter);
            this.picDown.MouseLeave += new System.EventHandler(this.picDown_MouseLeave);
            this.picDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picDown_MouseUp);
            // 
            // picUp
            // 
            resources.ApplyResources(this.picUp, "picUp");
            this.picUp.BackgroundImage = global::OnRadio.Properties.Resources.Symbol_Up32;
            this.picUp.Name = "picUp";
            this.picUp.TabStop = false;
            this.ToolTip1.SetToolTip(this.picUp, resources.GetString("picUp.ToolTip"));
            this.picUp.Click += new System.EventHandler(this.picUp_Click);
            this.picUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picUp_MouseDown);
            this.picUp.MouseEnter += new System.EventHandler(this.picUp_MouseEnter);
            this.picUp.MouseLeave += new System.EventHandler(this.picUp_MouseLeave);
            this.picUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picUp_MouseUp);
            // 
            // picDel
            // 
            resources.ApplyResources(this.picDel, "picDel");
            this.picDel.BackgroundImage = global::OnRadio.Properties.Resources.delete_press;
            this.picDel.Name = "picDel";
            this.picDel.TabStop = false;
            this.ToolTip1.SetToolTip(this.picDel, resources.GetString("picDel.ToolTip"));
            this.picDel.Click += new System.EventHandler(this.picDel_Click);
            this.picDel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picDel_MouseDown);
            this.picDel.MouseEnter += new System.EventHandler(this.picDel_MouseEnter);
            this.picDel.MouseLeave += new System.EventHandler(this.picDel_MouseLeave);
            this.picDel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picDel_MouseUp);
            // 
            // picAdd
            // 
            resources.ApplyResources(this.picAdd, "picAdd");
            this.picAdd.BackgroundImage = global::OnRadio.Properties.Resources.add32;
            this.picAdd.Name = "picAdd";
            this.picAdd.TabStop = false;
            this.ToolTip1.SetToolTip(this.picAdd, resources.GetString("picAdd.ToolTip"));
            this.picAdd.Click += new System.EventHandler(this.picAdd_Click);
            this.picAdd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picAdd_MouseDown);
            this.picAdd.MouseEnter += new System.EventHandler(this.picAdd_MouseEnter);
            this.picAdd.MouseLeave += new System.EventHandler(this.picAdd_MouseLeave);
            this.picAdd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picAdd_MouseUp);
            // 
            // AxWindowsMediaPlayer1
            // 
            resources.ApplyResources(this.AxWindowsMediaPlayer1, "AxWindowsMediaPlayer1");
            this.AxWindowsMediaPlayer1.Name = "AxWindowsMediaPlayer1";
            this.AxWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("AxWindowsMediaPlayer1.OcxState")));
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bt_eject);
            this.panel2.Controls.Add(this.bt_eject1);
            this.panel2.Controls.Add(this.bt_pause);
            this.panel2.Controls.Add(this.bt_stop);
            this.panel2.Controls.Add(this.bt_previous);
            this.panel2.Controls.Add(this.bt_next);
            this.panel2.Controls.Add(this.PicBox_logo);
            this.panel2.Controls.Add(this.picBox_collapse_icon);
            this.panel2.Controls.Add(this.bt_play);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.listBox1);
            this.panel3.Controls.Add(this.listBox2);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // bt_favorit
            // 
            resources.ApplyResources(this.bt_favorit, "bt_favorit");
            this.bt_favorit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.bt_favorit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_favorit.Name = "bt_favorit";
            this.bt_favorit.UseVisualStyleBackColor = false;
            this.bt_favorit.Click += new System.EventHandler(this.button9_Click);
            this.bt_favorit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button9_MouseUp);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.KOZ);
            this.panel4.Controls.Add(this.comBox_search);
            this.panel4.Controls.Add(this.picserch);
            this.panel4.Controls.Add(this.bt_favorit);
            this.panel4.Controls.Add(this.bt_re_favorit);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // KOZ
            // 
            resources.ApplyResources(this.KOZ, "KOZ");
            this.KOZ.BackColor = System.Drawing.SystemColors.Window;
            this.KOZ.Name = "KOZ";
            this.KOZ.TabStop = false;
            // 
            // bt_re_favorit
            // 
            this.bt_re_favorit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.bt_re_favorit.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.bt_re_favorit, "bt_re_favorit");
            this.bt_re_favorit.Name = "bt_re_favorit";
            this.bt_re_favorit.UseVisualStyleBackColor = false;
            this.bt_re_favorit.Click += new System.EventHandler(this.button8_Click);
            this.bt_re_favorit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button8_MouseUp);
            // 
            // panel5
            // 
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Controls.Add(this.lab_vsego);
            this.panel5.Controls.Add(this.picPly);
            this.panel5.Controls.Add(this.Combo_lang);
            this.panel5.Controls.Add(this.picExit);
            this.panel5.Controls.Add(this.picSave);
            this.panel5.Controls.Add(this.pictureBox7);
            this.panel5.Controls.Add(this.Localise_set);
            this.panel5.Controls.Add(this.pictureBox6);
            this.panel5.Controls.Add(this.picFdialog);
            this.panel5.Controls.Add(this.picDown);
            this.panel5.Controls.Add(this.picUp);
            this.panel5.Controls.Add(this.picDel);
            this.panel5.Controls.Add(this.picAdd);
            this.panel5.Controls.Add(this.PicBox_logo2);
            this.panel5.Controls.Add(this.lab_name_station);
            this.panel5.Controls.Add(this.lab_URL);
            this.panel5.Controls.Add(this.Tburl);
            this.panel5.Controls.Add(this.TBname);
            this.panel5.Name = "panel5";
            // 
            // lab_vsego
            // 
            resources.ApplyResources(this.lab_vsego, "lab_vsego");
            this.lab_vsego.Name = "lab_vsego";
            // 
            // pictureBox7
            // 
            resources.ApplyResources(this.pictureBox7, "pictureBox7");
            this.pictureBox7.BackgroundImage = global::OnRadio.Properties.Resources.browser;
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            resources.ApplyResources(this.pictureBox6, "pictureBox6");
            this.pictureBox6.BackgroundImage = global::OnRadio.Properties.Resources.nam32;
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.TabStop = false;
            // 
            // lab_name_station
            // 
            resources.ApplyResources(this.lab_name_station, "lab_name_station");
            this.lab_name_station.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lab_name_station.Name = "lab_name_station";
            // 
            // lab_URL
            // 
            resources.ApplyResources(this.lab_URL, "lab_URL");
            this.lab_URL.Name = "lab_URL";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
            // 
            // Radio
            // 
            resources.ApplyResources(this.Radio, "Radio");
            this.Radio.Click += new System.EventHandler(this.Radio_Click);
            // 
            // Timer1
            // 
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Timer2
            // 
            this.Timer2.Interval = 10000;
            this.Timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4
            // 
            this.timer4.Interval = 1200000;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // 
            // timer6
            // 
            this.timer6.Interval = 1000;
            this.timer6.Tick += new System.EventHandler(this.timer6_Tick);
            // 
            // bgnThread
            // 
            this.bgnThread.WorkerSupportsCancellation = true;
            this.bgnThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgnThread_DoWork);
            this.bgnThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgnThread_RunWorkerCompleted);
            // 
            // timer7
            // 
            this.timer7.Interval = 1000;
            this.timer7.Tick += new System.EventHandler(this.timer7_Tick);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.StatusStrip1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.AxWindowsMediaPlayer1);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseLeave += new System.EventHandler(this.Form1_MouseLeave);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Redactor_R)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_equalizer_icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_record_bt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_sound_icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Localise_set)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_logo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picserch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_eject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_eject1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_pause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_stop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_previous)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_next)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_collapse_icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_play)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFdialog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AxWindowsMediaPlayer1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.KOZ)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MB.Controls.ColorSlider colorSlider1;
        private System.Windows.Forms.StatusStrip StatusStrip1;
        private System.Windows.Forms.PictureBox picBox_sound_icon;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label lab_sound_level;
        internal System.Windows.Forms.PictureBox PicBox_logo;
        internal System.Windows.Forms.PictureBox picBox_collapse_icon;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolTip ToolTip1;
        internal System.Windows.Forms.NotifyIcon Radio;
        private AxWMPLib.AxWindowsMediaPlayer AxWindowsMediaPlayer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        
     
        private System.Windows.Forms.Timer Timer2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
  
   
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button bt_favorit;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button bt_re_favorit;
        private System.Windows.Forms.PictureBox bt_next;
        private System.Windows.Forms.PictureBox bt_previous;
        private System.Windows.Forms.PictureBox bt_stop;
        private System.Windows.Forms.PictureBox bt_pause;
        private System.Windows.Forms.PictureBox bt_play;
        private System.Windows.Forms.PictureBox bt_eject1;
        private System.Windows.Forms.PictureBox bt_eject;
        private System.Windows.Forms.PictureBox picBox_equalizer_icon;
        private System.Windows.Forms.Timer t;
        private System.Windows.Forms.PictureBox picBox_record_bt;
       
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Label vrema;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.ComboBox comBox_search;
        private System.Windows.Forms.PictureBox picserch;
        private System.Windows.Forms.PictureBox KOZ;
        private System.Windows.Forms.Button bt_effect1;
        private System.Windows.Forms.Button bt_effect3;
        private System.Windows.Forms.Button bt_effect2;
        private System.Windows.Forms.Button bt_effect4;
        private System.Windows.Forms.PictureBox Localise_set;
        private System.Windows.Forms.ComboBox Combo_lang;
        private System.Windows.Forms.Timer timer5;
        private System.Windows.Forms.Timer timer6;
        private System.Windows.Forms.PictureBox Redactor_R;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lab_name_station;
        private System.Windows.Forms.Label lab_URL;
        private System.Windows.Forms.TextBox Tburl;
        private System.Windows.Forms.TextBox TBname;
        internal System.Windows.Forms.PictureBox PicBox_logo2;
        private System.Windows.Forms.PictureBox picSave;
        private System.Windows.Forms.PictureBox picExit;
        private System.Windows.Forms.PictureBox picAdd;
        private System.Windows.Forms.PictureBox picDel;
        private System.Windows.Forms.PictureBox picDown;
        private System.Windows.Forms.PictureBox picUp;
        private System.Windows.Forms.PictureBox picFdialog;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox picPly;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.Label lab_vsego;
        internal System.ComponentModel.BackgroundWorker bgnThread;
        private System.Windows.Forms.Timer timer7;
    }
}

