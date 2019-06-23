using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JavaToCsharpEntity
{
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();
            
        }


        /// <summary>
        /// 文件初始化加载文件内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSrcPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string filePath = txtSrcPath.Text;
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("请确保文件存在");
                    return;
                }
                lstContent.Items.Clear();
                string content = FileUtils.Read(filePath, Encoding.UTF8);
                saveCacheData(ORI_CONTENT, content);
                saveCacheData(LOAD_FILE_PATH, filePath);
                if (!string.IsNullOrEmpty(content))
                {
                    string[] items = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    foreach (var item in items)
                    {
                        lstContent.Items.Add(item);
                    }
                }
                if (chxAutoTransfor.Checked)
                {
                    btnTransfor_Click(null,null);
                }

            }
        }
   
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dilog = new FolderBrowserDialog();
            dilog.Description = "请选择文件夹";
            if (dilog.ShowDialog() == DialogResult.OK || dilog.ShowDialog() == DialogResult.Yes)
            {
                txtDesPath.Text = dilog.SelectedPath + "\\";
            }
        }

        Dictionary<string, Object> data = new Dictionary<string, object>();
        private void saveCacheData(string key, object value)
        {
            if (data.ContainsKey(key))
            {
                data.Remove(key);
            }
            data.Add(key,value);
        }
        /// <summary>
        /// 最原始的内容
        /// </summary>
        public static string ORI_CONTENT = "ORI_CONTENT";
        /// <summary>
        /// 转换过后的内容  即需要保存的数据内容
        /// </summary>
        public static string SAVE_CONTENT = "SAVE_CONTENT";
        /// <summary>
        /// 加载的文件路径
        /// </summary>
        public static string LOAD_FILE_PATH = "LOAD_FILE_PATH";
        /// <summary>
        /// 设置的模型
        /// </summary>
        public static string SETTING_MODE = "SETTING_MODE";
        /// <summary>
        /// config file 
        /// </summary>
        public static string setting_file = System.Environment.CurrentDirectory + "\\setting_config";

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                FileUtils.CreateDir(txtDesPath.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("请选择正确的保存路径");
                return;
            }

            if (data.Count == 0 || !data.ContainsKey(SAVE_CONTENT))
            {
                MessageBox.Show("没有需要保存的内容");
                return;
            }
            string filePath = data[LOAD_FILE_PATH].ToString();
            string fileName =FileUtils.getFileNameNoSuffix(filePath);

            string saveFileName = fileName + ".cs";
            string saveFilePath = txtDesPath.Text + saveFileName;
            if (FileUtils.Write(saveFilePath, data[SAVE_CONTENT].ToString()))
            {
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存失败");
            }
         
        }

        private void button2_Click(object sender, EventArgs e)

        {
            SettingMode mode = collectSettingMode();
            saveCacheData(SETTING_MODE, mode);
            FileUtils.saveObject(mode, setting_file);
            MessageBox.Show("保存成功！");

        }

        private SettingMode collectSettingMode()
        {
            SettingMode mode = new SettingMode();

            mode.StrOutPutDir = txtDesPath.Text;
            mode.StrSpaceName = txtSpace.Text;
            mode.StrPropNote = txtPropertyNote.Text;
            mode.isFirstUpper = chxFirstUpper.Checked;
            mode.isAutoTransfor = chxAutoTransfor.Checked;

            mode.TypeModes = new List<TypeMode>();

            TypeMode intMode = new TypeMode();
            intMode.JavaName = "Integer";
            intMode.CSharpName = txtInt.Text;
            intMode.UsingSpace = "using System;";
            mode.TypeModes.Add(intMode);


            TypeMode decimalMode = new TypeMode();
            decimalMode.JavaName = "BigDecimal";
            decimalMode.CSharpName = txtBIgDecimal.Text;
            decimalMode.UsingSpace = "using System;";
            mode.TypeModes.Add(decimalMode);


            TypeMode shortMode = new TypeMode();
            shortMode.JavaName = "Short";
            shortMode.CSharpName = txtShort.Text;
            shortMode.UsingSpace = "using System;";
            mode.TypeModes.Add(shortMode);


            TypeMode stringMode = new TypeMode();
            stringMode.JavaName = "String";
            stringMode.CSharpName = txtString.Text;
            stringMode.UsingSpace = "using System;";
            mode.TypeModes.Add(stringMode);

            TypeMode longMode = new TypeMode();
            longMode.JavaName = "Long";
            longMode.CSharpName = txtLong.Text;
            longMode.UsingSpace = "using System;";
            mode.TypeModes.Add(longMode);

            TypeMode dateMode = new TypeMode();
            dateMode.JavaName = "Date";
            dateMode.CSharpName = txtDate.Text;
            dateMode.UsingSpace = "using System;";
            mode.TypeModes.Add(dateMode);

            TypeMode listMode = new TypeMode();
            listMode.JavaName = "List";
            listMode.CSharpName = txtList.Text;
            listMode.UsingSpace = "using System.Collections.Generic;";
            mode.TypeModes.Add(listMode);

            TypeMode mapMode = new TypeMode();
            mapMode.JavaName = "Map";
            mapMode.CSharpName = txtMap.Text;
            mapMode.UsingSpace = "using System;";
            mode.TypeModes.Add(mapMode);

            TypeMode floatMode = new TypeMode();
            floatMode.JavaName = "Float";
            floatMode.CSharpName = txtFloat.Text;
            floatMode.UsingSpace = "using System;";
            mode.TypeModes.Add(floatMode);


            TypeMode doubleMode = new TypeMode();
            doubleMode.JavaName = "Double";
            doubleMode.CSharpName = txtDouble.Text;
            doubleMode.UsingSpace = "using System;";
            mode.TypeModes.Add(doubleMode);


            TypeMode charMode = new TypeMode();
            charMode.JavaName = "Character";
            charMode.CSharpName = txtCharacter.Text;
            charMode.UsingSpace = "using System;";
            mode.TypeModes.Add(charMode);


            TypeMode byteMode = new TypeMode();
            byteMode.JavaName = "Byte";
            byteMode.CSharpName = txtCharacter.Text;
            byteMode.UsingSpace = "using System;";
            mode.TypeModes.Add(byteMode);
            return mode;
        }

        private void loadSettingConifg()
        {
            try
            {
                SettingMode mode = FileUtils.readObject<SettingMode>(setting_file);
                saveCacheData(SETTING_MODE,mode);

                txtDesPath.Text = mode.StrOutPutDir;
                txtSpace.Text = mode.StrSpaceName;
                txtPropertyNote.Text = mode.StrPropNote;
                chxFirstUpper.Checked = mode.isFirstUpper;
                chxAutoTransfor.Checked = mode.isAutoTransfor;
                #region
                foreach (var item in mode.TypeModes)
                {
                    setUiByTypeMode(item);
                }
                #endregion
            }
            catch { 

            }   
            
        }
        private void setUiByTypeMode(TypeMode typeMode)
        {
            #region set mode into ui display
            switch (typeMode.JavaName)
            {
                case "Integer":
                    txtInt.Text = typeMode.CSharpName;
                    break;
                case "BigDecimal":
                    txtBIgDecimal.Text = typeMode.CSharpName;
                    break;
                case "Short":
                    txtShort.Text = typeMode.CSharpName;
                    break;
                case "String":
                    txtString.Text = typeMode.CSharpName;
                    break;
                case "Long":
                    txtLong.Text = typeMode.CSharpName;
                    break;
                case "Date":
                    txtDate.Text = typeMode.CSharpName;
                    break;
                case "List":
                    txtList.Text = typeMode.CSharpName;
                    break;
                case "Map":
                    txtMap.Text = typeMode.CSharpName;
                    break;
                case "Float":
                    txtFloat.Text = typeMode.CSharpName;
                    break;
                case "Double":
                    txtDouble.Text = typeMode.CSharpName;
                    break;
                case "Character":
                    txtCharacter.Text = typeMode.CSharpName;
                    break;
                case "Byte":
                    txtCharacter.Text = typeMode.CSharpName;
                    break;
                default:
                    break;
            }
            #endregion
        }
        private void FMain_Load(object sender, EventArgs e)
        {
            if (FileUtils.Exists(setting_file))
            {
                loadSettingConifg();
            }
            else
            {
                SettingMode mode = collectSettingMode();
                saveCacheData(SETTING_MODE, mode);
            }
        }

        private void btnTransfor_Click(object sender, EventArgs e)
        {
            if (!data.ContainsKey(ORI_CONTENT))
            {
                MessageBox.Show("请先设置加载的文件");
                return;
            }
            lstContent.Items.Clear();
            string content = data[ORI_CONTENT].ToString();

            string resultTransfor = transforContent(content);
            saveCacheData(SAVE_CONTENT, resultTransfor);

            string[] items = resultTransfor.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var item in items)
            {
                lstContent.Items.Add(item);
            }
        }

        private string transforContent(string content)
        {
            //去掉头 ，package ... importz
            //package com.wdhis.mmis.model.basic;  import java.io.Serializable;
            string result = Regex.Replace(content,"((package|import) [a-z0-9A-Z]+(\\.[a-z0-9A-Z]+)+;\\r\\n)","");

            //类的转化  更改继承表示，implements Serializable去掉加上特定注释 extend改成: public class转成public partial class 
            result = Regex.Replace(result, " implements Serializable", "");
            result = Regex.Replace(result, " extends ", " : ");
            result = Regex.Replace(result, "public class ", "public partial class ");

            Regex r = new Regex(@"\{");
            result = r.Replace(result, "\r\n{", 1);
          
            //去掉原来JAVA的get set方法
            /**
             * This method was generated by MyBatis Generator.
             * This method returns the value of the database column MMIS_DIC_MED_STO_ATTR_VAULE.VALUE_DATETIME
             *
             * @return the value of MMIS_DIC_MED_STO_ATTR_VAULE.VALUE_DATETIME
             *
             * @mbg.generated Mon Aug 14 17:35:21 CST 2017
             */
            //public Date getValueDatetime() {
            //    return valueDatetime;
            //}
            //result = Regex.Replace(result, @"(/\*[\s\S]*?\*/[\r\n|\n])", "");//去多行注释
            result = Regex.Replace(result, @"/\*[^/]*?\*/[\s\r\n]+public [a-zA-Z]+ (set|get).+\(.*\).*\{[^\}]*\}", "");//去掉有多行注释的get set方法
            result = Regex.Replace(result, @"//[\s\r\n]+public [a-zA-Z<>]+ (set|get).+\(.*\).*\{[^\}]*\}", ""); ;//去掉有单行注释的get set方法
            result = Regex.Replace(result, @"public [a-zA-Z<>]+ (set|get).+\(.*\).*\{[^\}]*\}", ""); ;//去掉仅有get set方法
            result = Regex.Replace(result, @"(\r\n\r\n    \r\n\r\n)*", "");//去掉多余的回车符
            result = Regex.Replace(result, @"\r\n", "\r\n\t");//去掉多余的回车符
            result = Regex.Replace(result, ";", "");//去掉分号 C#中模型不用
            //字段处理  类型，注释，get set转换
            SettingMode mode = (SettingMode)data[SETTING_MODE];
            //加入命名空间
            result = (mode.StrSpaceName.StartsWith("namespace") ? mode.StrSpaceName : "namespace " + mode.StrSpaceName) + "\r\n{\r\n" + result;
            foreach (var item in mode.TypeModes)
            {
                result = handleGetSetAndNoted(result,item,mode);
            }
            return result + "\r\n}";
        }

        private string handleGetSetAndNoted(string content, TypeMode typeMode,SettingMode mode) {
            string result = content;
            //添加引用空间
            if (!result.Contains(typeMode.UsingSpace))
            {
                result = typeMode.UsingSpace + "\r\n" + result;
            }
            //类型替换 
            result = Regex.Replace(result, @" " + typeMode.JavaName + " ", " " + typeMode.CSharpName + " ");

            //get set  注释处理
            string partten = @"(\s*)private "+ typeMode.CSharpName + " ([a-zA-Z0-9]+)";
            //if (typeMode.CSharpName.EndsWith("?") && typeMode.CSharpName.Length > 1)
            //{
            //    int index = typeMode.CSharpName.LastIndexOf("?");
            //    partten = @"(\s*)private " + typeMode.CSharpName.Substring(0, index) + "\\" + typeMode.CSharpName.Substring(index) + " ([a-zA-Z0-9 ]+)";
            //}
            Regex r1 = new Regex(partten);
            MatchCollection matchCollection = r1.Matches(result);
            foreach (Match m in matchCollection)
            {
                var matchValue = m.Value;//private Int32 gnameId
                if (matchValue.Contains("?") && matchValue.Length > 1)
                {
                    int index = matchValue.LastIndexOf("?");
                    matchValue = matchValue.Substring(0, index) + "\\" + matchValue.Substring(index);
                }
                var spaceKey = m.Result("$1");
                var javaFieldName = m.Result("$2");
                var cSharpFieldName = javaFieldName.Substring(0, 1);
                if (mode.isFirstUpper)
	            {
                    cSharpFieldName = cSharpFieldName.ToUpper();
	            }
                if (javaFieldName.Length > 1)
                {
                    cSharpFieldName = cSharpFieldName + javaFieldName.Substring(1);
                }
                var replaceValue = spaceKey + "public " + typeMode.CSharpName + " " + cSharpFieldName + " { get; set; }";
                if (!string.IsNullOrEmpty(mode.StrPropNote))
                {
                    replaceValue = "\r\n\t    " + Regex.Replace(mode.StrPropNote, "\"fieldName\"", "\"" + javaFieldName + "\"") + replaceValue;
                }
                result = Regex.Replace(result, @"" + matchValue, replaceValue);
            }
           

            return result;
        }

        private void btnCopy_Click_1(object sender, EventArgs e)
        {
            if (data.ContainsKey(SAVE_CONTENT))
	        {
                Clipboard.SetDataObject(data[SAVE_CONTENT]);
            }
            else if (data.ContainsKey(ORI_CONTENT))
	        {
                Clipboard.SetDataObject(data[ORI_CONTENT]);
	        }
            else
            {
                MessageBox.Show("请先加载文件");
                return;
            }
            MessageBox.Show("复制成功！");
        }
        
    }
}
