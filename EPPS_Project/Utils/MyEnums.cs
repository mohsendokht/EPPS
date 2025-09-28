using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionPlanning
{
    public static class MyEnums
    {
        public enum ListFormCaller
        {
            LFC_NONE,
            LFC_OPERATRIONSTITLES,
            LFC_PRODUCTIONPARTS,
            LFC_TESTUNITS,
            LFC_UNITSRELATIONS,
            LFC_PRIMARYMATERIALS,
            LFC_MACHINES,
            LFC_CONTRACTORS,
            LFC_STORES,
            LFC_HALTREASON,
            LFC_MACHINENOTAVAILABLEREASON,
            LFC_HOLIDAYS,
            LFC_ACTINGCALENDARS,
            LFC_SUPPLIERS,
            LFC_OPERATORS,
            LFC_PRODUCTS,
            LFC_PRODUCTIONBATCHS,
            LFC_MACHINENOTAVAILABLE,
            LFC_PRODUCTIONPLANING,
            LFC_OPERATIONUNITS,
            LFC_BATCHSPRODUCTIONPROGRESS,
            LFC_CUSTOMERS,
            LFC_OEE
        }


        public enum EnumExecutionMethod : short
        {
            EM_MACHINE = 1,
            EM_NONMACHINE = 2,
            EM_CONTRACTOR = 3
        }

        public enum EnumTimeType: short
        {
            TT_SECOND = 1,
            TT_MINUTE = 2,
            TT_HOUR = 3,
            TT_DAY = 4,
            TT_WEEK = 5,
            TT_MONTH = 6
        }

        public enum EnumRelationType: short
        {
            RT_FS = 1,
            RT_FF = 2,
            RT_SS = 3,
            RT_SF = 4,
            RT_ASAP = 5
        }

        public enum EnumParticularDayType : short
        {
            PDT_IS_NOT = -1,  // IS NOT Particular.
            PDT_UNKNOWN = 0,
            PDT_OFF_DAY = 1,  // Particular Off Day  روز خاص تعطيل مي باشد
            PDT_WORK_DAY = 2  // Particular Work Day  روز خاص کاری مي باشد
        }

        public enum EnumDayType : short
        {
            DT_OFF_DAY = 1,    // Normal Off weekday: Friday
            DT_WORK_DAY = 2    // Normal working weekday: Saturday - Thursday 
        }
    }
}
