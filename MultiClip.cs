using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace MultiClip
{
    public partial class MultiClip : Form
    {
        private Board board;
        private Settings settings;
        private string jsonFileBoard;
        private string jsonFileSettings;
        private KeyboardHook hook1 = new KeyboardHook();
        private KeyboardHook hook2 = new KeyboardHook();
        private KeyboardHook hook3 = new KeyboardHook();
        private KeyboardHook hook4 = new KeyboardHook();
        private KeyboardHook hook5 = new KeyboardHook();
        private KeyboardHook hook6 = new KeyboardHook();
        private KeyboardHook hook7 = new KeyboardHook();
        private KeyboardHook hook8 = new KeyboardHook();
        private KeyboardHook hook9 = new KeyboardHook();
        private KeyboardHook hook10 = new KeyboardHook();
        private KeyboardHook hook11 = new KeyboardHook();
        private KeyboardHook hook12 = new KeyboardHook();

        public MultiClip()
        {
            InitializeComponent();

            board = new Board();
            settings = new Settings();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            StringBuilder filePath = new StringBuilder(path);
            filePath.Append(Path.DirectorySeparatorChar);
            filePath.Append(".bwsmith");
            filePath.Append(Path.DirectorySeparatorChar);
            filePath.Append(Constant.NAME);

            jsonFileBoard = new StringBuilder(filePath.ToString()).Append(Path.DirectorySeparatorChar).Append(Constant.JSON_FILE).ToString();
            jsonFileSettings = new StringBuilder(filePath.ToString()).Append(Path.DirectorySeparatorChar).Append("Settings.json").ToString();
            if (Directory.Exists(filePath.ToString()))
            {
                if (File.Exists(jsonFileBoard))
                {
                    // read the json file
                    string json = File.ReadAllText(jsonFileBoard);
                    if (json.Length > 0)
                    {
                        board = JsonConvert.DeserializeObject<Board>(json);
                        SetTextBoxText(textBox1, board.Clip1);
                        SetTextBoxText(textBox2, board.Clip2);
                        SetTextBoxText(textBox3, board.Clip3);
                        SetTextBoxText(textBox4, board.Clip4);
                        SetTextBoxText(textBox5, board.Clip5);
                        SetTextBoxText(textBox6, board.Clip6);
                        SetTextBoxText(textBox7, board.Clip7);
                        SetTextBoxText(textBox8, board.Clip8);
                        SetTextBoxText(textBox9, board.Clip9);
                        SetTextBoxText(textBox10, board.Clip10);
                        SetTextBoxText(textBox11, board.Clip11);
                        SetTextBoxText(textBox12, board.Clip12);
                    }
                }
                if (File.Exists(jsonFileSettings))
                {
                    string json = File.ReadAllText(jsonFileSettings);
                    if (json.Length > 0)
                    {
                        settings = JsonConvert.DeserializeObject<Settings>(json);
                        this.Location = settings.Location;
                    }
                }
            }
            else
            {
                // create the directory
                Directory.CreateDirectory(filePath.ToString());
            }

            // register the event that is fired after the key press.
            hook1.KeyPressed += new EventHandler<KeyPressedEventArgs>(ButCopy1_Click);
            hook2.KeyPressed += new EventHandler<KeyPressedEventArgs>(ButCopy2_Click);
            hook3.KeyPressed += new EventHandler<KeyPressedEventArgs>(ButCopy3_Click);
            hook4.KeyPressed += new EventHandler<KeyPressedEventArgs>(ButCopy4_Click);
            hook5.KeyPressed += new EventHandler<KeyPressedEventArgs>(ButCopy5_Click);
            hook6.KeyPressed += new EventHandler<KeyPressedEventArgs>(ButCopy6_Click);
            hook7.KeyPressed += new EventHandler<KeyPressedEventArgs>(ButCopy7_Click);
            hook8.KeyPressed += new EventHandler<KeyPressedEventArgs>(ButCopy8_Click);
            hook9.KeyPressed += new EventHandler<KeyPressedEventArgs>(ButCopy9_Click);
            hook10.KeyPressed += new EventHandler<KeyPressedEventArgs>(ButCopy10_Click);
            hook11.KeyPressed += new EventHandler<KeyPressedEventArgs>(ButCopy11_Click);
            hook12.KeyPressed += new EventHandler<KeyPressedEventArgs>(ButCopy12_Click);

            // register the control + F1 combination as hot key.
            ModifierKeys controlKey = System.Windows.Input.ModifierKeys.Control;
            hook1.RegisterHotKey(controlKey, Keys.F1);
            hook2.RegisterHotKey(controlKey, Keys.F2);
            hook3.RegisterHotKey(controlKey, Keys.F3);
            hook4.RegisterHotKey(controlKey, Keys.F4);
            hook5.RegisterHotKey(controlKey, Keys.F5);
            hook6.RegisterHotKey(controlKey, Keys.F6);
            hook7.RegisterHotKey(controlKey, Keys.F7);
            hook8.RegisterHotKey(controlKey, Keys.F8);
            hook9.RegisterHotKey(controlKey, Keys.F9);
            hook10.RegisterHotKey(controlKey, Keys.F10);
            hook11.RegisterHotKey(controlKey, Keys.F11);
            hook12.RegisterHotKey(controlKey, Keys.F12);

            this.FormClosing += ExitToolStripMenuItem_Click;
        }

        private void SetTextBoxText(TextBox textBox, string value)
        {
            if (value != null)
            {
                textBox.Text = value;
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            settings.Location = this.Location;
            WriteToJson(settings, jsonFileSettings);
            Application.Exit();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About modal = new About();
            modal.Show();
        }

        private void WriteToJson(object obj, string fileLocation)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(@fileLocation))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, obj);
            }
        }

        private void WriteToJson()
        {
            WriteToJson(board, jsonFileBoard);
        }

        private void CopyToClipboard(string value)
        {
            if (value != null)
            {
                Clipboard.SetText(value);
            }
        }

        private void SaveClipboardValue(TextBox textBox, int clipIndex)
        {
            string clipboardText = Clipboard.GetText();
            clipboardText = clipboardText.Replace("\n", "\r\n");

            MethodInfo method = board.GetType().GetMethod("SetClip" + clipIndex);
            method.Invoke(board, new object[] { clipboardText });
            textBox.Text = clipboardText;
            WriteToJson();
        }

        private void ButCopy1_Click(object sender, EventArgs e)
        {
            CopyToClipboard(board.Clip1);
        }

        private void ButSet1_Click(object sender, EventArgs e)
        {
            SaveClipboardValue(textBox1, 1);
        }

        private void ButCopy2_Click(object sender, EventArgs e)
        {
            CopyToClipboard(board.Clip2);
        }

        private void ButSet2_Click(object sender, EventArgs e)
        {
            SaveClipboardValue(textBox2, 2);
        }

        private void ButCopy3_Click(object sender, EventArgs e)
        {
            CopyToClipboard(board.Clip3);
        }

        private void ButSet3_Click(object sender, EventArgs e)
        {
            SaveClipboardValue(textBox3, 3);
        }

        private void ButCopy4_Click(object sender, EventArgs e)
        {
            CopyToClipboard(board.Clip4);
        }

        private void ButSet4_Click(object sender, EventArgs e)
        {
            SaveClipboardValue(textBox4, 4);
        }

        private void ButCopy5_Click(object sender, EventArgs e)
        {
            CopyToClipboard(board.Clip5);
        }

        private void ButSet5_Click(object sender, EventArgs e)
        {
            SaveClipboardValue(textBox5, 5);
        }

        private void ButCopy6_Click(object sender, EventArgs e)
        {
            CopyToClipboard(board.Clip6);
        }

        private void ButSet6_Click(object sender, EventArgs e)
        {
            SaveClipboardValue(textBox6, 6);
        }

        private void ButCopy7_Click(object sender, EventArgs e)
        {
            CopyToClipboard(board.Clip7);
        }

        private void ButSet7_Click(object sender, EventArgs e)
        {
            SaveClipboardValue(textBox7, 7);
        }

        private void ButCopy8_Click(object sender, EventArgs e)
        {
            CopyToClipboard(board.Clip8);
        }

        private void ButSet8_Click(object sender, EventArgs e)
        {
            SaveClipboardValue(textBox8, 8);
        }

        private void ButCopy9_Click(object sender, EventArgs e)
        {
            CopyToClipboard(board.Clip9);
        }

        private void ButSet9_Click(object sender, EventArgs e)
        {
            SaveClipboardValue(textBox9, 9);
        }

        private void ButCopy10_Click(object sender, EventArgs e)
        {
            CopyToClipboard(board.Clip10);
        }

        private void ButSet10_Click(object sender, EventArgs e)
        {
            SaveClipboardValue(textBox10, 10);
        }

        private void ButCopy11_Click(object sender, EventArgs e)
        {
            CopyToClipboard(board.Clip11);
        }

        private void ButSet11_Click(object sender, EventArgs e)
        {
            SaveClipboardValue(textBox11, 11);
        }

        private void ButCopy12_Click(object sender, EventArgs e)
        {
            CopyToClipboard(board.Clip12);
        }

        private void ButSet12_Click(object sender, EventArgs e)
        {
            SaveClipboardValue(textBox12, 12);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButCopy1_Click(sender, e);
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButSet1_Click(sender, e);
        }

        private void copy2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButCopy2_Click(sender, e);
        }

        private void setFromClipboard2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButSet2_Click(sender, e);
        }

        private void copy3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButCopy3_Click(sender, e);
        }

        private void setFromClipboard3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButSet3_Click(sender, e);
        }

        private void copy4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButCopy4_Click(sender, e);
        }

        private void setFromClipboard4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButSet4_Click(sender, e);
        }

        private void copy5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButCopy5_Click(sender, e);
        }

        private void setFromClipboard5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButSet5_Click(sender, e);
        }

        private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HowToUse modal = new HowToUse();
            modal.Show();
        }

        private void copy6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButCopy6_Click(sender, e);
        }

        private void copy7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButCopy7_Click(sender, e);
        }

        private void copy8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButCopy8_Click(sender, e);
        }

        private void copy9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButCopy9_Click(sender, e);
        }

        private void copy10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButCopy10_Click(sender, e);
        }

        private void copy11ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButCopy11_Click(sender, e);
        }

        private void copy12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButCopy12_Click(sender, e);
        }
    }
}
