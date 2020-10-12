using System;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace Test
{
    #region MainClass
    public class TestEnumClass
    {
        #region fields
        int? IToday;
        string SToday;
        string MemberToday;
        string DescriptionToday;
        #endregion
        
        
        #region Void Run
        public void Run()
        {
            
            IToday = (int)TestEnum.Monday;
            SToday = TestEnum.Monday.ToString();
            MemberToday = GetEnum.GetEnumMember(TestEnum.Monday);
            DescriptionToday = GetEnum.GetEnumDescription(TestEnum.Monday);

            Console.WriteLine("IToday is " + IToday);
            Console.WriteLine("SToday is " + SToday);
            Console.WriteLine("MemberToday is " + MemberToday);
            Console.WriteLine("DescriptionToday is " + DescriptionToday);
            Console.WriteLine("Test" + typeof(bool).ToString());
            Console.ReadKey();
        }
        #endregion
    }

    #endregion 

    #region MyTestEnum
    public enum TestEnum
    {
        [Description("周一")]
        [EnumMember(Value ="星期一")]
        Monday,
        [Description("周二")]
        [EnumMember(Value = "星期二")]
        Tuesday,
        [Description("周三")]
        [EnumMember(Value = "星期三")]
        Wensday,
        [Description("周四")]
        [EnumMember(Value = "星期四")]
        Thurseday,
        [Description("周五")]
        [EnumMember(Value = "星期五")]
        Friday,
        [Description("周六")]
        [EnumMember(Value = "星期六")]
        Saturday,
        [Description("周日")]
        [EnumMember(Value = "星期日")]
        Sunday
    }
    #endregion

    #region Extension
    public static class GetEnum
    {
        public static string GetEnumMember(this Enum enumvalue)
        {
            var type = enumvalue.GetType();
            var info = type.GetField(enumvalue.ToString());
            var da = (EnumMemberAttribute[])(info.GetCustomAttributes(typeof(EnumMemberAttribute), false));
            if (da.Length > 0)
                return da[0].Value;
            else
                return string.Empty;

        }
        public static string GetEnumDescription(this Enum enumvalue)
        {
            var type = enumvalue.GetType();
            var info = type.GetField(enumvalue.ToString());
            var da = (DescriptionAttribute[])(info.GetCustomAttributes(typeof(DescriptionAttribute), false));
            if (da.Length > 0)
                return da[0].Description;
            else
                return string.Empty;
        }

    }


    #endregion
}
