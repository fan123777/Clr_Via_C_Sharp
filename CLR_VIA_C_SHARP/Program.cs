using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLR_VIA_C_SHARP
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. CLR Basics
            if(false)
            {
                CLR_Basics.CodeExecutionModelInCLR.main();
                _1_CLR_Basics._2_ApplicationDeployment.ApplicationDeployment.main();
                _1_CLR_Basics._3_SharedAndStrongNameAssembly.SharedAndStrongNameAssembly.main();
            }
            
            // 2. Type Design
            if (false)
            {
                _2_Type_Design._4_TypeBasics.TypeBasics.main();
                _2_Type_Design._5_PrimitiveReferenceSignificantTypes.PrimitiveReferenceSignificantTypes.main();
                _2_Type_Design._6_MembersAndTypes.MembersAndTypes.main();
                _2_Type_Design._7_ConstantsAndFields.ConstantsAndFields.main();
                _2_Type_Design._8_Methods.Methods.main();
                _2_Type_Design._9_Parameters.Parameters.main();
                _2_Type_Design._10_Properties.Properties.main();
                _2_Type_Design._11_Events.Events.main();
                _2_Type_Design._12_Generic.Generic.main();
                _2_Type_Design._13_Interfaces.Interfaces.main();
                _2_Type_Design.Testing.Testing.main();
            }
            
            // 3. Base Data Types
            if (false)
            {
                _3_BaseDataTypes._14_Strings.Strings.main();
                _3_BaseDataTypes._15_EnumsBitFlags.EnumsBitFlags.main();
                _3_BaseDataTypes._16_Arrays.Arrays.main();
                _3_BaseDataTypes._17_Delegates.Delegates.main();
                _3_BaseDataTypes._18_CustomAttributes.CustomAttributes.main();
            }
            _3_BaseDataTypes._19_NullAbleValueType.NullAbleValueType.main();
        }
    }
}
