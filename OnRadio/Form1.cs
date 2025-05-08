using Microsoft.Win32;
using System;
using System.Data.SQLite;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
using WMPLib;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Globalization;
using Tirage.OnRadio;
using System.Data;
using System.Net;

namespace OnRadio
{
    public partial class Form1 : Form
    {
        Stopwatch sWatch = new Stopwatch();
        private Point m_oStartPos;
        public Point StartPos
        {
            get { return m_oStartPos; }
            set { m_oStartPos = value; }
        }
        public WMPPlayState playState { get; }
        public bool isOnline { get; }
        public int bitRate { get; }
        public object SQL { get; private set; }
        public string ONFavorit = null;
        private SQLiteConnection DB;
        private SQLiteCommand cmd;
     
         // Имя папки с базой
        public string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\OnRadio\\";
        // Папка сохранения записей
        public string pathRec = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\OnRadio\\";
        // Имя базы
        string baseName = "OnRadio.db";       
        string time = null;
        int Lang;
        
        public Form1()
        {
            Program.f1 = this; // теперь f1 будет ссылкой на форму Form1
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
             DoubleBuffered = true;  // включаем двойную буферизацию всей формы
          

            Create_db();//Создаем пустую базу данных 

            {            
                // запретить переход в спящий режим
                PowerManagement.xbs_system_functions.PreventSystemFromSleeping();
                try
                {     // Если в настройках есть язык, выбираем его в списке.
                    if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
                    {
                        Combo_lang.SelectedIndex = int.Parse(Properties.Settings.Default.Language);
                    }
                   
                   

                }
                 catch { }
                
                
                // загружаем список станций с базы в листбокс
                Update_list();
                Update_list2();
            }
            {                             
                try
                {    //чтение ключа из файла настроек
                    Set.Name_Stan = Properties.Settings.Default.Radiostantsiya;
                    colorSlider1.Value = Properties.Settings.Default.Volume;
                    Height = Properties.Settings.Default.Size_Height;
                    panel5.Height = Properties.Settings.Default.Size_Height_p5;
                    m_oStartPos.X = Properties.Settings.Default.StartPos_X;
                    m_oStartPos.Y = Properties.Settings.Default.StartPos_Y;
                    Location = new Point(m_oStartPos.X, m_oStartPos.Y);
                }
                catch
                {  // если записи в файле настроек нет
                    listBox1.SelectedIndex = 0; // первая в списке станция
                    colorSlider1.Value = 10; // громкость
                    Height = 505;         // размер формы
                    panel5.Height = 0;   // размер редактора
                    StartPosition = FormStartPosition.CenterScreen; // старт по центру
                }
                // при первом запуске запишем в реестр дату первой установки
                RegistryKey LokalMachineKey = Registry.LocalMachine;
                if (Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Classes\SOFTWARE\VL\InstalOnRadio", "InstallDate", null) == null)
                {  // проверяем первую дату установки в реестре, если нет то записуем
                    RegistryKey installdate = LokalMachineKey.CreateSubKey("SOFTWARE\\Classes\\SOFTWARE\\VL\\InstalOnRadio", RegistryKeyPermissionCheck.ReadWriteSubTree);
                    DateTime thisDay = DateTime.Today;
                    installdate.SetValue("InstallDate", thisDay.ToString("d"));
                }
                {
                    // читаем версию приложения 
                    var myVersion = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
                    Set.vers = "v." + myVersion.ToString();

                    if (listBox1.Visible == true)
                    {
                        bt_favorit.Text = MyStrings.button9text;//Все радиостанции (кнопка)
                        bt_re_favorit.Text = MyStrings.button8text2;//Удалить из избранного (кнопка)
                        bt_favorit.Image = global::OnRadio.Properties.Resources.favorites3; // Загрузить изображение (на кнопке)c области ресурсов
                    }
                    else
                    {
                        bt_re_favorit.Text = MyStrings.button8text;//Добавить в избранное (кнопка)
                        bt_favorit.Text = MyStrings.button9text2;//В избранное (кнопка)
                        bt_favorit.Image = null;               // без изображения(на кнопке)
                    }
                }

                if (Height < 400)
                {
                    bt_eject1.Visible = true;   // Меняем кнопки местами (кнопка развернуть)
                    bt_eject.Visible = false;   //Меняем кнопки местами    (кнопка свернуть)

                }

                {
                    // получить строку для поиска последней проиграной станции
                    string searchString = Set.Name_Stan;
                    if (!((searchString ?? "") == (string.Empty ?? "")))
                    {
                        int i = listBox1.FindString(searchString);
                        if (!(i == -1))
                        {
                            listBox1.SelectedIndex = i;

                        }
                    }
                }
            }
            {
                // очистить файл настроек  
                Properties.Settings.Default.Size_Height = 505;
                Properties.Settings.Default.Size_Height_p5 = 0;
                Properties.Settings.Default.StartPos_X = 798;
                Properties.Settings.Default.StartPos_Y = 200;
                Properties.Settings.Default.Save();


            }
            picBox_equalizer_icon.Visible = false;  //анимация эквалайзера GIF
            picBox_record_bt.Image = Properties.Resources.rec_bt1;  //кнопка запись
            picBox_record_bt.SizeMode = PictureBoxSizeMode.StretchImage;
            picBox_record_bt.Visible = false;

            this.AxWindowsMediaPlayer1.settings.volume = colorSlider1.Value;  //громкость в плеере как на слайдере
            this.lab_sound_level.Text = Math.Round(colorSlider1.Value / (double)(colorSlider1.Maximum - colorSlider1.Minimum) * 100 + 0, 1) + "%"; // процент громкости в лабеле
            Timer1.Interval = 6000;
            Timer1.Start();
            Timer2.Start();
            timer3.Start();

            listBox1.Focus(); //активируем список станций                         
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {   // закрытие формы
            if (Set.Lrec == "R")
            {
                e.Cancel = true;
                using (new CenterWinDialog(this))
                {     // сообщение остановить запись?
                    DialogResult dialogResult = MessageBox.Show(MyStrings.Stop_recording +   //остановить запись?"
                       "\n" + TBname.Text, MyStrings.Stop_recording,                        //Радіо станція: 
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        picBox_record_bt.Image = Properties.Resources.rec_bt1;
                        picBox_record_bt.SizeMode = PictureBoxSizeMode.StretchImage;
                        Set.Lrec = null;
                        bgnThread.CancelAsync();
                        timer7.Stop();
                        sWatch.Stop();
                        listBox1.Enabled = true;
                        listBox2.Enabled = true;
                        bt_re_favorit.Enabled = true;
                        bt_favorit.Enabled = true;
                        picserch.Enabled = true;
                        bt_pause.Enabled = true;
                        bt_previous.Enabled = true;
                        bt_next.Enabled = true;
                        Redactor_R.Visible = true;
                        e.Cancel = false;
                    }
                    else if (dialogResult == DialogResult.No)
                    {  // ничего не делаем
                        return;
                    }
                }
            }
            // разрешить переход в спящий режим
            PowerManagement.xbs_system_functions.restoreSystemSleepState();
            // Сохраняем выбранный язык.(фаил настроек)
            Properties.Settings.Default.Language = Combo_lang.SelectedIndex.ToString();
            // установим размер формы по умолчанию
            Size = new Size(302, 505);
            panel5.Size = new Size(286, 0);
            Properties.Settings.Default.Radiostantsiya = Set.Name_Stan;
            Properties.Settings.Default.Volume = colorSlider1.Value;
            
            if (WindowState == FormWindowState.Minimized)
            {   // если форма свернута , затираем в файл настроек размеры              
                Properties.Settings.Default.Size_Height = 0;
                Properties.Settings.Default.Size_Height_p5 = 0;
                Properties.Settings.Default.StartPos_X = 0;
                Properties.Settings.Default.StartPos_Y = 0;
            }
            else
            {   // если не свернута , запишем текущие размеры и положение формы              
                Properties.Settings.Default.Size_Height = Height;
                Properties.Settings.Default.Size_Height_p5 = panel5.Height;
                Properties.Settings.Default.StartPos_X = Location.X;
                Properties.Settings.Default.StartPos_Y = Location.Y;
            }
            Properties.Settings.Default.Save();
            AxWindowsMediaPlayer1.Dispose(); // выгружаем преер (WMP)
            Timer1.Stop();

            //удалить временные файлы с кеша системы после 60 включений радио
        
            if (Properties.Settings.Default.runCount > 60)
            {
                ClearCache();
                Properties.Settings.Default.runCount = 0;
            }
            Properties.Settings.Default.runCount += 1;
            Properties.Settings.Default.Save();
        }
        private void Update_list()
        {
            try
            {
                // обновить список listBox1
                // добавить контекстное меню сортировки listBox1
               
                contextMenuStrip1.Items.Clear();
                ToolStripMenuItem sortMenuItem = new ToolStripMenuItem(MyStrings.sortMenuOn); //"Сортировать"
                ToolStripMenuItem clearMenuItem = new ToolStripMenuItem(MyStrings.sortMenuOff);  //"Не сортировать"         
                contextMenuStrip1.Items.AddRange(new[] { sortMenuItem, clearMenuItem });
                listBox1.ContextMenuStrip = contextMenuStrip1;

                if (!String.IsNullOrEmpty(Properties.Settings.Default.Sortings))
                {
                    if (Properties.Settings.Default.Sortings == "true")
                    {
                        sortMenuItem.Checked = true;
                        clearMenuItem.Checked = false;
                        listBox1.Sorted = true;
                    }
                    else
                    {
                        sortMenuItem.Checked = false;
                        clearMenuItem.Checked = true;
                        listBox1.Sorted = false;
                    }
                }
                sortMenuItem.Click += sortMenuItem_Click;
                clearMenuItem.Click += clearMenuItem_Click;
                listBox1.Items.Clear();
               // comBox_search.DataSource = null;

                 SQLiteConnection connection = new SQLiteConnection();
                connection.ConnectionString = ("Data Source=" + path + baseName);
                SQLiteCommand command = new SQLiteCommand("select * from ONplaylist", connection);  // Имя таблицы
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                listBox1.Items.Add((string)reader["STANSIA"]);         //СтолбецТаблицы
                connection.Close();

                comBox_search.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comBox_search.AutoCompleteSource = AutoCompleteSource.ListItems;
                comBox_search.DataSource = listBox1.Items;
                comBox_search.Text = "";

       

                // получить строку для поиска последней проиграной станции
                string searchString1 = Set.Name_Stan;
                if (!((searchString1 ?? "") == (string.Empty ?? "")))
                {
                    int i = listBox1.FindString(searchString1);
                    if (!(i == -1))
                    {
                        listBox1.SelectedIndex = i;
                    }
                }
              

               lab_vsego.Text = MyStrings.count_str + listBox1.Items.Count.ToString();

            }
            catch { }

        }
        private void Update_list2()
        {
            try
            {
                // обновить список listBox2
                // добавить контекстное меню сортировки listBox2
                contextMenuStrip2.Items.Clear();
                ToolStripMenuItem sortMenuItem2 = new ToolStripMenuItem(MyStrings.sortMenuOn); //"Сортировать"
                ToolStripMenuItem clearMenuItem2 = new ToolStripMenuItem(MyStrings.sortMenuOff);  //"Не сортировать"         
                contextMenuStrip2.Items.AddRange(new[] { sortMenuItem2, clearMenuItem2 });
                listBox2.ContextMenuStrip = contextMenuStrip2;
                if (!String.IsNullOrEmpty(Properties.Settings.Default.Sortings2))
                {
                    if (Properties.Settings.Default.Sortings2 == "true")
                    {
                        sortMenuItem2.Checked = true;
                        clearMenuItem2.Checked = false;
                        listBox2.Sorted = true;
                    }
                    else
                    {
                        sortMenuItem2.Checked = false;
                        clearMenuItem2.Checked = true;
                        listBox2.Sorted = false;
                    }
                }
                sortMenuItem2.Click += sortMenuItem2_Click;
                clearMenuItem2.Click += clearMenuItem2_Click;
                listBox2.Items.Clear();

                SQLiteConnection connection = new SQLiteConnection();
                connection.ConnectionString = "Data Source = " + path + baseName;
                SQLiteCommand command = new SQLiteCommand("select * from ONFavorit", connection);  // Имя таблицы
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    listBox2.Items.Add((string)reader["STANSIA"]);         //СтолбецТаблицы
                connection.Close();
                // получить строку для поиска последней проиграной станции
                string searchString2 = Set.Name_Stan;
                if (!((searchString2 ?? "") == (string.Empty ?? "")))
                {
                    int i = listBox2.FindString(searchString2);
                    if (!(i == -1))
                    {
                        listBox2.SelectedIndex = i;
                    }
                }
            }
            catch { }
        }
        private void clearMenuItem_Click(object sender, EventArgs e)
        {
            // отменить сортировку
            listBox1.Sorted = false;          
            Properties.Settings.Default.Sortings = "false";
            Properties.Settings.Default.Save();
            Update_list();
           // Update_list2();
        }
        private void sortMenuItem_Click(object sender, EventArgs e)
        {
            // отсортировать список по алфовиту
            listBox1.Sorted = true;
            Properties.Settings.Default.Sortings = "true";
            Properties.Settings.Default.Save();
            Update_list();
          //  Update_list2();
        }
        private void clearMenuItem2_Click(object sender, EventArgs e)
        {
            // отменить сортировку
            listBox2.Sorted = false;
            Properties.Settings.Default.Sortings2 = "false";
            Properties.Settings.Default.Save();
           // Update_list();
            Update_list2();
        }
        private void sortMenuItem2_Click(object sender, EventArgs e)
        {
            // отсортировать список по алфовиту
            listBox2.Sorted = true;
            Properties.Settings.Default.Sortings2 = "true";
            Properties.Settings.Default.Save();
          //  Update_list();
            Update_list2();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {  // при изменении размера формы
            comBox_search.Text = "";
            comBox_search.Visible = false;
            KOZ.Visible = false;
            if (WindowState == FormWindowState.Normal)
            {
                Radio.Visible = false;
                ShowInTaskbar = true;
                timer4.Stop();
            }

            if (Height == 430)
            {
                Enabled = true;
                Redactor_R.Visible = false;
            }
        }
        public void ClearCache()
        {    //удалить временные файлы, кеш системы
            try
            {
                //Temporary Internet Files Временные интернет файлы
                Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 8"); //Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 8");
            }  
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private async void Create_db()
        {
            if (!Directory.Exists(path))
            {  // проверяем есть ли папка для базы,если нет создаем
                Directory.CreateDirectory(path);
            }         
             if (!File.Exists(path + baseName)) // проверяем ,если нет базы данных, то...
              {
                
                // создаем пустую базу данных
                SQLiteConnection.CreateFile(path + baseName);
                System.Threading.Thread.Sleep(500);
                using (var conn = new SQLiteConnection("Data Source=" + path + baseName))
                {
                    await conn.OpenAsync();
                    using (var cmd = conn.CreateCommand())
                    {                         // создаем таблицу 1
                        cmd.CommandText = @"create table if not exists [ONplaylist](                     

                        STANSIA  VARCHAR (25)  UNIQUE
                                               NOT NULL,
                        URL_ST   VARCHAR (128) NOT NULL,
                        IMAGE_ST               BLOB
                         );";
                        cmd.ExecuteNonQuery();
                    }
                    using (var cmd = conn.CreateCommand())
                    {                         // создаем таблицу 2
                        cmd.CommandText = @"create table if not exists [ONFavorit](                     

                          STANSIA VARCHAR (25) NOT NULL
                                               UNIQUE
                         );";
                        cmd.ExecuteNonQuery();

                        conn.Close();
                        
                        using (new CenterWinDialog(this))
                            MessageBox.Show(MyStrings.CreateBD); //База данных создана
                    }
                }
            }
        }
       
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.SelectedItem = comBox_search.SelectedItem;
            // если видимый избранный список,переключаем на весь список
            bt_favorit.Text = MyStrings.button9text2;//В избранное
            bt_favorit.TextAlign = ContentAlignment.MiddleCenter;
            bt_re_favorit.Text = MyStrings.button8text;//Добавить в избранное
            bt_favorit.Image = null;
            listBox1.Visible = true;
            listBox2.Visible = false;
            comBox_search.Text = "";
            comBox_search.Visible = false;
        }
        private void comboBox1_Leave(object sender, EventArgs e)
        {
            comBox_search.Text = "";
            comBox_search.Visible = false;
            KOZ.Visible = false;
        }
        private void comboBox1_Click(object sender, EventArgs e)
        {
            comBox_search.Text = "";
            comBox_search.Visible = false;
            KOZ.Visible = false;
        }
   
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            // новое слово с большой буквы
            if (((ComboBox)sender).Text.Length == 1)
                ((ComboBox)sender).Text = ((ComboBox)sender).Text.ToUpper();
            ((ComboBox)sender).Select(((ComboBox)sender).Text.Length, 0);
        }
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            if (Set.quiet == "on")
            {
                ToolTip1.SetToolTip(picBox_sound_icon, MyStrings.pictureOn);
            }
            else
            {
                ToolTip1.SetToolTip(picBox_sound_icon, MyStrings.pictureOff);
            }
        }
       
        private void Localise_set_MouseDown(object sender, MouseEventArgs e)
        {     // при наведении мышей  кнопки язык
            Set.Volium_l = lab_sound_level.Text;
            Combo_lang.Visible = true;
            try
            {     // запись настроек языка
            
            }
            catch { }
            Localise_set.Size = new Size(32, 32);
            Localise_set.Location = new Point(201, 212);
        }
        private void Combo_lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Combo_lang.SelectedIndexChanged -= new System.EventHandler(this.Combo_lang_SelectedIndexChanged);
                CultureInfo cultureInfo = new CultureInfo("ru-ru");
                if (Combo_lang.SelectedIndex == 1) cultureInfo = new CultureInfo("en-US");
                if (Combo_lang.SelectedIndex == 2) cultureInfo = new CultureInfo("uk-UA");
                ChangeLanguage.Instance.localizeForm(this, cultureInfo);

                this.Combo_lang.SelectedIndexChanged += new System.EventHandler(this.Combo_lang_SelectedIndexChanged);
                //  return;
                Combo_lang.SelectedIndex = int.Parse(Properties.Settings.Default.Language);
                if (Combo_lang.SelectedIndex == 0)
                {
                    Localise_set.BackgroundImage = Properties.Resources.ru__2_;
                }
                if (Combo_lang.SelectedIndex == 1)
                {
                    Localise_set.BackgroundImage = Properties.Resources.gb;
                }
                if (Combo_lang.SelectedIndex == 2)
                {
                    Localise_set.BackgroundImage = Properties.Resources.ua;
                }
                lab_sound_level.Text = Set.Volium_l.ToString();
            }
            catch { }
            Update_list();
            Update_list2();
        }
        private async void Combo_lang_SelectionChangeCommitted(object sender, EventArgs e)
        { // после того как выбран язык в списке
            try
            {                
                if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
                {
                    Lang = int.Parse(Properties.Settings.Default.Language);
                }
                if (Lang != Combo_lang.SelectedIndex)
                {
                    Properties.Settings.Default.Language = Combo_lang.SelectedIndex.ToString();
               
                    Properties.Settings.Default.Save();                  
                }         
                await Task.Delay(1500);
                Combo_lang.Visible = false;            
            }
            catch { }
        }
   
        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Space) return;
            //пауза
            // воспроизведение
            {
                if (AxWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying) // если музыка играет
                {
                    AxWindowsMediaPlayer1.Ctlcontrols.pause();
                    Timer1.Stop();
                    Text = "OnRadio " + Set.vers;
                    bt_play.Visible = true;
                    bt_pause.Visible = false;
                    picBox_equalizer_icon.Visible = false;
                    toolStripStatusLabel2.Text = MyStrings.toolStripStatusLabeltext2; //Приостановлено
                    toolStripStatusLabel2.ForeColor = Color.White;
                    toolStripStatusLabel2.Font = new Font("Cambria", 12.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                }
                else
               if (AxWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused) // если на паузе
                {
                    AxWindowsMediaPlayer1.Ctlcontrols.play();
                    bt_play.Visible = false;
                    bt_pause.Visible = true;
                    picBox_equalizer_icon.Visible = true;
                    toolStripStatusLabel2.Text = MyStrings.toolStripStatusLabeltext; //Воспроизведение
                    toolStripStatusLabel2.ForeColor = Color.LightGreen;
                }
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {   // при выборе сроки в лист боксе ,все станции
            comBox_search.Text = ""; // очистим строку поиска и скроим
            comBox_search.Visible = false;
            try
            {
                Set.Name_St = listBox1.SelectedItem.ToString();
            }
            catch { }
            toolStripStatusLabel1.Text = ""; // очистим нижнюю строку стауса
            toolStripStatusLabel2.Text = MyStrings.toolStripStatusLabeltext; //Воспроизведение в строке статус
            toolStripStatusLabel2.ForeColor = Color.LightGreen;
            toolStripStatusLabel2.Font = new Font("Cambria", 12.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            Timer1.Interval = 6000;
            Timer1.Start();
            // меняем кнопку плей на паузу
            bt_pause.Visible = true;
            bt_play.Visible = false;
            // если выбраная станция играет ,ничего не делаем
            if (Set.Name_Stan == listBox1.SelectedItem.ToString() && AxWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                return;
            }
            else
                // если не играет
                try
                {
                    Set.Name_Stan = listBox1.SelectedItem.ToString();
                    TBname.Text = listBox1.SelectedItem.ToString();
                    // Считываем URL радио станции с базы данных.
                    DB = new SQLiteConnection("Data Source=" + path + baseName);
                    DB.Open();
                    SQLiteCommand CMD = DB.CreateCommand();
                    CMD.CommandText = "select * from ONplaylist where STANSIA like '%' || @STAN || '%'  ";
                    CMD.Parameters.Add("@STAN", System.Data.DbType.String).Value = listBox1.SelectedItem;
                    SQLiteDataReader SQL = CMD.ExecuteReader();
                    if (SQL.HasRows)
                    {
                        while (SQL.Read())
                        {
                            Set.urladdress = SQL.GetString(SQL.GetOrdinal("URL_ST"));
                            Tburl.Text = Set.urladdress;
                            // Запускаем радио поток
                            AxWindowsMediaPlayer1.URL = Set.urladdress;
                            // Загружаем логотип радио станции с базы данных
                            if (!SQL.IsDBNull(0))
                            {
                                byte[] im = (byte[])SQL["IMAGE_ST"];
                                MemoryStream ms = new MemoryStream(im);
                                PicBox_logo.Image = Image.FromStream(ms);
                               PicBox_logo.SizeMode = PictureBoxSizeMode.StretchImage;
                                PicBox_logo2.Image = Image.FromStream(ms);
                                PicBox_logo2.SizeMode = PictureBoxSizeMode.StretchImage;
                            }
                            else
                            {
                                PicBox_logo.Image = null;
                                PicBox_logo2.Image = null;
                            }
                        }
                        DB.Close();
                    }
                    return;
                }
                catch
                { }
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            bt_pause.Visible = true;
            bt_play.Visible = false;
            comBox_search.Text = "";
            comBox_search.Visible = false;
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel2.Text = MyStrings.toolStripStatusLabeltext; //Воспроизведение
            toolStripStatusLabel2.ForeColor = Color.LightGreen;
            toolStripStatusLabel2.Font = new Font("Cambria", 12.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            Timer1.Interval = 6000;
            Timer1.Start();
            try
            {
                if (Set.Name_Stan == listBox2.SelectedItem.ToString() && AxWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
                {
                }
                else
                    try
                    {
                        Set.Name_Stan = listBox2.SelectedItem.ToString();
                        // Считываем URL радио станции с базы данных.
                        DB = new SQLiteConnection("Data Source=" + path + baseName);
                        DB.Open();
                        SQLiteCommand CMD = DB.CreateCommand();
                        CMD.CommandText = "select * from ONplaylist where STANSIA like '%' || @STAN || '%'  ";
                        CMD.Parameters.Add("@STAN", System.Data.DbType.String).Value = listBox2.SelectedItem;
                        SQLiteDataReader SQL = CMD.ExecuteReader();
                        if (SQL.HasRows)
                        {
                            while (SQL.Read())
                            {
                                Set.urladdress = SQL.GetString(SQL.GetOrdinal("URL_ST"));    
                                AxWindowsMediaPlayer1.URL = Set.urladdress;
                                // Загружаем логотип радио станции с базы данных
                                if (!SQL.IsDBNull(0))
                                {
                                    byte[] im = (byte[])SQL["IMAGE_ST"];
                                    MemoryStream ms = new MemoryStream(im);
                                    PicBox_logo.Image = Image.FromStream(ms);
                                    PicBox_logo.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                                else
                                {
                                    PicBox_logo.Image = null;
                                }
                            }
                            DB.Close();
                        }
                        return;
                    }
                    catch
                    { }
            }
            catch
            {
            }
        }
        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Space) return;
            //пауза
            // воспроизведение
            {
                if (AxWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying) // если музыка играет
                {
                    AxWindowsMediaPlayer1.Ctlcontrols.pause();
                    Timer1.Stop();
                    Text = "OnRadio " + Set.vers;
                    bt_play.Visible = true;
                    bt_pause.Visible = false;
                    picBox_equalizer_icon.Visible = false;
                    toolStripStatusLabel2.Text = MyStrings.toolStripStatusLabeltext2; //Приостановлено
                    toolStripStatusLabel2.ForeColor = Color.White;
                    toolStripStatusLabel2.Font = new Font("Cambria", 12.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                }
                else
               if (AxWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused) // если на паузе
                {
                    AxWindowsMediaPlayer1.Ctlcontrols.play();
                    bt_play.Visible = false;
                    bt_pause.Visible = true;
                    picBox_equalizer_icon.Visible = true;
                    toolStripStatusLabel2.Text = MyStrings.toolStripStatusLabeltext; //Воспроизведение;
                    toolStripStatusLabel2.ForeColor = Color.LightGreen;
                }
            }
        }
       
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //кнопка свернуть
            WindowState = FormWindowState.Minimized; // сворачиваем форму в трей
            ShowInTaskbar = false; // скрываем ярлык на панели
            Radio.Visible = true; // задаем иконку всплывающей подсказки
            Radio.BalloonTipIcon = ToolTipIcon.Info;    // задаем текст подсказки
            Radio.BalloonTipText = MyStrings.RadioBTip;//Я здесь, если ты передумаешь"; // устанавливаем зголовк
            Radio.BalloonTipTitle = "OnRadio";  // устанавливаем зголовок
            Radio.ShowBalloonTip(6); // отображаем подсказку 6 секунд
            timer4.Start();
        }
        private void PictureBox8_Click(object sender, EventArgs e)
        {      //о программе           
            Form AboutBox1 = new AboutBox1();
            AboutBox1.ShowDialog();
        }
        private void bt_play_Click(object sender, EventArgs e)
        {     //кнопка воспроизведение
            if (AxWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying) // если музыка играет ничего не делаем
            {
            }
            else
           if (AxWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused) // если на паузе запускаем
            {
                AxWindowsMediaPlayer1.Ctlcontrols.play();
            }
            else
            {
                // Запускаем радио поток
                AxWindowsMediaPlayer1.URL = Set.urladdress;
                AxWindowsMediaPlayer1.Ctlcontrols.play();
            }
            toolStripStatusLabel2.Text = MyStrings.toolStripStatusLabeltext; //Воспроизведение в строке статус
            toolStripStatusLabel2.ForeColor = Color.LightGreen;
            bt_pause.Visible = true;
            bt_play.Visible = false;
            Timer1.Interval = 6000;
            Timer1.Start();
        }
        private void bt_pause_Click(object sender, EventArgs e)
        {  //кнопка пауза
            AxWindowsMediaPlayer1.Ctlcontrols.pause();
            Timer1.Stop();
            Text = "OnRadio " + Set.vers;
            bt_play.Visible = true;
            bt_pause.Visible = false;
            picBox_equalizer_icon.Visible = false;
            toolStripStatusLabel2.Text = MyStrings.toolStripStatusLabeltext2; //Приостановлено в строке статус
            toolStripStatusLabel2.ForeColor = Color.White;
            toolStripStatusLabel2.Font = new Font("Cambria", 12.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
        }
        private void bt_previous_Click(object sender, EventArgs e)
        {
            //кнопка Предыдущая радио станция
            if (listBox1.Visible == true)
            {
                if (listBox1.SelectedIndex > 0)
                {
                    listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
                }
            }
            else
            if (listBox2.SelectedIndex > 0)
            {
                listBox2.SelectedIndex = listBox2.SelectedIndex - 1;
            }
        }
        private void bt_stop_Click(object sender, EventArgs e)
        {  //кнопка стоп
            if (Set.Lrec == "R")
            {
                //остановить запись
                using (new CenterWinDialog(this))
                {     // остановить запись?
                    DialogResult dialogResult = MessageBox.Show(MyStrings.Stop_recording +   //остановить запись?"
                       "\n" + TBname.Text, MyStrings.Stop_recording,                        //Радіо станція: 
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        picBox_record_bt.Image = Properties.Resources.rec_bt1;
                        picBox_record_bt.SizeMode = PictureBoxSizeMode.StretchImage;
                        Set.Lrec = null;
                        bgnThread.CancelAsync();
                        timer7.Stop();

                        sWatch.Stop();
                        Set.TimeRecStop = DateTime.Now.ToShortTimeString();                       
                        listBox1.Enabled = true;
                        listBox2.Enabled = true;
                        bt_re_favorit.Enabled = true;
                        bt_favorit.Enabled = true;
                        picserch.Enabled = true;
                        bt_pause.Enabled = true;
                        bt_previous.Enabled = true;
                        bt_next.Enabled = true;
                        Redactor_R.Visible = true;


                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        // ничего не делаем
                        return;
                    }
                }
            }
                if (listBox1.Visible == true)
            {
                listBox1.SelectedIndex = 0;
            }
            else
            {
                listBox2.SelectedIndex = 0;
            }
            AxWindowsMediaPlayer1.Ctlcontrols.stop();
            Timer1.Stop();
            toolStripStatusLabel2.Text = "";
            Text = "OnRadio " + Set.vers;
            bt_play.Visible = true;
            bt_pause.Visible = false;
            picBox_equalizer_icon.Visible = false;
            picBox_record_bt.Visible = false;
        }
        private void bt_next_Click(object sender, EventArgs e)
        {  //кнопка Следующая радио станция
            try
            {
                if (listBox1.Visible == true)
                {
                    listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
                }
                else
                {
                    listBox2.SelectedIndex = listBox2.SelectedIndex + 1;
                }
            }
            catch
            { }
        }
        private void bt_eject_Click(object sender, EventArgs e)
        {
            // кнопка свернуть
            try
            {
                // Свернуть
                if (Set.OpenRedakt == "Open")
                {
                    bt_eject.Enabled = false;
                    return;
                }

                    if (Height != 590) // размер с редактором
                {
                    Enabled = false;
                    if (Height != 505)  // размер по умолчанию
                    {
                        if (Height == 430) // средне Свернутая  длина окна формы
                        {
                            t = new Timer(components);
                            t.Start();
                            {
                                t.Interval = 14;
                                t.Tick += delegate
                                {
                                    if (Height > 190)

                                        Height -= 5;
                                    else
                                        t.Stop();

                                    if (Height == 190)
                                    {
                                        bt_eject1.Visible = true;   // Меняем кнопки местами
                                        bt_eject.Visible = false; //Меняем кнопки местами
                                        Enabled = true;
                                    }
                                };
                            }

                        }

                        else
                        {
                            Height = 190;  // Свернутая  длина окна формы
                            bt_eject1.Visible = true;   // Меняем кнопки местами
                            bt_eject.Visible = false; //Меняем кнопки местами
                            Enabled = true;
                        }
                    }
                    else
                    {
                        t = new Timer(components);
                        t.Start();
                        {
                            t.Interval = 14;
                            t.Tick += delegate
                            {
                                if (Height > 430)
                                    Height -= 5;
                                else
                                    t.Stop();
                            };
                        }
                    }
                }
            }
            catch
            {
                Height = 505;
                bt_eject.Visible = true;  //Меняем кнопки местами
                bt_eject1.Visible = false; //Меняем кнопки местами
            }
        }
        private void bt_eject1_Click(object sender, EventArgs e)
        {
            try
            {
                //  кнопка Развернуть
                Enabled = false;
                if (Height != 190) // минимальная длина формы
                {
                  //  Height = 505; //Полная длина окна формы
                    bt_eject.Visible = true;  //Меняем кнопки местами
                    bt_eject1.Visible = false; //Меняем кнопки местами
                    Enabled = true;

                }
                else
                {
                    t = new Timer(components);
                    t.Start();
                    {
                        t.Interval = 14;
                        t.Tick += delegate
                        {
                            if (Height < 505)
                                Height += 5;
                            else
                                t.Stop();
                            if (Height == 500)
                            {
                                bt_eject.Visible = true;  //Меняем кнопки местами
                                bt_eject1.Visible = false; //Меняем кнопки местами
                                Enabled = true;            // запретить любые действия
                                Redactor_R.Visible = true; // Отключить кнопку  редактор
                            }
                        };
                    }
                }
            }
            catch
            {
                Height = 505; //Полная длина окна формы
                bt_eject.Visible = true;  //Меняем кнопки местами
                bt_eject1.Visible = false; //Меняем кнопки местами
                Enabled = true;
            }
        }
        private void picserch_Click(object sender, EventArgs e)
        {
            //кнопка  поиск станции
            comBox_search.Visible = true;
            KOZ.Visible = true;
            comBox_search.Focus();
        }
        private void button8_Click(object sender, EventArgs e)
        { // кнопка добавить или удалить из избранного
            try
            {
                // кнопка добавить или удалить избранное
                if (listBox2.Visible == true)
                {
                    // удалить из избранного
                    var delrlm = listBox2.SelectedItem;

                    using (new CenterWinDialog(this))


                        if (MessageBox.Show(MyStrings.button8Click + delrlm.ToString() + "?", MyStrings.button8Click2, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            listBox2.Items.Remove(delrlm);
                            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=" + path + baseName))
                            {
                                Connect.Open();
                                string commandText = "DELETE FROM [ONFavorit] WHERE [STANSIA] = @STANSIA LIMIT 1"; // LIMIT в SQLite аналог TOP в MS SQL
                                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                                Command.Parameters.AddWithValue("@STANSIA", delrlm);
                                Command.ExecuteNonQuery();
                                using (new CenterWinDialog(this))
                                    MessageBox.Show(MyStrings.button8Click3 + Set.Name_Stan + "\n" + MyStrings.button8Click4, MyStrings.button8Click2, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                listBox2.Visible = false;
                                listBox1.Visible = true;
                                listBox2.Items.Clear();
                                SQLiteCommand command2 = new SQLiteCommand("select * from ONFavorit", Connect);  // Имя таблицы
                                DbDataReader reader2 = command2.ExecuteReader();
                                while (reader2.Read())
                                    listBox2.Items.Add((string)reader2["STANSIA"]);         //СтолбецТаблицы
                                Connect.Close();
                                // получить строку для поиска последней проиграной станции
                                string searchString = Set.Name_Stan;
                                if (!((searchString ?? "") == (string.Empty ?? "")))
                                {
                                    int i = listBox1.FindString(searchString);
                                    if (!(i == -1))
                                    {
                                        listBox1.SelectedIndex = i;
                                    }


                                    bt_re_favorit.Text = MyStrings.button8text;//Добавить в избранное
                                    bt_favorit.Text = MyStrings.button9text2;//В избранное
                                    bt_favorit.TextAlign = ContentAlignment.MiddleCenter;
                                    bt_favorit.Image = null;
                                }
                            }
                        }
                }
                else
                {
                    // добавить в избранное
                    // получить строку для поиска последней проиграной станции
                    string searchString = Set.Name_Stan;
                    if (!((searchString ?? "") == (string.Empty ?? "")))
                    {
                        int i = listBox2.FindString(searchString);
                        if (!(i == -1))
                        {
                            listBox2.Visible = true;
                            listBox1.Visible = false;
                            // если уже есть , выбераем в списке избранных
                            listBox2.SelectedIndex = i;
                            bt_favorit.Text = MyStrings.button9text; //Все радиостанции
                            bt_favorit.TextAlign = ContentAlignment.MiddleCenter;
                            bt_re_favorit.Text = MyStrings.button8text2;//Удалить из избранного
                            bt_favorit.Image = Properties.Resources.favorites3;
                        }
                        else
                        {
                            var addrlm = listBox1.SelectedItem;
                            using (new CenterWinDialog(this))
                                if (MessageBox.Show(MyStrings.button8Click5 + addrlm.ToString() + MyStrings.button8Click6, MyStrings.button8Click2, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    listBox2.Items.Add(addrlm);
                                    using (SQLiteConnection Connect = new SQLiteConnection("Data Source=" + path + baseName))
                                    {
                                        Connect.Open();
                                        string commandText = "INSERT INTO ONFavorit ('STANSIA') values ('" +
                                               addrlm + "')";
                                        SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                                        Command.Parameters.AddWithValue("@STANSIA", addrlm);
                                        Command.ExecuteNonQuery();
                                        using (new CenterWinDialog(this))
                                            MessageBox.Show(MyStrings.button8Click3 + Set.Name_Stan + "\n" + MyStrings.button8Click7, MyStrings.button8Click2, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        bt_favorit.Text = MyStrings.button9text;//Все радиостанции
                                        bt_favorit.TextAlign = ContentAlignment.MiddleCenter;
                                        bt_re_favorit.Text = MyStrings.button8text2;//Удалить из избранного
                                        bt_favorit.Image = Properties.Resources.favorites3;
                                        listBox2.Visible = true;
                                        listBox1.Visible = false;
                                        listBox2.Items.Clear();
                                        SQLiteCommand command2 = new SQLiteCommand("select * from ONFavorit", Connect);  // Имя таблицы                                                                                                                     // connection2.Open();
                                        DbDataReader reader2 = command2.ExecuteReader();
                                        while (reader2.Read())
                                            listBox2.Items.Add((string)reader2["STANSIA"]);         //СтолбецТаблицы
                                        Connect.Close();
                                    }
                                }
                        }
                    }
                    if (!((searchString ?? "") == (string.Empty ?? "")))
                    {
                        int i = listBox2.FindString(searchString);
                        if (!(i == -1))
                        {
                            listBox2.SelectedIndex = i;
                            listBox2.Visible = true;
                            listBox1.Visible = false;
                        }
                    }
                }
            }
            catch
            { }
        }
        private void button8_MouseUp(object sender, MouseEventArgs e)
        {
            AxWindowsMediaPlayer1.Focus();
        }
        private void button9_Click(object sender, EventArgs e)
        {  // кнопка все или избранные станции
            try
            {
                string searchString = Set.Name_Stan;
                if (listBox1.Visible == true)
                {  // если видно весь список переключаем на избранное
                    bt_favorit.Text = MyStrings.button9text;//Все радиостанции
                    bt_favorit.TextAlign = ContentAlignment.MiddleCenter;
                    bt_re_favorit.Text = MyStrings.button8text2;//Удалить из избранного
                    bt_favorit.Image = Properties.Resources.favorites3;
                    listBox2.Visible = true;
                    listBox1.Visible = false;
                    // если в избранном станция есть, выбираем её
                    if (!((searchString ?? "") == (string.Empty ?? "")))
                    {
                        int i = listBox2.FindString(searchString);
                        if (!(i == -1))
                        {
                            listBox2.SelectedIndex = i;
                        }
                        else
                        {
                            listBox2.SelectedItem = null;
                        }
                    }
                }
                else
                {
                    // если видимый избранный список,переключаем на весь список
                    bt_favorit.Text = MyStrings.button9text2; //В избранное
                    bt_favorit.TextAlign = ContentAlignment.MiddleCenter;
                    bt_re_favorit.Text = MyStrings.button8text; //Добавить в избранное
                    bt_favorit.Image = null;
                    listBox1.Visible = true;
                    listBox2.Visible = false;
                    // выделяем выбраную станцию 
                    if (!((searchString ?? "") == (string.Empty ?? "")))
                    {
                        int i = listBox1.FindString(searchString);
                        if (!(i == -1))
                        {
                            listBox1.SelectedIndex = i;
                        }
                        else
                        {
                            listBox1.SelectedItem = null;
                        }
                    }
                }
            }
            catch
            { }
        }
        private void button9_MouseUp(object sender, MouseEventArgs e)
        {
            AxWindowsMediaPlayer1.Focus();
        }
        private void button1_Click(object sender, EventArgs e)
        {    //визуальные ефекты Эфект Spectrum
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey Effect = currentUserKey.CreateSubKey("Software\\Microsoft\\MediaPlayer\\Preferences", RegistryKeyPermissionCheck.ReadWriteSubTree);
            Effect.SetValue("CurrentEffectType", "Bars");
            Effect.SetValue("CurrentEffectPreset", unchecked((int)0x00000000), RegistryValueKind.DWord);
            Effect.SetValue("ShowEffects", unchecked((int)0x00000001), RegistryValueKind.DWord);
         
            Application.Restart();
        }
        private void button2_Click(object sender, EventArgs e)
        {    //визуальные ефекты Эфект Lotus
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey Effect = currentUserKey.CreateSubKey("Software\\Microsoft\\MediaPlayer\\Preferences", RegistryKeyPermissionCheck.ReadWriteSubTree);
            Effect.SetValue("CurrentEffectType", "Battery");
            Effect.SetValue("CurrentEffectPreset", unchecked((int)0x00000011), RegistryValueKind.DWord);
            Effect.SetValue("ShowEffects", unchecked((int)0x00000001), RegistryValueKind.DWord);
          
            Application.Restart();
        }
        private void button3_Click(object sender, EventArgs e)
        {   //визуальные ефекты Эфект Alchemy
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey Effect = currentUserKey.CreateSubKey("Software\\Microsoft\\MediaPlayer\\Preferences", RegistryKeyPermissionCheck.ReadWriteSubTree);
            Effect.SetValue("CurrentEffectType", "Alchemy");
            Effect.SetValue("CurrentEffectPreset", unchecked((int)0x00000000), RegistryValueKind.DWord);
            Effect.SetValue("ShowEffects", unchecked((int)0x00000001), RegistryValueKind.DWord);
          
            Application.Restart();
        }
        private void button4_Click(object sender, EventArgs e)
        {  //визуальные ефекты Случайные эфекты
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey Effect = currentUserKey.CreateSubKey("Software\\Microsoft\\MediaPlayer\\Preferences", RegistryKeyPermissionCheck.ReadWriteSubTree);
            Effect.SetValue("CurrentEffectType", "Battery");
            Effect.SetValue("CurrentEffectPreset", unchecked((int)0x00000000), RegistryValueKind.DWord);
            Effect.SetValue("ShowEffects", unchecked((int)0x00000001), RegistryValueKind.DWord);
          
            Application.Restart();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // кнопка без звука
            try
            {
                if (Set.quiet == "on")
                {
                    Properties.Settings.Default.Volume = colorSlider1.Value;
                    Properties.Settings.Default.Save();
                    colorSlider1.Value = 0;
                    lab_sound_level.Text = "0%";
                    this.AxWindowsMediaPlayer1.settings.volume = colorSlider1.Value;
                    picBox_sound_icon.Image = Properties.Resources.kmixdocked_mute_off;
                    picBox_sound_icon.SizeMode = PictureBoxSizeMode.Zoom;
                    Set.quiet = "";
                    picBox_equalizer_icon.Visible = false;
                }
                else
                {
                   colorSlider1.Value = Properties.Settings.Default.Volume ;
                    this.AxWindowsMediaPlayer1.settings.volume = colorSlider1.Value;
                    this.lab_sound_level.Text = Math.Round(colorSlider1.Value / (double)(colorSlider1.Maximum - colorSlider1.Minimum) * 100 + 0, 1) + "%";
                    picBox_sound_icon.Image = Properties.Resources.kmixdocked_mute_4470;
                    picBox_sound_icon.SizeMode = PictureBoxSizeMode.Zoom;
                    Set.quiet = "on";
                    picBox_equalizer_icon.Visible = true;
                }
            }
            catch
            { }
        }
        private void picUp_Click(object sender, EventArgs e)
        {
            //перемещение вверх
            if (listBox1.Visible == true)
            {
                if (listBox1.SelectedIndex > 0)
                {
                    listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
                }
            }
            else
            if (listBox2.SelectedIndex > 0)
            {
                listBox2.SelectedIndex = listBox2.SelectedIndex - 1;
            }
        }
        private void picDown_Click(object sender, EventArgs e)
        {            //перемещение вниз
            try
            {
                if (listBox1.Visible == true)
                {
                    listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
                }
                else
                {
                    listBox2.SelectedIndex = listBox2.SelectedIndex + 1;
                }
            }
            catch
            { }

        }
        private void pictureBox5_DoubleClick(object sender, EventArgs e)
        {  // двойной клик очистит логотип
            PicBox_logo2.Image = null;
        }
        private void picFdialog_Click(object sender, EventArgs e)
        {
            // Кнопка выбора логотипа
            LogDialog();
        }
        private void LogDialog()
        {         // диалог выбора логотипа
            try
            {
                //создание нового диалога
                OpenFileDialog OpenFile = new OpenFileDialog();
                //выставляется фильтр с форматами файлов которые будут отображены в диалоговом окне
                OpenFile.Filter = "Image Files (*.JPEG, *.JPG,*.BMP, *.PNG)|*.jpeg;*.jpg;*.bmp;*.png";
                //Условие: если в диалоговом окне была нажата кнопка ОК
                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    Bitmap Pic = new Bitmap(OpenFile.FileName);
                    PicBox_logo2.Image = Pic;
                    float x = PicBox_logo2.Width; // Ширина клиентской области
                    float y = PicBox_logo2.Height; // Высота клиентской области
                    float z1 = PicBox_logo2.Image.Width; //Ширина загруженного изображения
                    float z2 = PicBox_logo2.Image.Height; //Высота загруженного изображения

                    if (z1 != x | z2 != y)
                    {                       
                        using (new CenterWinDialog(this))
                        {
                            DialogResult dialogResult = MessageBox.Show(MyStrings.picFdialog_C +
                        "\n" + MyStrings.picFdialog_C1 +
                        "\n" + MyStrings.picFdialog_C2 + z1 + MyStrings.picFdialog_C3 + z2 + MyStrings.picFdialog_C4 +
                        "\n" + MyStrings.picFdialog_C5, MyStrings.picFdialog_C6,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dialogResult == DialogResult.No)
                            {                              
                                //StretchImage растянет изображение по границам pictureBox1, а Zoom растянет пропорционально!
                                PicBox_logo2.SizeMode = PictureBoxSizeMode.StretchImage;
                                //создание нового битмапа (картинки) в нем OpenFile.FileName (то есть создается объект (изображение) из пути //который Вы выбрали в файл диолог)
                               
                                PicBox_logo2.Image = Pic;
                            }

                            else if (dialogResult == DialogResult.Yes)
                            {
                                PicBox_logo2.Image = null;
                                using (new CenterWinDialog(this))
                                    MessageBox.Show(MyStrings.picFdialog_C7, MyStrings.picFdialog_C6); //Логотип не выбран", "Выбор логотипа
                            }
                        }
                    }
                    else
                    {
                        PicBox_logo2.SizeMode = PictureBoxSizeMode.StretchImage;
                        PicBox_logo2.Image = Pic;        
                    }
                }
            }
            catch (Exception ex)
            {
                using (new CenterWinDialog(this))
                    MessageBox.Show(MyStrings.picFdialog_C8, MyStrings.picFdialog_C6 + (ex.ToString())); //Не вдалося внести зміни", "Выбор логотипа"
            }
    }

        private void picDel_Click(object sender, EventArgs e)
        {
            // Кнопка удалить выбранную станцию
            using (new CenterWinDialog(this))
            {
                DialogResult dialogResult = MessageBox.Show(MyStrings.picDel_C1 +   //Ви дійсно хочете видалити запис?"
                "\n" + MyStrings.picDel_C2 + TBname.Text +                         //Радіо станція: 
                "\n" + MyStrings.picDel_C3, MyStrings.picDel_C,                   //Запис неможливо буде відновити!" удаление записи
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {    // удалить строку в базе данных
                        string Nomer = TBname.Text;
                        string connectionString = ("Data Source=" + path + baseName);
                        SQLiteConnection conn = new SQLiteConnection(connectionString);
                        conn.Open();
                        SQLiteCommand myCommand = conn.CreateCommand();
                        myCommand.CommandText = "DELETE FROM ONplaylist WHERE STANSIA = @st";
                        myCommand.Parameters.Add("@st", DbType.String);
                        myCommand.Parameters["@st"].Value = Nomer;
                        int UspeshnoeIzmenenie = myCommand.ExecuteNonQuery();

                        SQLiteCommand myCommand2 = conn.CreateCommand();
                        myCommand2.CommandText = "DELETE FROM ONFavorit WHERE STANSIA = @st";
                        myCommand2.Parameters.Add("@st", DbType.String);
                        myCommand2.Parameters["@st"].Value = Nomer;


                        int UspeshnoeIzmenenie2 = myCommand2.ExecuteNonQuery();

                        Update_list(); // обновить лист радиостанций
                        Update_list2();

                        listBox1.SelectedIndex = 0;

                    }
                    catch (Exception ex)
                    {
                        using (new CenterWinDialog(this))
                            MessageBox.Show(MyStrings.picDel_C4, MyStrings.picDel_C);  //"Не вдалося внести зміни", "Видалення запису"
                        MessageBox.Show(ex.ToString());
                    }

                }
                else if (dialogResult == DialogResult.No)
                {
                    using (new CenterWinDialog(this))
                        MessageBox.Show(MyStrings.picDel_C5, MyStrings.picDel_C);  //Зміни в записах не внесені", "Видалення запису"
                }
            }
        }

        private void picAdd_Click(object sender, EventArgs e)
        {
            //Добавить новую станцию
            TBname.Text = "";
            Tburl.Text = "";
            PicBox_logo2.Image = null;
            Set.Redakt = "N";
            
        }
        private void picSave_Click(object sender, EventArgs e)
        {
            //Кнопка сохранить
        
            if (Set.Redakt == "N") //если новая запись
            {
                try
                {
                    if ((TBname.Text.Length == 0 && TBname.Text == "") || (Tburl.Text.Length == 0 && Tburl.Text == ""))
                    {
                         using (new CenterWinDialog(this))
                        MessageBox.Show(MyStrings.picSave_C1, MyStrings.picSave_C, MessageBoxButtons.OK, MessageBoxIcon.Information); // вывести сообщение  
                        return;
                    }
                    else
                    {                       
                        {
                             if(PicBox_logo2.Image != null)
                            {
                                byte[] imagesBytes = Image2Byte(PicBox_logo2.Image);
                                SaveImage(imagesBytes);
                            }
                            else
                            {
                                byte[] imagesBytes = Image2Byte(Properties.Resources.no_image);
                                SaveImage(imagesBytes);
                            }                         
                        }
                        Set.Redakt = "";
                    }
                }
                catch (Exception ex)
                {
                    using (new CenterWinDialog(this))
                  
                        MessageBox.Show(ex.Message);
                }
            }
            else
            {   // если редактировать существующую
               try
                {                   
                    if (TBname.Text == "" || Tburl.Text == "")
                    {
                         using (new CenterWinDialog(this))
                        MessageBox.Show(MyStrings.picSave_C1, MyStrings.picSave_C, MessageBoxButtons.OK, MessageBoxIcon.Information); // вывести сообщение  
                        return;
                    }
                    else
                      using (new CenterWinDialog(this))
                    {
                        DialogResult dialogResult = MessageBox.Show(MyStrings.picSave_C2 +         //Ви хочете змінити запис?"
                  "\n" + MyStrings.picDel_C2 + Set.Name_St  + MyStrings.picSave_C3 + TBname.Text +       // Радіо станція: " + Set.Name_St  +  "  буде змінена на
                  "\n" + MyStrings.picDel_C3, MyStrings.picSave_C,                           // " Запис неможливо буде відновити!", "Редактирование  записей"
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {

                            DB = new SQLiteConnection();
                            DB.ConnectionString = "Data Source=" + path + baseName;
                            DB.Open();
                            cmd = new SQLiteCommand("UPDATE [ONplaylist] SET (STANSIA, URL_ST, IMAGE_ST) = (@sta, @ur, @0)  WHERE [STANSIA] = @id", DB);
                            cmd.Parameters.AddWithValue("@sta", TBname.Text);
                            cmd.Parameters.AddWithValue("@ur", Tburl.Text);

                                byte[] imagesBytes = Image2Byte(PicBox_logo2.Image);
                               
                                //IMAGE_ST
                                cmd.Parameters.AddWithValue("@0", (imagesBytes));
                                cmd.Parameters.AddWithValue("@id", Set.Name_St.ToString());
                            try
                               {
                                cmd.ExecuteNonQuery();
                                           using (new CenterWinDialog(this))
                      MessageBox.Show(MyStrings.picDel_C2 + Set.Name_St + MyStrings.picSave_C4 + TBname.Text); //" Радіо станція: " + Set.Name_St + " - змінена на " + TBname.Text)
                                }
                            catch (Exception ex)
                            {
                                    using (new CenterWinDialog(this))
                                        MessageBox.Show(ex.Message);
                            }
                                DB.Close();
                                {                 
                                Set.Name_St = "";                    
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                             using (new CenterWinDialog(this))
                            MessageBox.Show(MyStrings.picDel_C5, MyStrings.picSave_C); //Зміни не внесені", "Зміна запису"
                            }
                    }
                }
                   catch (Exception ex)
                {
                    using (new CenterWinDialog(this))
                        MessageBox.Show(ex.Message);
                }                
            }
            Update_list();
            // получить строку для поиска последней проиграной станции
            string searchString = TBname.Text;
            if (!((searchString ?? "") == (string.Empty ?? "")))
            {
                int i = listBox1.FindString(searchString);
                if (!(i == -1))
                {
                    listBox1.SelectedIndex = i;
                }
                PicBox_logo.Image = PicBox_logo2.Image;
            }
        }
    
        private void SaveImage(byte[] imageBytes2Save)
        {
            // сохраняем в базу  строки и  фото
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path + baseName);
            string Query_ = "INSERT INTO ONplaylist (STANSIA, URL_ST, IMAGE_ST) VALUES ('" + TBname.Text + "','" + Tburl.Text + "',  @0);";
            SQLiteParameter PicParam = new SQLiteParameter("@0", DbType.Binary);
            PicParam.Value = imageBytes2Save;
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand(Query_, con);
            cmd.Parameters.Add(PicParam);
            cmd.ExecuteNonQuery();
            con.Close();

            using (new CenterWinDialog(this))
                MessageBox.Show( MyStrings.picDel_C2 + TBname.Text + MyStrings.SaveImag);         
        }

        private byte[] Image2Byte(Image imageToconvert)
        {            //кодируем фото в строку
           
                MemoryStream memoryStream = new MemoryStream();
                imageToconvert.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] bytesOfImage = memoryStream.ToArray();
                return bytesOfImage;
           
        }

      
        private void colorSlider1_ValueChanged(object sender, EventArgs e)
        {
            // установка уровня звука на слайдере громкости
            this.AxWindowsMediaPlayer1.settings.volume = colorSlider1.Value;
            this.lab_sound_level.Text = Math.Round(colorSlider1.Value / (double)(colorSlider1.Maximum - colorSlider1.Minimum) * 100 + 0, 1) + "%";
            picBox_sound_icon.Image = Properties.Resources.kmixdocked_mute_4470;
            picBox_sound_icon.SizeMode = PictureBoxSizeMode.Zoom;
            Set.quiet = "on";
            if (colorSlider1.Value == 0)
            {
                picBox_sound_icon.Image = Properties.Resources.kmixdocked_mute_off;
                picBox_sound_icon.SizeMode = PictureBoxSizeMode.Zoom;
                Set.quiet = "";
                picBox_equalizer_icon.Visible = false;
            }
            else
            {
                picBox_sound_icon.Image = Properties.Resources.kmixdocked_mute_4470;
                picBox_sound_icon.SizeMode = PictureBoxSizeMode.Zoom;
                Set.quiet = "on";
                picBox_equalizer_icon.Visible = true;
            }
        }
        private void Redactor_R_Click(object sender, EventArgs e)
        { // кнопка открыть редактор     
            listBox1.Visible = true;
            bt_re_favorit.Enabled = false;
            bt_favorit.Enabled = false;

            if (Height == 505) // минимальная длина формы
            {
            t = new Timer(components);
                t.Start();
                {
                    t.Interval = 14;
                    t.Tick += delegate
                    {
                        if (Height < 590)
                            Height += 5;
                        else
                            t.Stop();

                        if (Height == 590)
                        {
                            panel5.Size = new Size(286, 252);
                            panel5.Visible = true;
                            bt_eject.Enabled = false;
                            Set.OpenRedakt = "Open";
                            TBname.Focus();
                        }
                    };
                }
            }
        }
        private void Radio_Click(object sender, EventArgs e)
        {  //нажимаем  кнопка в трее 
            WindowState = FormWindowState.Normal; //разворачиваем форму
            Radio.Visible = false;        // скрываем кнопку в трее
            ShowInTaskbar = true;       //отображаем ярлык на панели
            timer4.Stop();
        }
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        private void stopRec()
        {

            saveFileDialog1.Title = "Select output file:";
            saveFileDialog1.Filter = "mp3 Files (*.mp3)|*.mp3";


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
            }


        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //кнопка запись 

            if (Set.Lrec != "R")
            {           //("запускаем запись?");
                using (new CenterWinDialog(this))
                {
                    DialogResult dialogResult = MessageBox.Show(MyStrings.Start_record +   //Начать запись?"
                    "\n" + TBname.Text, MyStrings.Start_record,                        //Радіо станція: 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        picBox_record_bt.Image = Properties.Resources.rec_1;
                        picBox_record_bt.SizeMode = PictureBoxSizeMode.StretchImage;
                       Set.Lrec = "R";
                        // запускаем запись
                        if (!Directory.Exists(pathRec))
                        {  // проверяем есть ли папка для записи,если нет создаем
                            Directory.CreateDirectory(pathRec);
                        }                    
                        bgnThread.RunWorkerAsync(); // запускаем поток "Идет запись"
                        timer7.Start();                       
                        listBox1.Enabled = false;
                        listBox2.Enabled = false;
                        bt_re_favorit.Enabled = false;
                        bt_favorit.Enabled = false;
                        picserch.Enabled = false;
                        bt_pause.Enabled = false;
                        bt_previous.Enabled = false;
                        bt_next.Enabled = false;
                        Redactor_R.Visible = false;                      
                        bt_effect1.Enabled = false;
                        bt_effect2.Enabled = false;
                        bt_effect3.Enabled = false;
                        bt_effect4.Enabled = false;

                        sWatch = Stopwatch.StartNew();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        // ничего не делаем
                        return;
                    }
                }
            }
            else
            {
                //остановить запись
                using (new CenterWinDialog(this))
                {     // остановить запись?
                    DialogResult dialogResult = MessageBox.Show(MyStrings.Stop_recording +   //остановить запись?"
                       "\n" + TBname.Text, MyStrings.Stop_recording,                        //Радіо станція: 
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        picBox_record_bt.Image = Properties.Resources.rec_bt1;
                        picBox_record_bt.SizeMode = PictureBoxSizeMode.StretchImage;
                        Set.Lrec = null;
                        bgnThread.CancelAsync();
                        timer7.Stop();
                         
                        sWatch.Stop();
                   
                        listBox1.Enabled = true;
                        listBox2.Enabled = true;
                        bt_re_favorit.Enabled = true;
                        bt_favorit.Enabled = true;
                        picserch.Enabled = true;
                        bt_pause.Enabled = true;
                        bt_previous.Enabled = true;
                        bt_next.Enabled = true;
                        Redactor_R.Visible = true;
                        bt_effect1.Enabled = true;
                        bt_effect2.Enabled = true;
                        bt_effect3.Enabled = true;
                        bt_effect4.Enabled = true;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        // ничего не делаем
                        return;
                    }
                }
            }
        }
      
        public void bgnThread_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // запись выполняется в отдельном потоке
            WebClient wcDownload = new WebClient();
            try
            {
                Stream strResponse = wcDownload.OpenRead(Set.urladdress);
                FileStream strLocal = new FileStream(pathRec + Set.Name_Stan + "_(" + DateTime.Now.ToString("yyyy-MM-dd_HH/mm/ss") + ").mp3", FileMode.Create, FileAccess.Write, FileShare.None);
                int bytesSize = 0;
                byte[] downBuffer = new byte[2049];
                do
                {
                    bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length);
                    strLocal.Write(downBuffer, 0, bytesSize);
                } while (!(bytesSize == 0 || bgnThread.CancellationPending)); // выход также если запрошено завершение
                strLocal.Close();
                strResponse.Close();
            }
            catch (Exception ex)
            {
               bt_stop.Click += new EventHandler(bt_stop_Click);
              //  defaultInstance = null;
                using (new CenterWinDialog(this))
                    MessageBox.Show(ex.Message);
                Set.Lrec = null;
                bgnThread.Dispose();
            }
        }

       

        public void recordstop()
        {

           

            picBox_record_bt.Image = Properties.Resources.rec_bt1;
            picBox_record_bt.SizeMode = PictureBoxSizeMode.StretchImage;
                    Set.Lrec = null;
                    bgnThread.CancelAsync();
                    timer7.Stop();
                    sWatch.Stop();
                    listBox1.Enabled = true;
                    listBox2.Enabled = true;
                    bt_re_favorit.Enabled = true;
                    bt_favorit.Enabled = true;
                    picserch.Enabled = true;
                    bt_pause.Enabled = true;
                    bt_previous.Enabled = true;
                    bt_next.Enabled = true;
                    Redactor_R.Visible = true;
                    bt_effect1.Enabled = true;
                    bt_effect2.Enabled = true;
                    bt_effect3.Enabled = true;
                    bt_effect4.Enabled = true;

          
        }
        public void bgnThread_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (Set.Lrec == "R")
                {
                    recordstop();
                    // return;
                }
                using (new CenterWinDialog(this))
                {    //Відкрити папку із файлом?"
                    DialogResult dialogResult = MessageBox.Show(MyStrings.Recording_saved +   
                       "\n" + pathRec + "\n" + MyStrings.Open_folder, MyStrings.button8Click2,                     
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                     Process.Start("explorer",  pathRec);
                    }

                    else if (dialogResult == DialogResult.No)
                    { // ничего не делаем
                        return;
                    }
                }
            }
            catch { }
        }
        private void timer7_Tick(object sender, EventArgs e)
        { // таймер записи станции
            TimeSpan interval = TimeSpan.FromMilliseconds(sWatch.ElapsedMilliseconds);
          time = interval.Hours.ToString("00") + ":" + interval.Minutes.ToString("00") + ":" + interval.Seconds.ToString("00");
 
        }
        private void Timer1_Tick(object sender, EventArgs e)
        
            {
                if (!AxWindowsMediaPlayer1.isOnline)
                {
                    AxWindowsMediaPlayer1.Ctlcontrols.stop();
                    bt_pause.Visible = false;
                    bt_play.Visible = true;
                    toolStripStatusLabel2.Text = MyStrings.toolStripStatusLabeltext3; //Нет подключения к интернету!
                    toolStripStatusLabel2.ForeColor = Color.Red;
                    toolStripStatusLabel2.Font = new Font("Cambria", 12.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                    Text = "OnRadio " + Set.vers;
                    Timer2.Stop();
                    toolStripStatusLabel1.Text = "";
                    picBox_equalizer_icon.Visible = false;
                    picBox_record_bt.Visible = false;
            }
                else
                {
                    if (AxWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                    
                    {
                        AxWindowsMediaPlayer1.Ctlcontrols.play();
                        bt_pause.Visible = true;
                        bt_play.Visible = false;
                    }
                    if (AxWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying) // если музыка играет
                    {
                    picBox_record_bt.Enabled = true;
                    if (Set.Lrec == "R")
                    {  // если запись включена то пишем в  строке статуса
                        toolStripStatusLabel2.Text = MyStrings.is_recorded + " " + time; //запись идет!
                        toolStripStatusLabel2.ForeColor = Color.Crimson;
                        toolStripStatusLabel2.Font = new Font("Cambria", 12.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                        Text = "OnRadio "+ MyStrings.is_recorded;
                    }
                    else
                    {
                        //  "Cкорость потока: 
                        double d = AxWindowsMediaPlayer1.network.bandWidth / 1000;
                        string s = string.Format("{0:0}", d);
                        toolStripStatusLabel2.Text = MyStrings.toolStripStatusLabeltext6 + " " + s + MyStrings.toolStripStatusLabeltext7;

                        if (toolStripStatusLabel2.Text == MyStrings.toolStripStatusLabeltext6 + "0" + MyStrings.toolStripStatusLabeltext7)
                        {
                            toolStripStatusLabel2.Text = "";
                        }

                        else
                        {
                            toolStripStatusLabel2.Text = MyStrings.toolStripStatusLabeltext6 + " " + s + MyStrings.toolStripStatusLabeltext7;
                            toolStripStatusLabel2.ForeColor = Color.Yellow;
                            toolStripStatusLabel2.Font = new Font("Cambria", 12.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                            Timer2.Start();
                           
                            bt_pause.Visible = true;
                            bt_play.Visible = false;
                           
                        }
                        Text = "OnRadio " + Set.vers;
                    }
                   
                }
                    else
                    {
                        if (AxWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsTransitioning)
                        {
                            Timer1.Stop();
                            toolStripStatusLabel1.Text = "";
                            toolStripStatusLabel2.Text = MyStrings.toolStripStatusLabeltext4; //Подключение. . . . . . . . . . .
                            toolStripStatusLabel2.ForeColor = Color.Yellow;
                            toolStripStatusLabel2.Font = new Font("Cambria", 12.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                            Timer1.Interval = 6000;
                            Timer1.Start();
                            Timer2.Stop();
                            picBox_equalizer_icon.Visible = false;
                             picBox_record_bt.Visible = false;
                               
                    }
                        else
                        {
                            AxWindowsMediaPlayer1.Ctlcontrols.stop();
                            Timer1.Stop();
                            toolStripStatusLabel2.Text = MyStrings.toolStripStatusLabeltext5; //Сервер выключен или не отвечает!
                            toolStripStatusLabel2.ForeColor = Color.Blue;
                            toolStripStatusLabel2.Font = new Font("Cambria", 10.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                            Text = "OnRadio " + Set.vers;                      
                        try
                            {
                                AxWindowsMediaPlayer1.URL = Set.urladdress;
                            AxWindowsMediaPlayer1.Ctlcontrols.play();
                            }
                            catch { }
                            Timer1.Interval = 6000;
                            Timer1.Start();
                            Timer2.Stop();
                            toolStripStatusLabel1.Text = "";
                            picBox_equalizer_icon.Visible = false;
                            picBox_record_bt.Visible = false;
                             
                    }
                    }
                }
                if (AxWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying) // если музыка играет
                {
                    Timer1.Interval = 1000;
                    Timer1.Start();
                    vrema.Text = DateTime.Now.ToString("yyyy-MM-dd") + "             " + DateTime.Now.ToString("HH:mm:ss");
                    if (Set.quiet == "on")
                        picBox_equalizer_icon.Visible = true;
                        picBox_record_bt.Visible= true;
                           
            }
            }
        
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (toolStripStatusLabel1.Text == Set.Name_Stan) 
            {
                toolStripStatusLabel1.Text = "";
            }
            else
            {
                toolStripStatusLabel1.Text = Set.Name_Stan;
            }
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            //бегущая строка
            if (vrema.Left > -vrema.Width)
            {
                vrema.Left -= 1;
            }
            else
            {
                vrema.Left = panel1.Width;
            }
        }
        private void timer4_Tick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Radio.Visible = true; // задаем иконку всплывающей подсказки
                Radio.BalloonTipIcon = ToolTipIcon.Info;    // задаем текст подсказки
                Radio.BalloonTipText = MyStrings.RadioBTip2 + "" + Set.Name_Stan; // устанавливаем зголовк "Сейчас в эфире:  "
                Radio.BalloonTipTitle = "OnRadio";   // отображаем подсказку 6 секунд
                Radio.ShowBalloonTip(6);
            }
        }
        
        private void timer6_Tick(object sender, EventArgs e)
        {
            Combo_lang.Visible = false;

            timer6.Stop();
        }

        private async void Combo_lang_MouseLeave(object sender, EventArgs e)
        {
           await Task.Delay(2000);
     
        timer6.Start();
        }
        private void colorSlider1_MouseUp(object sender, MouseEventArgs e)
        {
            // установим фокус на списки после изменения громкости
            if (listBox1.Visible == true)
            {
                listBox1.Focus();
            }
            else
                listBox2.Focus();
        }
        private async void Form1_MouseLeave(object sender, EventArgs e)
        {
            // когда курсор ушол с формы 

            await Task.Delay(2000);
        Combo_lang.Visible = false;
            timer6.Stop();
        }
        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {  // кнопка свернуть в трей при наведении
            picBox_collapse_icon.Image = Properties.Resources._40_l;
        }
        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {  // кнопка свернуть в трей при отведении
            picBox_collapse_icon.Image = Properties.Resources._39_b;
        }
        private void bt_play_MouseDown(object sender, MouseEventArgs e)
        {    // при нажатии кнопки плей
            bt_play.Size = new Size(47, 47);
            bt_play.Location = new Point(15, 75);
        }
        private void bt_play_MouseEnter(object sender, EventArgs e)
        {   // при наведении мышей  кнопки плей
           
            bt_play.Size = new Size(58, 58);
            bt_play.Location = new Point(7, 71);
        }
        private void bt_play_MouseLeave(object sender, EventArgs e)
        {  // когда отходит мыша кнопки плей
          
            bt_play.Size = new Size(53, 53);
            bt_play.Location = new Point(10, 73);
        }
        private void bt_play_MouseUp(object sender, MouseEventArgs e)
        {     // при отпускании кнопки пауза
            bt_play.Size = new Size(53, 53);
            bt_play.Location = new Point(10, 73);
           
        }      
        private void bt_pause_MouseDown(object sender, MouseEventArgs e)
        {  // при нажатии кнопки пауза
            bt_pause.Size = new Size(47, 47);
            bt_pause.Location = new Point(15, 75);
        }
        private void bt_pause_MouseEnter(object sender, EventArgs e)
        {  // при наведении мышей  кнопки пауза
         
            bt_pause.Size = new Size(58, 58);
            bt_pause.Location = new Point(7, 71);
        }
        private void bt_pause_MouseLeave(object sender, EventArgs e)
        {  // когда отходит мыша кнопки пауза
          
            bt_pause.Size = new Size(53, 53);
            bt_pause.Location = new Point(10, 73);
        }
        private void bt_pause_MouseUp(object sender, MouseEventArgs e)
        {     // при отпускании кнопки пауза
            bt_pause.Size = new Size(53, 53);
            bt_pause.Location = new Point(10, 73);
          
        }       
        private void bt_previous_MouseDown(object sender, MouseEventArgs e)
        { // при нажатии кнопки назад
            bt_previous.Size = new Size(47, 47);
            bt_previous.Location = new Point(66, 75);
        }
        private void bt_previous_MouseEnter(object sender, EventArgs e)
        {    // при наведении мышей  кнопки назад
             
            bt_previous.Size = new Size(58, 58);
            bt_previous.Location = new Point(60, 71);
        }
        private void bt_previous_MouseLeave(object sender, EventArgs e)
        {    // когда отходит мыша кнопки назад
            bt_previous.Size = new Size(53, 53);
            bt_previous.Location = new Point(63, 73);
           
        }
        private void bt_previous_MouseUp(object sender, MouseEventArgs e)
        {    // при отпускании кнопки назад
            bt_previous.Size = new Size(53, 53);
            bt_previous.Location = new Point(63, 73);
          
        }       
        private void bt_stop_MouseDown(object sender, MouseEventArgs e)
        {     // при нажатии кнопки стоп
            bt_stop.Size = new Size(47, 47);
            bt_stop.Location = new Point(119, 75);
            
        }
        private void bt_stop_MouseEnter(object sender, EventArgs e)
        {
            // при наведении мышей  кнопки стоп
            

            bt_stop.Size = new Size(58, 58);
            bt_stop.Location = new Point(113, 71);
        }
        private void bt_stop_MouseLeave(object sender, EventArgs e)
        {  // когда отходит мыша кнопки стоп
           
            bt_stop.Size = new Size(53, 53);
            bt_stop.Location = new Point(116, 73);
        }
        private void bt_stop_MouseUp(object sender, MouseEventArgs e)
        {  // при отпускании кнопки стоп
            bt_stop.Size = new Size(53, 53);
            bt_stop.Location = new Point(116, 73);
           
        }     
        private void bt_next_MouseDown(object sender, MouseEventArgs e)
        {    // при нажатии кнопки дальше
            bt_next.Size = new Size(47, 47);
            bt_next.Location = new Point(172, 75);
        }
        private void bt_next_MouseEnter(object sender, EventArgs e)
        {    // при наведении мышей  кнопки дальше
             
            bt_next.Size = new Size(58, 58);
            bt_next.Location = new Point(167, 71);
        }
        private void bt_next_MouseLeave(object sender, EventArgs e)
        {  // когда отходит мыша кнопки дальше
            bt_next.Size = new Size(53, 53);
            bt_next.Location = new Point(169, 73);
           
        }
        private void bt_next_MouseUp(object sender, MouseEventArgs e)
        {     // при отпускании кнопки дальше
            bt_next.Size = new Size(53, 53);
            bt_next.Location = new Point(169, 73);
           
        } 
        private void bt_eject_MouseDown(object sender, MouseEventArgs e)
        {     // при нажатии кнопки свернуть
            bt_eject.Size = new Size(47, 47);
            bt_eject.Location = new Point(225, 75);
        }
        private void bt_eject_MouseEnter(object sender, EventArgs e)
        {  // при наведении мышей  кнопки свернуть
           
            bt_eject.Size = new Size(58, 58);
            bt_eject.Location = new Point(221, 71);
        }
        private void bt_eject_MouseLeave(object sender, EventArgs e)
        {     // когда отходит мыша кнопки свернуть
              
            bt_eject.Size = new Size(53, 53);
            bt_eject.Location = new Point(222, 73);
        }
        private void bt_eject_MouseUp(object sender, MouseEventArgs e)
        {      // при отпускании кнопки свернуть
            bt_eject.Size = new Size(53, 53);
            bt_eject.Location = new Point(222, 73);
           
        }      
        private void bt_eject1_MouseDown(object sender, MouseEventArgs e)
        {      // при нажатии кнопки развернуть
            bt_eject1.Size = new Size(47, 47);
            bt_eject1.Location = new Point(225, 75);
        }
        private void bt_eject1_MouseEnter(object sender, EventArgs e)
        {  // при наведении мышей  кнопки развернуть
           
            bt_eject1.Size = new Size(58, 58);
            bt_eject1.Location = new Point(221, 71);
        }
        private void bt_eject1_MouseLeave(object sender, EventArgs e)
        {  // когда отходит мыша кнопки развернуть
           
            bt_eject1.Size = new Size(53, 53);
            bt_eject1.Location = new Point(222, 73);
        }
        private void bt_eject1_MouseUp(object sender, MouseEventArgs e)
        {     // при отпускании кнопки развернуть
            bt_eject1.Size = new Size(53, 53);
            bt_eject1.Location = new Point(222, 73);
          
        }
        private void picserch_MouseEnter(object sender, EventArgs e)
        {
            picserch.BackColor = Color.Crimson;
        }
        private void picserch_MouseLeave(object sender, EventArgs e)
        {
            picserch.BackColor = Color.Transparent;
        }
        private void button1_MouseEnter(object sender, EventArgs e)
        {

            bt_effect1.Size = new Size(65, 25);
            bt_effect1.Text = "Spectrum";

        }
        private async void button1_MouseLeave(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            bt_effect1.Size = new Size(65, 10);
            bt_effect1.Text = "";
        }
        private void button2_MouseEnter(object sender, EventArgs e)
        {

            bt_effect2.Size = new Size(65, 25);
            bt_effect2.Text = "Lotus";

        }
        private async void button2_MouseLeave(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            bt_effect2.Size = new Size(65, 10);
            bt_effect2.Text = "";
        }
        private void button3_MouseEnter(object sender, EventArgs e)
        {

            bt_effect3.Size = new Size(65, 25);
            bt_effect3.Text = "Alchemy";

        }
        private async void button3_MouseLeave(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            bt_effect3.Size = new Size(65, 10);
            bt_effect3.Text = "";
        }
        private void button4_MouseEnter(object sender, EventArgs e)
        {

            bt_effect4.Size = new Size(65, 25);
            bt_effect4.Text = "Random";
        }
        private async void button4_MouseLeave(object sender, EventArgs e)
        {

            await Task.Delay(2000);
            bt_effect4.Size = new Size(65, 10);
            bt_effect4.Text = "";


        }    
        private void Localise_set_MouseEnter(object sender, EventArgs e)
        {   // при наведении мышей  кнопки язык
            Localise_set.Size = new Size(36, 36);
            Localise_set.Location = new Point(199, 210);
           
        }
        private void Localise_set_MouseLeave(object sender, EventArgs e)
        {      // когда отходит мыша кнопки язык
              
            Localise_set.Size = new Size(32, 32);
            Localise_set.Location = new Point(201, 212);
        }
        private void Localise_set_MouseUp(object sender, MouseEventArgs e)
        {      // при отпускании кнопки язык
            Localise_set.Size = new Size(32, 32);
            Localise_set.Location = new Point(201, 212);
          
        }
        private void Redactor_R_MouseEnter(object sender, EventArgs e)
        {   // при наведении мышей  кнопки редактор
            
            Redactor_R.Size = new Size(32, 32);
            Redactor_R.Location = new Point(246, 22);
        }
        private void Redactor_R_MouseLeave(object sender, EventArgs e)
        {      // когда отходит мыша кнопки редактор
            
            Redactor_R.Size = new Size(24, 24);
            Redactor_R.Location = new Point(250, 26);
        }
        private void Redactor_R_MouseDown(object sender, MouseEventArgs e)
        {       // при нажатии кнопки редактор
            Redactor_R.Size = new Size(22, 22);
            Redactor_R.Location = new Point(251, 27);
        }
        private void Redactor_R_MouseUp(object sender, MouseEventArgs e)
        {     // при отпускании кнопки редактор
            Redactor_R.Size = new Size(24, 24);
            Redactor_R.Location = new Point(250, 26);
            
        }
        private void picUp_MouseEnter(object sender, EventArgs e)
        {
            picUp.BackColor = Color.Crimson;
        }
        private void picUp_MouseDown(object sender, MouseEventArgs e)
        {
            picUp.Size = new Size(30, 30);
            picUp.Location = new Point(15, 137);
        }
        private void picUp_MouseUp(object sender, MouseEventArgs e)
        {
            picUp.Size = new Size(32, 32);
            picUp.Location = new Point(14, 138);
            picUp.BackColor = Color.Transparent;
        }
        private void picUp_MouseLeave(object sender, EventArgs e)
        {
            picUp.BackColor = Color.Transparent;
        }
        private void picDown_MouseEnter(object sender, EventArgs e)
        {
            picDown.BackColor = Color.Crimson;
        }
        private void picDown_MouseLeave(object sender, EventArgs e)
        {
            picDown.BackColor = Color.Transparent;
        }
        private void picDown_MouseDown(object sender, MouseEventArgs e)
        {
            picDown.Size = new Size(30, 30);
            picDown.Location = new Point(15, 173);
        }
        private void picDown_MouseUp(object sender, MouseEventArgs e)
        {
            picDown.Size = new Size(32, 32);
            picDown.Location = new Point(14, 170);
            picDown.BackColor = Color.Transparent;
        }
        private void picPly_MouseLeave(object sender, EventArgs e)
        {    // когда отходит мыша кнопки плей
             
            picPly.Size = new Size(32, 32);
            picPly.Location = new Point(16, 212);
        }
        private void picPly_MouseDown(object sender, MouseEventArgs e)
        {      // при нажатии кнопки плей
            picPly.Size = new Size(30, 30);
            picPly.Location = new Point(17, 213);
        }
        private void picPly_MouseUp(object sender, MouseEventArgs e)
        {       // при отпускании кнопки плей
            picPly.Size = new Size(32, 32);
            picPly.Location = new Point(16, 212);
           

        }
        private void picPly_MouseEnter(object sender, EventArgs e)
        {    // при наведении мышей  кнопки плей
            
            picPly.Size = new Size(36, 36);
            picPly.Location = new Point(15, 210);
        }
        private void picFdialog_MouseEnter(object sender, EventArgs e)
        {  // при наведении мышей  кнопки выбор логотипа
           
            picFdialog.Size = new Size(36, 36);
            picFdialog.Location = new Point(52, 210);
        }
        private void picFdialog_MouseLeave(object sender, EventArgs e)
        {       // когда отходит мыша кнопки выбор логотипа
              
            picFdialog.Size = new Size(32, 32);
            picFdialog.Location = new Point(53, 212);
        }
        private void picFdialog_MouseUp(object sender, MouseEventArgs e)
        {       // при отпускании кнопки выбор логотипа
            picFdialog.Size = new Size(32, 32);
            picFdialog.Location = new Point(53, 212);
         
        }
        private void picFdialog_MouseDown(object sender, MouseEventArgs e)
        {  // при нажатии кнопки выбор логотипа
            picFdialog.Size = new Size(30, 30);
            picFdialog.Location = new Point(54, 213);
        }
        private void picDel_MouseUp(object sender, MouseEventArgs e)
        {    // при отпускании кнопки удалить
            picDel.Size = new Size(32, 32);
            picDel.Location = new Point(90, 212);
           

        }
        private void picDel_MouseDown(object sender, MouseEventArgs e)
        {   // при нажатии кнопки  удалить
            picDel.Size = new Size(30, 30);
            picDel.Location = new Point(91, 213);
        }
        private void picDel_MouseEnter(object sender, EventArgs e)
        {  // при наведении мышей  кнопки удалить
           
            picDel.Size = new Size(36, 36);
            picDel.Location = new Point(89, 210);
        }
        private void picDel_MouseLeave(object sender, EventArgs e)
        {    // когда отходит мыша кнопки удалить
             
            picDel.Size = new Size(32, 32);
            picDel.Location = new Point(90, 212);
        }
        private void picAdd_MouseEnter(object sender, EventArgs e)
        {   // при наведении мышей  кнопки добавить
            
            picAdd.Size = new Size(36, 36);
            picAdd.Location = new Point(125, 210);
        }
        private void picAdd_MouseLeave(object sender, EventArgs e)
        {    // когда отходит мыша кнопки добавить
             
            picAdd.Size = new Size(32, 32);
            picAdd.Location = new Point(127, 212);
        }
        private void picAdd_MouseUp(object sender, MouseEventArgs e)
        {     // при отпускании кнопки добавить
            picAdd.Size = new Size(32, 32);
            picAdd.Location = new Point(127, 212);
          
        }
        private void picAdd_MouseDown(object sender, MouseEventArgs e)
        {   // при нажатии кнопки  добавить
            picAdd.Size = new Size(30, 30);
            picAdd.Location = new Point(128, 213);
        }
        private void picSave_MouseUp(object sender, MouseEventArgs e)
        {    // при отпускании кнопки сохранить
            picSave.Size = new Size(32, 32);
            picSave.Location = new Point(164, 212);
         
        }
        private void picSave_MouseDown(object sender, MouseEventArgs e)
        {   // при нажатии кнопки  сохранить
            picSave.Size = new Size(30, 30);
            picSave.Location = new Point(165, 213);
        }
        private void picSave_MouseEnter(object sender, EventArgs e)
        {   // при наведении мышей  кнопки сохранить
            
            picSave.Size = new Size(36, 36);
            picSave.Location = new Point(162, 210);
        }
        private void picSave_MouseLeave(object sender, EventArgs e)
        {  // когда отходит мыша кнопки сохранить
           
            picSave.Size = new Size(32, 32);
            picSave.Location = new Point(164, 212);
        }      
        private void picExit_MouseEnter(object sender, EventArgs e)
        {   // при наведении мышей  кнопки выход с редактора
             picExit.BackColor = Color.Crimson;
         
        }
        private void picExit_MouseLeave(object sender, EventArgs e)
        {    // когда отходит мыша кнопки выход с редактора
            picExit.BackColor = Color.Transparent;
         
        }      
        private void picExit_MouseUp(object sender, MouseEventArgs e)
        {
            // при отпускании кнопки закрыть редактор
          
            if (Height == 590) // средне Свернутая  длина окна формы
            {
                t = new Timer(components);
                t.Start();
                {
                    t.Interval = 14;
                    t.Tick += delegate
                    {
                        if (Height > 550)

                            Height -= 5;
                        else
                            t.Stop();

                        if (Height == 550)
                        {


                            Set.OpenRedakt = "";
                            bt_eject.Enabled = true;
                            bt_re_favorit.Enabled = true;
                            bt_favorit.Enabled = true;
                            Clipboard.Clear(); // очистить буфер обмена
                            Size = new Size(302, 505);
                            panel5.Size = new Size(286, 0);
                         
                            VacuumDB();

                        }
                    };
                }

            }



        }
        public void VacuumDB()
        {

            try
            {       // сжать базу данных
                using (SQLiteConnection con = new SQLiteConnection("Data Source=" + path + baseName))
                {
                    var cmd = new SQLiteCommand();
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "VACUUM;";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 30;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch //(Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {  // при отпускании кнопки без звука
            picBox_sound_icon.Size = new Size(57, 40);
            picBox_sound_icon.Location = new Point(0, 18);
            
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {  // при наведении мышей  кнопки без звука
            picBox_sound_icon.Size = new Size(62, 45);
            picBox_sound_icon.Location = new Point(-2, 16);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {   // при нажатии кнопки без звука
            picBox_sound_icon.Size = new Size(53, 36);
            picBox_sound_icon.Location = new Point(2, 20);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {   // когда отходит мыша кнопки без звука
            picBox_sound_icon.Size = new Size(57, 40);
            picBox_sound_icon.Location = new Point(0, 18);
        }
  
        private void TBname_TextChanged(object sender, EventArgs e)
        {
            //каждое новое слово в строке имя станций  будет с большой буквы

            if (((TextBox)sender).Text.Length == 1)
                ((TextBox)sender).Text = ((TextBox)sender).Text.ToUpper();
            ((TextBox)sender).Select(((TextBox)sender).Text.Length, 0);
        }
        private void TBname_Enter(object sender, EventArgs e)
        {        // Меняем раскладку в поле имя станций на  выбраный язык 
            try
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(CultureInfo.CurrentUICulture); 
            }
            catch { }
        }
        private void Tburl_Enter(object sender, EventArgs e)
        {   // Меняем раскладку в поле адрес  на английскую ( русский язык(1049),английскую(1033),Украинская (1058))
            try
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(System.Globalization.CultureInfo.GetCultureInfo(1033)); 
            }
            catch { }
        }
       
        private void Tburl_DoubleClick(object sender, EventArgs e)
        { // двойной клик очистит плое имя станций
            Tburl.Text = "";
            // Объявляет IDataObject для хранения данных, возвращаемых из буфера обмена.
            // Извлекает данные из буфера обмена.
            IDataObject iData = Clipboard.GetDataObject();

            // Определяет, находятся ли данные в формате, который вы можете использовать.
            if (iData.GetDataPresent(DataFormats.Text))
            {
                // Да, это так, отобразите его в текстовом поле.
                Tburl.Text = (String)iData.GetData(DataFormats.Text);
            }
            else
            {
                // если не так.
                Tburl.Text = MyStrings.IDataO;
            }
        }
        private void TBname_DoubleClick(object sender, EventArgs e)
        { //двойной клик очистит поле адрес
            TBname.Text = "";
        }
        private void TBname_KeyPress(object sender, KeyPressEventArgs e)
        {
            //запрещаем вводить пробел первым в поле имя станций
            if (TBname.Text.Length == 0)
                e.Handled = e.KeyChar == ' ';


            // по нажатии на Ентер переходим на другой текстбокс без звука
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                Tburl.Focus();
            }
        }
        private void Tburl_KeyPress(object sender, KeyPressEventArgs e)
        {
            //запрещаем вводить пробел первым в поле адреса
            if (Tburl.Text.Length == 0)
                e.Handled = e.KeyChar == ' ';


            // по нажатии на Ентер переходим на другой текстбокс без звука
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;


                AxWindowsMediaPlayer1.Ctlcontrols.pause();

                {
                    // Запускаем радио поток
                    if (Tburl.Text != "")
                    {
                        AxWindowsMediaPlayer1.URL = Tburl.Text;
                        AxWindowsMediaPlayer1.Ctlcontrols.play();
                    }
                }
                picFdialog.Focus();
                LogDialog(); // диалог выбора логотипа
            }
        }

        private void picPly_Click(object sender, EventArgs e)
        {
               AxWindowsMediaPlayer1.Ctlcontrols.pause();         
           {
                // Запускаем радио поток
                if (Tburl.Text != "")
                {
                    AxWindowsMediaPlayer1.URL = Tburl.Text;
                    AxWindowsMediaPlayer1.Ctlcontrols.play();
                }
           }
        }

        private void listBox1_MouseEnter(object sender, EventArgs e)
        {
            listBox1.Focus();
        }

        private void listBox2_MouseEnter(object sender, EventArgs e)
        {
            listBox2.Focus();
        }

        private void colorSlider1_MouseEnter(object sender, EventArgs e)
        {
            colorSlider1.Focus();
        }

        private void listBox1_MouseLeave(object sender, EventArgs e)
        {
            panel2.Focus();
        }

        private void listBox2_MouseLeave(object sender, EventArgs e)
        {
            panel2.Focus();
        }

        private void colorSlider1_MouseLeave(object sender, EventArgs e)
        {
            panel2.Focus();
        }
    }
    }

 
 

