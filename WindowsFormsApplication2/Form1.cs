using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> myTable = new Dictionary<string, string>();
        Dictionary<string, int> myTable2 = new Dictionary<string, int>();
        List<string> temp_listBox1 = new List<string>();
 
        private int temp_clickedIndex;

        public Form1()
        {
            InitializeComponent();
            var myTable = new Dictionary<string, int>();
            myTable.Clear();
            myTable2.Clear();
            temp_listBox1.Clear();
            temp_clickedIndex = -1;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString() != "")
                if (myTable.ContainsKey(textBox1.Text))
                    MessageBox.Show("\"" + textBox1.Text + "\" は既に存在します。");
                else
                {
                    checkedListBox1.Items.Add(textBox1.Text);
                    add_myTable();
                }
            else
                MessageBox.Show("登録する項目の名前を入力してください。");

        }

        private void add_myTable()
        {
            if (radioButton6.Checked && !radioButton7.Checked)
                myTable.Add(textBox1.Text, numericUpDown1.Value.ToString());
            else
                myTable.Add(textBox1.Text, numericUpDown3.Value.ToString() + "%");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void remove_from_listBox2()
        {
            var index = new List<int>();
            index.Clear();

            for (int i = 0; i < listBox2.Items.Count; i++)
                if (listBox2.Items[i] == checkedListBox1.SelectedItem)
                    index.Add(i);

            for (int i = 0; i < index.Count; i++)
                listBox2.Items.RemoveAt(index[i] - i);          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int temp = checkedListBox1.SelectedIndex;

            if (temp >= 0)
            {
                string str = checkedListBox1.Items[temp].ToString();

                myTable.Remove(str);
                listBox1.Items.Remove(checkedListBox1.SelectedItem);
                temp_listBox1.Remove(checkedListBox1.SelectedItem.ToString());
                remove_from_listBox2();
                checkedListBox1.Items.Remove(checkedListBox1.SelectedItem);

                if (temp - 1 >= 0)
                    checkedListBox1.SelectedIndex = temp - 1;
                else
                {
                    if (checkedListBox1.Items.Count > 0)
                        checkedListBox1.SelectedIndex = temp;
                }

                checkedListBox1_Click(this, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex >= 0)
            {
                string temp_str = checkedListBox1.Items[checkedListBox1.SelectedIndex].ToString();
                string new_str = textBox1.Text.ToString();
                if (new_str != "")
                    if (myTable.ContainsKey(textBox1.Text) && checkedListBox1.Items[checkedListBox1.SelectedIndex].ToString() != new_str)
                        MessageBox.Show("\"" + textBox1.Text + "\" は既に存在します。");
                    else
                    {
                        checkedListBox1.Items[checkedListBox1.SelectedIndex] = new_str;
                        myTable.Remove(temp_str);
                        add_myTable();
                        int index = temp_listBox1.IndexOf(temp_str);
                        if (index >= 0 && index < temp_listBox1.Count)
                            temp_listBox1[index] = new_str;

                        reload_listBox1(this, e);
                        reload_listBox2(temp_str, new_str);
                    }
                else
                {
                    textBox1.Text = temp_str;
                    MessageBox.Show("登録する項目の名前を入力してください。");
                }
            }

        }

        private void reload_listBox1(object sender, EventArgs e)
        {
            switch (get_panel2_checkedRadioButton())
            {
                case "昇順":
                    radioButton4_Click(this, e);
                    break;

                case "降順":
                    radioButton3_Click(this, e);
                    break;

                default:
                    // do nothing
                    break;
            }
        }

        private void reload_listBox2(string temp_str, string new_str)
        {
            if (listBox2.Items.Contains(temp_str))
            {
                int index = listBox2.Items.IndexOf(temp_str);
                listBox2.Items[index] = new_str;
            }
        }

        private void checkedListBox1_Click(object sender, EventArgs e)
        {
            Boolean is_matched = false;
            if (checkedListBox1.SelectedItem != null)
            {
                textBox1.Text = checkedListBox1.SelectedItem.ToString();
                MatchCollection match = Regex.Matches(myTable[textBox1.Text], "%");
                foreach (Match m in match)
                {
                    is_matched = true;
                    break;
                }

                if (is_matched)
                {
                    string value = Regex.Replace(myTable[textBox1.Text], @"[^0-9]", "");
                    numericUpDown3.Value = Convert.ToDecimal(value);
                    radioButton7.Checked = true;
                    numericUpDown1.Value = 0;
                }
                else
                {
                    numericUpDown1.Value = Convert.ToDecimal(myTable[textBox1.Text]);
                    radioButton6.Checked = true;
                    numericUpDown3.Value = 0;
                }
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Boolean not_in_list = true;
            for (int i = 0; i < listBox1.Items.Count; i++)
                if (listBox1.Items[i] == checkedListBox1.SelectedItem)
                {
                    not_in_list = false;
                    break;
                }

            if (not_in_list)
            {
                listBox1.Items.Add(checkedListBox1.SelectedItem);
                temp_listBox1.Add(checkedListBox1.SelectedItem.ToString());
 
            }
            else
            {
                temp_listBox1.Remove(checkedListBox1.SelectedItem.ToString());

                listBox1.Items.Remove(checkedListBox1.SelectedItem);
                remove_from_listBox2();
            }
            reload_listBox1(this, e);
        }

        private string get_panel1_checkedRadioButton()
        {
            var RadioButtonChecked_InGroup = panel1.Controls.OfType<RadioButton>().SingleOrDefault(rb => rb.Checked == true);
            string checked_radioButton = "none";
            // 結果
            if (RadioButtonChecked_InGroup != null)
                checked_radioButton = RadioButtonChecked_InGroup.Text;
            return checked_radioButton;
        }

        private string get_panel2_checkedRadioButton()
        {
            var RadioButtonChecked_InGroup = panel2.Controls.OfType<RadioButton>().SingleOrDefault(rb => rb.Checked == true);
            string checked_radioButton = "none";
            // 結果
            if (RadioButtonChecked_InGroup != null)
                checked_radioButton = RadioButtonChecked_InGroup.Text;
            return checked_radioButton;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
                if (listBox2.SelectedIndex >= 0)                
                    listBox2.Items.Insert(listBox2.SelectedIndex, listBox1.SelectedItem);
                else
                    listBox2.Items.Add(listBox1.SelectedItem);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int temp = listBox2.SelectedIndex;
            listBox2.Items.Remove(listBox2.SelectedItem);

            if (temp - 1 >= 0)
                listBox2.SelectedIndex = temp - 1;
            else
            {
                if (listBox2.Items.Count > 0)
                    listBox2.SelectedIndex = temp;
            }
        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void radioButton5_Click(object sender, EventArgs e)
        {
            switch (get_panel2_checkedRadioButton())
            {
                case "昇順":
                    addedSort_down();
                    break;

                case "降順":
                    addedSort_up();
                    break;

                default:
                    // do nothing
                    break;
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            switch (get_panel2_checkedRadioButton())
            {
                case "昇順":
                    nameSort_down();
                    break;

                case "降順":
                    nameSort_up();
                    break;

                default:
                    // do nothing
                    break;
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            switch (get_panel2_checkedRadioButton())
            {
                case "昇順":
                    numSort_down();
                    break;

                case "降順":
                    numSort_up();
                    break;

                default:
                    // do nothing
                    break;
            }
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            switch (get_panel1_checkedRadioButton())
            {
                case "追加":
                    addedSort_up();
                    break;

                case "名前":
                    nameSort_up();
                    break;

                case "値":
                    numSort_up();
                    break;

                default:
                    // do nothing
                    break;
            }
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            switch (get_panel1_checkedRadioButton())
            {
                case "追加":
                    addedSort_down();
                    break;

                case "名前":
                    nameSort_down();
                    break;

                case "値":
                    numSort_down();
                    break;

                default:
                    // do nothing
                    break;
            }
        }

        private void numSort_up()
        {
            var myTable_sorted = myTable.OrderByDescending((x) => x.Value);
            int i = 0;

            foreach (var index in myTable_sorted)
            {
                listBox1.Items[i] = index.Key.ToString();
                i += 1;
            }
        }

        private void numSort_down()
        {
            var myTable_sorted = myTable.OrderBy((x) => x.Value);
            int i = 0;

            foreach (var index in myTable_sorted)
            {
                listBox1.Items[i] = index.Key.ToString();
                i += 1;
            }
        }

        private void nameSort_up()
        {
            string[] strs = new string[listBox1.Items.Count];
            int i = 0;

            foreach (var index in myTable)
            {
                strs[i] = index.Key.ToString();
                i += 1;
            }

            StringComparer cmp = StringComparer.OrdinalIgnoreCase;
            Array.Sort(strs, cmp);

            for (int j = 0; j < listBox1.Items.Count; j++)
                listBox1.Items[j] = strs[listBox1.Items.Count - 1 - j];
        }

        private void nameSort_down()
        {
            string[] strs = new string[listBox1.Items.Count];
            int i = 0;

            foreach (var index in myTable)
            {
                strs[i] = index.Key.ToString();
                i += 1;
            }

            StringComparer cmp = StringComparer.OrdinalIgnoreCase;
            Array.Sort(strs, cmp);

            for (int j = 0; j < listBox1.Items.Count; j++)
                listBox1.Items[j] = strs[j];
        }

        private void addedSort_up()
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
                listBox1.Items[i] = temp_listBox1[listBox1.Items.Count - 1 - i];
        }

        private void addedSort_down()
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
                listBox1.Items[i] = temp_listBox1[i];
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox2.SelectedIndex == temp_clickedIndex)
            {
                listBox2.ClearSelected();
                temp_clickedIndex = -1;
            }
            else
                temp_clickedIndex = listBox2.SelectedIndex;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int need_hp = 0;
            int max_value = 10000000;  // 10000000が最適値
            decimal my_hp = numericUpDown2.Value;
            string str = need_hp.ToString();
            Boolean has_dead = false;
            Boolean has_limited = false;
            var messageList = new List<string>();
            messageList.Clear();



            if (my_hp >= 1 && listBox2.Items.Count >= 1)
            {
                label2.Text = calc_damage(Convert.ToInt32(my_hp)).ToString();
                var remaining_hp = my_hp - calc_damage(Convert.ToInt32(my_hp));
                if (remaining_hp < 0)
                {
                    label5.Text = "0";
                    need_hp = calc_need_hp(Convert.ToInt32(my_hp), max_value);
                    has_dead = true;
                }
                else
                {
                    label5.Text = remaining_hp.ToString();
                    need_hp = 0;
                }

                if (need_hp >= max_value)
                {
                    str = need_hp.ToString() + "+";
                    has_limited = true;
                }

                else
                    str = need_hp.ToString();

                label7.Text = str;              
            }

            if (my_hp <= 0)
                messageList.Add("HPを指定してください。");
            if (listBox2.Items.Count <= 0)
                messageList.Add("ダメージが追加されていません。");

            if (messageList.Count >= 1)
                MessageBox.Show(messageList2str(messageList));

            if (messageList.Count <= 0 && has_dead)
            {
                int temp_value = need_hp - Convert.ToInt32(my_hp);
                string temp_str = "GAME OVER";
                if (!has_limited)
                    temp_str += "\n現在のHPに対して +HP (" + temp_value +") が必要です。";

                MessageBox.Show(temp_str);
            }
        }

        private string messageList2str(List<string> list)
        {
            string message = "";
            for (int i = 0; i < list.Count; i++)
                message += list[i] + "\n";

            return message;
        }

        private int calc_need_hp(int my_hp, int max_value)
        {
            int need_hp = my_hp;

            while (true)
            {
                int damage = calc_damage(need_hp);
                if (damage < need_hp || need_hp >= max_value)
                    break;

                need_hp += 1;
    
            }
            return need_hp;
        }

        private int calc_damage(int my_hp)
        {
            double damage = 0;
            double damage_temp;
            double hp_temp = my_hp;
            Boolean is_matched;

            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                MatchCollection match = Regex.Matches(myTable[listBox2.Items[i].ToString()], "%");
                is_matched = false;
                foreach (Match m in match)
                    is_matched = true;

                damage_temp = 0;

                if (is_matched)
                {
                    if (hp_temp > 0)
                        damage_temp = Convert.ToDouble(hp_temp) * (Convert.ToDouble(Regex.Replace(myTable[listBox2.Items[i].ToString()], @"[^0-9]", "")) / 100);
                }
                else
                    damage_temp = Convert.ToDouble(myTable[listBox2.Items[i].ToString()]);

                if (listBox3.Items.Count >= 1)
                    damage_temp *= calc_cuttingRate();

                damage += damage_temp;
                hp_temp -= damage_temp;
            }

            return Convert.ToInt32(Math.Round(damage));
        }

        private double calc_cuttingRate()
        {
            string[] strs = new string[myTable2.Count];
            int i = 0;
            double cuttingRate = 1.0;

            foreach (var index in myTable2)
            {
                strs[i] = index.Key.ToString();
                i += 1;
            }
            for (int j = 0; j < myTable2.Count; j++)
            {
                for (int k = 0; k < myTable2[strs[j]]; k++)
                    cuttingRate *= (1.0 - double.Parse(strs[j])/100);
            }
            return cuttingRate;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            listBox3.Items.Clear();
            listBox3.Items.Add("なし");
            myTable2.Clear();
            button8.Enabled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                string temp_str = Regex.Replace(comboBox1.SelectedItem.ToString(), @"[^0-9]", "");

                if (myTable2.ContainsKey(temp_str))
                { 
                    int temp_cnt = myTable2[temp_str];
                    myTable2.Remove(temp_str);
                    myTable2.Add(temp_str, temp_cnt + 1);
                    
                }
                else
                    myTable2.Add(temp_str, 1);

                reload_listBox3();               
            }
        }

        private void reload_listBox3()
        {
            string[] strs = new string[myTable2.Count];
            int i = 0;

            foreach (var index in myTable2)
            {
                strs[i] = index.Key.ToString();
                i += 1;
            }

            for (int j = 0; j < strs.Length; j++)
            {
                if (myTable2[strs[j]] == 1)
                {
                    listBox3.Items[j] = strs[j];
                    listBox3.Items.Add("");
                }

                else
                    listBox3.Items[j] = strs[j] + "*" + myTable2[strs[j]];
                
            }

            if (myTable2.ContainsKey("100"))
            {
                listBox3.Items.Clear();
                listBox3.Items.Add(comboBox1.SelectedItem.ToString());
                button8.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = true;
            numericUpDown3.Enabled = false;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = false;
            numericUpDown3.Enabled = true;
        }
    }
}
