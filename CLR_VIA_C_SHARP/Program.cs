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
            }
            _2_Type_Design._9_Parameters.Parameters.main();
        }
    }
}
