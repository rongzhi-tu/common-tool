using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaToCsharpEntity
{
    [Serializable]
    class SettingMode 
    {
        /// <summary>
        /// 输出目录
        /// </summary>
        public string StrOutPutDir { get; set; }

        /// <summary>
        /// 文件保存的命名空间
        /// </summary>
        public string StrSpaceName { get; set; }

        /// <summary>
        /// 属性注释 
        /// </summary>
        public string StrPropNote { get; set; }

        /// <summary>
        /// 类型列表
        /// </summary>
        public List<TypeMode> TypeModes { get; set; }

        /// <summary>
        /// 属性转换时是滞首字母转大写
        /// </summary>
        public bool isFirstUpper { get; set; }

        /// <summary>
        /// 是否加载文件时就自动转换
        /// </summary>
        public bool isAutoTransfor { get; set; }
      
    }

    [Serializable]
    class TypeMode
    {
        public string UsingSpace { get; set; }
        public string JavaName { get; set; }
        public string CSharpName { get; set; }
    }
}
